using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TX.Framework.WindowUI.Forms;

namespace TX.Framework.WindowUI.Controls
{
    /// <summary>
    /// 分页控件
    /// </summary>
    /// User:Ryan  CreateTime:2011-08-19 16:45.
    [ToolboxBitmap( typeof( UserControl ) )]
    public class TXPager : UserControl
    {
        #region fileds

        private double _ItemCount = 0;

        private int _PageIndex = 1;
        private System.Windows.Forms.Panel panelCheckItem;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbFirst;
        private ToolStripButton tsbPrevious;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton tsbNext;
        private ToolStripButton tsbLast;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel toolStripLabel2;
        private ToolStripTextBox tstPageIndex;
        private ToolStripLabel toolStripLabel3;
        private ToolStripButton tsbGoto;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripLabel tsbPageInfo;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripDropDownButton tsbPageSize;
        private ToolStripMenuItem tsbPageSize10;
        private ToolStripMenuItem tsbPageSize20;
        private ToolStripMenuItem tsbPageSize50;
        private ToolStripMenuItem tsbPageSize80;
        private ToolStripMenuItem tsbPageSize150;
        private ToolStripMenuItem tsbPageSize250;
        private ToolStripSeparator toolStripSeparator5;
        public ToolStripButton tsbSearch;

        private int _PageSize = 20;

        private List<int> _PageSizes;

        #endregion

        #region Initializes

        public TXPager()
            : base()
        {
            this.InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.BackColor = SkinManager.CurrentSkin.BaseColor;
            int[ ] sizes = new int[ ] { 10, 20, 50, 80, 150, 250 };
            this._PageSizes = new List<int>();
            this._PageSizes.AddRange( sizes );
        }

        #endregion

        #region Properties

        [Description( "每页记录数" )]
        public int PageSize
        {
            get
            {
                return this._PageSize;
            }
            set
            {
                this._PageSize = value;
                this.AddPageSize( value );
            }
        }

        /// <summary>
        /// 获取页数
        /// </summary>
        [Browsable( false )]
        public int Pages
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取当前页码
        /// </summary>
        [Browsable( false )]
        public int PageIndex
        {
            get
            {
                return this._PageIndex;
            }
            private set
            {
                this._PageIndex = value;
                this.tstPageIndex.Text = value.ToString();
                this.RefreshPagerControlInfo();
            }

        }

        /// <summary>
        /// 获取或设置需要分页的元素的总个数
        /// </summary>
        [Browsable( false )]
        public double Total
        {
            get
            {
                return _ItemCount;
            }
            set
            {
                _ItemCount = value;
                this.UpdatePageInfo();
                this.RefreshPagerControlInfo();
            }
        }

        #endregion

        #region Event

        /// <summary>
        /// 当分页时发生
        /// </summary>
        /// User:Ryan  CreateTime:2011-08-10 14:20.
        [Category( "TXEvents" )]
        [Description( "当分页控件查询数据时发生" )]
        public event EventHandler<PagerEventArgs> OnPaging;

        #endregion

        #region private methods - pageing

        /// <summary>
        /// Handles the Click event of the tsbPageSize control.
        /// </summary>
        /// <param name="sender">(控件对象).The source of the event.</param>
        /// <param name="e">(事件数据).The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// User:Ryan  CreateTime:2011-08-10 14:19.
        private void tsbPageSize_Click( object sender, EventArgs e )
        {
            ToolStripMenuItem tsm = sender as ToolStripMenuItem;
            int pageSize = Convert.ToInt32( tsm.Tag );
            if( pageSize != this.PageSize )
            {
                this.SetPageSize( pageSize );
                this.UpdatePageInfo();
                this.RefreshPagerControlInfo();
                this.PageIndex = 1;
                this.Paging( sender );
            }
        }

        public void OnSearchClick( object sender, EventArgs e )
        {
            this.PageIndex = 1;
            this.Paging( sender );
        }

        private void tsbFirst_Click( object sender, EventArgs e )
        {
            this.PageIndex = 1;
            this.Paging( sender );
        }

        private void tsbPrevious_Click( object sender, EventArgs e )
        {
            this.PageIndex--;
            this.Paging( sender );
        }

