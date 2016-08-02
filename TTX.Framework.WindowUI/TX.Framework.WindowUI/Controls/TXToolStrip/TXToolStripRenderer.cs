using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;

namespace TX.Framework.WindowUI.Controls
{
    /// <summary>
    /// 注意：这里不是控件，他是对.net菜单类组件的抽象类ToolStripRenderer的重新设计以实现对菜单的重绘。
    /// 使用方法就是重新定义ToolStripManager的Renderer属性即可,而控件本身不需要做任何修改。
    /// </summary>
    /// User:K.Anding  CreateTime:2011-7-24 23:10.
    public class TXToolStripRenderer : ToolStripRenderer
    {
        #region fileds

        /// <summary>
        /// 左区域边距
        /// </summary>
        private readonly int _OffsetMargin;

        #endregion

        #region Initializes

        public TXToolStripRenderer()
            : base()
        {
            this._OffsetMargin = 24;
            this.MenuCornerRadius = 0;
            this.MenuImageMarginBackColor = SkinManager.CurrentSkin.DefaultControlColor.First;
            this.MenuImageBackImage = Properties.Resources.logo_mini;
            this.MenuImageBackImageOpacity = 0.16f;
            this.ShowMenuBackImage = true;
            this.MenuBorderColor = SkinManager.CurrentSkin.BorderColor;
            this.BackColor = SkinManager.CurrentSkin.BaseColor;
            this.ItemCornerRadius = 1;
        }
        #endregion

        #region Proterties

        public Color BackColor { get; set; }

        /// <summary>
        /// 菜单区域圆角值
        /// </summary>
        public int MenuCornerRadius { get; set; }

        /// <summary>
        /// Item项的背景圆角值
        /// </summary>
        /// <value>The item corner radius.</value>
        /// User:K.Anding  CreateTime:2011-8-7 10:19.
        public int ItemCornerRadius { get; set; }

        /// <summary>
        /// 菜单图标栏的背景色
        /// </summary>
        public Color MenuImageMarginBackColor { get; set; }

        /// <summary>
        /// 菜单图标栏的背景图片
        /// </summary>
        public Image MenuImageBackImage { get; set; }

        /// <summary>
        /// 菜单栏图标栏的背景图片透明度
        /// </summary>
        public float MenuImageBackImageOpacity { get; set; }

        /// <summary>
        /// 是否显示菜单图标栏的背景图片
        /// </summary>
        public bool ShowMenuBackImage { get; set; }

        /// <summary>
        /// 菜单的边框颜色
        /// </summary>
        public Color MenuBorderColor { get; set; }

        #endregion

        #region override methods

        #region 绘制区域背景(OnRenderToolStripBackground)

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Graphics g = e.Graphics;
            GDIHelper.InitializeGraphics(g);
            Rectangle rect = e.AffectedBounds;
            CornerRadius toolStripCornerRadius = new CornerRadius(this.MenuCornerRadius);
            RoundRectangle roundRect = new RoundRectangle(rect, toolStripCornerRadius);
            if (toolStrip is ToolStripDropDown || toolStrip is ContextMenuStrip)
            {
                this.CreateToolStripRegion(toolStrip, roundRect);
                GDIHelper.FillPath(g, roundRect, this.BackColor, this.BackColor);
            }
            else if (toolStrip is TXMenuStrip)
            {
                TXMenuStrip ms = toolStrip as TXMenuStrip;
                Color c1 = ms.BeginBackColor;
                Color c2 = ms.EndBackColor;
                GDIHelper.FillPath(g, new RoundRectangle(rect, new CornerRadius(0)), c1, c2);
            }
            else if (toolStrip is TXToolStrip)
            {
                rect.Inflate(1, 1);
                TXToolStrip ts = toolStrip as TXToolStrip;
                Color c1 = ts.BeginBackColor;
                Color c2 = ts.EndBackColor;
                GDIHelper.FillPath(g, new RoundRectangle(rect, new CornerRadius(0)), c1, c2);
            }
            else if (toolStrip is TXStatusStrip)
            {
                TXStatusStrip ss = toolStrip as TXStatusStrip;
                Color c1 = ss.BeginBackColor;
                Color c2 = ss.EndBackColor;
                GDIHelper.FillPath(g, new RoundRectangle(rect, new CornerRadius(0)), c1, c2);
            }
        }

        #endregion

