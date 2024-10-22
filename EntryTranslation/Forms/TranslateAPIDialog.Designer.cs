namespace EntryTranslation.Forms
{
    partial class TranslateAPIDialog
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
            chbOverwrite = new CheckBox();
            cbSourse = new ComboBox();
            cbTarget = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            lblDefaultLanguage = new Label();
            cbDefaultLanguage = new ComboBox();
            SuspendLayout();
            // 
            // chbOverwrite
            // 
            chbOverwrite.AutoSize = true;
            chbOverwrite.Location = new Point(5, 95);
            chbOverwrite.Name = "chbOverwrite";
            chbOverwrite.Size = new Size(498, 22);
            chbOverwrite.TabIndex = 3;
            chbOverwrite.Text = "Overwrite already translated text in target language";
            chbOverwrite.UseVisualStyleBackColor = true;
            chbOverwrite.CheckedChanged += ChbOverwrite_CheckedChanged;
            // 
            // cbSourse
            // 
            cbSourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSourse.FormattingEnabled = true;
            cbSourse.Location = new Point(174, 35);
            cbSourse.Name = "cbSourse";
            cbSourse.Size = new Size(129, 26);
            cbSourse.TabIndex = 0;
            cbSourse.SelectedIndexChanged += cbFrom_SelectedIndexChanged;
            // 
            // cbTarget
            // 
            cbTarget.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTarget.FormattingEnabled = true;
            cbTarget.Location = new Point(174, 64);
            cbTarget.Name = "cbTarget";
            cbTarget.Size = new Size(128, 26);
            cbTarget.TabIndex = 2;
            cbTarget.SelectedIndexChanged += cbTo_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 35);
            label1.Name = "label1";
            label1.Size = new Size(233, 18);
            label1.TabIndex = 98;
            label1.Text = "Translate source language";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 68);
            label2.Name = "label2";
            label2.Size = new Size(233, 18);
            label2.TabIndex = 99;
            label2.Text = "Translate target language";
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(5, 125);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(120, 40);
            btnOk.TabIndex = 4;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(183, 125);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblDefaultLanguage
            // 
            lblDefaultLanguage.AutoSize = true;
            lblDefaultLanguage.Enabled = false;
            lblDefaultLanguage.Location = new Point(5, 39);
            lblDefaultLanguage.Name = "lblDefaultLanguage";
            lblDefaultLanguage.Size = new Size(287, 18);
            lblDefaultLanguage.TabIndex = 100;
            lblDefaultLanguage.Text = "Please specify default language";
            // 
            // cbDefaultLanguage
            // 
            cbDefaultLanguage.Enabled = false;
            cbDefaultLanguage.FormattingEnabled = true;
            cbDefaultLanguage.Location = new Point(174, 35);
            cbDefaultLanguage.Name = "cbDefaultLanguage";
            cbDefaultLanguage.Size = new Size(128, 26);
            cbDefaultLanguage.TabIndex = 1;
            cbDefaultLanguage.SelectedIndexChanged += CbDefaultLanguage_SelectedIndexChanged;
            // 
            // TranslateAPIDialog
            // 
            AcceptButton = btnOk;
            AutoScaleMode = AutoScaleMode.None;
            CancelButton = btnCancel;
            ClientSize = new Size(610, 262);
            Controls.Add(cbDefaultLanguage);
            Controls.Add(lblDefaultLanguage);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbTarget);
            Controls.Add(cbSourse);
            Controls.Add(chbOverwrite);
            Font = new Font("宋体", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TranslateAPIDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "TranslateAPIDialog";
            ZoomScaleRect = new Rectangle(19, 19, 309, 171);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.CheckBox chbOverwrite;
        private System.Windows.Forms.ComboBox cbSourse;
        private System.Windows.Forms.ComboBox cbTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDefaultLanguage;
        private System.Windows.Forms.ComboBox cbDefaultLanguage;
    }
}