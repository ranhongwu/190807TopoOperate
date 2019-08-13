using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.ConversionTools;
using ESRI.ArcGIS.Geoprocessing;

namespace _190807CreateFileGDB
{
    public partial class FrmCreateDS : Form
    {
        public FrmCreateDS()
        {
            InitializeComponent();
        }

        List<IFeatureClass> pFeatureClassList = new List<IFeatureClass>();
        IWorkspace pWorkspace;
        ISpatialReference pSpatialReference;

        //选择数据集路径
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "选择文件GDB";
            folderBrowserDialog.ShowNewFolderButton = false;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if(folderBrowserDialog.SelectedPath.Substring(folderBrowserDialog.SelectedPath.LastIndexOf("."))!=".gdb")
                {
                    MessageBox.Show("请选择正确的地理数据库");
                    return;
                }
                IWorkspaceFactory pWorkspaceFactory = new FileGDBWorkspaceFactory();
                pWorkspace = pWorkspaceFactory.OpenFromFile(folderBrowserDialog.SelectedPath, 0);
                textBox1.Text = folderBrowserDialog.SelectedPath;
            }
        }

        //选择坐标系
        private void button2_Click(object sender, EventArgs e)
        {
            ISpatialReferenceDialog2 spatialReferenceDialog2 = new SpatialReferenceDialogClass();
            pSpatialReference = spatialReferenceDialog2.DoModalCreate(true, false, false, 0);
            if (pSpatialReference == null) return;
            textBox2.Text = pSpatialReference.Name;
        }

        //添加导入的要素类
        private void button5_Click(object sender, EventArgs e)
        {
            int index;
            string path, name;
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
            IWorkspace pWorkspace2;
            IFeatureWorkspace pFeatureWorkspace;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择要添加的要素类";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "shp文件(*.shp)|*.shp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> FileList = new List<string>();
                foreach(string s in openFileDialog.FileNames)
                {
                    listBox1.Items.Add(s);
                    index = s.LastIndexOf("\\");
                    path = s.Substring(0, index);
                    name = s.Substring(index + 1);
                    pWorkspace2 = pWorkspaceFactory.OpenFromFile(path,0);
                    pFeatureWorkspace = pWorkspace2 as IFeatureWorkspace;
                    pFeatureClassList.Add(pFeatureWorkspace.OpenFeatureClass(name));
                }
            }
        }

        //删除一项
        private void button4_Click(object sender, EventArgs e)
        {
            foreach(IFeatureClass pFeatureClass in pFeatureClassList)
            {
                if (pFeatureClass.AliasName == listBox1.SelectedItem.ToString())
                {
                    pFeatureClassList.Remove(pFeatureClass);
                    break;
                }
            }
        }

        //全部清除
        private void button3_Click(object sender, EventArgs e)
        {
            pFeatureClassList.Clear();
            listBox1.Items.Clear();
        }

        //确定
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "")
            {
                MessageBox.Show("请输入正确的信息!", "提示");
                return;
            }
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("请选择要导入的要素类!", "提示");
                return;
            }
            //数据库中新建要素数据集
            IFeatureDataset pFeatureDataset = CreateDatasetInGDB(pWorkspace, textBox3.Text, pSpatialReference);
            //建好的要素数据集中导入要素类
            foreach (IFeatureClass eachFeatureClass in pFeatureClassList)
            {
                string f = pFeatureDataset.Workspace.PathName;
                if (ImportFeatureIntoDS(eachFeatureClass, pFeatureDataset.Workspace.PathName + "\\" + pFeatureDataset.Name, eachFeatureClass.AliasName) == false)
                    return;
            }
            MessageBox.Show("创建成功!", "提示");
        }

        //在文件数据库中新建要素数据集
        public IFeatureDataset CreateDatasetInGDB(IWorkspace pWorkspace, string name, ISpatialReference pSpatialReference)
        {
            IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
            IFeatureDataset pFeatureDataset = pFeatureWorkspace.CreateFeatureDataset(name, pSpatialReference);
            return pFeatureDataset;
        }

        //将要素类导入要素数据集方法
        public static bool ImportFeatureIntoDS(IFeatureClass pInFeatureClass, string outPath, string name)
        {
            object sev = null;
            Geoprocessor GP = new Geoprocessor();
            try
            {
                FeatureClassToFeatureClass featureClassToFeatureClass = new FeatureClassToFeatureClass();
                featureClassToFeatureClass.in_features = pInFeatureClass;
                featureClassToFeatureClass.out_path = outPath;
                featureClassToFeatureClass.out_name = name;
                GP.OverwriteOutput = false;
                IGeoProcessorResult pResult = new GeoProcessorResult();
                GP.Execute(featureClassToFeatureClass, null);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(GP.GetMessages(ref sev));
                return false;
            }
        }
    }
}
