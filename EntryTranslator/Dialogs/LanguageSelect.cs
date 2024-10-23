using EntryTranslator.Models;
using EntryTranslator.Properties;
using EntryTranslator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EntryTranslator.Dialogs
{
    public partial class LanguageSelect : WindowBase
    {
        private string _selectedLanguage;

        private List<CultureModel> cultureModels;

        /// <summary>
        /// 返回语言的Code代号
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static string ShowLanguageSelectDialog(Form owner)
        {
            using (var window = new LanguageSelect())
            {
                window.Icon = owner.Icon;
                window.StartPosition = FormStartPosition.CenterParent;
                return window.ShowDialog(owner) == DialogResult.OK ? window._selectedLanguage : null;
            }
        }

        private LanguageSelect()
        {
            InitializeComponent();
        }

        private async void LanguageSelect_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            cultureModels = await CultureLangHelper.GetLanguageList();
            comboBox1.Items.AddRange(cultureModels.Select(item => item.CodeName).ToArray());

            Settings.Binder.SendUpdates(this);
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex < 0) return;
            var langCodeName = comboBox1.Text;
            if (langCodeName != null)
            {
                _selectedLanguage = cultureModels.FirstOrDefault(item => langCodeName == item.CodeName).Code;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "语言非法", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void UpdateComboboxItems(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            var languaes = await CultureLangHelper.GetLanguageList();
            comboBox1.Items.AddRange(languaes.Select(item => item.CodeName).ToArray());
        }

        private void LanguageSelectDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Binder.RemoveHandlers(this);
        }
    }
}