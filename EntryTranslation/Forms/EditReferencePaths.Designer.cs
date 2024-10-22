namespace EntryTranslation.Forms
{
    partial class EditReferencePaths
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditReferencePaths));
            buttonAdd = new Button();
            buttonRemove = new Button();
            listBox1 = new ListBox();
            panel2 = new Panel();
            groupBox1 = new GroupBox();
            panel1 = new Panel();
            buttonAccept = new Button();
            buttonCancel = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonAdd
            // 
            resources.ApplyResources(buttonAdd, "buttonAdd");
            buttonAdd.Name = "buttonAdd";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonRemove
            // 
            resources.ApplyResources(buttonRemove, "buttonRemove");
            buttonRemove.Name = "buttonRemove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // listBox1
            // 
            resources.ApplyResources(listBox1, "listBox1");
            listBox1.FormattingEnabled = true;
            listBox1.Name = "listBox1";
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Controls.Add(buttonRemove);
            panel2.Controls.Add(buttonAdd);
            panel2.Name = "panel2";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listBox1);
            groupBox1.Controls.Add(panel2);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonAccept);
            panel1.Controls.Add(buttonCancel);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // buttonAccept
            // 
            resources.ApplyResources(buttonAccept, "buttonAccept");
            buttonAccept.Name = "buttonAccept";
            buttonAccept.UseVisualStyleBackColor = true;
            buttonAccept.Click += buttonAccept_Click;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // EditReferencePaths
            // 
            AcceptButton = buttonAccept;
            AutoScaleMode = AutoScaleMode.None;
            CancelButton = buttonCancel;
            resources.ApplyResources(this, "$this");
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Name = "EditReferencePaths";
            ZoomScaleRect = new Rectangle(19, 19, 384, 236);
            panel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}