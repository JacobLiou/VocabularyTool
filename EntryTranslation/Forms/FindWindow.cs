﻿using EntryTranslation.ResourceOperations;
using Sunny.UI;

namespace EntryTranslation.Forms
{
    public partial class FindWindow : UIForm
    {
        public static SearchParams ShowDialog(Form owner)
        {
            using (var window = new FindWindow())
            {
                window.Icon = owner.Icon;
                window.StartPosition = FormStartPosition.CenterParent;
                if (window.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(window.CurrentSearch.Text))
                    return window.CurrentSearch;
                return null;
            }
        }

        private FindWindow()
        {
            InitializeComponent();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            var sp = new SearchParams(
                textBoxSearch.Text
                , checkBoxLang.Checked
                , checkBoxKey.Checked
                , checkBoxOrigText.Checked
                , checkBoxTranslText.Checked
                , checkBoxFile.Checked
                , radioButtonRegexp.Checked
                , checkBoxCS.Checked
                , checkBoxWord.Checked);
            sp.Save();
            CurrentSearch = sp;
            DialogResult = DialogResult.OK;
            Close();
        }

        private SearchParams CurrentSearch { get; set; }

        private void FindDialog_Load(object sender, EventArgs e)
        {
            var sp = new SearchParams();
            textBoxSearch.Text = sp.Text;
            checkBoxLang.Checked = sp.SearchLanguage;
            checkBoxKey.Checked = sp.SearchKeys;
            checkBoxOrigText.Checked = sp.SearchOriginalText;
            checkBoxTranslText.Checked = sp.SearchTranslatedText;
            checkBoxFile.Checked = sp.SearchFileName;
            radioButtonRegexp.Checked = sp.UseRegex;
            checkBoxCS.Checked = sp.OptCase;
            checkBoxWord.Checked = sp.OptWord;
        }
    }
}