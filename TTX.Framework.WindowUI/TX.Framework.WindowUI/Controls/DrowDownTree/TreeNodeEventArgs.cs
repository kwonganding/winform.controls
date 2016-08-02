using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TX.Framework.WindowUI.Controls
{
    /// <summary>
    /// 树节点的事件参数
    /// </summary>
    /// User:Ryan  CreateTime:2012-9-27 18:47.
    public  class TreeNodeEventArgs:EventArgs
    {
        public TreeNodeEventArgs()
            : base()
        {
            this.SelectedNode = null;
            this.DataSource = null;
        }

        public TreeNodeEx SelectedNode { get; set; }

        public object DataSource{get;set;}
    }
}
