using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TX.Framework.WindowUI.Controls
{
    public class DefaultTreeNode : TreeNodeEx
    {
        public DefaultTreeNode()
            : base()
        {
            base.Value = "-1";
            base.Text = "--请选择--";
            base.LeafNode = true;
            base.Datasource = null;
            this.IsDefaultNode = true;
        }

        public DefaultTreeNode(string text)
            : this()
        {
            base.Text = text;
        }

        public bool IsDefaultNode { get; set; }
    }
}
