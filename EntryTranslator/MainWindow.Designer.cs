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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelCurrentItem = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPageEditedResource = new System.Windows.Forms.TabPage();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllModifiedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.languageSettings1 = new EntryTranslator.Controls.LanguageSettings();
            this.resourceGrid1 = new EntryTranslator.Controls.LanguageEditor();
            this.ImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPageEditedResource.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
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
            this.panelMain.Controls.Add(this.splitContainerMain);
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.Name = "panelMain";
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
            // menuStripMain
            // 
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.keysToolStripMenuItem,
            this.languagesToolStripMenuItem,
            this.toolStripMenuItemGT,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStripMain, "menuStripMain");
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
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            resources.ApplyResources(this.exportToolStripMenuItem, "exportToolStripMenuItem");
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportAllResourcesToolStripMenuItem_Click);
            // 
            // openLocationToolStripMenuItem
            // 
            this.openLocationToolStripMenuItem.Name = "openLocationToolStripMenuItem";
            resources.ApplyResources(this.openLocationToolStripMenuItem, "openLocationToolStripMenuItem");
            this.openLocationToolStripMenuItem.Click += new System.EventHandler(this.openResourceLocationToolStripMenuItem_Click);
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
            this.toolStripMenuItemGT.Click += new System.EventHandler(this.toolStripMenuItemGT_Click);
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
            // languageSettings1
            // 
            resources.ApplyResources(this.languageSettings1, "languageSettings1");
            this.languageSettings1.Name = "languageSettings1";
            // 
            // resourceGrid1
            // 
            this.resourceGrid1.CurrentResource = null;
            this.resourceGrid1.CurrentSearch = null;
            resources.ApplyResources(this.resourceGrid1, "resourceGrid1");
            this.resourceGrid1.Name = "resourceGrid1";
            this.resourceGrid1.ShowNullValuesAsGrayed = false;
            // 
            // ImportToolStripMenuItem
            // 
            this.ImportToolStripMenuItem.Image = global::EntryTranslator.Properties.Resources.openfolderHS;
            this.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem";
            resources.ApplyResources(this.ImportToolStripMenuItem, "ImportToolStripMenuItem");
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPageEditedResource.ResumeLayout(false);
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
        private SplitContainer splitContainerMain;
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
        private Controls.LanguageEditor resourceGrid1;
        private TabControl tabControl3;
        private TabPage tabPageEditedResource;
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
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Controls.LanguageSettings languageSettings1;
        private ToolStripMenuItem ImportToolStripMenuItem;
    }
}

