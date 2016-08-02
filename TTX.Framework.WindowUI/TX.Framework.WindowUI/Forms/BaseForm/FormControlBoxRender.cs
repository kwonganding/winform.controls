using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace TX.Framework.WindowUI.Forms
{
    /// <summary>
    /// 绘制标题栏的控制按钮
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-3 18:29.
    internal class FormControlBoxRender
    {
        #region ControlBox path

        #region CreateCloseFlagPoints

        /// <summary>
        /// 关闭图标
        /// Creates the close flag points.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>
        /// Return a data(or instance) of GraphicsPath.
        /// </returns>
        /// User:Ryan  CreateTime:2012-8-3 22:10.
        public GraphicsPath CreateCloseFlagPoints(Rectangle rect)
        {
            PointF centerPoint = new PointF(
                rect.X + rect.Width / 2.0f,
                rect.Y + rect.Height / 2.0f);
            GraphicsPath path = new GraphicsPath();
            path.AddLine(
                centerPoint.X,
                centerPoint.Y - 2,
                centerPoint.X - 2,
                centerPoint.Y - 4);
            path.AddLine(
                centerPoint.X - 2,
                centerPoint.Y - 4,
                centerPoint.X - 6,
                centerPoint.Y - 4);
            path.AddLine(
                centerPoint.X - 6,
                centerPoint.Y - 4,
                centerPoint.X - 2,
                centerPoint.Y);
            path.AddLine(
                centerPoint.X - 2,
                centerPoint.Y,
                centerPoint.X - 6,
                centerPoint.Y + 4);
            path.AddLine(
                centerPoint.X - 6,
                centerPoint.Y + 4,
                centerPoint.X - 2,
                centerPoint.Y + 4);
            path.AddLine(
                centerPoint.X - 2,
                centerPoint.Y + 4,
                centerPoint.X,
                centerPoint.Y + 2);
            path.AddLine(
                centerPoint.X,
                centerPoint.Y + 2,
                centerPoint.X + 2,
                centerPoint.Y + 4);
            path.AddLine(
               centerPoint.X + 2,
               centerPoint.Y + 4,
               centerPoint.X + 6,
               centerPoint.Y + 4);
            path.AddLine(
              centerPoint.X + 6,
              centerPoint.Y + 4,
              centerPoint.X + 2,
              centerPoint.Y);
            path.AddLine(
             centerPoint.X + 2,
             centerPoint.Y,
             centerPoint.X + 6,
             centerPoint.Y - 4);
            path.AddLine(
             centerPoint.X + 6,
             centerPoint.Y - 4,
             centerPoint.X + 2,
             centerPoint.Y - 4);
            path.CloseFigure();
            return path;
        }

        #endregion

        #region CreateMinimizeFlagPath

        public GraphicsPath CreateMinimizeFlagPath(Rectangle rect)
        {
            PointF centerPoint = new PointF(
                rect.X + rect.Width / 2.0f,
                rect.Y + rect.Height / 2.5f);

            GraphicsPath path = new GraphicsPath();

            path.AddRectangle(new RectangleF(
                centerPoint.X - 6,
                centerPoint.Y + 1,
                12,
                2));
            return path;
        }

        #endregion

        #region CreateMaximizeFlafPath

        public GraphicsPath CreateMaximizeFlafPath(
            Rectangle rect, bool maximize)
        {
            PointF centerPoint = new PointF(
               rect.X + rect.Width / 2.0f,
               rect.Y + rect.Height / 1.9f);

            GraphicsPath path = new GraphicsPath();

            if (maximize)
            {
                path.AddLine(
                    centerPoint.X - 3,
                    centerPoint.Y - 2,
                    centerPoint.X - 6,
                    centerPoint.Y - 2);
                path.AddLine(
                    centerPoint.X - 6,
                    centerPoint.Y - 3,
                    centerPoint.X - 6,
                    centerPoint.Y + 5);
                path.AddLine(
                    centerPoint.X - 6,
                    centerPoint.Y + 5,
                    centerPoint.X + 3,
                    centerPoint.Y + 5);
                path.AddLine(
                    centerPoint.X + 3,
                    centerPoint.Y + 5,
                    centerPoint.X + 3,
                    centerPoint.Y + 1);
                path.AddLine(
                    centerPoint.X + 3,
                    centerPoint.Y + 1,
                    centerPoint.X + 6,
                    centerPoint.Y + 1);
                path.AddLine(
                    centerPoint.X + 6,
                    centerPoint.Y + 1,
                    centerPoint.X + 6,
                    centerPoint.Y - 6);
                path.AddLine(
                    centerPoint.X + 6,
                    centerPoint.Y - 6,
                    centerPoint.X - 3,
                    centerPoint.Y - 6);
                path.CloseFigure();

                path.AddRectangle(new RectangleF(
                    centerPoint.X - 4,
                    centerPoint.Y,
                    5,
                    3));

                path.AddLine(
                    centerPoint.X - 1,
                    centerPoint.Y - 4,
                    centerPoint.X + 4,
                    centerPoint.Y - 4);
                path.AddLine(
                    centerPoint.X + 4,
                    centerPoint.Y - 4,
                    centerPoint.X + 4,
                    centerPoint.Y - 1);
                path.AddLine(
                    centerPoint.X + 4,
                    centerPoint.Y - 1,
                    centerPoint.X + 3,
                    centerPoint.Y - 1);
                path.AddLine(
                    centerPoint.X + 3,
                    centerPoint.Y - 1,
                    centerPoint.X + 3,
                    centerPoint.Y - 3);
                path.AddLine(
                    centerPoint.X + 3,
                    centerPoint.Y - 3,
                    centerPoint.X - 1,
                    centerPoint.Y - 3);
                path.CloseFigure();
            }
            else
            {
                path.AddRectangle(new RectangleF(
                    centerPoint.X - 6,
                    centerPoint.Y - 4,
                    12,
                    8));
                path.AddRectangle(new RectangleF(
                    centerPoint.X - 5,
                    centerPoint.Y - 1,
                   10,
                    4));
            }

            return path;
        }

        #endregion

        #endregion

        #region DrawControlBox

        #region DrawControlBox

        /// <summary>
        /// 绘制控制按钮背景（最大化，最小化）
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="controlState">State of the control.</param>
        /// User:Ryan  CreateTime:2012-8-3 22:52.
        public void DrawControlBox(Graphics g, Rectangle rect, EnumControlState controlState)
        {
            GradientColor color;
            switch (controlState)
            {
                case EnumControlState.HeightLight:
                    color = SkinManager.CurrentSkin.ControlBoxHeightLightColor;
                    break;
                case EnumControlState.Focused:
                    color = SkinManager.CurrentSkin.ControlBoxPressedColor;
                    break;
                default:
                    color = SkinManager.CurrentSkin.ControlBoxDefaultColor;
                    break;
            }
            ////去掉底部的线
            Rectangle exRect = new Rectangle(rect.Left, rect.Bottom, rect.Width, 1);
            g.SetClip(exRect, CombineMode.Exclude);
            GDIHelper.FillRectangle(g, rect, color);
            ////绘制边线
            Color c1, c2;
            c1 = SkinManager.CurrentSkin.BorderColor;
            c2 = Color.FromArgb(10, c1);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, c1, c2, 90))
            {
                brush.Blend.Positions = color.Positions;
                brush.Blend.Factors = color.Factors;
                using (Pen pen = new Pen(brush, 1))
                {
                    g.DrawLine(pen, rect.X, rect.Y, rect.X, rect.Bottom);
                }
            }

            g.ResetClip();
        } 
        #endregion

        #region DrawCloseBox

        /// <summary>
        /// 绘制关闭按钮背景
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="controlState">State of the control.</param>
        /// <param name="radius">The radius.</param>
        /// User:Ryan  CreateTime:2012-8-3 22:52.
        public void DrawCloseBox(Graphics g, Rectangle rect, EnumControlState controlState, int radius)
        {
            GradientColor color;
            switch (controlState)
            {
                case EnumControlState.HeightLight:
                    color = SkinManager.CurrentSkin.CloseBoxHeightLightColor;
                    break;
                case EnumControlState.Focused:
                    color = SkinManager.CurrentSkin.CloseBoxPressedColor;
                    break;
                default:
                    color = SkinManager.CurrentSkin.ControlBoxDefaultColor;
                    break;
            }
            Rectangle exRect = new Rectangle(rect.Left, rect.Bottom, rect.Width, 1);
            g.SetClip(exRect, CombineMode.Exclude);
            CornerRadius cr = new CornerRadius(0, radius + 2, 0, 0);
            GDIHelper.FillRectangle(g, new RoundRectangle(rect, cr), color);
            ////绘制边线
            Color c1, c2;
            c1 = SkinManager.CurrentSkin.BorderColor;
            c2 = Color.FromArgb(10, c1);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, c1, c2, 90))
            {
                brush.Blend.Positions = color.Positions;
                brush.Blend.Factors = color.Factors;
                using (Pen pen = new Pen(brush, 1))
                {
                    g.DrawLine(pen, rect.X, rect.Y, rect.X, rect.Bottom);
                }
            }

            g.ResetClip();
        } 
        #endregion

        #endregion
    }
}
