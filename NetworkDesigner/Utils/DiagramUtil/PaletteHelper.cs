using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms.Diagram.Controls;
using NetworkDesigner.Utils.FileUtil;

namespace NetworkDesigner.Utils.DiagramUtil
{
    class PaletteHelper
    {
        /// <summary>
        /// 从模型编辑器添加到面板时对节点的设置
        /// </summary>
        public static void SetNodeInPalette(Node n)
        {
            n.EnableCentralPort = true;//Enable之后可连link
            n.CentralPort.ConnectionsLimit = 10000;
            n.EditStyle.HidePinPoint = true;
            n.EditStyle.HideRotationHandle = true;
            n.EditStyle.AllowChangeHeight = false;
            n.EditStyle.AllowChangeWidth = false;
            //n.EditStyle.DefaultHandleEditMode = HandleEditMode.None;//隐藏掉选中时周围出现的格子，这时需要在选中事件中额外高亮
        }
        /// <summary>
        /// 从面板添加到模型编辑器时对节点的设置
        /// </summary>
        public static void SetNodeInSymbol(Node n)
        {
            n.EditStyle.AllowSelect = true;
            n.EditStyle.HideRotationHandle = false;
            n.EditStyle.AllowChangeHeight = true;
            n.EditStyle.AllowChangeWidth = true;
            //n.EditStyle.DefaultHandleEditMode = HandleEditMode.Resize;
        }

        public static void SetPaletteView(PaletteGroupView view)
        {
            view.ButtonView = true;
            view.FlowView = true;
            view.BorderStyle = System.Windows.Forms.BorderStyle.None;
            view.ShowToolTips = true;
            view.ShowFlowViewItemText = true;
            view.ShowDragNodeCue = false;
            view.SelectedItemColor = Color.FromArgb(255, 219, 118);
            view.HighlightItemColor = Color.FromArgb(255, 227, 149);
            view.SelectingItemColor = Color.FromArgb(255, 238, 184);
            view.SelectedHighlightItemColor = Color.FromArgb(255, 218, 115);
            view.FlowViewItemTextLength = 60;
            view.SmallImageView = false;
            //view.BackgroundImage = Image.FromFile(@"..\Common\images\Diagram\background_2.jpg");
            view.FlatLook = true;
        }

        public static bool ContainsSameNameNode(SymbolPalette symbol,string name)
        {
            foreach (Node n in symbol.Nodes)
            {
                if (n.Name.Equals(name)) //注意避免把name放前面，防止name为null
                    return true;
            }
            return false;
        }

        public static int RemoveNodeByName(SymbolPalette symbol, string name)
        {
            int i = 0;
            foreach (Node n in symbol.Nodes)
            {
                if (n.Name.Equals(name)) //注意避免把name放前面，防止name为null
                {
                    symbol.RemoveChild(n);
                    return i;
                }
                i++;
            }
            return -1;
        }

        public static Image Node2Image(Node node,int targetWidth,int targetHeight)
        {
            Bitmap bmp = new Bitmap(Convert.ToInt32(node.Size.Width + node.LineStyle.LineWidth),
                        Convert.ToInt32(node.Size.Height + node.LineStyle.LineWidth));
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                System.Drawing.Drawing2D.Matrix m = new System.Drawing.Drawing2D.Matrix();
                m.Translate(Convert.ToInt32(-node.BoundingRectangle.Left),
                Convert.ToInt32(-node.BoundingRectangle.Top));
                gr.Transform = m;
                node.Draw(gr);
                //bmp.Save("D:\\Temp Files\\image_" + bmp.Size.Width + "x" + bmp.Size.Height + ".png", System.Drawing.Imaging.ImageFormat.Png);
                Bitmap bmp2 = new Bitmap(ImageHelper.ZoomImage(bmp, targetWidth, targetHeight));
                //bmp2.Save("D:\\Temp Files\\image_" + bmp.Size.Width + "x" + bmp.Size.Height + ".png", System.Drawing.Imaging.ImageFormat.Png);
                bmp.Dispose();
                return bmp2;
            }
        }

        public static Node MaxWidthNode(NodeCollection nodes)
        {
            Node n = null;
            float w = -1;
            foreach (Node node in nodes)
            {
                if (node.Size.Width > w)
                {
                    n = node;
                    w = node.Size.Width;
                }
            }
            return n;
        }

        public static Node MaxHeightNode(NodeCollection nodes)
        {
            Node n = null;
            float h = -1;
            foreach (Node node in nodes)
            {
                if (node.Size.Height > h)
                {
                    n = node;
                    h = node.Size.Height;
                }
            }
            return n;
        }

        public static Node[] MaxWidthNodeAndMaxHeightNode(NodeCollection nodes)
        {
            Node nW = null;
            Node nH = null;
            float w = -1;
            float h = -1;
            foreach (Node node in nodes)
            {
                if (node.Size.Width > w)
                {
                    nW = node;
                    w = node.Size.Width;
                }
                if (node.Size.Height > h)
                {
                    nH = node;
                    h = node.Size.Height;
                }
            }
            return new Node[]{nW,nH};
        }

        /// <summary>
        /// 在矩形范围内布局nodes，按节点的最大宽度和最大高度均匀划分格子
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="leftup"></param>
        /// <param name="rightdown"></param>
        public static void LayoutGrid(NodeCollection nodes, PointF leftup, Size size)
        {
            if (nodes.Count < 1)
                return;

            Node[] mwhNode = MaxWidthNodeAndMaxHeightNode(nodes);
            float xSpace = mwhNode[0].Size.Width + 20;//同一行内相邻node的间距
            float ySpace = mwhNode[1].Size.Height + 20;//同一列内相邻node的间距
            if (xSpace < 1f)
                xSpace = 1f;
            if (ySpace < 1f)
                ySpace = 1f;
            int countX = (int)((size.Width - xSpace) / xSpace) + 1;//一行内最多放多少个node，注意左右两边最边上要留白
            //int countY = (int)((size.Height - leftup.Y - ySpace) / ySpace) + 1;//一列内最多放多少个node，注意上下两边最边上要留白
            if (countX < 1)
                countX = 1;
            //if (countY < 1)
            //    countY = 1;

            float xStart = leftup.X + xSpace / 2;//垂直基准线：第一列node的X
            float yStart = leftup.Y + ySpace / 2;//水平基准线：第一行node的Y
            float x = 0;
            float y = 0;
            //float xMaxHeight = -1;//当前行中node最大高度
            //float yMaxWidth = -1;//当前列中node最大宽度

            int nc = 0;//第几列
            int nr = 0;//第几行

            foreach (Node node in nodes)
            {
                x = xStart + nc * xSpace;
                y = yStart + nr * ySpace;
                node.PinPoint = new PointF(x, y);
                nc++;
                if(nc == countX)
                {
                    nc = 0;
                    nr++;
                }
            }
        }

    }
}
