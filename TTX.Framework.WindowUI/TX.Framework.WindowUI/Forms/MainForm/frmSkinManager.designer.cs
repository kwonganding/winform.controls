using TX.Framework.WindowUI.Controls;
namespace TX.Framework.WindowUI.Forms
{
    partial class frmSkinManager
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
            this.panelWorkArea = new TX.Framework.WindowUI.Controls.TXPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rbn0 = new TX.Framework.WindowUI.Controls.TXRadioButton();
            this.rbn1 = new TX.Framework.WindowUI.Controls.TXRadioButton();
            this.rbn3 = new TX.Framework.WindowUI.Controls.TXRadioButton();
            this.rbn2 = new TX.Framework.WindowUI.Controls.TXRadioButton();
            this.rbn4 = new TX.Framework.WindowUI.Controls.TXRadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbBgEnable = new TX.Framework.WindowUI.Controls.TXCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackOpacity = new System.Windows.Forms.TrackBar();
            this.picBoxBg = new System.Windows.Forms.PictureBox();
            this.panelControlArea = new System.Windows.Forms.Panel();
            this.btnApply = new TX.Framework.WindowUI.Controls.TXButton();
            this.btnCancel = new TX.Framework.WindowUI.Controls.TXButton();
            this.btnOK = new TX.Framework.WindowUI.Controls.TXButton();
            this.panelWorkArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBg)).BeginInit();
            this.panelControlArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelWorkArea
            // 
            this.panelWorkArea.BackColor = System.Drawing.Color.Transparent;
            this.panelWorkArea.BorderColor = System.Drawing.Color.LightGray;
            this.panelWorkArea.Controls.Add(this.tableLayoutPanel1);
            this.panelWorkArea.CornerRadius = 5;
            this.panelWorkArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWorkArea.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelWorkArea.Location = new System.Drawing.Point(6, 30);
            this.panelWorkArea.MinimumSize = new System.Drawing.Size(27, 27);
            this.panelWorkArea.Name = "panelWorkArea";
            this.panelWorkArea.Size = new System.Drawing.Size(441, 203);
            this.panelWorkArea.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.rbn0, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.rbn1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.rbn3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.rbn2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.rbn4, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(441, 203);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // rbn0
            // 
            this.rbn0.AutoSize = true;
            this.rbn0.Location = new System.Drawing.Point(23, 3);
            this.rbn0.MaxRadius = 8;
            this.rbn0.MinimumSize = new System.Drawing.Size(22, 22);
            this.rbn0.MinRadius = 4;
            this.rbn0.Name = "rbn0";
            this.rbn0.Size = new System.Drawing.Size(71, 22);
            this.rbn0.TabIndex = 1;
            this.rbn0.Tag = "0";
            this.rbn0.Text = "默认皮肤";
            this.rbn0.UseVisualStyleBackColor = true;
            this.rbn0.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            // 
            // rbn1
            // 
            this.rbn1.AutoSize = true;
            this.rbn1.Location = new System.Drawing.Point(23, 31);
            this.rbn1.MaxRadius = 8;
            this.rbn1.MinimumSize = new System.Drawing.Size(22, 22);
            this.rbn1.MinRadius = 4;
            this.rbn1.Name = "rbn1";
            this.rbn1.Size = new System.Drawing.Size(131, 22);
            this.rbn1.TabIndex = 2;
            this.rbn1.Tag = "1";
            this.rbn1.Text = "面朝大海，春暖花开";
            this.rbn1.UseVisualStyleBackColor = true;
            // 
            // rbn3
            // 
            this.rbn3.AutoSize = true;
            this.rbn3.Location = new System.Drawing.Point(23, 59);
            this.rbn3.MaxRadius = 8;
            this.rbn3.MinimumSize = new System.Drawing.Size(22, 22);
            this.rbn3.MinRadius = 4;
            this.rbn3.Name = "rbn3";
            this.rbn3.Size = new System.Drawing.Size(131, 22);
            this.rbn3.TabIndex = 3;
            this.rbn3.Tag = "3";
            this.rbn3.Text = "如花美眷，流年似水";
            this.rbn3.UseVisualStyleBackColor = true;
            // 
            // rbn2
            // 
            this.rbn2.AutoSize = true;
            this.rbn2.Location = new System.Drawing.Point(23, 87);
            this.rbn2.MaxRadius = 8;
            this.rbn2.MinimumSize = new System.Drawing.Size(22, 22);
            this.rbn2.MinRadius = 4;
            this.rbn2.Name = "rbn2";
            this.rbn2.Size = new System.Drawing.Size(71, 22);
            this.rbn2.TabIndex = 4;
            this.rbn2.Tag = "2";
            this.rbn2.Text = "天使之吻";
            this.rbn2.UseVisualStyleBackColor = true;
            // 
            // rbn4
            // 
            this.rbn4.AutoSize = true;
            this.rbn4.Location = new System.Drawing.Point(23, 115);
            this.rbn4.MaxRadius = 8;
            this.rbn4.MinimumSize = new System.Drawing.Size(22, 22);
            this.rbn4.MinRadius = 4;
            this.rbn4.Name = "rbn4";
            this.rbn4.Size = new System.Drawing.Size(131, 22);
            this.rbn4.TabIndex = 5;
            this.rbn4.Tag = "4";
            this.rbn4.Text = "夕阳西下，明月天涯";
            this.rbn4.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbBgEnable);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.trackOpacity);
            this.panel1.Controls.Add(this.picBoxBg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(23, 143);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 57);
            this.panel1.TabIndex = 6;
            // 
            // cbBgEnable
            // 
            this.cbBgEnable.AutoSize = true;
            this.cbBgEnable.Location = new System.Drawing.Point(3, 17);
            this.cbBgEnable.MinimumSize = new System.Drawing.Size(20, 20);
            this.cbBgEnable.Name = "cbBgEnable";
            this.cbBgEnable.Size = new System.Drawing.Size(96, 20);
            this.cbBgEnable.TabIndex = 3;
            this.cbBgEnable.Text = "开启主题背景";
            this.cbBgEnable.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(208, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "透明度";
            // 
            // trackOpacity
            // 
            this.trackOpacity.BackColor = System.Drawing.Color.White;
            this.trackOpacity.Location = new System.Drawing.Point(244, 14);
            this.trackOpacity.Maximum = 100;
            this.trackOpacity.Name = "trackOpacity";
            this.trackOpacity.Size = new System.Drawing.Size(168, 45);
            this.trackOpacity.TabIndex = 1;
            this.trackOpacity.TickFrequency = 5;
            this.trackOpacity.Value = 20;
            // 
            // picBoxBg
            // 
            this.picBoxBg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxBg.Location = new System.Drawing.Point(105, 4);
            this.picBoxBg.Name = "picBoxBg";
            this.picBoxBg.Size = new System.Drawing.Size(68, 50);
            this.picBoxBg.TabIndex = 0;
            this.picBoxBg.TabStop = false;
            this.picBoxBg.Click += new System.EventHandler(this.picBoxBg_Click);
            // 
            // panelControlArea
            // 
            this.panelControlArea.BackColor = System.Drawing.Color.Transparent;
            this.panelControlArea.Controls.Add(this.btnApply);
            this.panelControlArea.Controls.Add(this.btnCancel);
            this.panelControlArea.Controls.Add(this.btnOK);
            this.panelControlArea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlArea.Location = new System.Drawing.Point(6, 233);
            this.panelControlArea.Margin = new System.Windows.Forms.Padding(0);
            this.panelControlArea.Name = "panelControlArea";
            this.panelControlArea.Size = new System.Drawing.Size(441, 30);
            this.panelControlArea.TabIndex = 1;
            // 
            // btnApply
            // 
            this.btnApply.Image = null;
            this.btnApply.Location = new System.Drawing.Point(182, 1);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(80, 25);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "应 用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(271, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnBtnCancelClick);
            // 
            // btnOK
            // 
            this.btnOK.Image = null;
            this.btnOK.Location = new System.Drawing.Point(93, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确 定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.OnBtnOkClick);
            // 
            // frmSkinManager
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(453, 266);
            this.Controls.Add(this.panelWorkArea);
            this.Controls.Add(this.panelControlArea);
            this.CornerRadius = 1;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Location = new System.Drawing.Point(0, 0);
            this.LogoSize = new System.Drawing.Size(26, 26);
            this.MaximizeBox = false;
            this.Name = "frmSkinManager";
            this.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.ResizeEnable = false;
            this.Text = "皮肤管理中心";
            this.Load += new System.EventHandler(this.frmSkinManager_Load);
            this.panelWorkArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBg)).EndInit();
            this.panelControlArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected TXButton btnCancel;
        protected TXButton btnOK;
        protected TXPanel panelWorkArea;
        protected System.Windows.Forms.Panel panelControlArea;
        protected TXButton btnApply;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private TXRadioButton rbn0;
        private TXRadioButton rbn1;
        private TXRadioButton rbn3;
        private TXRadioButton rbn2;
        private TXRadioButton rbn4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBoxBg;
        private System.Windows.Forms.TrackBar trackOpacity;
        private System.Windows.Forms.Label label1;
        private TXCheckBox cbBgEnable;
    }
}