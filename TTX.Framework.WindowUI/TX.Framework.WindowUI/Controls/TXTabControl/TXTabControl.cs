using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Text;

namespace TX.Framework.WindowUI.Controls
{
    [ToolboxBitmap(typeof(TabControl))]
    public class TXTabControl : TabControl
    {
        #region Fields

        private UpDownButtonNativeWindow _upDownButtonNativeWindow;
        private Color _BaseTabolor = SkinManager.CurrentSkin.DefaultControlColor.First;
        private Color _BackColor = SkinManager.CurrentSkin.BaseColor;
        private Color _BorderColor = SkinManager.CurrentSkin.BorderColor;
        private Color _HeightLightTabColor = SkinManager.CurrentSkin.HeightLightControlColor.First;
        private Color _CheckedTabColor = SkinManager.CurrentSkin.FocusedControlColor.First;
        private int _TabCornerRadius = 3;
        private int _TabMargin = 0;
        private Font _CaptionFont = SystemFonts.DefaultFont;
        private Color _CaptionForceColor = SystemColors.ControlText;

        private const string UpDownButtonClassName = "msctls_updown32";

        private EnumTabStyle _TabStyle = EnumTabStyle.AnglesWing;

        private static readonly object EventPaintUpDownButton = new object();

        #endregion

        #region Constructors

        public TXTabControl()
            : base()
        {
            SetStyles();
            base.Appearance = TabAppearance.Buttons;
        }

        #endregion

        #region Events

        public event UpDownButtonPaintEventHandler PaintUpDownButton
        {
            add { base.Events.AddHandler(EventPaintUpDownButton, value); }
            remove { base.Events.RemoveHandler(EventPaintUpDownButton, value); }
        }

        #endregion

        #region Properties

        [Category("TXProperties")]
        [DefaultValue(typeof(Font), "DefaultFont")]
        [Description("标题栏的字体")]
        public Font CaptionFont
        {
            get { return this._CaptionFont; }
            set
            {
                if (value != null)
                {
                    this._CaptionFont = value;
                }
                else
                {
                    this._CaptionFont = SystemFonts.CaptionFont; ;
                }

                base.Invalidate();
            }
        }

        [Category("TXProperties")]
        [DefaultValue(typeof(Color), "ControlText")]
        [Description("标题栏的字体颜色")]
        public Color CaptionForceColor
        {
            get { return this._CaptionForceColor; }
            set
            {
                this._CaptionForceColor = value;
                base.Invalidate();
            }
        }

