using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TX.Framework.WindowUI.Controls
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Header")]
    public class RangeValueHeader
    {
        private string _LowerValueTitle;

        private string _UpperValueTitle;

        private string _ValueTitle;

        public EventHandler OnTitleChanged;

        public RangeValueHeader()
        {
            this._LowerValueTitle = "范围下限";
            this._UpperValueTitle = "范围上限";
            this._ValueTitle = "金额数值";
        }

        /// <summary>
        /// 下限标题(LowerValueTitle)
        /// </summary>
        /// <value></value>
        /// User:Ryan  CreateTime:2012-11-19 15:37.
        public string LowerValueTitle
        {
            get
            {
                return this._LowerValueTitle;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._LowerValueTitle = value;
                    if (this.OnTitleChanged != null)
                    {
                        this.OnTitleChanged(null, null);
                    }
                }
            }
        }

        /// <summary>
        /// 上限标题(UpperValueTitle)
        /// </summary>
        /// <value></value>
        /// User:Ryan  CreateTime:2012-11-19 15:38.
        public string UpperValueTitle
        {
            get
            {
                return  this._UpperValueTitle;
            }
            set
            {
                if(!string.IsNullOrEmpty(value))
                {
                    this._UpperValueTitle = value;
                    if (this.OnTitleChanged != null)
                    {
                        this.OnTitleChanged(null, null);
                    }
                }
            }
        }

        /// <summary>
        /// 值标题(ValueTitle)
        /// </summary>
        /// <value></value>
        /// User:Ryan  CreateTime:2012-11-19 15:38.
        public string ValueTitle
        {
            get
            {
                return this._ValueTitle;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._ValueTitle = value;
                    if (this.OnTitleChanged != null)
                    {
                        this.OnTitleChanged(null, null);
                    }
                }
            }
        }
    }
}
