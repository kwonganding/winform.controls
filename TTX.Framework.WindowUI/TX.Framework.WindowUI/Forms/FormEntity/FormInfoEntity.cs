using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TX.Framework.WindowUI.Controls; 

namespace TX.Framework.WindowUI.Forms
{
    public partial class FormInfoEntity : BaseForm
    {
        #region fileds

        private int _ControlMargin = 20;

        private bool _ShowBtnOk = true;

        private bool _ShowBtnCancel = true;

        #endregion

        public FormInfoEntity()
            :base()
        {
            InitializeComponent();
            ControlHelper.BindMouseMoveEvent(this.panelControlArea);
            this.ResetBtnPosition();
        }

        #region properties

        [Category("TXProperties")]
        [DefaultValue(true)]
        [Description("显示确定按钮")]
        public bool ShowBtnOk
        {
            get { return this._ShowBtnOk; }
            set
            {
                this._ShowBtnOk = value;
                this.ResetBtnPosition();
            }
        }

        [Category("TXProperties")]
        [DefaultValue(true)]
        [Description("显示取消按钮")]
        public bool ShowBtnCancel
        {
            get { return this._ShowBtnCancel; }
            set
            {
                this._ShowBtnCancel = value;
                this.ResetBtnPosition();
            }
        }

        [Category("TXProperties")]
        [DefaultValue(20)]
        [Description("确定、取消控制按钮之间的间距")]
        public int ControlMargin
        {
            get { return this._ControlMargin; }
            set
            {
                this._ControlMargin = value;
                this.ResetBtnPosition();
            }
        }

        #endregion

        #region events

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.panelControlArea != null)
            {
                this.ResetBtnPosition();
            }
        }

        protected virtual void OnBtnOkClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        protected virtual void OnBtnCancelClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region private methods

        private void ResetBtnPosition()
        {
            int margin = this._ControlMargin;
            Point center = new Point(panelControlArea.Width / 2, panelControlArea.Height / 2 - 2);
            if (this._ShowBtnCancel && this._ShowBtnOk)
            {
                btnCancel.Visible = true;
                btnOK.Visible = true;
                btnOK.Location = new Point(center.X - margin - btnOK.Width, center.Y - btnOK.Height / 2);
                btnCancel.Location = new Point(center.X + margin, center.Y - btnCancel.Height / 2);
            }
            else if (this._ShowBtnOk && !this._ShowBtnCancel)
            {
                btnOK.Location = new Point(center.X - btnOK.Width / 2, center.Y - btnOK.Height / 2);
                btnOK.Visible = true;
                btnCancel.Visible = false;
            }
            else if (!this._ShowBtnOk && this._ShowBtnCancel)
            {
                btnCancel.Location = new Point(center.X - btnCancel.Width / 2, center.Y - btnCancel.Height / 2);
                btnOK.Visible = false;
                btnCancel.Visible = true;
            }
            else
            {
                btnCancel.Visible = false;
                btnOK.Visible = false;
            }
        }

        #endregion
    }
}
