using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Diagram.Controls;
using NetworkDesigner.Beans.DataStruct;
using NetworkDesigner.Beans.Model;
using NetworkDesigner.Service.Model;

namespace NetworkDesigner.Utils.DiagramUtil
{
    class NodeHelper
    {
        /// <summary>
        /// 设置节点tag，源节点为null或其tag为空时，new新的tag，否则以源节点的tag作深拷贝
        /// </summary>
        /// <param name="newNode"></param>
        /// <param name="oldNode"></param>
        public static void SetNodeTag(Node newNode, Node oldNode=null)
        {
            ModelInfo mod;
            if (oldNode == null)
            {
                mod = new ModelInfo();
                mod.modType = new ModelType();//GetNodeType(newNode);
                mod.DiagramNode = newNode;
                newNode.Tag = mod;
            }
            else
            {
                ModelInfo oldNodeInfo = oldNode.Tag as ModelInfo;
                if (oldNodeInfo == null)
                {
                    mod = new ModelInfo();
                    mod.modType = new ModelType();
                    mod.DiagramNode = newNode;
                    newNode.Tag = mod;
                }
                else //不管newNode有没有tag都要新建tag，防止浅拷贝问题
                {
                    mod = oldNodeInfo.DeepCopy();
                    mod.DiagramNode = newNode;
                    newNode.Tag = mod;
                }
            }
            //这只是暂时很粗粒度的设置
            if (newNode is LineConnector)
            {
                mod.modType.类别 = "实体";
                mod.modType.类型 = "链路";
            }
        }

        /// <summary>
        /// 注意这个返回的类型仅仅是根据diagram的定义来的，并且粒度较粗
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static ModelType GetNodeType(Node n)
        {
            ModelType t = new ModelType();

            if (n is ConnectorBase)
                t.TypeName = "链路";
            else if (n is BitmapNode)
                t.TypeName = "节点";
            else if (n is FilledPath)
                t.TypeName = "节点";

            return t;
        }

        public static void ClearNodeLabel(Node node)
        {
            PropertyInfo plabels = node.GetType().GetProperty("Labels");
            if (plabels != null)
            {
                LabelCollection labels = plabels.GetValue(node) as LabelCollection;
                if (labels != null)
                {
                    labels.Clear();
                }
            }
        }
        public static void SetNodeLabel0(Node node, string text)
        {
            //if (node is TextNode)
                //return;//文本节点不添加Label //事实上确实也没有 Labels 属性
            PropertyInfo plabels = node.GetType().GetProperty("Labels");
            if (plabels != null)
            {
                LabelCollection labels = plabels.GetValue(node) as LabelCollection;
                if (labels != null)
                {
                    float width = 50;
                    float height = 12;
                    Label label = null;
                    if (labels.Count != 0)
                    {
                        label = labels[0];
                        labels.Clear();
                        width = label.Size.Width;
                        height = label.Size.Height;
                    }
                    label = new Syncfusion.Windows.Forms.Diagram.Label();
                    label.Text = text;
                    label.FontStyle.Family = "宋体";
                    label.FontStyle.Size = 9;
                    label.Size = new SizeF(width, height);
                    label.HorizontalAlignment = StringAlignment.Center;
                    label.Position = Syncfusion.Windows.Forms.Diagram.Position.BottomCenter;
                    SimModel model = node.Tag as SimModel;
                    if(model!=null)
                    {
                        var type = ModelInfoHelper.GetModelType(model.modelID);
                        if (type == SimModelType.链路)
                        {
                            label.Visible = false;
                        }
                    }
                    label.Container = node; //与底层源码保持一致，例如BitmapNode的Clone方法
                    labels.Add(label);
                }
            }
        }

        /// <summary>
        /// 获取节点的继承关系，测试使用
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GetNodeBaseInfo(Type nt)
        {
            StringBuilder sbd = new StringBuilder();
            sbd.Append("***All Base Class***\n");
            Type type = nt;
            Type[] intfs = type.GetInterfaces();
            do{
                sbd.Append(type.Name).Append(": ");
                if(type==typeof(object))
                    break;
                type = type.BaseType;
            }while(true);
            //sbd.Append("\nDirect base Interfaces***");
            //foreach (Type t in intfs)
            //{
            //     sbd.Append(t.Name).Append(",");
            //}
            return sbd.ToString();
        }
        /// <summary>
        /// 使用默认的LineConnector连接两个节点，连接不成功时返回null
        /// </summary>
        public static LineConnector ConnectNodes(Diagram diagram, Node parentNode, Node subNode)
        {
            if (parentNode.CentralPort == null || subNode.CentralPort == null)
                return null;

            // Create directed link
            PointF[] pts = new PointF[] { PointF.Empty, new PointF(0, 1) };
            LineConnector line = new LineConnector(pts[0], pts[1]);
            diagram.Model.AppendChild(line);
            parentNode.CentralPort.TryConnect(line.TailEndPoint);
            subNode.CentralPort.TryConnect(line.HeadEndPoint);
            return line;
        }

    }
}
