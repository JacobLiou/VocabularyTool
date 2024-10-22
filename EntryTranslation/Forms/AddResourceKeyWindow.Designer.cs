namespace EntryTranslation.Forms
{
    partial class AddResourceKeyWindow
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddResourceKeyWindow));
            tableLayoutPanel = new TableLayoutPanel();
            labelKey = new Label();
            labelNoXlateValue = new Label();
            textboxKeyName = new TextBox();
            textboxDefault = new TextBox();
            labelDefaultValue = new Label();
            textboxTranslated = new TextBox();
            btnAdd = new Button();
            errorProvider = new ErrorProvider(components);
            button1 = new Button();
            panel1 = new Panel();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(tableLayoutPanel, "tableLayoutPanel");
            tableLayoutPanel.Controls.Add(labelKey, 0, 0);
            tableLayoutPanel.Controls.Add(labelNoXlateValue, 0, 1);
            tableLayoutPanel.Controls.Add(textboxKeyName, 1, 0);
            tableLayoutPanel.Controls.Add(textboxDefault, 1, 1);
            tableLayoutPanel.Controls.Add(labelDefaultValue, 0, 2);
            tableLayoutPanel.Controls.Add(textboxTranslated, 1, 2);
            tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // labelKey
            // 
            resources.ApplyResources(labelKey, "labelKey");
            labelKey.Name = "labelKey";
            // 
            // labelNoXlateValue
            // 
            resources.ApplyResources(labelNoXlateValue, "labelNoXlateValue");
            labelNoXlateValue.Name = "labelNoXlateValue";
            // 
            // textboxKeyName
            // 
            resources.ApplyResources(textboxKeyName, "textboxKeyName");
            textboxKeyName.Name = "textboxKeyName";
            textboxKeyName.TextChanged += txtKey_TextChanged;
            // 
            // textboxDefault
            // 
            resources.ApplyResources(textboxDefault, "textboxDefault");
            textboxDefault.Name = "textboxDefault";
            // 
            // labelDefaultValue
            // 
            resources.ApplyResources(labelDefaultValue, "labelDefaultValue");
            labelDefaultValue.Name = "labelDefaultValue";
            // 
            // textboxTranslated
            // 
            resources.ApplyResources(textboxTranslated, "textboxTranslated");
            textboxTranslated.Name = "textboxTranslated";
            // 
            // btnAdd
            // 
            resources.ApplyResources(btnAdd, "btnAdd");
            btnAdd.DialogResult = DialogResult.OK;
            btnAdd.Name = "btnAdd";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.DialogResult = DialogResult.Cancel;
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Controls.Add(btnAdd);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // AddResourceKeyWindow
            // 
            AcceptButton = btnAdd;
            AutoScaleMode = AutoScaleMode.None;
            CancelButton = button1;
            resources.ApplyResources(this, "$this");
            Controls.Add(tableLayoutPanel);
            Controls.Add(panel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddResourceKeyWindow";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            ZoomScaleRect = new Rectangle(19, 19, 398, 170);
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Label labelNoXlateValue;
        private System.Windows.Forms.TextBox textboxKeyName;
        private System.Windows.Forms.TextBox textboxDefault;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label labelDefaultValue;
        private System.Windows.Forms.TextBox textboxTranslated;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}