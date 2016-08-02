using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;
using TX.Framework.WindowUI.Controls;

namespace TX.Framework.WindowUI.Forms
{
    public partial class BaseForm
    {
        #region private attribute

        /// <summary>
        /// 显示的等待框
        /// </summary>
        private System.Windows.Forms.Panel waitingBox;

        /// <summary>
        /// 等候的标准信息
        /// </summary>
        private readonly string[] iWaittingMessage = new string[] { 
            "请稍等......",
            "亲，我们正努力的处理，请稍等... "
            ,"The request is being processed,please wait..."
            ,"有时候，等待也是一种幸福..."
            ,"您的指令已收到，正在非常努力的执行..."
            ,"请稍等，让我来见证那奇迹的诞生..."
            ,"有一种等待，叫做望穿秋水..."
            ,"生命是一个奋斗的过程，也是等待的过程..."
            ,"有一种幸福，叫做等待..."
            ,"等待，本身也是一种勇气！"
            ,"星云大师：在等待的日子里，刻苦读书，谦卑做人，养得深根，日后才能枝叶茂盛！"
            ,"有些人注定是等待别人的，有些人是注定被人等候的。"
            ,"听说雪会来，于是，我等待，一场倾城的飞扬。"
            ,"缘分是需要需要耐心等待的！要相信，在遇见Ta之前所经历的一切都是为等待"
            ,"你一定要等，不要失望，不要犹疑。这么大的世界，这么长的人生，总有一个人让你想温柔的对待。"
            ,"生活是一场漫长的旅行，不要浪费时间，去等待那些不愿与你携手同行的人。"
            ,"人生只有走出来的美丽，没有等出来的辉煌。"
            ,"其实，我不是一定要等你，只是等上了，就等不了别人了。——《朝露若颜》"
        };

        TXPanel waitingBoxInnerPanel;

        Label waitingBoxLab;

        private bool _IsWaitingBoxCreated = false;

        private delegate void WorkThreadExceptionHandler(Exception ex);

        private IButtonControl _btnAcceptOfKeyboard;

        private IButtonControl _btnCancelOfKeyboard;

        private bool _isOnWaiting = false;

        private PictureBox _waitPicBox;

        #endregion

        #region properties

        /// <summary>
        /// 设置等待窗体显示的消息内容
        /// </summary>
        /// <value>The wainting box message.</value>
        /// User:Ryan  CreateTime:2011-10-24 14:41.
        public string WaintingBoxMessage
        {
            set { this.Invoke(new Action<string>(this.SetWaitingMessage), value); }
        }

        internal Exception AscException { get; set; }

        public delegate void WorkThreadExceptionEventHanlder(Exception ex);

        public WorkThreadExceptionEventHanlder OnWorkThreadException;

        #endregion

        #region Public Methods

        /// <summary>
        /// 显示非模式的等待窗体界面（对窗体数据的更新等操作可在等待委托内处理）
        /// </summary>
        /// <param name="method">等待需要执行的方法（无参数）.</param>
        /// <param name="message">等候显示消息.</param>
        /// User:Ryan  CreateTime:2011-10-24 11:28.
        public void Waiting(MethodInvoker method, string message)
        {
            if (this._isOnWaiting)
            {
                return;
            }
            this._isOnWaiting = true;
            this.CreateWaitingBox();
            Random ran = new Random(DateTime.Now.Millisecond);
            message = string.IsNullOrEmpty(message) ? this.iWaittingMessage[ran.Next(0, this.iWaittingMessage.Length)] : message;
            this.SetWaitingMessage(message);
            waitingBox.Visible = true;
            waitingBox.BringToFront();
            //AcceptButton,CancelButton
            this._btnAcceptOfKeyboard = this.AcceptButton;
            this._btnCancelOfKeyboard = this.CancelButton;
            this.AcceptButton = null;
            this.CancelButton = null;
            //execution
            IAsyncResult ar = method.BeginInvoke(this.WorkComplete, method);
        }

        /// <summary>
        /// 显示非模式的等待窗体界面（对窗体数据的更新等操作可在等待委托内处理）
        /// </summary>
        /// <param name="method">等待需要执行的方法（无参数）.</param>
        /// User:Ryan  CreateTime:2011-10-24 11:28.
        public void Waiting(MethodInvoker method)
        {
            this.Waiting(method, string.Empty);
        }

        #endregion

        #region private methods

        #region WorkComplete

