using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TX.Framework.WindowUI.Forms
{
    /// <summary>
    /// 主窗口
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-3 23:08.
    public partial class MainForm : BaseForm
    {
        #region private attributes

        /// <summary>
        /// 主窗体水晶按钮状态
        /// </summary>
        private EnumControlState _RibbonBtnState = EnumControlState.Default;

        /// <summary>
        /// 皮肤按钮状态
        /// </summary>
        private EnumControlState _SkinBtnState = EnumControlState.Default;

        /// <summary>
        /// 水晶按钮大小
        /// </summary>
        private Size _RibbonButtonSize;

        private Size _SkinBoxSize;

        #endregion

        #region Initializes

        /// <summary>
        /// (构造函数).Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// User:Ryan  CreateTime:2012-8-4 13:04.
        public MainForm()
            : base()
        {
            this._RibbonButtonSize = new Size(34, 34);
            base.CapitionLogo = null;
            this.LogoSize = new Size(32, 32); //Old:72,30 new :100,32
            this.CaptionHeight = 31;
            this._SkinBoxSize = new Size(14, 14);
            this.OnSkinButtonClick += new EventHandler<BtnEventArgs>(MainForm_OnSkinButtonClick);
        }

        #endregion

        #region Events

        [Description("当主窗口水晶图标按钮被点击后发生")]
        public event EventHandler<BtnEventArgs> OnRibbonButtonClick;

        [Description("当主窗口皮肤图标按钮被点击后发生")]
        public event EventHandler<BtnEventArgs> OnSkinButtonClick;

        private void MainForm_OnSkinButtonClick(object sender, BtnEventArgs e)
        {
            frmSkinManager frm = new frmSkinManager();
            frm.ShowDialog();
            frm.Dispose();
        }

        #endregion

        #region Protect fileds

        /// <summary>
        /// 皮肤按钮区域
        /// </summary>
        /// <value>The skin box rect.</value>
        /// User:Ryan  CreateTime:2012-8-4 13:17.
        protected Rectangle SkinBoxRect
        {
            get
            {
                if (base.ControlBox)
                {
                    return new Rectangle(base.Width - 1 - base.ControlBoxSize.Width - base.MinimizeBoxRect.Width - base.MaximizeBoxRect.Width - base.CloseBoxRect.Width,
                       0, base.ControlBoxSize.Width, base.ControlBoxSize.Height);
                }

                return Rectangle.Empty;
            }
        }

        /// <summary>
        /// 水晶按钮区域
        /// </summary>
        /// <value>The ribbon BTN rect.</value>
        /// User:Ryan  CreateTime:2012-8-4 13:17.
        protected Rectangle RibbonBtnRect
        {
            get
            {
                return new Rectangle(this.Offset.X, this.CaptionHeight / 2 - this._RibbonButtonSize.Height / 2 + 1, this._RibbonButtonSize.Width,
                    this._RibbonButtonSize.Height);
            }
        }

        /// <summary>
        /// 重写Logo图标区域
        /// </summary>
        /// <value>The logo rect.</value>
        /// User:Ryan  CreateTime:2012-8-4 13:18.
        protected override Rectangle LogoRect
        {
            get
            {
                if (base.ShowIcon && this.CapitionLogo != null)
                {
                    return new Rectangle(this.RibbonBtnRect.Right + this.Offset.X, this.CaptionHeight / 2 - this.LogoSize.Height / 2 + 1, this.LogoSize.Width,
                    this.LogoSize.Height);
                }

                return Rectangle.Empty;
            }
        }

        #endregion

        #region Override methods

        #region WmNcHitTest

        /// <summary>
        /// 对窗体鼠标消息的处理
        /// </summary>
        /// <param name="m">windows窗体消息</param>
        /// User:Ryan  CreateTime:2011-07-28 10:22.
        protected override void WmNcHitTest(ref Message m)
        {
            int wparam = m.LParam.ToInt32();
            Point point = new Point(
                Win32.LOWORD(wparam), Win32.HIWORD(wparam));
            point = PointToClient(point);
            if (this.RibbonBtnRect.Contains(point))
            {
                m.Result = new IntPtr((int)NCHITTEST.HTCLIENT);
                return;
            }
            ////调整窗体大小
            if (this.ResizeEnable && this.CaptionHeight > 0)
            {
                int w = 4;
                if (point.X <= w && point.Y <= w)
                {
                    m.Result = new IntPtr((int)NCHITTEST.HTTOPLEFT);
                    return;
                }

                if (point.X >= (base.Width - w) && point.Y <= w)
                {
                    m.Result = new IntPtr((int)NCHITTEST.HTTOPRIGHT);
                    return;
                }

                if (point.X >= (base.Width - w) && point.Y >= (base.Height - w))
                {
                    m.Result = new IntPtr((int)NCHITTEST.HTBOTTOMRIGHT);
                    return;
                }

                if (point.X <= w && point.Y >= (base.Height - w))
                {
                    m.Result = new IntPtr((int)NCHITTEST.HTBOTTOMLEFT);
                    return;
                }

                if (point.Y <= w)
                {
                    m.Result = new IntPtr((int)NCHITTEST.HTTOP);
                    return;
                }

                if (point.Y >= (base.Height - w))
                {
                    m.Result = new IntPtr((int)NCHITTEST.HTBOTTOM);
                    return;
                }

                if (point.X <= w)
                {
                    m.Result = new IntPtr((int)NCHITTEST.HTLEFT);
                    return;
                }

                if (point.X >= (base.Width - w))
                {
                    m.Result = new IntPtr((int)NCHITTEST.HTRIGHT);
                    return;
                }
            }

            if (point.Y <= this.CaptionHeight)
            {
                if (!this.CloseBoxRect.Contains(point)
                    && !this.MaximizeBoxRect.Contains(point)
                    && !this.MinimizeBoxRect.Contains(point)
                    && !this.SkinBoxRect.Contains(point)
                    && !this.RibbonBtnRect.Contains(point)
                    )
                {
                    m.Result = new IntPtr((int)NCHITTEST.HTCAPTION);
                    return;
                }
                else
                {
                    ////终于算是找着你了，都可以移动了
                    m.Result = new IntPtr((int)NCHITTEST.HTCLIENT);
                    return;
                }
            }

            if (this.CaptionHeight > 0)
            {
                m.Result = new IntPtr((int)NCHITTEST.HTCAPTION);
            }
        }
        #endregion

        #region DrawControlBox

        /// <summary>
        /// Draws the control box.
        /// </summary>
        /// <param name="g">The g.</param>
        /// User:Ryan  CreateTime:2012-8-4 13:38.
        protected override void DrawControlBox(Graphics g)
        {
            base.DrawControlBox(g);
            this.DrawSkinControlBox(g);
        }
        #endregion

        #region DrawCaption

        /// <summary>
        /// Draws the caption.
        /// </summary>
        /// <param name="g">The g.</param>
        /// User:Ryan  CreateTime:2012-8-5 14:13.
        protected override void DrawCaption(Graphics g)
        {
            base.DrawCaption(g);
            if (this.CaptionHeight > 0)
            {
                this.DrawRibbonBtn(g);
            }
        }
        #endregion

        #region 鼠标事件扑捉

        protected override void ProcessMouseDown(Point p)
        {

            if (!this.RibbonBtnRect.IsEmpty && this.RibbonBtnRect.Contains(p))
            {
                this._RibbonBtnState = EnumControlState.Focused;
            }

            if (!this.SkinBoxRect.IsEmpty && this.SkinBoxRect.Contains(p))
            {
                this._SkinBtnState = EnumControlState.Focused;
            }

            base.ProcessMouseDown(p);
        }

        protected override void ProcessMouseUp(Point p)
        {
            if (!this.RibbonBtnRect.IsEmpty && this.RibbonBtnRect.Contains(p))
            {
                this._RibbonBtnState = EnumControlState.Default;
                if (this.OnRibbonButtonClick != null)
                {
                    BtnEventArgs e = new BtnEventArgs(this.RibbonBtnRect);
                    this.OnRibbonButtonClick(null, e);
                }
            }

            if (!this.SkinBoxRect.IsEmpty && this.SkinBoxRect.Contains(p))
            {
                this._SkinBtnState = EnumControlState.Default;
                if (this.OnSkinButtonClick != null)
                {
                    BtnEventArgs e = new BtnEventArgs(this.SkinBoxRect);
                    this.OnSkinButtonClick(null, e);
                }
            }

            base.ProcessMouseUp(p);
        }

        protected override void ProcessMouseLeave(Point p)
        {
            this._SkinBtnState = EnumControlState.Default;
            this._RibbonBtnState = EnumControlState.Default;
            base.ProcessMouseLeave(p);
        }

        protected override void ProcessMouseMove(Point p)
        {
            this._SkinBtnState = EnumControlState.Default;
            if (!this.RibbonBtnRect.IsEmpty && this.RibbonBtnRect.Contains(p))
            {
                this._RibbonBtnState = EnumControlState.HeightLight;
            }

            if (!this.SkinBoxRect.IsEmpty && this.SkinBoxRect.Contains(p))
            {
                this._SkinBtnState = EnumControlState.HeightLight;
            }

            base.ProcessMouseMove(p);
        }
        #endregion

        #endregion

        #region DrawSkinControlBox

        /// <summary>
        /// 绘制皮肤按钮
        /// </summary>
        /// <param name="g">The g.</param>
        /// User:Ryan  CreateTime:2012-8-4 13:37.
        private void DrawSkinControlBox(Graphics g)
        {
            if (!this.SkinBoxRect.IsEmpty)
            {
                base.ControlBoxRender.DrawControlBox(g, this.SkinBoxRect, this._SkinBtnState);
                GDIHelper.DrawImage(g, this.SkinBoxRect, Properties.Resources.skin, this._SkinBoxSize);
            }
        }

        #endregion

        #region DrawRibbonBtn

        /// <summary>
        /// 绘制水晶按钮
        /// </summary>
        /// <param name="g">The g.</param>
        /// User:Ryan  CreateTime:2012-8-5 14:13.
        protected void DrawRibbonBtn(Graphics g)
        {
            Rectangle exRect = new Rectangle(0, base.CaptionHeight, this.Width, this.Height - base.CaptionHeight + 1);
            g.SetClip(exRect, CombineMode.Exclude);
            GDIHelper.InitializeGraphics(g);
            Rectangle rect = this.RibbonBtnRect;
            rect.Inflate(-1, -1);
            GDIHelper.FillEllipse(g, rect, Color.White);
            Color c1 = Color.Empty, c2 = Color.Empty, c3 = Color.FromArgb(232, 246, 250);
            Blend blend = new Blend();
            blend.Positions = new float[] { 0f, 0.3f, 0.5f, 0.8f, 1f };
            blend.Factors = new float[] { 0.15f, 0.55f, 0.7f, 0.8f, 0.95f };
            switch (this._RibbonBtnState)
            {
                case EnumControlState.HeightLight:
                    c1 = Color.FromArgb(225, 179, 27);
                    c2 = Color.FromArgb(255, 251, 232);
                    break;
                case EnumControlState.Focused:
                    c1 = Color.FromArgb(191, 113, 5);
                    c2 = Color.FromArgb(248, 227, 222);
                    break;
                default:
                    c1 = Color.FromArgb(239, 246, 249);
                    c2 = Color.FromArgb(224, 221, 231);
                    blend.Positions = new float[] { 0f, 0.3f, 0.5f, 0.85f, 1f };
                    blend.Factors = new float[] { 0.95f, 0.70f, 0.45f, 0.3f, 0.15f };
                    break;
            }

            GDIHelper.DrawCrystalButton(g, rect, c1, c2, c3, blend);
            Color borderColor = Color.FromArgb(65, 177, 199);
            GDIHelper.DrawEllipseBorder(g, rect, borderColor, 1);
            Size imgSize = new System.Drawing.Size(20, 20);
            GDIHelper.DrawImage(g, rect, Properties.Resources.naruto, imgSize);
            g.ResetClip();
        }
        #endregion
    }
}
