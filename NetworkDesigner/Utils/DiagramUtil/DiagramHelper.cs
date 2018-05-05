using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkDesigner.Utils.DiagramUtil
{
    class DiagramHelper
    {
        /// <summary>
        /// Get on current selecton list has textBoxNodes.
        /// </summary>
        /// <returns>true - one or more text nodes; false - none</returns>
        public static bool CheckTextSelecionNode(Diagram diagram)
        {
            bool bResult = false;

            if (diagram.Controller == null)
                return bResult;

            if (diagram.Controller.TextEditor.IsEditing)
                bResult = true;
            else
            {
                NodeCollection selectionNodes = diagram.Controller.SelectionList;

                if (selectionNodes != null)
                {
                    foreach (INode node in selectionNodes)
                    {
                        if (node is TextNode)
                        {
                            bResult = true;
                            break;
                        }
                    }
                }
            }

            return bResult;
        }
        /// <summary>
        /// 将diagram可视区域中心调整到外部容器控件的中心坐标，当外部容器足够全部显示时，不调整
        /// </summary>
        public static void SetViewCenter(Diagram diagram, RectangleF rect)
        {
            float x = rect.X + rect.Width / 2 - diagram.Size.Width / 2;
            if (x < 0)
                x = 0;
            float y = rect.Y + rect.Height / 2 - diagram.Size.Height / 2;
            if(y < 0)
                y=0;
            diagram.View.Origin = new PointF(x, y);
        }

        /// <summary>
        /// 给定任意两点确定一个矩形
        /// </summary>
        public static System.Drawing.Rectangle GetRectangle(Point p0, Point p1)
        {
            return new System.Drawing.Rectangle(
                Math.Min(p0.X, p1.X),
                Math.Min(p0.Y, p1.Y),
                Math.Abs(p1.X - p0.X), 
                Math.Abs(p1.Y - p0.Y)
                );
        }
    }
}
