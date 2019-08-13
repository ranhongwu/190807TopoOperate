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
using ESRI.ArcGIS.Geodatabase;
using System.Runtime.InteropServices;

namespace RHW.Topo
{
    public partial class FrmTopo4 : DevExpress.XtraEditors.XtraForm
    {
        public FrmTopo4()
        {
            InitializeComponent();
        }

        private void FrmTopo4_Load(object sender, EventArgs e)
        {
            string Name,FeatureClassesName,Rules;
            Name = "拓扑名称： " + GlobalTopoVaribate.GTopoName;
            FeatureClassesName = "要素列表:";
            foreach(IFeatureClass pFeatureClass in GlobalTopoVaribate.GTopoFeatureClassList)
            {
                FeatureClassesName += "\n" + pFeatureClass.AliasName;
            }
            Rules = "拓扑规则：";
            for(int i = 0; i < GlobalTopoVaribate.GRuleDT.Rows.Count; i++)
            {
                Rules += "\n" + GlobalTopoVaribate.GRuleDT.Rows[i][0]+" - "+ GlobalTopoVaribate.GRuleDT.Rows[i][1] + " - "+ GlobalTopoVaribate.GRuleDT.Rows[i][2] ;
            }
            label1.Text = "  " + Name +
                "\n\n  " + FeatureClassesName +
                "\n\n  " + Rules;
        }

        //确定
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                TopoOperation topoOperation = new TopoOperation();
                ITopology2 pTopology=topoOperation.CreateToplolgy(GlobalTopoVaribate.GTopoName, GlobalTopoVaribate.GFeatureDS, GlobalTopoVaribate.GTopoFeatureClassList, GlobalTopoVaribate.GRuleDT);
                if(pTopology!=null) MessageBox.Show("拓扑创建成功！");
                GlobalTopoVaribate.ClearAllVaribate();
                this.Close();
            }
            catch(COMException COMEx)
            {
                return;
            }
        }

        //上一步
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            GlobalTopoVaribate.GFrmLocation = this.Location;
            GlobalTopoVaribate.GFrmTopo3.Location = GlobalTopoVaribate.GFrmLocation;
            GlobalTopoVaribate.GFrmTopo4 = null;
            GlobalTopoVaribate.GFrmTopo3.Show();
            this.Close();
        }

        //取消
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            GlobalTopoVaribate.ClearAllVaribate();
            this.Close();
        }
    }
}