        #region 绘制项的背景(OnRenderImageMargin)

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Graphics g = e.Graphics;
            GDIHelper.InitializeGraphics(g);
            Rectangle rect = e.AffectedBounds;
            rect.Width -= 1; rect.Height -= 1;
            if (toolStrip is ToolStripDropDown)
            {
                rect.Width = this._OffsetMargin;
                Color c = this.MenuImageMarginBackColor;
                CornerRadius toolStripCornerRadius = new CornerRadius(this.MenuCornerRadius);
                RoundRectangle roundRect = new RoundRectangle(rect, toolStripCornerRadius);
                GDIHelper.FillPath(g, new RoundRectangle(rect, new CornerRadius(this.MenuCornerRadius, 0, this.MenuCornerRadius, 0)), c, c);
                Image img = this.MenuImageBackImage;
                if (img != null && this.ShowMenuBackImage)
                {
                    ImageAttributes imgAttributes = new ImageAttributes();
                    GDIHelper.SetImageOpacity(imgAttributes, this.MenuImageBackImageOpacity);
                    g.DrawImage(Properties.Resources.logo_mini, new Rectangle(rect.X + 1, rect.Y + 2, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
                }

                ////绘制间隔线
                Point p1, p2;
                p1 = new Point(rect.X + this._OffsetMargin, rect.Y + 3);
                p2 = new Point(rect.X + this._OffsetMargin, rect.Bottom - 3);
                using (Pen pen = new Pen(SkinManager.CurrentSkin.BorderColor))
                {
                    g.DrawLine(pen, p1, p2);
                }
            }
            else
            {
                base.OnRenderImageMargin(e);
            }
        }

        #endregion

        #region 绘制边框效果(OnRenderToolStripBorder)

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Graphics g = e.Graphics;
            GDIHelper.InitializeGraphics(g);
            Rectangle rect = e.AffectedBounds;
            if (toolStrip is ToolStripDropDown)
            {
                ////阴影边框
                rect.Width--; rect.Height--;
                CornerRadius toolStripCornerRadius = new CornerRadius(this.MenuCornerRadius);
                RoundRectangle roundRect = new RoundRectangle(rect, toolStripCornerRadius);
                GDIHelper.DrawPathBorder(g, roundRect, this.MenuBorderColor);
            }
            else
            {
                base.OnRenderToolStripBorder(e);
            }
        }

        #endregion

