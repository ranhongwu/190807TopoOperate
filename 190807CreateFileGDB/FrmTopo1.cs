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
using ESRI.ArcGIS.DataSourcesGDB;

namespace RHW.Topo
{
    public partial class FrmTopo1 : DevExpress.XtraEditors.XtraForm
    {
        public FrmTopo1()
        {
            InitializeComponent();
        }

        //数据库中的要素数据集列表
        List<IFeatureDataset> FeatureDSList = new List<IFeatureDataset>();

        //加载时初始化各个文本框等的数据
        private void FrmTopo1_Load(object sender, EventArgs e)
        {
            GlobalTopoVaribate.GFrmTopo1 = this;
            //if(GlobalTopoVaribate.GFrmLocation!=Point.Empty)
            //    this.Location = GlobalTopoVaribate.GFrmLocation;
            tbxGDBPath.Text = GlobalTopoVaribate.GDBPath;
            if (GlobalTopoVaribate.GFeatureDSList == null) return;
            foreach (IFeatureDataset pFeatureDS in GlobalTopoVaribate.GFeatureDSList)
            {
                cbxFeatureDS.Properties.Items.Add(pFeatureDS.Name);
            }
        }

        //浏览文件夹选择文件gdb
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string path="";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.Description = "选择.gdb文件数据库";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;
                if (path.Substring((path.Length) - 4) != ".gdb")
                {
                    MessageBox.Show("请选择*.gdb文件地理数据库！", "提示");
                    return;
                }
                tbxGDBPath.Text = path;
                GlobalTopoVaribate.GDBPath = path;
            }
            if (path == "") return;
            IWorkspaceFactory pWorkspaceFactory = new FileGDBWorkspaceFactory();
            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(path, 0);
            FeatureDSList = getGDBFeatureDS(pWorkspace);
            GlobalTopoVaribate.GFeatureDSList = FeatureDSList;
            cbxFeatureDS.Properties.Items.Clear();
            cbxFeatureDS.Text = "";
            foreach (IFeatureDataset pFeatureDS in FeatureDSList)
            {
                cbxFeatureDS.Properties.Items.Add(pFeatureDS.Name);
            }
        }

        //取消
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            GlobalTopoVaribate.ClearAllVaribate();
            this.Close();
        }

        //下一步
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (tbxGDBPath.Text.Trim() == "" || cbxFeatureDS.Text.Trim() == "")
            {
                MessageBox.Show("请输入完整信息!","提示");
                return;
            }

            //将选择的数据集加到全局变量类中
            foreach(IFeatureDataset eachFeatureDS in GlobalTopoVaribate.GFeatureDSList)
            {
                if (cbxFeatureDS.Text == eachFeatureDS.Name)
                {
                    GlobalTopoVaribate.GFeatureDS = eachFeatureDS;
                    break;
                }
            }

            //隐藏当前窗体，实例化下一窗体
            GlobalTopoVaribate.GFrmLocation = this.Location;
            FrmTopo2 frmTopo2 = new FrmTopo2();
            frmTopo2.Location = GlobalTopoVaribate.GFrmLocation;
            this.Hide();
            frmTopo2.Show();
        }

        //遍历数据库中的要素数据集
        public List<IFeatureDataset> getGDBFeatureDS(IWorkspace pWorkspace)
        {
            List<IFeatureDataset> FeatureDSList = new List<IFeatureDataset>();
            IEnumDataset pEnumDS = pWorkspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);
            IDataset pFesDS;
            while ((pFesDS = pEnumDS.Next()) != null)
            {
                FeatureDSList.Add(pFesDS as IFeatureDataset);
            }
            return FeatureDSList;
        }
    }
}