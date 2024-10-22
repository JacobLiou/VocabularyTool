namespace EntryTranslation.Forms
{
    partial class FindWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindWindow));
            textBoxSearch = new TextBox();
            radioButton1 = new RadioButton();
            radioButtonRegexp = new RadioButton();
            groupBox1 = new GroupBox();
            flowLayoutPanel3 = new FlowLayoutPanel();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            flowLayoutPanel4 = new FlowLayoutPanel();
            checkBoxOrigText = new CheckBox();
            checkBoxTranslText = new CheckBox();
            checkBoxKey = new CheckBox();
            checkBoxLang = new CheckBox();
            checkBoxFile = new CheckBox();
            groupBox4 = new GroupBox();
            flowLayoutPanel2 = new FlowLayoutPanel();
            checkBoxCS = new CheckBox();
            checkBoxWord = new CheckBox();
            buttonFind = new Button();
            button1 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox1.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            groupBox4.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxSearch
            // 
            resources.ApplyResources(textBoxSearch, "textBoxSearch");
            textBoxSearch.Name = "textBoxSearch";
            // 
            // radioButton1
            // 
            resources.ApplyResources(radioButton1, "radioButton1");
            radioButton1.Checked = true;
            radioButton1.Name = "radioButton1";
            radioButton1.TabStop = true;
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButtonRegexp
            // 
            resources.ApplyResources(radioButtonRegexp, "radioButtonRegexp");
            radioButtonRegexp.Name = "radioButtonRegexp";
            radioButtonRegexp.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(flowLayoutPanel3);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(radioButton1);
            flowLayoutPanel3.Controls.Add(radioButtonRegexp);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(textBoxSearch);
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Controls.Add(flowLayoutPanel4);
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Controls.Add(checkBoxOrigText);
            flowLayoutPanel4.Controls.Add(checkBoxTranslText);
            flowLayoutPanel4.Controls.Add(checkBoxKey);
            flowLayoutPanel4.Controls.Add(checkBoxLang);
            flowLayoutPanel4.Controls.Add(checkBoxFile);
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // checkBoxOrigText
            // 
            resources.ApplyResources(checkBoxOrigText, "checkBoxOrigText");
            checkBoxOrigText.Checked = true;
            checkBoxOrigText.CheckState = CheckState.Checked;
            checkBoxOrigText.Name = "checkBoxOrigText";
            checkBoxOrigText.UseVisualStyleBackColor = true;
            // 
            // checkBoxTranslText
            // 
            resources.ApplyResources(checkBoxTranslText, "checkBoxTranslText");
            checkBoxTranslText.Checked = true;
            checkBoxTranslText.CheckState = CheckState.Checked;
            checkBoxTranslText.Name = "checkBoxTranslText";
            checkBoxTranslText.UseVisualStyleBackColor = true;
            // 
            // checkBoxKey
            // 
            resources.ApplyResources(checkBoxKey, "checkBoxKey");
            checkBoxKey.Name = "checkBoxKey";
            checkBoxKey.UseVisualStyleBackColor = true;
            // 
            // checkBoxLang
            // 
            resources.ApplyResources(checkBoxLang, "checkBoxLang");
            checkBoxLang.Name = "checkBoxLang";
            checkBoxLang.UseVisualStyleBackColor = true;
            // 
            // checkBoxFile
            // 
            resources.ApplyResources(checkBoxFile, "checkBoxFile");
            checkBoxFile.Name = "checkBoxFile";
            checkBoxFile.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Controls.Add(flowLayoutPanel2);
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(checkBoxCS);
            flowLayoutPanel2.Controls.Add(checkBoxWord);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // checkBoxCS
            // 
            resources.ApplyResources(checkBoxCS, "checkBoxCS");
            checkBoxCS.Name = "checkBoxCS";
            checkBoxCS.UseVisualStyleBackColor = true;
            // 
            // checkBoxWord
            // 
            resources.ApplyResources(checkBoxWord, "checkBoxWord");
            checkBoxWord.Name = "checkBoxWord";
            checkBoxWord.UseVisualStyleBackColor = true;
            // 
            // buttonFind
            // 
            resources.ApplyResources(buttonFind, "buttonFind");
            buttonFind.Name = "buttonFind";
            buttonFind.UseVisualStyleBackColor = true;
            buttonFind.Click += buttonFind_Click;
            // 
            // button1
            // 
            button1.DialogResult = DialogResult.Cancel;
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.Controls.Add(buttonFind);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // FindWindow
            // 
            AcceptButton = buttonFind;
            AutoScaleMode = AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            CancelButton = button1;
            Controls.Add(flowLayoutPanel1);
            Controls.Add(groupBox4);
            Controls.Add(groupBox1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FindWindow";
            SizeGripStyle = SizeGripStyle.Hide;
            ZoomScaleRect = new Rectangle(19, 19, 362, 246);
            Load += FindDialog_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButtonRegexp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxOrigText;
        private System.Windows.Forms.CheckBox checkBoxKey;
        private System.Windows.Forms.CheckBox checkBoxLang;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxWord;
        private System.Windows.Forms.CheckBox checkBoxCS;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.CheckBox checkBoxFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBoxTranslText;
    }
}