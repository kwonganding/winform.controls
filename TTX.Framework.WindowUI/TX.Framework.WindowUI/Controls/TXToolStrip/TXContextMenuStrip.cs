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
    [ToolboxBitmap(typeof(ContextMenuStrip))]
    public class TXContextMenuStrip : ContextMenuStrip
    {
        //TXToolStripRenderer renderer;

        #region Initializes

        public TXContextMenuStrip()
            : base()
        {
            base.BackColor = SkinManager.CurrentSkin.BaseColor;
            //renderer = new TXToolStripRenderer();
            //this.Renderer = renderer;
            base.RenderMode = ToolStripRenderMode.ManagerRenderMode;

        }

        #endregion

        #region Properties

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

        //public new Color BackColor
        //{
        //    get { return base.BackColor; }
        //    set { base.BackColor = value; this.renderer.BackColor = value; base.Invalidate(); }
        //}

        #endregion
    }
}
