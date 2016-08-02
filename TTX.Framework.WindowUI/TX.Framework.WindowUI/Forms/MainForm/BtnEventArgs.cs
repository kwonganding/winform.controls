using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TX.Framework.WindowUI.Forms
{
    /// <summary>
    /// 按钮点击的事件参数
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-4 12:45.
    public class BtnEventArgs : EventArgs
    {
        private Rectangle _Bounds;

        public BtnEventArgs(Rectangle bounds)
            : base()
        {
            this._Bounds = bounds;
        }
        /// <summary>
        /// 获取按钮的区域
        /// </summary>
        /// <value>The bounds.</value>
        /// User:Ryan  CreateTime:2011-08-12 16:26.
        public Rectangle Bounds
        {
            get
            {
                return this._Bounds;
            }
        }
    }
}
