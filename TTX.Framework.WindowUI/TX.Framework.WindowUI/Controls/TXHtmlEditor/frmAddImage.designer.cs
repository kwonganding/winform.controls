namespace TX.Framework.WindowUI.Controls
{
    partial class frmAddImage
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
            this.rBtn_URL = new TX.Framework.WindowUI.Controls.TXRadioButton();
            this.rBtn_Local = new TX.Framework.WindowUI.Controls.TXRadioButton();
            this.txt_URL = new TX.Framework.WindowUI.Controls.TXTextBox();
            this.btn_LocalImageUpdate = new TX.Framework.WindowUI.Controls.TXButton();
            this.txtFileName = new TX.Framework.WindowUI.Controls.TXTextBox();
            this.panelWorkArea.SuspendLayout();
            this.panelControlArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(249, 1);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(129, 1);
            // 
            // panelWorkArea
            // 
            this.panelWorkArea.Controls.Add(this.txtFileName);
            this.panelWorkArea.Controls.Add(this.btn_LocalImageUpdate);
            this.panelWorkArea.Controls.Add(this.txt_URL);
            this.panelWorkArea.Controls.Add(this.rBtn_Local);
            this.panelWorkArea.Controls.Add(this.rBtn_URL);
            this.panelWorkArea.Size = new System.Drawing.Size(459, 131);
            // 
            // panelControlArea
            // 
            this.panelControlArea.Location = new System.Drawing.Point(6, 161);
            this.panelControlArea.Size = new System.Drawing.Size(459, 30);
            // 
            // rBtn_URL
            // 
            this.rBtn_URL.AutoSize = true;
            this.rBtn_URL.Checked = true;
            this.rBtn_URL.Location = new System.Drawing.Point(12, 9);
            this.rBtn_URL.MaxRadius = 8;
            this.rBtn_URL.MinimumSize = new System.Drawing.Size(22, 22);
            this.rBtn_URL.MinRadius = 4;
            this.rBtn_URL.Name = "rBtn_URL";
            this.rBtn_URL.Size = new System.Drawing.Size(137, 22);
            this.rBtn_URL.TabIndex = 3;
            this.rBtn_URL.TabStop = true;
            this.rBtn_URL.Text = "URL地址（网络图片）";
            this.rBtn_URL.UseVisualStyleBackColor = true;
            this.rBtn_URL.CheckedChanged += new System.EventHandler(this.rBtn_URL_CheckedChanged);
            // 
            // rBtn_Local
            // 
            this.rBtn_Local.AutoSize = true;
            this.rBtn_Local.Location = new System.Drawing.Point(12, 67);
            this.rBtn_Local.MaxRadius = 8;
            this.rBtn_Local.MinimumSize = new System.Drawing.Size(22, 22);
            this.rBtn_Local.MinRadius = 4;
            this.rBtn_Local.Name = "rBtn_Local";
            this.rBtn_Local.Size = new System.Drawing.Size(71, 22);
            this.rBtn_Local.TabIndex = 2;
            this.rBtn_Local.Text = "本地图片";
            this.rBtn_Local.UseVisualStyleBackColor = true;
            // 
            // txt_URL
            // 
            this.txt_URL.BackColor = System.Drawing.Color.Transparent;
            this.txt_URL.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
            this.txt_URL.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_URL.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_URL.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(55)))), ((int)(((byte)(188)))), ((int)(((byte)(241)))));
            this.txt_URL.Image = null;
            this.txt_URL.ImageSize = new System.Drawing.Size(0, 0);
            this.txt_URL.Location = new System.Drawing.Point(36, 36);
            this.txt_URL.Name = "txt_URL";
            this.txt_URL.Padding = new System.Windows.Forms.Padding(2);
            this.txt_URL.PasswordChar = '\0';
            this.txt_URL.Required = false;
            this.txt_URL.Size = new System.Drawing.Size(412, 25);
            this.txt_URL.TabIndex = 0;
            this.txt_URL.Text = "http://";
            // 
            // btn_LocalImageUpdate
            // 
            this.btn_LocalImageUpdate.Image = null;
            this.btn_LocalImageUpdate.Location = new System.Drawing.Point(36, 95);
            this.btn_LocalImageUpdate.Name = "btn_LocalImageUpdate";
            this.btn_LocalImageUpdate.Size = new System.Drawing.Size(108, 25);
            this.btn_LocalImageUpdate.TabIndex = 1;
            this.btn_LocalImageUpdate.Text = "上传本地图片...";
            this.btn_LocalImageUpdate.UseVisualStyleBackColor = true;
            this.btn_LocalImageUpdate.Click += new System.EventHandler(this.btn_LocalImageUpdate_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.Color.Transparent;
            this.txtFileName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
            this.txtFileName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFileName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFileName.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(55)))), ((int)(((byte)(188)))), ((int)(((byte)(241)))));
            this.txtFileName.Image = null;
            this.txtFileName.ImageSize = new System.Drawing.Size(0, 0);
            this.txtFileName.Location = new System.Drawing.Point(151, 95);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Padding = new System.Windows.Forms.Padding(2);
            this.txtFileName.PasswordChar = '\0';
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Required = false;
            this.txtFileName.Size = new System.Drawing.Size(297, 25);
            this.txtFileName.TabIndex = 2;
            // 
            // frmAddImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionHeight = 25;
            this.ClientSize = new System.Drawing.Size(471, 194);
            this.Name = "frmAddImage";
            this.Text = "frmAddImage";
            this.panelWorkArea.ResumeLayout(false);
            this.panelWorkArea.PerformLayout();
            this.panelControlArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TXRadioButton rBtn_Local;
        private TXRadioButton rBtn_URL;
        private TXTextBox txt_URL;
        private TXButton btn_LocalImageUpdate;
        private TXTextBox txtFileName;

    }
}