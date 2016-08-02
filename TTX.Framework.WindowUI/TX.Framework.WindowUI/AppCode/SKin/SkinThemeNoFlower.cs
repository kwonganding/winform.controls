using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TX.Framework.WindowUI
{
    /// <summary>
    /// 主题色彩：如花美眷，似水流年
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-7 22:22.
    internal class SkinThemeNoFlower : SkinTheme
    {
        public SkinThemeNoFlower()
            : base()
        {
        }

        /// <summary>
        /// 初始化主题色彩方案
        /// </summary>
        /// User:Ryan  CreateTime:2012-8-7 22:11.
        /// User:Ryan  CreateTime:2012-8-7 22:22.
        public override void IniSkinTheme()
        {
            this.ThemeStyle = EnumTheme.NoFlower;
            this.ThemeName = "如花美眷，似水流年";
            this.BackGroundImage = Properties.Resources.bg02;
            this.BackGroundImageEnable = false;
            this.BackGroundImageOpacity = 0.8F;
            this.BaseColor = Color.FromArgb(238, 247, 252);
            this.BorderColor = Color.FromArgb(255, 105, 105);
            this.InnerBorderColor = Color.FromArgb(254, 186, 186);
            this.OuterBorderColor = Color.FromArgb(255, 105, 105);
            this.DefaultControlColor = new GradientColor(Color.FromArgb(246, 247, 250), Color.FromArgb(254, 211, 211),
                new float[] { 0.0f, 0.15f, 0.05f, 0.2f, 0.6f, 0.8f, 1f },
                new float[] { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 0.9f, 1.0f });
            this.HeightLightControlColor = new GradientColor(Color.FromArgb(180, 254, 186, 186), Color.FromArgb(255, 105, 105),
                null, null);
            this.FocusedControlColor = new GradientColor(Color.FromArgb(254, 211, 211), Color.FromArgb(255, 105, 105),
                new float[] { 0.0f, 0.15f, 0.05f, 0.2f, 0.6f, 0.8f, 1f },
                new float[] { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 0.9f, 1.0f });
            this.UselessColor = Color.FromArgb(159, 159, 159);
            this.CaptionColor = new GradientColor(Color.FromArgb(180, 255, 101, 102), Color.FromArgb(1, 255, 101, 102),
                new float[] { 0.0f, 0.15f, 0.05f, 0.2f, 0.6f, 0.8f, 1f },
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