        private void tsbNext_Click( object sender, EventArgs e )
        {
            this.PageIndex++;
            this.Paging( sender );
        }

        private void tsbLast_Click( object sender, EventArgs e )
        {
            this.PageIndex = this.Pages;
            this.Paging( sender );
        }

        private void tsbGoto_Click( object sender, EventArgs e )
        {
            int index;
            if( int.TryParse( tstPageIndex.Text.Trim(), out index ) && index >= 1 && index <= this.Pages )
            {
                if( index == this.PageIndex )
                {
                    return;
                }
                else
                {
                    this.PageIndex = index;
                    this.Paging( sender );
                }
            }
            else
            {
                TXMessageBoxExtensions.Warning( "请输入有效的页数！" );
                tstPageIndex.Focus();
                tstPageIndex.SelectAll();
            }
        }

        #endregion

        #region private methods

        private void Paging( object sender )
        {
            if( OnPaging != null )
            {
                PagerEventArgs e = new PagerEventArgs( this.PageIndex, this.PageSize );
                this.OnPaging( sender, e );
            }
            else
            {
                TXMessageBoxExtensions.Error( "这个技术人员很懒啊，还没实现分页查询功能的！" );
            }
        }



        /// <summary>
        /// 添加分页选项
        /// </summary>
        /// User:Ryan  CreateTime:2011-08-10 15:05.
        private void AddPageSize( int pageSize )
        {
            int index = this._PageSizes.BinarySearch( pageSize );
            if( index < 0 )
            {
                ToolStripMenuItem tsm = new ToolStripMenuItem();
                tsm.Text = string.Format( "每页{0}条", pageSize );
                tsm.Tag = pageSize;
                tsm.Click += new EventHandler( tsbPageSize_Click );
                tsbPageSize.DropDownItems.Insert( -index - 1, tsm );
                this._PageSizes.Add( pageSize );
                this.SetPageSize( pageSize );
            }
            else
            {
                this.SetPageSize( pageSize );
            }
        }

        /// <summary>
        /// 设置当前分页大小
        /// </summary>
        /// User:Ryan  CreateTime:2011-08-10 15:05.
        private void SetPageSize( int pageSize )
        {
            foreach( ToolStripMenuItem tm in tsbPageSize.DropDownItems )
            {
                if( tm.Tag.ToString() == pageSize.ToString() )
                {
                    tm.Checked = true;
                }
                else
                {
                    tm.Checked = false;
                }
            }

            this._PageSize = pageSize;
            this.tsbPageSize.Text = string.Format( "每页{0}条", pageSize );
        }

        /// <summary>
        /// 刷新分页导航控件的控制信息
        /// </summary>
        /// User:Ryan  CreateTime:2011-08-10 15:05.
        private void RefreshPagerControlInfo()
        {
            if( this.DesignMode )
                return;
            tsbFirst.Enabled = false;
            tsbPrevious.Enabled = false;
            tsbNext.Enabled = false;
            tsbLast.Enabled = false;
            if( PageIndex != 1 )
            {
                tsbFirst.Enabled = true;
                tsbPrevious.Enabled = true;
            }
            if( PageIndex != Pages && Pages > 1 )
            {
                tsbNext.Enabled = true;
                tsbLast.Enabled = true;
            }
        }

        /// <summary>
        /// 更新分页数据（总数变更，当前页变更，分页大小变更）
        /// </summary>
        /// User:Ryan  CreateTime:2011-08-10 15:05.
        private void UpdatePageInfo()
        {
            double divide = Total / PageSize;
            double ceiled = Math.Ceiling( divide );
            Pages = Convert.ToInt32( ceiled );
            this.tsbPageInfo.Text = string.Format( "共{0}页({1}条记录)", this.Pages, this.Total );
        }

        #endregion

        #region InitializeComponent

