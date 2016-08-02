using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;

namespace TX.Framework.WindowUI.Controls
{
    [ToolboxBitmap(typeof(RadioButton))]
    public class TXRadioButton : RadioButton
    {
        #region fileds

        /// <summary>
        /// 控件的状态
        /// </summary>
        private EnumControlState _ControlState;

        /// <summary>
        /// 外圆形框的半径
        /// </summary>
        private int _MaxRadius = 8;

        /// <summary>
        /// 内圆形框的半径
        /// </summary>
        private int _MinRadius = 4;

        /// <summary>
        /// 内容边距间隔
        /// </summary>
        private int _Margin = 2;

        #endregion

        #region Initializes

        public TXRadioButton()
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
            base.MinimumSize = new Size(22, 22);
            this._ControlState = EnumControlState.Default;
        }

        #endregion

        #region Properties

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
            get { return new Size(22, 22); }
            set { base.MinimumSize = new Size(22, 22); }
        }

        [Category("TXProperties")]
        [DefaultValue("8")]
        [Description("外圆半径值")]
        public int MaxRadius
        {
            get { return this._MaxRadius; }
            set
            {
                this._MaxRadius = value >= 3 ? value : 3;
                this.Invalidate();
            }
        }

        [Category("TXProperties")]
        [DefaultValue("4")]
        [Description("内圆半径值")]
        public int MinRadius
        {
            get { return this._MinRadius; }
            set
            {
                this._MinRadius = value >= 1 ? value : 1;
                this.Invalidate();
            }
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
                if (ClientRectangle.Contains(e.Location))
                {
                    this._ControlState = EnumControlState.HeightLight;
                }
                else
                {
                    this._ControlState = EnumControlState.Default;
                }
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
            Rectangle maxRect = new Rectangle(this._Margin, h / 2 - this._MaxRadius, this._MaxRadius * 2, this._MaxRadius * 2);
            Rectangle minRect = new Rectangle(this._Margin + this._MaxRadius - this._MinRadius, h / 2 - this._MinRadius, this._MinRadius * 2, this._MinRadius * 2);
            Size textSize = g.MeasureString(this.Text, this.Font).ToSize();
            Rectangle textRect = new Rectangle();
            textRect.X = maxRect.Right + this._Margin;
            textRect.Y = h / 2 - textSize.Height / 2 + 1;
            textRect.Height = textSize.Height;
            textRect.Width = this.Width - textRect.Left;
            GDIHelper.DrawEllipseBorder(g, maxRect, SkinManager.CurrentSkin.BorderColor, 2);
            //GDIHelper.DrawEllipseBorder(g, minRect, SkinManager.CurrentSkin.BorderColor, 2);
            GDIHelper.FillEllipse(g, minRect, SkinManager.CurrentSkin.DefaultControlColor.First);
            GDIHelper.DrawEllipseBorder(g, minRect, SkinManager.CurrentSkin.BorderColor, 1);
            switch (this._ControlState)
            {
                case EnumControlState.HeightLight:
                case EnumControlState.Focused:
                    maxRect.Inflate(1, 1);
                    GDIHelper.DrawEllipseBorder(g, maxRect, SkinManager.CurrentSkin.OuterBorderColor, 1);
                    maxRect.Inflate(-2, -2);
                    GDIHelper.DrawEllipseBorder(g, maxRect, SkinManager.CurrentSkin.InnerBorderColor, 1);
                    break;
            }

            Color c = this.Enabled ? this.ForeColor : SkinManager.CurrentSkin.UselessColor;
            TextRenderer.DrawText(g, this.Text, this.Font, textRect, c, TextFormatFlags.Default);
            if (this.Checked)
            {
                GDIHelper.FillEllipse(g, minRect, Color.FromArgb(15, 216, 32), Color.Green);
                c = SkinManager.CurrentSkin.BorderColor;
                GDIHelper.DrawEllipseBorder(g, minRect, c, 1);
            }
        }

        #endregion

        #endregion
    }
}
