using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.Reflection;

namespace TX.Framework.WindowUI.Controls
{
    /// <summary>
    /// 树形多级选择控件
    /// </summary>
    /// User:Ryan  CreateTime:2012-9-27 22:20.
    [ToolboxBitmap(typeof(ComboBox))]
    [ToolboxItem(true)]
    public partial class TXTreeComboBox : TXPopupComboBox
    {
        #region private attributes

        /// <summary>
        /// 被勾选的节点集合
        /// </summary>
        private List<TreeNodeEx> _CheckedNodes = new List<TreeNodeEx>();

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        private bool _CheckBox = false;

        /// <summary>
        /// 设置树的最大深度，0表示无限深度
        /// </summary>
        private int _TreeMaxDegree = 0;

        #endregion

        #region protect attributes

        /// <summary>
        /// 弹出树控件
        /// </summary>
        protected TreeView PopDownTree;

        #endregion

        #region IniControls

        public TXTreeComboBox()
            : base()
        {
            //load tree control
            TreeComboBoxContainer container = new TreeComboBoxContainer();
            DropDownControl = container;
            PopupDropDown.Resizable = true;
            this.PopDownTree = new TreeView();
            this.IniTree();
            container.Controls.Add(this.PopDownTree);
            //initialize default value
            this.IsInsertDefaultNode = true;
            this.DoubleSelecteEnable = true;
            this.DoubleSelectNodeLevel = 0;
            this.DefaultNodeText = string.Empty;
            this.MultiLevelDataSourceMember = string.Empty;
            this.ShowFullPathText = true;
            //bind events
            this.Click += new EventHandler(TXTreeComboBox_Click);
            this.PopDownTree.BeforeExpand += new TreeViewCancelEventHandler(PopDownTree_BeforeExpand);
            this.PopDownTree.AfterSelect += new TreeViewEventHandler(PopDownTree_AfterSelect);
            this.PopDownTree.AfterCheck += new TreeViewEventHandler(PopDownTree_AfterCheck);
            this.PopDownTree.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(PopDownTree_NodeMouseDoubleClick);
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置是否插入默认节点
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-27 17:34.
        [Browsable(true)]
        [Description("获取或设置是否插入默认节点")]
        public bool IsInsertDefaultNode { get; set; }

        /// <summary>
        /// 获取或者设置是否启用双击选中
        /// </summary>
        /// User:Ryan  CreateTime:2012-10-11 20:25.
        [Browsable(true)]
        [Description("获取或者设置是否启用双击选中")]
        public bool DoubleSelecteEnable { get; set; }

        /// <summary>
        /// 获取或者设置是否启用双击选中的节点级数
        /// </summary>
        /// User:Ryan  CreateTime:2012-10-11 20:25.
        [Browsable(true)]
        [DefaultValue(0)]
        [Description("获取或者设置是否启用双击选中的节点级数")]
        public int DoubleSelectNodeLevel { get; set; }

        [Browsable(true)]
        [Description("设置树的最大深度，0表示无限深度")]
        public int TreeMaxDegree
        {
            get { return this._TreeMaxDegree; }
            set { this._TreeMaxDegree = value < 0 ? 0 : value; }
        }

        /// <summary>
        /// 默认节点文本值
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-27 17:34.
        [Browsable(true)]
        [Description("默认节点文本值")]
        public string DefaultNodeText { get; set; }

        /// <summary>
        /// 绑定多级节点数据源成员名称
        /// </summary>
        /// <value>The multi level data source member.</value>
        /// User:Ryan  CreateTime:2012-9-27 17:35.
        [Browsable(true)]
        [Description("绑定多级节点数据源成员名称")]
        public string MultiLevelDataSourceMember { get; set; }

        /// <summary>
        /// 设置是否显示全路径文本
        /// </summary>
        /// <value><c>true</c> if [show full path text]; otherwise, <c>false</c>.</value>
        /// User:Ryan  CreateTime:2012-9-27 18:43.
        [Browsable(true)]
        [Description("设置是否显示全路径文本")]
        public bool ShowFullPathText { get; set; }

        /// <summary>
        /// 设置是否显示checkbox
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-27 21:20.
        [Browsable(true)]
        [Description("设置是否显示checkbox")]
        public bool CheckBox
        {
            get
            {
                return this.PopDownTree.CheckBoxes;
            }
            set
            {
                this._CheckBox = value;
                this.PopDownTree.CheckBoxes = this._CheckBox;
            }
        }

        /// <summary>
        /// 获取或设置多级路径分割字符串
        /// </summary>
        /// <value>The path separator.</value>
        /// User:Ryan  CreateTime:2012-9-28 20:23.
        [Browsable(true)]
        [Description("获取或设置多级路径分割字符串")]
        public string PathSeparator
        {
            get { return this.PopDownTree.PathSeparator; }
            set { this.PopDownTree.PathSeparator = value; }
        }

        /// <summary>
        /// 设置或者获取选中的节点
        /// </summary>
        /// <value>The selected node.</value>
        /// User:Ryan  CreateTime:2012-9-27 19:18.
        [Browsable(false)]
        public TreeNodeEx SelectedNode
        {
            get
            {
                return this.PopDownTree.SelectedNode as TreeNodeEx;
            }
            set
            {
                if (value != null)
                {
                    this.PopDownTree.SelectedNode = value;
                    //this.UpdateText(value);
                }
            }
        }

        /// <summary>
        /// 获取或者设置选中节点的值
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-27 19:18.
        [Browsable(false)]
        public new string SelectedValue
        {
            get
            {
                return this.SelectedNode == null ? string.Empty : this.SelectedNode.Value;
            }
            set
            {
                this.SetSelectedNodeByValue(value);
            }
        }

        /// <summary>
        /// 获取选中节点的文本
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-27 19:20.
        [Browsable(false)]
        public new string SelectedText
        {
            get
            {
                return this.SelectedNode == null ? string.Empty : this.SelectedNode.Text;
            }
        }

        /// <summary>
        /// 获取选中节点的全路径值数组
        /// 注意：节点数值是从根节点开始的。
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-27 19:26.
        [Browsable(false)]
        public string[] SelectedValues
        {
            get
            {
                List<string> values = this.SelectedNode == null ? null : this.GetFullPathValues(this.SelectedNode);
                if (values != null)
                {
                    values.Reverse();
                    return values.ToArray();
                }
                else
                {
                    return null;
                }

            }
        }

        /// <summary>
        /// 获取选中节点的全路径文本
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-27 19:26.
        [Browsable(false)]
        public string SelectedTexts
        {
            get { return this.SelectedNode == null ? string.Empty : this.SelectedNode.FullPath; }
        }


        /// <summary>
        /// 获取被勾选的节点集合
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-27 21:37.
        [Browsable(false)]
        public List<TreeNodeEx> CheckedNodes
        {
            get
            {
                return this._CheckedNodes;
            }
        }

        /// <summary>
        /// 获取被勾选节点的值集合
        /// </summary>
        /// <value>The checked values.</value>
        /// User:Ryan  CreateTime:2012-9-27 22:01.
        [Browsable(false)]
        public string[] CheckedValues
        {
            get
            {
                List<string> values = new List<string>();
                if (this._CheckedNodes != null && this._CheckedNodes.Count > 0)
                {
                    this._CheckedNodes.ForEach(s => values.Add(s.Value));
                    return values.ToArray();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.SetCheckedNodeByValues(value);
                //this.UpdateText(null);
            }
        }

        /// <summary>
        /// 被选中节点的文本数据
        /// </summary>
        /// <value>The checked texts.</value>
        /// User:Ryan  CreateTime:2012-9-28 20:21.
        [Browsable(false)]
        public string CheckedTexts
        {
            get { return this.GetCheckedTexts(); }
        }

        #endregion

        #region Events

        /// <summary>
        /// 加载子节点数据事件
        /// </summary>
        [Browsable(true)]
        public event EventHandler<TreeNodeEventArgs> LoadChildNodesDataSource;

        /// <summary>
        /// 当节点被选中时发生
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-27 18:48.
        public event EventHandler<TreeNodeEventArgs> OnTreeNodeSelected;

        /// <summary>
        /// 控件框点击事件：弹出选择树
        /// </summary>
        /// <param name="e"></param>
        protected virtual void TXTreeComboBox_Click(object sender, EventArgs e)
        {
            base.ShowDropDown();
        }

        /// <summary>
        /// 节点双击事件：隐藏下拉框
        /// </summary>
        private void PopDownTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.DoubleSelecteEnable && (e.Node.Level + 1) >= this.DoubleSelectNodeLevel)
            {
                this.HideDropDown();
            }
        }

