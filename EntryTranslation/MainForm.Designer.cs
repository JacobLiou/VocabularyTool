using System.Windows.Forms;

namespace EntryTranslation
{
    sealed partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panelMain = new Panel();
            splitContainerMain = new SplitContainer();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            languageSettings1 = new Controls.LanguageSettings();
            tabControl3 = new TabControl();
            tabPageEditedResource = new TabPage();
            resourceGrid1 = new Controls.ResourceGrid();
            menuStripMain = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveAllModifiedToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            exportAllResourcesToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            revertCurrentFileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            findToolStripMenuItem = new ToolStripMenuItem();
            findToolStripMenuItem1 = new ToolStripMenuItem();
            findNextToolStripMenuItem = new ToolStripMenuItem();
            clearSearchToolStripMenuItem = new ToolStripMenuItem();
            keysToolStripMenuItem = new ToolStripMenuItem();
            addNewKeyToolStripMenuItem = new ToolStripMenuItem();
            deleteKeyToolStripMenuItem = new ToolStripMenuItem();
            languagesToolStripMenuItem = new ToolStripMenuItem();
            addLanguageToolStripMenuItem = new ToolStripMenuItem();
            removeLanguageToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem1 = new ToolStripMenuItem();
            导入ToolStripMenuItem = new ToolStripMenuItem();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabControl3.SuspendLayout();
            tabPageEditedResource.SuspendLayout();
            menuStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(splitContainerMain);
            resources.ApplyResources(panelMain, "panelMain");
            panelMain.Name = "panelMain";
            // 
            // splitContainerMain
            // 
            resources.ApplyResources(splitContainerMain, "splitContainerMain");
            splitContainerMain.FixedPanel = FixedPanel.Panel1;
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(tabControl1);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(tabControl3);
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(languageSettings1);
            resources.ApplyResources(tabPage1, "tabPage1");
            tabPage1.Name = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // languageSettings1
            // 
            resources.ApplyResources(languageSettings1, "languageSettings1");
            languageSettings1.Name = "languageSettings1";
            // 
            // tabControl3
            // 
            tabControl3.Controls.Add(tabPageEditedResource);
            resources.ApplyResources(tabControl3, "tabControl3");
            tabControl3.Name = "tabControl3";
            tabControl3.SelectedIndex = 0;
            // 
            // tabPageEditedResource
            // 
            tabPageEditedResource.Controls.Add(resourceGrid1);
            resources.ApplyResources(tabPageEditedResource, "tabPageEditedResource");
            tabPageEditedResource.Name = "tabPageEditedResource";
            tabPageEditedResource.UseVisualStyleBackColor = true;
            // 
            // resourceGrid1
            // 
            resourceGrid1.CurrentResource = null;
            resourceGrid1.CurrentSearch = null;
            resources.ApplyResources(resourceGrid1, "resourceGrid1");
            resourceGrid1.Name = "resourceGrid1";
            resourceGrid1.ShowNullValuesAsGrayed = false;
            // 
            // menuStripMain
            // 
            menuStripMain.ImageScalingSize = new Size(20, 20);
            menuStripMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, findToolStripMenuItem, keysToolStripMenuItem, languagesToolStripMenuItem, helpToolStripMenuItem });
            resources.ApplyResources(menuStripMain, "menuStripMain");
            menuStripMain.Name = "menuStripMain";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveAllModifiedToolStripMenuItem, closeToolStripMenuItem, 导入ToolStripMenuItem, exportAllResourcesToolStripMenuItem, toolStripSeparator1, saveToolStripMenuItem, revertCurrentFileToolStripMenuItem, toolStripSeparator3 });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = Properties.Resources.openfolderHS;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(openToolStripMenuItem, "openToolStripMenuItem");
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveAllModifiedToolStripMenuItem
            // 
            saveAllModifiedToolStripMenuItem.Image = Properties.Resources.SaveAllHS;
            saveAllModifiedToolStripMenuItem.Name = "saveAllModifiedToolStripMenuItem";
            resources.ApplyResources(saveAllModifiedToolStripMenuItem, "saveAllModifiedToolStripMenuItem");
            saveAllModifiedToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            resources.ApplyResources(closeToolStripMenuItem, "closeToolStripMenuItem");
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // exportAllResourcesToolStripMenuItem
            // 
            exportAllResourcesToolStripMenuItem.Name = "exportAllResourcesToolStripMenuItem";
            resources.ApplyResources(exportAllResourcesToolStripMenuItem, "exportAllResourcesToolStripMenuItem");
            exportAllResourcesToolStripMenuItem.Click += exportAllResourcesToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = Properties.Resources.saveHS;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(saveToolStripMenuItem, "saveToolStripMenuItem");
            saveToolStripMenuItem.Click += saveCurrentToolStripMenuItem_Click;
            // 
            // revertCurrentFileToolStripMenuItem
            // 
            revertCurrentFileToolStripMenuItem.Image = Properties.Resources.Edit_UndoHS;
            revertCurrentFileToolStripMenuItem.Name = "revertCurrentFileToolStripMenuItem";
            resources.ApplyResources(revertCurrentFileToolStripMenuItem, "revertCurrentFileToolStripMenuItem");
            revertCurrentFileToolStripMenuItem.Click += revertCurrentToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // findToolStripMenuItem
            // 
            findToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { findToolStripMenuItem1, findNextToolStripMenuItem, clearSearchToolStripMenuItem });
            findToolStripMenuItem.Name = "findToolStripMenuItem";
            resources.ApplyResources(findToolStripMenuItem, "findToolStripMenuItem");
            findToolStripMenuItem.DropDownOpened += findToolStripMenuItem_DropDownOpened;
            // 
            // findToolStripMenuItem1
            // 
            findToolStripMenuItem1.Image = Properties.Resources.Find_VS;
            findToolStripMenuItem1.Name = "findToolStripMenuItem1";
            resources.ApplyResources(findToolStripMenuItem1, "findToolStripMenuItem1");
            findToolStripMenuItem1.Click += findToolStripMenuItem1_Click;
            // 
            // findNextToolStripMenuItem
            // 
            findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
            resources.ApplyResources(findNextToolStripMenuItem, "findNextToolStripMenuItem");
            findNextToolStripMenuItem.Click += findNextToolStripMenuItem_Click;
            // 
            // clearSearchToolStripMenuItem
            // 
            clearSearchToolStripMenuItem.Name = "clearSearchToolStripMenuItem";
            resources.ApplyResources(clearSearchToolStripMenuItem, "clearSearchToolStripMenuItem");
            clearSearchToolStripMenuItem.Click += clearSearchToolStripMenuItem_Click;
            // 
            // keysToolStripMenuItem
            // 
            keysToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addNewKeyToolStripMenuItem, deleteKeyToolStripMenuItem });
            keysToolStripMenuItem.Name = "keysToolStripMenuItem";
            resources.ApplyResources(keysToolStripMenuItem, "keysToolStripMenuItem");
            // 
            // addNewKeyToolStripMenuItem
            // 
            addNewKeyToolStripMenuItem.Image = Properties.Resources.Add;
            addNewKeyToolStripMenuItem.Name = "addNewKeyToolStripMenuItem";
            resources.ApplyResources(addNewKeyToolStripMenuItem, "addNewKeyToolStripMenuItem");
            addNewKeyToolStripMenuItem.Click += addNewKeyToolStripMenuItem_Click;
            // 
            // deleteKeyToolStripMenuItem
            // 
            deleteKeyToolStripMenuItem.Image = Properties.Resources.Delete_black_32x32;
            deleteKeyToolStripMenuItem.Name = "deleteKeyToolStripMenuItem";
            resources.ApplyResources(deleteKeyToolStripMenuItem, "deleteKeyToolStripMenuItem");
            deleteKeyToolStripMenuItem.Click += deleteKeyToolStripMenuItem_Click;
            // 
            // languagesToolStripMenuItem
            // 
            languagesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addLanguageToolStripMenuItem, removeLanguageToolStripMenuItem });
            languagesToolStripMenuItem.Name = "languagesToolStripMenuItem";
            resources.ApplyResources(languagesToolStripMenuItem, "languagesToolStripMenuItem");
            languagesToolStripMenuItem.DropDownOpened += languagesToolStripMenuItem_DropDownOpened;
            // 
            // addLanguageToolStripMenuItem
            // 
            addLanguageToolStripMenuItem.Image = Properties.Resources.Add;
            addLanguageToolStripMenuItem.Name = "addLanguageToolStripMenuItem";
            resources.ApplyResources(addLanguageToolStripMenuItem, "addLanguageToolStripMenuItem");
            addLanguageToolStripMenuItem.DropDownItemClicked += addLanguageToolStripMenuItem_DropDownItemClicked;
            // 
            // removeLanguageToolStripMenuItem
            // 
            removeLanguageToolStripMenuItem.Image = Properties.Resources.Delete_black_32x32;
            removeLanguageToolStripMenuItem.Name = "removeLanguageToolStripMenuItem";
            resources.ApplyResources(removeLanguageToolStripMenuItem, "removeLanguageToolStripMenuItem");
            removeLanguageToolStripMenuItem.DropDownItemClicked += removeLanguageToolStripMenuItem_DropDownItemClicked;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { helpToolStripMenuItem1 });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // helpToolStripMenuItem1
            // 
            helpToolStripMenuItem1.Image = Properties.Resources.Help;
            helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            resources.ApplyResources(helpToolStripMenuItem1, "helpToolStripMenuItem1");
            helpToolStripMenuItem1.Click += helpToolStripMenuItem1_Click;
            // 
            // 导入ToolStripMenuItem
            // 
            导入ToolStripMenuItem.Name = "导入ToolStripMenuItem";
            resources.ApplyResources(导入ToolStripMenuItem, "导入ToolStripMenuItem");
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            Controls.Add(panelMain);
            Controls.Add(menuStripMain);
            Name = "MainForm";
            ZoomScaleRect = new Rectangle(19, 19, 1179, 747);
            FormClosing += MainForm_FormClosing;
            Shown += MainForm_Shown;
            panelMain.ResumeLayout(false);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabControl3.ResumeLayout(false);
            tabPageEditedResource.ResumeLayout(false);
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panelMain;
        private SplitContainer splitContainerMain;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveAllModifiedToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem revertCurrentFileToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem findToolStripMenuItem;
        private ToolStripMenuItem findToolStripMenuItem1;
        private ToolStripMenuItem keysToolStripMenuItem;
        private ToolStripMenuItem addNewKeyToolStripMenuItem;
        private ToolStripMenuItem deleteKeyToolStripMenuItem;
        private ToolStripMenuItem addLanguageToolStripMenuItem;
        private Controls.ResourceGrid resourceGrid1;
        private TabControl tabControl3;
        private TabPage tabPageEditedResource;
        private ToolStripMenuItem languagesToolStripMenuItem;
        private ToolStripMenuItem removeLanguageToolStripMenuItem;
        private ToolStripMenuItem clearSearchToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem exportAllResourcesToolStripMenuItem;
        private ToolStripMenuItem findNextToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Controls.LanguageSettings languageSettings1;
        private ToolStripMenuItem 导入ToolStripMenuItem;
    }
}

