using EntryTranslation.Helpers;
using EntryTranslation.Properties;
using Sunny.UI;
using System.Globalization;

namespace EntryTranslation.Forms
{
    public partial class LanguageSelectDialog : UIForm
    {
        public static CultureInfo ShowLanguageSelectDialog(Form owner)
        {
            using (var window = new LanguageSelectDialog())
            {
                window.Icon = owner.Icon;
                window.StartPosition = FormStartPosition.CenterParent;
                return window.ShowDialog(owner) == DialogResult.OK ? window._selectedLanguage : null;
            }
        }

        private LanguageSelectDialog()
        {
            InitializeComponent();

            UpdateComboboxItems(this, EventArgs.Empty);
            button1.Enabled = false;

            Settings.Binder.BindControl(checkBox1, s => s.LanguageSelectOnlyNeutral, this);
            Settings.Binder.SendUpdates(this);
        }

        private CultureInfo _selectedLanguage;

        private void button1_Click(object sender, EventArgs e)
        {
            var selection = comboBox1.SelectedItem as ComboBoxWrapper<CultureInfo>;
            if (selection != null)
            {
                _selectedLanguage = selection.WrappedObject;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "MessageBox_SelectedLanguageInvalid_Message",
                    "MessageBox_SelectedLanguageInvalid_Title", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateComboboxItems(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(CultureInfo.GetCultures(checkBox1.Checked ? CultureTypes.NeutralCultures : CultureTypes.AllCultures)
                .Where(x => !string.IsNullOrWhiteSpace(x.Name)) //Exclude the invariant culture
                .OrderBy(x => x.Name)
                .Select(x => new ComboBoxWrapper<CultureInfo>(x, info => $"{info.Name} - {info.DisplayName}"))
                .Cast<object>()
                .ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = comboBox1.SelectedItem as ComboBoxWrapper<CultureInfo>;
            button1.Enabled = selection != null;
        }

        private void LanguageSelectDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Binder.RemoveHandlers(this);
        }
    }
}