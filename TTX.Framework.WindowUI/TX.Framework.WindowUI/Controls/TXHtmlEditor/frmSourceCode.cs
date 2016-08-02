using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TX.Framework.WindowUI.Forms;

namespace TX.Framework.WindowUI.Controls
{
    public partial class frmSourceCode : FormInfoEntity
    {
        public delegate void ReturnValue(string sourceCode);
        public ReturnValue _ReturnValue;

        public frmSourceCode(string sourceCode)
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.Text = "编辑HTML源代码";
            txt_SourceCode.Text = sourceCode;
        }
        #region //Events

        protected override void OnBtnOkClick(object sender, EventArgs e)
        {
            if (txt_SourceCode.Text != null)
            {
                _ReturnValue(txt_SourceCode.Text.Trim());
                base.OnBtnOkClick(sender, e);
            }
        }

        #endregion Events
    }
}
