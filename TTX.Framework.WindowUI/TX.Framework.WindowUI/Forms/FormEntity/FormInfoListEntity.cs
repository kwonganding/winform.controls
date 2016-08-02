using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Forms
{
    public partial class FormInfoListEntity : FormListEntity
    {
        public FormInfoListEntity()
        {
            InitializeComponent();
        }
        private void FormInfoListEntity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.pager.OnSearchClick(sender, e);
            }
        }

        #region protected methods

        #region check

        /// <summary>
        /// 设置列表全选，必须列表支持复选框才有效
        /// </summary>
        /// <param name="sender">(控件对象).The source of the event.</param>
        /// <param name="e">(事件数据).The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// User:Ryan  CreateTime:2011-08-19 11:13.
        protected virtual void toolBar_CheckAll(object sender, EventArgs e)
        {
            if (tlvList.CheckBoxes && tlvList.Items.Count > 0)
            {
                foreach (ListViewItem item in tlvList.Items)
                {
                    item.Checked = true;
                }
            }
        }

        /// <summary>
        /// 设置列表反选，必须列表支持复选框才有效
        /// </summary>
        /// <param name="sender">(控件对象).The source of the event.</param>
        /// <param name="e">(事件数据).The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// User:Ryan  CreateTime:2011-08-19 11:13.
        protected virtual void toolBar_CheckInvert(object sender, EventArgs e)
        {
            if (tlvList.CheckBoxes && tlvList.Items.Count > 0)
            {
                foreach (ListViewItem item in tlvList.Items)
                {
                    item.Checked = item.Checked ? false : true;
                }
            }
        }
        #endregion

        #endregion

    }
}
