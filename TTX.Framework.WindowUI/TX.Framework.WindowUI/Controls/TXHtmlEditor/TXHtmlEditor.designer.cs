//#####################################################################################
//Ryan-2010-9-19
//说明：HTML编辑器下载自网络开源代码（http://www.cnpopsoft.com ）,在其基础之上扩展的新的功
//能，进一步完善了原有的功能。
//#####################################################################################

namespace TX.Framework.WindowUI.Controls
{
    partial class TXHtmlEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TXHtmlEditor));
            this.webBrowserBody = new System.Windows.Forms.WebBrowser();
            this.toolStripToolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxName = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBoxSize = new TX.Framework.WindowUI.Controls.TXHtmlEditor.ToolStripComboBoxEx();
            this.toolStripButtonBold = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonItalic = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUnderline = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorFont = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonNumbers = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBullets = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOutdent = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonIndent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorFormat = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCenter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFull = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorAlign = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHyperlink = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPicture = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSourceCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButtonPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowserBody
            // 
            this.webBrowserBody.AllowWebBrowserDrop = false;
            this.webBrowserBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserBody.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserBody.Location = new System.Drawing.Point(3, 28);
            this.webBrowserBody.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserBody.Name = "webBrowserBody";
            this.webBrowserBody.Size = new System.Drawing.Size(558, 115);
            this.webBrowserBody.TabIndex = 0;
            this.webBrowserBody.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserBody_DocumentCompleted);
            this.webBrowserBody.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.webBrowserBody_PreviewKeyDown);
            // 
            // toolStripToolBar
            // 
            this.toolStripToolBar.BackColor = System.Drawing.Color.Transparent;
            this.toolStripToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxName,
            this.toolStripComboBoxSize,
            this.toolStripButtonBold,
            this.toolStripButtonItalic,
            this.toolStripButtonUnderline,
            this.toolStripButtonColor,
            this.toolStripSeparatorFont,
            this.toolStripButtonNumbers,
            this.toolStripButtonBullets,
            this.toolStripButtonOutdent,
            this.toolStripButtonIndent,
            this.toolStripSeparatorFormat,
            this.toolStripButtonLeft,
            this.toolStripButtonCenter,
            this.toolStripButtonRight,
            this.toolStripButtonFull,
            this.toolStripSeparatorAlign,
            this.toolStripButtonLine,
            this.toolStripButtonHyperlink,
            this.toolStripButtonPicture,
            this.toolStripButtonSourceCode,
            this.toolStripSplitButtonPreview});
            this.toolStripToolBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripToolBar.Location = new System.Drawing.Point(3, 3);
            this.toolStripToolBar.Name = "toolStripToolBar";
            this.toolStripToolBar.Size = new System.Drawing.Size(558, 25);
            this.toolStripToolBar.TabIndex = 1;
            this.toolStripToolBar.Text = "Tool Bar";
            // 
            // toolStripComboBoxName
            // 
            this.toolStripComboBoxName.MaxDropDownItems = 30;
            this.toolStripComboBoxName.Name = "toolStripComboBoxName";
            this.toolStripComboBoxName.Size = new System.Drawing.Size(100, 25);
            this.toolStripComboBoxName.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxName_SelectedIndexChanged);
            // 
            // toolStripComboBoxSize
            // 
            this.toolStripComboBoxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxSize.Name = "toolStripComboBoxSize";
            this.toolStripComboBoxSize.Size = new System.Drawing.Size(40, 25);
            this.toolStripComboBoxSize.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxSize_SelectedIndexChanged);
            // 
            // toolStripButtonBold
            // 
            this.toolStripButtonBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBold.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBold.Image")));
            this.toolStripButtonBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBold.Name = "toolStripButtonBold";
            this.toolStripButtonBold.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonBold.Text = "加粗";
            this.toolStripButtonBold.Click += new System.EventHandler(this.toolStripButtonBold_Click);
            // 
            // toolStripButtonItalic
            // 
            this.toolStripButtonItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonItalic.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonItalic.Image")));
            this.toolStripButtonItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonItalic.Name = "toolStripButtonItalic";
            this.toolStripButtonItalic.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonItalic.Text = "斜体";
            this.toolStripButtonItalic.Click += new System.EventHandler(this.toolStripButtonItalic_Click);
            // 
            // toolStripButtonUnderline
            // 
            this.toolStripButtonUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUnderline.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUnderline.Image")));
            this.toolStripButtonUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUnderline.Name = "toolStripButtonUnderline";
            this.toolStripButtonUnderline.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUnderline.Text = "下划线";
            this.toolStripButtonUnderline.Click += new System.EventHandler(this.toolStripButtonUnderline_Click);
            // 
            // toolStripButtonColor
            // 
            this.toolStripButtonColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonColor.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonColor.Image")));
            this.toolStripButtonColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonColor.Name = "toolStripButtonColor";
            this.toolStripButtonColor.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonColor.Text = "字体颜色";
            this.toolStripButtonColor.Click += new System.EventHandler(this.toolStripButtonColor_Click);
            // 
            // toolStripSeparatorFont
            // 
            this.toolStripSeparatorFont.Name = "toolStripSeparatorFont";
            this.toolStripSeparatorFont.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonNumbers
            // 
            this.toolStripButtonNumbers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNumbers.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNumbers.Image")));
            this.toolStripButtonNumbers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNumbers.Name = "toolStripButtonNumbers";
            this.toolStripButtonNumbers.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNumbers.Text = "数字编号";
            this.toolStripButtonNumbers.Click += new System.EventHandler(this.toolStripButtonNumbers_Click);
            // 
            // toolStripButtonBullets
            // 
            this.toolStripButtonBullets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBullets.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBullets.Image")));
            this.toolStripButtonBullets.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBullets.Name = "toolStripButtonBullets";
            this.toolStripButtonBullets.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonBullets.Text = "项目符号";
            this.toolStripButtonBullets.Click += new System.EventHandler(this.toolStripButtonBullets_Click);
            // 
            // toolStripButtonOutdent
            // 
            this.toolStripButtonOutdent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOutdent.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOutdent.Image")));
            this.toolStripButtonOutdent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOutdent.Name = "toolStripButtonOutdent";
            this.toolStripButtonOutdent.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOutdent.Text = "减少缩进量";
            this.toolStripButtonOutdent.Click += new System.EventHandler(this.toolStripButtonOutdent_Click);
            // 
            // toolStripButtonIndent
            // 
            this.toolStripButtonIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonIndent.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonIndent.Image")));
            this.toolStripButtonIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonIndent.Name = "toolStripButtonIndent";
            this.toolStripButtonIndent.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonIndent.Text = "增加缩进量";
            this.toolStripButtonIndent.Click += new System.EventHandler(this.toolStripButtonIndent_Click);
            // 
            // toolStripSeparatorFormat
            // 
            this.toolStripSeparatorFormat.Name = "toolStripSeparatorFormat";
            this.toolStripSeparatorFormat.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonLeft
            // 
            this.toolStripButtonLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLeft.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLeft.Image")));
            this.toolStripButtonLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLeft.Name = "toolStripButtonLeft";
            this.toolStripButtonLeft.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLeft.Text = "左对齐";
            this.toolStripButtonLeft.Click += new System.EventHandler(this.toolStripButtonLeft_Click);
            // 
            // toolStripButtonCenter
            // 
            this.toolStripButtonCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCenter.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCenter.Image")));
            this.toolStripButtonCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCenter.Name = "toolStripButtonCenter";
            this.toolStripButtonCenter.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCenter.Text = "居中";
            this.toolStripButtonCenter.Click += new System.EventHandler(this.toolStripButtonCenter_Click);
            // 
            // toolStripButtonRight
            // 
            this.toolStripButtonRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRight.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRight.Image")));
            this.toolStripButtonRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRight.Name = "toolStripButtonRight";
            this.toolStripButtonRight.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRight.Text = "右对齐";
            this.toolStripButtonRight.Click += new System.EventHandler(this.toolStripButtonRight_Click);
            // 
            // toolStripButtonFull
            // 
            this.toolStripButtonFull.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFull.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFull.Image")));
            this.toolStripButtonFull.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFull.Name = "toolStripButtonFull";
            this.toolStripButtonFull.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonFull.Text = "两边对其";
            this.toolStripButtonFull.Click += new System.EventHandler(this.toolStripButtonFull_Click);
            // 
            // toolStripSeparatorAlign
            // 
            this.toolStripSeparatorAlign.Name = "toolStripSeparatorAlign";
            this.toolStripSeparatorAlign.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonLine
            // 
            this.toolStripButtonLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLine.Image")));
            this.toolStripButtonLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLine.Name = "toolStripButtonLine";
            this.toolStripButtonLine.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLine.Text = "添加水平线";
            this.toolStripButtonLine.Click += new System.EventHandler(this.toolStripButtonLine_Click);
            // 
            // toolStripButtonHyperlink
            // 
            this.toolStripButtonHyperlink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHyperlink.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHyperlink.Image")));
            this.toolStripButtonHyperlink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHyperlink.Name = "toolStripButtonHyperlink";
            this.toolStripButtonHyperlink.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonHyperlink.Text = "创建链接";
            this.toolStripButtonHyperlink.Click += new System.EventHandler(this.toolStripButtonHyperlink_Click);
            // 
            // toolStripButtonPicture
            // 
            this.toolStripButtonPicture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPicture.Image = global::TX.Framework.WindowUI.Properties.Resources.image;
            this.toolStripButtonPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPicture.Name = "toolStripButtonPicture";
            this.toolStripButtonPicture.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPicture.Text = "插入图片";
            this.toolStripButtonPicture.Click += new System.EventHandler(this.toolStripButtonPicture_Click);
            // 
            // toolStripButtonSourceCode
            // 
            this.toolStripButtonSourceCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSourceCode.Image = global::TX.Framework.WindowUI.Properties.Resources.code;
            this.toolStripButtonSourceCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSourceCode.Name = "toolStripButtonSourceCode";
            this.toolStripButtonSourceCode.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSourceCode.Text = "源码(可以编辑)";
            this.toolStripButtonSourceCode.Click += new System.EventHandler(this.toolStripButtonSourceCode_Click);
            // 
            // toolStripSplitButtonPreview
            // 
            this.toolStripSplitButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonPreview.Image = global::TX.Framework.WindowUI.Properties.Resources.preview;
            this.toolStripSplitButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonPreview.Name = "toolStripSplitButtonPreview";
            this.toolStripSplitButtonPreview.Size = new System.Drawing.Size(23, 22);
            this.toolStripSplitButtonPreview.Text = "效果预览";
            this.toolStripSplitButtonPreview.Click += new System.EventHandler(this.toolStripSplitButtonPreview_Click);
            // 
            // TXHtmlEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.webBrowserBody);
            this.Controls.Add(this.toolStripToolBar);
            this.Name = "TXHtmlEditor";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(564, 146);
            this.toolStripToolBar.ResumeLayout(false);
            this.toolStripToolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripToolBar;
        private System.Windows.Forms.WebBrowser webBrowserBody;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxName;
        private ToolStripComboBoxEx toolStripComboBoxSize;
        private System.Windows.Forms.ToolStripButton toolStripButtonBold;
        private System.Windows.Forms.ToolStripButton toolStripButtonItalic;
        private System.Windows.Forms.ToolStripButton toolStripButtonUnderline;
        private System.Windows.Forms.ToolStripButton toolStripButtonColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFont;
        private System.Windows.Forms.ToolStripButton toolStripButtonNumbers;
        private System.Windows.Forms.ToolStripButton toolStripButtonBullets;
        private System.Windows.Forms.ToolStripButton toolStripButtonOutdent;
        private System.Windows.Forms.ToolStripButton toolStripButtonIndent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFormat;
        private System.Windows.Forms.ToolStripButton toolStripButtonLeft;
        private System.Windows.Forms.ToolStripButton toolStripButtonCenter;
        private System.Windows.Forms.ToolStripButton toolStripButtonRight;
        private System.Windows.Forms.ToolStripButton toolStripButtonFull;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorAlign;
        private System.Windows.Forms.ToolStripButton toolStripButtonLine;
        private System.Windows.Forms.ToolStripButton toolStripButtonHyperlink;
        private System.Windows.Forms.ToolStripButton toolStripButtonPicture;
        private System.Windows.Forms.ToolStripButton toolStripButtonSourceCode;
        private System.Windows.Forms.ToolStripButton toolStripSplitButtonPreview;
    }
}
