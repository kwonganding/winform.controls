using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TX.Framework.WindowUI
{
    /// <summary>
    /// 可设置圆角的矩形区域
    /// </summary>
    /// User:Ryan  CreateTime:2011-07-19 9:52.
    internal class RoundRectangle
    {
        #region Initializes

        /// <summary>
        /// (构造函数).Initializes a new instance of the <see cref="RoundRectangle"/> class.
        /// </summary>
        /// <param name="roundRect">The roundRect.</param>
        /// <param name="radius">The radius.</param>
        /// User:Ryan  CreateTime:2011-07-19 16:59.
        public RoundRectangle(Rectangle rect, int radius)
            : this(rect, new CornerRadius(radius))
        {
        }

        /// <summary>
        /// (构造函数).Initializes a new instance of the <see cref="RoundRectangle"/> class.
        /// </summary>
        /// <param name="roundRect">The roundRect.</param>
        /// <param name="_CornerRadius">The corner radius.</param>
        /// User:Ryan  CreateTime:2011-07-19 16:59.
        public RoundRectangle(Rectangle rect, CornerRadius cornerRadius)
        {
            this.Rect = rect;
            this.CornerRadius = cornerRadius;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取或者设置矩形区域
        /// </summary>
        /// <value>The roundRect.</value>
        /// User:Ryan  CreateTime:2011-07-19 16:59.
        public Rectangle Rect { get; set; }

        /// <summary>
        /// 获取或者设置圆角值
        /// </summary>
        /// <value>The corner radius.</value>
        /// User:Ryan  CreateTime:2011-07-19 16:59.
        public CornerRadius CornerRadius { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 获取该圆角矩形的GraphicsPath对象(圆角使用Bezier曲线实现)
        /// </summary>
        /// <returns>
        /// Return a data(or instance) of GraphicsPath.
        /// </returns>
        /// User:Ryan  CreateTime:2011-07-20 11:52.
        public GraphicsPath ToGraphicsBezierPath()
        {
            GraphicsPath path = new GraphicsPath();
            int x = this.Rect.X;
            int y = this.Rect.Y;
            int w = this.Rect.Width;
            int h = this.Rect.Height;
            path.AddBezier(x, y + this.CornerRadius.TopLeft, x, y, x + this.CornerRadius.TopLeft, y, x + this.CornerRadius.TopLeft, y);
            path.AddLine(x + this.CornerRadius.TopLeft, y, x + w - this.CornerRadius.TopRight, y);
            path.AddBezier(x + w - this.CornerRadius.TopRight, y, x + w, y, x + w, y + this.CornerRadius.TopRight, x + w, y + this.CornerRadius.TopRight);
            path.AddLine(x + w, y + this.CornerRadius.TopRight, x + w, y + h - this.CornerRadius.BottomRigth);
            path.AddBezier(x + w, y + h - this.CornerRadius.BottomRigth, x + w, y + h, x + w - this.CornerRadius.BottomRigth, y + h, x + w - this.CornerRadius.BottomRigth, y + h);
            path.AddLine(x + w - this.CornerRadius.BottomRigth, y + h, x + this.CornerRadius.BottomLeft, y + h);
            path.AddBezier(x + this.CornerRadius.BottomLeft, y + h, x, y + h, x, y + h - this.CornerRadius.BottomLeft, x, y + h - this.CornerRadius.BottomLeft);
            path.AddLine(x, y + h - this.CornerRadius.BottomLeft, x, y + this.CornerRadius.TopLeft);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 获取该圆角矩形的GraphicsPath对象(圆角使用矩形圆弧曲线曲线实现)
        /// </summary>
        /// <returns></returns>
        /// User:K.Anding  CreateTime:2011-7-31 23:25.
        public GraphicsPath ToGraphicsArcPath()
        {
            GraphicsPath path = new GraphicsPath();
            int x = this.Rect.X;
            int y = this.Rect.Y;
            int w = this.Rect.Width;
            int h = this.Rect.Height;
            path.AddArc(x, y, this.CornerRadius.TopLeft, this.CornerRadius.TopLeft, 180, 90);
            path.AddArc(x + w - this.CornerRadius.TopRight, y, this.CornerRadius.TopRight, this.CornerRadius.TopRight, 270, 90);
            path.AddArc(x + w - this.CornerRadius.BottomRigth, y + h - this.CornerRadius.BottomRigth,
                this.CornerRadius.BottomRigth, this.CornerRadius.BottomRigth,
                0, 90);
            path.AddArc(x, y + h - this.CornerRadius.BottomLeft, this.CornerRadius.BottomLeft, this.CornerRadius.BottomLeft, 90, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 获取该圆角矩形的GraphicsPath对象(天使之翼的区域样式，主要用于Tabcontrol的标签样式)
        /// </summary>
        /// <returns>
        /// Return a data(or instance) of GraphicsPath.
        /// </returns>
        /// User:Ryan  CreateTime:2011-07-20 11:52.
        public GraphicsPath ToGraphicsAnglesWingPath()
        {
            GraphicsPath path = new GraphicsPath();
            int x = this.Rect.X;
            int y = this.Rect.Y;
            int w = this.Rect.Width;
            int h = this.Rect.Height;
            path.AddBezier(x, y + this.CornerRadius.TopLeft, x, y, x + this.CornerRadius.TopLeft, y, x + this.CornerRadius.TopLeft, y);
            path.AddLine(x + this.CornerRadius.TopLeft, y, x + w - this.CornerRadius.TopRight, y);
            path.AddBezier(x + w - this.CornerRadius.TopRight, y, x + w, y, x + w, y + this.CornerRadius.TopRight, x + w, y + this.CornerRadius.TopRight);
            path.AddLine(x + w, y + this.CornerRadius.TopRight, x + w, y + h - this.CornerRadius.BottomRigth);
            path.AddBezier(x + w, y + h - this.CornerRadius.BottomRigth, x + w, y + h, x + w + this.CornerRadius.BottomRigth, y + h, x + w + this.CornerRadius.BottomRigth, y + h);
            path.AddLine(x + w + this.CornerRadius.BottomRigth, y + h, x - this.CornerRadius.BottomLeft, y + h);
            path.AddBezier(x - this.CornerRadius.BottomLeft, y + h, x, y + h, x, y + h - this.CornerRadius.BottomLeft, x, y + h - this.CornerRadius.BottomLeft);
            path.AddLine(x, y + h - this.CornerRadius.BottomLeft, x, y + this.CornerRadius.TopLeft);
            path.CloseFigure();
            return path;
        }

        #endregion
    }
}
