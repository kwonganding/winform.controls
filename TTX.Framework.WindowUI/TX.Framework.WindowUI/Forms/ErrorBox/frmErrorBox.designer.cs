namespace TX.Framework.WindowUI.Forms
{
    partial class frmErrorBox
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbError = new System.Windows.Forms.Label();
            this.txTableLayoutPanel1 = new TX.Framework.WindowUI.Controls.TXTableLayoutPanel();
            this.textBox1 = new TX.Framework.WindowUI.Controls.TXTextBox();
            this.txtError = new TX.Framework.WindowUI.Controls.TXTextBox();
            this.txtErrorDetail = new TX.Framework.WindowUI.Controls.TXTextBox();
            this.panelWorkArea.SuspendLayout();
            this.panelControlArea.SuspendLayout();
            this.txTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(303, 1);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(183, 1);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
            // 
            // panelWorkArea
            // 
            this.panelWorkArea.Controls.Add(this.txTableLayoutPanel1);
            this.panelWorkArea.Size = new System.Drawing.Size(567, 346);
            // 
            // panelControlArea
            // 
            this.panelControlArea.Location = new System.Drawing.Point(6, 376);
            this.panelControlArea.Size = new System.Drawing.Size(567, 30);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "异常类型:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(3, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "堆栈信息:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbError.ForeColor = System.Drawing.Color.Red;
            this.lbError.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbError.Location = new System.Drawing.Point(3, 0);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(74, 30);
            this.lbError.TabIndex = 6;
            this.lbError.Text = "错误信息:";
            this.lbError.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txTableLayoutPanel1
            // 
            this.txTableLayoutPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
            this.txTableLayoutPanel1.ColumnCount = 2;
            this.txTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.txTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.txTableLayoutPanel1.Controls.Add(this.lbError, 0, 0);
            this.txTableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.txTableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.txTableLayoutPanel1.Controls.Add(this.textBox1, 1, 2);
            this.txTableLayoutPanel1.Controls.Add(this.txtError, 1, 0);
            this.txTableLayoutPanel1.Controls.Add(this.txtErrorDetail, 1, 3);
            this.txTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.txTableLayoutPanel1.Name = "txTableLayoutPanel1";
            this.txTableLayoutPanel1.RowCount = 5;
            this.txTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.txTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.txTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.txTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.txTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.txTableLayoutPanel1.Size = new System.Drawing.Size(567, 346);
            this.txTableLayoutPanel1.TabIndex = 9;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Transparent;
            this.textBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.ForeColor = System.Drawing.Color.Firebrick;
            this.textBox1.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(44)))), ((int)(((byte)(182)))), ((int)(((byte)(240)))));
            this.textBox1.Image = null;
            this.textBox1.ImageSize = new System.Drawing.Size(0, 0);
            this.textBox1.Location = new System.Drawing.Point(83, 63);
            this.textBox1.Name = "textBox1";
            this.textBox1.Padding = new System.Windows.Forms.Padding(2);
            this.textBox1.PasswordChar = '\0';
            this.textBox1.ReadOnly = true;
            this.textBox1.Required = false;
            this.textBox1.Size = new System.Drawing.Size(481, 22);
            this.textBox1.TabIndex = 9;
            // 
            // txtError
            // 
            this.txtError.BackColor = System.Drawing.Color.Transparent;
            this.txtError.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
            this.txtError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtError.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtError.ForeColor = System.Drawing.Color.Firebrick;
            this.txtError.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(44)))), ((int)(((byte)(182)))), ((int)(((byte)(240)))));
            this.txtError.Image = null;
            this.txtError.ImageSize = new System.Drawing.Size(0, 0);
            this.txtError.Location = new System.Drawing.Point(83, 3);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.Padding = new System.Windows.Forms.Padding(2);
            this.txtError.PasswordChar = '\0';
            this.txtError.ReadOnly = true;
            this.txtError.Required = false;
            this.txTableLayoutPanel1.SetRowSpan(this.txtError, 2);
            this.txtError.Size = new System.Drawing.Size(481, 54);
            this.txtError.TabIndex = 7;
            // 
            // txtErrorDetail
            // 
            this.txtErrorDetail.BackColor = System.Drawing.Color.Transparent;
            this.txtErrorDetail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
            this.txtErrorDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtErrorDetail.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtErrorDetail.ForeColor = System.Drawing.Color.Firebrick;
            this.txtErrorDetail.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(44)))), ((int)(((byte)(182)))), ((int)(((byte)(240)))));
            this.txtErrorDetail.Image = null;
            this.txtErrorDetail.ImageSize = new System.Drawing.Size(0, 0);
            this.txtErrorDetail.Location = new System.Drawing.Point(83, 93);
            this.txtErrorDetail.Multiline = true;
            this.txtErrorDetail.Name = "txtErrorDetail";
            this.txtErrorDetail.Padding = new System.Windows.Forms.Padding(2);
            this.txtErrorDetail.PasswordChar = '\0';
            this.txtErrorDetail.ReadOnly = true;
            this.txtErrorDetail.Required = false;
            this.txTableLayoutPanel1.SetRowSpan(this.txtErrorDetail, 2);
            this.txtErrorDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtErrorDetail.Size = new System.Drawing.Size(481, 250);
            this.txtErrorDetail.TabIndex = 10;
            // 
            // frmErrorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CapitionLogo = global::TX.Framework.WindowUI.Properties.Resources.naruto;
            this.CaptionColor = System.Drawing.Color.Maroon;
            this.CaptionFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(579, 409);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "frmErrorBox";
            this.Text = " 呃，出现异常了！";
            this.Load += new System.EventHandler(this.frmErrorBox_Load);
            this.panelWorkArea.ResumeLayout(false);
            this.panelControlArea.ResumeLayout(false);
            this.txTableLayoutPanel1.ResumeLayout(false);
            this.txTableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TX.Framework.WindowUI.Controls.TXTableLayoutPanel txTableLayoutPanel1;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private TX.Framework.WindowUI.Controls.TXTextBox textBox1;
        private TX.Framework.WindowUI.Controls.TXTextBox txtError;
        private TX.Framework.WindowUI.Controls.TXTextBox txtErrorDetail;


    }
}