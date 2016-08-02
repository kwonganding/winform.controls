using TX.Framework.WindowUI.Controls;
namespace TX.Framework.WindowUI.Forms
{
    partial class FormInfoListEntity
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
            System.Text.Template.TemplateContext templateContext1 = new System.Text.Template.TemplateContext();
            this.pager = new TX.Framework.WindowUI.Controls.TXPager();
            this.toolBar = new TX.Framework.WindowUI.Controls.TXToolBar();
            this.tlvList = new TX.Framework.WindowUI.Controls.TemplateListView();
            this.panelPager.SuspendLayout();
            this.panelToolBar.SuspendLayout();
            this.panelWorkArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPager
            // 
            this.panelPager.Controls.Add(this.pager);
            this.panelPager.Location = new System.Drawing.Point(6, 518);
            this.panelPager.Size = new System.Drawing.Size(827, 26);
            // 
            // panelSearchCondition
            // 
            this.panelSearchCondition.Location = new System.Drawing.Point(6, 488);
            this.panelSearchCondition.Size = new System.Drawing.Size(827, 30);
            // 
            // panelToolBar
            // 
            this.panelToolBar.Controls.Add(this.toolBar);
            this.panelToolBar.Size = new System.Drawing.Size(827, 28);
            // 
            // panelWorkArea
            // 
            this.panelWorkArea.Controls.Add(this.tlvList);
            this.panelWorkArea.Size = new System.Drawing.Size(827, 430);
            // 
            // pager
            // 
            this.pager.BackColor = System.Drawing.Color.Transparent;
            this.pager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pager.Location = new System.Drawing.Point(0, 0);
            this.pager.Name = "pager";
            this.pager.PageSize = 900;
            this.pager.Size = new System.Drawing.Size(827, 26);
            this.pager.TabIndex = 0;
            this.pager.Total = 0D;
            // 
            // toolBar
            // 
            this.toolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.toolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(827, 28);
            this.toolBar.TabIndex = 0;
            this.toolBar.CheckAll += new System.EventHandler(this.toolBar_CheckAll);
            this.toolBar.CheckInvert += new System.EventHandler(this.toolBar_CheckInvert);
            // 
            // tlvList
            // 
            this.tlvList.BackColor = System.Drawing.Color.White;
            this.tlvList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
            this.tlvList.DataSource = null;
            this.tlvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvList.DoubleClickActivation = false;
            this.tlvList.Font = new System.Drawing.Font("宋体", 9.6F);
            this.tlvList.FullRowSelect = true;
            this.tlvList.HeaderBeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.tlvList.HeaderEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.tlvList.Location = new System.Drawing.Point(0, 0);
            this.tlvList.Name = "tlvList";
            this.tlvList.OwnerDraw = true;
            this.tlvList.ResizeColumnsThreading = false;
            this.tlvList.RowBackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(254)))));
            this.tlvList.RowBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.tlvList.SelectedBeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.tlvList.SelectedEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(225)))), ((int)(((byte)(253)))));
            this.tlvList.Size = new System.Drawing.Size(827, 430);
            this.tlvList.TabIndex = 0;
            templateContext1.AssignmentPermissions = System.Text.Template.AssignmentPermissions.None;
            templateContext1.EmptyCollectionIsFalse = false;
            templateContext1.EmptyStringIsFalse = false;
            templateContext1.NonEmptyStringIsTrue = false;
            templateContext1.NotNullIsTrue = false;
            templateContext1.NotZeroIsTrue = false;
            templateContext1.NullIsFalse = true;
            templateContext1.ReturnNullWhenNullReference = false;
            this.tlvList.TemplateContext = templateContext1;
            this.tlvList.UseCompatibleStateImageBehavior = false;
            this.tlvList.View = System.Windows.Forms.View.Details;
            // 
            // FormInfoListEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 550);
            this.Name = "FormInfoListEntity";
            this.Text = "FormInfoListEntity";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormInfoListEntity_KeyDown);
            this.panelPager.ResumeLayout(false);
            this.panelToolBar.ResumeLayout(false);
            this.panelWorkArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected TXPager pager;
        protected Controls.TXToolBar toolBar;
        protected TemplateListView tlvList;

    }
}