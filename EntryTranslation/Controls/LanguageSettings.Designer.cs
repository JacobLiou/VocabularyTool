namespace EntryTranslation.Controls
{
    partial class LanguageSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LanguageSettings));
            listView1 = new ListView();
            columnHeaderCode = new ColumnHeader();
            columnHeaderName = new ColumnHeader();
            toolStrip1 = new ToolStrip();
            toolStripButtonSelectAll = new ToolStripButton();
            toolStripButtonSelectNone = new ToolStripButton();
            toolStripButtonSelectInvert = new ToolStripButton();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.BorderStyle = BorderStyle.None;
            listView1.CheckBoxes = true;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeaderCode, columnHeaderName });
            resources.ApplyResources(listView1, "listView1");
            listView1.GridLines = true;
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.ShowGroups = false;
            listView1.ShowItemToolTips = true;
            listView1.Sorting = SortOrder.Ascending;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.ItemChecked += listView1_ItemChecked;
            // 
            // columnHeaderCode
            // 
            resources.ApplyResources(columnHeaderCode, "columnHeaderCode");
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(columnHeaderName, "columnHeaderName");
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButtonSelectAll, toolStripButtonSelectNone, toolStripButtonSelectInvert });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButtonSelectAll
            // 
            resources.ApplyResources(toolStripButtonSelectAll, "toolStripButtonSelectAll");
            toolStripButtonSelectAll.Name = "toolStripButtonSelectAll";
            toolStripButtonSelectAll.Click += toolStripButtonSelectAll_Click;
            // 
            // toolStripButtonSelectNone
            // 
            resources.ApplyResources(toolStripButtonSelectNone, "toolStripButtonSelectNone");
            toolStripButtonSelectNone.Name = "toolStripButtonSelectNone";
            toolStripButtonSelectNone.Click += toolStripButtonSelectNone_Click;
            // 
            // toolStripButtonSelectInvert
            // 
            toolStripButtonSelectInvert.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonSelectInvert.Image = Properties.Resources.clipboard;
            resources.ApplyResources(toolStripButtonSelectInvert, "toolStripButtonSelectInvert");
            toolStripButtonSelectInvert.Name = "toolStripButtonSelectInvert";
            toolStripButtonSelectInvert.Click += toolStripButtonSelectInvert_Click;
            // 
            // LanguageSettings
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(listView1);
            Controls.Add(toolStrip1);
            Name = "LanguageSettings";
            resources.ApplyResources(this, "$this");
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderCode;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectAll;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectNone;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectInvert;
    }
}