        [Category("TXProperties")]
        [Description("标签的基本背景色")]
        public Color BaseTabColor
        {
            get { return _BaseTabolor; }
            set
            {
                _BaseTabolor = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("标签按钮的展现方式")]
        [DefaultValue(TabAppearance.Buttons)]
        public new TabAppearance Appearance
        {
            get { return base.Appearance; }
            set
            {
                base.Appearance = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("标签之间的间距")]
        [DefaultValue(0)]
        public int TabMargin
        {
            get { return this._TabMargin; }
            set
            {
                this._TabMargin = value > 0 ? value : 0;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("标签按钮的边框样式")]
        [DefaultValue(EnumTabStyle.AnglesWing)]
        public EnumTabStyle TabStyle
        {
            get { return this._TabStyle; }
            set
            {
                this._TabStyle = value;
                base.Invalidate();
            }
        }

        [Browsable(true)]
        [Category("TXProperties")]
        [Description("基本背景色")]
        public override Color BackColor
        {
            get { return _BackColor; }
            set
            {
                _BackColor = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("边框色")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                _BorderColor = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("标签的高亮背景色")]
        public Color HeightLightTabColor
        {
            get { return _HeightLightTabColor; }
            set
            {
                _HeightLightTabColor = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("标签的选中背景色")]
        public Color CheckedTabColor
        {
            get { return this._CheckedTabColor; }
            set
            {
                this._CheckedTabColor = value;
                base.Invalidate(true);
            }
        }

        [Category("TXProperties")]
        [Description("标签的上面圆角半径值")]
        public int TabCornerRadius
        {
            get { return this._TabCornerRadius; }
            set
            {
                this._TabCornerRadius = value > 0 ? value : 0;
                base.Invalidate(true);
            }
        }

        internal IntPtr UpDownButtonHandle
        {
            get { return FindUpDownButton(); }
        }

        #endregion

        #region Protected Methods

        protected virtual void OnPaintUpDownButton(
            UpDownButtonPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;

            Color upButtonBaseColor = SkinManager.CurrentSkin.DefaultControlColor.First;
            Color upButtonBorderColor = this._BorderColor;
            Color upButtonArrowColor = Color.Green;

            Color downButtonBaseColor = SkinManager.CurrentSkin.DefaultControlColor.First;
            Color downButtonBorderColor = this._BorderColor;
            Color downButtonArrowColor = Color.Green;

            Rectangle upButtonRect = rect;
            upButtonRect.X += 2;
            upButtonRect.Y += 2;
            upButtonRect.Width = rect.Width / 2 - 4;
            upButtonRect.Height -= 4;

            Rectangle downButtonRect = rect;
            downButtonRect.X = upButtonRect.Right;
            downButtonRect.Y += 2;
            downButtonRect.Width = rect.Width / 2 - 4;
            downButtonRect.Height -= 4;

            if (Enabled)
            {
                if (e.MouseOver)
                {
                    if (e.MousePress)
                    {
                        if (e.MouseInUpButton)
                        {
                            upButtonBaseColor = SkinManager.CurrentSkin.HeightLightControlColor.First;
                        }
                        else
                        {
                            downButtonBaseColor = SkinManager.CurrentSkin.HeightLightControlColor.First;
                        }
                    }
                    else
                    {
                        if (e.MouseInUpButton)
                        {
                            upButtonBaseColor = SkinManager.CurrentSkin.DefaultControlColor.First;
                        }
                        else
                        {
                            downButtonBaseColor = SkinManager.CurrentSkin.DefaultControlColor.First;
                        }
                    }
                }
            }
            else
            {
                upButtonBaseColor = SystemColors.Control;
                upButtonBorderColor = SystemColors.ControlDark;
                upButtonArrowColor = SystemColors.ControlDark;
                downButtonBaseColor = SystemColors.Control;
                downButtonBorderColor = SystemColors.ControlDark;
                downButtonArrowColor = SystemColors.ControlDark;
            }

            GDIHelper.FillPath(g, new RoundRectangle(rect, new CornerRadius()), this._BackColor, this._BackColor);
            RenderButton(
                g,
                upButtonRect,
                upButtonBaseColor,
                upButtonBorderColor,
                upButtonArrowColor,
                ArrowDirection.Left);
            RenderButton(
                g,
                downButtonRect,
                downButtonBaseColor,
                downButtonBorderColor,
                downButtonArrowColor,
                ArrowDirection.Right);

            UpDownButtonPaintEventHandler handler =
                base.Events[EventPaintUpDownButton] as UpDownButtonPaintEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            base.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawTabContrl(e.Graphics);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (_upDownButtonNativeWindow != null)
            {
                _upDownButtonNativeWindow.Dispose();
                _upDownButtonNativeWindow = null;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        #endregion

        #region Help Methods

        private IntPtr FindUpDownButton()
        {
            return Win32.FindWindowEx(
                base.Handle,
                IntPtr.Zero,
                UpDownButtonClassName,
                null);
        }

        private void SetStyles()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
        }

        private void DrawTabContrl(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            DrawDrawBackgroundAndHeader(g);
            DrawTabPages(g);
            DrawBorder(g);
        }

        private void DrawDrawBackgroundAndHeader(Graphics g)
        {
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;

            switch (Alignment)
            {
                case TabAlignment.Top:
                    x = 0;
                    y = 0;
                    width = ClientRectangle.Width;
                    height = ClientRectangle.Height - DisplayRectangle.Height;
                    break;
                case TabAlignment.Bottom:
                    x = 0;
                    y = DisplayRectangle.Height;
                    width = ClientRectangle.Width;
                    height = ClientRectangle.Height - DisplayRectangle.Height;
                    break;
                case TabAlignment.Left:
                    x = 0;
                    y = 0;
                    width = ClientRectangle.Width - DisplayRectangle.Width;
                    height = ClientRectangle.Height;
                    break;
                case TabAlignment.Right:
                    x = DisplayRectangle.Width;
                    y = 0;
                    width = ClientRectangle.Width - DisplayRectangle.Width;
                    height = ClientRectangle.Height;
                    break;
            }

            Rectangle headerRect = new Rectangle(x, y, width, height);
            //Color backColor = Enabled ? _BackColor : SystemColors.Control;
            using (SolidBrush brush = new SolidBrush(this._BackColor))
            {
                g.FillRectangle(brush, ClientRectangle);
                g.FillRectangle(brush, headerRect);
            }
        }

        private void DrawTabPages(Graphics g)
        {
            Rectangle tabRect;
            Point cusorPoint = PointToClient(MousePosition);
            bool hover;
            bool selected;
            bool hasSetClip = false;
            bool alignHorizontal =
                (Alignment == TabAlignment.Top ||
                Alignment == TabAlignment.Bottom);
            LinearGradientMode mode = alignHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            if (alignHorizontal)
            {
                IntPtr upDownButtonHandle = UpDownButtonHandle;
                bool hasUpDown = upDownButtonHandle != IntPtr.Zero;
                if (hasUpDown)
                {
                    if (Win32.IsWindowVisible(upDownButtonHandle))
                    {
                        RECT upDownButtonRect = new RECT();
                        Win32.GetWindowRect(
                            upDownButtonHandle, ref upDownButtonRect);
                        Rectangle upDownRect = Rectangle.FromLTRB(
                            upDownButtonRect.left,
                            upDownButtonRect.top,
                            upDownButtonRect.right,
                            upDownButtonRect.bottom);
                        upDownRect = RectangleToClient(upDownRect);

                        switch (Alignment)
                        {
                            case TabAlignment.Top:
                                upDownRect.Y = 0;
                                break;
                            case TabAlignment.Bottom:
                                upDownRect.Y =
                                    ClientRectangle.Height - DisplayRectangle.Height;
                                break;
                        }
                        upDownRect.Height = ClientRectangle.Height;
                        g.SetClip(upDownRect, CombineMode.Exclude);
                        hasSetClip = true;
                    }
                }
            }

            for (int index = 0; index < base.TabCount; index++)
            {
                TabPage page = TabPages[index];
                tabRect = GetTabRect(index);
                hover = tabRect.Contains(cusorPoint);
                selected = SelectedIndex == index;
                Color baseColor = _BaseTabolor;
                Color borderColor = _BorderColor;
                Blend blend = new Blend();
                blend.Positions = new float[] { 0f, 0.3f, 0.5f, 0.7f, 1.0f };
                blend.Factors = new float[] { 0.1f, 0.3f, 0.5f, 0.8f, 1.0f };
                if (selected)
                {
                    baseColor = this._CheckedTabColor;
                }
                else if (hover)
                {
                    baseColor = this._HeightLightTabColor;
                    blend.Positions = new float[] { 0f, 0.3f, 0.6f, 0.8f, 1f };
                    blend.Factors = new float[] { .2f, 0.4f, 0.6f, 0.5f, .4f };
                }
                Rectangle exRect = new Rectangle(tabRect.Left, tabRect.Bottom, tabRect.Width, 1);
                g.SetClip(exRect, CombineMode.Exclude);
                CornerRadius cr = new CornerRadius(this._TabCornerRadius, this._TabCornerRadius, 0, 0);
                tabRect.X += this._TabMargin; tabRect.Width -= this._TabMargin;
                tabRect.Y++;
                tabRect.Height--;
                RoundRectangle roundRect = new RoundRectangle(tabRect, cr);
                GDIHelper.InitializeGraphics(g);
                switch (this._TabStyle)
                {
                    case EnumTabStyle.AnglesWing:
                        cr = new CornerRadius(this._TabCornerRadius);
                        tabRect.X += this._TabCornerRadius;tabRect.Width-=this._TabCornerRadius*2;
                        roundRect = new RoundRectangle(tabRect, cr);
                        using (GraphicsPath path = roundRect.ToGraphicsAnglesWingPath())
                        {
                            using (LinearGradientBrush brush = new LinearGradientBrush(roundRect.Rect, baseColor, this._BackColor, LinearGradientMode.Vertical))
                            {
                                brush.Blend = blend;
                                g.FillPath(brush, path);
                            }
                        }
                        using (GraphicsPath path = roundRect.ToGraphicsAnglesWingPath())
                        {
                            using (Pen pen = new Pen(this._BorderColor, 1))
                            {
                                g.DrawPath(pen, path);
                            }
                        }
                        break;
                    case EnumTabStyle.Default:
                        GDIHelper.FillPath(g, roundRect, baseColor, this._BackColor, blend);
                        GDIHelper.DrawPathBorder(g, roundRect, this._BorderColor);
                        break;
                }

                g.ResetClip();
                if (this.Alignment == TabAlignment.Top)
                {

                    Image img = null;
                    Size imgSize = Size.Empty;
                    if (this.ImageList != null && page.ImageIndex >= 0)
                    {
                        img = this.ImageList.Images[page.ImageIndex];
                        imgSize = img.Size;
                    }

                    GDIHelper.DrawImageAndString(g, tabRect, img, imgSize, page.Text, this._CaptionFont, this._CaptionForceColor);
                }
                else
                {
                    bool hasImage = DrawTabImage(g, page, tabRect);
                    DrawtabText(g, page, tabRect, hasImage);
                }
            }
            if (hasSetClip)
            {
                g.ResetClip();
            }
        }

        private void DrawtabText(
            Graphics g, TabPage page, Rectangle tabRect, bool hasImage)
        {
            Rectangle textRect = tabRect;
            RectangleF newTextRect;
            StringFormat sf;

            switch (Alignment)
            {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    if (hasImage)
                    {
                        textRect.X = tabRect.X + _TabCornerRadius / 2 + tabRect.Height - 2;
                        textRect.Width = tabRect.Width - _TabCornerRadius - tabRect.Height;
                    }

                    TextRenderer.DrawText(
                        g,
                        page.Text,
                        page.Font,
                        textRect,
                        page.ForeColor);
                    break;
                case TabAlignment.Left:
                    if (hasImage)
                    {
                        textRect.Height = tabRect.Height - tabRect.Width + 2;
                    }
                    g.TranslateTransform(textRect.X, textRect.Bottom);
                    g.RotateTransform(270F);
                    sf = new StringFormat(StringFormatFlags.NoWrap);
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.Character;
                    newTextRect = textRect;
                    newTextRect.X = 0;
                    newTextRect.Y = 0;
                    newTextRect.Width = textRect.Height;
                    newTextRect.Height = textRect.Width;
                    using (Brush brush = new SolidBrush(page.ForeColor))
                    {
                        g.DrawString(
                            page.Text,
                            page.Font,
                            brush,
                            newTextRect,
                            sf);
                    }
                    g.ResetTransform();
                    break;
                case TabAlignment.Right:
                    if (hasImage)
                    {
                        textRect.Y = tabRect.Y + _TabCornerRadius / 2 + tabRect.Width - 2;
                        textRect.Height = tabRect.Height - _TabCornerRadius - tabRect.Width;
                    }
                    g.TranslateTransform(textRect.Right, textRect.Y);
                    g.RotateTransform(90F);
                    sf = new StringFormat(StringFormatFlags.NoWrap);
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.Character;
                    newTextRect = textRect;
                    newTextRect.X = 0;
                    newTextRect.Y = 0;
                    newTextRect.Width = textRect.Height;
                    newTextRect.Height = textRect.Width;
                    using (Brush brush = new SolidBrush(page.ForeColor))
                    {
                        g.DrawString(
                            page.Text,
                            page.Font,
                            brush,
                            newTextRect,
                            sf);
                    }
                    g.ResetTransform();
                    break;
            }
        }

        private void DrawBorder(Graphics g)
        {
            if (SelectedIndex != -1)
            {
                Rectangle tabRect = GetTabRect(SelectedIndex);
                Rectangle clipRect = ClientRectangle;
                Point[] points = new Point[6];

                IntPtr upDownButtonHandle = UpDownButtonHandle;
                bool hasUpDown = upDownButtonHandle != IntPtr.Zero;
                if (hasUpDown)
                {
                    if (Win32.IsWindowVisible(upDownButtonHandle))
                    {
                        RECT upDownButtonRect = new RECT();
                        Win32.GetWindowRect(
                            upDownButtonHandle,
                            ref upDownButtonRect);
                        Rectangle upDownRect = Rectangle.FromLTRB(
                            upDownButtonRect.left,
                            upDownButtonRect.top,
                            upDownButtonRect.right,
                            upDownButtonRect.bottom);
                        upDownRect = RectangleToClient(upDownRect);

                        tabRect.X = tabRect.X > upDownRect.X ?
                            upDownRect.X : tabRect.X;
                        tabRect.Width = tabRect.Right > upDownRect.X ?
                            upDownRect.X - tabRect.X : tabRect.Width;
                    }
                }

                int margin = 0;
                if (this._TabStyle == EnumTabStyle.AnglesWing)
                {
                    margin = this._TabCornerRadius / 2;
                }

                switch (Alignment)
                {
                    case TabAlignment.Top:
                        points[0] = new Point(
                            tabRect.X + this._TabMargin + margin,
                            tabRect.Bottom);
                        points[1] = new Point(
                            clipRect.X,
                            tabRect.Bottom);
                        points[2] = new Point(
                            clipRect.X,
                            clipRect.Bottom - 1);
                        points[3] = new Point(
                            clipRect.Right - 1,
                            clipRect.Bottom - 1);
                        points[4] = new Point(
                            clipRect.Right - 1,
                            tabRect.Bottom);
                        points[5] = new Point(
                            tabRect.Right - margin,
                            tabRect.Bottom);
                        break;
                    case TabAlignment.Bottom:
                        points[0] = new Point(
                            tabRect.X,
                            tabRect.Y);
                        points[1] = new Point(
                            clipRect.X,
                            tabRect.Y);
                        points[2] = new Point(
                            clipRect.X,
                            clipRect.Y);
                        points[3] = new Point(
                            clipRect.Right - 1,
                            clipRect.Y);
                        points[4] = new Point(
                            clipRect.Right - 1,
                            tabRect.Y);
                        points[5] = new Point(
                            tabRect.Right,
                            tabRect.Y);
                        break;
                    case TabAlignment.Left:
                        points[0] = new Point(
                            tabRect.Right,
                            tabRect.Y);
                        points[1] = new Point(
                            tabRect.Right,
                            clipRect.Y);
                        points[2] = new Point(
                            clipRect.Right - 1,
                            clipRect.Y);
                        points[3] = new Point(
                            clipRect.Right - 1,
                            clipRect.Bottom - 1);
                        points[4] = new Point(
                            tabRect.Right,
                            clipRect.Bottom - 1);
                        points[5] = new Point(
                            tabRect.Right,
                            tabRect.Bottom);
                        break;
                    case TabAlignment.Right:
                        points[0] = new Point(
                            tabRect.X,
                            tabRect.Y);
                        points[1] = new Point(
                            tabRect.X,
                            clipRect.Y);
                        points[2] = new Point(
                            clipRect.X,
                            clipRect.Y);
                        points[3] = new Point(
                            clipRect.X,
                            clipRect.Bottom - 1);
                        points[4] = new Point(
                            tabRect.X,
                            clipRect.Bottom - 1);
                        points[5] = new Point(
                            tabRect.X,
                            tabRect.Bottom);
                        break;
                }
                using (Pen pen = new Pen(_BorderColor))
                {
                    g.DrawLines(pen, points);
                }
            }
        }

        internal void RenderArrowInternal(
             Graphics g,
             Rectangle dropDownRect,
             ArrowDirection direction,
             Brush brush)
        {
            Point point = new Point(
                dropDownRect.Left + (dropDownRect.Width / 2),
                dropDownRect.Top + (dropDownRect.Height / 2));
            Point[] points = null;
            switch (direction)
            {
                case ArrowDirection.Left:
                    points = new Point[] { 
                        new Point(point.X + 2, point.Y - 6), 
                        new Point(point.X + 2, point.Y + 6), 
                        new Point(point.X - 4, point.Y) };
                    break;

                case ArrowDirection.Up:
                    points = new Point[] { 
                        new Point(point.X - 4, point.Y + 2), 
                        new Point(point.X + 4, point.Y + 2), 
                        new Point(point.X, point.Y - 2) };
                    break;

                case ArrowDirection.Right:
                    points = new Point[] {
                        new Point(point.X - 2, point.Y - 6), 
                        new Point(point.X - 2, point.Y + 6), 
                        new Point(point.X + 4, point.Y) };
                    break;

                default:
                    points = new Point[] {
                        new Point(point.X - 4, point.Y - 2), 
                        new Point(point.X + 4, point.Y - 2), 
                        new Point(point.X, point.Y + 2) };
                    break;
            }
            g.FillPolygon(brush, points);
        }

        internal void RenderButton(
            Graphics g,
            Rectangle rect,
            Color baseColor,
            Color borderColor,
            Color arrowColor,
            ArrowDirection direction)
        {
            CornerRadius cr = new CornerRadius();
            switch (direction)
            {
                case ArrowDirection.Left:
                    cr = new CornerRadius(2, 0, 2, 0);
                    break;
                case ArrowDirection.Right:
                    cr = new CornerRadius(0, 2, 0, 2);
                    break;
            }

            RoundRectangle roundRect = new RoundRectangle(rect, cr);
            GDIHelper.FillPath(g, roundRect, baseColor, baseColor);
            GDIHelper.DrawPathBorder(g, roundRect);
            using (SolidBrush brush = new SolidBrush(arrowColor))
            {
                RenderArrowInternal(
                    g,
                    rect,
                    direction,
                    brush);
            }
        }

        internal void RenderTabBackgroundInternal(
          Graphics g,
          Rectangle rect,
          Color baseColor,
          Color borderColor,
          float basePosition,
          LinearGradientMode mode)
        {
            using (GraphicsPath path = CreateTabPath(rect))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                   rect, Color.Transparent, Color.Transparent, mode))
                {
                    Color[] colors = new Color[4];
                    colors[0] = GetColor(baseColor, 0, 35, 24, 9);
                    colors[1] = GetColor(baseColor, 0, 13, 8, 3);
                    colors[2] = baseColor;
                    colors[3] = GetColor(baseColor, 0, 68, 69, 54);

                    ColorBlend blend = new ColorBlend();
                    blend.Positions =
                        new float[] { 0.0f, basePosition, basePosition + 0.05f, 1.0f };
                    blend.Colors = colors;
                    brush.InterpolationColors = blend;
                    g.FillPath(brush, path);
                }

                if (baseColor.A > 80)
                {
                    Rectangle rectTop = rect;
                    if (mode == LinearGradientMode.Vertical)
                    {
                        rectTop.Height = (int)(rectTop.Height * basePosition);
                    }
                    else
                    {
                        rectTop.Width = (int)(rect.Width * basePosition);
                    }
                    using (SolidBrush brushAlpha =
                        new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                    {
                        g.FillRectangle(brushAlpha, rectTop);
                    }
                }

                rect.Inflate(-1, -1);
                using (GraphicsPath path1 = CreateTabPath(rect))
                {
                    using (Pen pen = new Pen(Color.FromArgb(255, 255, 255)))
                    {
                        if (Multiline)
                        {
                            g.DrawPath(pen, path1);
                        }
                        else
                        {
                            g.DrawLines(pen, path1.PathPoints);
                        }
                    }
                }

                using (Pen pen = new Pen(borderColor))
                {
                    if (Multiline)
                    {
                        g.DrawPath(pen, path);
                    }
                    {
                        g.DrawLines(pen, path.PathPoints);
                    }
                }
            }
        }

        private bool DrawTabImage(Graphics g, TabPage page, Rectangle rect)
        {
            bool hasImage = false;
            if (ImageList != null)
            {
                Image image = null;
                if (page.ImageIndex != -1)
                {
                    image = ImageList.Images[page.ImageIndex];
                }
                else if (page.ImageKey != null)
                {
                    image = ImageList.Images[page.ImageKey];
                }

                if (image != null)
                {
                    hasImage = true;
                    Rectangle destRect = Rectangle.Empty;
                    Rectangle srcRect = new Rectangle(Point.Empty, image.Size);
                    switch (Alignment)
                    {
                        case TabAlignment.Top:
                        case TabAlignment.Bottom:
                            destRect = new Rectangle(
                                 rect.X + _TabCornerRadius / 2 + 2,
                                 rect.Y + 2,
                                 rect.Height - 4,
                                 rect.Height - 4);
                            break;
                        case TabAlignment.Left:
                            destRect = new Rectangle(
                                rect.X + 2,
                                rect.Bottom - (rect.Width - 4) - _TabCornerRadius / 2 - 2,
                                rect.Width - 4,
                                rect.Width - 4);
                            break;
                        case TabAlignment.Right:
                            destRect = new Rectangle(
                                rect.X + 2,
                                rect.Y + _TabCornerRadius / 2 + 2,
                                rect.Width - 4,
                                rect.Width - 4);
                            break;
                    }

                    g.DrawImage(
                        image,
                        destRect,
                        srcRect,
                        GraphicsUnit.Pixel);
                }
            }
            return hasImage;
        }

        private GraphicsPath CreateTabPath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            switch (Alignment)
            {
                case TabAlignment.Top:
                    rect.X++;
                    rect.Width -= 2;
                    path.AddLine(
                        rect.X,
                        rect.Bottom,
                        rect.X,
                        rect.Y + _TabCornerRadius / 2);
                    path.AddArc(
                        rect.X,
                        rect.Y,
                        _TabCornerRadius,
                        _TabCornerRadius,
                        180F,
                        90F);
                    path.AddArc(
                        rect.Right - _TabCornerRadius,
                        rect.Y,
                        _TabCornerRadius,
                        _TabCornerRadius,
                        270F,
                        90F);
                    path.AddLine(
                        rect.Right,
                        rect.Y + _TabCornerRadius / 2,
                        rect.Right,
                        rect.Bottom);
                    break;
                case TabAlignment.Bottom:
                    rect.X++;
                    rect.Width -= 2;
                    path.AddLine(
                        rect.X,
                        rect.Y,
                        rect.X,
                        rect.Bottom - _TabCornerRadius / 2);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - _TabCornerRadius,
                        _TabCornerRadius,
                        _TabCornerRadius,
                        180,
                        -90);
                    path.AddArc(
                        rect.Right - _TabCornerRadius,
                        rect.Bottom - _TabCornerRadius,
                        _TabCornerRadius,
                        _TabCornerRadius,
                        90,
                        -90);
                    path.AddLine(
                        rect.Right,
                        rect.Bottom - _TabCornerRadius / 2,
                        rect.Right,
                        rect.Y);

                    break;
                case TabAlignment.Left:
                    rect.Y++;
                    rect.Height -= 2;
                    path.AddLine(
                        rect.Right,
                        rect.Y,
                        rect.X + _TabCornerRadius / 2,
                        rect.Y);
                    path.AddArc(
                        rect.X,
                        rect.Y,
                        _TabCornerRadius,
                        _TabCornerRadius,
                        270F,
                        -90F);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - _TabCornerRadius,
                        _TabCornerRadius,
                        _TabCornerRadius,
                        180F,
                        -90F);
                    path.AddLine(
                        rect.X + _TabCornerRadius / 2,
                        rect.Bottom,
                        rect.Right,
                        rect.Bottom);
                    break;
                case TabAlignment.Right:
                    rect.Y++;
                    rect.Height -= 2;
                    path.AddLine(
                        rect.X,
                        rect.Y,
                        rect.Right - _TabCornerRadius / 2,
                        rect.Y);
                    path.AddArc(
                        rect.Right - _TabCornerRadius,
                        rect.Y,
                        _TabCornerRadius,
                        _TabCornerRadius,
                        270F,
                        90F);
                    path.AddArc(
                        rect.Right - _TabCornerRadius,
                        rect.Bottom - _TabCornerRadius,
                        _TabCornerRadius,
                        _TabCornerRadius,
                        0F,
                        90F);
                    path.AddLine(
                        rect.Right - _TabCornerRadius / 2,
                        rect.Bottom,
                        rect.X,
                        rect.Bottom);
                    break;
            }
            path.CloseFigure();
            return path;
        }

        private Color GetColor(Color colorBase, int a, int r, int g, int b)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a + a0 > 255) { a = 255; } else { a = Math.Max(a + a0, 0); }
            if (r + r0 > 255) { r = 255; } else { r = Math.Max(r + r0, 0); }
            if (g + g0 > 255) { g = 255; } else { g = Math.Max(g + g0, 0); }
            if (b + b0 > 255) { b = 255; } else { b = Math.Max(b + b0, 0); }

