using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TX.Framework.WindowUI.Controls
{
    [ToolboxBitmap(typeof(DateTimePicker))]
    public class TXDateTimePicker : DateTimePicker
    {
        #region fileds

        private int _Margin = 2;

        private int _CornerRadius = 0;

        private Color _BackColor = Color.White;

        private bool DroppedDown = false;

        private int InvalidateSince = 0;

        #endregion

        #region Initializes

        public TXDateTimePicker()
            : base()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();
            this.Size = new Size(150, 20);
            base.CalendarForeColor = Color.Blue;
            base.CalendarTrailingForeColor = Color.CadetBlue;
            base.CalendarMonthBackground = SkinManager.CurrentSkin.DefaultControlColor.First;
            base.CalendarTitleBackColor = SkinManager.CurrentSkin.CaptionColor.First;
            this.ShowCheckBox = true;
            this.Checked = true;
        }

        #endregion

        #region Properties

        [Category("TXProperties")]
        [Description("获取或者设置控件的事件日期时间值")]
        [Browsable(true)]
        public new DateTime? Value
        {
            get
            {
                if (this.Checked)
                {
                    return base.Value;
                }
                return null;
            }
            set
            {
                if (value == null || value == DateTime.MinValue || value == DateTime.MaxValue)
                {
                    this.ShowCheckBox = true;
                    this.Checked = false;
                    base.Value = DateTime.Now;
                }
                else
                {
                    base.Value = (DateTime)value;
                }
            }
        }

        internal Rectangle ButtonRect
        {
            get
            {
                return this.GetDropDownButtonRect();
            }
        }


        [Browsable(false)]
        public new Color BackColor
        {
            get { return base.BackColor; }
        }

        [Browsable(false)]
        public new RightToLeft RightToLeft
        {
            get { return base.RightToLeft; }
        }

        #endregion

        #region Override methods

        protected override void OnDropDown(EventArgs eventargs)
        {
            InvalidateSince = 0;
            DroppedDown = true;
            base.OnDropDown(eventargs);
        }
        protected override void OnCloseUp(EventArgs eventargs)
        {
            DroppedDown = false;
            base.OnCloseUp(eventargs);
            this.Invalidate(this.ButtonRect);
        }

        protected override void WndProc(ref Message m)
        {
            IntPtr hDC = IntPtr.Zero;
            Graphics gdc = null;
            switch (m.Msg)
            {
                case (int)WindowMessages.WM_NCPAINT:
                    hDC = Win32.GetWindowDC(m.HWnd);
                    Win32.SendMessage(this.Handle, (int)WindowMessages.WM_ERASEBKGND, hDC, 0);
                    this.SendPrintClientMsg();
                    m.Result = Win32.TRUE;
                    Win32.ReleaseDC(m.HWnd, hDC);
                    break;
                case (int)WindowMessages.WM_PAINT:
                    base.WndProc(ref m);
                    hDC = Win32.GetWindowDC(m.HWnd);
                    gdc = Graphics.FromHdc(hDC);
                    this.DrawButton(gdc);
                    this.DrawComboBoxBorder(gdc);
                    Win32.ReleaseDC(m.HWnd, hDC);
                    gdc.Dispose();
                    break;
                case (int)WindowMessages.WM_SETCURSOR:
                    base.WndProc(ref m);
                    //The value 3 is discovered by trial on error, and cover all kinds of scenarios
                    //InvalidateSince < 2 wil have problem if the control is not in focus and dropdown is clicked
                    //if (DroppedDown && InvalidateSince < 3)
                    //{
                    //    Invalidate();
                    //    InvalidateSince++;
                    //}
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void SendPrintClientMsg()
        {
            Graphics gClient = this.CreateGraphics();
            IntPtr ptrClientDC = gClient.GetHdc();
            Win32.SendMessage(this.Handle, (int)WindowMessages.WM_PRINTCLIENT, ptrClientDC, 0);
            gClient.ReleaseHdc(ptrClientDC);
            gClient.Dispose();
        }

        #endregion

        #region private methods

        #region RenderComboBox

        /// <summary>
        /// 绘制下拉框区域.
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// User:Ryan  CreateTime:2011-07-29 15:44.
        private void DrawComboBoxBorder(Graphics g)
        {
            GDIHelper.InitializeGraphics(g);
            Rectangle rect = new Rectangle(Point.Empty, this.Size);
            rect.Width--;
            rect.Height--;
            using (Pen pen = new Pen(SkinManager.CurrentSkin.BorderColor, 1))
            {
                g.DrawRectangle(pen, rect);
            }
        }

        /// <summary>
        ///  绘制按钮
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// User:Ryan  CreateTime:2011-08-02 14:23.
        private void DrawButton(Graphics g)
        {
            GDIHelper.InitializeGraphics(g);
            RoundRectangle btnRoundRect = new RoundRectangle(this.ButtonRect, 0);
            Color c = this.Enabled ? this._BackColor : SystemColors.Control;
            Size btnSize = new Size(20, 20);
            GDIHelper.FillRectangle(g, btnRoundRect, c);
            GDIHelper.DrawImage(g, this.ButtonRect, Properties.Resources.calendar, btnSize);
        }

        #endregion

        #region GetBoxInfo

        private Rectangle GetDropDownButtonRect()
        {
            ComboBox cb = new ComboBox();
            ComboBoxInfo cbi = new ComboBoxInfo();
            cbi.cbSize = Marshal.SizeOf(cbi);
            Win32.GetComboBoxInfo(cb.Handle, ref cbi);
            cb.Dispose();
            int width = cbi.rcButton.Rect.Width;
            Rectangle rect = new Rectangle(this.Width - width - this._Margin * 2, this._Margin, this.Height - this._Margin, this.Height - this._Margin * 2);
            return rect;
        }

        #endregion

        #region ResetRegion

        private void ResetRegion()
        {
            if (this._CornerRadius > 0)
            {
                Rectangle rect = new Rectangle(Point.Empty, this.Size);
                RoundRectangle roundRect = new RoundRectangle(rect, new CornerRadius(this._CornerRadius));
                if (this.Region != null)
                {
                    this.Region.Dispose();
                }

                this.Region = new Region(roundRect.ToGraphicsBezierPath());
            }
        }
        #endregion

        #endregion
    }
}
