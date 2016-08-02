using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Web;
using TX.Framework.WindowUI.Forms;

namespace TX.Framework.WindowUI.Controls
{
    public partial class frmAddImage : FormInfoEntity
    {
        public delegate void ReturnValue(string URLAddress);
        public ReturnValue _ReturnValue;
        //private FileUploader _FileUploader;

        public frmAddImage()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.Text = "插入图片";
            this.StartPosition = FormStartPosition.CenterParent;
            //
            //rBtn_Local.Checked = true;
            this.rBtn_URL_CheckedChanged(null, null);
        }
        #region //Events

        private void rBtn_URL_CheckedChanged(object sender, EventArgs e)
        {
            if (rBtn_URL.Checked)
            {
                txt_URL.Enabled = true;
                txt_URL.Focus();
                btn_LocalImageUpdate.Enabled = false;
            }
            else
            {
                txt_URL.Enabled = false;
                btn_LocalImageUpdate.Enabled = true;
            }
        }

        private void btn_LocalImageUpdate_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "jpg (*.jpg)|*.jpg|jpeg (*.jpeg)|*.jpeg|gif (*.gif)|*.gif|所有文件 (*.*)|*.*";
            ofd.RestoreDirectory = true;
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;
            ofd.Title = "添加本地图片";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileFullName = ofd.FileName;
                txtFileName.Text = fileFullName;
            }
        }

        public Func<string,string> OnImageUpdate;

        protected override void OnBtnOkClick(object sender, EventArgs e)
        {
            //read url
            if (rBtn_URL.Checked)
            {
                if (!string.IsNullOrEmpty(txt_URL.Text.Trim()))
                {
                    this._ReturnValue(txt_URL.Text.Trim());
                }
                else
                {
                    this.Warning("请输入您的网络图片地址");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.txtFileName.Text.Trim()))
                {
                    this.Info("请从本地上传您的图片！");
                    return;
                }

                if ( this.OnImageUpdate != null )
                {
                    this._ReturnValue(this.OnImageUpdate(txtFileName.Text.Trim()));
                }
                //上传图片到WEB服务器上
                //string tempFileName = txtFileName.Text.Trim(); ;
                
                //string saveFileName = DateTime.Now.ToString("yyyyMMddhhmmss") + DateTime.Now.Millisecond.ToString() + ".jpg";
                //try
                //{
                //    _FileUploader = Singleton<FileUploader>.Instance;
                //    using (FileStream fs = new FileStream(tempFileName, FileMode.Open, FileAccess.Read))
                //    {
                //        UploadResponseInfo uploadResponseInfo = new UploadResponseInfo();
                //        string serverPath = @"\HtmlEditorPicture";
                //        if (_FileUploader.Upload(fs, saveFileName, serverPath, out uploadResponseInfo))
                //        {
                //            this.Info("图片上传成功!");

                //            string url = @"http://image.tianxiahotel.com/"
                //                + uploadResponseInfo.FileRelativePath.Replace("\\", "/");
                //            _ReturnValue(url);
                //        }
                //        else
                //        {
                //            this.Error("图片上传失败，请联系管理员", "错误");
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    this.Error("图片上传失败，请联系管理员!错误信息：\n" + ex.Message, "错误");
                //}
            }

            base.OnBtnOkClick(sender, e);
        }

        #endregion Events
    }
}
