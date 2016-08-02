namespace TX.Framework.WindowUI.Controls
{
    partial class frmPreview
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panelWorkArea.SuspendLayout();
            this.panelControlArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(380, 1);
            this.btnCancel.Size = new System.Drawing.Size(94, 25);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(303, 1);
            this.btnOK.Size = new System.Drawing.Size(94, 25);
            // 
            // panelWorkArea
            // 
            this.panelWorkArea.BorderColor = System.Drawing.Color.Silver;
            this.panelWorkArea.Controls.Add(this.webBrowser1);
            this.panelWorkArea.Padding = new System.Windows.Forms.Padding(3);
            this.panelWorkArea.Size = new System.Drawing.Size(700, 375);
            // 
            // panelControlArea
            // 
            this.panelControlArea.Location = new System.Drawing.Point(6, 406);
            this.panelControlArea.Size = new System.Drawing.Size(700, 30);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(694, 369);
            this.webBrowser1.TabIndex = 0;
            // 
            // frmPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionHeight = 25;
            this.ClientSize = new System.Drawing.Size(712, 439);
            this.ControlMargin = 25;
            this.MaximizeBox = true;
            this.Name = "frmPreview";
            this.ResizeEnable = true;
            this.ShowBtnCancel = false;
            this.Text = "frmPreview";
            this.panelWorkArea.ResumeLayout(false);
            this.panelControlArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;

    }
}