        private void InitializeComponent()
        {
            this.panelCheckItem = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbFirst = new System.Windows.Forms.ToolStripButton();
            this.tsbPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNext = new System.Windows.Forms.ToolStripButton();
            this.tsbLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tstPageIndex = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsbGoto = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPageInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPageSize = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbPageSize10 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbPageSize20 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbPageSize50 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbPageSize80 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbPageSize150 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbPageSize250 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCheckItem
            // 
            this.panelCheckItem.BackColor = System.Drawing.Color.Transparent;
            this.panelCheckItem.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCheckItem.Location = new System.Drawing.Point( 0, 0 );
            this.panelCheckItem.Name = "panelCheckItem";
            this.panelCheckItem.Size = new System.Drawing.Size( 14, 25 );
            this.panelCheckItem.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[ ] {
            this.toolStripLabel2,
            this.tstPageIndex,
            this.toolStripLabel3,
            this.tsbGoto,
            this.toolStripSeparator3,
            this.tsbFirst,
            this.tsbPrevious,
            this.toolStripSeparator4,
            this.tsbNext,
            this.tsbLast,
            this.toolStripSeparator1,
            this.tsbPageInfo,
            this.toolStripSeparator2,
            this.tsbPageSize,
            this.toolStripSeparator5,
            this.tsbSearch} );
            this.toolStrip1.Location = new System.Drawing.Point( 14, 0 );
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size( 683, 25 );
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "分页控件";
            // 
            // tsbFirst
            // 
            this.tsbFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFirst.Image = global::TX.Framework.WindowUI.Properties.Resources.resultset_first;
            this.tsbFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFirst.Name = "tsbFirst";
            this.tsbFirst.Size = new System.Drawing.Size( 23, 22 );
            this.tsbFirst.Tag = "Vicky";
            this.tsbFirst.Text = "第一页(&F)";
            this.tsbFirst.Click += new System.EventHandler( this.tsbFirst_Click );
            // 
            // tsbPrevious
            // 
            this.tsbPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrevious.Image = global::TX.Framework.WindowUI.Properties.Resources.resultset_previous;
            this.tsbPrevious.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrevious.Name = "tsbPrevious";
            this.tsbPrevious.Size = new System.Drawing.Size( 23, 22 );
            this.tsbPrevious.Tag = "Vicky";
            this.tsbPrevious.Text = "上一页(P)";
            this.tsbPrevious.Click += new System.EventHandler( this.tsbPrevious_Click );
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size( 6, 25 );
            // 
            // tsbNext
            // 
            this.tsbNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNext.Image = global::TX.Framework.WindowUI.Properties.Resources.resultset_next;
            this.tsbNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNext.Name = "tsbNext";
            this.tsbNext.Size = new System.Drawing.Size( 23, 22 );
            this.tsbNext.Tag = "Vicky";
            this.tsbNext.Text = "下一页(&N)";
            this.tsbNext.Click += new System.EventHandler( this.tsbNext_Click );
            // 
            // tsbLast
            // 
            this.tsbLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLast.Image = global::TX.Framework.WindowUI.Properties.Resources.resultset_last;
            this.tsbLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLast.Name = "tsbLast";
            this.tsbLast.Size = new System.Drawing.Size( 23, 22 );
            this.tsbLast.Tag = "Vicky";
            this.tsbLast.Text = "最后一页(&L)";
            this.tsbLast.Click += new System.EventHandler( this.tsbLast_Click );
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size( 6, 25 );
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size( 17, 22 );
            this.toolStripLabel2.Text = "第";
            // 
            // tstPageIndex
            // 
            this.tstPageIndex.BackColor = System.Drawing.Color.Ivory;
            this.tstPageIndex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tstPageIndex.Name = "tstPageIndex";
            this.tstPageIndex.Size = new System.Drawing.Size( 30, 25 );
            this.tstPageIndex.Text = "1";
            this.tstPageIndex.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size( 17, 22 );
            this.toolStripLabel3.Text = "页";
            // 
            // tsbGoto
            // 
            this.tsbGoto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGoto.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tsbGoto.Image = global::TX.Framework.WindowUI.Properties.Resources._goto;
            this.tsbGoto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGoto.Name = "tsbGoto";
            this.tsbGoto.Size = new System.Drawing.Size( 23, 22 );
            this.tsbGoto.Text = "转到(&G)";
            this.tsbGoto.Click += new System.EventHandler( this.tsbGoto_Click );
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size( 6, 25 );
            // 
            // tsbPageInfo
            // 
            this.tsbPageInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbPageInfo.Name = "tsbPageInfo";
            this.tsbPageInfo.Size = new System.Drawing.Size( 89, 22 );
            this.tsbPageInfo.Text = "共0页(0条记录)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size( 6, 25 );
            // 
            // tsbPageSize
            // 
            this.tsbPageSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbPageSize.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[ ] {
            this.tsbPageSize10,
            this.tsbPageSize20,
            this.tsbPageSize50,
            this.tsbPageSize80,
            this.tsbPageSize150,
            this.tsbPageSize250} );
            this.tsbPageSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPageSize.Name = "tsbPageSize";
            this.tsbPageSize.Size = new System.Drawing.Size( 66, 22 );
            this.tsbPageSize.Tag = "20";
            this.tsbPageSize.Text = "每页20条";
            // 
            // tsbPageSize10
            // 
            this.tsbPageSize10.Name = "tsbPageSize10";
            this.tsbPageSize10.Size = new System.Drawing.Size( 152, 22 );
            this.tsbPageSize10.Tag = "10";
            this.tsbPageSize10.Text = "每页10条";
            this.tsbPageSize10.Click += new System.EventHandler( this.tsbPageSize_Click );
            // 
            // tsbPageSize20
            // 
            this.tsbPageSize20.Checked = true;
            this.tsbPageSize20.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbPageSize20.Name = "tsbPageSize20";
            this.tsbPageSize20.Size = new System.Drawing.Size( 152, 22 );
            this.tsbPageSize20.Tag = "20";
            this.tsbPageSize20.Text = "每页20条";
            this.tsbPageSize20.Click += new System.EventHandler( this.tsbPageSize_Click );
            // 
            // tsbPageSize50
            // 
            this.tsbPageSize50.Name = "tsbPageSize50";
            this.tsbPageSize50.Size = new System.Drawing.Size( 152, 22 );
            this.tsbPageSize50.Tag = "50";
            this.tsbPageSize50.Text = "每页50条";
            this.tsbPageSize50.Click += new System.EventHandler( this.tsbPageSize_Click );
            // 
            // tsbPageSize80
            // 
            this.tsbPageSize80.Name = "tsbPageSize80";
            this.tsbPageSize80.Size = new System.Drawing.Size( 152, 22 );
            this.tsbPageSize80.Tag = "80";
            this.tsbPageSize80.Text = "每页80条";
            this.tsbPageSize80.Click += new System.EventHandler( this.tsbPageSize_Click );
            // 
            // tsbPageSize150
            // 
            this.tsbPageSize150.Name = "tsbPageSize150";
            this.tsbPageSize150.Size = new System.Drawing.Size( 152, 22 );
            this.tsbPageSize150.Tag = "150";
            this.tsbPageSize150.Text = "每页150条";
            this.tsbPageSize150.Click += new System.EventHandler( this.tsbPageSize_Click );
            // 
            // tsbPageSize250
            // 
            this.tsbPageSize250.Name = "tsbPageSize250";
            this.tsbPageSize250.Size = new System.Drawing.Size( 152, 22 );
            this.tsbPageSize250.Tag = "250";
            this.tsbPageSize250.Text = "每页250条";
            this.tsbPageSize250.Click += new System.EventHandler( this.tsbPageSize_Click );
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size( 6, 25 );
            // 
            // tsbSearch
            // 
            this.tsbSearch.Image = global::TX.Framework.WindowUI.Properties.Resources.search;
            this.tsbSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSearch.Name = "tsbSearch";
            this.tsbSearch.Size = new System.Drawing.Size( 67, 22 );
            this.tsbSearch.Text = "查询(&S)";
            this.tsbSearch.Click += new System.EventHandler( this.OnSearchClick );
            // 
            // TXPager
            // 
            this.Controls.Add( this.toolStrip1 );
            this.Controls.Add( this.panelCheckItem );
            this.Name = "TXPager";
            this.Size = new System.Drawing.Size( 697, 25 );
            this.toolStrip1.ResumeLayout( false );
            this.toolStrip1.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion
    }
}
