using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDesigner.Beans.Model
{
    /// <summary>
    /// 仿真场景的信息，通常一个FrmDiagram有一个SimScenario，包含全部的节点、链路、业务等信息
    /// </summary>
    public class SimScenario
    {
        /// <summary>
        /// 所有的节点，节点名->节点
        /// </summary>
        public Dictionary<string, SimModel> nodes;

        /// <summary>
        /// 所有的主机节点
        /// </summary>
        public List<SimModel> hosts;

        /// <summary>
        /// 所有的交换机节点
        /// </summary>
        public List<SimModel> switchs;

        /// <summary>
        /// 所有的路由器节点
        /// </summary>
        public List<SimModel> routes;

        /// <summary>
        /// 所有的链路，链路没有名称
        /// </summary>
        public List<SimModel> links;
        /// <summary>
        /// 所有的接口，编号->接口
        /// </summary>
        public Dictionary<string, SimModel> intfs;
        /// <summary>
        /// 所有的业务，业务名->业务项？
        /// </summary>
        public List<SimTraffic> traffics;
        public Dictionary<string, SimModel> results;

        public Dictionary<string, object> 场景参数;

        public static int counter = 1;
        public SimScenario()
        {
            nodes = new Dictionary<string, SimModel>();
            links = new List<SimModel>();
            intfs = new Dictionary<string, SimModel>();
            traffics = new List<SimTraffic>();
            results = new Dictionary<string, SimModel>();
            
            hosts = new List<SimModel>();
            switchs = new List<SimModel>();
            routes = new List<SimModel>();

            场景参数 = new Dictionary<string, object>();
            场景参数.Add("SimRange", new ScenarioRange());
            场景参数.Add("SimTime", "1000");
            场景参数.Add("SimSeed", "10");
            场景参数.Add("SimName", "qualnet-1");

            场景参数.Add("AllLink", links);            
            场景参数.Add("AllNode", nodes.Values);
            场景参数.Add("AllTraffic", traffics);

            场景参数.Add("AllHosts", hosts);
            场景参数.Add("AllSwitchs", switchs);
            场景参数.Add("AllRoutes", routes);
        }
        public object this[string ix]
        {
            get
            {
                if (场景参数.ContainsKey(ix))
                    return 场景参数[ix];
                switch (ix)
                {
                    case "AllLink":
                        return links;
                        break;
                    case "AllNode":
                        return nodes.Values;
                        break;
                    case "AllTraffic":
                        return traffics;
                        break;
                    default:
                        break;
                }
                return "不存在该属性"; //正式版应当返回""，并记录此错误
            }
            set
            {
                if (场景参数.ContainsKey(ix))
                    场景参数[ix] = value;
            }
        }
        public void TransOutput(string type)
        {
            switch (type)
            {
                case "Qualnet":
                    TransQualnet();
                    break;
                case "OPNET":
                    TransOPNET();
                    break;
                case "Mininet":
                    TransMininet();
                    break;
                default:
                    break;
            }
        }
        private void TransQualnet()
        {
            var st = ModelTransInfo.GetQualnetTmplate("Qualnet-main");
            st.SetAttribute("Scenario", this);
            System.IO.File.WriteAllText(@"D:\Temp Files\设计项目\项目3\"
                + 场景参数["SimName"] + ".config", st.ToString());
            st = ModelTransInfo.GetQualnetTmplate("Qualnet-nodes");
            st.SetAttribute("Scenario", this);
            System.IO.File.WriteAllText(@"D:\Temp Files\设计项目\项目3\"
                + 场景参数["SimName"] + ".nodes", st.ToString());
        }
        private void TransOPNET()
        {
            var st = ModelTransInfo.GetQualnetTmplate("OPNET-main");
            st.SetAttribute("Scenario", this);
            System.IO.File.WriteAllText(@"D:\Temp Files\设计项目\项目3\"
                + 场景参数["SimName"] + ".xml", st.ToString());
        }
        private void TransMininet()
        {
            var st = ModelTransInfo.GetQualnetTmplate("Mininet-main");
            st.SetAttribute("Scenario", this);
            System.IO.File.WriteAllText(@"D:\Temp Files\设计项目\项目3\"
                + 场景参数["SimName"] + ".py", st.ToString());
        }
    }
    public class SimTraffic
    {
        Dictionary<string, object> param;
        public SimTraffic()
        {
            param = new Dictionary<string, object>();
        }
        public object this[string ix]
        {
            get
            {
                if (param.ContainsKey(ix))
                    return param[ix];
                return "";
            }
            set 
            {
                param[ix] = value;
            }
        }
        public static SimTraffic GenerateTraffic(string type, List<SimModel> models)
        {
            SimTraffic traffic = new SimTraffic();
            int from = 0;
            int to = 0;
            Random rand = new Random();
            int i = 0;
            from = rand.Next(models.Count);
            to = rand.Next(models.Count);
            while (from == to && i < 100)
            {
                to = rand.Next(models.Count);
                i++;
            }
            traffic["SrcNode"] = models[from];
            traffic["DestNode"] = models[to];
            switch (type)
            {
                //case "CBR": //default
                //    break;
                case "FTP":
                    traffic["trafficName"] = "FTP";
                    traffic["trafficParam"] = "100 1S";
                    break;
                default:
                    traffic["trafficName"] = "CBR";
                    traffic["trafficParam"] = "100 512 1S 1S 300S";
                    break;
            }
            return traffic;
        }
    }
    public class ScenarioRange
    {
        public Dictionary<string, object> param;
        public ScenarioRange()
        {
            param = new Dictionary<string, object>();
            param.Add("LefUp", new PointF());
            param.Add("RightUp", new PointF());
            param.Add("LeftDown", new PointF());
            param.Add("RightDown", new PointF());
            param.Add("Width", 1000);
            param.Add("Height", 1000);
        }
        public object this[string ix]
        {
            get
            {
                if (param.ContainsKey(ix))
                    return param[ix];

                return "不存在该属性";
            }
            set 
            {
                if (param.ContainsKey(ix))
                    param[ix] = value;
            }
        }
    }
}
