using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls
{
    [ToolboxBitmap(typeof(GroupBox))]
    public class TXGroupBox : GroupBox
    {
        #region fileds

        /// <summary>
        /// 圆角值
        /// </summary>
        private int _CornerRadius = 6;

        /// <summary>
        /// 边框宽度
        /// </summary>
        private int _BorderWidth = 1;

        /// <summary>
        /// 边框颜色
        /// </summary>
        private Color _BorderColor = SkinManager.CurrentSkin.BorderColor;

        /// <summary>
        /// 文本的边距
        /// </summary>
        private int _TextMargin = 6;

        /// <summary>
        /// 边框样式
        /// </summary>
        private EnumBorderStyle _BorderStyle = EnumBorderStyle.Default;

        private Font _CaptionFont = new Font("宋体", 9, FontStyle.Bold);

        private Color _CaptionColor = Color.Black;

        #endregion

        #region Initializes

        public TXGroupBox()
            : base()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            base.BackColor = Color.Transparent;
            ControlHelper.BindMouseMoveEvent(this);
        }

        #endregion

        #region Properties

        [Category("TXProperties")]
        [DefaultValue(6)]
        [Description("圆角值")]
        public int CornerRadius
        {
            get { return this._CornerRadius; }
            set
            {
                this._CornerRadius = value > 0 ? value : 0;
                base.Invalidate();
            }
        }

        [Category("TXProperties")]
        [Description("标题字体")]
        public Font CaptionFont
        {
            get { return this._CaptionFont; }
            set
            {
                this._CaptionFont = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("标题颜色")]
        public Color CaptionColor
        {
            get { return this._CaptionColor; }
            set
            {
                this._CaptionColor = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [DefaultValue(1)]
        [Description("边框宽度")]
        public int BorderWidth
        {
            get { return this._BorderWidth; }
            set
            {
                this._BorderWidth = value > 1 ? value : 1;
                this.Invalidate();
            }
        }

        [Category("TXProperties")]
        [Description("边框颜色")]
        public Color BorderColor
        {
            get { return this._BorderColor; }
            set
            {
                this._BorderColor = value;
                this.Invalidate();
            }
        }

        [Category("TXProperties")]
        [DefaultValue(3)]
        [Description("文本的边距")]
        public int TextMargin
        {
            get { return this._TextMargin; }
            set
            {
                this._TextMargin = value > this._CornerRadius ? value : this._CornerRadius;
                this.Invalidate();
            }
        }

        [Category("TXProperties")]
        [DefaultValue(typeof(EnumBorderStyle), "Default")]
        [Description("边框样式")]
        public EnumBorderStyle BorderStyle
        {
            get { return this._BorderStyle; }
            set
            {
                this._BorderStyle = value;
                this.Invalidate();
            }
        }

        #endregion

        #region Override methods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            GDIHelper.InitializeGraphics(g);
            Rectangle textRect = this.GetTextRect(g);
            Color textColor = this.Enabled ? this._CaptionColor : SkinManager.CurrentSkin.UselessColor;
            switch (this._BorderStyle)
            {
                case EnumBorderStyle.QQStyle:
                    this.DrawQQStyleBorder(g, textRect);
                    break;
                case EnumBorderStyle.Default:
                    this.DrawDefaultBorder(g, textRect);
                    break;
                default:
                    break;
            }

            TextRenderer.DrawText(g, this.Text, this._CaptionFont, textRect, textColor, TextFormatFlags.Left);
        }

        #endregion

        #region private methods

        /// <summary>
        /// 计算文本区域
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// <returns></returns>
        /// User:K.Anding  CreateTime:2011-7-30 22:30.
        private Rectangle GetTextRect(Graphics g)
        {
            Rectangle textRect = new Rectangle();
            Size textSize = g.MeasureString(this.Text, this._CaptionFont).ToSize();
            switch (this._BorderStyle)
            {
                case EnumBorderStyle.Default:
                case EnumBorderStyle.None:
                    textRect.X = this.ClientRectangle.X + this._TextMargin;
                    textRect.Y = 0;
                    textRect.Height = textSize.Height;
                    textRect.Width = textSize.Width + 1;
                    break;
                case EnumBorderStyle.QQStyle:
                    textRect.X = 0;
                    textRect.Y = 0;
                    textRect.Width = textSize.Width + 1;
                    textRect.Height = textSize.Height;
                    break;
            }

            return textRect;
        }

        /// <summary>
        /// 绘制默认边框
        /// </summary>
        /// User:K.Anding  CreateTime:2011-7-30 22:30.
        private void DrawDefaultBorder(Graphics g, Rectangle textRect)
        {
            Rectangle rect = new Rectangle();
            rect.X = 0;
            rect.Y = textRect.Height / 2;
            rect.Height = this.Height - textRect.Height / 2 - 1;
            rect.Width = this.Width - 1;
            RoundRectangle roundRect = new RoundRectangle(rect, new CornerRadius(this._CornerRadius));
            g.SetClip(textRect, CombineMode.Exclude);
            GDIHelper.DrawPathBorder(g, roundRect, this._BorderColor, this._BorderWidth);
            g.ResetClip();
        }

        /// <summary>
        /// 绘制qq风格的边框
        /// </summary>
        /// User:K.Anding  CreateTime:2011-7-30 22:30.
        private void DrawQQStyleBorder(Graphics g, Rectangle textRect)
        {
            Color c1 = this._BorderColor;
            Color c2 = Color.FromArgb(20, c1);
            Rectangle rect = new Rectangle(textRect.Right + this._TextMargin,
                textRect.Height / 2,
                this.Width - textRect.Right - this._TextMargin,
                this._BorderWidth);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, c1, c2, 180))
            {
                Blend blend = new Blend();
                blend.Positions = new float[] { 0f, .2f, 1f };
                blend.Factors = new float[] { 1f, .6f, 0.2f };
                brush.Blend = blend;
                using (Pen pen = new Pen(brush, this._BorderWidth))
                {
                    g.DrawLine(pen, rect.X, rect.Y, rect.Right, rect.Y);
                }
            }
        }

        #endregion
    }
}