        /// <summary>
        /// 节点选中后的事件
        /// </summary>/// User:Ryan  CreateTime:2012-9-27 20:15.
        void PopDownTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.SetSelectedNode(e.Node as TreeNodeEx);
        }

        /// <summary>
        /// 节点选中后的事件
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-28 15:42.
        void PopDownTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNodeEx node = e.Node as TreeNodeEx;
            if (node != null)
            {
                if (node is DefaultTreeNode)
                {
                    return;
                }
                else
                {
                    //add
                    if (node.Checked)
                    {
                        this._CheckedNodes.Add(node);
                    }
                    //remove
                    else
                    {
                        this._CheckedNodes.Remove(node);
                    }

                    //update text
                    this.UpdateText(node);
                }
            }
        }

        #region PopDownTree_BeforeExpand（树节点展开处理事件）

        /// <summary>
        /// 树节点展开处理事件
        /// </summary>
        /// <param name="sender">(控件对象).The source of the event.</param>
        /// <param name="e">(事件数据).The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        /// User:Ryan  CreateTime:2012-9-27 18:50.
        void PopDownTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeEx node = e.Node as TreeNodeEx;
            if (!node.LeafNode && node.Datasource == null && node.Nodes[0] != null && node.Nodes[0] is EmptyTreeNode)
            {
                //clear data
                node.LeafNode = true;
                node.Nodes.Clear();
                //实现该事件以加载子节点数据
                if (this.LoadChildNodesDataSource != null)
                {
                    TreeNodeEventArgs args = new TreeNodeEventArgs();
                    args.SelectedNode = node;
                    this.LoadChildNodesDataSource(sender, args);
                    //bind data
                    this.BindChildNode(node, args.DataSource);
                }
            }
        }
        #endregion

        #endregion

        #region Override methods

        protected override void OnResize(EventArgs e)
        {
            if (DropDownControl != null)
            {
                Size Size = new Size(Width, DropDownControl.Height + 20);
                PopupDropDown.Size = Size;
            }
            base.OnResize(e);
        }
        #endregion

        #region Virtual methods

        #region  BindData

        /// <summary>
        /// 初始绑定数据源
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-27 17:36.
        public virtual void BindData()
        {
            //1.validation parameters
            if (this.DataSource == null || string.IsNullOrEmpty(this.ValueMember) || string.IsNullOrEmpty(this.DisplayMember))
            {
                return;
            }
            //2.get datasouce
            IEnumerable source = this.GetSource(this.DataSource);
            if (source == null)
            {
                throw new ArgumentException("数据源必须是支持枚举器的列表数据！");
            }

            //3.bind data
            //3.0 clear items
            this.PopDownTree.Nodes.Clear();
            this.Text = "";
            //3.1 bind default node
            if (this.IsInsertDefaultNode)
            {
                DefaultTreeNode node = new DefaultTreeNode();
                node.Text = string.IsNullOrEmpty(this.DefaultNodeText) ? node.Text : this.DefaultNodeText;
                this.PopDownTree.Nodes.Add(node);
                this.SetSelectedNode(node);
            }
            //3.2 bind tree nodes
            foreach (object item in source)
            {
                this.BindRootNode(item);
            }
        }
        #endregion

        #region BindRootNode

        /// <summary>
        /// 绑定一级节点
        /// </summary>
        /// <param name="item">The item.</param>
        /// User:Ryan  CreateTime:2012-9-27 17:39.
        protected virtual void BindRootNode(object item)
        {
            TreeNodeEx node = this.GetRootNode(item);
            this.PopDownTree.Nodes.Add(node);
            //绑定子节点
            this.BindChildNode(item, node);
        }

        protected virtual TreeNodeEx GetRootNode(object item)
        {
            return new TreeNodeEx(this, item, this.TreeMaxDegree == 1 ? true : false);
        }

        #endregion

        #region BindChildNode 绑定子节点（自反射数据源）

        /// <summary>
        /// 绑定子节点（自反射数据源）
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="node">The node.</param>
        /// User:Ryan  CreateTime:2012-9-27 17:39.
        protected virtual void BindChildNode(object item, TreeNodeEx node)
        {
            //绑定多级节点
            if (!string.IsNullOrEmpty(this.MultiLevelDataSourceMember))
            {
                object dataSource = item.TryGetValue(this.MultiLevelDataSourceMember);
                if (dataSource != null)
                {
                    this.BindChildNode(node, dataSource);
                }
            }
        }
        #endregion

        #region BindChildNode 绑定子节点（指定数据源）

        /// <summary>
        /// BindChildNode 绑定子节点（指定数据源）
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="dataSource">The data source.</param>
        /// User:Ryan  CreateTime:2012-9-27 17:40.
        protected virtual void BindChildNode(TreeNodeEx node, object dataSource)
        {
            //验证绑定树的深度：如果TreeMaxDegree为0标示无限级数，其它只绑定相应级数的数据
            if (node != null && dataSource != null && (this.TreeMaxDegree == 0 || node.Level < this.TreeMaxDegree - 1))
            {
                node.Datasource = dataSource;
                //get data source
                IEnumerable source = this.GetSource(dataSource);
                if (source == null)
                {
                    throw new ArgumentException("数据源必须是支持枚举器的列表数据！");
                }
                //bind data
                node.Nodes.Clear();
                foreach (object item in source)
                {
                    TreeNodeEx childNode = this.GetChildNode(node, item);
                    node.Nodes.Add(childNode);
                    this.BindChildNode(item, childNode);
                }
            }
        }

        protected virtual TreeNodeEx GetChildNode(TreeNodeEx node, object item)
        {
            return new TreeNodeEx(this, item, node.Level == this.TreeMaxDegree - 2 ? true : false);
        }

        #endregion

        #region GetSource（获取数据源的枚举器）

        /// <summary>
        /// GetSource（获取数据源的枚举器）
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <returns>
        /// Return a data(or instance) of IEnumerable.
        /// </returns>
        /// User:Ryan  CreateTime:2012-9-27 17:41.
        protected virtual IEnumerable GetSource(object dataSource)
        {
            if (dataSource == null)
                return null;
            IEnumerable source = dataSource as IEnumerable;
            return source == null ? null : source;
        }
        #endregion

        #endregion

        #region Public Methods

        #region 清除所有数据（Clear）

        /// <summary>
        /// 清除所有数据（Clear）
        /// </summary>
        public void Clear()
        {
            this.PopDownTree.Nodes.Clear();
        }
        #endregion

        #endregion

        #region private methods

        #region IniTree（初始化树）

        /// <summary>
        ///  IniTree（初始化树）
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-27 10:49.
        private void IniTree()
        {
            this.PopDownTree.FullRowSelect = true;
            this.PopDownTree.ItemHeight = 18;
            this.PopDownTree.Dock = DockStyle.Fill;
            this.PopDownTree.HideSelection = false;
            this.PopDownTree.ShowLines = true;
            this.PopDownTree.BorderStyle = BorderStyle.None;
            this.PopDownTree.LineColor = SkinManager.CurrentSkin.HeightLightControlColor.First;
            this.PopDownTree.PathSeparator = " ";
        }
        #endregion

        #region GetFullPathValues

        /// <summary>
        /// 获取指定接的的全部值
        /// </summary>
        /// User:Ryan  CreateTime:2012-9-27 19:25.
        private List<string> GetFullPathValues(TreeNodeEx node)
        {
            List<string> list = new List<string>();
            list.Add(node.Value);
            if (node.Parent != null)
            {
                list.AddRange(this.GetFullPathValues(node.Parent as TreeNodeEx));
            }

            return list;
        }
        #endregion

        #region SetSelectedNode

        /// <summary>
        /// 设置当即选中树节点
        /// </summary>
        /// <param name="node">The node.</param>
        /// User:Ryan  CreateTime:2012-9-27 20:14.
        private void SetSelectedNode(TreeNodeEx node)
        {
            this.UpdateText(node);
            if (node is DefaultTreeNode)
            {
                this.SelectedNode = null;
            }
            else
            {
                this.SelectedNode = node;
                if (this.OnTreeNodeSelected != null)
                {
                    TreeNodeEventArgs args = new TreeNodeEventArgs();
                    args.SelectedNode = this.SelectedNode;
                    this.OnTreeNodeSelected(this.PopDownTree, args);
                }
            }
        }
        #endregion

        #region SetSelectedNodeByValue（根据值设置选中节点）

        private void SetSelectedNodeByValue(string value)
        {
            if (this.PopDownTree.Nodes != null && this.PopDownTree.Nodes.Count > 0)
            {
                foreach (TreeNodeEx node in this.PopDownTree.Nodes)
                {
                    this.SetSelectedNodeByValue(value, node);
                }
            }
        }

        private void SetSelectedNodeByValue(string value, TreeNodeEx node)
        {
            if (node == null)
            {
                return;
            }

            if (node.Value.Equals(value))
            {
                //this.SelectedNode = node;
                this.SetSelectedNode(node);
                return;
            }
            else
            {
                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    foreach (TreeNodeEx item in node.Nodes)
                    {
                        this.SetSelectedNodeByValue(value, item);
                    }
                }
            }
        }
        #endregion

        #region GetCheckedNodes（获取被勾选的节点集合）

        private List<TreeNodeEx> GetCheckedNodes()
        {
            if (this.PopDownTree.CheckBoxes && this.PopDownTree.Nodes != null && this.PopDownTree.Nodes.Count > 0)
            {
                List<TreeNodeEx> list = new List<TreeNodeEx>();
                foreach (TreeNodeEx item in this.PopDownTree.Nodes)
                {
                    list.AddRange(this.GetCheckedNodes(item));
                }

                return list;
            }
            else
            {
                return null;
            }
        }

        private List<TreeNodeEx> GetCheckedNodes(TreeNodeEx node)
        {
            if (node != null && this.PopDownTree.CheckBoxes)
            {
                List<TreeNodeEx> list = new List<TreeNodeEx>();
                if (node.Checked)
                {
                    list.Add(node);
                }

                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    list.AddRange(this.GetCheckedNodes(node));
                }

                return list;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region GetCheckedTexts（获取被勾选节点文本字符）

        private string GetCheckedTexts()
        {
            StringBuilder sb = new StringBuilder();
            if (this._CheckedNodes != null && this._CheckedNodes.Count > 0)
            {
                this._CheckedNodes.ForEach(s => sb.Append(string.Concat(this.PopDownTree.PathSeparator, s.Text)));
            }
            return sb.ToString();
        }
        #endregion

        #region SetCheckedNodeByValues（根据值设置被勾选的节点）

        private void SetCheckedNodeByValues(string[] values)
        {
            if (values == null || values.Length <= 0)
            {
                return;
            }

            if (this.PopDownTree.Nodes != null && this.PopDownTree.Nodes.Count > 0)
            {
                foreach (TreeNodeEx node in this.PopDownTree.Nodes)
                {
                    this.SetCheckedNodeByValues(values, node);
                }
            }
        }


        private void SetCheckedNodeByValues(string[] values, TreeNodeEx node)
        {
            if (node == null)
            {
                return;
            }

            if (values.Contains(node.Value))
            {
                node.Checked = true;
            }
            else
            {
                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    foreach (TreeNodeEx item in node.Nodes)
                    {
                        this.SetCheckedNodeByValues(values, item);
                    }
                }
            }
        }
        #endregion

        #region UpdateText（更新文本框显示的文本）

        /// <summary>
        /// 更新文本框显示的文本
        /// </summary>
        /// <param name="node">The node.</param>
        /// User:Ryan  CreateTime:2012-9-27 21:58.
        private void UpdateText(TreeNodeEx node)
        {
            if (node == null)
            {
                return;
            }

            if (this.CheckBox)
            {
                this.Text = this.GetCheckedTexts();
            }
            else
            {
                if (this.ShowFullPathText)
                {
                    this.Text = node.FullPath;
                }
                else
                {
                    this.Text = node.Text;
                }
            }
        }
        #endregion

        #endregion
    }
}
