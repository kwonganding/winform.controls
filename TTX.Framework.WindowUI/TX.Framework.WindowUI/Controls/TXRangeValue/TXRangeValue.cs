using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls
{
    /// <summary>
    /// 范围设置控件
    /// </summary>
    /// User:Ryan  CreateTime:2012-11-19 15:42.
    [ToolboxItem(true)]
    public partial class TXRangeValue : UserControl
    {
        #region private attributes

        /// <summary>
        /// row
        /// </summary>
        private int _RowIndex;

        /// <summary>
        /// 范围上限”输入框
        /// </summary>
        private List<TXTextBox> _TxtLowerLimit;

        /// <summary>
        /// 范围下限”输入框
        /// </summary>
        private List<TXTextBox> _TxtUpperLimit;

        /// <summary>
        /// 数据输入框
        /// </summary>
        private List<TXTextBox> _TxtValue;

        private RangeValueHeader _Header;

        private RangeValueCollection _RangeValues;

        private bool _EditEnable;

        #endregion

        public TXRangeValue()
        {
            InitializeComponent();
            this._RangeValues = new RangeValueCollection();
            this._Header = new RangeValueHeader();
            this._Header.OnTitleChanged = this.HeaderValueChanged;
            this.Text = "范围值设置：";
            this._EditEnable = true;
            this.ErrorMessage = string.Empty;
            this.IsValid = true;
            this.TitleRender();
            this.IniControls();
        }

        #region Properties

        /// <summary>
        /// 区域数据源(RangeValues)
        /// </summary>
        /// <value></value>
        /// User:Ryan  CreateTime:2012-11-19 15:42.
        public RangeValueCollection RangeValues
        {
            get
            {
                return this.GetRangeValues();
            }
            set
            {
                if (value != null)
                {
                    this._RangeValues = value;
                    this.BindData();
                }
            }
        }

        /// <summary>
        /// 错误信息(ErrorMessage)
        /// </summary>
        /// <value></value>
        /// User:Ryan  CreateTime:2012-11-19 18:09.
        [Browsable(false)]
        public string ErrorMessage { get; protected set; }

        /// <summary>
        /// 输入是否合法
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        /// User:Ryan  CreateTime:2012-11-19 18:09.
        [Browsable(false)]
        public bool IsValid { get; protected set; }

        /// <summary>
        ///是否允许编辑
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-19 15:45.
        public bool EditEnable
        {
            get { return this._EditEnable; }
            set
            {
                this._EditEnable = value;
                this.linkAdd.Enabled = value;
            }
        }

        /// <summary>
        /// 标题项(Header)
        /// </summary>
        /// <value></value>
        /// User:Ryan  CreateTime:2012-11-19 16:49.
        [Browsable(true)]
        public RangeValueHeader Header
        {
            get { return this._Header; }
            set
            {
                if (value != null)
                {
                    this._Header = value;
                }
            }
        }

        [Browsable(true)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.TitleRender();
            }
        }

        #endregion

        #region private events

        private void HeaderValueChanged(object obj, EventArgs args)
        {
            this.TitleRender();
        }

        private void linkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.AddItem(new RangeValueItem());
        }

        #endregion

        #region Public methods

        #region GetRangeValues

        public RangeValueCollection GetRangeValues()
        {
            List<RangeValueItem> list = new List<RangeValueItem>();
            string mes = string.Empty;
            if (this.TryParseInput(out mes, list))
            {
                this.IsValid = true;
                return new RangeValueCollection(list);
            }
            else
            {
                this.IsValid = false;
                this.ErrorMessage = mes;
                return null;
            }
        }
        #endregion

        #endregion

        #region private methods

        #region TitleRender

        /// <summary>
        /// 标题的界面刷新
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-19 15:40.
        private void TitleRender()
        {
            this.labLower.Text = this.Header.LowerValueTitle;
            this.labUpper.Text = this.Header.UpperValueTitle;
            this.labValue.Text = this.Header.ValueTitle;
            this.labTitle.Text = this.Text;
        }
        #endregion

        #region IniControls

        /// <summary>
        /// 初始化控件的值（Inis the controls.）
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-19 15:48.
        public void IniControls()
        {
            this._RowIndex = 0;
            this._TxtLowerLimit = new List<TXTextBox>();
            this._TxtUpperLimit = new List<TXTextBox>();
            this._TxtValue = new List<TXTextBox>();
            this._TxtLowerLimit.Clear();
            this._TxtUpperLimit.Clear();
            this._TxtValue.Clear();
            this.tlpItems.Controls.Clear();
            this.tlpItems.RowStyles.Clear();
            this.tlpItems.RowCount = 2;
            this.tlpItems.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            this.tlpItems.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }
        #endregion

        #region AddItem

        private void AddItem(RangeValueItem item)
        {
            //1. validation
            if (item == null)
            {
                return;
            }
            RowStyle rs = new RowStyle(SizeType.Absolute, 30F);
            tlpItems.RowStyles.Insert(this._RowIndex, rs);
            var txt1 = this.CreateTextBox(item.LowerValue.ToString("F2"));
            this._TxtLowerLimit.Add(txt1);
            tlpItems.Controls.Add(txt1, 0, this._RowIndex);
            var txt2 = this.CreateTextBox(item.UpperValue.ToString("F2"));
            this._TxtUpperLimit.Add(txt2);
            tlpItems.Controls.Add(txt2, 1, this._RowIndex);
            var txt3 = this.CreateTextBox(item.Value.ToString("F2"));
            this._TxtValue.Add(txt3);
            tlpItems.Controls.Add(txt3, 2, this._RowIndex);
            ////button
            var btn = this.CreateDelLinkBtn(this._RowIndex);
            tlpItems.Controls.Add(btn, 3, this._RowIndex);
            ////Scroll
            tlpItems.RowCount++;
            tlpItems.Width -= 1;
            tlpItems.Refresh();
            this._RowIndex++;
        }
        #endregion

        #region BindData

        private void BindData()
        {
            this.IniControls();
            if (this._RangeValues != null && this._RangeValues.Count > 0)
            {
                //sort:order by lower value
                this._RangeValues.Sort();
                foreach (var item in this._RangeValues)
                {
                    this.AddItem(item);
                }
            }
        }
        #endregion

        #region TryParseInput

        /// <summary>
        /// 验证读取优惠设置信息,true:验证成功，输入合法；false：验证失败，输入不合法
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="list">The list.</param>
        /// <returns>Return a object of System.Boolean</returns>
        private bool TryParseInput(out string message, List<RangeValueItem> list)
        {
            message = string.Empty;
            int len = this._TxtValue.Count();
            if (len <= 0)
            {
                message = "不能保存空数据！";
                return false;
            }

            decimal lower;
            decimal upper;
            decimal value;
            RangeValueItem item;
            for (int i = 0; i < len; i++)
            {
                if (this._TxtLowerLimit[i] == null || this._TxtLowerLimit[i].Visible == false)
                {
                    continue;
                }
                ////上限必须大于下限；不可有范围交叉
                if (decimal.TryParse(this._TxtLowerLimit[i].Text.Trim(), out lower)
                    && decimal.TryParse(this._TxtUpperLimit[i].Text.Trim(), out upper)
                    && decimal.TryParse(this._TxtValue[i].Text.Trim(), out value)
                    )
                {
                    item = new RangeValueItem();
                    item.LowerValue = lower;
                    item.UpperValue = upper;
                    item.Value = value;
                    //判断交叉
                    if (this.TryParseCross(list, item))
                    {
                        message = "范围设置与前面的范围设置存在交叉，请重新设置！";
                        this._TxtLowerLimit[i].Focus();
                        this._TxtLowerLimit[i].SelectAll();
                        return false;
                    }
                    else
                    {
                        list.Add(item);
                    }
                }
                else
                {
                    this._TxtLowerLimit[i].Focus();
                    this._TxtLowerLimit[i].SelectAll();
                    string temp = message == string.Empty ? "上限必须大于下限，且输入必须为有效的数值类型" : message;
                    message = "输入有误！" + temp;
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region TryParseCross

        /// <summary>
        /// 判断指定优惠设置是否与现有设置存在交叉，true：有交叉；false：没有交叉
        /// </summary>
        /// <returns>Return a object of System.Boolean</returns>
        private bool TryParseCross(List<RangeValueItem> list, RangeValueItem info)
        {
            if (list == null || list.Count <= 0)
            {
                return false;
            }

            bool state = false;
            foreach (RangeValueItem item in list)
            {
                if (info.LowerValue >= item.LowerValue && info.LowerValue < item.UpperValue)
                {
                    state = true;
                    break;
                }

                if (info.UpperValue > item.LowerValue && info.UpperValue <= item.UpperValue)
                {
                    state = true;
                    break;
                }
            }

            return state;
        }
        #endregion

        #region DeleteItem

        private void DeleteItem(object obj, EventArgs args)
        {
            LinkLabel link = obj as LinkLabel;
            int index = link.Name.ToString().ToInt32();
            link.Visible = false;
            this._TxtValue[index].Visible = false;
            this._TxtLowerLimit[index].Visible = false;
            this._TxtUpperLimit[index].Visible = false;
            this._TxtValue[index] = null;
            this._TxtLowerLimit[index] = null;
            this._TxtUpperLimit[index] = null;
            tlpItems.RowStyles[index].Height = 0F;
            ////tlpItems.RowStyles.RemoveAt(index);
            ////tlpItems.RowCount--;
            ////this._RowIndex--;  
            tlpItems.Refresh();
        }
        #endregion

        #region CreateControls

        private LinkLabel CreateDelLinkBtn(int index)
        {
            LinkLabel link = new LinkLabel();
            link.Dock = DockStyle.Fill;
            link.Text = " 删 除 ";
            link.TextAlign = ContentAlignment.MiddleLeft;
            link.Name = index.ToString();
            link.Click += this.DeleteItem;
            link.Enabled = this.EditEnable;
            return link;
        }

        private TXTextBox CreateTextBox(string text)
        {
            TXTextBox txt = new TXTextBox();
            txt.Dock = DockStyle.Fill;
            txt.Text = text;
            txt.MaxLength = 12;
            txt.ReadOnly = !this.EditEnable;
            return txt;
        }

        #endregion

        #endregion
    }
}
