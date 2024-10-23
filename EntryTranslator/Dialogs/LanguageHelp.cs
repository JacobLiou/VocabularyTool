using EntryTranslator.Utils;
using System;
using System.Linq;

namespace EntryTranslator.Dialogs
{
    public partial class LanguageHelp : WindowBase
    {
        public LanguageHelp()
        {
            InitializeComponent();
        }

        private async void uiRichTextBox1_Load(object sender, EventArgs e)
        {
            var laguages = await CultureLangHelper.GetLanguageList();
            this.Invoke(() =>
            {
                uiRichTextBox1.Lines = laguages.Select(item => item.CodeName).ToArray();
            });
        }
    }
}