        /// <summary>
        /// Works the complete.
        /// </summary>
        /// <param name="results">The results.</param>
        /// User:Ryan  CreateTime:2012-8-5 16:23.
        private void WorkComplete(IAsyncResult results)
        {
            if (!this.waitingBox.Visible || !this.IsHandleCreated)
            {
                return;
            }

            if (this.waitingBox.InvokeRequired)
            {
                this.Invoke(new Action<IAsyncResult>(this.WorkComplete), results);
            }
            else
            {
                try
                {
                    ((MethodInvoker)results.AsyncState).EndInvoke(results);
                }
                catch (Exception ex)
                {
                    //this.Invoke(new MethodInvoker(() => { throw ex; }));
                    //throw ex; 
                    new WorkThreadExceptionHandler(ThrowException).BeginInvoke(ex, null, null);
                }
                finally
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        waitingBox.Visible = false;
                        this.AcceptButton = this._btnAcceptOfKeyboard;
                        this.CancelButton = this._btnCancelOfKeyboard;
                        this._isOnWaiting = false;
                    }));
                    //waitingBox.Visible = false; 
                }
            }
        }

        private void ThrowException(Exception ex)
        {
            if (this.OnWorkThreadException != null)
            {
                this.OnWorkThreadException(ex);
            }
        }
        #endregion

        #region CreateWaitingBox

        /// <summary>
        /// Creates the waiting box.
        /// </summary>
        /// User:Ryan  CreateTime:2012-8-5 16:22.
        private void CreateWaitingBox()
        {
            if (!this._IsWaitingBoxCreated)
            {
                #region CreateWaitingBox

                this.waitingBox = new System.Windows.Forms.Panel();
                //ControlHelper.BindMouseMoveEvent(this.waitingBox);
                waitingBox.BackColor = Color.FromArgb(234, 244, 252);
                ////innerpanel
                waitingBoxInnerPanel = new TXPanel();
                waitingBoxInnerPanel.Width = 280;
                waitingBoxInnerPanel.Height = 80;
                waitingBoxInnerPanel.CornerRadius = 6;
                waitingBoxInnerPanel.BackBeginColor = Color.White;
                waitingBoxInnerPanel.BackEndColor = Color.White;
                waitingBoxInnerPanel.Padding = new System.Windows.Forms.Padding(8, 5, 5, 5);
                ////label
                waitingBoxLab = new Label();
                waitingBoxLab.TextAlign = ContentAlignment.MiddleLeft;
                waitingBoxLab.AutoEllipsis = true;
                waitingBoxLab.Dock = DockStyle.Fill;
                //waitingBox.AutoSize = false;
                //ControlHelper.BindMouseMoveEvent(this.waitingBoxLab);
                waitingBoxInnerPanel.Controls.Add(waitingBoxLab);
                ////pictruebox
                PictureBox pb = new PictureBox();
                pb.Dock = DockStyle.Left;
                pb.Size = new System.Drawing.Size(72, 70);
                pb.Image = LoadResource.GetRandomLoadImage();
                pb.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                this._waitPicBox = pb;
                waitingBoxInnerPanel.Controls.Add(pb);
                ////...
                waitingBox.Controls.Add(waitingBoxInnerPanel);
                waitingBox.BringToFront();
                if (!this.Controls.Contains(waitingBox))
                {
                    this.Controls.Add(waitingBox);
                }
                waitingBox.Show();
                this._IsWaitingBoxCreated = true;
                #endregion
            }

            Rectangle rect = this.WorkRectangle;
            waitingBox.Width = rect.Width;
            waitingBox.Height = rect.Height;
            waitingBox.Location = new Point(rect.X, rect.Y);
            this._waitPicBox.Image = LoadResource.GetRandomLoadImage();
            waitingBox.BackgroundImage = this.CreateBacgroundImage();
            //waitingBox.BackgroundImage = Properties.Resources.logo_mini;
            waitingBox.BackgroundImageLayout = ImageLayout.Stretch;
        }
        #endregion

        #region SetWaitingMessage

        /// <summary>
        /// 设置等待显示的信息
        /// </summary>
        /// <param name="message">The message.</param>
        /// User:Ryan  CreateTime:2012-8-5 16:22.
        private void SetWaitingMessage(string message)
        {
            message = " " + message.Trim();
            if (this.waitingBoxLab != null && this.waitingBoxInnerPanel != null)
            {
                using (Graphics g = this.CreateGraphics())
                {
                    int w = Convert.ToInt32(g.MeasureString(message, this.waitingBoxLab.Font).Width);
                    w = w >= 200 ? w : 200;
                    w = this.Width - 100 >= w ? w : this.Width - 100;
                    this.waitingBoxInnerPanel.Width = w + 60;
                    waitingBoxInnerPanel.Location = new Point(waitingBox.Bounds.X + waitingBox.Width / 2 - waitingBoxInnerPanel.Width / 2,
                        waitingBox.Bounds.Y + waitingBox.Height / 2 - waitingBoxInnerPanel.Height);
                }

                this.waitingBoxLab.Text = message;
            }
        }
        #endregion

        #region CreateBacgroundImage

        /// <summary>
        /// 创建临时背景图片
        /// </summary>
        /// <returns>Return a data(or instance) of Bitmap.</returns>
        /// User:Ryan  CreateTime:2012-8-5 16:21.
        private Bitmap CreateBacgroundImage()
        {
            Rectangle rect = this.WorkRectangle;
            int w = rect.Width;
            int h = rect.Height;
            Point p1 = new Point(this.Location.X + this.Padding.Left,
                this.Location.Y + this.CaptionHeight + this.Padding.Top);
            Point p = this.Parent == null ? p1 : this.PointToScreen(p1);
            Bitmap TempImg = new Bitmap(w, h);
            try
            {

                Bitmap img = new Bitmap(w, h);
                using (Graphics g = Graphics.FromImage(TempImg))
                {
                    g.CopyFromScreen(p, new Point(0, 0), new Size(w, h));
                }

                using (Graphics g = Graphics.FromImage(img))
                {
                    GDIHelper.DrawImage(g, new Rectangle(0, 0, w, h), TempImg, 0.36F);
                }

                return img;
            }
            catch
            {
                return null;
            }
            finally
            {
                TempImg.Dispose();
            }
        }

        #endregion

        #endregion
    }
}
