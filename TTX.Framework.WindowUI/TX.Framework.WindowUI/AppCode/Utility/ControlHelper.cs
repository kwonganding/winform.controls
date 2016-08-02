using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TX.Framework.WindowUI.Forms;

namespace TX.Framework.WindowUI
{
    /// <summary>
    /// 控件处理的基本帮助类
    /// </summary>
    /// User:Ryan  CreateTime:2011-08-19 16:49.
    public class ControlHelper
    {
        /// <summary>
        /// Not applicable
        /// </summary>
        public const string NA = "0.0";

        #region GetTextFormatFlags

        /// <summary>
        /// 获取文本的格式
        /// </summary>
        public static TextFormatFlags GetTextFormatFlags(ContentAlignment alignment, bool rightToleft)
        {
            TextFormatFlags flags = TextFormatFlags.WordBreak | TextFormatFlags.SingleLine;
            if (rightToleft)
            {
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            }

            switch (alignment)
            {
                case ContentAlignment.BottomCenter:
                    flags |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.BottomLeft:
                    flags |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                    break;
                case ContentAlignment.BottomRight:
                    flags |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                    break;
                case ContentAlignment.MiddleCenter:
                    flags |= TextFormatFlags.HorizontalCenter |
                        TextFormatFlags.VerticalCenter;
                    break;
                case ContentAlignment.MiddleLeft:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case ContentAlignment.MiddleRight:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;
                case ContentAlignment.TopCenter:
                    flags |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.TopLeft:
                    flags |= TextFormatFlags.Top | TextFormatFlags.Left;
                    break;
                case ContentAlignment.TopRight:
                    flags |= TextFormatFlags.Top | TextFormatFlags.Right;
                    break;
            }
            return flags;
        }
        #endregion

        #region BindMouseMoveEvent

        /// <summary>
        /// 给控件绑定鼠标移动的处理事件
        /// </summary>
        /// <param name="control">The control.</param>
        /// User:Ryan  CreateTime:2011-08-19 16:52.
        public static void BindMouseMoveEvent(Control control)
        {
            if (control != null)
            {
                control.MouseDown +=
                    delegate
                    {
                        Win32.ReleaseCapture();
                        BaseForm fb = control.FindForm() as BaseForm;
                        if (fb != null && fb.CaptionHeight > 0 && fb.WindowState!=FormWindowState.Maximized)
                        {
                            Win32.SendMessage(control.FindForm().Handle, (int)WindowMessages.WM_SYSCOMMAND, (int)SystemCommands.SC_MOVE + (int)NCHITTEST.HTCAPTION, 0);
                        }
                    };

            }
        }

        #endregion

        #region


        #endregion

        #region

        #endregion
    }
}
