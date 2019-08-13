using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Management.Instrumentation;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace RHW.Topo
{
    public partial class FrmTopo2 : DevExpress.XtraEditors.XtraForm
    {
        public FrmTopo2()
        {
            InitializeComponent();
        }

        List<IFeatureClass> pAllFeatureClassList = new List<IFeatureClass>();

        //加载窗体
        private void FrmTopo2_Load(object sender, EventArgs e)
        {
            this.Location= GlobalTopoVaribate.GFrmLocation;
            GlobalTopoVaribate.GFrmTopo2 = this;
            //遍历数据集中的所有要素，加到Listbox中
            IEnumDataset pEnumDS = GlobalTopoVaribate.GFeatureDS.Subsets;
            IDataset pDS;
            checkedListBoxControl1.Items.Clear();
            while ((pDS = pEnumDS.Next()) != null)
            {
                if(pDS is IFeatureClass)
                {
                    pAllFeatureClassList.Add(pDS as IFeatureClass);
                    checkedListBoxControl1.Items.Add(pDS.Name);
                }
            }
            //默认的拓扑命名
            textEdit1.Text = GlobalTopoVaribate.GFeatureDS.Name + "_Topology";
        }

        //全选
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            checkedListBoxControl1.CheckAll();
        }

        //全部取消
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            checkedListBoxControl1.UnCheckAll();
        }

        //上一步
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            GlobalTopoVaribate.GFrmLocation = this.Location;
            GlobalTopoVaribate.GFrmTopo1.Location = GlobalTopoVaribate.GFrmLocation;
            GlobalTopoVaribate.GFrmTopo2 = null;
            GlobalTopoVaribate.GFrmTopo1.Show();
            this.Close();
        }

        //下一步
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Trim() == "")
            {
                MessageBox.Show("请输入建立的拓扑名称!", "提示");
                return;
            }
            if (checkedListBoxControl1.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择建立拓扑的要素类!", "提示");
                return;
            }
            //将选择的要素类和拓扑名添加到Topo全局变量中
            GlobalTopoVaribate.GTopoName = textEdit1.Text;
            GlobalTopoVaribate.GTopoFeatureClassList.Clear();
            for (int i = 0; i < checkedListBoxControl1.CheckedItems.Count; i++)
            {
                for(int j=0;j< pAllFeatureClassList.Count;j++)
                { 
                    if (pAllFeatureClassList[j].AliasName == checkedListBoxControl1.CheckedItems[i].ToString())
                    {
                        GlobalTopoVaribate.GTopoFeatureClassList.Add(pAllFeatureClassList[j]);
                    }
                }
            }

            GlobalTopoVaribate.GFrmLocation = this.Location;
            //实例化下一窗体
            FrmTopo3 frmTopo3 = new FrmTopo3();
            frmTopo3.Location = GlobalTopoVaribate.GFrmLocation;
            this.Hide();
            frmTopo3.Show();
        }

        //取消
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            GlobalTopoVaribate.ClearAllVaribate();
            this.Close();
        }
    }
}