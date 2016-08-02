using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Forms
{
    public class WaitWindow
    {
        internal delegate void MethodInvoker<T>(T parameter1);
        private TXWaitingBox frmWaitWindow;

        private WaitWindow()
        {
        }

        public static object Show(EventHandler<WaitWindowEventArgs> workerMethod)
        {
            return WaitWindow.Show(workerMethod, null);
        }

        public static object Show(EventHandler<WaitWindowEventArgs> workerMethod, string message)
        {
            WaitWindow instance = new WaitWindow();
            return instance.Show(workerMethod, message, new List<object>());
        }

        public static object Show(EventHandler<WaitWindowEventArgs> workerMethod, string message, params object[] args)
        {
            List<object> arguments = new List<object>();
            arguments.AddRange(args);

            WaitWindow instance = new WaitWindow();
            return instance.Show(workerMethod, message, arguments);
        }

        public void Cancel()
        {
            this.frmWaitWindow.Invoke(new MethodInvoker(this.frmWaitWindow.Cancel), null);
        }

        private object Show(EventHandler<WaitWindowEventArgs> workerMethod, string message, List<object> args)
        {
            Guard.ArgumentNotNull(workerMethod, "workerMethod");

            this.WorkerMethod = workerMethod;
            this.Args = args;


            this.frmWaitWindow = new TXWaitingBox(this);
            this.frmWaitWindow.SetMessage(message);
            this.frmWaitWindow.ShowDialog();
            object result = this.frmWaitWindow.Result;
            Exception ex = this.frmWaitWindow.Error;
            this.frmWaitWindow.Dispose();

            if (ex != null)
            {
                throw ex;
            }

            return result;
        }

        public string Message
        {
            set { this.frmWaitWindow.Invoke(new MethodInvoker<string>(this.frmWaitWindow.SetMessage), value); }
        }

        internal EventHandler<WaitWindowEventArgs> WorkerMethod { get; set; }
        internal List<object> Args { get; set; }
    }

    public class WaitWindowEventArgs : EventArgs
    {
        public WaitWindowEventArgs()
        {
            this.Arguments = new List<object>();
        }

        public WaitWindowEventArgs(WaitWindow gui, List<object> args)
            : base()
        {
            this.Window = gui;
            this.Arguments = args;
        }

        /// <summary>
        /// 
        /// </summary>
        public WaitWindow Window { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public List<object> Arguments { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public object Result { get; set; }
    }
}
