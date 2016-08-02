namespace WindowsTest
{
    partial class frmList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmList));
            System.Text.Template.TemplateContext templateContext1 = new System.Text.Template.TemplateContext();
            this.txStatusStrip1 = new TX.Framework.WindowUI.Controls.TXStatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.txToolStrip1 = new TX.Framework.WindowUI.Controls.TXToolStrip();
            this.新建NToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.打开OToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.保存SToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.打印PToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.剪切UToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.复制CToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.粘贴PToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.帮助LToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.txPager1 = new TX.Framework.WindowUI.Controls.TXPager();
            this.templateListView1 = new TX.Framework.WindowUI.Controls.TemplateListView();
            this.templateColumnHeader1 = ((TX.Framework.WindowUI.Controls.TemplateColumnHeader)(new TX.Framework.WindowUI.Controls.TemplateColumnHeader()));
            this.templateColumnHeader2 = ((TX.Framework.WindowUI.Controls.TemplateColumnHeader)(new TX.Framework.WindowUI.Controls.TemplateColumnHeader()));
            this.panelPager.SuspendLayout();
            this.panelSearchCondition.SuspendLayout();
            this.panelToolBar.SuspendLayout();
            this.panelWorkArea.SuspendLayout();
            this.txStatusStrip1.SuspendLayout();
            this.txToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPager
            // 
            this.panelPager.Controls.Add(this.txStatusStrip1);
            this.panelPager.Location = new System.Drawing.Point(6, 458);
            this.panelPager.Size = new System.Drawing.Size(725, 26);
            // 
            // panelSearchCondition
            // 
            this.panelSearchCondition.Controls.Add(this.txPager1);
            this.panelSearchCondition.Location = new System.Drawing.Point(6, 428);
            this.panelSearchCondition.Size = new System.Drawing.Size(725, 30);
            // 
            // panelToolBar
            // 
            this.panelToolBar.Controls.Add(this.txToolStrip1);
            this.panelToolBar.Size = new System.Drawing.Size(725, 28);
            // 
            // panelWorkArea
            // 
            this.panelWorkArea.Controls.Add(this.templateListView1);
            this.panelWorkArea.Size = new System.Drawing.Size(725, 370);
            // 
            // txStatusStrip1
            // 
            this.txStatusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.txStatusStrip1.BeginBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txStatusStrip1.EndBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.toolStripDropDownButton1});
            this.txStatusStrip1.Location = new System.Drawing.Point(0, 4);
            this.txStatusStrip1.Name = "txStatusStrip1";
            this.txStatusStrip1.Size = new System.Drawing.Size(725, 22);
            this.txStatusStrip1.TabIndex = 0;
            this.txStatusStrip1.Text = "txStatusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = "状态栏";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 20);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // txToolStrip1
            // 
            this.txToolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.txToolStrip1.BeginBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txToolStrip1.EndBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.txToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建NToolStripButton,
            this.打开OToolStripButton,
            this.保存SToolStripButton,
            this.打印PToolStripButton,
            this.toolStripSeparator,
            this.剪切UToolStripButton,
            this.复制CToolStripButton,
            this.粘贴PToolStripButton,
            this.toolStripSeparator1,
            this.帮助LToolStripButton});
            this.txToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.txToolStrip1.Name = "txToolStrip1";
            this.txToolStrip1.Size = new System.Drawing.Size(725, 25);
            this.txToolStrip1.TabIndex = 0;
            this.txToolStrip1.Text = "txToolStrip1";
            // 
            // 新建NToolStripButton
            // 
            this.新建NToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.新建NToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("新建NToolStripButton.Image")));
            this.新建NToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.新建NToolStripButton.Name = "新建NToolStripButton";
            this.新建NToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.新建NToolStripButton.Text = "新建(&N)";
            // 
            // 打开OToolStripButton
            // 
            this.打开OToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.打开OToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("打开OToolStripButton.Image")));
            this.打开OToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打开OToolStripButton.Name = "打开OToolStripButton";
            this.打开OToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.打开OToolStripButton.Text = "打开(&O)";
            // 
            // 保存SToolStripButton
            // 
            this.保存SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("保存SToolStripButton.Image")));
            this.保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.保存SToolStripButton.Name = "保存SToolStripButton";
            this.保存SToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.保存SToolStripButton.Text = "保存(&S)";
            // 
            // 打印PToolStripButton
            // 
            this.打印PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.打印PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("打印PToolStripButton.Image")));
            this.打印PToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打印PToolStripButton.Name = "打印PToolStripButton";
            this.打印PToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.打印PToolStripButton.Text = "打印(&P)";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // 剪切UToolStripButton
            // 
            this.剪切UToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.剪切UToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("剪切UToolStripButton.Image")));
            this.剪切UToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.剪切UToolStripButton.Name = "剪切UToolStripButton";
            this.剪切UToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.剪切UToolStripButton.Text = "剪切(&U)";
            // 
            // 复制CToolStripButton
            // 
            this.复制CToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.复制CToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("复制CToolStripButton.Image")));
            this.复制CToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.复制CToolStripButton.Name = "复制CToolStripButton";
            this.复制CToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.复制CToolStripButton.Text = "复制(&C)";
            // 
            // 粘贴PToolStripButton
            // 
            this.粘贴PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.粘贴PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("粘贴PToolStripButton.Image")));
            this.粘贴PToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.粘贴PToolStripButton.Name = "粘贴PToolStripButton";
            this.粘贴PToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.粘贴PToolStripButton.Text = "粘贴(&P)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // 帮助LToolStripButton
            // 
            this.帮助LToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.帮助LToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("帮助LToolStripButton.Image")));
            this.帮助LToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.帮助LToolStripButton.Name = "帮助LToolStripButton";
            this.帮助LToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.帮助LToolStripButton.Text = "帮助(&L)";
            // 
            // txPager1
            // 
            this.txPager1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.txPager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txPager1.Location = new System.Drawing.Point(0, 0);
            this.txPager1.Name = "txPager1";
            this.txPager1.PageSize = 20;
            this.txPager1.Size = new System.Drawing.Size(725, 30);
            this.txPager1.TabIndex = 0;
            this.txPager1.Total = 0D;
            // 
            // templateListView1
            // 
            this.templateListView1.BackColor = System.Drawing.Color.White;
            this.templateListView1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
            this.templateListView1.CheckedTester = null;
            this.templateListView1.Columns.Add(this.templateColumnHeader1);
            this.templateListView1.Columns.Add(this.templateColumnHeader2);
            this.templateListView1.DataSource = null;
            this.templateListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templateListView1.DoubleClickActivation = false;
            this.templateListView1.Font = new System.Drawing.Font("宋体", 9.6F);
            this.templateListView1.FullRowSelect = true;
            this.templateListView1.HeaderBeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.templateListView1.HeaderEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.templateListView1.Location = new System.Drawing.Point(0, 0);
            this.templateListView1.Name = "templateListView1";
            this.templateListView1.OwnerDraw = true;
            this.templateListView1.ResizeColumnsThreading = false;
            this.templateListView1.RowBackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(254)))));
            this.templateListView1.RowBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.templateListView1.SelectedBeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.templateListView1.SelectedEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(225)))), ((int)(((byte)(253)))));
            this.templateListView1.Size = new System.Drawing.Size(725, 370);
            this.templateListView1.TabIndex = 0;
            templateContext1.AssignmentPermissions = System.Text.Template.AssignmentPermissions.None;
            templateContext1.EmptyCollectionIsFalse = false;
            templateContext1.EmptyStringIsFalse = false;
            templateContext1.NonEmptyStringIsTrue = false;
            templateContext1.NotNullIsTrue = false;
            templateContext1.NotZeroIsTrue = false;
            templateContext1.NullIsFalse = true;
            templateContext1.ReturnNullWhenNullReference = false;
            this.templateListView1.TemplateContext = templateContext1;
            this.templateListView1.UseCompatibleStateImageBehavior = false;
            this.templateListView1.View = System.Windows.Forms.View.Details;
            // 
            // templateColumnHeader1
            // 
            this.templateColumnHeader1.Column = 0;
            this.templateColumnHeader1.Template = "$this.Name";
            this.templateColumnHeader1.Text = "年龄";
            this.templateColumnHeader1.Width = 180;
            // 
            // templateColumnHeader2
            // 
            this.templateColumnHeader2.Column = 0;
            this.templateColumnHeader2.Template = "$this.Value";
            this.templateColumnHeader2.Text = "年龄";
            this.templateColumnHeader2.Width = 200;
            // 
            // frmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 490);
            this.Name = "frmList";
            this.TabText = "第一个测试窗体";
            this.Text = "Form3";
            this.panelPager.ResumeLayout(false);
            this.panelPager.PerformLayout();
            this.panelSearchCondition.ResumeLayout(false);
            this.panelToolBar.ResumeLayout(false);
            this.panelToolBar.PerformLayout();
            this.panelWorkArea.ResumeLayout(false);
            this.txStatusStrip1.ResumeLayout(false);
            this.txStatusStrip1.PerformLayout();
            this.txToolStrip1.ResumeLayout(false);
            this.txToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TX.Framework.WindowUI.Controls.TXStatusStrip txStatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private TX.Framework.WindowUI.Controls.TXToolStrip txToolStrip1;
        private System.Windows.Forms.ToolStripButton 新建NToolStripButton;
        private System.Windows.Forms.ToolStripButton 打开OToolStripButton;
        private System.Windows.Forms.ToolStripButton 保存SToolStripButton;
        private System.Windows.Forms.ToolStripButton 打印PToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton 剪切UToolStripButton;
        private System.Windows.Forms.ToolStripButton 复制CToolStripButton;
        private System.Windows.Forms.ToolStripButton 粘贴PToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton 帮助LToolStripButton;
        private TX.Framework.WindowUI.Controls.TXPager txPager1;
        private TX.Framework.WindowUI.Controls.TemplateListView templateListView1;
        private TX.Framework.WindowUI.Controls.TemplateColumnHeader templateColumnHeader1;
        private TX.Framework.WindowUI.Controls.TemplateColumnHeader templateColumnHeader2;

    }
}