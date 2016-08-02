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
    [DefaultEvent("Click")]
    [ToolboxBitmap(typeof(Button))]
    public partial class TXButton : Button
    {
        #region attribute

        /// <summary>
        /// 圆角值
        /// </summary>
        private int _CornerRadius = 2;

        /// <summary>
        /// 内容边距间隔
        /// </summary>
        private int _Margin = 4;

        /// <summary>
        /// 图标大小
        /// </summary>
        private Size _ImageSize = new Size(16, 16);

        /// <summary>
        /// 控件的状态
        /// </summary>
        private EnumControlState _ControlState;

        #endregion

        #region Initializes

        /// <summary>
        /// (构造函数).Initializes a new instance of the <see cref="TXButton"/> class.
        /// </summary>
        /// User:Ryan  CreateTime:2011-08-01 16:15.
        public TXButton()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.UpdateStyles();
            this._ControlState = EnumControlState.Default;
            base.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.Size = new Size(100, 28);
            this.ResetRegion();
        }
        #endregion

        #region Properties

        [Category("TXProperties")]
        [Description("圆角的半径值")]
        [DefaultValue(2)]
        public int CornerRadius
        {
            get { return this._CornerRadius; }
            set
            {
                this._CornerRadius = value;
                this.Invalidate();
            }
        }

        [Category("TXProperties")]
        [Browsable(true)]
        [Description("图标大小")]
        [DefaultValue(typeof(Size), "16,16")]
        public Size ImageSize
        {
            get { return this._ImageSize; }
            set { this._ImageSize = value; this.Invalidate(); }
        }

        [Category("TXProperties")]
        [Browsable(true)]
        [Description("图标")]
        public new Image Image
        {
            get { return base.Image; }
            set
            {
                base.Image = value;
                this.Invalidate();
            }
        }

        [Category("TXProperties")]
        [Browsable(true)]
        [DefaultValue(typeof(TextImageRelation), "ImageBeforeText")]
        public new TextImageRelation TextImageRelation
        {
            get { return base.TextImageRelation; }
            set { base.TextImageRelation = value; }
        }

        [Browsable(false)]
        public new Color BackColor
        {
            get { return base.BackColor; }
        }

        [Browsable(false)]
        public new ContentAlignment TextAlign
        {
            get { return base.TextAlign; }
        }

        [Browsable(false)]
        public new ContentAlignment ImageAlign
        {
            get { return base.ImageAlign; }
        }

        #endregion

        #region Override methods

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this._ControlState = EnumControlState.HeightLight;
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this._ControlState = EnumControlState.Focused;
                this.Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this._ControlState = EnumControlState.Default;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                this._ControlState = EnumControlState.HeightLight;
                this.Invalidate();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space)
            {
                this._ControlState = EnumControlState.Focused;
                this.Invalidate();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Space)
            {
                this._ControlState = EnumControlState.Default;
                this.Invalidate();
                this.OnClick(e);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this._ControlState = EnumControlState.HeightLight;
            this.Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this._ControlState = EnumControlState.Default;
            this.Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.ResetRegion();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.ResetRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaintBackground(e);
            this.ResetRegion();
            Graphics g = e.Graphics;
            this.DrawBackGround(g);
            this.DrawContent(g);
        }

        #endregion

        #region Private methods

        #region DrawBackGround

        /// <summary>
        /// 绘制背景和边框等
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// User:Ryan  CreateTime:2011-08-01 16:47.
        private void DrawBackGround(Graphics g)
        {
            GDIHelper.InitializeGraphics(g);
            Rectangle rect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);
            RoundRectangle roundRect = new RoundRectangle(rect, new CornerRadius(this._CornerRadius));
            switch (this._ControlState)
            {
                case EnumControlState.Default:
                    if (this.FlatStyle != FlatStyle.Flat)
                    {
                        GDIHelper.FillRectangle(g, roundRect, SkinManager.CurrentSkin.DefaultControlColor);
                        GDIHelper.DrawPathBorder(g, roundRect);
                    }
                    break;
                case EnumControlState.HeightLight:
                    GDIHelper.FillRectangle(g, roundRect, SkinManager.CurrentSkin.HeightLightControlColor);
                    GDIHelper.DrawPathBorder(g, roundRect);
                    break;
                case EnumControlState.Focused:
                    GDIHelper.FillRectangle(g, roundRect, SkinManager.CurrentSkin.FocusedControlColor);
                    GDIHelper.DrawPathBorder(g, roundRect);
                    GDIHelper.DrawPathInnerBorder(g, roundRect);
                    break;
            }
        }

        #endregion

        #region DrawContent

        /// <summary>
        /// 绘制按钮的内容：图标和文字
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// User:Ryan  CreateTime:2011-08-01 16:45.
        private void DrawContent(Graphics g)
        {
            Rectangle imageRect;
            Rectangle textRect;
            this.CalculateRect(out imageRect, out textRect);
            if (this.Image != null)
            {
                g.DrawImage(this.Image, imageRect, 0, 0, this._ImageSize.Width, this._ImageSize.Height, GraphicsUnit.Pixel);
            }

            Color forceColor = this.Enabled ? this.ForeColor : SkinManager.CurrentSkin.UselessColor;
            TextRenderer.DrawText(g, this.Text, this.Font, textRect, forceColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        #endregion

        #region CalculateRect

        /// <summary>
        /// 计算图标和文字的区域
        /// </summary>
        /// <param name="imageRect">The image rect.</param>
        /// <param name="textRect">The text rect.</param>
        /// User:Ryan  CreateTime:2011-08-01 15:52.
        private void CalculateRect(out Rectangle imageRect, out Rectangle textRect)
        {
            imageRect = Rectangle.Empty;
            textRect = Rectangle.Empty;
            if (Image == null)
            {
                textRect = new Rectangle(
                   this._Margin,
                   this._Margin,
                   this.Width - this._Margin * 2,
                   this.Height - this._Margin * 2);
                return;
            }
            Size textSize = TextRenderer.MeasureText(this.Text, this.Font);
            int textMaxWidth = this.Width - this._ImageSize.Width - this._Margin * 3;
            int textWidth = textSize.Width >= textMaxWidth ? textMaxWidth : textSize.Width;
            int contentWidth = this._Margin + this._ImageSize.Width + textWidth;
            switch (TextImageRelation)
            {
                case TextImageRelation.Overlay:
                    imageRect = new Rectangle(
                        this._Margin,
                        (this.Height - this._ImageSize.Height) / 2,
                        this._ImageSize.Width,
                        this._ImageSize.Height);
                    textRect = new Rectangle(
                        this._Margin,
                        this._Margin,
                        this.Width - this._Margin * 2,
                        this.Height);
                    break;
                case TextImageRelation.ImageAboveText:
                    imageRect = new Rectangle(
                        (this.Width - this._ImageSize.Width) / 2,
                        this._Margin,
                        this._ImageSize.Width,
                        this._ImageSize.Height);
                    textRect = new Rectangle(
                        this._Margin,
                        imageRect.Bottom,
                        this.Width - this._Margin * 2,
                        this.Height - imageRect.Bottom - this._Margin);
                    break;
                case TextImageRelation.ImageBeforeText:
                    imageRect = new Rectangle(
                        (this.Width - contentWidth) / 2,
                        (this.Height - this._ImageSize.Height) / 2,
                        this._ImageSize.Width,
                        this._ImageSize.Height);
                    textRect = new Rectangle(
                        imageRect.Right + this._Margin,
                        this._Margin,
                        textWidth,
                        this.Height - this._Margin * 2);
                    break;
                case TextImageRelation.TextAboveImage:
                    imageRect = new Rectangle(
                        (this.Width - this._ImageSize.Width) / 2,
                        this.Height - this._ImageSize.Height - this._Margin,
                        this._ImageSize.Width,
                        this._ImageSize.Height);
                    textRect = new Rectangle(
                        this._Margin,
                        this._Margin,
                        this.Width - this._Margin * 2,
                        this.Height - imageRect.Y - this._Margin);
                    break;
                case TextImageRelation.TextBeforeImage:
                    imageRect = new Rectangle(
                        (this.Width + contentWidth) / 2 - this._ImageSize.Width,
                        (this.Height - this._ImageSize.Height) / 2,
                        this._ImageSize.Width,
                        this._ImageSize.Height);
                    textRect = new Rectangle(
                        (this.Width - contentWidth) / 2,
                        this._Margin,
                        textWidth,
                        this.Height - this._Margin * 2);
                    break;
            }

            if (RightToLeft == RightToLeft.Yes)
            {
                imageRect.X = this.Width - imageRect.Right;
                textRect.X = this.Width - textRect.Right;
            }
        }
        #endregion

        #region ResetRegion

        private void ResetRegion()
        {
            if (this._CornerRadius > 0)
            {
                Rectangle rect = new Rectangle(Point.Empty, this.Size);
                //rect.Height--;
                //rect.Width--;
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
