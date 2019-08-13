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
using ESRI.ArcGIS.Geometry;
using RHW.Topo;

namespace RHW.Topo
{
    public partial class FrmAddTopoRule : DevExpress.XtraEditors.XtraForm
    {
        public FrmAddTopoRule()
        {
            InitializeComponent();
        }

        IFeatureClass pFeatureClass1;

        //加载
        private void FrmAddTopoRule_Load(object sender, EventArgs e)
        {
            comboBoxEdit1.Properties.Items.Clear();
            foreach (IFeatureClass pFeatureClass in GlobalTopoVaribate.GTopoFeatureClassList)
            {
                comboBoxEdit1.Properties.Items.Add(pFeatureClass.AliasName);
            }
        }

        //选择要素1时，判断要素1的类型，进而确定拓扑规则
        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据要素类型不同返回的规则的列表
            List<TopoOperation.TopoRules> TopoRules = new List<TopoOperation.TopoRules>();
            foreach (IFeatureClass pFeatureClass in GlobalTopoVaribate.GTopoFeatureClassList)
            {
                if(comboBoxEdit1.Text== pFeatureClass.AliasName)
                {
                    pFeatureClass1 = pFeatureClass;
                    break;
                }
            }
            switch (pFeatureClass1.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    TopoRules = TopoOperation.getPointRules();
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    TopoRules = TopoOperation.getLineRules();
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    TopoRules = TopoOperation.getPolygonRules();
                    break;
                default:
                    break;
            }
            comboBoxEdit3.Properties.Items.Clear();
            //将拓扑规则加到下拉框
            foreach (TopoOperation.TopoRules inTopoRules in TopoRules)
            {
                comboBoxEdit3.Properties.Items.Add(inTopoRules.ToString());
            }
        }

        //选择拓扑规则时确定要素2
        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<IFeatureClass> FeatureClass2List = new List<IFeatureClass>();
            TopoOperation.TopoRules topoRules = (TopoOperation.TopoRules)Enum.Parse(typeof(TopoOperation.TopoRules), comboBoxEdit3.Text);
            char fea2Type= ((int)topoRules).ToString()[1];
            switch (fea2Type)
            {
                case '0':
                    comboBoxEdit2.Enabled = false;
                    break;
                case '1':
                    comboBoxEdit2.Enabled = true;
                    FeatureClass2List = GetTypeFeatureClassList(GlobalTopoVaribate.GTopoFeatureClassList, esriGeometryType.esriGeometryPoint);
                    break;
                case '2':
                    comboBoxEdit2.Enabled = true;
                    FeatureClass2List = GetTypeFeatureClassList(GlobalTopoVaribate.GTopoFeatureClassList, esriGeometryType.esriGeometryPolyline);
                    break;
                case '3':
                    comboBoxEdit2.Enabled = true;
                    FeatureClass2List = GetTypeFeatureClassList(GlobalTopoVaribate.GTopoFeatureClassList, esriGeometryType.esriGeometryPolygon);
                    break;
                default:
                    break;
            }
            foreach(IFeatureClass pFeatureClass in FeatureClass2List)
            {
                comboBoxEdit2.Properties.Items.Add(pFeatureClass.AliasName);
            }
        }

        //取消
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //添加
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GlobalTopoVaribate.GRuleRow.Clear();
            GlobalTopoVaribate.GRuleRow.Add(comboBoxEdit1.Text);
            GlobalTopoVaribate.GRuleRow.Add(comboBoxEdit3.Text);
            GlobalTopoVaribate.GRuleRow.Add(comboBoxEdit2.Text);
            GlobalTopoVaribate.GRuleDT.Rows.Add(GlobalTopoVaribate.GRuleRow[0], GlobalTopoVaribate.GRuleRow[1], GlobalTopoVaribate.GRuleRow[2]);
            this.Close();
        }

        /// <summary>
        /// 返回要素类列表中指定点线面类型的要素列表
        /// </summary>
        /// <param name="AllFeatureClassList">输入的要素列表</param>
        /// <param name="type">指定的要素类型：点线面</param>
        /// <returns>返回指定类型的要素类列表</returns>
        public List<IFeatureClass> GetTypeFeatureClassList(List<IFeatureClass> AllFeatureClassList, esriGeometryType type)
        {
            List<IFeatureClass> pFeatureClassList = new List<IFeatureClass>();
            switch (type)
            {
                case esriGeometryType.esriGeometryPoint:
                    foreach (IFeatureClass pFeatureClass in AllFeatureClassList)
                    {
                        if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                        {
                            pFeatureClassList.Add(pFeatureClass);
                        }
                    }
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    foreach (IFeatureClass pFeatureClass in AllFeatureClassList)
                    {
                        if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                        {
                            pFeatureClassList.Add(pFeatureClass);
                        }
                    }
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    foreach (IFeatureClass pFeatureClass in AllFeatureClassList)
                    {
                        if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                        {
                            pFeatureClassList.Add(pFeatureClass);
                        }
                    }
                    break;
                default:
                    break;
            }
            return pFeatureClassList;
        }
    }
}