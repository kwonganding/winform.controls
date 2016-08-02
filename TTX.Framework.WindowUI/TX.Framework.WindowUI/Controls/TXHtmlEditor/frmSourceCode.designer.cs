namespace TX.Framework.WindowUI.Controls
{
    partial class frmSourceCode
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
            this.txt_SourceCode = new TX.Framework.WindowUI.Controls.TXTextBox();
            this.panelWorkArea.SuspendLayout();
            this.panelControlArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(360, 1);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(240, 1);
            // 
            // panelWorkArea
            // 
            this.panelWorkArea.Controls.Add(this.txt_SourceCode);
            this.panelWorkArea.Size = new System.Drawing.Size(680, 361);
            // 
            // panelControlArea
            // 
            this.panelControlArea.Location = new System.Drawing.Point(6, 392);
            this.panelControlArea.Size = new System.Drawing.Size(680, 30);
            // 
            // txt_SourceCode
            // 
            this.txt_SourceCode.BackColor = System.Drawing.Color.Transparent;
            this.txt_SourceCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
            this.txt_SourceCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_SourceCode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_SourceCode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_SourceCode.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(55)))), ((int)(((byte)(188)))), ((int)(((byte)(241)))));
            this.txt_SourceCode.Image = null;
            this.txt_SourceCode.ImageSize = new System.Drawing.Size(0, 0);
            this.txt_SourceCode.Location = new System.Drawing.Point(0, 0);
            this.txt_SourceCode.Multiline = true;
            this.txt_SourceCode.Name = "txt_SourceCode";
            this.txt_SourceCode.Padding = new System.Windows.Forms.Padding(2);
            this.txt_SourceCode.PasswordChar = '\0';
            this.txt_SourceCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_SourceCode.Size = new System.Drawing.Size(680, 361);
            this.txt_SourceCode.TabIndex = 0;
            // 
            // frmSourceCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionHeight = 25;
            this.ClientSize = new System.Drawing.Size(692, 425);
            this.MaximizeBox = true;
            this.Name = "frmSourceCode";
            this.ResizeEnable = true;
            this.Text = "frmSourceCode";
            this.panelWorkArea.ResumeLayout(false);
            this.panelControlArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TXTextBox txt_SourceCode;

    }
}