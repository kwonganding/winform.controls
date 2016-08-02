using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TX.Framework.WindowUI.Controls;

namespace TX.Framework.WindowUI.Forms
{
    internal partial class frmSkinManager : BaseForm
    {
        public frmSkinManager()
        {
            InitializeComponent();
            ControlHelper.BindMouseMoveEvent(this.panelControlArea);
        }

        #region events

        private void frmSkinManager_Load(object sender, EventArgs e)
        {
            //bind data
            this.BindTheme();
        }

        protected virtual void OnBtnOkClick(object sender, EventArgs e)
        {
            this.SaveTheme();
            this.ApplyTheme();
            this.DialogResult = DialogResult.OK;
        }

        protected virtual void OnBtnCancelClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.SaveTheme();
            this.ApplyTheme();
        }

        #endregion

        #region private methods

        private void ApplyTheme()
        {
            foreach (Form item in Application.OpenForms)
            {
                item.Invalidate();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// User:Ryan  CreateTime:2012-8-7 23:06.
        private void BindTheme()
        {
            ////bind default theme
            cbBgEnable.Checked = SkinManager.CurrentSkin.BackGroundImageEnable;
            picBoxBg.BackgroundImage = SkinManager.CurrentSkin.BackGroundImage;
            trackOpacity.Value = (int)(SkinManager.CurrentSkin.BackGroundImageOpacity * 100);
            switch (SkinManager.CurrentSkin.ThemeStyle)
            {
                case EnumTheme.Default:
                    rbn0.Checked = true;
                    break;
                case EnumTheme.BlueSea:
                    rbn1.Checked = true;
                    break;
                case EnumTheme.KissOfAngel:
                    rbn2.Checked = true;
                    break;
                case EnumTheme.NoFlower:
                    rbn3.Checked = true;
                    break;
                case EnumTheme.SunsetRed:
                    rbn4.Checked = true;
                    break;
            }
        }

        /// <summary>
        /// 保存主题
        /// </summary>
        /// User:Ryan  CreateTime:2012-8-7 23:06.
        private void SaveTheme()
        {
            int i = rbn0.Checked ? 0 : rbn1.Checked ? 1 : rbn2.Checked ? 2 : rbn3.Checked ? 3 : rbn4.Checked ? 4 : 0;
            SkinManager.SettingSkinTeme(i.ToEnumByValue<EnumTheme>());
            SkinManager.CurrentSkin.BackGroundImageEnable = cbBgEnable.Checked;
            SkinManager.CurrentSkin.BackGroundImageOpacity = trackOpacity.Value / 100F;
            SkinManager.CurrentSkin.BackGroundImage = (Bitmap)picBoxBg.BackgroundImage;
            SkinManager.Save();
        }

        /// <summary>
        /// 修改主题图片
        /// </summary>
        /// <param name="sender">(控件对象).The source of the event.</param>
        /// <param name="e">(事件数据).The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// User:Ryan  CreateTime:2012-8-6 12:22.
        private void picBoxBg_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "(所有文件)|*.*|(jpg图片)|*.jpg|(jpeg)|*.jpeg|(gif图片)|*.gif";
            fd.Multiselect = false;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                picBoxBg.BackgroundImage = Image.FromFile(fd.FileName);
            }
        }

        #endregion

        private void rbn_CheckedChanged(object sender, EventArgs e)
        {
            TXRadioButton rad = (TXRadioButton)sender;
            if (rad != null)
            {
                
            }
        }
    }
}
