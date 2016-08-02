using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls
{
    /// <summary>
    /// 常用操作工具条的按钮事件
    /// </summary>
    /// User:Ryan  CreateTime:2011-08-19 11:31.
    public class TXToolBarEventArgs : EventArgs
    {
        private ListViewItem _Item;

        private List<ListViewItem> _Items;

        public TXToolBarEventArgs()
            : base()
        {
            this._Item = null;
            this._Items = null;
        }

        /// <summary>
        /// 当前选中的项的第一项（包括selected和checked），selected优先
        /// </summary>
        /// <value>The selected item.</value>
        /// User:Ryan  CreateTime:2011-08-19 11:28.
        public ListViewItem SelectedItem
        {
            get { return this._Item; }
            set { this._Item = value; }
        }

        /// <summary>
        /// 当前选中的项的集合（主要是checked为true的，即列表需设置了checkbox选项），若没有checked的项，则取selected的项
        /// </summary>
        /// <value>The selected items.</value>
        /// User:Ryan  CreateTime:2011-08-19 11:28.
        public List<ListViewItem> SelectedItems
        {
            get { return this._Items; }
            set { this._Items = value; }
        }
    }
}
