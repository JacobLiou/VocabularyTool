using EntryTranslator.Const;
using EntryTranslator.Dialogs;
using EntryTranslator.Models;
using EntryTranslator.Properties;
using EntryTranslator.Models;
using EntryTranslator.Utils;
using STranslate.ViewModels.Preference.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EntryTranslator
{
    public sealed partial class MainWindow : WindowBase
    {
        private SearchParams _currentSearch;

        public LangDicHolder LangDicHolder { get; }

        public MainWindow()
        {
            InitializeComponent();

            LangDicHolder = new LangDicHolder();
            LangDicHolder.ResourceLoadProgress += OnResourceLoadProgress;
            LangDicHolder.ResourcesChanged += OnResourceLoaderOnResourcesChanged;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            LoadResourcesFromFolder();
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

        private void LoadResourcesFromFolder()
        {
            Enabled = false;
            toolStripStatusLabel1.Text = string.Format("打开..");
            Application.DoEvents();

            LangDicHolder.Load();
            resourceGrid1.LangDicHolder = LangDicHolder;

            Enabled = true;
        }

        private void OnResourceLoadProgress(object sender, LoadProgressEventArgs args)
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
                UpdateTitlebar();

                var usedLanguages = LangDicHolder.GetUsedLanguages().ToList();

                languageSettings1.RefreshLanguages(usedLanguages);
            });
        }

        private void UpdateTitlebar()
        {

        }

        #region 菜单快捷栏事件

        private void UpdateMenuStrip()
        {
            removeLanguageToolStripMenuItem.DropDownItems.Clear();
            addLanguageToolStripMenuItem.DropDownItems.Clear();

            foreach (var info in LangDicHolder.Languages.Values.Select(x => x.CultureInfo).OrderBy(x => x.Name))
            {
                removeLanguageToolStripMenuItem.DropDownItems.Add($"{info.Name} - {info.DisplayName}").Tag = info;
            }
        }

        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonImport_Click(sender, e);
        }

        private void exportAllResourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonExport_Click(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resourceGrid1.ApplyCurrentCellEdit();
            LangDicHolder.Save();
        }

        private void findToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            clearSearchToolStripMenuItem.Enabled = _currentSearch != null;
        }

        private void findToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var result = FindWindow.ShowDialog(this);
            if (result != null)
                SetCurrentSearch(result);
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentSearch == null)
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
            if (language != null && !LangDicHolder.Languages.ContainsKey(language))
            {
                LangDicHolder.AddLanguage(language, Settings.Default.AddDefaultValuesOnLanguageAdd);

                UpdateMenuStrip();
                resourceGrid1.RefreshResourceDisplay();
            }
        }

        private void removeLanguageToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LangDicHolder.DeleteLanguage(((CultureInfo)e.ClickedItem.Tag).Name);

            UpdateMenuStrip();
            resourceGrid1.RefreshResourceDisplay();
        }

        private void addNewKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LangDicHolder != null)
            {
                bool dialogResult = false;
                try
                {
                    dialogResult = AddResourceKey.ShowDialog(this, LangDicHolder);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "创建新行失败",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (dialogResult)
                    resourceGrid1.RefreshResourceDisplay();
            }
        }

        private void deleteKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LangDicHolder == null || resourceGrid1.RowCount == 0)
                return;

            var dialogResult = MessageBox.Show("确定要删除当前选定的行吗", "删除键",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                resourceGrid1.DeleteSelectedRow();
            }
        }

        private async void translateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LangDicHolder == null)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            var languages = LangDicHolder.Languages.Select(x => x.Key).ToList();

            try
            {
                using var tad = new TranslateApiSetting(languages);

                if (tad.ShowDialog() != DialogResult.OK)
                {
                    Cursor.Current = Cursors.Default;
                    return;
                }

                List<string> textToTranslate = LangDicHolder.GetTextForTranslating(tad.TranslateAPIConfig);

                if (textToTranslate == null || !textToTranslate.Any())
                {
                    Cursor.Current = Cursors.Default;

                    return;
                }

                string targetLanguage = tad.TranslateAPIConfig.TargetLanguage;
                string sourceLanguage = tad.TranslateAPIConfig.SourceLanguage;

                //var translatorApi = new TranslatorApi();
                var translatorBaidu = new TranslatorBaidu();

                IList<StranslationResult> result = new List<StranslationResult>();
                for (int i = 0; i < textToTranslate.Count; i++)
                {
                    var requestModel = new RequestModel
                    {
                        Text = textToTranslate[i],
                        SourceLang = sourceLanguage,
                        TargetLang = targetLanguage,
                        SourceLangText = tad.TranslateAPIConfig.SourceLanguageZh,
                        TargetLangText = tad.TranslateAPIConfig.TargetLanguageZh,
                    };
                    var item = await translatorBaidu.TranslateAsync(requestModel, CancellationToken.None);
                    Thread.Sleep(1000);
                    OnResourceLoadProgress(this, new LoadProgressEventArgs("联网翻译进行中...", null, i + 1, textToTranslate.Count));
                    if (item != null && item.IsSuccess)
                    {
                        result.Add(item);
                    }
                }

                LangDicHolder.SetTranslatedText(tad.TranslateAPIConfig, result);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                OnResourceLoadProgress(this, new LoadProgressEventArgs("完成", null, 0, 0));
                Cursor.Current = Cursors.Default;
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var readmePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Help.pdf");
            if (File.Exists(readmePath))
                Process.Start("explorer.exe", $"\"{readmePath}\"");
        }

        private void cultureoolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var language = new LanguageHelp();
            language.ShowDialog();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("导入将删除现有的所有数据", "导入", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult != DialogResult.Yes)
                return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = $@"{AppDomain.CurrentDomain.BaseDirectory}Templates";

            if (uiRadioButtonExcel.Checked)
                openFileDialog.Filter = "Excel Files|*.xlsx";
            else
                openFileDialog.Filter = "CSV Files|*.csv";

            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string fileName = openFileDialog.FileName;
            DataTable dataTable;
            if (uiRadioButtonExcel.Checked)
                dataTable = ExcelUtil.ImportExcelFile(fileName);
            else
                dataTable = CsvUtil.ImportFromCsv(fileName);
            LangDicHolder.StringsTable = dataTable;
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files|*.xlsx";
                sfd.Title = "保存Excel文件";
                sfd.DefaultExt = "xlsx";

                if (uiRadioButtonExcel.Checked)
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.Title = "保存Excel文件";
                    sfd.DefaultExt = "xlsx";
                }
                else
                {
                    sfd.Filter = "CSV Files|*.csv";
                    sfd.Title = "保存CSV文件";
                    sfd.DefaultExt = "csv";
                }

                sfd.AddExtension = true;
                sfd.FileName = "Sofar词条库_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                if (uiRadioButtonExcel.Checked)
                    SaveExcelFile(sfd);
                else
                    SaveCSVFile(sfd);
            }
        }

        private void SaveExcelFile(SaveFileDialog sfd)
        {
            try
            {
                if (ExcelUtil.DataTableToExcel(LangDicHolder.StringsTable, sfd.FileName, GlobalSettings.FilterColNames))
                    MessageBox.Show("文件保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("保存文件失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存文件失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveCSVFile(SaveFileDialog sfd)
        {
            try
            {
                if (CsvUtil.ExportToCsv(LangDicHolder.StringsTable, sfd.FileName, GlobalSettings.FilterColNames))
                    MessageBox.Show("文件保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("保存文件失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存文件失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonaddLanguage_Click(object sender, EventArgs e)
        {
            addLanguageToolStripMenuItem_Clicked(sender, e);
        }

        private void buttonaddNewKey_Click(object sender, EventArgs e)
        {
            addNewKeyToolStripMenuItem_Click(sender, e);
        }

        private void buttondeleteKey_Click(object sender, EventArgs e)
        {
            deleteKeyToolStripMenuItem_Click(sender, e);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSearch.Text))
            {
                MessageBox.Show("搜索不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var sp = new SearchParams(
                textBoxSearch.Text
                , false
                , false
                , true
                , true
                , false
                , false
                , false);
            sp.Save();
            SetCurrentSearch(sp);
        }

        private void buttonSearchNext_Click(object sender, EventArgs e)
        {
            findNextToolStripMenuItem_Click(sender, e);
        }

        private void buttonClearSearch_Click(object sender, EventArgs e)
        {
            clearSearchToolStripMenuItem_Click(sender, e);
        }

        #endregion 菜单快捷栏事件
    }
}