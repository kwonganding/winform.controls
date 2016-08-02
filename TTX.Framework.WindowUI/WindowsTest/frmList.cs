using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsTest
{
    public partial class frmList : TX.Framework.WindowUI.Forms.FormListEntity
    {
        public frmList()
        {
            InitializeComponent();
            this.templateListView1.DataSource = TestData.GetTreeData(30);
            
        }

        private void toolBar_Refresh(object sender, EventArgs e)
        {
            this.Waiting(() =>
                {
                    System.Threading.Thread.Sleep(1000);
                });
        }
    }
}
