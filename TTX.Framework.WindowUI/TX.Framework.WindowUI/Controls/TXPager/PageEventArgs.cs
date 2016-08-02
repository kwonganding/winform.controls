using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TX.Framework.WindowUI.Controls
{
    /// <summary>
    /// 分页事件源参数（用于分页按钮、查询按钮事件）
    /// </summary>
    public class PagerEventArgs : EventArgs
    {
        public PagerEventArgs(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        /// <value>The size of the page.</value>
        /// User:Ryan  CreateTime:2012-8-14 19:10.
        public int PageSize { get; set; }
    }
}
