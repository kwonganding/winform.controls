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
    [ToolboxItem(false)]
    public class TXPopupComboBox : PopupComboBox
    {
        #region fileds

        private IntPtr _EditHandle = IntPtr.Zero;

        private int _Margin = 2;

        private bool _BeginPainting = false;

        private Color _BackColor = Color.White;

        #endregion

        #region Initializes

        public TXPopupComboBox()
            : base()
        {
            this.Size = new Size(150, 20);
            base.DropDownStyle = ComboBoxStyle.DropDown;
        }

        #endregion

        #region Properties

        internal Rectangle ButtonRect
        {
            get
            {
                return this.GetDropDownButtonRect();
            }
        }

        internal Rectangle EditRect
        {
            get
            {
                if (this.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    Rectangle rect = new Rectangle(
                        this._Margin, this._Margin, Width - this.ButtonRect.Width - this._Margin * 2, Height - this._Margin * 2);
                    if (RightToLeft == RightToLeft.Yes)
                    {
                        rect.X += this.ButtonRect.Right;
                    }

                    return rect;
                }

                if (IsHandleCreated && this._EditHandle != IntPtr.Zero)
                {
                    RECT rcClient = new RECT();
                    Win32.GetWindowRect(this._EditHandle, ref rcClient);
                    return RectangleToClient(rcClient.Rect);
                }

                return Rectangle.Empty;
            }
        }

        #endregion

        #region Override methods

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case(int)WindowMessages.MOUSEWHEEL:
                    return;
                case (int)WindowMessages.WM_PAINT:
                    switch (this.DropDownStyle)
                    {
                        case ComboBoxStyle.DropDown:
                            if (!this._BeginPainting)
                            {
                                PAINTSTRUCT ps = new PAINTSTRUCT();
                                this._BeginPainting = true;
                                Win32.BeginPaint(m.HWnd, ref ps);
                                this.DrawComboBox(ref m);
                                Win32.EndPaint(m.HWnd, ref ps);
                                this._BeginPainting = false;
                                m.Result = Win32.TRUE;
                            }
                            else
                            {
                                base.WndProc(ref m);
                            }
                            break;
                        case ComboBoxStyle.DropDownList:
                            base.WndProc(ref m);
                            this.DrawComboBox(ref m);
                            break;
                        default:
                            base.WndProc(ref m);
                            break;
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

        #region private methods

        #region RenderComboBox

        /// <summary>
        /// 绘制复选框和内容.
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-29 15:44.
        private void DrawComboBox(ref Message msg)
        {
            using (Graphics g = Graphics.FromHwnd(msg.HWnd))
            {
                this.DrawComboBox(g);
            }
        }

        /// <summary>
        /// 绘制下拉框区域.
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// User:Ryan  CreateTime:2011-07-29 15:44.
        private void DrawComboBox(Graphics g)
        {
            GDIHelper.InitializeGraphics(g);
            Rectangle rect = new Rectangle(Point.Empty, this.Size);
            rect.Width--; rect.Height--;
            ////背景
            RoundRectangle roundRect = new RoundRectangle(rect, 0);
            Color backColor = this.Enabled ? this._BackColor : SystemColors.Control;
            g.SetClip(this.EditRect, CombineMode.Exclude);
            GDIHelper.FillRectangle(g, roundRect, backColor);
            g.ResetClip();
            this.DrawButton(g);
            GDIHelper.DrawPathBorder(g, roundRect);
        }

        /// <summary>
        ///  绘制按钮
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// User:Ryan  CreateTime:2011-08-02 14:23.
        private void DrawButton(Graphics g)
        {
            Rectangle btnRect;
            EnumControlState btnState = this.GetComboBoxButtonPressed() ? EnumControlState.HeightLight : EnumControlState.Default;
            btnRect = new Rectangle(this.ButtonRect.X, this.ButtonRect.Y - 1, this.ButtonRect.Width + 1 + this._Margin, this.ButtonRect.Height + 2);
            RoundRectangle btnRoundRect = new RoundRectangle(btnRect, new CornerRadius(0));
            //Blend blend = new Blend(3);
            //blend.Positions = new float[] { 0f, 0.5f, 1f };
            //blend.Factors = new float[] { 0f, 1f, 0f };
            GDIHelper.FillRectangle(g, btnRoundRect, SkinManager.CurrentSkin.DefaultControlColor);
            Size btnSize = new Size(12, 7);
            GDIHelper.DrawArrow(g, ArrowDirection.Down, btnRect, btnSize, 0f, Color.FromArgb(30, 178, 239));
            Color lineColor = SkinManager.CurrentSkin.BorderColor;
            GDIHelper.DrawGradientLine(g, lineColor, 90, btnRect.X, btnRect.Y, btnRect.X, btnRect.Bottom - 1);
        }

        #endregion

        #region GetBoxInfo

        private ComboBoxInfo GetComboBoxInfo()
        {
            ComboBoxInfo cbi = new ComboBoxInfo();
            cbi.cbSize = Marshal.SizeOf(cbi);
            Win32.GetComboBoxInfo(base.Handle, ref cbi);
            return cbi;
        }

        private bool GetComboBoxButtonPressed()
        {
            ComboBoxInfo cbi = this.GetComboBoxInfo();
            return cbi.stateButton == ComboBoxButtonState.STATE_SYSTEM_PRESSED;
        }

        private Rectangle GetDropDownButtonRect()
        {
            ComboBoxInfo cbi = this.GetComboBoxInfo();
            return cbi.rcButton.Rect;
        }

        #endregion

        #endregion
    }
}
