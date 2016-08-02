using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls
{
    [ToolboxItem(false)]
    public class TreeComboBoxContainer : UserControl
    {
        

        public TreeComboBoxContainer()
            : base()
        {
            BackColor = SkinManager.CurrentSkin.ThemeColor;
            BorderStyle = BorderStyle.FixedSingle;
            AutoScaleMode = AutoScaleMode.None;
            ResizeRedraw = true;
            MinimumSize = new Size(1, 1);
            MaximumSize = new Size(800, 1000);
            
            this.Padding = new Padding(0, 0, 0, 14);
            
        }

        protected override void WndProc(ref Message m)
        {
            if ((Parent as Popup).ProcessResizing(ref m))
            {
                return;
            }
            base.WndProc(ref m);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TreeComboBoxContainer
            // 
            this.Name = "TreeComboBoxContainer";
            this.Size = new System.Drawing.Size(250, 310);
            this.ResumeLayout(false);

        }
    }
}
