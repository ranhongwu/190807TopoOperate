using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;

namespace RHW.Topo
{
    public partial class FrmTopo3 : DevExpress.XtraEditors.XtraForm
    {
        public FrmTopo3()
        {
            InitializeComponent();
            //初始化datatable
            GlobalTopoVaribate.GRuleDT.Columns.Add("要素类1", typeof(string));
            GlobalTopoVaribate.GRuleDT.Columns.Add("拓扑规则", typeof(string));
            GlobalTopoVaribate.GRuleDT.Columns.Add("要素类2", typeof(string));
        }

        List<string> LineTopoRule = new List<string>();

        //清空datatable的所有数据
        private void InitDT()
        {
            GlobalTopoVaribate.GRuleDT.Clear();
        }

        //加载窗体
        private void FrmTopo3_Load(object sender, EventArgs e)
        {
            this.Location = GlobalTopoVaribate.GFrmLocation;
            GlobalTopoVaribate.GFrmTopo3 = this;
            InitDT();
            gridControl1.DataSource = GlobalTopoVaribate.GRuleDT;

            //每一列自适应调整宽度
            gridView1.Columns[0].BestFit();
            gridView1.Columns[1].BestFit();
            gridView1.Columns[2].BestFit();
        }

        //上一步
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //删除该窗体相关全局变量(拓扑规则表)
            GlobalTopoVaribate.GRuleDT = new DataTable();
            GlobalTopoVaribate.GRuleRow = new List<string>();

            GlobalTopoVaribate.GFrmLocation = this.Location;
            GlobalTopoVaribate.GFrmTopo2.Location = GlobalTopoVaribate.GFrmLocation;
            GlobalTopoVaribate.GFrmTopo3 = null;
            GlobalTopoVaribate.GFrmTopo2.Show();
            this.Close();
        }

        //添加规则
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FrmAddTopoRule frmAddTopoRule = new FrmAddTopoRule();
            frmAddTopoRule.ShowDialog();
        }

        //删除规则
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            int[] row= gridView1.GetSelectedRows();
            for(int i = row.Length-1; i >= 0; i--)
            {
                GlobalTopoVaribate.GRuleDT.Rows[i].Delete();
            }
        }

        //全部删除
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            GlobalTopoVaribate.GRuleDT.Clear();
        }

        //下一步
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (GlobalTopoVaribate.GRuleDT.Rows.Count == 0)
            {
                MessageBox.Show("请指定拓扑规则!", "提示");
                return;
            }

            //实例化下一窗体，隐藏当前窗体
            GlobalTopoVaribate.GFrmLocation = this.Location;
            FrmTopo4 frmTopo4 = new FrmTopo4();
            frmTopo4.Location = GlobalTopoVaribate.GFrmLocation;
            this.Hide();
            frmTopo4.Show();
        }

        //取消
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            GlobalTopoVaribate.ClearAllVaribate();
            this.Close();
        }
    }
}