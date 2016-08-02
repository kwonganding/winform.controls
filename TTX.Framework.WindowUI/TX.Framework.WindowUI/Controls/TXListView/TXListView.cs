using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Collections;

namespace TX.Framework.WindowUI.Controls
{
    /// <summary>
    /// 扩展的ListView控件
    /// </summary>
    /// User:Ryan  CreateTime:2011-10-19 9:14.
    [ToolboxBitmap(typeof(ListView))]
    public class TXListView : ListView
    {
        #region fileds

        private Color _RowBackColor1 = Color.FromArgb(255, 255, 254);
        private Color _RowBackColor2 = Color.FromArgb(243, 246, 253);
        private Color _SelectedBeginColor = Color.FromArgb(211, 238, 255);
        private Color _SelectedEndColor = Color.FromArgb(175, 225, 253);
        private Color _HeaderBeginColor = Color.FromArgb(253, 253, 253);
        private Color _HeaderEndColor = Color.FromArgb(235, 235, 235);
        private Color _BorderColor = SkinManager.CurrentSkin.BorderColor;
        private SizeType _ColumnsWidthType = SizeType.Absolute;

        private HeaderNativeWindow _headerNativeWindow;

        private Font _Font;

        private Size _CheckBoxSize = new Size(12, 12);

        private ArrayList _EmbeddedItems = new ArrayList();

        private int _cpadding = 3;

        private int _HeaderHeight;

        #endregion

        #region Initializes

        /// <summary>
        /// (构造函数).Initializes a new instance of the <see cref="TXListView"/> class.
        /// </summary>
        /// User:Ryan  CreateTime:2011-10-19 15:28.
        public TXListView()
            : base()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();
            this.SuspendLayout();
            this.OwnerDraw = true;
            this.FullRowSelect = true;
            this.View = View.Details;
            base.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.White;
            this.Font = new Font("宋体", 9.6f);
            this.ResetColoumsWidth();
            this._HeaderHeight = 0;
        }

        #endregion

        #region properties

        [Category("TXProperties")]
        public override Font Font
        {
            get
            {
                return this._Font;
            }
            set
            {
                this._Font = value;
                base.Font = new Font(value.FontFamily, value.Size + 4);
                base.Invalidate();
            }
        }

        /// <summary>
        /// 复选框的大小，设置显示复选框才会有效
        /// </summary>
        /// <value>The size of the check box.</value>
        /// User:Ryan  CreateTime:2011-08-19 11:09.
        [Category("TXProperties")]
        [Description("复选框的大小，设置显示复选框才会有效")]
        [DefaultValue(typeof(Size), "12,12")]
        public Size CheckBoxSize
        {
            get { return this._CheckBoxSize; }
            set { this._CheckBoxSize = value; }
        }

