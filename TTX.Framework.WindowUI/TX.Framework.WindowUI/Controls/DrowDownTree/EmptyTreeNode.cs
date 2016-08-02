using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TX.Framework.WindowUI.Controls
{
    public class EmptyTreeNode : TreeNodeEx
    {
        public EmptyTreeNode()
            : base()
        {
            base.Value = "0";
            base.Text = "...";
            base.LeafNode = true;
            base.Datasource = null;
            this.IsEmptyNode = true;
        }

        public bool IsEmptyNode { get; set; }
    }
}
