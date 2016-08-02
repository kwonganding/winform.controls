namespace TX.Framework.WindowUI.Controls
{
    partial class TXRangeValue
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
            this.tlpRangeValue = new System.Windows.Forms.TableLayoutPanel();
            this.labTitle = new System.Windows.Forms.Label();
            this.linkAdd = new System.Windows.Forms.LinkLabel();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.labLower = new System.Windows.Forms.Label();
            this.labUpper = new System.Windows.Forms.Label();
            this.labValue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tlpItems = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRangeValue.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpRangeValue
            // 
            this.tlpRangeValue.ColumnCount = 2;
            this.tlpRangeValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRangeValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRangeValue.Controls.Add(this.labTitle, 0, 0);
            this.tlpRangeValue.Controls.Add(this.linkAdd, 1, 0);
            this.tlpRangeValue.Controls.Add(this.tlpHeader, 0, 1);
            this.tlpRangeValue.Controls.Add(this.tlpItems, 0, 2);
            this.tlpRangeValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRangeValue.Location = new System.Drawing.Point(0, 0);
            this.tlpRangeValue.Name = "tlpRangeValue";
            this.tlpRangeValue.RowCount = 3;
            this.tlpRangeValue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpRangeValue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpRangeValue.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRangeValue.Size = new System.Drawing.Size(665, 409);
            this.tlpRangeValue.TabIndex = 0;
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitle.Location = new System.Drawing.Point(3, 0);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(326, 25);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "区域设置：";
            this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkAdd
            // 
            this.linkAdd.AutoSize = true;
            this.linkAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkAdd.Location = new System.Drawing.Point(335, 0);
            this.linkAdd.Name = "linkAdd";
            this.linkAdd.Size = new System.Drawing.Size(327, 25);
            this.linkAdd.TabIndex = 1;
            this.linkAdd.TabStop = true;
            this.linkAdd.Text = ">>添加设置(&A)";
            this.linkAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAdd_LinkClicked);
            // 
            // tlpHeader
            // 
            this.tlpHeader.ColumnCount = 4;
            this.tlpRangeValue.SetColumnSpan(this.tlpHeader, 2);
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tlpHeader.Controls.Add(this.labLower, 0, 0);
            this.tlpHeader.Controls.Add(this.labUpper, 1, 0);
            this.tlpHeader.Controls.Add(this.labValue, 2, 0);
            this.tlpHeader.Controls.Add(this.label4, 3, 0);
            this.tlpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHeader.Location = new System.Drawing.Point(3, 28);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 1;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpHeader.Size = new System.Drawing.Size(659, 24);
            this.tlpHeader.TabIndex = 2;
            // 
            // labLower
            // 
            this.labLower.AutoSize = true;
            this.labLower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labLower.Location = new System.Drawing.Point(3, 0);
            this.labLower.Name = "labLower";
            this.labLower.Size = new System.Drawing.Size(184, 24);
            this.labLower.TabIndex = 0;
            this.labLower.Text = "范围下限";
            this.labLower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labUpper
            // 
            this.labUpper.AutoSize = true;
            this.labUpper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labUpper.Location = new System.Drawing.Point(193, 0);
            this.labUpper.Name = "labUpper";
            this.labUpper.Size = new System.Drawing.Size(184, 24);
            this.labUpper.TabIndex = 1;
            this.labUpper.Text = "范围上限";
            this.labUpper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labValue
            // 
            this.labValue.AutoSize = true;
            this.labValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labValue.Location = new System.Drawing.Point(383, 0);
            this.labValue.Name = "labValue";
            this.labValue.Size = new System.Drawing.Size(156, 24);
            this.labValue.TabIndex = 2;
            this.labValue.Text = "值(%)";
            this.labValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(545, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "操作";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpItems
            // 
            this.tlpItems.AutoScroll = true;
            this.tlpItems.ColumnCount = 4;
            this.tlpRangeValue.SetColumnSpan(this.tlpItems, 2);
            this.tlpItems.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpItems.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpItems.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpItems.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tlpItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpItems.Location = new System.Drawing.Point(3, 58);
            this.tlpItems.Name = "tlpItems";
            this.tlpItems.RowCount = 2;
            this.tlpItems.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpItems.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpItems.Size = new System.Drawing.Size(659, 348);
            this.tlpItems.TabIndex = 3;
            // 
            // TXRangeValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tlpRangeValue);
            this.Name = "TXRangeValue";
            this.Size = new System.Drawing.Size(665, 409);
            this.tlpRangeValue.ResumeLayout(false);
            this.tlpRangeValue.PerformLayout();
            this.tlpHeader.ResumeLayout(false);
            this.tlpHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRangeValue;
        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.LinkLabel linkAdd;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private System.Windows.Forms.TableLayoutPanel tlpItems;
        private System.Windows.Forms.Label labLower;
        private System.Windows.Forms.Label labUpper;
        private System.Windows.Forms.Label labValue;
        private System.Windows.Forms.Label label4;
    }
}
