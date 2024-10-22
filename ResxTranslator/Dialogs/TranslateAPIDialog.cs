using ResxTranslator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ResxTranslator.Dialogs
{
    public partial class TranslateAPIDialog : Form
    {
        private readonly List<string> _languages;
        private List<string> _languagesFrom;
        private List<string> _languagesTo;
        private bool init;

        public TranslateAPIConfig TranslateAPIConfig { get; } = new TranslateAPIConfig();

        public TranslateAPIDialog()
        {
            InitializeComponent();
            var le = CommonUtil.GetEnumList<LanguageEnum>();
            cbSourse.DataSource = le.Keys.ToList();
            cbTarget.DataSource = le.Keys.ToList();

            cbSourse.SelectedIndex = 0;
            cbTarget.SelectedIndex = -1;
        }

        private void ChbOverwrite_CheckedChanged(object sender, EventArgs e)
        {
            TranslateAPIConfig.Overwrite = chbOverwrite.Checked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            TranslateAPIConfig.SourceLanguage = (string)cbSourse.SelectedItem;
            TranslateAPIConfig.TargetLanguage = (string)cbTarget.SelectedItem;
            if (TranslateAPIConfig.SourceLanguage == TranslateAPIConfig.TargetLanguage)
            {
                MessageBox.Show("语言不能相同");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}