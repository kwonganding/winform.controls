namespace TX.Framework.WindowUI.Forms
{
    partial class TXWaitingBox
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
            this.txPanel1 = new TX.Framework.WindowUI.Controls.TXPanel();
            this.loadImage = new System.Windows.Forms.PictureBox();
            this.labWaitMessage = new System.Windows.Forms.Label();
            this.txPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadImage)).BeginInit();
            this.SuspendLayout();
            // 
            // txPanel1
            // 
            this.txPanel1.BackColor = System.Drawing.Color.Transparent;
            this.txPanel1.BorderColor = System.Drawing.Color.DarkGray;
            this.txPanel1.Controls.Add(this.loadImage);
            this.txPanel1.Controls.Add(this.labWaitMessage);
            this.txPanel1.Location = new System.Drawing.Point(17, 39);
            this.txPanel1.Name = "txPanel1";
            this.txPanel1.Size = new System.Drawing.Size(340, 116);
            this.txPanel1.TabIndex = 0;
            // 
            // loadImage
            // 
            this.loadImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.loadImage.Image = global::TX.Framework.WindowUI.Properties.Resources.loader;
            this.loadImage.Location = new System.Drawing.Point(129, 2);
            this.loadImage.Name = "loadImage";
            this.loadImage.Size = new System.Drawing.Size(72, 70);
            this.loadImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loadImage.TabIndex = 0;
            this.loadImage.TabStop = false;
            // 
            // labWaitMessage
            // 
            this.labWaitMessage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labWaitMessage.ForeColor = System.Drawing.Color.ForestGreen;
            this.labWaitMessage.Location = new System.Drawing.Point(3, 73);
            this.labWaitMessage.Name = "labWaitMessage";
            this.labWaitMessage.Size = new System.Drawing.Size(330, 39);
            this.labWaitMessage.TabIndex = 1;
            this.labWaitMessage.Text = "正在处理，请稍后...";
            this.labWaitMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXWaitingBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientSize = new System.Drawing.Size(374, 172);
            this.ControlBox = false;
            this.Controls.Add(this.txPanel1);
            this.CornerRadius = 2;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "TXWaitingBox";
            this.ResizeEnable = false;
            this.Text = "天下酒店网-请稍后...";
            this.txPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TX.Framework.WindowUI.Controls.TXPanel txPanel1;
        private System.Windows.Forms.Label labWaitMessage;
        private System.Windows.Forms.PictureBox loadImage;
    }
}