            return Color.FromArgb(a, r, g, b);
        }

        #endregion

        #region UpDownButtonNativeWindow

        private class UpDownButtonNativeWindow : NativeWindow, IDisposable
        {
            private TXTabControl _owner;
            private bool _bPainting;

            public UpDownButtonNativeWindow(TXTabControl owner)
                : base()
            {
                _owner = owner;
                base.AssignHandle(owner.UpDownButtonHandle);
            }

            private bool LeftKeyPressed()
            {
                if (SystemInformation.MouseButtonsSwapped)
                {
                    return (Win32.GetKeyState((int)KeyStatesMasks.VK_RBUTTON) < 0);
                }
                else
                {
                    return (Win32.GetKeyState((int)KeyStatesMasks.VK_LBUTTON) < 0);
                }
            }

            private void DrawUpDownButton()
            {
                bool mouseOver = false;
                bool mousePress = LeftKeyPressed();
                bool mouseInUpButton = false;

                RECT rect = new RECT();

                Win32.GetClientRect(base.Handle, ref rect);

                Rectangle clipRect = Rectangle.FromLTRB(
                    rect.top, rect.left, rect.right, rect.bottom);

                Point cursorPoint = new Point();
                Win32.GetCursorPos(ref cursorPoint);
                Win32.GetWindowRect(base.Handle, ref rect);

                mouseOver = Win32.PtInRect(ref rect, cursorPoint);

                cursorPoint.X -= rect.left;
                cursorPoint.Y -= rect.top;

                mouseInUpButton = cursorPoint.X < clipRect.Width / 2;

                using (Graphics g = Graphics.FromHwnd(base.Handle))
                {
                    UpDownButtonPaintEventArgs e =
                        new UpDownButtonPaintEventArgs(
                        g,
                        clipRect,
                        mouseOver,
                        mousePress,
                        mouseInUpButton);
                    _owner.OnPaintUpDownButton(e);
                }
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case (int)WindowMessages.WM_PAINT:
                        if (!_bPainting)
                        {
                            PAINTSTRUCT ps = new PAINTSTRUCT();
                            _bPainting = true;
                            Win32.BeginPaint(m.HWnd, ref ps);
                            DrawUpDownButton();
                            Win32.EndPaint(m.HWnd, ref ps);
                            _bPainting = false;
                            m.Result = Win32.TRUE;
                        }
                        else
                        {
                            base.WndProc(ref m);
                        }
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }

            #region IDisposable 成员

            public void Dispose()
            {
                _owner = null;
                base.ReleaseHandle();
            }

            #endregion
        }

        #endregion
    }
}