        #region 绘制Item的状态背景样式（）

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            ToolStripItem item = e.Item;
            Graphics g = e.Graphics;
            GDIHelper.InitializeGraphics(g);
            Rectangle rect = new Rectangle(2, -1, item.Width - 4, item.Height + 1);
            RoundRectangle roundRect = new RoundRectangle(rect, new CornerRadius(0));
            if (item.Selected || item.Pressed)
            {
                Color c1 = Color.FromArgb(200, SkinManager.CurrentSkin.HeightLightControlColor.First);
                Color c2 = Color.FromArgb(250, c1);
                GDIHelper.FillRectangle(g, rect, SkinManager.CurrentSkin.HeightLightControlColor);
                //GDIHelper.DrawPathBorder(g, roundRect);
            }
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            ToolStripItem item = e.Item;
            Graphics g = e.Graphics;
            GDIHelper.InitializeGraphics(g);
            ////你真没救了！好吧，我承认我是个具有文艺气质的2B程序员
            if (item.Tag != null && item.Tag.Equals("Vicky"))
            {
                int temp = item.Width >= item.Height ? item.Height : item.Width;
                Rectangle rect = new Rectangle(0, 0, temp, temp);
                rect.Inflate(-1, -1);
                Color c1 = Color.Empty, c2 = Color.Empty, c3 = Color.FromArgb(255, 220, 102);
                Blend blend = new Blend();
                blend.Positions = new float[] { 0f, 0.5f, 1f };
                blend.Factors = new float[] { 0.25f, 0.75f, 1f };
                Color borderColor = item.Selected || item.Pressed ? Color.FromArgb(24, 116, 205) :
                    SkinManager.CurrentSkin.BorderColor;
                float w = 1.0F;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                if (item.Selected || item.Pressed)
                {
                    w = 2.0F;
                    c1 = Color.FromArgb(255, 226, 48);
                    c2 = Color.FromArgb(255, 220, 102);
                    GDIHelper.DrawCrystalButton(g, rect, c1, c2, c3, blend);
                }

                using (Pen p = new Pen(borderColor, w))
                {
                    g.DrawEllipse(p, rect);
                }
            }
            else
            {
                Rectangle rect = new Rectangle(1, 1, item.Width - 4, item.Height - 3);
                RoundRectangle roundRect = new RoundRectangle(rect, this.ItemCornerRadius);
                if (item.Selected || item.Pressed)
                {
                    GDIHelper.FillRectangle(g, roundRect, SkinManager.CurrentSkin.HeightLightControlColor);
                    GDIHelper.DrawPathBorder(g, roundRect);
                }
            }
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            ToolStripItem item = e.Item;
            Graphics g = e.Graphics;
            GDIHelper.InitializeGraphics(g);
            Rectangle rect = new Rectangle(0, 0, item.Width - 1, item.Height - 1);
            RoundRectangle roundRect = new RoundRectangle(rect, this.ItemCornerRadius);
            if (item.Selected || item.Pressed)
            {
                GDIHelper.FillRectangle(g, roundRect, SkinManager.CurrentSkin.HeightLightControlColor);
                GDIHelper.DrawPathBorder(g, roundRect);
            }
        }

        #endregion

        #region OnRenderArrow

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            Size arrowSize = new Size(8, 8);
            Graphics g = e.Graphics;
            GDIHelper.InitializeGraphics(g);
            Rectangle rect = e.ArrowRectangle;
            rect.X -= 2;
            GDIHelper.DrawArrow(g, e.Direction, rect, arrowSize);
        }
        #endregion

        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderOverflowButtonBackground(e);
            Graphics g = e.Graphics;
            GDIHelper.InitializeGraphics(g);
            ToolStripItem item = e.Item;
            Rectangle rect = item.Bounds;
            rect = new Rectangle(0, 0, rect.Width, rect.Height);
            Size arrowSize = new Size(8, 8);
            GDIHelper.DrawArrow(g, ArrowDirection.Down, rect, arrowSize);
        }

        #region 绘制Item的图标（OnRenderItemImage）

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemImage(e);
            ////这个暂时不用，若用到了额外的offset，则需要重写。
        }

        #endregion

        #region 绘制item的text（OnRenderItemText）

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            base.OnRenderItemText(e);
        }

        #endregion

        #region 绘制item项的check状态(OnRenderItemCheck)

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Graphics g = e.Graphics;
            GDIHelper.InitializeGraphics(g);
            Rectangle rect = e.ImageRectangle;
            if (toolStrip is ToolStripDropDown)
            {
                rect.Width -= 2; rect.Height -= 2;
                RoundRectangle roundRect = new RoundRectangle(rect, 1);
                GDIHelper.DrawCheckBox(g, roundRect);
                GDIHelper.DrawCheckedStateByImage(g, rect);
            }
            else
            {
                base.OnRenderItemCheck(e);
            }
        }

        #endregion

        #region 绘制分割线

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Rectangle rect = e.Item.ContentRectangle;
            if (toolStrip is ToolStripDropDown)
            {
                rect.X += this._OffsetMargin;
                rect.Width -= this._OffsetMargin;
            }

            Graphics g = e.Graphics;
            this.DrawSeparatorLine(rect, g, e.Vertical);
        }
        #endregion

        #endregion

        #region private methods

        #region CreateToolStripRegion

        /// <summary>
        /// 创建ToolStrip的工作区域
        /// </summary>
        /// <param name="toolStrip">The ToolStrip.</param>
        /// <param name="roundRect">The RoundRectangle.</param>
        /// User:Ryan  CreateTime:2011-07-25 18:40.
        internal void CreateToolStripRegion(ToolStrip toolStrip, RoundRectangle roundRect)
        {
            using (GraphicsPath path = roundRect.ToGraphicsBezierPath())
            {
                Region region = new Region(path);
                path.Widen(new Pen(this.MenuBorderColor));
                region.Union(path);
                if (toolStrip.Region != null)
                {
                    toolStrip.Region.Dispose();
                }

                toolStrip.Region = region;
            }
        }

        #endregion

        #region DrawSeparatorLine

        /// <summary>
        /// 绘制分割线
        /// </summary>
        /// <param name="roundRect">The Rectangle.</param>
        /// <param name="g">The Graphics.</param>
        /// User:Ryan  CreateTime:2011-07-25 18:40.
        private void DrawSeparatorLine(Rectangle rect, Graphics g, bool isVertical)
        {
            Color c1 = SkinManager.CurrentSkin.BorderColor;
            Color c2 = Color.FromArgb(10, c1);
            int angle = isVertical ? 90 : 180;
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, c1, c2, angle))
            {
                Blend blend = new Blend();
                blend.Positions = new float[] { 0f, .2f, .5f, .8f, 1f };
                blend.Factors = new float[] { 1f, .3f, 0f, .3f, 1f };
                brush.Blend = blend;
                using (Pen pen = new Pen(brush))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    if (isVertical)
                    {
                        g.DrawLine(pen, rect.X, rect.Y + 1, rect.X, rect.Bottom - 1);
                    }
                    else
                    {
                        g.DrawLine(pen, rect.X, rect.Y, rect.Right, rect.Y);
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}
