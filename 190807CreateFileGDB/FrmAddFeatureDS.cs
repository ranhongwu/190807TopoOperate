using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace _190807CreateFileGDB
{
    public partial class FrmAddFeatureDS : Form
    {
        public FrmAddFeatureDS(IMap _map)
        {
            InitializeComponent();
            pMap = _map;
        }

        IMap pMap;
        string path;
        IWorkspace pWorkspace;
        List<IFeatureDataset> FeatureDSList = new List<IFeatureDataset>();

        //打开数据库
        private void button1_Click(object sender, EventArgs e)
        {
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
            }
            IWorkspaceFactory pWorkspaceFactory = new FileGDBWorkspaceFactory();
            pWorkspace = pWorkspaceFactory.OpenFromFile(path, 0);
            FeatureDSList = getGDBFeatureDS(pWorkspace);
            foreach (IDataset pDS in FeatureDSList)
            {
                checkedListBox1.Items.Add(pDS.Name);
            }
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

        //清除
        private void button2_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            FeatureDSList.Clear();
            pWorkspace = null;
        }

        //加载数据集
        private void button3_Click(object sender, EventArgs e)
        {
            IFeatureWorkspace pFeaWS = pWorkspace as IFeatureWorkspace;
            List<IFeatureDataset> FeatureDSList2 = new List<IFeatureDataset>();
            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                foreach (IFeatureDataset eachDS in FeatureDSList)
                {
                    if (eachDS.Name == checkedListBox1.CheckedItems[i].ToString())
                    {
                        FeatureDSList2.Add(eachDS);
                    }
                }
            }
            foreach (IFeatureDataset FeaDS2 in FeatureDSList2)
            {
                IEnumDataset pEnumDS = FeaDS2.Subsets;
                IDataset pFeaDS2;
                while ((pFeaDS2 = pEnumDS.Next()) != null)
                {
                    if (pFeaDS2 is IFeatureClass)
                    {
                        IFeatureLayer pFeaLayer = new FeatureLayer();
                        pFeaLayer.FeatureClass = pFeaWS.OpenFeatureClass(pFeaDS2.Name);
                        pFeaLayer.Name = pFeaDS2.Name;
                        pMap.AddLayer(pFeaLayer as ILayer);
                    }
                }
                //加载拓扑结果
                ITopologyLayer pTopolayer;
                ITopology pTopology;
                ILayer pLayer;
                ITopologyContainer pTopoContainer = (ITopologyContainer)FeaDS2;
                for (int i = 0; i < pTopoContainer.TopologyCount; i++)
                {
                    pTopolayer = new TopologyLayerClass();
                    pTopology = pTopoContainer.Topology[i];
                    pTopolayer.Topology = pTopology;
                    pLayer = pTopolayer as ILayer;
                    pLayer.Name = FeaDS2.Name + "_拓扑" + i + 1;
                    pMap.AddLayer(pLayer);
                }
            }
        }
    }
}
