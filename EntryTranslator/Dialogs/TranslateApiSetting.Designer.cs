namespace EntryTranslator.Dialogs
{
    partial class TranslateApiSetting
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
            this.chbOverwrite = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbSourse = new Sunny.UI.UIComboBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.cbTarget = new Sunny.UI.UIComboBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // chbOverwrite
            // 
            this.chbOverwrite.AutoSize = true;
            this.chbOverwrite.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chbOverwrite.Location = new System.Drawing.Point(74, 185);
            this.chbOverwrite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chbOverwrite.Name = "chbOverwrite";
            this.chbOverwrite.Size = new System.Drawing.Size(151, 24);
            this.chbOverwrite.TabIndex = 3;
            this.chbOverwrite.Text = "覆盖已有翻译词条";
            this.chbOverwrite.UseVisualStyleBackColor = true;
            this.chbOverwrite.CheckedChanged += new System.EventHandler(this.ChbOverwrite_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(71, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 98;
            this.label1.Text = "源语言";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(71, 126);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 99;
            this.label2.Text = "目标语言";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(165, 226);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(101, 32);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(343, 226);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 32);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cbSourse
            // 
            this.cbSourse.DataSource = null;
            this.cbSourse.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cbSourse.FillColor = System.Drawing.Color.White;
            this.cbSourse.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSourse.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cbSourse.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cbSourse.Location = new System.Drawing.Point(211, 66);
            this.cbSourse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSourse.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbSourse.Name = "cbSourse";
            this.cbSourse.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbSourse.Size = new System.Drawing.Size(293, 29);
            this.cbSourse.SymbolSize = 24;
            this.cbSourse.TabIndex = 100;
            this.cbSourse.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbSourse.Watermark = "";
            // 
            // uiLabel1
            // 
            this.uiLabel1.BackColor = System.Drawing.Color.Cornsilk;
            this.uiLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.Firebrick;
            this.uiLabel1.Location = new System.Drawing.Point(80, 280);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(464, 28);
            this.uiLabel1.TabIndex = 101;
            this.uiLabel1.Text = "实验性功能，翻译来自于腾讯翻译君，语言语种受到限制";
            // 
            // cbTarget
            // 
            this.cbTarget.DataSource = null;
            this.cbTarget.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cbTarget.FillColor = System.Drawing.Color.White;
            this.cbTarget.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbTarget.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cbTarget.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cbTarget.Location = new System.Drawing.Point(211, 121);
            this.cbTarget.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbTarget.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbTarget.Name = "cbTarget";
            this.cbTarget.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbTarget.Size = new System.Drawing.Size(293, 29);
            this.cbTarget.SymbolSize = 24;
            this.cbTarget.TabIndex = 102;
            this.cbTarget.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbTarget.Watermark = "";
            // 
            // uiLabel2
            // 
            this.uiLabel2.BackColor = System.Drawing.Color.Cornsilk;
            this.uiLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.Firebrick;
            this.uiLabel2.Location = new System.Drawing.Point(224, 319);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(155, 28);
            this.uiLabel2.TabIndex = 101;
            this.uiLabel2.Text = "联网API查询可能受限";
            // 
            // TranslateApiSetting
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(636, 359);
            this.Controls.Add(this.cbTarget);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.cbSourse);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chbOverwrite);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TranslateApiSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "翻译设置";
            this.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Load += new System.EventHandler(this.TranslateApiSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbOverwrite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private Sunny.UI.UIComboBox cbSourse;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIComboBox cbTarget;
        private Sunny.UI.UILabel uiLabel2;
    }
}