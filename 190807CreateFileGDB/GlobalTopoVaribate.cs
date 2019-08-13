using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHW.Topo
{
    class GlobalTopoVaribate
    {
        /// <summary>
        /// 存储窗体的位置
        /// </summary>
        public static Point GFrmLocation
        {
            get; set;
        }

        /// <summary>
        /// 存储拓扑中打开的各个窗体实例
        /// </summary>
        public static FrmTopo1 GFrmTopo1 = null;
        public static FrmTopo2 GFrmTopo2 = null;
        public static FrmTopo3 GFrmTopo3 = null;
        public static FrmTopo3 GFrmTopo4 = null;

        /// <summary>
        /// 存储文件数据库的路径
        /// </summary>
        public static string GDBPath
        {
            get; set;
        }

        /// <summary>
        /// 文件数据库中存储的要素数据集
        /// </summary>
        public static List<IFeatureDataset> GFeatureDSList = new List<IFeatureDataset>();

        /// <summary>
        /// 选择的要素数据集
        /// </summary>
        public static IFeatureDataset GFeatureDS
        {
            get; set;
        }

        /// <summary>
        /// 选择的数据集中所有的要素类列表
        /// </summary>
        public static List<IFeatureClass> GAllFeatureClassList = new List<IFeatureClass>();

        /// <summary>
        /// 选择的建立拓扑的要素类列表
        /// </summary>
        public static List<IFeatureClass> GTopoFeatureClassList = new List<IFeatureClass>();

        /// <summary>
        /// 建立的拓扑名称
        /// </summary>
        public static string GTopoName
        {
            get; set;
        }

        /// <summary>
        /// 选择规则时临时存储的一行规则
        /// </summary>
        public static List<string> GRuleRow = new List<string>();

        /// <summary>
        /// 存储拓扑规则的表
        /// </summary>
        public static DataTable GRuleDT = new DataTable();

        /// <summary>
        /// 初始化所有静态变量的方法
        /// </summary>
        public static void ClearAllVaribate()
        {
            //删除窗体
            GFrmTopo1 = null;
            GFrmTopo2 = null;
            GFrmTopo3 = null;
            GFrmTopo4 = null;

            //删除各静态变量
            GDBPath = null;
            GFeatureDSList = new List<IFeatureDataset>();
            GFeatureDS = null;
            GAllFeatureClassList = new List<IFeatureClass>();
            GTopoFeatureClassList = new List<IFeatureClass>();
            GTopoName = null;
            List<string> GRuleRow = new List<string>();
            DataTable GRuleDT = new DataTable();
        }

        
    }
}
