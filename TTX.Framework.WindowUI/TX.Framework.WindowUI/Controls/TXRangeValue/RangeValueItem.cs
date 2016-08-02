using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TX.Framework.WindowUI.Controls
{
    public class RangeValueItem
    {
        public RangeValueItem()
        {
            this.LowerValue = 0;
            this.UpperValue = 0;
            this.Value = 0;
        }

        public decimal LowerValue { get; set; }

        public decimal UpperValue { get; set; }

        public decimal Value { get; set; }

        public object Tag { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", this.LowerValue.ToString("F2"), this.UpperValue.ToString("F2"), this.Value.ToString("F2"));
        }
    }

    public class RangeValueCollection : List<RangeValueItem>
    {
        public RangeValueCollection()
        {
        }

        public RangeValueCollection(List<RangeValueItem> list)
        {
            if (list != null && list.Count > 0)
            {
                this.AddRange(list);
            }
        }

        public void Sort()
        {
            this.OrderBy(s => s.LowerValue).ToList();
        }
    }
}
