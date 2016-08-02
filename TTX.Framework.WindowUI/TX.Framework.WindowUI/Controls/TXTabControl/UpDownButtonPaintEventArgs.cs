using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TX.Framework.WindowUI.Controls
{
    public delegate void UpDownButtonPaintEventHandler(
        object sender,
        UpDownButtonPaintEventArgs e);

    public class UpDownButtonPaintEventArgs : PaintEventArgs
    {
        private bool _mouseOver;
        private bool _mousePress;
        private bool _mouseInUpButton;

        public UpDownButtonPaintEventArgs(
            Graphics graphics,
            Rectangle clipRect,
            bool mouseOver,
            bool mousePress,
            bool mouseInUpButton)
            : base(graphics, clipRect)
        {
            _mouseOver = mouseOver;
            _mousePress = mousePress;
            _mouseInUpButton = mouseInUpButton;
        }

        public bool MouseOver
        {
            get { return _mouseOver; }
        }

        public bool MousePress
        {
            get { return _mousePress; }
        }

        public bool MouseInUpButton
        {
            get { return _mouseInUpButton; }
        }
    }
}
