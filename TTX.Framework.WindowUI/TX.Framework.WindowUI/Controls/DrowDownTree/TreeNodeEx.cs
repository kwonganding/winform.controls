using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls
{
    /// <summary>
    /// 扩展的树节点
    /// </summary>
    /// User:Ryan  CreateTime:2012-9-27 14:14.
    public class TreeNodeEx : TreeNode
    {
        private TXTreeComboBox Owner;

        public TreeNodeEx()
            : base()
        {
            this.Value = string.Empty;
            this.Datasource = null;
            this.LeafNode = true;
        }

        public TreeNodeEx(TXTreeComboBox owner, object obj,bool leafNode)
            : this()
        {
            this.Owner = owner;
            this.Tag = obj;
            this.LeafNode = leafNode;
            //bind data
            this.Text = obj.TryGetPropertyValue(this.Owner.DisplayMember).ToString();
            this.Value = obj.TryGetPropertyValue(this.Owner.ValueMember).ToString();
            if (!leafNode)
            {
                this.Nodes.Add(new EmptyTreeNode());
            }
        }

        public string Value { get; set; }

        public object Datasource { get; set; }

        public bool LeafNode { get; set; }
    }
}
