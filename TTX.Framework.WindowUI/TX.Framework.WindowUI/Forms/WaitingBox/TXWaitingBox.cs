using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TX.Framework.WindowUI.Forms
{
    public partial class TXWaitingBox : BaseForm
    {
        #region fileds

        private delegate T FunctionInvoker<T>();

        private WaitWindow _Parent;

        private IAsyncResult _ThreadResult;

        #endregion

        #region Initailizes

        public TXWaitingBox(WaitWindow parent)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            _Parent = parent;
            this.Opacity = 0.95f;
            this.labWaitMessage.Text = "正在处理，请稍候..."
                + "\n"
                + "Dear,Please wait a moment !";
            this.txPanel1.BackEndColor = this.GetRandomColor();
            ControlHelper.BindMouseMoveEvent(this.labWaitMessage);
            //加载的图片
            this.loadImage.Image = LoadResource.GetRandomLoadImage();
        }
        #endregion

        #region override

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WindowMessages.WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == 61539 || m.WParam.ToInt32() == 61587)
                {
                    return;
                }
            }

            base.WndProc(ref m);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            FunctionInvoker<object> threadController = new FunctionInvoker<object>(this.DoWork);
            this._ThreadResult = threadController.BeginInvoke(this.WorkComplete, threadController);
        }

        #endregion

        #region private methods

        internal object DoWork()
        {
            WaitWindowEventArgs e = new WaitWindowEventArgs(_Parent, _Parent.Args);
            if ((this._Parent.WorkerMethod != null))
            {
                this._Parent.WorkerMethod(this, e);
            }
            return e.Result;
        }

        private void WorkComplete(IAsyncResult results)
        {
            if (!this.IsDisposed)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new WaitWindow.MethodInvoker<IAsyncResult>(this.WorkComplete), results);
                }
                else
                {
                    try
                    {
                        this.Result = ((FunctionInvoker<object>)results.AsyncState).EndInvoke(results);
                    }
                    catch (Exception ex)
                    {
                        this.Error = ex;
                    }
                    this.Close();
                }
            }
        }

        internal void SetMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                this.labWaitMessage.Text = message;
            }
        }

        internal void Cancel()
        {
            this.Invoke(new MethodInvoker(this.Close), null);
        }

        #region GetRandomColor

        private Color GetRandomColor()
        {
            int rMax = 248;
            int rMin = 204;
            int gMax = 250;
            int gMin = 215;
            int bMax = 250;
            int bMin = 240;
            Random random = new Random(DateTime.Now.Millisecond);
            int r = random.Next(rMin, rMax);
            int g = random.Next(gMin, gMax);
            int b = random.Next(bMin, bMax);
            Color color = Color.FromArgb(r, g, b);
            return color;
        }

        #endregion

        #endregion

        internal object Result { get; set; }

        internal new Exception Error { get; set; }
    }
}
