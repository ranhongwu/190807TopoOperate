using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RHW.Topo
{
    class TopoOperation
    {
        private List<IFeatureClass> pFeatureClassList = new List<IFeatureClass>();

        /// <summary>
        /// 创建拓扑集成方法
        /// </summary>
        /// <param name="TopoName"></param>
        /// <param name="pFeatureDS"></param>
        /// <param name="FeatureClassList"></param>
        /// <param name="TopoDT"></param>
        /// <returns></returns>
        public ITopology2 CreateToplolgy(string TopoName,IFeatureDataset pFeatureDS,List<IFeatureClass> FeatureClassList,DataTable TopoDT)
        {
            ISchemaLock pSchemaLock = (ISchemaLock)pFeatureDS;
            try
            {
                pFeatureClassList = FeatureClassList;
                pSchemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);

                //创建拓扑
                ITopologyContainer2 pTopoContainer = pFeatureDS as ITopologyContainer2;
                ITopology2 pTopology = pTopoContainer.CreateTopology(TopoName, pTopoContainer.DefaultClusterTolerance, -1, "") as ITopology2;

                //添加要素
                foreach(IFeatureClass pFeatureClass in FeatureClassList)
                {
                    pTopology.AddClass(pFeatureClass, 5, 1, 1, false);
                }

                //添加规则
                for(int i = 0; i < TopoDT.Rows.Count; i++)
                {
                    string s = TopoDT.Rows[i][0].ToString();
                    if (TopoDT.Rows[i][2].ToString() == "")
                    {
                        AddTopoRules(pTopology, TopoDT.Rows[i][0].ToString(), TopoDT.Rows[i][1].ToString());
                    }
                    else
                    {
                        AddTopoRules(pTopology, TopoDT.Rows[i][0].ToString(), TopoDT.Rows[i][1].ToString(), TopoDT.Rows[i][2].ToString());
                    }
                }

                //验证拓扑
                IGeoDataset pGeoDataset = pTopology as IGeoDataset;
                IEnvelope pEnvelope = pGeoDataset.Extent;
                VaildateTopo(pTopology, pEnvelope);
                return pTopology;
            }
            catch(COMException COMEx)
            {
                MessageBox.Show(COMEx.Message);
                return null;
            }
            finally
            {
                pSchemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
            }
        }

        /// <summary>
        /// 添加单要素拓扑规则
        /// </summary>
        /// <param name="topology">建立的拓扑</param>
        /// <param name="FeatureClassName1">要素的名称</param>
        /// <param name="TopoRule">拓扑规则中文描述</param>
        private void AddTopoRules(ITopology topology, string FeatureClassName1, string TopoRule)
        {
            IFeatureClass pFeatureClass1 = getFeatureClassByName(FeatureClassName1, pFeatureClassList);
            esriTopologyRuleType RuleType = getTopoRuleByDescription(TopoRule);

            //创建拓扑规则
            ITopologyRule pTopoRule = new TopologyRuleClass();
            pTopoRule.OriginClassID = pFeatureClass1.FeatureClassID;
            pTopoRule.TopologyRuleType = RuleType;

            //这两个参数应该设置为true，否则生成的拓扑没有东西
            pTopoRule.AllDestinationSubtypes = true;
            pTopoRule.AllOriginSubtypes = true;

            ITopologyRuleContainer pTopologyRuleContainer = topology as ITopologyRuleContainer;
            if (pTopologyRuleContainer.CanAddRule[pTopoRule])
            {
                pTopologyRuleContainer.AddRule(pTopoRule);
            }
            else
            {
                throw new ArgumentException("无法添加" + pFeatureClass1.AliasName + "-" + pTopoRule.Name + "!");
            }
        }

        /// <summary>
        /// 添加双要素拓扑规则
        /// </summary>
        /// <param name="topology">建立的拓扑</param>
        /// <param name="FeatureClassName1">要素1的名称</param>
        /// <param name="TopoRule">拓扑规则中文描述</param>
        /// <param name="FeatureClassName2">要素2的名称</param>
        private void AddTopoRules(ITopology topology, string FeatureClassName1, string TopoRule,string FeatureClassName2)
        {
            IFeatureClass pFeatureClass1 = getFeatureClassByName(FeatureClassName1, pFeatureClassList);
            IFeatureClass pFeatureClass2 = getFeatureClassByName(FeatureClassName2, pFeatureClassList);
            esriTopologyRuleType RuleType = getTopoRuleByDescription(TopoRule);

            //创建拓扑规则
            ITopologyRule pTopoRule = new TopologyRuleClass();
            pTopoRule.OriginClassID = pFeatureClass1.FeatureClassID;
            pTopoRule.TopologyRuleType = RuleType;
            pTopoRule.DestinationClassID = pFeatureClass2.FeatureClassID;
            pTopoRule.AllDestinationSubtypes = true;
            pTopoRule.AllOriginSubtypes = true;

            ITopologyRuleContainer pTopologyRuleContainer = topology as ITopologyRuleContainer;
            if (pTopologyRuleContainer.CanAddRule[pTopoRule])
            {
                pTopologyRuleContainer.AddRule(pTopoRule);
            }
            else
            {
                throw new ArgumentException("无法添加" + pFeatureClass1.AliasName + "-" + pTopoRule.Name + "-" + pFeatureClass2.AliasName + "!");
            }
        }

        //验证拓扑
        private void VaildateTopo(ITopology pTopology,IEnvelope pEnvelope)
        {
            double x= pEnvelope.LowerLeft.X;
            double y = pEnvelope.LowerLeft.Y;
            IGeoDataset pGeoDS = pTopology as IGeoDataset;
            pTopology.ValidateTopology(pEnvelope);
        }

        /// <summary>
        /// 将字符串的拓扑规则描述转换成esri拓扑规则
        /// </summary>
        /// <param name="RuleName">拓扑的名字</param>
        /// <returns>返回esri拓扑规则</returns>
        private esriTopologyRuleType getTopoRuleByDescription(string RuleName)
        {
            TopoRules pTopoRules = (TopoRules)Enum.Parse(typeof(TopoRules), RuleName);
            return TransferToESRITopoRule(pTopoRules);
        }

        /// <summary>
        /// 根据要素类的名字搜索要素类列表中的要素类
        /// </summary>
        /// <param name="name">待搜索的要素类的名字</param>
        /// <param name="FeatureClassList">搜索的要素类列表</param>
        /// <returns>返回搜索到的要素类</returns>
        private IFeatureClass getFeatureClassByName(string name,List<IFeatureClass> FeatureClassList)
        {
            IFeatureClass target_FeatureClass=null;
            foreach(IFeatureClass pFeatureClass in FeatureClassList)
            {
                if (pFeatureClass.AliasName == name)
                {
                    target_FeatureClass= pFeatureClass;
                    break;
                }
            }
            return target_FeatureClass;
        }

        /// <summary>
        /// 将中文拓扑规则翻译为esri拓扑规则
        /// </summary>
        /// <param name="_topoRules">中文拓扑规则描述</param>
        /// <returns></returns>
        public static esriTopologyRuleType TransferToESRITopoRule(TopoRules _topoRules)
        {
            esriTopologyRuleType pESRITopoType=esriTopologyRuleType.esriTRTAny;
            switch (_topoRules)
            {
                case TopoRules.一个面图层里各要素之间不能有个缝隙:
                    pESRITopoType = esriTopologyRuleType.esriTRTAreaNoGaps;
                    break;
                case TopoRules.一个面图层里各要素间不能有叠加:
                    pESRITopoType = esriTopologyRuleType.esriTRTAreaNoOverlap;
                    break;
                case TopoRules.第二个图层面要素必须被第一个图层任一面要素覆盖:
                    pESRITopoType = esriTopologyRuleType.esriTRTAreaCoveredByAreaClass;
                    break;
                case TopoRules.面要素必须只包含一个点要素:
                    pESRITopoType = esriTopologyRuleType.esriTRTAreaContainOnePoint;
                    break;
                case TopoRules.两图层面要素必须互相覆盖:
                    pESRITopoType = esriTopologyRuleType.esriTRTAreaAreaCoverEachOther;
                    break;
                case TopoRules.第一个图层面要素必须被另一个图层任一面要素包含:
                    pESRITopoType = esriTopologyRuleType.esriTRTAreaCoveredByArea;
                    break;
                case TopoRules.图层间面要素不能相互覆盖:
                    pESRITopoType = esriTopologyRuleType.esriTRTAreaNoOverlapArea;
                    break;
                case TopoRules.线要素必须跟面图层边界的一部分或全部重叠:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary;
                    break;
                case TopoRules.点要素必须落在面要素边界上:
                    pESRITopoType = esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary;
                    break;
                case TopoRules.点要素必须落在面要素内:
                    pESRITopoType = esriTopologyRuleType.esriTRTPointProperlyInsideArea;
                    break;
                case TopoRules.线要素间不能有相互重叠部分:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineNoOverlap;
                    break;
                case TopoRules.线要素之间不能相交:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineNoIntersection;
                    break;
                case TopoRules.线要素不允许有悬挂点:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineNoDangles;
                    break;
                case TopoRules.线要素不允许有假节点:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineNoPseudos;
                    break;
                case TopoRules.第一个图层线要素应被第二个线图层线要素覆盖:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineCoveredByLineClass;
                    break;
                case TopoRules.第一个图层线要素不被第二个线图层线要素覆盖:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineNoOverlapLine;
                    break;
                case TopoRules.点要素应被线要素覆盖:
                    pESRITopoType = esriTopologyRuleType.esriTRTPointCoveredByLine;
                    break;
                case TopoRules.点要素应在线要素的端点上:
                    pESRITopoType = esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint;
                    break;
                case TopoRules.面要素边界必须被线要素覆盖:
                    pESRITopoType = esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine;
                    break;
                case TopoRules.面要素的边界必须被另一面要素边界覆盖:
                    pESRITopoType = esriTopologyRuleType.esriTRTAreaBoundaryCoveredByAreaBoundary;
                    break;
                case TopoRules.线要素不能自重叠:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineNoSelfOverlap;
                    break;
                case TopoRules.线要素不能自相交:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineNoSelfIntersect;
                    break;
                case TopoRules.线要素间不能重叠和相交:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch;
                    break;
                case TopoRules.线要素端点必须被点要素覆盖:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint;
                    break;
                case TopoRules.面要素内必须包含至少一个点要素:
                    pESRITopoType = esriTopologyRuleType.esriTRTAreaContainPoint;
                    break;
                case TopoRules.线不能是多段:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineNoMultipart;
                    break;
                case TopoRules.点要素之间不相交:
                    pESRITopoType = esriTopologyRuleType.esriTRTPointDisjoint;
                    break;
                case TopoRules.线要素必须不相交:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineNoIntersectLine;
                    break;
                case TopoRules.线必须不相交或内部接触:
                    pESRITopoType = esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouchLine;
                    break;
                default:
                    break;
            }
            return pESRITopoType;
        }

        /// <summary>
        /// 拓扑规则的枚举
        /// </summary>
        public enum TopoRules
        {
            //面规则
            一个面图层里各要素之间不能有个缝隙 = 301,
            一个面图层里各要素间不能有叠加 = 302,
            第二个图层面要素必须被第一个图层任一面要素覆盖 = 331,
            两图层面要素必须互相覆盖 = 332,
            第一个图层面要素必须被另一个图层任一面要素包含 = 333,
            图层间面要素不能相互覆盖 = 334,
            面要素边界必须被线要素覆盖 = 321,
            面要素的边界必须被另一面要素边界覆盖 = 335,
            面要素内必须包含至少一个点要素 = 311,
            面要素必须只包含一个点要素 = 312,

            //线规则
            线要素端点必须被点要素覆盖 = 211,
            第一个图层线要素应被第二个线图层线要素覆盖 = 221,
            第一个图层线要素不被第二个线图层线要素覆盖 = 222,
            线要素间不能有相互重叠部分 = 223,
            线要素之间不能相交 = 224,
            线要素必须跟面图层边界的一部分或全部重叠 = 231,
            线要素必须在面内 = 232,
            线要素不允许有悬挂点 = 201,
            线要素不允许有假节点 = 202,
            线要素不能自重叠 = 203,
            线要素不能自相交 = 204,
            线要素间不能重叠和相交 = 205,
            线不能是多段 = 206,
            线要素必须不相交 = 207,
            线必须不相交或内部接触 = 208,

            //点规则
            点要素之间不相交 = 111,
            点要素应被线要素覆盖 = 121,
            点要素应在线要素的端点上 = 122,
            点要素必须落在面要素边界上 = 131,
            点要素必须落在面要素内 = 132,
            点要素不重合 = 101,
        }

        /// <summary>
        /// 面的拓扑规则
        /// </summary>
        /// <returns>返回所有面拓扑规则的列表</returns>
        public static List<TopoRules> getPolygonRules()
        {
            List<TopoRules> PolygontopoRules = new List<TopoRules>();
            PolygontopoRules.Add(TopoRules.一个面图层里各要素之间不能有个缝隙);
            PolygontopoRules.Add(TopoRules.一个面图层里各要素间不能有叠加);
            PolygontopoRules.Add(TopoRules.第二个图层面要素必须被第一个图层任一面要素覆盖);
            PolygontopoRules.Add(TopoRules.两图层面要素必须互相覆盖);
            PolygontopoRules.Add(TopoRules.第一个图层面要素必须被另一个图层任一面要素包含);
            PolygontopoRules.Add(TopoRules.图层间面要素不能相互覆盖);
            PolygontopoRules.Add(TopoRules.面要素边界必须被线要素覆盖);
            PolygontopoRules.Add(TopoRules.面要素的边界必须被另一面要素边界覆盖);
            PolygontopoRules.Add(TopoRules.面要素内必须包含至少一个点要素);
            PolygontopoRules.Add(TopoRules.面要素必须只包含一个点要素);
            return PolygontopoRules;
        }

        /// <summary>
        /// 线的拓扑规则
        /// </summary>
        /// <returns>返回所有线拓扑规则列表</returns>
        public static List<TopoRules> getLineRules()
        {
            List<TopoRules> LineRules = new List<TopoRules>();
            LineRules.Add(TopoRules.线要素端点必须被点要素覆盖);
            LineRules.Add(TopoRules.第一个图层线要素应被第二个线图层线要素覆盖);
            LineRules.Add(TopoRules.第一个图层线要素不被第二个线图层线要素覆盖);
            LineRules.Add(TopoRules.线要素间不能有相互重叠部分);
            LineRules.Add(TopoRules.线要素之间不能相交);
            LineRules.Add(TopoRules.线要素必须跟面图层边界的一部分或全部重叠);
            LineRules.Add(TopoRules.线要素必须在面内);
            LineRules.Add(TopoRules.线要素不允许有悬挂点);
            LineRules.Add(TopoRules.线要素不允许有假节点);
            LineRules.Add(TopoRules.线要素不能自重叠);
            LineRules.Add(TopoRules.线要素不能自相交);
            LineRules.Add(TopoRules.线要素间不能重叠和相交);
            LineRules.Add(TopoRules.线不能是多段);
            LineRules.Add(TopoRules.线要素必须不相交);
            LineRules.Add(TopoRules.线必须不相交或内部接触);
            return LineRules;
        }

        /// <summary>
        /// 点的拓扑规则
        /// </summary>
        /// <returns>返回所有点拓扑规则列表</returns>
        public static List<TopoRules> getPointRules()
        {
            List<TopoRules> PointRules = new List<TopoRules>();
            PointRules.Add(TopoRules.点要素之间不相交);
            PointRules.Add(TopoRules.点要素应被线要素覆盖);
            PointRules.Add(TopoRules.点要素应在线要素的端点上);
            PointRules.Add(TopoRules.点要素必须落在面要素边界上);
            PointRules.Add(TopoRules.点要素必须落在面要素内);
            PointRules.Add(TopoRules.点要素不重合);
            return PointRules;
        }
    }
}
