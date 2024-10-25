using EntryTranslator.ResourceOperations;
using System;
using System.Windows.Forms;

namespace EntryTranslator.Dialogs
{
    public partial class FindExWindow : WindowBase
    {
        public static SearchParams ShowDialog(Form owner)
        {
            using (var window = new FindExWindow())
            {
                window.Icon = owner.Icon;
                window.StartPosition = FormStartPosition.CenterParent;
                if (window.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(window.CurrentSearch.Text))
                    return window.CurrentSearch;
                return null;
            }
        }

        private FindExWindow()
        {
            InitializeComponent();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            //var sp = new SearchParams(
            //    textBoxSearch.Text
            //    , checkBoxLang.Checked
            //    , checkBoxKey.Checked
            //    , checkBoxOrigText.Checked
            //    , checkBoxTranslText.Checked
            //    , radioButtonRegexp.Checked
            //    , checkBoxCS.Checked
            //    , checkBoxWord.Checked);
            //sp.Save();
            //CurrentSearch = sp;
            //DialogResult = DialogResult.OK;
            //Close();
        }

        private SearchParams CurrentSearch { get; set; }

        private void FindDialog_Load(object sender, EventArgs e)
        {
        }
    }
}