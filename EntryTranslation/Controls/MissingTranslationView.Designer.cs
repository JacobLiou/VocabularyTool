namespace EntryTranslation.Controls
{
    partial class MissingTranslationView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MissingTranslationView));
            comboBox1 = new ComboBox();
            listView1 = new ListView();
            columnHeader2 = new ColumnHeader();
            columnHeader1 = new ColumnHeader();
            panel1 = new Panel();
            panel2 = new Panel();
            button1 = new Button();
            buttonRefresh = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            resources.ApplyResources(comboBox1, "comboBox1");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.AddRange(new object[] { resources.GetString("comboBox1.Items") });
            comboBox1.Name = "comboBox1";
            comboBox1.SelectedIndexChanged += RefreshListView;
            // 
            // listView1
            // 
            listView1.BorderStyle = BorderStyle.None;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader2, columnHeader1 });
            resources.ApplyResources(listView1, "listView1");
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Name = "listView1";
            listView1.ShowItemToolTips = true;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.Click += listView1_Click;
            // 
            // columnHeader2
            // 
            resources.ApplyResources(columnHeader2, "columnHeader2");
            // 
            // columnHeader1
            // 
            resources.ApplyResources(columnHeader1, "columnHeader1");
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(buttonRefresh);
            panel1.Name = "panel1";
            // 
            // panel2
            // 
            panel2.Controls.Add(comboBox1);
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // buttonRefresh
            // 
            resources.ApplyResources(buttonRefresh, "buttonRefresh");
            buttonRefresh.Image = Properties.Resources.RefreshArrow;
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // MissingTranslationView
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(listView1);
            Controls.Add(panel1);
            resources.ApplyResources(this, "$this");
            Name = "MissingTranslationView";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
