using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TX.Framework.WindowUI.Forms;

namespace TX.Framework.WindowUI.Controls
{
    /// <summary>
    /// 常用操作工具条（主要针对基本信息的操作处理）
    /// </summary>
    /// User:Ryan  CreateTime:2011-08-19 11:31.
    [ToolboxBitmap(typeof(UserControl))]
    [DesignTimeVisibleAttribute(false)]
    public partial class TXToolBar : UserControl
    {
        #region fileds

        private ToolStripItemDisplayStyle _DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

        private bool _AddVisible = true;

        private bool _CheckItemVisible = true;
        private bool _DetailVisible = true;
        private bool _EditVisible = true;
        private bool _DeleteVisible = true;
        private bool _ExportVisible = true;
        private bool _HelpVisible = true;

        #endregion

        public TXToolBar()
        {
            InitializeComponent();
            this.BackColor = SkinManager.CurrentSkin.BaseColor;
        }

        #region Events

        [Category("TXEvents")]
        [Description("点击添加时发生")]
        public event EventHandler Add;

        [Category("TXEvents")]
        [Description("修改")]
        public event EventHandler Edit;

        [Category("TXEvents")]
        [Description("删除")]
        public event EventHandler Delete;

        [Category("TXEvents")]
        [Description("刷新")]
        public new event EventHandler Refresh;

        [Category("TXEvents")]
        [Description("导出")]
        public event EventHandler Export;

        [Category("TXEvents")]
        [Description("帮助")]
        public event EventHandler Help;

        [Category("TXEvents")]
        [Description("全选")]
        public event EventHandler CheckAll;

        [Category("TXEvents")]
        [Description("反选")]
        public event EventHandler CheckInvert;

        [Category("TXEvents")]
        [Description("查看详情")]
        public event EventHandler Detail;

        #endregion

        #region Properties

        [Category("TXProperties")]
        [Description("是否显示全选、反选项")]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool CheckItemVisible
        {
            get { return this._CheckItemVisible; }
            set
            {
                this._CheckItemVisible = value;
                this.panelCheckItem.Visible = value;
            }
        }

        [Category("TXProperties")]
        [Description("是否显示添加")]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AddVisible
        {
            get { return this._AddVisible; }
            set
            {
                this._AddVisible = value;
                this.tsbAdd.Visible = value;
            }
        }

        [Category("TXProperties")]
        [Description("是否显示修改")]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool EditVisible
        {
            get { return this._EditVisible; }
            set
            {
                this._EditVisible = value;
                this.tsbEdit.Visible = value;
            }
        }

        [Category("TXProperties")]
        [Description("是否显示查看详情")]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool DetailVisible
        {
            get { return this._DetailVisible; }
            set
            {
                this._DetailVisible = value;
                this.tsbDetails.Visible = value;
            }
        }

        [Category("TXProperties")]
        [Description("是否显示删除")]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool DeleteVisible
        {
            get { return this._DeleteVisible; }
            set
            {
                this._DeleteVisible = value;
                this.tsbDelete.Visible = value;
            }
        }

        [Category("TXProperties")]
        [Description("是否显示导出")]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ExportVisible
        {
            get { return this._ExportVisible; }
            set
            {
                this._ExportVisible = value;
                this.tsbExport.Visible = value;
            }
        }

        [Category("TXProperties")]
        [Description("是否显示帮助")]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool HelpVisible
        {
            get { return this._HelpVisible; }
            set
            {
                this._HelpVisible = value;
                this.tsbHelp.Visible = value;
            }
        }

        /// <summary>
        /// 获取或者设置按钮的显示方式（图标和文本的显示）
        /// </summary>
        /// <value>The ToolStripItemDisplayStyle.</value>
        /// User:Ryan  CreateTime:2011-08-19 14:18.
        [Category("TXProperties")]
        [Description("控制按钮的显示样式")]
        [DefaultValue(typeof(ToolStripItemDisplayStyle), "ImageAndText")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ToolStripItemDisplayStyle DisPlayStyle
        {
            get { return this._DisplayStyle; }
            set
            {
                if (this._DisplayStyle != value)
                {
                    this._DisplayStyle = value;
                    foreach (ToolStripItem item in this.txToolStrip1.Items)
                    {
                        item.DisplayStyle = value;
                    }
                }
            }
        }

        #endregion

        #region private methods

        private void linkAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.CheckAll != null)
            {
                this.CheckAll(sender, e);
            }
            else
            {
                TXMessageBoxExtensions.Error("非常遗憾，该功能还未实现！开发人员和凤姐一起私奔了！");
            }
        }

        private void linkInvert_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.CheckInvert != null)
            {
                this.CheckInvert(sender, e);
            }
            else
            {
                TXMessageBoxExtensions.Error("非常遗憾，该功能还未实现！开发人员和凤姐一起私奔了！");
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            if (this.Add != null)
            {
                this.Add(sender, e);
            }
            else
            {
                TXMessageBoxExtensions.Error("Error Information", "Oh!,I'm really awfully sorry,\n\nThe functions cannot be used!");
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (this.Edit != null)
            {
                this.Edit(sender, e);
            }
            else
            {
                TXMessageBoxExtensions.Error("非常遗憾，该功能还未实现！开发人员和凤姐一起私奔了！");
            }
        }

        private void tsbDetails_Click(object sender, EventArgs e)
        {
            if (this.Detail != null)
            {
                this.Detail(sender,e);
            }
            else
            {
                TXMessageBoxExtensions.Error("非常遗憾，该功能还未实现！开发人员和凤姐一起私奔了！");
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (this.Delete != null)
            {
                this.Delete(sender, e);
            }
            else
            {
                TXMessageBoxExtensions.Error("非常遗憾，该功能还未实现！开发人员和凤姐一起私奔了！");
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            if (this.Refresh != null)
            {
                this.Refresh(sender, e);
            }
            else
            {
                TXMessageBoxExtensions.Error("非常遗憾，该功能还未实现！开发人员和凤姐一起私奔了！");
            }
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            if (this.Export != null)
            {
                this.Export(sender, e);
            }
            else
            {
                TXMessageBoxExtensions.Error("非常遗憾，该功能还未实现！开发人员和凤姐一起私奔了！");
            }
        }

        private void tsbHelp_Click(object sender, EventArgs e)
        {
            if (this.Help != null)
            {
                this.Help(sender, e);
            }
            else
            {
                TXMessageBoxExtensions.Error("非常遗憾，该功能还未实现！开发人员和凤姐一起私奔了！");
            }
        }

        #endregion

        #region 添加按钮
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="button"></param>
        public void AddButton( ToolStripItem button )
        {
            this.txToolStrip1.Items.Add( button );
            this.txToolStrip1.Width += button.Width;
        } 
        #endregion
    }
}
