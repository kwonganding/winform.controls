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
    [ToolboxBitmap(typeof(ComboBox))]
    public class TXComboBox : ComboBox
    {
        #region fileds

        /// <summary>
        /// 控件的状态
        /// </summary>
        private EnumControlState _ControlState;

        private IntPtr _EditHandle = IntPtr.Zero;

        private int _Margin = 2;

        private bool _BeginPainting = false;

        private int _CornerRadius = 0;

        private Color _BackColor = Color.White;

        #endregion

        #region Initializes

        public TXComboBox()
            : base()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();
            this.Size = new Size(150, 20);
            this._ControlState = EnumControlState.Default;
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
            RoundRectangle roundRect = new RoundRectangle(rect, new CornerRadius(this._CornerRadius));
            Color backColor = this.Enabled ? this._BackColor : SystemColors.Control;
            g.SetClip(this.EditRect, CombineMode.Exclude);
            GDIHelper.FillRectangle(g, roundRect, backColor);
            g.ResetClip();
            this.DrawButton(g);
            ////边框
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
            btnRect = new Rectangle(this.ButtonRect.X-2, this.ButtonRect.Y - 1, this.ButtonRect.Width + 1 + this._Margin, this.ButtonRect.Height + 2);
            RoundRectangle btnRoundRect = new RoundRectangle(btnRect, new CornerRadius(0, this._CornerRadius, 0, this._CornerRadius));
            Blend blend = new Blend(3);
            blend.Positions = new float[] { 0f, 0.5f, 1f };
            blend.Factors = new float[] { 0f, 1f, 0f };
            GDIHelper.FillRectangle(g, btnRoundRect, SkinManager.CurrentSkin.DefaultControlColor);
            Size btnSize = new Size(12, 7);
            ArrowDirection direction = ArrowDirection.Down;
            GDIHelper.DrawArrow(g, direction, btnRect, btnSize, 0f, Color.FromArgb(30, 178, 239));
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

        #region 附加

        #region 获取值
        /// <summary>
        /// 获取值
        /// </summary>
        private object Value
        {
            get
            {
                ComboBoxItem item = this.SelectedItem as ComboBoxItem;

                if ( item == null )
                {
                    return string.Empty;
                }

                return item.Value;
            }
        }
        #endregion

        #region 类型转换

        #region 转换为日期

        #region 重载1
        /// <summary>
        /// 转换为日期
        /// </summary>
        public DateTime ToDateTime()
        {
            return Convert.ToDateTime( this.Value );
        }
        #endregion

        #region 重载2
        /// <summary>
        /// 转换为日期或null
        /// </summary>
        public DateTime? ToDateTimeOrNull()
        {
            return Convert.ToDateTimeOrNull( this.Value );
        }
        #endregion

        #endregion        

        #region 将数据转换为GUID
        /// <summary>
        /// 将数据转换为GUID
        /// </summary>
        public Guid ToGuid()
        {
            return Convert.ToGuid( this.Value );
        }
        #endregion

        #region 将数据转换为整型
        /// <summary>
        /// 将Text转换为整数值
        /// </summary>
        public int ToInt()
        {
            return Convert.ToInt( this.Value );
        }
        #endregion

        #region 将数据转换为长整型
        /// <summary>
        /// 将数据转换为长整型
        /// </summary>
        public long ToLong()
        {
            return Convert.ToLong( this.Value );
        }
        #endregion

        #region 将数据转换为字符串
        /// <summary>
        /// 将数据转换为字符串
        /// </summary>
        public override string ToString()
        {
            return Convert.ToString( this.Value );
        }
        #endregion

        #region 将数据转换为布尔型
        /// <summary>
        /// 将数据转换为布尔型
        /// </summary>
        public bool ToBool()
        {
            return Convert.ToBool( this.Value );
        }
        #endregion

        #region 将数据转换为单精度浮点型
        /// <summary>
        /// 将数据转换为单精度浮点型
        /// </summary>
        public float ToFloat()
        {
            return Convert.ToFloat( this.Value );
        }
        #endregion

        #region 将数据转换为双精度浮点型

        #region 重载1
        /// <summary>
        /// 将Text转换为浮点值
        /// </summary>
        public double ToDouble()
        {
            return Convert.ToDouble( this.Value );
        }
        #endregion

        #region 重载2
        /// <summary>
        /// 将数据转换为双精度浮点型,并设置小数位
        /// </summary>
        /// <param name="decimals">小数的位数</param>
        public double ToDouble( int decimals )
        {
            return Convert.ToDouble( this.Value, decimals );
        }
        #endregion

        #endregion

        #region 将数据转换为Decimal类型

        #region 重载1
        /// <summary>
        /// 将数据转换为Decimal类型
        /// </summary>
        public decimal ToDecimal()
        {
            return Convert.ToDecimal( this.Value );
        }
        #endregion

        #region 重载2
        /// <summary>
        /// 将数据转换为Decimal类型
        /// </summary>
        /// <param name="decimals">小数的位数</param>
        public decimal ToDecimal( int decimals )
        {
            return Convert.ToDecimal( this.Value, decimals );
        }
        #endregion

        #endregion

        #region 将数据转换为任意类型
        /// <summary>
        /// 将数据转换为任意类型
        /// </summary>
        /// <typeparam name="T">转换的目标类型</typeparam>
        public T ConvertTo<T>()
        {
            return Convert.ConvertTo<T>( this.Value );
        }
        #endregion

        #region 转换为枚举
        /// <summary>
        /// 转换为枚举
        /// </summary>
        /// <typeparam name="T">枚举类型,即enum关键字定义的枚举名,比如Enum1</typeparam>
        public T ToEnum<T>()
        {
            return TX.Framework.Helper.Enum.GetInstance<T>( this.Value );
        }
        #endregion

        #endregion

        #endregion
    }

    #region ComboBoxItem(绑定下拉框的自定义对象)

    /// <summary>
    /// 绑定下拉框的自定义对象
    /// </summary>
    /// User:Ryan  CreateTime:2012-9-14 20:48.
    public class ComboBoxItem
    {
        public ComboBoxItem( string text, string value )
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// 内容文本
        /// </summary>
        /// <value>The text.</value>
        /// User:Ryan  CreateTime:2012-9-14 20:49.
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// 值
        /// </summary>
        /// <value>The value.</value>
        /// User:Ryan  CreateTime:2012-9-14 20:49.
        public string Value
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
    #endregion
}
