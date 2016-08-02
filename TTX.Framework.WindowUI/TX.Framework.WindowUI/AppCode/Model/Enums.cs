using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TX.Framework.WindowUI
{
    #region EnumControlState

    /// <summary>
    /// 控件的基本状态
    /// </summary>
    internal enum EnumControlState
    {
        None,

        /// <summary>
        /// 默认状态
        /// </summary>
        Default,

        /// <summary>
        /// 高亮状态（鼠标悬浮）
        /// </summary>
        HeightLight,

        /// <summary>
        /// 焦点（鼠标按下、已选择、输入状态等）
        /// </summary>
        Focused,
    }

    #endregion

    #region ControlStyelMode

    /// <summary>
    /// 控件的样式控制方式。
    /// （暂时都使用全局的）
    /// </summary>
    internal enum EnumControlStyelMode
    {
        /// <summary>
        /// 自定义样式控制
        /// </summary>
        Custom,

        /// <summary>
        /// 全局的样式控制
        /// </summary>
        Overall,
    }
    #endregion

    #region EnumBorderStyle

    /// <summary>
    /// GroupBox的边框样式
    /// </summary>
    public enum EnumBorderStyle : int
    {
        /// <summary>
        /// 无
        /// </summary>
        None,

        /// <summary>
        /// 默认样式绘制
        /// </summary>
        Default,

        /// <summary>
        /// QQ风格的样式绘制
        /// </summary>
        QQStyle,
    }

    #endregion

    #region EnumMessageBox

    /// <summary>
    /// EnumMessageBox的信息类型
    /// </summary>
    internal enum EnumMessageBox
    {
        /// <summary>
        /// 信息
        /// </summary>
        Info,

        /// <summary>
        /// 错误
        /// </summary>
        Error,

        /// <summary>
        /// 询问
        /// </summary>
        Question,

        /// <summary>
        /// 警告
        /// </summary>
        Warning,
    }

    #endregion

    #region EnumTabStyle

    /// <summary>
    /// Tabcontrol的边框样式
    /// </summary>
    public enum EnumTabStyle
    {
        Default,

        AnglesWing,
    }

    #endregion

    #region EnumPageSize

    /// <summary>
    /// 分页大小
    /// </summary>
    internal enum EnumPageSize
    {
        Size_10 = 10,

        Size_20 = 20,

        Size_50 = 50,

        Size_80 = 80,

        Size_150 = 150,

        Size_250 = 250,
    }

    #endregion

    #region EnumShowWindowMode

    /// <summary>
    /// 窗口显示动画模式
    /// </summary>
    internal enum EnumShowWindowMode
    {
        None = 0,

        /// <summary>
        /// 从左到右显示
        /// </summary>
        LeftToRight = 0x00000001,

        /// <summary>
        /// 从右到左显示
        /// </summary>
        RightToLeft = 0x00000002,


        /// <summary>
        /// 从上到下显示
        /// </summary>
        TopToBottom = 0x00000004,

        /// <summary>
        /// 从下到上显示
        /// </summary>
        BottomToTop = 0x00000008,

        /// <summary>
        /// 否则使窗口向外扩展，即展开窗口
        /// </summary>
        Center = 0x00000010,
    }

    #endregion

    #region AnimateWindow flag

    /// <summary>
    /// 窗体动画标识
    /// </summary>
    internal enum AnimateWindowFlag
    {
        /// <summary>
        /// 从左到右显示
        /// </summary>
        AW_HOR_POSITIVE = 0x00000001,

        /// <summary>
        /// 从右到左显示
        /// </summary>
        AW_HOR_NEGATIVE = 0x00000002,


        /// <summary>
        /// 从上到下显示
        /// </summary>
        AW_VER_POSITIVE = 0x00000004,

        /// <summary>
        /// 从下到上显示
        /// </summary>
        AW_VER_NEGATIVE = 0x00000008,

        /// <summary>
        /// 若使用了AW_HIDE标志，则使窗口向内重叠，即收缩窗口；否则使窗口向外扩展，即展开窗口
        /// </summary>
        AW_CENTER = 0x00000010,

        //
        /// <summary>
        /// 隐藏窗口，缺省则显示窗口
        /// </summary>
        AW_HIDE = 0x00010000,

        /// <summary>
        /// 激活窗口。在使用了AW_HIDE标志后不能使用这个标志
        /// </summary>
        AW_ACTIVATE = 0x00020000,

        //
        /// <summary>
        /// 使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略
        /// </summary>
        AW_SLIDE = 0x00040000,

        /// <summary>
        /// 透明度从高到低
        /// </summary>
        AW_BLEND = 0x00080000,
    }

    #endregion

    #region WindowMessages

    /// <summary>
    /// Windows常量消息参数
    /// </summary>
    internal enum WindowMessages : int
    {
        WM_NULL = 0x0000,

        WM_CREATE = 0x0001,

        WM_DESTROY = 0x0002,

        WM_MOVE = 0x0003,

        WM_SIZE = 0x0005,

        WM_ACTIVATE = 0x0006,

        WM_SETFOCUS = 0x0007,

        WM_KILLFOCUS = 0x0008,

        WM_ENABLE = 0x000A,

        WM_SETREDRAW = 0x000B,

        WM_SETTEXT = 0x000C,

        WM_GETTEXT = 0x000D,

        WM_GETTEXTLENGTH = 0x000E,

        WM_PAINT = 0x000F,

        WM_CLOSE = 0x0010,

        WM_QUIT = 0x0012,

        WM_ERASEBKGND = 0x0014,

        WM_SYSCOLORCHANGE = 0x0015,

        WM_SHOWWINDOW = 0x0018,

        WM_ACTIVATEAPP = 0x001C,

        WM_SETCURSOR = 0x0020,

        WM_MOUSEACTIVATE = 0x0021,

        WM_GETMINMAXINFO = 0x24,

        WM_WINDOWPOSCHANGING = 0x0046,

        WM_WINDOWPOSCHANGED = 0x0047,

        WM_CONTEXTMENU = 0x007B,

        WM_STYLECHANGING = 0x007C,

        WM_STYLECHANGED = 0x007D,

        WM_DISPLAYCHANGE = 0x007E,

        WM_GETICON = 0x007F,

        WM_SETICON = 0x0080,

        // non client area
        /// <summary>
        /// 在WM_CREATE前的一个消息
        /// </summary>
        WM_NCCREATE = 0x0081,

        WM_NCDESTROY = 0x0082,

        /// <summary>
        /// 计算窗体客户区域大小和位置的消息
        /// </summary>
        WM_NCCALCSIZE = 0x0083,

        WM_NCHITTEST = 0x84,

        WM_NCPAINT = 0x0085,

        WM_NCACTIVATE = 0x0086,

        WM_GETDLGCODE = 0x0087,

        WM_SYNCPAINT = 0x0088,

        // non client mouse
        WM_NCMOUSEMOVE = 0x00A0,

        WM_NCLBUTTONDOWN = 0x00A1,

        WM_NCLBUTTONUP = 0x00A2,

        WM_NCLBUTTONDBLCLK = 0x00A3,
        WM_NCRBUTTONDOWN = 0x00A4,
        WM_NCRBUTTONUP = 0x00A5,
        WM_NCRBUTTONDBLCLK = 0x00A6,
        WM_NCMBUTTONDOWN = 0x00A7,
        WM_NCMBUTTONUP = 0x00A8,
        WM_NCMBUTTONDBLCLK = 0x00A9,

        // keyboard
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_CHAR = 0x0102,

        WM_SYSCOMMAND = 0x0112,

        // menu
        WM_INITMENU = 0x0116,
        WM_INITMENUPOPUP = 0x0117,
        WM_MENUSELECT = 0x011F,
        WM_MENUCHAR = 0x0120,
        WM_ENTERIDLE = 0x0121,
        WM_MENURBUTTONUP = 0x0122,
        WM_MENUDRAG = 0x0123,
        WM_MENUGETOBJECT = 0x0124,
        WM_UNINITMENUPOPUP = 0x0125,
        WM_MENUCOMMAND = 0x0126,

        WM_CHANGEUISTATE = 0x0127,
        WM_UPDATEUISTATE = 0x0128,
        WM_QUERYUISTATE = 0x0129,

        // mouse
        WM_MOUSEFIRST = 0x0200,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_MOUSEWHEEL = 0x020A,
        WM_MOUSELAST = 0x020D,

        WM_PARENTNOTIFY = 0x0210,
        WM_ENTERMENULOOP = 0x0211,
        WM_EXITMENULOOP = 0x0212,

        WM_NEXTMENU = 0x0213,
        WM_SIZING = 0x0214,
        WM_CAPTURECHANGED = 0x0215,
        WM_MOVING = 0x0216,

        WM_ENTERSIZEMOVE = 0x0231,
        WM_EXITSIZEMOVE = 0x0232,

        WM_MOUSELEAVE = 0x02A3,
        WM_MOUSEHOVER = 0x02A1,
        WM_NCMOUSEHOVER = 0x02A0,
        WM_NCMOUSELEAVE = 0x02A2,

        WM_MDIACTIVATE = 0x0222,
        WM_HSCROLL = 0x0114,
        WM_VSCROLL = 0x0115,

        WM_PRINT = 0x0317,
        WM_PRINTCLIENT = 0x0318,
        WM_PASTE = 0X302,

        APP = 32768,
        ACTIVATE = 6,
        ACTIVATEAPP = 28,
        AFXFIRST = 864,
        AFXLAST = 895,
        ASKCBFORMATNAME = 780,
        CANCELJOURNAL = 75,
        CANCELMODE = 31,
        CAPTURECHANGED = 533,
        CHANGECBCHAIN = 781,
        CHAR = 258,
        CHARTOITEM = 47,
        CHILDACTIVATE = 34,
        CLEAR = 771,
        CLOSE = 16,
        COMMAND = 273,
        COMMNOTIFY = 68,
        COMPACTING = 65,
        COMPAREITEM = 57,
        CONTEXTMENU = 123,
        COPY = 769,
        COPYDATA = 74,
        CREATE = 1,
        CTLCOLOR = 0x0019,
        CTLCOLORBTN = 309,
        CTLCOLORDLG = 310,
        CTLCOLOREDIT = 307,
        CTLCOLORLISTBOX = 308,
        CTLCOLORMSGBOX = 306,
        CTLCOLORSCROLLBAR = 311,
        CTLCOLORSTATIC = 312,
        CUT = 768,
        DEADCHAR = 259,
        DELETEITEM = 45,
        DESTROY = 2,
        DESTROYCLIPBOARD = 775,
        DEVICECHANGE = 537,
        DEVMODECHANGE = 27,
        DISPLAYCHANGE = 126,
        DRAWCLIPBOARD = 776,
        DRAWITEM = 43,
        DROPFILES = 563,
        ENABLE = 10,
        ENDSESSION = 22,
        ENTERIDLE = 289,
        ENTERMENULOOP = 529,
        ENTERSIZEMOVE = 561,
        ERASEBKGND = 20,
        EXITMENULOOP = 530,
        EXITSIZEMOVE = 562,
        FONTCHANGE = 29,
        GETDLGCODE = 135,
        GETFONT = 49,
        GETHOTKEY = 51,
        GETICON = 127,
        GETMINMAXINFO = 36,
        GETTEXT = 13,
        GETTEXTLENGTH = 14,
        HANDHELDFIRST = 856,
        HANDHELDLAST = 863,
        HELP = 83,
        HOTKEY = 786,
        HSCROLL = 276,
        HSCROLLCLIPBOARD = 782,
        ICONERASEBKGND = 39,
        INITDIALOG = 272,
        INITMENU = 278,
        INITMENUPOPUP = 279,
        UNINITMENUPOPUP = 293,
        INPUTLANGCHANGE = 81,
        INPUTLANGCHANGEREQUEST = 80,
        KEYDOWN = 256,
        KEYUP = 257,
        KILLFOCUS = 8,
        MDIACTIVATE = 546,
        MDICASCADE = 551,
        MDICREATE = 544,
        MDIDESTROY = 545,
        MDIGETACTIVE = 553,
        MDIICONARRANGE = 552,
        MDIMAXIMIZE = 549,
        MDINEXT = 548,
        MDIREFRESHMENU = 564,
        MDIRESTORE = 547,
        MDISETMENU = 560,
        MDITILE = 550,
        MEASUREITEM = 44,
        MENUCHAR = 288,
        MENUSELECT = 287,
        MENUCOMMAND = 294,
        NEXTMENU = 531,
        MOVE = 3,
        MOVING = 534,
        NCACTIVATE = 134,
        NCCALCSIZE = 131,
        NCCREATE = 129,
        NCDESTROY = 130,
        NCHITTEST = 132,
        NCLBUTTONDBLCLK = 163,
        NCLBUTTONDOWN = 161,
        NCLBUTTONUP = 162,
        NCMBUTTONDBLCLK = 169,
        NCMBUTTONDOWN = 167,
        NCMBUTTONUP = 168,
        NCMOUSEMOVE = 160,
        NCPAINT = 133,
        NCRBUTTONDBLCLK = 166,
        NCRBUTTONDOWN = 164,
        NCRBUTTONUP = 165,
        NEXTDLGCTL = 40,
        NOTIFY = 78,
        NOTIFYFORMAT = 85,
        NULL = 0,
        PAINT = 15,
        PAINTCLIPBOARD = 777,
        PAINTICON = 38,
        PALETTECHANGED = 785,
        PALETTEISCHANGING = 784,
        PARENTNOTIFY = 528,
        PASTE = 770,
        PENWINFIRST = 896,
        PENWINLAST = 911,
        POWER = 72,
        POWERBROADCAST = 536,
        PRINT = 791,
        PRINTCLIENT = 792,
        QUERYDRAGICON = 55,
        QUERYENDSESSION = 17,
        QUERYNEWPALETTE = 783,
        QUERYOPEN = 19,
        QUEUESYNC = 35,
        QUIT = 18,
        RENDERALLFORMATS = 774,
        RENDERFORMAT = 773,
        SETCURSOR = 32,
        SETFOCUS = 7,
        SETFONT = 48,
        SETHOTKEY = 50,
        SETICON = 128,
        SETREDRAW = 11,
        SETTEXT = 12,
        SETTINGCHANGE = 26,
        SHOWWINDOW = 24,
        SIZE = 5,
        SIZECLIPBOARD = 779,
        SIZING = 532,
        SPOOLERSTATUS = 42,
        STYLECHANGED = 125,
        STYLECHANGING = 124,
        SYSCHAR = 262,
        SYSCOLORCHANGE = 21,
        SYSCOMMAND = 274,
        SYSDEADCHAR = 263,
        SYSKEYDOWN = 260,
        SYSKEYUP = 261,
        TCARD = 82,
        TIMECHANGE = 30,
        TIMER = 275,
        UNDO = 772,
        USER = 1024,
        USERCHANGED = 84,
        VKEYTOITEM = 46,
        VSCROLL = 277,
        VSCROLLCLIPBOARD = 778,
        WINDOWPOSCHANGED = 71,
        WINDOWPOSCHANGING = 70,
        WININICHANGE = 26,
        KEYFIRST = 256,
        KEYLAST = 264,
        SYNCPAINT = 136,
        MOUSEACTIVATE = 33,
        MOUSEMOVE = 512,
        LBUTTONDOWN = 513,
        LBUTTONUP = 514,
        LBUTTONDBLCLK = 515,
        RBUTTONDOWN = 516,
        RBUTTONUP = 517,
        RBUTTONDBLCLK = 518,
        MBUTTONDOWN = 519,
        MBUTTONUP = 520,
        MBUTTONDBLCLK = 521,
        MOUSEWHEEL = 522,
        MOUSEFIRST = 512,
        MOUSELAST = 522,
        MOUSEHOVER = 0x2A1,
        MOUSELEAVE = 0x2A3,
        SHNOTIFY = 0x0401,
        UNICHAR = 0x0109,
        THEMECHANGED = 0x031A

    }
    #endregion

    #region SystemCommands

    /// <summary>
    /// 系统窗口管理的命令
    /// </summary>
    internal enum SystemCommands
    {
        SC_SIZE = 0xF000,
        SC_MOVE = 0xF010,
        SC_MINIMIZE = 0xF020,
        SC_MAXIMIZE = 0xF030,
        SC_MAXIMIZE2 = 0xF032,
        SC_NEXTWINDOW = 0xF040,
        SC_PREVWINDOW = 0xF050,
        SC_CLOSE = 0xF060,
        SC_VSCROLL = 0xF070,
        SC_HSCROLL = 0xF080,
        SC_MOUSEMENU = 0xF090,
        SC_KEYMENU = 0xF100,
        SC_ARRANGE = 0xF110,
        SC_RESTORE = 0xF120,
        SC_RESTORE2 = 0xF122,
        SC_TASKLIST = 0xF130,
        SC_SCREENSAVE = 0xF140,
        SC_HOTKEY = 0xF150,

        SC_DEFAULT = 0xF160,
        SC_MONITORPOWER = 0xF170,
        SC_CONTEXTHELP = 0xF180,
        SC_SEPARATOR = 0xF00F
    }

    #endregion

    #region NCHITTEST

    /// <summary>
    /// 鼠标在窗体上的位置枚举
    /// </summary>
    internal enum NCHITTEST
    {
        /// <summary>
        /// On the screen background or on a dividing line between windows 
        /// (same as HTNOWHERE, except that the DefWindowProc function produces a system beep to indicate an error).
        /// </summary>
        HTERROR = (-2),
        /// <summary>
        /// In a window currently covered by another window in the same thread 
        /// (the message will be sent to underlying windows in the same thread until one of them returns a code that is not HTTRANSPARENT).
        /// </summary>
        HTTRANSPARENT = (-1),
        /// <summary>
        /// On the screen background or on a dividing line between windows.
        /// </summary>
        HTNOWHERE = 0,
        /// <summary>In a client area.</summary>
        HTCLIENT = 1,
        /// <summary>In a title bar.</summary>
        HTCAPTION = 2,
        /// <summary>In a window menu or in a Close button in a child window.</summary>
        HTSYSMENU = 3,
        /// <summary>In a size box (same as HTSIZE).</summary>
        HTGROWBOX = 4,
        /// <summary>In a menu.</summary>
        HTMENU = 5,
        /// <summary>In a horizontal scroll bar.</summary>
        HTHSCROLL = 6,
        /// <summary>In the vertical scroll bar.</summary>
        HTVSCROLL = 7,
        /// <summary>In a Minimize button.</summary>
        HTMINBUTTON = 8,
        /// <summary>In a Maximize button.</summary>
        HTMAXBUTTON = 9,
        /// <summary>In the left border of a resizable window 
        /// (the user can click the mouse to resize the window horizontally).</summary>
        HTLEFT = 10,
        /// <summary>
        /// In the right border of a resizable window 
        /// (the user can click the mouse to resize the window horizontally).
        /// </summary>
        HTRIGHT = 11,
        /// <summary>In the upper-horizontal border of a window.</summary>
        HTTOP = 12,
        /// <summary>In the upper-left corner of a window border.</summary>
        HTTOPLEFT = 13,
        /// <summary>In the upper-right corner of a window border.</summary>
        HTTOPRIGHT = 14,
        /// <summary>	In the lower-horizontal border of a resizable window 
        /// (the user can click the mouse to resize the window vertically).</summary>
        HTBOTTOM = 15,
        /// <summary>In the lower-left corner of a border of a resizable window 
        /// (the user can click the mouse to resize the window diagonally).</summary>
        HTBOTTOMLEFT = 16,
        /// <summary>	In the lower-right corner of a border of a resizable window 
        /// (the user can click the mouse to resize the window diagonally).</summary>
        HTBOTTOMRIGHT = 17,
        /// <summary>In the border of a window that does not have a sizing border.</summary>
        HTBORDER = 18,

        HTOBJECT = 19,
        /// <summary>In a Close button.</summary>
        HTCLOSE = 20,
        /// <summary>In a Help button.</summary>
        HTHELP = 21,
    }

    #endregion

    #region WindowStyle

    /// <summary>
    /// 窗体样式的参数枚举
    /// </summary>
    [Flags]
    internal enum WindowStyle : uint
    {
        WS_OVERLAPPED = 0x00000000,
        WS_POPUP = 0x80000000,
        WS_CHILD = 0x40000000,
        WS_MINIMIZE = 0x20000000,
        WS_VISIBLE = 0x10000000,
        WS_DISABLED = 0x08000000,
        WS_CLIPSIBLINGS = 0x04000000,
        WS_CLIPCHILDREN = 0x02000000,
        WS_MAXIMIZE = 0x01000000,
        WS_CAPTION = 0x00C00000,
        WS_BORDER = 0x00800000,
        WS_DLGFRAME = 0x00400000,
        WS_VSCROLL = 0x00200000,
        WS_HSCROLL = 0x00100000,
        WS_SYSMENU = 0x00080000,
        WS_THICKFRAME = 0x00040000,
        WS_GROUP = 0x00020000,
        WS_TABSTOP = 0x00010000,
        WS_MINIMIZEBOX = 0x00020000,
        WS_MAXIMIZEBOX = 0x00010000,
        WS_TILED = WS_OVERLAPPED,
        WS_ICONIC = WS_MINIMIZE,
        WS_SIZEBOX = WS_THICKFRAME,
        WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,
        WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU |
                                WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),
        WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU),
        WS_CHILDWINDOW = (WS_CHILD)
    }

    #endregion

    #region WindowStyleEx

    /// <summary>
    /// 扩展窗体样式
    /// </summary>
    [Flags]
    internal enum WindowStyleEx
    {
        WS_EX_DLGMODALFRAME = 0x00000001,
        WS_EX_NOPARENTNOTIFY = 0x00000004,
        WS_EX_TOPMOST = 0x00000008,
        WS_EX_ACCEPTFILES = 0x00000010,
        WS_EX_TRANSPARENT = 0x00000020,
        WS_EX_MDICHILD = 0x00000040,
        WS_EX_TOOLWINDOW = 0x00000080,
        WS_EX_WINDOWEDGE = 0x00000100,
        WS_EX_CLIENTEDGE = 0x00000200,
        WS_EX_CONTEXTHELP = 0x00000400,
        WS_EX_RIGHT = 0x00001000,
        WS_EX_LEFT = 0x00000000,
        WS_EX_RTLREADING = 0x00002000,
        WS_EX_LTRREADING = 0x00000000,
        WS_EX_LEFTSCROLLBAR = 0x00004000,
        WS_EX_RIGHTSCROLLBAR = 0x00000000,
        WS_EX_CONTROLPARENT = 0x00010000,
        WS_EX_STATICEDGE = 0x00020000,
        WS_EX_APPWINDOW = 0x00040000,
        WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE),
        WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST),
        WS_EX_LAYERED = 0x00080000,
        WS_EX_NOINHERITLAYOUT = 0x00100000, // Disable inheritence of mirroring by children
        WS_EX_LAYOUTRTL = 0x00400000, // Right to left mirroring
        WS_EX_COMPOSITED = 0x02000000,
        WS_EX_NOACTIVATE = 0x08000000,
    }

    #endregion

    #region KeyStatesMasks
    /// <summary>
    /// 虚拟键码常数
    /// </summary>
    internal enum KeyStatesMasks
    {
        VK_LBUTTON = 0x0001,
        VK_RBUTTON = 0x0002,
        VK_SHIFT = 0x0004,
        VK_CONTROL = 0x0008,
        VK_MBUTTON = 0x0010,
        VK_XBUTTON1 = 0x0020,
        VK_XBUTTON2 = 0x0040,

        LBUTTON = 0x0001,
        RBUTTON = 0x0002,
        SHIFT = 0x0004,
        CONTROL = 0x0008,
        MBUTTON = 0x0010,
        XBUTTON1 = 0x0020,
        XBUTTON2 = 0x0040,
    }
    #endregion

    #region ComboBoxButtonState

    /// <summary>
    /// ComboBoxButton状态
    /// </summary>
    internal enum ComboBoxButtonState
    {
        STATE_SYSTEM_NONE = 0,

        STATE_SYSTEM_INVISIBLE = 0x00008000,

        STATE_SYSTEM_PRESSED = 0x00000008
    }

    #endregion

    #region ListViewMessages / LVM

    /// <summary>
    /// ListView Messages / LVM
    /// </summary>
    internal enum ListViewMessages : int
    {
        FIRST = 0x1000,
        SCROLL = FIRST + 20,
        GETITEM = FIRST + 75,
        SETITEM = FIRST + 76,
        GETITEMTEXTW = FIRST + 115,
        SETCOLUMNWIDTH = FIRST + 30,
        LVSCW_AUTOSIZE = -1,
        LVSCW_AUTOSIZE_USEHEADER = -2,
        SETITEMSTATE = FIRST + 43,
        INSERTITEMA = FIRST + 77,
        DELETEITEM = FIRST + 8,
        GETITEMCOUNT = FIRST + 4,
        GETCOUNTPERPAGE = FIRST + 40,
        GETSUBITEMRECT = FIRST + 56,
        SUBITEMHITTEST = FIRST + 57,
        GETCOLUMN = FIRST + 25,
        SETCOLUMN = FIRST + 26,
        GETCOLUMNORDERARRAY = FIRST + 59,
        SETCOLUMNORDERARRAY = FIRST + 58,
        SETEXTENDEDLISTVIEWSTYLE = FIRST + 54,
        GETEXTENDEDLISTVIEWSTYLE = FIRST + 55,
        EDITLABELW = FIRST + 118,
        GETITEMRECT = FIRST + 14,
        HITTEST = FIRST + 18,
        GETEDITCONTROL = FIRST + 24,
        CANCELEDITLABEL = FIRST + 179,
        GETHEADER = FIRST + 31,
        REDRAWITEMS = FIRST + 21,
        GETSELECTIONMARK = FIRST + 66,
        SETSELECTIONMARK = FIRST + 67,
        ENSUREVISIBLE = (FIRST + 19),
    }
    #endregion

    #region HeaderItem flags / HDI
    /// <summary>
    /// HeaderItem flags / HDI
    /// </summary>
    internal enum HeaderItemFlags
    {
        WIDTH = 0x0001,
        HEIGHT = WIDTH,
        TEXT = 0x0002,
        FORMAT = 0x0004,
        LPARAM = 0x0008,
        BITMAP = 0x0010,
        IMAGE = 0x0020,
        DI_SETITEM = 0x0040,
        ORDER = 0x0080
    }
    #endregion

    #region Header Control Messages / HDM
    /// <summary>
    /// Header Control Messages / HDM
    /// </summary>
    internal enum HeaderControlMessages : int
    {
        FIRST = 0x1200,
        GETITEMRECT = (FIRST + 7),
        HITTEST = (FIRST + 6),
        SETIMAGELIST = (FIRST + 8),
        GETITEMW = (FIRST + 11),
        ORDERTOINDEX = (FIRST + 15),
        SETITEM = (FIRST + 12),
        SETORDERARRAY = (FIRST + 18),
        GETITEMCOUNT = (FIRST + 0),
        GETITEMA = (FIRST + 3),

    }
    #endregion
}
