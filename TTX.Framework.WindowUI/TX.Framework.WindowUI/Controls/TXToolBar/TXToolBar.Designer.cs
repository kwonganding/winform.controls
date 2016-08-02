namespace TX.Framework.WindowUI.Controls
{
    partial class TXToolBar
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.txToolStrip1 = new TX.Framework.WindowUI.Controls.TXToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbDetails = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.panelCheckItem = new System.Windows.Forms.Panel();
            this.linkInvert = new System.Windows.Forms.LinkLabel();
            this.linkAll = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.txToolStrip1.SuspendLayout();
            this.panelCheckItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txToolStrip1);
            this.panel1.Controls.Add(this.panelCheckItem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(692, 26);
            this.panel1.TabIndex = 0;
            // 
            // txToolStrip1
            // 
            this.txToolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.txToolStrip1.BeginBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.txToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txToolStrip1.EndBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.txToolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.txToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.txToolStrip1.ImageScalingSize = new System.Drawing.Size(17, 17);
            this.txToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.tsbAdd,
            this.tsbEdit,
            this.tsbDetails,
            this.tsbDelete,
            this.tsbRefresh,
            this.toolStripSeparator2,
            this.tsbExport,
            this.tsbHelp});
            this.txToolStrip1.Location = new System.Drawing.Point(72, 2);
            this.txToolStrip1.Name = "txToolStrip1";
            this.txToolStrip1.Size = new System.Drawing.Size(618, 22);
            this.txToolStrip1.TabIndex = 1;
            this.txToolStrip1.Text = "txToolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 22);
            // 
            // tsbAdd
            // 
            this.tsbAdd.Image = global::TX.Framework.WindowUI.Properties.Resources.add;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Margin = new System.Windows.Forms.Padding(0);
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(68, 22);
            this.tsbAdd.Text = "新增(&A)";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbEdit
            // 
            this.tsbEdit.Image = global::TX.Framework.WindowUI.Properties.Resources.edit;
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Margin = new System.Windows.Forms.Padding(0);
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(68, 22);
            this.tsbEdit.Text = "修改(&E)";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // tsbDetails
            // 
            this.tsbDetails.Image = global::TX.Framework.WindowUI.Properties.Resources.Detail;
            this.tsbDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDetails.Name = "tsbDetails";
            this.tsbDetails.Size = new System.Drawing.Size(50, 19);
            this.tsbDetails.Text = "详情";
            this.tsbDetails.ToolTipText = "查看详情";
            this.tsbDetails.Click += new System.EventHandler(this.tsbDetails_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.Image = global::TX.Framework.WindowUI.Properties.Resources.delete;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Margin = new System.Windows.Forms.Padding(0);
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(68, 22);
            this.tsbDelete.Text = "删除(&D)";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::TX.Framework.WindowUI.Properties.Resources.refresh2;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(68, 22);
            this.tsbRefresh.Text = "刷新(&R)";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 22);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = global::TX.Framework.WindowUI.Properties.Resources.excel;
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Margin = new System.Windows.Forms.Padding(0);
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(92, 22);
            this.tsbExport.Text = "导出数据(&X)";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tsbHelp
            // 
            this.tsbHelp.Image = global::TX.Framework.WindowUI.Properties.Resources.help;
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Margin = new System.Windows.Forms.Padding(0);
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(68, 22);
            this.tsbHelp.Text = "帮助(&H)";
            this.tsbHelp.Click += new System.EventHandler(this.tsbHelp_Click);
            // 
            // panelCheckItem
            // 
            this.panelCheckItem.Controls.Add(this.linkInvert);
            this.panelCheckItem.Controls.Add(this.linkAll);
            this.panelCheckItem.Controls.Add(this.label1);
            this.panelCheckItem.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCheckItem.Location = new System.Drawing.Point(2, 2);
            this.panelCheckItem.Name = "panelCheckItem";
            this.panelCheckItem.Size = new System.Drawing.Size(70, 22);
            this.panelCheckItem.TabIndex = 0;
            // 
            // linkInvert
            // 
            this.linkInvert.AutoSize = true;
            this.linkInvert.LinkColor = System.Drawing.Color.DarkViolet;
            this.linkInvert.Location = new System.Drawing.Point(37, 5);
            this.linkInvert.Name = "linkInvert";
            this.linkInvert.Size = new System.Drawing.Size(29, 12);
            this.linkInvert.TabIndex = 1;
            this.linkInvert.TabStop = true;
            this.linkInvert.Text = "反选";
            this.linkInvert.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkInvert_LinkClicked);
            // 
            // linkAll
            // 
            this.linkAll.AutoSize = true;
            this.linkAll.LinkColor = System.Drawing.Color.DarkViolet;
            this.linkAll.Location = new System.Drawing.Point(3, 5);
            this.linkAll.Name = "linkAll";
            this.linkAll.Size = new System.Drawing.Size(29, 12);
            this.linkAll.TabIndex = 0;
            this.linkAll.TabStop = true;
            this.linkAll.Text = "全选";
            this.linkAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAll_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "|";
            // 
            // TXToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.panel1);
            this.Name = "TXToolBar";
            this.Size = new System.Drawing.Size(692, 26);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.txToolStrip1.ResumeLayout(false);
            this.txToolStrip1.PerformLayout();
            this.panelCheckItem.ResumeLayout(false);
            this.panelCheckItem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelCheckItem;
        private System.Windows.Forms.LinkLabel linkInvert;
        private System.Windows.Forms.LinkLabel linkAll;
        public TXToolStrip txToolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.ToolStripButton tsbDetails;
        private System.Windows.Forms.Label label1;

    }
}
