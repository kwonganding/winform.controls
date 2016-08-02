using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TX.Framework.WindowUI
{
    /// <summary>
    /// 主题色彩：夕阳西下，明月天涯
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-7 22:22.
    internal class SkinThemeSunsetRed : SkinTheme
    {
        public SkinThemeSunsetRed()
            : base()
        {
        }

        /// <summary>
        /// 初始化主题色彩方案
        /// </summary>
        /// User:Ryan  CreateTime:2012-8-7 22:11.
        /// User:Ryan  CreateTime:2012-8-7 22:19.
        public override void IniSkinTheme()
        {
            this.ThemeStyle = EnumTheme.SunsetRed;
            this.ThemeName = "夕阳西下，明月天涯";
            this.BackGroundImage = Properties.Resources.bg05;
            this.BackGroundImageEnable = false;
            this.BackGroundImageOpacity = 0.8F;
            this.BaseColor = Color.FromArgb(238, 247, 252);
            this.BorderColor = Color.FromArgb(254, 196, 0);
            this.InnerBorderColor = Color.FromArgb(227, 227, 227);
            this.OuterBorderColor = Color.FromArgb(174, 3, 123);
            this.DefaultControlColor = new GradientColor(Color.FromArgb(248, 245, 251), Color.FromArgb(255, 225, 193),
                new float[] { 0.0f, 0.15f, 0.05f, 0.2f, 0.6f, 0.8f, 0.85f },
                new float[] { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 0.9f, 1.0f });
            this.HeightLightControlColor = new GradientColor(Color.FromArgb(254, 196, 0), Color.FromArgb(252, 105, 0),
                new float[] { 0.0F, 0.7F, 1.5F }, new float[] { 0.0F, 0.6F, 1F });
            this.FocusedControlColor = new GradientColor(Color.FromArgb(252, 105, 0), Color.FromArgb(254, 196, 0),
                new float[] { 0.2F, 0.6F, 0.8F, 0.4F, 0.2F }, new float[] { 0.0F, 0.3F, 0.6F, 0.8F, 1F });
            this.UselessColor = Color.FromArgb(159, 159, 159);
            this.CaptionColor = new GradientColor(Color.FromArgb(240, 255, 199, 140), Color.FromArgb(1, 255, 199, 140),
                new float[] { 0.0f, 0.15f, 0.05f, 0.2f, 0.6f, 0.8f, 0.85f },
                new float[] { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 0.9f, 1.0f });
            this.ThemeColor = Color.FromArgb(238, 247, 252);
            this.CaptionFontColor = Color.FromArgb(25, 5, 255);
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
