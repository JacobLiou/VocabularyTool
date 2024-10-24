using EntryTranslator.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EntryTranslator.Dialogs
{
    public sealed partial class RowEditor : WindowBase
    {
        public List<LangValuePair> EditLangValuePairs { get; set; }

        public RowEditor()
        {
            InitializeComponent();
        }

        public RowEditor(List<LangValuePair> editLangValuePairs)
        {
            InitializeComponent();

            EditLangValuePairs = editLangValuePairs;
            if (EditLangValuePairs != null && EditLangValuePairs.Count != 0)
            {
                uiDataGridView1.Rows.Clear();
                foreach (var langValuePair in EditLangValuePairs)
                {
                    uiDataGridView1.AddRow(langValuePair.Name, langValuePair.Value);
                }
            }
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < uiDataGridView1.Rows.Count; i++)
            {
                //编辑单元格
                DataGridViewCell cell = uiDataGridView1.Rows[i].Cells[1];
                EditLangValuePairs[i].Value = cell.Value.ToString();
            }

            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}