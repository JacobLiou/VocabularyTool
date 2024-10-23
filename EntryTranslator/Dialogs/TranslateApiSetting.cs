using EntryTranslator.Models;
using EntryTranslator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EntryTranslator.Dialogs
{
    public partial class TranslateApiSetting : WindowBase
    {
        private List<CultureModel> _cultureModels;

        private List<string> _curLangCodes;

        public TranslateAPIConfig TranslateAPIConfig { get; } = new TranslateAPIConfig();

        public TranslateApiSetting()
        {
            InitializeComponent();
            var le = CommonUtil.GetEnumList<LanguageEnum>();
            cbSourse.DataSource = le.Keys.ToList();
            cbTarget.DataSource = le.Keys.ToList();

            cbSourse.SelectedIndex = 0;
            cbTarget.SelectedIndex = -1;
        }

        public TranslateApiSetting(List<string> curLangCodes) : this()
        {
            _curLangCodes = curLangCodes;
        }

        private void ChbOverwrite_CheckedChanged(object sender, EventArgs e)
        {
            TranslateAPIConfig.Overwrite = chbOverwrite.Checked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbSourse.SelectedIndex < 0 ||
                cbTarget.SelectedIndex < 0)
            {
                MessageBox.Show("输入输入语言");
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            if (cbSourse.SelectedIndex == cbTarget.SelectedIndex)
            {
                MessageBox.Show("语言不能相同");
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            //var sourceLangCode = CommonUtil.GetEnumValueFromDescription<LanguageEnum>(cbSourse.SelectedText).ToString().ToLower();
            //var targetLangCode = CommonUtil.GetEnumValueFromDescription<LanguageEnum>(cbTarget.SelectedText).ToString().ToLower();

            var sourceLangCode = _cultureModels.FirstOrDefault(item =>
                item.Name.Contains(cbSourse.SelectedText))?.Code;
            var targetLangCode = _cultureModels.FirstOrDefault(item =>
                item.Name.Contains(cbTarget.SelectedText))?.Code;

            if (!_curLangCodes.Contains(sourceLangCode)
                || !_curLangCodes.Contains(targetLangCode))
            {
                MessageBox.Show("当前语言编辑区还不存在翻译语言，请先添加");
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            TranslateAPIConfig.SourceLanguage = sourceLangCode;
            TranslateAPIConfig.TargetLanguage = targetLangCode;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async void TranslateApiSetting_Load(object sender, EventArgs e)
        {
            _cultureModels = await CultureLangHelper.GetLanguageList();
        }
    }
}