using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls
{
    [ToolboxBitmap(typeof(MenuStrip))]
    public class TXMenuStrip : MenuStrip
    {
        #region fileds

        private Color _BeginBackColor = SkinManager.CurrentSkin.BaseColor;

        private Color _EndBackColor = SkinManager.CurrentSkin.BaseColor;

        #endregion

        #region Initializes

        public TXMenuStrip()
        {
            base.BackColor = SkinManager.CurrentSkin.BaseColor;
            base.RenderMode = ToolStripRenderMode.ManagerRenderMode;
        }

        #endregion

        #region Properties

        [Category("TXProperties")]
        [Description("背景色")]
        [Browsable(false)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set
            {

                base.BackColor = value;
                base.Invalidate();
            }
        }

        [Category("TXProperties")]
        [DefaultValue(typeof(ToolStripRenderMode), "ManagerRenderMode")]
        public new ToolStripRenderMode RenderMode
        {
            get { return base.RenderMode; }
            set
            {

                base.RenderMode = value;
                base.Invalidate();
            }
        }

        [Category("TXProperties")]
        [Description("开始部分背景色")]
        public Color BeginBackColor
        {
            get { return this._BeginBackColor; }
            set
            {
                this._BeginBackColor = value;
                base.Invalidate();
            }
        }

        [Category("TXProperties")]
        [Description("后面部分背景色")]
        public Color EndBackColor
        {
            get { return this._EndBackColor; }
            set
            {
                this._EndBackColor = value;
                base.Invalidate();
            }
        }

        #endregion
    }
}
