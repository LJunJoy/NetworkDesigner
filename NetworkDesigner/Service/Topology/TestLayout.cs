using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Diagram.Controls;

namespace NetworkDesigner.Service.Topology
{
    class TestLayout
    {
        public Syncfusion.Windows.Forms.Diagram.Controls.Diagram diagram1;

        public TestLayout(Diagram dia)
        {
            this.diagram1 = dia;
            PopulateNodes();

            RadialTreeLayoutManager rdlLyt = new RadialTreeLayoutManager(this.diagram1.Model, 0, 10, 10);
            rdlLyt.LeftMargin = 40;
            rdlLyt.TopMargin = 20;
            this.diagram1.LayoutManager = rdlLyt;
            this.diagram1.LayoutManager.UpdateLayout(null);
        }
        /// <summary>
        /// Initialize the nodes in daigram
        /// </summary>
        private void PopulateNodes()
        {
            //First level node
            Ellipse e1 = new Ellipse(0, 0, 45, 45);
            e1.FillStyle.Color = Color.FromArgb(174, 205, 227);
            e1.FillStyle.ForeColor = Color.FromArgb(121, 188, 220);
            e1.FillStyle.Type = FillStyleType.LinearGradient;
            e1.LineStyle.LineWidth = 1;
            e1.EnableShading = false;
            this.diagram1.Model.AppendChild(e1);
            GenerateInnerLevelNodes(e1, 9, Color.FromArgb(237, 195, 229), Color.FromArgb(249, 226, 249), 0);
        }

        /// <summary>
        /// Generates the Inner level nodes
        /// </summary>
        /// <param name="parentNode">Parent Node</param>
        /// <param name="maxSubNodes">Maximum sub nodes</param>
        /// <param name="LevelColor">Fill color for nodes</param>
        /// <param name="foreColor">ForeColor for nodes</param>
        /// <param name="n"></param>
        private void GenerateInnerLevelNodes(Node parentNode, int maxSubNodes, Color LevelColor, Color foreColor, int n)
        {

            for (int i = 1; i <= maxSubNodes; i++)
            {
                Ellipse ellipse1 = GetEllipse(LevelColor, foreColor);
                ellipse1.FillStyle.Type = FillStyleType.LinearGradient;
                this.diagram1.Model.AppendChild(ellipse1);
                if (i == 7)
                {
                    Ellipse ellipse2 = GetEllipse(Color.FromArgb(151, 204, 237), Color.FromArgb(184, 255, 255));
                    this.diagram1.Model.AppendChild(ellipse2);
                    ConnectNodes(ellipse1, ellipse2, Color.Black);
                    for (int l = 0; l < 4; l++)
                    {
                        Ellipse ellipse4 = GetEllipse(Color.FromArgb(179, 220, 179), Color.FromArgb(222, 233, 162));
                        this.diagram1.Model.AppendChild(ellipse4);
                        ConnectNodes(ellipse2, ellipse4, Color.Black);
                        if (l == 2 || l == 3)
                        {
                            for (int m = 0; m < 1; m++)
                            {
                                Ellipse ellipse5 = GetEllipse(Color.FromArgb(179, 220, 179), Color.FromArgb(222, 233, 162));
                                this.diagram1.Model.AppendChild(ellipse5);
                                ConnectNodes(ellipse4, ellipse5, Color.Black);
                            }

                        }
                    }

                }
                ConnectNodes(parentNode, ellipse1, Color.Black);
                if (i == 2 || i == 3 || i == 8 || i == 9)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Ellipse ellipse2 = GetEllipse(Color.FromArgb(151, 204, 237), Color.FromArgb(184, 255, 255));
                        this.diagram1.Model.AppendChild(ellipse2);
                        ConnectNodes(ellipse1, ellipse2, Color.Black);
                        if (i == 2 || i == 3 && j == 1 || i == 9)
                        {
                            for (int k = 0; k < 2; k++)
                            {
                                Ellipse ellipse3 = GetEllipse(Color.FromArgb(179, 220, 179), Color.FromArgb(222, 233, 162));
                                this.diagram1.Model.AppendChild(ellipse3);
                                ConnectNodes(ellipse2, ellipse3, Color.Black);
                                if (i == 2 && j == 0 || i == 2 && j == 1 && k == 1 || i == 3 && j == 1 && k == 1)
                                {
                                    for (int l = 0; l < 1; l++)
                                    {
                                        Ellipse ellipse9 = GetEllipse(Color.FromArgb(179, 220, 179), Color.FromArgb(222, 233, 162));
                                        this.diagram1.Model.AppendChild(ellipse9);
                                        ConnectNodes(ellipse3, ellipse9, Color.Black);
                                    }

                                }
                                if (i == 2 && j == 1 && k == 0)
                                {
                                    for (int l = 0; l < 2; l++)
                                    {
                                        Ellipse ellipse9 = GetEllipse(Color.FromArgb(179, 220, 179), Color.FromArgb(222, 233, 162));
                                        this.diagram1.Model.AppendChild(ellipse9);
                                        ConnectNodes(ellipse3, ellipse9, Color.Black);
                                    }

                                }
                            }
                        }
                        if (i == 8 && j == 0)
                        {

                            for (int l = 0; l < 4; l++)
                            {
                                Ellipse ellipse4 = GetEllipse(Color.FromArgb(179, 220, 179), Color.FromArgb(222, 233, 162));
                                this.diagram1.Model.AppendChild(ellipse4);
                                ConnectNodes(ellipse2, ellipse4, Color.Black);
                            }
                        }

                    }
                }
            }
        }

        /// <summary>
        /// New function for create a ellipse
        /// </summary>
        /// <returns></returns>
        private static Ellipse GetEllipse(Color fillColor,Color foreColor)
        {
            Ellipse ellipse1 = new Ellipse(0, 0, 30, 30);
            ellipse1.FillStyle.Color = fillColor;
            ellipse1.FillStyle.ForeColor = foreColor;
            ellipse1.FillStyle.Type = FillStyleType.LinearGradient;
            return ellipse1;
        }

        /// <summary>
        /// Connects the given nodes
        /// </summary>
        /// <param name="parentNode">Parent Node</param>
        /// <param name="childNode">Child node</param>
        /// <param name="connectionColor">Connector Color</param>
        private void ConnectNodes(Node parentNode, Node childNode, Color connectionColor)
        {
            if (parentNode != null && childNode != null)
            {
                LineConnector lConnector = new LineConnector(PointF.Empty, new PointF(0, 1));
                //lConnector.HeadDecorator.DecoratorShape = DecoratorShape.Filled45Arrow;               
                lConnector.HeadDecorator.FillStyle.Color = Color.White;
                lConnector.HeadDecorator.LineStyle.LineColor = connectionColor;
                lConnector.LineStyle.LineColor = connectionColor;


                parentNode.CentralPort.TryConnect(lConnector.TailEndPoint);
                childNode.CentralPort.TryConnect(lConnector.HeadEndPoint);
                this.diagram1.Model.AppendChild(lConnector);
                this.diagram1.Model.SendToBack(lConnector);
            }
        }
    }
}
