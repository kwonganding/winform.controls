using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TX.Framework.WindowUI.Controls
{
    /// <summary>
    /// 房价（房控）信息
    /// </summary>
    public class RoomRateItem
    {
        /// <summary>
        /// 价格标题，默认“预订价”
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-14 22:55.
        private readonly string _priceTitle;

        /// <summary>
        /// 佣金标题，默认“佣金”
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-14 22:55.
        private readonly string _commissionTitle;

        /// <summary>
        /// 服务费标题，默认“服务费”
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-14 22:55.
        private readonly string _serviceFeeTitle;

        public RoomRateItem( DateItem item )
        {
            this.Price = 0;
            this._priceTitle = "预订价:";
            this._commissionTitle = "佣金:";
            this._serviceFeeTitle = "服务费:";
            this.Commssion = 0;
            this.CommssionPercent = 0;
            this.RoomControl = ControlHelper.NA;
            this.DateItem = item;
        }

        public DateItem DateItem
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取或者设置预订价格
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-14 22:55.
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或者设置佣金金额
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-14 22:55.
        public decimal Commssion { get; set; }

        /// <summary>
        /// 获取或者设置佣金比例
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-14 22:55.
        public decimal? CommssionPercent { get; set; } 

        /// <summary>
        /// 获取或者设置房控信息
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-14 22:55.
        public string RoomControl { get; set; }

        /// <summary>
        /// 获取或者设置房控信息
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-14 22:55.
        public decimal ServiceFee { get; set; }

        /// <summary>
        /// 加床价
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-14 22:55.
        public string ExtraBedPrice { get; set; }

        /// <summary>
        /// 最晚保留时间
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-14 22:55.
        public string ReserveTime { get; set; }

        /// <summary>
        /// 获取房价的字符串信息
        /// </summary>
        /// <value>The text.</value>
        /// User:Ryan  CreateTime:2012-11-14 22:47.
        public string Text
        {
            get
            {
                return this.ToString();
            }
        }

        /// <summary>
        /// 获取或者设置房价实体对象（Gets or sets  the price tag）
        /// </summary>
        /// <value>The price tag.</value>
        /// User:Ryan  CreateTime:2012-11-15 9:36.
        public object PriceTag { get; set; }

        /// <summary>
        /// 获取或者设置房控实体对象（Gets or sets  the control tag）
        /// </summary>
        /// <value>The control tag.</value>
        /// User:Ryan  CreateTime:2012-11-15 9:36.
        public object ControlTag { get; set; }

        /// <summary>
        /// 返回当前对象的字符串值
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-14 22:47.
        public override string ToString()
        {
            return string.Concat(
                this._priceTitle, this.Price.Equals(0) ? ControlHelper.NA : this.Price.ToString("F2"),
                Environment.NewLine,
                this._commissionTitle, this.GetCommissionText(), Environment.NewLine,
                this._serviceFeeTitle, this.ServiceFee.ToString("F2") + "%",Environment.NewLine,
                this.RoomControl.Equals(ControlHelper.NA) ? Environment.NewLine : this.RoomControl
                    );
        }

        /// <summary>
        /// 获取佣金应该显示的字符
        /// </summary>
        /// User:Ryan  CreateTime:2012-11-14 22:54.
        private string GetCommissionText()
        {
            if (!this.Commssion.Equals(0))
            {
                return this.Commssion.ToString();
            }
            //else if (this.CommssionPercent != null && !this.CommssionPercent.Equals(0))
            //{
            //    return string.Format("{0}%", ((double)this.CommssionPercent * 100).ToString("F2"));
            //}
            else
            {
                return ControlHelper.NA;
            }
        }
    }
}
