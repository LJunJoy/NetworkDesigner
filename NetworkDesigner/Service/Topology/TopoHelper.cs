using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using NetworkDesigner.Beans.DataStruct;
using System.Drawing;

namespace NetworkDesigner.Service.Topology
{
    class TopoHelper
    {
        public static readonly Dictionary<string, Type> Topos;
        static TopoHelper()
        {
            Topos = new Dictionary<string, Type>();
            //Topos.Add(TopoGrid.Name, typeof(TopoGrid));
        }

        public static List<string> GetToposNames()
        {
            return new List<string>(Topos.Keys);
        }

        /// <summary>
        /// 反射获取topo布局类的Name静态字段值
        /// </summary>
        public static string ReflectTopoName(string topo)
        {
            Type type = Topos[topo];
            if (type == null)
                return "";
            var feildName = type.GetField("Name");
            return (string)feildName.GetValue(null);
        }
        /// <summary>
        /// 反射获取topo布局类的Info静态字段值
        /// </summary>
        public static string ReflectTopoInfo(string topo)
        {
            Type type = Topos[topo];
            if (type == null)
                return "";
            var feildName = type.GetField("Info");
            return (string)feildName.GetValue(null);
        }
        /// <summary>
        /// 反射获取topo布局类的Lines静态字段值
        /// </summary>
        public static Dictionary<string, string> ReflectTopoLines(string topo)
        {
            Type type = Topos[topo];
            if (type == null)
                return null;
            var feildName = type.GetField("Lines");
            return (Dictionary<string, string>)feildName.GetValue(null);
        }

        /// <summary>
        /// 根据layout命令执行相应的节点生成和布局操作，需由调用者处理异常消息
        /// </summary>
        public static TopoResult DoLayout(string range, string layout)
        {
            TopoRange topoRange = TopoRange.Parse(range);
            string[] cmds = layout.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (cmds.Length < 2 || !cmds[0].Equals("layout") || !Topos.ContainsKey(cmds[1]))
                throw new Exception("请输入正确的布局命令：layout 布局名 参数");
            Type topo = Topos[cmds[1]];
            Dictionary<string, string> iparam = GetParams(cmds, 2);
            
            TopoResult result = new TopoResult();
            result.range = RectangleF.Empty;// topoRange;
            object worker = Activator.CreateInstance(topo);
            MethodInfo method = topo.GetMethod("DoLayout");
            object[] parameters = new object[] { result, iparam };
            method.Invoke(worker, parameters);
            return result;
        }
        public static Dictionary<string, string> GetParams(string[] cmds,int start)
        {
            Dictionary<string, string> iparam = new Dictionary<string, string>();
            StringBuilder sbd = new StringBuilder();
            string str = "";
            for (int i = start; i < cmds.Length; i++)
            {
                if (cmds[i].StartsWith("-"))
                {
                    if (str.Length != 0)
                    {
                        iparam.Add(str, sbd.ToString());
                        sbd.Clear(); //-p node[0] model=m1 -f lxf
                    }
                    str = cmds[i];
                }
                else
                {
                    sbd.Append(" ").Append(cmds[i]);
                }
            }
            bool f1 = str.Length > 0 && sbd.Length > 0;
            bool f2 = str.Length > 0 && sbd.Length == 0 && !iparam.ContainsKey(str);
            if (f1)
                iparam.Add(str, sbd.ToString());
            else if (f2)
                iparam.Add(str, "");
            return iparam;
        }

        public static double Degree2Radian(double degree)
        {
            return degree * Math.PI / 180;
        }
        public static double Radian2Degree(double radian)
        {
            return radian * 180 / Math.PI;
        }
        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// 平均放置count个节点的一维坐标，基准坐标计为0，两边各空半个space
        /// </summary>
        /// <param name="count">节点个数</param>
        /// <param name="width">范围宽度</param>
        public static List<float> GetPosition(int count,float width)
        {
            List<float> result = new List<float>(count);
            if (count == 0)
                return result;
            float space = width / count;
            float start = 0 + space / 2;
            for (int i = 0; i < count; i++)
                result.Add(start + i*space);
            return result;
        }
        /// <summary>
        /// 计算环形节点的坐标，默认起始节点在圆心正上方，顺时针方向计数，可设置旋转角度调整各节点坐标
        /// </summary>
        /// <param name="count">圆环上节点个数</param>
        /// <param name="center">圆环中心坐标</param>
        /// <param name="radius">圆环半径</param>
        /// <param name="rotate">圆环旋转角度，单位度</param>
        public static List<PointF> GetPosCircle(int count,PointF center,double radius,double rotate = 0)
        {
            List<PointF> result = new List<PointF>(count);
            if (count == 0)
                return result;
            double space = 360.0 / count; //单位度
            double rot;
            for (int i = 0; i < count; i++)
            {
                PointF pt = new PointF();
                rot = rotate + space * i;
                pt.X = (float)(center.X + radius * Math.Sin(Degree2Radian(rot)));
                pt.Y = (float)(center.Y - radius * Math.Cos(Degree2Radian(rot)));
                result.Add(pt); //注意：point是值类型，添加point是直接拷贝的，因此要先赋值然后才Add
            }
            return result;
        }
        /// <summary>
        /// 计算环形上单个节点的坐标，旋转角度为以圆心正上方为起点的顺时针旋转
        /// </summary>
        /// <param name="center">圆环中心坐标</param>
        /// <param name="radius">圆环半径</param>
        /// <param name="rotate">圆环旋转角度，单位度</param>
        public static PointF GetPosCircleAtRotate(PointF center, double radius, double rotate)
        {
            PointF pt = new PointF();
            pt.X = (float)(center.X + radius * Math.Sin(Degree2Radian(rotate)));
            pt.Y = (float)(center.Y - radius * Math.Cos(Degree2Radian(rotate)));
            return pt;
        }
    }
}
