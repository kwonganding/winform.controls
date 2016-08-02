//#####################################################################################
//Ryan-2010-9-19
//说明：HTML编辑器下载自网络开源代码（http://www.cnpopsoft.com ）,在其基础之上扩展的新的功
//能，进一步完善了原有的功能。
//#####################################################################################

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TX.Framework.WindowUI.Controls
{
    /// <summary>
    /// Html编辑器
    /// </summary>
    [Description( "Html编辑器" ), ClassInterface( ClassInterfaceType.AutoDispatch )]
    [ToolboxBitmap( typeof( WebBrowser ) )]
    public partial class TXHtmlEditor : UserControl
    {
        #region fileds

        /// <summary>
        /// 圆角值
        /// </summary>
        private int _CornerRadius = 2;

        private Color _BorderColor = SkinManager.CurrentSkin.BorderColor;

        private Color _HeightLightBolorColor = SkinManager.CurrentSkin.HeightLightControlColor.First;

        /// <summary>
        /// 当前控件状态
        /// </summary>
        private EnumControlState _ControlState = EnumControlState.Default;

        public Func<string, string> OnLocalImageUpdate;

        
        #endregion

        #region Initializes

        public TXHtmlEditor()
        {
            dataUpdate = 0;
            InitializeComponent();
            InitializeControls();
            this.webBrowserBody.GotFocus += new EventHandler( webBrowserBody_GotFocus );
            this.webBrowserBody.LostFocus += new EventHandler( webBrowserBody_LostFocus );
            this.webBrowserBody.Navigating += new WebBrowserNavigatingEventHandler( webBrowserBody_Navigating );
        }


        #endregion

        #region 扩展属性

        [Category( "TXProperties" )]
        [Browsable( true )]
        [Description( "圆角的半径值" )]
        [DefaultValue( 2 )]
        public int CornerRadius
        {
            get
            {
                return this._CornerRadius;
            }
            set
            {
                this._CornerRadius = value;
                this.Invalidate();
            }
        }

        [Browsable( false )]
        public new BorderStyle BorderStyle
        {
            get
            {
                return BorderStyle.None;
            }
        }

        [Category( "TXProperties" )]
        [Browsable( true )]
        [Description( "边框颜色" )]
        public Color BorderColor
        {
            get
            {
                return this._BorderColor;
            }
            set
            {
                this._BorderColor = value;
                base.Invalidate();
            }
        }

        [Category( "TXProperties" )]
        [Browsable( true )]
        [Description( "边框的高亮色" )]
        public Color HeightLightBolorColor
        {
            get
            {
                return this._HeightLightBolorColor;
            }
            set
            {
                this._HeightLightBolorColor = value;
            }

        }
        /// <summary>
        /// 获取全部Html文本
        /// </summary>
        public string Html
        {
            get
            {
                return webBrowserBody.DocumentText;
            }
        }


        /// <summary>
        /// Gets  the inner HTML text
        /// </summary>
        /// <value>The inner HTML text.</value>
        /// User:Ryan  CreateTime:2011-08-09 17:45.
        public string InnerHtml
        {
            get
            {
                return this.webBrowserBody.Document.Body.InnerHtml;
            }
        }

        /// <summary>
        /// 获取和设置当前的Text值
        /// </summary>
        public override string Text
        {
            get
            {
                string str = webBrowserBody.Document.Body == null ? string.Empty : webBrowserBody.Document.Body.InnerText;
                return string.IsNullOrEmpty( str ) ? string.Empty : str;
            }
            set
            {
                webBrowserBody.DocumentText = value.Replace( "\r\n", "<br>" );
            }
        }


        /// <summary>
        /// 获取插入的图片名称集合
        /// </summary>
        public string[ ] Images
        {
            get
            {
                List<string> images = new List<string>();

                foreach( HtmlElement element in webBrowserBody.Document.Images )
                {
                    string image = element.GetAttribute( "src" );
                    if( !images.Contains( image ) )
                    {
                        images.Add( image );
                    }
                }

                return images.ToArray();
            }
        }

        #endregion

        #region Override methods

        protected override void OnPaint( PaintEventArgs e )
        {
            base.OnPaint( e );
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle( 0, 0, this.Width - 1, this.Height - 1 );
            rect.Inflate( -1, -1 );
            GDIHelper.InitializeGraphics( g );
            GDIHelper.DrawPathBorder( g, new RoundRectangle( rect, this._CornerRadius ), this._BorderColor );
            if( this._ControlState == EnumControlState.HeightLight )
            {
                GDIHelper.DrawPathOuterBorder( g, new RoundRectangle( rect, this._CornerRadius ), this._HeightLightBolorColor );
            }
        }

        void webBrowserBody_LostFocus( object sender, EventArgs e )
        {
            this._ControlState = EnumControlState.Default;
            this.Invalidate();
        }

        void webBrowserBody_Navigating( object sender, WebBrowserNavigatingEventArgs e )
        {
            this.webBrowserBody.Document.Write( "<script>functionshowModalDialog{return;}" );
        }

        void webBrowserBody_GotFocus( object sender, EventArgs e )
        {
            this._ControlState = EnumControlState.HeightLight;
            this.Invalidate();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 控件初始化
        /// </summary>
        private void InitializeControls()
        {
            BeginUpdate();
            //工具栏
            foreach( FontFamily family in FontFamily.Families )
            {
                toolStripComboBoxName.Items.Add( family.Name );
            }

            toolStripComboBoxSize.Items.AddRange( FontSize.All.ToArray() );
            toolStripComboBoxSize.SelectedIndex = 1;
            toolStripComboBoxName.SelectedText = "宋体";

            //浏览器            
            webBrowserBody.DocumentText = string.Empty;
            webBrowserBody.Document.Click += new HtmlElementEventHandler( webBrowserBody_DocumentClick );
            webBrowserBody.Document.Focusing += new HtmlElementEventHandler( webBrowserBody_DocumentFocusing );
            webBrowserBody.Document.ExecCommand( "EditMode", false, null );
            webBrowserBody.Document.ExecCommand( "LiveResize", false, null );
            EndUpdate();
        }

        /// <summary>
        /// 刷新按钮状态
        /// </summary>
        private void RefreshToolBar()
        {
            BeginUpdate();
            try
            {
                mshtml.IHTMLDocument2 document = ( mshtml.IHTMLDocument2 )webBrowserBody.Document.DomDocument;

                toolStripComboBoxName.Text = document.queryCommandValue( "FontName" ).ToString();
                toolStripComboBoxSize.SelectedItem = FontSize.Find( ( int )document.queryCommandValue( "FontSize" ) );
                toolStripButtonBold.Checked = document.queryCommandState( "Bold" );
                toolStripButtonItalic.Checked = document.queryCommandState( "Italic" );
                toolStripButtonUnderline.Checked = document.queryCommandState( "Underline" );

                toolStripButtonNumbers.Checked = document.queryCommandState( "InsertOrderedList" );
                toolStripButtonBullets.Checked = document.queryCommandState( "InsertUnorderedList" );

                toolStripButtonLeft.Checked = document.queryCommandState( "JustifyLeft" );
                toolStripButtonCenter.Checked = document.queryCommandState( "JustifyCenter" );
                toolStripButtonRight.Checked = document.queryCommandState( "JustifyRight" );
                toolStripButtonFull.Checked = document.queryCommandState( "JustifyFull" );
            }
            catch( Exception e )
            {
                System.Diagnostics.Debug.WriteLine( e );
            }
            finally
            {
                EndUpdate();
            }
        }

        #endregion

        #region 更新相关

        private int dataUpdate;
        private bool Updating
        {
            get
            {
                return dataUpdate != 0;
            }
        }

        private void BeginUpdate()
        {
            ++dataUpdate;
        }
        private void EndUpdate()
        {
            --dataUpdate;
        }

        #endregion

        #region 工具栏

        private void toolStripComboBoxName_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "FontName", false, toolStripComboBoxName.Text );
        }
        private void toolStripComboBoxSize_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            int size = ( toolStripComboBoxSize.SelectedItem == null ) ? 1 : ( toolStripComboBoxSize.SelectedItem as FontSize ).Value;
            webBrowserBody.Document.ExecCommand( "FontSize", false, size );
        }
        private void toolStripButtonBold_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "Bold", false, null );
            RefreshToolBar();
        }
        private void toolStripButtonItalic_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "Italic", false, null );
            RefreshToolBar();
        }
        private void toolStripButtonUnderline_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "Underline", false, null );
            RefreshToolBar();
        }
        private void toolStripButtonColor_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }
            ColorDialog dialog = new ColorDialog();
            try
            {
                int fontcolor = ( int )( ( mshtml.IHTMLDocument2 )webBrowserBody.Document.DomDocument ).queryCommandValue( "ForeColor" );
                dialog.Color = Color.FromArgb( 0xff, fontcolor & 0xff, ( fontcolor >> 8 ) & 0xff, ( fontcolor >> 16 ) & 0xff );
            }
            catch
            {
                dialog.Color = Color.Black;
            }

            DialogResult result = dialog.ShowDialog();
            if( result == DialogResult.OK )
            {
                string color = dialog.Color.Name;
                if( !dialog.Color.IsNamedColor )
                {
                    color = "#" + color.Remove( 0, 2 );
                }

                webBrowserBody.Document.ExecCommand( "ForeColor", false, color );
            }
            RefreshToolBar();
        }

        private void toolStripButtonNumbers_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "InsertOrderedList", false, null );
            RefreshToolBar();
        }
        private void toolStripButtonBullets_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "InsertUnorderedList", false, null );
            RefreshToolBar();
        }
        private void toolStripButtonOutdent_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "Outdent", false, null );
            RefreshToolBar();
        }
        private void toolStripButtonIndent_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "Indent", false, null );
            RefreshToolBar();
        }

        private void toolStripButtonLeft_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "JustifyLeft", false, null );
            RefreshToolBar();
        }
        private void toolStripButtonCenter_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "JustifyCenter", false, null );
            RefreshToolBar();
        }
        private void toolStripButtonRight_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "JustifyRight", false, null );
            RefreshToolBar();
        }
        private void toolStripButtonFull_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "JustifyFull", false, null );
            RefreshToolBar();
        }

        private void toolStripButtonLine_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "InsertHorizontalRule", false, null );
            RefreshToolBar();
        }
        private void toolStripButtonHyperlink_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }

            webBrowserBody.Document.ExecCommand( "CreateLink", true, null );
            RefreshToolBar();
        }
        private void toolStripButtonPicture_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }
            frmAddImage f = new frmAddImage();
            f.OnImageUpdate = this.OnLocalImageUpdate;
            f._ReturnValue = insertImage;
            f.ShowDialog();
            f.Dispose();
        }
        private void insertImage( string imageUrl )
        {
            webBrowserBody.Document.ExecCommand( "InsertImage", false, imageUrl );
            RefreshToolBar();
        }
        private void toolStripButtonSourceCode_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }
            frmSourceCode f = new frmSourceCode( webBrowserBody.DocumentText );
            f._ReturnValue = UpdateDocumentText;
            f.ShowDialog();
            f.Dispose();
        }
        private void UpdateDocumentText( string DocumentText )
        {
            webBrowserBody.Document.OpenNew( true );//解决跟新其documentText后弹出修改提示框。终于找到你了！小样
            this.Text = DocumentText;
            webBrowserBody.Document.OpenNew( true );
            RefreshToolBar();
        }
        private void toolStripSplitButtonPreview_Click( object sender, EventArgs e )
        {
            if( Updating )
            {
                return;
            }
            frmPreview f = new frmPreview( webBrowserBody.DocumentText );
            f.ShowDialog();
            f.Dispose();
        }

        #endregion

        #region 浏览器

        private void webBrowserBody_DocumentCompleted( object sender, WebBrowserDocumentCompletedEventArgs e )
        {
        }

        private void webBrowserBody_PreviewKeyDown( object sender, PreviewKeyDownEventArgs e )
        {
            if( e.IsInputKey )
            {
                return;
            }
            RefreshToolBar();
        }

        private void webBrowserBody_DocumentClick( object sender, HtmlElementEventArgs e )
        {
            RefreshToolBar();
        }

        private void webBrowserBody_DocumentFocusing( object sender, HtmlElementEventArgs e )
        {
            RefreshToolBar();
        }

        #endregion

        #region 字体大小转换

        private class FontSize
        {
            private static List<FontSize> allFontSize = null;
            public static List<FontSize> All
            {
                get
                {
                    if( allFontSize == null )
                    {
                        allFontSize = new List<FontSize>();
                        allFontSize.Add( new FontSize( 8, 1 ) );
                        allFontSize.Add( new FontSize( 10, 2 ) );
                        allFontSize.Add( new FontSize( 12, 3 ) );
                        allFontSize.Add( new FontSize( 14, 4 ) );
                        allFontSize.Add( new FontSize( 18, 5 ) );
                        allFontSize.Add( new FontSize( 24, 6 ) );
                        allFontSize.Add( new FontSize( 36, 7 ) );
                    }

                    return allFontSize;
                }
            }

            public static FontSize Find( int value )
            {
                if( value < 1 )
                {
                    return All[0];
                }

                if( value > 7 )
                {
                    return All[6];
                }

                return All[value - 1];
            }

            private FontSize( int display, int value )
            {
                displaySize = display;
                valueSize = value;
            }

            private int valueSize;
            public int Value
            {
                get
                {
                    return valueSize;
                }
            }

            private int displaySize;
            public int Display
            {
                get
                {
                    return displaySize;
                }
            }

            public override string ToString()
            {
                return displaySize.ToString();
            }
        }

        #endregion

        #region 下拉框

        private class ToolStripComboBoxEx : ToolStripComboBox
        {
            public override Size GetPreferredSize( Size constrainingSize )
            {
                Size size = base.GetPreferredSize( constrainingSize );
                size.Width = Math.Max( Width, 0x20 );
                return size;
            }
        }

        #endregion


    }
}