        [Category("TXProperties")]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                base.Invalidate();
            }
        }

        [Category("TXProperties")]
        [Description("行交替颜色1")]
        public Color RowBackColor1
        {
            get { return _RowBackColor1; }
            set
            {
                _RowBackColor1 = value;
                base.Invalidate();
            }
        }

        [Category("TXProperties")]
        [Description("行交替颜色2")]
        public Color RowBackColor2
        {
            get { return _RowBackColor2; }
            set
            {
                _RowBackColor2 = value;
                base.Invalidate();
            }
        }

        [Category("TXProperties")]
        [Description("标题颜色")]
        public Color HeaderBeginColor
        {
            get { return _HeaderBeginColor; }
            set
            {
                this._HeaderBeginColor = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("标题颜色")]
        public Color HeaderEndColor
        {
            get { return _HeaderEndColor; }
            set
            {
                this._HeaderEndColor = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("边框颜色")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                this._BorderColor = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("选择状态颜色")]
        public Color SelectedBeginColor
        {
            get { return _SelectedBeginColor; }
            set
            {
                this._SelectedBeginColor = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("选择状态颜色")]
        public Color SelectedEndColor
        {
            get { return _SelectedEndColor; }
            set
            {
                this._SelectedEndColor = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("列的宽度类型")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(SizeType), "Absolute")]
        public SizeType ColumnsWidthType
        {
            get { return _ColumnsWidthType; }
            set
            {
                this._ColumnsWidthType = value;
                this.ResetColoumsWidth();
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(BorderStyle), "FixedSingle")]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set
            {
                base.BorderStyle = value;
                this.Invalidate();
            }
        }

        private IntPtr HeaderWnd
        {
            get { return new IntPtr(Win32.SendMessage(base.Handle, (int)ListViewMessages.GETHEADER, 0, 0)); }
        }

        private int ColumnCount
        {
            get { return Win32.SendMessage(HeaderWnd, (int)HeaderControlMessages.GETITEMCOUNT, 0, 0); }
        }

        #endregion

        #region Override methods

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (_headerNativeWindow == null)
            {
                if (HeaderWnd != IntPtr.Zero)
                {
                    _headerNativeWindow = new HeaderNativeWindow(this);
                }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (_headerNativeWindow != null)
            {
                _headerNativeWindow.Dispose();
                _headerNativeWindow = null;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case (int)WindowMessages.WM_PAINT:
                    ////绑定控件
                    this.BindEmbeddedItem();
                    break;
                case (int)WindowMessages.WM_NCPAINT:
                    this.NcPaint(ref m);
                    break;
                case (int)WindowMessages.WM_WINDOWPOSCHANGED:
                    IntPtr result = m.Result;
                    this.NcPaint(ref m);
                    m.Result = result;
                    break;
            }
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            base.OnDrawColumnHeader(e);
            Graphics g = e.Graphics;
            GDIHelper.InitializeGraphics(g);
            Rectangle bounds = e.Bounds;
            GDIHelper.FillPath(g, new RoundRectangle(bounds, 0), this._HeaderBeginColor, this._HeaderEndColor);
            bounds.Height--;
            if (this.BorderStyle != BorderStyle.None)
            {
                using (Pen p = new Pen(this.BorderColor))
                {
                    g.DrawLine(p, new Point(bounds.Right, bounds.Bottom), new Point(bounds.Right, bounds.Top));
                    g.DrawLine(p, new Point(bounds.Left, bounds.Bottom), new Point(bounds.Right, bounds.Bottom));
                }
            }
            else
            {
                GDIHelper.DrawPathBorder(g, new RoundRectangle(bounds, 0), this._BorderColor);
            }

            bounds.Height++;
            TextFormatFlags flags = GetFormatFlags(e.Header.TextAlign);
            Rectangle textRect = new Rectangle(
                       bounds.X + 3,
                       bounds.Y,
                       bounds.Width - 6,
                       bounds.Height); ;
            Image image = null;
            Size imgSize = new System.Drawing.Size(16, 16);
            Rectangle imageRect = Rectangle.Empty;
            if (e.Header.ImageList != null)
            {
                image = e.Header.ImageIndex == -1 ?
                    null : e.Header.ImageList.Images[e.Header.ImageIndex];
            }

            GDIHelper.DrawImageAndString(g, bounds, image, imgSize, e.Header.Text, this._Font, e.ForeColor);
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (this.View != View.Details)
            {
                e.DrawDefault = true;
            }
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            base.OnDrawSubItem(e);
            if (View != View.Details || e.ItemIndex == -1)
            {
                return;
            }

            Rectangle bounds = e.Bounds;
            ListViewItemStates itemState = e.ItemState;
            Graphics g = e.Graphics;
            GDIHelper.InitializeGraphics(g);
            Blend blen = new Blend();
            blen.Positions = new float[] { 0f, 0.4f, 0.7f, 1f };
            blen.Factors = new float[] { 0f, 0.3f, 0.8f, 0.2f };
            Color c1, c2;
            if ((itemState & ListViewItemStates.Selected) == ListViewItemStates.Selected)
            {
                c1 = this._SelectedBeginColor;
                c2 = this._SelectedEndColor;
                //使用全局皮肤色彩，注意选择文字需要反色处理
                c1 = SkinManager.CurrentSkin.HeightLightControlColor.First;
                c2 = SkinManager.CurrentSkin.HeightLightControlColor.Second;
                blen.Factors = SkinManager.CurrentSkin.HeightLightControlColor.Factors;
                blen.Positions = SkinManager.CurrentSkin.HeightLightControlColor.Positions;
                GDIHelper.FillPath(g, new RoundRectangle(bounds, 0), c1, c2, blen);
            }
            else
            {
                if (e.ColumnIndex == 0)
                {
                    bounds.Inflate(0, -1);
                }
                c1 = e.ItemIndex % 2 == 0 ? this._RowBackColor1 : this._RowBackColor2;
                c2 = c1;
                GDIHelper.FillPath(g, new RoundRectangle(bounds, 0), c1, c2, blen);
            }

            if (e.ColumnIndex == 0)
            {
                this.OnDrawFirstSubItem(e, g);
            }
            else
            {
                this.DrawNormalSubItem(e, g);
            }
        }
        #endregion

        #region Private methods

        #region DrawSubItem

        private void DrawNormalSubItem(DrawListViewSubItemEventArgs e, Graphics g)
        {
            TextFormatFlags flags = GetFormatFlags(e.Header.TextAlign);
            Rectangle rect = e.Bounds;
            rect.X += 2; rect.Width -= 4;
            Color c = (e.ItemState & ListViewItemStates.Selected) == ListViewItemStates.Selected ?
                Color.White : e.SubItem.ForeColor;
            TextRenderer.DrawText(g, e.SubItem.Text, this._Font, rect, c, flags);
        }

        protected virtual void OnDrawFirstSubItem(DrawListViewSubItemEventArgs e, Graphics g)
        {
            TextFormatFlags flags = GetFormatFlags(e.Header.TextAlign);
            Rectangle rect = e.Bounds;
            Image img = null;
            Size imgSize = Size.Empty;
            Rectangle checkBoxRect = new Rectangle(rect.X, rect.Y, 0, 0);
            Rectangle imgRect = checkBoxRect;
            Rectangle textRect = rect;
            int offset = 2;
            if (e.Item.ListView.CheckBoxes)
            {
                checkBoxRect.X += offset * 2;
                checkBoxRect.Y = rect.Top + (rect.Height - this._CheckBoxSize.Height) / 2;
                checkBoxRect.Width = this._CheckBoxSize.Width;
                checkBoxRect.Height = this._CheckBoxSize.Height;
                imgRect.X = checkBoxRect.Right;
                textRect.X = checkBoxRect.Right;
                textRect.Width -= this._CheckBoxSize.Width - offset * 2;
                GDIHelper.DrawCheckBox(g, new RoundRectangle(checkBoxRect, 1));
                if (e.Item.Checked)
                {
                    GDIHelper.DrawCheckedStateByImage(g, checkBoxRect);
                }
            }

            if (e.Item.ImageList != null && e.Item.ImageIndex >= 0)
            {
                img = e.Item.ImageList.Images[e.Item.ImageIndex];
                imgSize = e.Item.ImageList.ImageSize;
                imgRect.X += offset * 3;
                imgRect.Y = rect.Y + offset;
                int width = rect.Height - offset * 2;
                imgRect.Width = width;
                imgRect.Height = width;
                textRect.X = imgRect.Right;
                textRect.Width -= width - offset * 2;
                GDIHelper.DrawImage(g, imgRect, img, imgSize);
            }

            textRect.X += offset;
            textRect.Width -= offset * 2;
            Color c = (e.ItemState & ListViewItemStates.Selected) == ListViewItemStates.Selected ?
                Color.White : e.SubItem.ForeColor;
            TextRenderer.DrawText(g, e.SubItem.Text, this._Font, textRect, c, flags);
        }
        #endregion

        #region GetFormatFlags

        protected TextFormatFlags GetFormatFlags(HorizontalAlignment align)
        {
            TextFormatFlags flags =
                    TextFormatFlags.EndEllipsis |
                    TextFormatFlags.VerticalCenter;

            switch (align)
            {
                case HorizontalAlignment.Center:
                    flags |= TextFormatFlags.HorizontalCenter;
                    break;
                case HorizontalAlignment.Right:
                    flags |= TextFormatFlags.Right;
                    break;
                case HorizontalAlignment.Left:
                    flags |= TextFormatFlags.Left;
                    break;
            }

            return flags;
        }
        #endregion

        #region ColumnAtIndex

        private int ColumnAtIndex(int column)
        {
            HDITEM hd = new HDITEM();
            hd.mask = (int)HeaderItemFlags.ORDER;
            for (int i = 0; i < ColumnCount; i++)
            {
                if (Win32.SendMessage(HeaderWnd, (int)HeaderControlMessages.GETITEMA, column, ref hd) != IntPtr.Zero)
                {
                    return hd.iOrder;
                }
            }
            return 0;
        }
        #endregion

        private Rectangle HeaderEndRect()
        {
            RECT rect = new RECT();
            IntPtr headerWnd = HeaderWnd;
            Win32.SendMessage(
                headerWnd, (int)HeaderControlMessages.GETITEMRECT, ColumnAtIndex(ColumnCount - 1), ref rect);
            int left = rect.right;
            Win32.GetWindowRect(headerWnd, ref rect);
            Win32.OffsetRect(ref rect, -rect.left, -rect.top);
            rect.left = left;
            return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
        }

        private void ResetColoumsWidth()
        {
            if (this.Columns.Count > 0 && this._ColumnsWidthType != SizeType.Absolute)
            {
                int maxWidth = 0;
                foreach (ColumnHeader header in this.Columns)
                {
                    maxWidth += header.Width;
                }

                float rate = 1;
                foreach (ColumnHeader header in this.Columns)
                {
                    rate = Convert.ToSingle(header.Width) / Convert.ToSingle(maxWidth);
                    header.Width = Convert.ToInt32((this.Width - 20) * rate);
                }
            }
        }

        #region BindEmbeddedItem

        /// <summary>
        /// 绑定内嵌到subItem的控件，主要在onpaint事件中调用。
        /// </summary>
        /// User:Ryan  CreateTime:2011-10-19 9:15.
        private void BindEmbeddedItem()
        {
            if (this._HeaderHeight <= 0)
            {
                this._HeaderHeight = this.HeaderEndRect().Height;
            }

            Rectangle r;
            using (Graphics g = this.CreateGraphics())
            {
                foreach (EmbeddedItem item in this._EmbeddedItems)
                {
                    r = item.SubItem.Bounds;
                    ////不是第一列要特殊处理，不然会不兼容
                    if (r.Y > (this._HeaderHeight - r.Height) && r.Y > 0 && r.Y < this.ClientRectangle.Height)
                    {
                        item.EmbeddedControl.Visible = true;
                        int w = Convert.ToInt32(g.MeasureString(item.EmbeddedControl.Text, item.SubItem.Font).Width) + 2 * _cpadding;
                        if (r.X <= 10 && w >= this.Columns[0].Width)
                        {
                            w = this.Columns[0].Width - 2 * _cpadding;
                        }

                        item.EmbeddedControl.Bounds = new Rectangle(r.X + _cpadding, r.Y + _cpadding, w, r.Height - (2 * _cpadding));
                    }
                    else
                    {
                        item.EmbeddedControl.Visible = false;
                    }
                }
            }
        }
        #endregion

        #region NcPaint

        private void NcPaint(ref Message msg)
        {
            if (base.BorderStyle == BorderStyle.None)
            {
                return;
            }

            IntPtr hDC = Win32.GetWindowDC(msg.HWnd);
            if (hDC == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            Rectangle bounds = new Rectangle(0, 0, Width - 1, Height - 1);
            using (Graphics g = Graphics.FromHdc(hDC))
            {
                GDIHelper.DrawPathBorder(g, new RoundRectangle(bounds, 0), this._BorderColor);
            }
            msg.Result = IntPtr.Zero;
            Win32.ReleaseDC(msg.HWnd, hDC);
        }
        #endregion

        #endregion

        #region Public methods

        /// <summary>
        /// 绑定控件到subitem上
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="item">The item.</param>
        /// <param name="itemIndex">Index of the item.</param>
        /// User:Ryan  CreateTime:2011-10-19 11:22.
        public void AddControlToSubItem(Control control, ListViewItem.ListViewSubItem item, int itemIndex)
        {
            this.Controls.Add(control);
            EmbeddedItem ei;
            ei.EmbeddedControl = control;
            ei.SubItem = item;
            ei.ItemIndex = itemIndex;
            this._EmbeddedItems.Add(ei);
        }

        /// <summary>
        /// 清除列表中绑定的控件
        /// </summary>
        /// User:Ryan  CreateTime:2011-10-19 11:23.
        public void ClearEmbeddedItems()
        {
            foreach (EmbeddedItem item in this._EmbeddedItems)
            {
                item.EmbeddedControl.Visible = false;
                item.EmbeddedControl.Dispose();
            }

            this._EmbeddedItems.Clear();
        }

        #endregion

        #region Class HeaderNativeWindow

        internal class HeaderNativeWindow : NativeWindow, IDisposable
        {
            private TXListView _owner;

            public HeaderNativeWindow(TXListView owner)
                : base()
            {
                _owner = owner;
                base.AssignHandle(owner.HeaderWnd);
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                switch (m.Msg)
                {
                    case (int)WindowMessages.WM_PAINT:
                    case (int)WindowMessages.WM_WINDOWPOSCHANGED:
                        IntPtr hdc = Win32.GetDC(m.HWnd);
                        try
                        {
                            using (Graphics g = Graphics.FromHdc(hdc))
                            {
                                Rectangle bounds = _owner.HeaderEndRect();
                                GDIHelper.InitializeGraphics(g);
                                GDIHelper.FillPath(g, new RoundRectangle(bounds, 0), this._owner._HeaderBeginColor, this._owner._HeaderEndColor);
                                bounds.Width--; bounds.Height--;
                                if (this._owner.BorderStyle != BorderStyle.None)
                                {
                                    using (Pen p = new Pen(this._owner.BorderColor))
                                    {
                                        g.DrawLine(p, new Point(bounds.Left, bounds.Bottom), new Point(bounds.Left, bounds.Top));
                                        g.DrawLine(p, new Point(bounds.Right, bounds.Bottom), new Point(bounds.Right, bounds.Top));
                                        g.DrawLine(p, new Point(bounds.Left, bounds.Bottom), new Point(bounds.Right, bounds.Bottom));
                                    }
                                }
                                else
                                {
                                    GDIHelper.DrawPathBorder(g, new RoundRectangle(bounds, 0), this._owner._BorderColor);
                                }
                            }
                        }
                        finally
                        {
                            Win32.ReleaseDC(m.HWnd, hdc);
                        }
                        break;
                }
            }

            #region IDisposable 成员

            public void Dispose()
            {
                ReleaseHandle();
                _owner = null;
            }

            #endregion
        }

        #endregion

        #region struct of EmbeddedItem

        public struct EmbeddedItem
        {
            public ListViewItem.ListViewSubItem SubItem;
            public Control EmbeddedControl;
            public int ItemIndex;
        }

        #endregion
    }
}