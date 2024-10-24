﻿using EntryTranslation.Forms;
using EntryTranslation.Helpers;
using EntryTranslation.Properties;
using EntryTranslation.ResourceOperations;
using Sunny.UI;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace EntryTranslation
{
    public partial class MainForm : UIForm
    {
        private static readonly string MoreLanguagesMenuitemName = "MainWindow_MoreLanguagesMenuItem";
        private readonly string _defaultWindowTitle;

        private ResourceHolder _currentResource;
        private SearchParams _currentSearch;

        public MainForm()
        {
            Opacity = 0;
            InitializeComponent();

            _defaultWindowTitle = $"{Text} {Assembly.GetAssembly(typeof(MainForm)).GetName().Version.ToString(2)}";

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

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetAssembly(typeof(MainForm)).Location);
        }

        public SearchParams CurrentSearch => _currentSearch;

        public void SetCurrentSearch(SearchParams value)
        {
            _currentSearch = value;
            //var hits = resourceTreeView1.ExecuteFindInNodes(value);
            resourceGrid1.CurrentSearch = _currentSearch;

            if (value != null)
            {
                MessageBox.Show(string.Format("Message_FindResults_Description", value.Text, "hits"),
                                "Message_FindResults_Title",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        public ResourceLoader ResourceLoader { get; }

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

                    tabPageEditedResource.Text = value?.Filename ?? "MainWindow_CurrentResource_NoResourceLoaded";
                    UpdateMenuStrip();
                });
            }
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
            OnResourceLoadProgress(this, new ResourceLoadProgressEventArgs("LoadProgress_ReferenceAssemblies"));

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
                    "MessageBox_ConfirmLoadAssemblies_Message",
                    assembliesToLoad.Count),
                "MessageBox_ConfirmLoadAssemblies_Title", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                OnResourceLoadProgress(this, new ResourceLoadProgressEventArgs("LoadProgress_Done", null, 0, 0));
                return;
            }

            var count = 0;
            foreach (var filename in assembliesToLoad)
            {
                count++;
                OnResourceLoadProgress(this, new ResourceLoadProgressEventArgs("LoadProgress_ReferenceAssemblies",
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

            OnResourceLoadProgress(this, new ResourceLoadProgressEventArgs("LoadProgress_Done", null, 0, 0));
        }

        private void UpdateMenuStrip()
        {
            var notNull = _currentResource != null;
            keysToolStripMenuItem.Enabled = notNull;
            addNewKeyToolStripMenuItem.Enabled = notNull;
            languagesToolStripMenuItem.Enabled = notNull;

            removeLanguageToolStripMenuItem.DropDownItems.Clear();
            addLanguageToolStripMenuItem.DropDownItems.Clear();

            if (_currentResource == null) return;

            foreach (var info in ResourceLoader.GetUsedLanguages()
                .Where(x => !_currentResource.Languages.Values.Any(y => y.CultureInfo.Equals(x)))
                .OrderBy(x => x.Name))
            {
                addLanguageToolStripMenuItem.DropDownItems.Add($"{info.Name} - {info.DisplayName}").Tag = info;
            }

            if (addLanguageToolStripMenuItem.DropDownItems.Count > 0)
                addLanguageToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            addLanguageToolStripMenuItem.DropDownItems.Add(MoreLanguagesMenuitemName);

            foreach (var info in _currentResource.Languages.Values.Select(x => x.CultureInfo).OrderBy(x => x.Name))
            {
                removeLanguageToolStripMenuItem.DropDownItems.Add($"{info.Name} - {info.DisplayName}").Tag = info;
            }
        }

        private void toolsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var notNull = _currentResource != null;
            //removeNonTLFromOpenedTranslationsToolStripMenuItem.Enabled = notNull && CurrentResource.Languages.Count > 0;
            //removeNonTLFromAllTranslationsToolStripMenuItem.Enabled = ResourceLoader.Resources.Any(x => x.Languages.Count > 0);
            //trimWhitespaceFromCellsToolStripMenuItem.Enabled = notNull && resourceGrid1.SelectedCellCount > 0;
        }

        private void addLanguageToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var tag = e.ClickedItem.Tag as CultureInfo;
            if (tag != null)
            {
                CurrentResource.AddLanguage(tag.Name, Settings.Default.AddDefaultValuesOnLanguageAdd);

                UpdateMenuStrip();
                resourceGrid1.RefreshResourceDisplay();
            }
            else if (e.ClickedItem.Text.Equals(MoreLanguagesMenuitemName, StringComparison.InvariantCulture))
            {
                var language = LanguageSelectDialog.ShowLanguageSelectDialog(this);
                if (language != null && !CurrentResource.Languages.ContainsKey(language.Name))
                {
                    CurrentResource.AddLanguage(language.Name, Settings.Default.AddDefaultValuesOnLanguageAdd);

                    UpdateMenuStrip();
                    resourceGrid1.RefreshResourceDisplay();
                }
            }
        }

        private void addNewKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentResource != null)
            {
                try
                {
                    AddResourceKeyWindow.ShowDialog(this, CurrentResource);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "MainWindow_Failed_to_create_a_new_row",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                resourceGrid1.RefreshResourceDisplay();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ResourceLoader.CanClose())
                return;

            ResourceLoader.Close();
        }

        private void deleteKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentResource == null || resourceGrid1.RowCount == 0)
                return;

            if (MessageBox.Show("MessageBox_ConfirmDeleteRow_Message", "MessageBox_ConfirmDeleteRow_Title",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                resourceGrid1.DeleteSelectedRow();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
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

        private void LoadResourcesFromFolder(string path)
        {
            if (!ResourceLoader.CanClose())
                return;

            Enabled = false;
            //toolStripStatusLabel1.Text = string.Format("LoadProgress_OpeningDirectory", path);
            Application.DoEvents();

            ResourceLoader.OpenProject(path);

            Enabled = true;
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

            //Settings.Default.SplitterLeft = splitContainerLeft.SplitterDistance;
            Settings.Default.SplitterMain = splitContainerMain.SplitterDistance;

            Settings.Default.Save();

            if (!ResourceLoader.CanClose())
                e.Cancel = true;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!Settings.Default.WindowSize.IsEmpty)
            {
                Location = Settings.Default.WindowLocation;
                Size = Settings.Default.WindowSize;
                WindowState = Settings.Default.WindowState;
            }

            //if (Settings.Default.SplitterLeft > 10)
            //    splitContainerLeft.SplitterDistance = Settings.Default.SplitterLeft;
            if (Settings.Default.SplitterMain > 10)
                splitContainerMain.SplitterDistance = Settings.Default.SplitterMain;

            Opacity = 1;

            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1 && !string.IsNullOrEmpty(args[1].Trim()))
            {
                var path = args[1].Trim();
                try
                {
                    var fldr = new DirectoryInfo(path);
                    if (!fldr.Exists)
                        throw new ArgumentException(string.Format("Error_DirectoryMissing", path));
                    path = (fldr.FullName + "\\").Replace("\\\\", "\\");
                    LoadResourcesFromFolder(path);
                }
                catch (Exception inner)
                {
                    throw new ArgumentException(
                        string.Format("Error_InvalidCommandLine", Environment.CommandLine, path), inner);
                }
            }
            else if (Settings.Default.OpenLastDirOnStart &&
                     !string.IsNullOrEmpty(Settings.Default.LastOpenedDirectory) &&
                     Directory.Exists(Settings.Default.LastOpenedDirectory))
            {
                LoadResourcesFromFolder(Settings.Default.LastOpenedDirectory);
            }
        }

        private void OnResourceLoadProgress(object sender, ResourceLoadProgressEventArgs args)
        {
            this.InvokeIfRequired(_ =>
            {
                //toolStripStatusLabelCurrentItem.Text = args.CurrentlyProcessedItem ?? string.Empty;
                //toolStripStatusLabel1.Text = args.CurrentProcess ?? string.Empty;
                //if (args.Progress < args.ProgressTop)
                //{
                //    toolStripProgressBar1.Visible = true;
                //    if (toolStripProgressBar1.Maximum != args.ProgressTop)
                //        toolStripProgressBar1.Maximum = args.ProgressTop;
                //    toolStripProgressBar1.Value = args.Progress;
                //}
                //else
                //{
                //    toolStripProgressBar1.Visible = false;
                //}
            });
        }

        private void OnResourceLoaderOnResourcesChanged(object sender, EventArgs args)
        {
            (this).InvokeIfRequired(_ =>
            {
                var nothingLoaded = string.IsNullOrEmpty(ResourceLoader.OpenedPath);
                findToolStripMenuItem.Enabled = !nothingLoaded;

                UpdateTitlebar();

                CurrentResource = null;

                //resourceTreeView1.LoadResources(ResourceLoader);

                var usedLanguages = ResourceLoader.GetUsedLanguages().ToList();

                languageSettings1.RefreshLanguages(usedLanguages, false);

                LoadReferenceAssemblies();
            });
        }

        private void UpdateTitlebar()
        {
            Text = string.IsNullOrEmpty(ResourceLoader.OpenedPath)
                                ? _defaultWindowTitle
                                : $"{ResourceLoader.OpenedPath}{(CurrentResource?.IsDirty == true ? "*" : "")} - {_defaultWindowTitle}";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentResource = null;
            Application.DoEvents();
            LoadResourcesFromFolder($@"{AppDomain.CurrentDomain.BaseDirectory}LangDic");
        }

        private void revertCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentResource == null)
                return;

            CurrentResource.Revert();
            resourceGrid1.RefreshResourceDisplay();
        }

        private void saveCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resourceGrid1.ApplyCurrentCellEdit();
            CurrentResource?.Save();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resourceGrid1.ApplyCurrentCellEdit();
            ResourceLoader.SaveAll();
        }

        private void removeLanguageToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            CurrentResource.DeleteLanguage(((CultureInfo)e.ClickedItem.Tag).Name);

            UpdateMenuStrip();
            resourceGrid1.RefreshResourceDisplay();
        }

        private void languagesToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            removeLanguageToolStripMenuItem.Enabled = removeLanguageToolStripMenuItem.DropDownItems.Count > 0;
        }

        private void clearSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetCurrentSearch(null);
        }

        private void findToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            clearSearchToolStripMenuItem.Enabled = CurrentSearch != null;
        }

        private void openResourceLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentResource == null) return;
            Process.Start("explorer.exe", $"\"{Path.GetDirectoryName(CurrentResource.Filename)}\"");
        }

        private void reloadCurrentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ResourceLoader.OpenedPath))
                LoadResourcesFromFolder(ResourceLoader.OpenedPath);
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var readmePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "README.md");
            if (File.Exists(readmePath))
                Process.Start("notepad.exe", $"\"{readmePath}\"");
        }

        private void licenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var licensePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LICENSE");
            if (File.Exists(licensePath))
                Process.Start("notepad.exe", $"\"{licensePath}\"");
            else
                Process.Start(Properties.Resources.Homepage);
        }

        private void fromOpenedTranslationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AskToRemoveNontranslatable()) return;

            CurrentResource?.SaveWithoutNontranslatableData();
        }

        private static bool AskToRemoveNontranslatable()
        {
            return MessageBox.Show("MessageBox_RemoveNontranslatableQuestion_Message",
                "MessageBox_RemoveNontranslatableQuestion_Title", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK;
        }

        private void fromAllTranslationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AskToRemoveNontranslatable()) return;

            foreach (var resource in ResourceLoader.Resources)
            {
                resource.SaveWithoutNontranslatableData();
            }
        }

        private void trimWhitespaceFromCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resourceGrid1.TrimWhitespaceFromSelectedCells();
        }

        private void exportAllResourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void toolStripMenuItemGT_Click(object sender, EventArgs e)
        {

        }
    }
}