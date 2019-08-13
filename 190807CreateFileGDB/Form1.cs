using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using RHW.Topo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _190807CreateFileGDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //按下新建文件GDB按钮
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文件地理数据库(*.gdb)|*.gdb";
            saveFileDialog.Title = "新建文件地理数据库";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.File.Exists(saveFileDialog.FileName))
                {
                    MessageBox.Show("文件已存在!");
                    return;
                }
                try
                {
                    int index = saveFileDialog.FileName.LastIndexOf("\\");
                    string GDBName = saveFileDialog.FileName.Substring(index + 1);
                    string GDBPath = saveFileDialog.FileName.Substring(0, index);
                    IWorkspaceFactory pWorkspaceFactory = new FileGDBWorkspaceFactory();
                    pWorkspaceFactory.Create(GDBPath, GDBName, null, 0);
                    MessageBox.Show("创建成功！", "提示");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //新建要素数据集
        private void button2_Click(object sender, EventArgs e)
        {
            FrmCreateDS frmCreateDS = new FrmCreateDS();
            frmCreateDS.ShowDialog();
        }

        //加载要素数据集
        private void button3_Click(object sender, EventArgs e)
        {
            FrmAddFeatureDS frmAddFeatureDS = new FrmAddFeatureDS(axMapControl1.Map);
            frmAddFeatureDS.ShowDialog();
        }

        //拓扑检查
        private void button4_Click(object sender, EventArgs e)
        {
            FrmTopo1 frmTopo1 = new FrmTopo1();
            GlobalTopoVaribate.GFrmTopo1 = frmTopo1;
            frmTopo1.ShowDialog();
        }
    }
}
