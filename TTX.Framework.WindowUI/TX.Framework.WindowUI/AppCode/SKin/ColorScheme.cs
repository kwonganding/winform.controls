using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TX.Framework.WindowUI
{
    #region  线性色彩 LinearColor

    /// <summary>
    /// 线性色彩
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-2 23:09.
    internal class LinearColor
    {
        #region Initializes

        /// <summary>
        /// (构造函数).Initializes a new instance of the <see cref="LinearColor"/> struct.
        /// </summary>
        /// <param name="color1">The color1.</param>
        /// <param name="color2">The color2.</param>
        /// User:Ryan  CreateTime:2011-07-19 13:24.
        public LinearColor(Color color1, Color color2)
        {
            this.First = color1;
            this.Second = color2;
        }

        #endregion

        #region Fields

        /// <summary>
        /// 线性色彩1
        /// </summary>
        public Color First;

        /// <summary>
        /// 线性色彩2
        /// </summary>
        public Color Second;

        #endregion
    }
    #endregion

    #region 阶梯渐变色彩 GradientColor

    /// <summary>
    /// 阶梯渐变色彩
    /// </summary>
    internal struct GradientColor
    {
        /// <summary>
        /// (构造函数).Initializes a new instance of the <see cref="GradientColor"/> struct.
        /// </summary>
        /// <param name="color1">The color1.</param>
        /// <param name="color2">The color2.</param>
        /// <param name="factors">The factors.</param>
        /// <param name="positions">The positions.</param>
        /// User:Ryan  CreateTime:2012-8-2 23:16.
        public GradientColor(Color color1, Color color2, float[] factors, float[] positions)
        {
            this.First = color1;
            this.Second = color2;
            this.Factors = factors == null ? new float[] { } : factors;
            this.Positions = positions == null ? new float[] { } : positions;
        }

        /// <summary>
        /// 初始色彩
        /// </summary>
        public Color First;

        /// <summary>
        /// 结束色彩
        /// </summary>
        public Color Second;

        /// <summary>
        /// 色彩渲染系数（0到1的浮点数值）
        /// </summary>
        public float[] Factors;

        /// <summary>
        /// 色彩渲染位置（0到1的浮点数值）
        /// </summary>
        public float[] Positions;

    }

    #endregion
}
