using System.Windows.Forms;

namespace EntryTranslator
{
    sealed partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.splitContainerAll = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonClearSearch = new System.Windows.Forms.Button();
            this.buttonSearchNext = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.languageSettings1 = new EntryTranslator.Controls.LanguageSettings();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPageEditedResource = new System.Windows.Forms.TabPage();
            this.resourceGrid1 = new EntryTranslator.Controls.LanguageEditor();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelCurrentItem = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllModifiedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.findNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemGT = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.uiContextMenuStrip1 = new Sunny.UI.UIContextMenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAll)).BeginInit();
            this.splitContainerAll.Panel1.SuspendLayout();
            this.splitContainerAll.Panel2.SuspendLayout();
            this.splitContainerAll.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPageEditedResource.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerAll
            // 
            resources.ApplyResources(this.splitContainerAll, "splitContainerAll");
            this.splitContainerAll.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerAll.Name = "splitContainerAll";
            // 
            // splitContainerAll.Panel1
            // 
            this.splitContainerAll.Panel1.Controls.Add(this.flowLayoutPanel1);
            resources.ApplyResources(this.splitContainerAll.Panel1, "splitContainerAll.Panel1");
            // 
            // splitContainerAll.Panel2
            // 
            this.splitContainerAll.Panel2.Controls.Add(this.splitContainerMain);
            resources.ApplyResources(this.splitContainerAll.Panel2, "splitContainerAll.Panel2");
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowLayoutPanel1.Controls.Add(this.groupBoxFile);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxSearch);
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.BackColor = System.Drawing.Color.LightGray;
            this.groupBoxFile.Controls.Add(this.buttonExport);
            this.groupBoxFile.Controls.Add(this.buttonImport);
            this.groupBoxFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.groupBoxFile, "groupBoxFile");
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.TabStop = false;
            // 
            // buttonExport
            // 
            this.buttonExport.BackColor = System.Drawing.SystemColors.Control;
            this.buttonExport.Image = global::EntryTranslator.Properties.Resources.GoToParentFolderHS;
            resources.ApplyResources(this.buttonExport, "buttonExport");
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.UseVisualStyleBackColor = false;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.BackColor = System.Drawing.SystemColors.Control;
            this.buttonImport.Image = global::EntryTranslator.Properties.Resources.openfolderHS;
            resources.ApplyResources(this.buttonImport, "buttonImport");
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.UseVisualStyleBackColor = false;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.BackColor = System.Drawing.Color.LightGray;
            this.groupBoxSearch.Controls.Add(this.textBoxSearch);
            this.groupBoxSearch.Controls.Add(this.buttonClearSearch);
            this.groupBoxSearch.Controls.Add(this.buttonSearchNext);
            this.groupBoxSearch.Controls.Add(this.buttonSearch);
            this.groupBoxSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.groupBoxSearch, "groupBoxSearch");
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.TabStop = false;
            // 
            // textBoxSearch
            // 
            resources.ApplyResources(this.textBoxSearch, "textBoxSearch");
            this.textBoxSearch.Name = "textBoxSearch";
            // 
            // buttonClearSearch
            // 
            this.buttonClearSearch.BackColor = System.Drawing.SystemColors.Control;
            this.buttonClearSearch.Image = global::EntryTranslator.Properties.Resources.Edit_UndoHS;
            resources.ApplyResources(this.buttonClearSearch, "buttonClearSearch");
            this.buttonClearSearch.Name = "buttonClearSearch";
            this.buttonClearSearch.UseVisualStyleBackColor = false;
            this.buttonClearSearch.Click += new System.EventHandler(this.buttonClearSearch_Click);
            // 
            // buttonSearchNext
            // 
            this.buttonSearchNext.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSearchNext.Image = global::EntryTranslator.Properties.Resources.Find_VS;
            resources.ApplyResources(this.buttonSearchNext, "buttonSearchNext");
            this.buttonSearchNext.Name = "buttonSearchNext";
            this.buttonSearchNext.UseVisualStyleBackColor = false;
            this.buttonSearchNext.Click += new System.EventHandler(this.buttonSearchNext_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSearch.Image = global::EntryTranslator.Properties.Resources.Find_VS;
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // splitContainerMain
            // 
            resources.ApplyResources(this.splitContainerMain, "splitContainerMain");
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.tabControl1);
            resources.ApplyResources(this.splitContainerMain.Panel1, "splitContainerMain.Panel1");
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControl3);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.languageSettings1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // languageSettings1
            // 
            resources.ApplyResources(this.languageSettings1, "languageSettings1");
            this.languageSettings1.Name = "languageSettings1";
            this.languageSettings1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPageEditedResource);
            resources.ApplyResources(this.tabControl3, "tabControl3");
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            // 
            // tabPageEditedResource
            // 
            this.tabPageEditedResource.Controls.Add(this.resourceGrid1);
            resources.ApplyResources(this.tabPageEditedResource, "tabPageEditedResource");
            this.tabPageEditedResource.Name = "tabPageEditedResource";
            this.tabPageEditedResource.UseVisualStyleBackColor = true;
            // 
            // resourceGrid1
            // 
            this.resourceGrid1.CurrentResource = null;
            this.resourceGrid1.CurrentSearch = null;
            resources.ApplyResources(this.resourceGrid1, "resourceGrid1");
            this.resourceGrid1.Name = "resourceGrid1";
            this.resourceGrid1.ShowNullValuesAsGrayed = false;
            this.resourceGrid1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.toolStripStatusLabelCurrentItem});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // toolStripStatusLabelCurrentItem
            // 
            this.toolStripStatusLabelCurrentItem.Name = "toolStripStatusLabelCurrentItem";
            resources.ApplyResources(this.toolStripStatusLabelCurrentItem, "toolStripStatusLabelCurrentItem");
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.splitContainerAll);
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.Name = "panelMain";
            // 
            // menuStripMain
            // 
            resources.ApplyResources(this.menuStripMain, "menuStripMain");
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.keysToolStripMenuItem,
            this.languagesToolStripMenuItem,
            this.toolStripMenuItemGT,
            this.helpToolStripMenuItem});
            this.menuStripMain.Name = "menuStripMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.saveAllModifiedToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.openLocationToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // ImportToolStripMenuItem
            // 
            this.ImportToolStripMenuItem.Image = global::EntryTranslator.Properties.Resources.openfolderHS;
            this.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem";
            resources.ApplyResources(this.ImportToolStripMenuItem, "ImportToolStripMenuItem");
            this.ImportToolStripMenuItem.Click += new System.EventHandler(this.ImportToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Image = global::EntryTranslator.Properties.Resources.GoToParentFolderHS;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            resources.ApplyResources(this.exportToolStripMenuItem, "exportToolStripMenuItem");
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportAllResourcesToolStripMenuItem_Click);
            // 
            // saveAllModifiedToolStripMenuItem
            // 
            this.saveAllModifiedToolStripMenuItem.Image = global::EntryTranslator.Properties.Resources.SaveAllHS;
            this.saveAllModifiedToolStripMenuItem.Name = "saveAllModifiedToolStripMenuItem";
            resources.ApplyResources(this.saveAllModifiedToolStripMenuItem, "saveAllModifiedToolStripMenuItem");
            this.saveAllModifiedToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Image = global::EntryTranslator.Properties.Resources.RefreshArrow;
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            resources.ApplyResources(this.reloadToolStripMenuItem, "reloadToolStripMenuItem");
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadCurrentDirectoryToolStripMenuItem_Click);
            // 
            // openLocationToolStripMenuItem
            // 
            this.openLocationToolStripMenuItem.Name = "openLocationToolStripMenuItem";
            resources.ApplyResources(this.openLocationToolStripMenuItem, "openLocationToolStripMenuItem");
            this.openLocationToolStripMenuItem.Click += new System.EventHandler(this.openLocationToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem1,
            this.findNextToolStripMenuItem,
            this.clearSearchToolStripMenuItem});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            resources.ApplyResources(this.searchToolStripMenuItem, "searchToolStripMenuItem");
            this.searchToolStripMenuItem.DropDownOpened += new System.EventHandler(this.findToolStripMenuItem_DropDownOpened);
            // 
            // findToolStripMenuItem1
            // 
            this.findToolStripMenuItem1.Image = global::EntryTranslator.Properties.Resources.Find_VS;
            this.findToolStripMenuItem1.Name = "findToolStripMenuItem1";
            resources.ApplyResources(this.findToolStripMenuItem1, "findToolStripMenuItem1");
            this.findToolStripMenuItem1.Click += new System.EventHandler(this.findToolStripMenuItem1_Click);
            // 
            // findNextToolStripMenuItem
            // 
            this.findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
            resources.ApplyResources(this.findNextToolStripMenuItem, "findNextToolStripMenuItem");
            this.findNextToolStripMenuItem.Click += new System.EventHandler(this.findNextToolStripMenuItem_Click);
            // 
            // clearSearchToolStripMenuItem
            // 
            this.clearSearchToolStripMenuItem.Name = "clearSearchToolStripMenuItem";
            resources.ApplyResources(this.clearSearchToolStripMenuItem, "clearSearchToolStripMenuItem");
            this.clearSearchToolStripMenuItem.Click += new System.EventHandler(this.clearSearchToolStripMenuItem_Click);
            // 
            // keysToolStripMenuItem
            // 
            this.keysToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewKeyToolStripMenuItem,
            this.deleteKeyToolStripMenuItem});
            this.keysToolStripMenuItem.Name = "keysToolStripMenuItem";
            resources.ApplyResources(this.keysToolStripMenuItem, "keysToolStripMenuItem");
            // 
            // addNewKeyToolStripMenuItem
            // 
            this.addNewKeyToolStripMenuItem.Image = global::EntryTranslator.Properties.Resources.Add;
            this.addNewKeyToolStripMenuItem.Name = "addNewKeyToolStripMenuItem";
            resources.ApplyResources(this.addNewKeyToolStripMenuItem, "addNewKeyToolStripMenuItem");
            this.addNewKeyToolStripMenuItem.Click += new System.EventHandler(this.addNewKeyToolStripMenuItem_Click);
            // 
            // deleteKeyToolStripMenuItem
            // 
            this.deleteKeyToolStripMenuItem.Image = global::EntryTranslator.Properties.Resources.Delete_black_32x32;
            this.deleteKeyToolStripMenuItem.Name = "deleteKeyToolStripMenuItem";
            resources.ApplyResources(this.deleteKeyToolStripMenuItem, "deleteKeyToolStripMenuItem");
            this.deleteKeyToolStripMenuItem.Click += new System.EventHandler(this.deleteKeyToolStripMenuItem_Click);
            // 
            // languagesToolStripMenuItem
            // 
            this.languagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLanguageToolStripMenuItem,
            this.removeLanguageToolStripMenuItem});
            this.languagesToolStripMenuItem.Name = "languagesToolStripMenuItem";
            resources.ApplyResources(this.languagesToolStripMenuItem, "languagesToolStripMenuItem");
            this.languagesToolStripMenuItem.DropDownOpened += new System.EventHandler(this.languagesToolStripMenuItem_DropDownOpened);
            // 
            // addLanguageToolStripMenuItem
            // 
            this.addLanguageToolStripMenuItem.Image = global::EntryTranslator.Properties.Resources.Add;
            this.addLanguageToolStripMenuItem.Name = "addLanguageToolStripMenuItem";
            resources.ApplyResources(this.addLanguageToolStripMenuItem, "addLanguageToolStripMenuItem");
            this.addLanguageToolStripMenuItem.Click += new System.EventHandler(this.addLanguageToolStripMenuItem_Clicked);
            // 
            // removeLanguageToolStripMenuItem
            // 
            this.removeLanguageToolStripMenuItem.Image = global::EntryTranslator.Properties.Resources.Delete_black_32x32;
            this.removeLanguageToolStripMenuItem.Name = "removeLanguageToolStripMenuItem";
            resources.ApplyResources(this.removeLanguageToolStripMenuItem, "removeLanguageToolStripMenuItem");
            this.removeLanguageToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.removeLanguageToolStripMenuItem_DropDownItemClicked);
            // 
            // toolStripMenuItemGT
            // 
            this.toolStripMenuItemGT.Name = "toolStripMenuItemGT";
            resources.ApplyResources(this.toolStripMenuItemGT, "toolStripMenuItemGT");
            this.toolStripMenuItemGT.Click += new System.EventHandler(this.translateToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Image = global::EntryTranslator.Properties.Resources.Help;
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            resources.ApplyResources(this.helpToolStripMenuItem1, "helpToolStripMenuItem1");
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // uiContextMenuStrip1
            // 
            this.uiContextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.uiContextMenuStrip1, "uiContextMenuStrip1");
            this.uiContextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.uiContextMenuStrip1.Name = "uiContextMenuStrip1";
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.splitContainerAll.Panel1.ResumeLayout(false);
            this.splitContainerAll.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAll)).EndInit();
            this.splitContainerAll.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPageEditedResource.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private Panel panelMain;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveAllModifiedToolStripMenuItem;
        private ToolStripMenuItem searchToolStripMenuItem;
        private ToolStripMenuItem findToolStripMenuItem1;
        private ToolStripMenuItem keysToolStripMenuItem;
        private ToolStripMenuItem addNewKeyToolStripMenuItem;
        private ToolStripMenuItem deleteKeyToolStripMenuItem;
        private ToolStripMenuItem addLanguageToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabelCurrentItem;
        private ToolStripMenuItem languagesToolStripMenuItem;
        private ToolStripMenuItem removeLanguageToolStripMenuItem;
        private ToolStripMenuItem clearSearchToolStripMenuItem;
        private ToolStripMenuItem openLocationToolStripMenuItem;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem findNextToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemGT;
        private ToolStripMenuItem ImportToolStripMenuItem;
        private SplitContainer splitContainerAll;
        private FlowLayoutPanel flowLayoutPanel1;
        private SplitContainer splitContainerMain;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Controls.LanguageSettings languageSettings1;
        private TabControl tabControl3;
        private TabPage tabPageEditedResource;
        private Controls.LanguageEditor resourceGrid1;
        private GroupBox groupBoxFile;
        private Button buttonImport;
        private Button buttonExport;
        private GroupBox groupBoxSearch;
        private Button buttonClearSearch;
        private Button buttonSearch;
        private TextBox textBoxSearch;
        private Button buttonSearchNext;
        private Sunny.UI.UIContextMenuStrip uiContextMenuStrip1;
    }
}

