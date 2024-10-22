namespace EntryTranslation.Forms
{
    sealed partial class CellEditorWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CellEditorWindow));
            panel1 = new Panel();
            buttonOK = new Button();
            panel2 = new Panel();
            checkBox1 = new CheckBox();
            chbShowWhitespace = new CheckBox();
            buttonCancel = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonOK);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(chbShowWhitespace);
            panel1.Controls.Add(buttonCancel);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // buttonOK
            // 
            resources.ApplyResources(buttonOK, "buttonOK");
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Name = "buttonOK";
            buttonOK.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // checkBox1
            // 
            resources.ApplyResources(checkBox1, "checkBox1");
            checkBox1.Name = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // chbShowWhitespace
            // 
            resources.ApplyResources(chbShowWhitespace, "chbShowWhitespace");
            chbShowWhitespace.Name = "chbShowWhitespace";
            chbShowWhitespace.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // CellEditorWindow
            // 
            AcceptButton = buttonOK;
            AutoScaleMode = AutoScaleMode.None;
            CancelButton = buttonCancel;
            resources.ApplyResources(this, "$this");
            Controls.Add(panel1);
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CellEditorWindow";
            ShowInTaskbar = false;
            ZoomScaleRect = new Rectangle(19, 19, 450, 300);
            FormClosed += ZoomWindow_FormClosed;
            KeyPress += CellEditorWindow_KeyPress;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkBox1;
        internal ScintillaNET.Scintilla textBoxString;
        private System.Windows.Forms.CheckBox chbShowWhitespace;
        private System.Windows.Forms.Panel panel2;
    }
}