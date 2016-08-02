using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TX.Framework.WindowUI.Forms;

namespace WindowsTest
{
    public partial class winMain : MainForm
    {
        public winMain()
        {
            InitializeComponent();

            this.txTreeComboBox1.DataSource = TestData.GetTreeData(10);
            this.txTreeComboBox1.MultiLevelDataSourceMember = "Users";
            this.txTreeComboBox1.DisplayMember = "Name";
            this.txTreeComboBox1.ValueMember = "Value";
            this.txTreeComboBox1.BindData();
        }

        private void txButton1_Click(object sender, EventArgs e)
        {
            //BaseForm
            BaseForm win = new BaseForm();
            win.ShowDialog();
        }

        private void txButton4_Click(object sender, EventArgs e)
        {
            frmErrorBox.ShowError(new Exception("some error",new ArgumentException("argument error","test")));
        }

        private void txButton2_Click(object sender, EventArgs e)
        {
            PopForm win = new PopForm();
            win.Show();
        }

        private void txButton7_Click(object sender, EventArgs e)
        {
            this.Waiting(() =>
            {
                System.Threading.Thread.Sleep(12000);
            });
        }

        private void txButton3_Click(object sender, EventArgs e)
        {
            this.Info("提示消息！");
        }

        private void txButton5_Click(object sender, EventArgs e)
        {
            this.Warning("客官，请自重！");
        }

        private void txButton6_Click(object sender, EventArgs e)
        {
            this.Error("出错了！");
        }

        private void txButton8_Click(object sender, EventArgs e)
        {
            this.Question("你确定要退出吗？");
        }

        private void txButton9_Click(object sender, EventArgs e)
        {
            frmList win = new frmList();
            win.ShowDialog();
        }

        private void winMain_Load(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
