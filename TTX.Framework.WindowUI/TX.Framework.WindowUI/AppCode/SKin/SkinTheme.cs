using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TX.Framework.WindowUI
{
    internal class SkinTheme
    {
        #region initialize

        /// <summary>
        /// (构造函数).Initializes a new instance of the <see cref="SkinTheme"/> class.
        /// </summary>
        /// User:Ryan  CreateTime:2012-8-7 22:12.
        public SkinTheme()
        {
            this.IniSkinTheme();
        }

        #endregion

        #region protected methods

        /// <summary>
        /// 初始化主题色彩方案
        /// </summary>
        /// User:Ryan  CreateTime:2012-8-7 22:11.
        public virtual void IniSkinTheme()
        {
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets  the theme style
        /// </summary>
        /// <value>The theme style.</value>
        /// User:Ryan  CreateTime:2012-8-7 22:42.
        public EnumTheme ThemeStyle { get; set; }

        /// <summary>
        /// 主题名称
        /// </summary>
        /// <value>The name of the theme.</value>
        /// User:Ryan  CreateTime:2012-8-4 14:44.
        public string ThemeName { get; set; }

        /// <summary>
        /// 主题背景图片
        /// </summary>
        /// <value>The back ground image.</value>
        /// User:Ryan  CreateTime:2012-8-4 14:44.
        public Bitmap BackGroundImage { get; set; }

        /// <summary>
        /// 主题背景图片透明度
        /// </summary>
        /// <value>The back ground image opacity.</value>
        /// User:Ryan  CreateTime:2012-8-4 14:44.
        public float BackGroundImageOpacity { get; set; }

        /// <summary>
        /// 主题背景图片是否可用
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [back ground image enable]; otherwise, <c>false</c>.
        /// </value>
        /// User:Ryan  CreateTime:2012-8-4 14:45.
        public bool BackGroundImageEnable { get; set; }

        /// <summary>
        /// 基本色彩，用于控件或者区域的背景色
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-28 14:06.
        public Color BaseColor { get; set; }

        /// <summary>
        /// 边框色彩
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-28 14:07.
        public Color BorderColor { get; set; }

        /// <summary>
        /// 内边框色彩
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-28 14:07.
        public Color InnerBorderColor { get; set; }

        /// <summary>
        /// 外边框色彩（用于控件的阴影效果的绘制）
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-28 14:07.
        public Color OuterBorderColor { get; set; }

        /// <summary>
        /// 默认控件色彩
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-28 14:07.
        public GradientColor DefaultControlColor { get; set; }

        /// <summary>
        /// 高亮控件色彩
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-28 14:07.
        public GradientColor HeightLightControlColor { get; set; }

        /// <summary>
        /// 焦点控件色彩
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-28 14:07.
        public GradientColor FocusedControlColor { get; set; }

        /// <summary>
        /// 不可用状态的渲染色彩
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-28 14:07.
        public Color UselessColor { get; set; }

        /// <summary>
        /// 窗口标题栏色彩
        /// Gets or sets the color of the caption.
        /// </summary>
        /// <value>The color of the caption.</value>
        /// User:Ryan  CreateTime:2012-8-2 23:21.
        public GradientColor CaptionColor { get; set; }

        /// <summary>
        /// 窗口标题颜色
        /// </summary>
        /// <value>The color of the caption font.</value>
        /// User:Ryan  CreateTime:2012-8-8 20:06.
        public Color CaptionFontColor { get; set; }

        /// <summary>
        /// 主题颜色（窗体背景色）
        /// Gets or sets the color of the theme.
        /// </summary>
        /// <value>The color of the theme.</value>
        /// User:Ryan  CreateTime:2012-8-3 14:42.
        public Color ThemeColor { get; set; }

        /// <summary>
        /// 窗体关闭按钮高亮颜色
        /// Gets or sets the color of the close box height light.
        /// </summary>
        /// <value>The color of the close box height light.</value>
        /// User:Ryan  CreateTime:2012-8-3 16:53.
        public GradientColor CloseBoxHeightLightColor { get; set; }

        /// <summary>
        /// 窗体关闭按钮按下渲染色彩
        /// Gets or sets the color of the close box pressed.
        /// </summary>
        /// <value>The color of the close box pressed.</value>
        /// User:Ryan  CreateTime:2012-8-3 17:06.
        public GradientColor CloseBoxPressedColor { get; set; }

        /// <summary>
        /// 窗体控制按钮默认色彩
        /// Gets or sets  the control box default colo
        /// </summary>
        /// <value>The control box default colo.</value>
        /// User:Ryan  CreateTime:2012-8-3 17:06.
        public GradientColor ControlBoxDefaultColor { get; set; }

        /// <summary>
        /// 窗体控制按钮高亮渲染色彩
        /// Gets or sets the color of the control box height light.
        /// </summary>
        /// <value>The color of the control box height light.</value>
        /// User:Ryan  CreateTime:2012-8-3 17:06.
        public GradientColor ControlBoxHeightLightColor { get; set; }

        /// <summary>
        /// 窗体控制按钮按下渲染色彩
        /// Gets or sets the color of the control box pressed.
        /// </summary>
        /// <value>The color of the control box pressed.</value>
        /// User:Ryan  CreateTime:2012-8-3 17:06.
        public GradientColor ControlBoxPressedColor { get; set; }

        /// <summary>
        /// 窗体按钮图案标记的颜色
        /// Gets or sets the color of the control box flag.
        /// </summary>
        /// <value>The color of the control box flag.</value>
        /// User:Ryan  CreateTime:2012-8-3 22:20.
        public Color ControlBoxFlagColor { get; set; }

        #endregion
    }
}
