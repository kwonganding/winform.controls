using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Drawing;

namespace TX.Framework.WindowUI
{
    /// <summary>
    /// Win32API
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-3 15:23.
    internal class Win32
    {
        #region fileds

        public static readonly IntPtr TRUE = new IntPtr(1);

        public static readonly IntPtr FALSE = IntPtr.Zero;

        #endregion

        #region extern methods

        /// <summary>
        /// windows窗口的动画效果
        /// </summary>
        /// <param name="whnd">The WHND.</param>
        /// <param name="dwtime">The dwtime.</param>
        /// <param name="dwflag">动画标识，多个以|分隔）</param>
        /// <returns></returns>
        /// User:K.Anding  CreateTime:2011-7-19 23:55.
        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);

        /// <summary>
        /// 获取指定窗口控件的系统信息
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-21 13:44.
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr handle);

        /// <summary>
        /// 释放设备上下文环境
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-21 13:45.
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ReleaseDC(IntPtr handle, IntPtr hDC);

        /// <summary>
        /// 从当前线程的窗体中释放对鼠标的扑捉
        /// </summary>
        /// <returns>Return a data(or instance) of Boolean.</returns>
        /// User:Ryan  CreateTime:2011-07-26 15:27.
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        //[DllImport("user32.dll", EntryPoint = "SendMessage")]
        //public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, int lParam);

        //[DllImport("user32.dll")]
        //public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(
            IntPtr hWnd, int Msg, int wParam, ref RECT lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(
            IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(
            IntPtr hwnd, int msg, int wParam, ref HDITEM lParam);

        /// <summary>
        /// 获取ComboBox的控件信息
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref ComboBoxInfo info);

        /// <summary>
        /// 设置圆角窗体
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

        /// <summary>
        /// 获取窗体的工作区域
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        /// <summary>
        /// 创建圆角的区域
        /// </summary>
        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);

        /// <summary>
        /// 
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

        [DllImport("user32.dll")]
        public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(
            IntPtr hwndParent,
            IntPtr hwndChildAfter,
            string lpszClass,
            string lpszWindow);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll")]
        public extern static int OffsetRect(ref RECT lpRect, int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PtInRect([In] ref RECT lprc, Point pt);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClientRect(IntPtr hWnd, ref RECT r);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool ValidateRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr handle);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);

        [DllImport("user32.dll")]
        public static extern int ShowScrollBar(IntPtr hWnd, int iBar, int bShow);

        #endregion

        #region methods

        /// <summary>
        /// 取地两位值
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// User:Ryan  CreateTime:2011-07-26 15:29.
        public static int LOWORD(int value)
        {
            return value & 0xFFFF;
        }

        /// <summary>
        /// 取高两位值
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// User:Ryan  CreateTime:2011-07-26 15:30.
        public static int HIWORD(int value)
        {
            return value >> 16;
        }

        public static IntPtr GetWindowLong(HandleRef hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
            {
                return GetWindowLong32(hWnd, nIndex);
            }
            return GetWindowLongPtr64(hWnd, nIndex);
        }
        #endregion
    }
}
