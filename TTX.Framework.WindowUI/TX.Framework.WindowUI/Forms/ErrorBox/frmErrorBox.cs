using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace TX.Framework.WindowUI.Forms
{
    /// <summary>
    /// 显示系统错误的窗口界面
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-14 19:19.
    public partial class frmErrorBox : FormInfoEntity
    {
        public frmErrorBox()
        {
            InitializeComponent();
            Random ran = new Random( DateTime.Now.Second );
            this.Text = this.iErrorCaptions[ran.Next( 0, this.iErrorCaptions.Length )];
            this.ShowInTaskbar = true;
        }

        public void SetException( Exception exception )
        {
            txtError.Text = exception.Message;
            txtErrorDetail.Text = exception.StackTrace;
            textBox1.Text = exception.GetType().Name;
        }

        public static void ShowError( Exception ex )
        {
            frmErrorBox f = new frmErrorBox();
            try
            {
                f.SetException( ex );
                f.ShowDialog();
            }
            finally
            {
                f.Dispose();
            }
        }

        public static void ShowError( string friendMessage, Exception ex )
        {
            if ( ex == null )
            {
                return;
            }
            frmErrorBox f = new frmErrorBox();
            Exception realex = getrealexp( ex );
            try
            {
                string message = string.Empty;
                if ( realex is MessageSecurityException )
                {
                    FaultException exception = realex.InnerException as FaultException;
                    if ( exception != null )
                    {
                        message = exception.Message;
                    }
                }

                f.SetException( realex );
                f.txtError.Text = friendMessage + message;
                f.ShowDialog();
            }
            finally
            {
                f.Dispose();
            }
        }

        private static Exception getrealexp( Exception exp )
        {
            if ( exp is TargetInvocationException )
            {
                return getrealexp( ( exp as TargetInvocationException ).InnerException );
            }
            else
            {
                return exp;
            }
        }

        private void frmErrorBox_Load( object sender, EventArgs e )
        {

        }

        private void btnOK_Click_1( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
