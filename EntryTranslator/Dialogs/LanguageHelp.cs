using EntryTranslator.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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

        private void uiButton_Save_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text Files|*.txt";
                sfd.Title = "txt文件";
                sfd.DefaultExt = "txt";
                sfd.AddExtension = true;

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                File.WriteAllText(sfd.FileName, uiRichTextBox1.Text);
                Process.Start("notepad.exe", sfd.FileName);
            }
        }
    }
}