using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TX.Framework.WindowUI
{
    /// <summary>
    /// 系统默认
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-7 22:16.
    internal class SkinThemeDefault : SkinTheme
    {
        public SkinThemeDefault()
            : base()
        {
        }

        /// <summary>
        /// 初始化主题色彩方案
        /// </summary>
        /// User:Ryan  CreateTime:2012-8-7 22:11.
        /// User:Ryan  CreateTime:2012-8-7 22:15.
        public override void IniSkinTheme()
        {
            this.ThemeStyle = EnumTheme.Default;
            this.ThemeName = "系统默认";
            this.ThemeColor = Color.FromArgb(238, 247, 252);
            this.BackGroundImage = Properties.Resources.bg01;
            this.BackGroundImageEnable = false;
            this.BackGroundImageOpacity = 0.8F;
            this.BaseColor = Color.FromArgb(238, 247, 252);
            this.BorderColor = Color.FromArgb(182, 168, 192);
            this.InnerBorderColor = Color.FromArgb(232, 218, 222);
            this.OuterBorderColor = Color.FromArgb(44, 182, 240);
            this.DefaultControlColor = new GradientColor(Color.FromArgb(246, 247, 250), Color.FromArgb(204, 214, 223), null, null);
            this.HeightLightControlColor = new GradientColor(Color.FromArgb(140, 67, 165, 220), Color.FromArgb(67, 165, 220),
                null, null);
            this.FocusedControlColor = new GradientColor(Color.FromArgb(67, 165, 220), Color.FromArgb(39, 88, 142),
                null, null);
            this.UselessColor = Color.FromArgb(159, 159, 159);
            this.CaptionColor = new GradientColor(Color.FromArgb(220, 74, 181, 237), Color.FromArgb(1, 19, 174, 233),
                new float[] { 0.0f, 0.15f, 0.05f, 0.2f, 0.6f, 0.8f, 1f },
                new float[] { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 0.9f, 1.0f });
            this.CaptionFontColor = Color.FromArgb(25, 5, 255);
            this.CaptionFontColor = Color.Black;
            ////control color
            this.ControlBoxDefaultColor = new GradientColor(Color.FromArgb(110, 195, 226), Color.FromArgb(0, 110, 195, 226),
                new float[] { 0f, .1f, .7f, 1f }, new float[] { 0f, .3f, .6f, 1f });
            this.ControlBoxHeightLightColor = new GradientColor(Color.FromArgb(40, 183, 236), Color.FromArgb(0, 40, 183, 236),
                new float[] { 0f, .1f, .7f, 1f }, new float[] { 0f, .3f, .6f, 1f });
            this.ControlBoxPressedColor = new GradientColor(Color.FromArgb(33, 154, 202), Color.FromArgb(0, 33, 154, 202),
                new float[] { 0f, .7f, .2f, 0f }, new float[] { 0f, .3f, .6f, 1f });
            this.CloseBoxHeightLightColor = new GradientColor(Color.FromArgb(219, 85, 55), Color.FromArgb(0, 219, 85, 55),
                new float[] { 0f, .1f, .7f, 1f }, new float[] { 0f, .3f, .6f, 1f });
            this.CloseBoxPressedColor = new GradientColor(Color.FromArgb(167, 78, 58), Color.FromArgb(0, 167, 78, 58),
                new float[] { 0f, .7f, .2f, 0f }, new float[] { 0f, .3f, .6f, 1f });
            this.ControlBoxFlagColor = Color.White;
        }
    }
}
