using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls
{
    [ToolboxBitmap(typeof(TableLayoutPanel))]
    public class TXTableLayoutPanel : TableLayoutPanel
    {
        private ScrollBars _ScrollBars = ScrollBars.Vertical;

        private Color _BorderColor = SkinManager.CurrentSkin.BorderColor;

        public TXTableLayoutPanel()
            : base()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();
            this.BorderWidth = 0;
            ControlHelper.BindMouseMoveEvent(this);
        }

        [Category("TXProperties")]
        [Description("滚动条的显示方式")]
        [DefaultValue(typeof(ScrollBars), "Vertical")]
        public ScrollBars ScrollBars
        {
            get { return this._ScrollBars; }
            set
            {
                if (value != this._ScrollBars)
                {
                    this._ScrollBars = value;
                    base.Invalidate();
                }
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
        [Description("边框的宽带")]
        [DefaultValue(0)]
        public int BorderWidth { get; set; }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case (int)WindowMessages.WM_PAINT:
                    if (this.BorderWidth > 0)
                    {
                        using (Graphics g = Graphics.FromHwnd(m.HWnd))
                        {
                            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                            GDIHelper.DrawPathBorder(g, new RoundRectangle(rect, 0), this._BorderColor, this.BorderWidth);
                        }
                    }
                    break;
                case (int)WindowMessages.WM_VSCROLL:
                case (int)WindowMessages.WM_HSCROLL:
                    this.Invalidate(true);
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            switch (this._ScrollBars)
            {
                case ScrollBars.Horizontal:
                    Win32.ShowScrollBar(base.Handle, 1, 0);
                    break;
                case ScrollBars.Vertical:
                    Win32.ShowScrollBar(base.Handle, 0, 0);
                    break;
                case ScrollBars.None:
                    Win32.ShowScrollBar(base.Handle, 1, 0);
                    Win32.ShowScrollBar(base.Handle, 0, 0);
                    break;
            }
        }
    }
}
