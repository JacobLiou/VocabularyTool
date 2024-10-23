using EntryTranslator.Dialogs;
using EntryTranslator.Properties;
using EntryTranslator.ResourceOperations;
using EntryTranslator.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace EntryTranslator
{
    public sealed partial class MainWindow : Form
    {
        private readonly string _defaultWindowTitle;

        private ResourceHolder _currentResource;

        private ResourceHolder CurrentResource
        {
            get { return _currentResource; }
            set
            {
                this.InvokeIfRequired(_ =>
                {
                    if (_currentResource != null)
                    {
                        _currentResource.LanguageChange -= OnCurrentResourceLanguageChange;
                        _currentResource.DirtyChanged -= _currentResource_DirtyChanged;
                    }

                    _currentResource = value;

                    if (_currentResource != null)
                    {
                        _currentResource.LanguageChange += OnCurrentResourceLanguageChange;
                        _currentResource.DirtyChanged += _currentResource_DirtyChanged;

                        _currentResource.EvaluateAllRows(
                            languageSettings1.EnabledLanguages.Select(x => x.Name).ToArray());
                    }

                    resourceGrid1.CurrentResource = value;
                    resourceGrid1.SetVisibleLanguageColumns(
                        languageSettings1.EnabledLanguages.Select(x => x.Name).ToArray());
                    UpdateMenuStrip();
                });
            }
        }

        private SearchParams _currentSearch;

        public SearchParams CurrentSearch => _currentSearch;

        public ResourceLoader ResourceLoader { get; }

        public MainWindow()
        {
            InitializeComponent();

            _defaultWindowTitle = $"{Text} {Assembly.GetAssembly(typeof(MainWindow)).GetName().Version.ToString(2)}";

            ResourceLoader = new ResourceLoader();
            ResourceLoader.ResourceLoadProgress += OnResourceLoadProgress;
            ResourceLoader.ResourcesChanged += OnResourceLoaderOnResourcesChanged;

            languageSettings1.EnabledLanguagesChanged += (sender, args) =>
            {
                if (resourceGrid1.CurrentResource == null) return;

                var languageIds = languageSettings1.EnabledLanguages.Select(x => x.Name).ToArray();
                resourceGrid1.CurrentResource.EvaluateAllRows(languageIds);
                resourceGrid1.SetVisibleLanguageColumns(languageIds);
                resourceGrid1.Refresh();
            };

            Settings.Binder.Subscribe((sender, args) => ResourceLoader.HideEmptyResources = args.NewValue,
                settings => settings.HideEmptyResources, this);
            Settings.Binder.Subscribe((sender, args) => ResourceLoader.HideNontranslatedResources = args.NewValue,
                settings => settings.HideNontranslatedResources, this);
            Settings.Binder.Subscribe((sender, args) => resourceGrid1.ShowNullValuesAsGrayed = args.NewValue,
                settings => settings.ShowNullValuesAsGrayed, this);

            Settings.Binder.SendUpdates(this);

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetAssembly(typeof(MainWindow)).Location);
        }

        private void _currentResource_DirtyChanged(object sender, EventArgs e)
        {
            UpdateTitlebar();
        }

        private void OnCurrentResourceLanguageChange(object sender, EventArgs eventArgs)
        {
            this.InvokeIfRequired(x =>
            {
                languageSettings1.RefreshLanguages(ResourceLoader.GetUsedLanguages(), true);
                UpdateMenuStrip();
            });
        }

        private void LoadReferenceAssemblies()
        {
            OnResourceLoadProgress(this, new ResourceLoadProgressEventArgs("加载程序集"));

            var assembliesToLoad = new List<string>();

            if (Settings.Default.ReferencePaths != null)
            {
                foreach (var path in Settings.Default.ReferencePaths.Cast<string>().Where(Directory.Exists))
                {
                    assembliesToLoad.AddRange(Directory.EnumerateFiles(path, "*.dll", SearchOption.TopDirectoryOnly));
                }
            }

            if (Settings.Default.ReferencePathsFromResourceDir && Directory.Exists(ResourceLoader.OpenedPath))
            {
                assembliesToLoad.AddRange(Directory.EnumerateFiles(ResourceLoader.OpenedPath, "*.dll", SearchOption.AllDirectories));
            }

            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic)
                .Select(x => x.Location).Distinct().ToList();

            assembliesToLoad = assembliesToLoad.Select(x => x.ToLowerInvariant()).Distinct().ToList();

            if (assembliesToLoad.Count > 300 && MessageBox.Show(
                string.Format(
                    "使用当前设置，此操作将尝试加载{0}程序集。你真的要把它们都装上吗？如果选择no，将跳过加载",
                    assembliesToLoad.Count),
                "", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                OnResourceLoadProgress(this, new ResourceLoadProgressEventArgs("完成", null, 0, 0));
                return;
            }

            var count = 0;
            foreach (var filename in assembliesToLoad)
            {
                count++;
                OnResourceLoadProgress(this, new ResourceLoadProgressEventArgs("加载程序集...",
                    Path.GetFileName(filename), count, assembliesToLoad.Count));

                try
                {
                    if (loadedAssemblies.All(x => !string.Equals(x, filename, StringComparison.OrdinalIgnoreCase)))
                    {
                        Assembly.LoadFile(filename);
                        loadedAssemblies.Add(filename);
                    }
                }
                catch (NotSupportedException) { }
                catch (BadImageFormatException) { }
                catch (FileLoadException) { }
            }

            OnResourceLoadProgress(this, new ResourceLoadProgressEventArgs("完成", null, 0, 0));
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!Settings.Default.WindowSize.IsEmpty)
            {
                Location = Settings.Default.WindowLocation;
                Size = Settings.Default.WindowSize;
                WindowState = Settings.Default.WindowState;
            }

            if (Settings.Default.SplitterMain > 10)
                splitContainerMain.SplitterDistance = Settings.Default.SplitterMain;

            Opacity = 1;

            LoadResourcesFromFolder($@"{AppDomain.CurrentDomain.BaseDirectory}LangDic");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.LastOpenedDirectory = ResourceLoader.OpenedPath ?? string.Empty;

            switch (WindowState)
            {
                case FormWindowState.Normal:
                    Settings.Default.WindowLocation = Location;
                    Settings.Default.WindowSize = Size;
                    Settings.Default.WindowState = WindowState;
                    break;

                case FormWindowState.Maximized:
                    Settings.Default.WindowState = WindowState;
                    break;
            }

            Settings.Default.SplitterMain = splitContainerMain.SplitterDistance;

            Settings.Default.Save();

            if (!ResourceLoader.CanClose())
                e.Cancel = true;
        }

        public void SetCurrentSearch(SearchParams value)
        {
            _currentSearch = value;
            resourceGrid1.CurrentSearch = _currentSearch;

            if (value != null)
            {
                MessageBox.Show(string.Format("搜索到字符串 {0}", value.Text),
                                "搜索",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void LoadResourcesFromFolder(string path)
        {
            if (!ResourceLoader.CanClose())
                return;

            Enabled = false;
            toolStripStatusLabel1.Text = string.Format("打开 {0}...", path);
            Application.DoEvents();

            ResourceLoader.OpenProject(path);
            if (ResourceLoader.Resources.Count() != 0)
                CurrentResource = ResourceLoader.Resources.First();

            Enabled = true;
        }

        private void OnResourceLoadProgress(object sender, ResourceLoadProgressEventArgs args)
        {
            this.InvokeIfRequired(_ =>
            {
                toolStripStatusLabelCurrentItem.Text = args.CurrentlyProcessedItem ?? string.Empty;
                toolStripStatusLabel1.Text = args.CurrentProcess ?? string.Empty;
                if (args.Progress < args.ProgressTop)
                {
                    toolStripProgressBar1.Visible = true;
                    if (toolStripProgressBar1.Maximum != args.ProgressTop)
                        toolStripProgressBar1.Maximum = args.ProgressTop;
                    toolStripProgressBar1.Value = args.Progress;
                }
                else
                {
                    toolStripProgressBar1.Visible = false;
                }
            });
        }

        private void OnResourceLoaderOnResourcesChanged(object sender, EventArgs args)
        {
            (this).InvokeIfRequired(_ =>
            {
                var nothingLoaded = string.IsNullOrEmpty(ResourceLoader.OpenedPath);
                searchToolStripMenuItem.Enabled = !nothingLoaded;

                UpdateTitlebar();

                CurrentResource = null;

                var usedLanguages = ResourceLoader.GetUsedLanguages().ToList();

                languageSettings1.RefreshLanguages(usedLanguages, false);

                LoadReferenceAssemblies();
            });
        }

        private void UpdateTitlebar()
        {
            Text = $"{_defaultWindowTitle}{(CurrentResource?.IsDirty == true ? " - *" : "")}";
        }

        #region 菜单快捷栏事件

        private void UpdateMenuStrip()
        {
            var notNull = _currentResource != null;
            keysToolStripMenuItem.Enabled = notNull;
            addNewKeyToolStripMenuItem.Enabled = notNull;
            languagesToolStripMenuItem.Enabled = notNull;
            toolStripMenuItemGT.Enabled = notNull;
            groupBoxSearch.Enabled = notNull;

            removeLanguageToolStripMenuItem.DropDownItems.Clear();
            addLanguageToolStripMenuItem.DropDownItems.Clear();

            if (_currentResource == null) return;

            foreach (var info in _currentResource.Languages.Values.Select(x => x.CultureInfo).OrderBy(x => x.Name))
            {
                removeLanguageToolStripMenuItem.DropDownItems.Add($"{info.Name} - {info.DisplayName}").Tag = info;
            }
        }

        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void exportAllResourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reloadCurrentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ResourceLoader.OpenedPath))
                LoadResourcesFromFolder(ResourceLoader.OpenedPath);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resourceGrid1.ApplyCurrentCellEdit();
            ResourceLoader.SaveAll();
        }

        private void openLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentResource != null)
                Process.Start("explorer.exe", $"\"{Path.GetDirectoryName(CurrentResource.Filename)}\"");
        }

        private void findToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            clearSearchToolStripMenuItem.Enabled = CurrentSearch != null;
        }

        private void findToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var result = FindWindow.ShowDialog(this);
            if (result != null)
                SetCurrentSearch(result);
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentSearch == null)
            {
                findToolStripMenuItem1_Click(sender, e);
                return;
            }

            resourceGrid1.Focus();
            resourceGrid1.SelectNextSearchResult();
        }

        private void clearSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetCurrentSearch(null);
        }

        private void languagesToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            removeLanguageToolStripMenuItem.Enabled = removeLanguageToolStripMenuItem.DropDownItems.Count > 0;
        }

        private void addLanguageToolStripMenuItem_Clicked(object sender, EventArgs e)
        {
            var language = LanguageSelect.ShowLanguageSelectDialog(this);
            if (language != null && !CurrentResource.Languages.ContainsKey(language.Name))
            {
                CurrentResource.AddLanguage(language.Name, Settings.Default.AddDefaultValuesOnLanguageAdd);

                UpdateMenuStrip();
                resourceGrid1.RefreshResourceDisplay();
            }
        }

        private void removeLanguageToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            CurrentResource.DeleteLanguage(((CultureInfo)e.ClickedItem.Tag).Name);

            UpdateMenuStrip();
            resourceGrid1.RefreshResourceDisplay();
        }

        private void addNewKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentResource != null)
            {
                try
                {
                    AddResourceKey.ShowDialog(this, CurrentResource);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "创建新行失败",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                resourceGrid1.RefreshResourceDisplay();
            }
        }

        private void deleteKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentResource == null || resourceGrid1.RowCount == 0)
                return;

            if (MessageBox.Show("确定要删除当前选定的行吗？", "",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                resourceGrid1.DeleteSelectedRow();
            }
        }

        private async void translateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentResource == null)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            SortedDictionary<string, LanguageHolder> lngs = CurrentResource?.Languages;
            var languages = lngs.Select(x => x.Key).ToList();

            try
            {
                using var tad = new TranslateApiSetting();

                if (tad.ShowDialog() != DialogResult.OK)
                {
                    Cursor.Current = Cursors.Default;
                    return;
                }

                List<string> textToTranslate = CurrentResource.GetTextForTranslating(tad.TranslateAPIConfig);

                if (textToTranslate == null || !textToTranslate.Any())
                {
                    Cursor.Current = Cursors.Default;

                    return;
                }

                string targetLanguage = tad.TranslateAPIConfig.TargetLanguage == Properties.Resources.ColNameNoLang ? tad.TranslateAPIConfig.DefaultLanguage : tad.TranslateAPIConfig.TargetLanguage;
                string sourceLanguage = tad.TranslateAPIConfig.SourceLanguage == Properties.Resources.ColNameNoLang ? tad.TranslateAPIConfig.DefaultLanguage : tad.TranslateAPIConfig.SourceLanguage;

                TranslatorApi translatorApi = new TranslatorApi();

                IList<StranslationResult> result = new List<StranslationResult>();
                foreach (var text in textToTranslate)
                {
                    var requestModel = new RequestModel(text, sourceLanguage, targetLanguage);
                    var item = await translatorApi.TranslateAsync(requestModel, CancellationToken.None);
                    if (item != null && item.IsSuccess)
                    {
                        result.Add(item);
                    }
                }

                CurrentResource.SetTranslatedText(tad.TranslateAPIConfig, result);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var readmePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "帮助文档.docx");
            if (File.Exists(readmePath))
                Process.Start("explorer.exe", $"\"{readmePath}\"");
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

        }

        private void buttonSearchNext_Click(object sender, EventArgs e)
        {

        }

        private void buttonClearSearch_Click(object sender, EventArgs e)
        {

        }

        #endregion 菜单快捷栏事件
    }
}