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
    [ToolboxBitmap(typeof(CheckBox))]
    public class TXCheckBox : CheckBox
    {
        #region fileds

        /// <summary>
        /// 控件的状态
        /// </summary>
        private EnumControlState _ControlState;

        /// <summary>
        /// 复选框大小
        /// </summary>
        private Size _BoxSize = new Size(14, 14);

        /// <summary>
        /// 圆角值
        /// </summary>
        private int _CornerRadius = 1;

        /// <summary>
        /// 内容边距间隔
        /// </summary>
        private int _Margin = 2;

        #endregion

        #region Initializes

        public TXCheckBox()
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
            this.MinimumSize = new Size(20, 20);
            this._ControlState = EnumControlState.Default;
        }

        #endregion

        #region Properties

        [Category("TXProperties")]
        [Description("圆角的半径值")]
        [DefaultValue(1)]
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
        [DefaultValue(typeof(Size), "14,14")]
        [Description("复选框的大小")]
        public Size BoxSize
        {
            get { return this._BoxSize; }
            set
            {
                this._BoxSize = value;
                this.Invalidate();
            }
        }

        [Browsable(false)]
        public new RightToLeft RightToLeft
        {
            get { return RightToLeft.No; }
            set { base.RightToLeft = RightToLeft.No; }
        }

        [Browsable(false)]
        public new ContentAlignment TextAlign
        {
            get { return ContentAlignment.MiddleLeft; }
            set { base.TextAlign = ContentAlignment.MiddleLeft; }
        }

        [Browsable(false)]
        public new Size MinimumSize
        {
            get { return new Size(20, 20); }
            set { base.MinimumSize = new Size(20, 20); }

        }
        #endregion

        #region Override methods

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this._ControlState = EnumControlState.HeightLight;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this._ControlState = EnumControlState.Default;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                this._ControlState = EnumControlState.Focused;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                this._ControlState = EnumControlState.Default;
                //if (ClientRectangle.Contains(e.Location))
                //{
                //    this._ControlState = EnumControlState.HeightLight;
                //}
                //else
                //{
                //    this._ControlState = EnumControlState.HeightLight;
                //}
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaintBackground(e);
            this.DrawContent(e.Graphics);
        }

        #endregion

        #region private methods

        #region DrawContent

        /// <summary>
        /// 绘制复选框和内容.
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// User:Ryan  CreateTime:2011-07-29 15:44.
        private void DrawContent(Graphics g)
        {
            GDIHelper.InitializeGraphics(g);
            int w = this.Width;
            int h = this.Height;
            Rectangle boxRect = new Rectangle(this._Margin, h / 2 - this._BoxSize.Height / 2, this._BoxSize.Width, this._BoxSize.Height);
            Size textSize = g.MeasureString(this.Text, this.Font).ToSize();
            Rectangle textRect = new Rectangle();
            textRect.X = boxRect.Right + this._Margin;
            textRect.Y = this._Margin;
            textRect.Height = this.Height - this._Margin * 2;
            textRect.Width = textSize.Width;
            RoundRectangle roundRect = new RoundRectangle(boxRect, this._CornerRadius);
            switch (this._ControlState)
            {
                case EnumControlState.HeightLight:
                    //GDIHelper.DrawPathOuterBorder(g, roundRect, SkinManager.CurrentSkin.OuterBorderColor);
                    GDIHelper.DrawPathBorder(g, roundRect, SkinManager.CurrentSkin.OuterBorderColor);
                    GDIHelper.DrawPathInnerBorder(g, roundRect, SkinManager.CurrentSkin.HeightLightControlColor.First);
                    break;
                default:
                    GDIHelper.DrawCheckBox(g, roundRect);
                    break;
            }

            Color c = base.Enabled ? this.ForeColor : SkinManager.CurrentSkin.UselessColor;

            //TextRenderer.DrawText(g, this.Text, this.Font, textRect, c, TextFormatFlags.Default);
            GDIHelper.DrawImageAndString(g, textRect, null, Size.Empty, this.Text, this.Font, c);
            switch (this.CheckState)
            {
                case System.Windows.Forms.CheckState.Checked:
                    GDIHelper.DrawCheckedStateByImage(g, boxRect);
                    break;
                case System.Windows.Forms.CheckState.Indeterminate:
                    Rectangle innerRect = boxRect;
                    innerRect.Inflate(-3, -3);
                    Color cc = Color.FromArgb(46, 117, 35);
                    GDIHelper.FillRectangle(g, new RoundRectangle(innerRect, this._CornerRadius), cc);
                    break;
            }
        }

        #endregion

        #endregion
    }
}
