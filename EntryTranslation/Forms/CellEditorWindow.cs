﻿using EntryTranslation.Properties;
using ScintillaNET;
using Sunny.UI;

namespace EntryTranslation.Forms
{
    public sealed partial class CellEditorWindow : UIForm
    {
        public CellEditorWindow()
        {
            InitializeComponent();

            textBoxString.Styles[ScintillaNET.Style.Default].Font = "Microsoft Sans Serif";
            textBoxString.Styles[ScintillaNET.Style.Default].Size = 11;
            textBoxString.StyleClearAll();

            textBoxString.Styles[ScintillaNET.Style.LineNumber].BackColor = Color.DarkGray;
            textBoxString.Styles[ScintillaNET.Style.LineNumber].ForeColor = Color.LightGray;
            var nums = textBoxString.Margins[1];
            nums.Type = MarginType.Number;
            nums.Mask = 0;

            textBoxString.SetWhitespaceForeColor(true, Color.Brown);

            Settings.Binder.BindControl(checkBox1, settings => settings.CellEditorWrapContents, this);
            Settings.Binder.Subscribe((sender, args) => textBoxString.WrapMode = args.NewValue ? WrapMode.Word : WrapMode.None,
                settings => settings.CellEditorWrapContents, this);

            Settings.Binder.BindControl(chbShowWhitespace, settings => settings.CellEditorShowWhitespace, this);
            Settings.Binder.Subscribe((sender, args) => textBoxString.ViewWhitespace = args.NewValue ? WhitespaceMode.VisibleAlways : WhitespaceMode.Invisible,
                settings => settings.CellEditorShowWhitespace, this);

            Settings.Binder.SendUpdates(this);
        }

        private void ZoomWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Binder.RemoveHandlers(this);
        }

        private void CellEditorWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
            {
                e.Handled = true;
                buttonOK.PerformClick();
            }

            if (e.KeyChar == 27)
            {
                e.Handled = true;
                buttonCancel.PerformClick();
            }
        }
    }
}