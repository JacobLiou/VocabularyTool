using Sunny.UI;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace EntryTranslator.Controls
{
    public partial class LanguageSettings : UIUserControl
    {
        public LanguageSettings()
        {
            InitializeComponent();
        }

        public void RefreshLanguages(IEnumerable<CultureInfo> languages)
        {
            listView1.SuspendLayout();
            listView1.BeginUpdate();

            listView1.Items.Clear();

            foreach (var cultureInfo in languages)
            {
                listView1.Items.Add(new ListViewItem(new[] { cultureInfo.Name, cultureInfo.DisplayName })
                {
                    Tag = cultureInfo,
                    Checked = true,
                });
            }

            listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            if (listView1.Columns[1].Width < 100)
                listView1.Columns[1].Width = 100;

            listView1.EndUpdate();
            listView1.ResumeLayout();
        }
    }
}