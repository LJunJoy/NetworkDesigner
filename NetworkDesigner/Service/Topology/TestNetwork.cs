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
    class TestNetwork
    {
        public Syncfusion.Windows.Forms.Diagram.Controls.Diagram diagram1;
        public Syncfusion.Windows.Forms.Diagram.Controls.PaletteGroupBar paletteGroupBar1;
        public int paletteNetworkIndex = 7;
        public TestNetwork(Diagram dia,PaletteGroupBar pal)
        {
            this.diagram1 = dia;
            this.paletteGroupBar1 = pal;

            InitailizeDiagram();
        }

        protected void InitailizeDiagram()
        {
            Layer ethnet = CreateLayer("Ethernet Layer");
            Layer controlnet = CreateLayer("ControlNet Layer");
            Layer devicenet = CreateLayer("DeviceNet Layer");

            //	Add TextNodes to display Layer Names
            InsertTextNode("Ethernet", new PointF(350, 180), ethnet);
            InsertTextNode("Control Net", new PointF(320, 370), controlnet);
            InsertTextNode("Device Net", new PointF(500, 550), devicenet);

            // Add  TextNodes to highlight Various SymbolName.
            // Add a TextNode to highlight the Diagram Name
            Syncfusion.Windows.Forms.Diagram.TextNode txtnode = new TextNode("WIRELESS NETWORK FLOW DIAGRAM");
            txtnode.FontStyle.Size = 17;
            txtnode.FontStyle.Bold = true;
            txtnode.FontStyle.Family = "Arial";
            txtnode.FontColorStyle.Color = Color.Black;
            txtnode.LineStyle.LineColor = Color.Transparent;
            txtnode.SizeToText(Size.Empty);
            InsertNode(txtnode, new PointF(415, 25), null);

            if (this.paletteGroupBar1.CurrentSymbolPalette == null)
                return;

            // TextNode For Servers
            InsertHighlightTextNode("Servers", new PointF(250, 100), ethnet);
            // TextNode For WorkStations
            InsertHighlightTextNode("WorkStations", new PointF(480, 100), ethnet);
            // TextNode For Modem
            InsertHighlightTextNode("Modem", new PointF(600, 175), ethnet);
            // TextNode For Modem1
            InsertHighlightTextNode("Modem1", new PointF(730, 120), ethnet);
            // TextNode For RemoteController
            InsertHighlightTextNode("RemoteController", new PointF(870, 100), ethnet);
            // TextNode For RemoteWorkStations
            InsertHighlightTextNode("Remote WorkStations", new PointF(930, 250), ethnet);
            // TextNode For Modem2
            InsertHighlightTextNode("Modem2", new PointF(650, 270), ethnet);
            // TextNode For PortableWorkStations
            InsertHighlightTextNode("Portable WorkStation", new PointF(930, 320), ethnet);
            // TextNode For Modem3
            InsertHighlightTextNode("Modem3", new PointF(500, 350), ethnet);
            // TextNode For Modem4
            InsertHighlightTextNode("Modem4", new PointF(450, 230), ethnet);

            // TextNode For ControlLogic
            InsertHighlightTextNode("ControlLogic", new PointF(170, 400), controlnet);
            // TextNode For ControlLogic1
            InsertHighlightTextNode("ControlLogic1", new PointF(750, 360), controlnet);

            // TextNode For Hub Management Interface
            InsertHighlightTextNode("HMI", new PointF(910, 410), devicenet);
            // TextNode For DriveC
            InsertHighlightTextNode("DriveC", new PointF(800, 520), devicenet);
            // TextNode For DriveB
            InsertHighlightTextNode("DriveB", new PointF(650, 520), devicenet);
            // TextNode For DriveA
            InsertHighlightTextNode("DriveA", new PointF(550, 520), devicenet);
            // TextNode For Sensor
            InsertHighlightTextNode("Sensor", new PointF(400, 510), devicenet);
            // TextNode For AnalogI/O Device
            InsertHighlightTextNode("Analog I/O Device", new PointF(250, 550), devicenet);

            // Insert the Network Symbols.
            // Insert server  
            Node server = InsertNodeFromPallete(0, new PointF(236, 100), ethnet);
            //Insert server1  
            Node server1 = InsertNodeFromPallete(0, new PointF(186, 100), ethnet);
            // Insert WorkStation
            Node ws = InsertNodeFromPallete(1, new PointF(420, 100), ethnet);
            //Insert WorkStation 
            Node ws1 = InsertNodeFromPallete(1, new PointF(520, 100), ethnet);
            // Insert modem
            Node modem = InsertNodeFromPallete(2, new PointF(630, 140), ethnet);
            //Insert modem1 
            Node modem1 = InsertNodeFromPallete(2, new PointF(760, 140), ethnet);
            //Insert Portable WS
            Node rc = InsertNodeFromPallete(3, new PointF(890, 110), ethnet);
            //Insert RemoteWorkStations
            Node rws = InsertNodeFromPallete(1, new PointF(850, 230), ethnet);
            //Insert modem2
            Node modem2 = InsertNodeFromPallete(2, new PointF(650, 220), ethnet);
            //Insert RemoteController
            Node pws = InsertNodeFromPallete(1, new PointF(850, 320), ethnet);
            //Insert modem3
            Node modem3 = InsertNodeFromPallete(2, new PointF(500, 300), ethnet);
            //Insert modem4  
            Node modem4 = InsertNodeFromPallete(2, new PointF(500, 200), ethnet);

            //Insert ControlLogic
            Node clx = InsertNodeFromPallete(3, new PointF(800, 360), controlnet);
            //Insert ControlLogic1
            Node clx1 = InsertNodeFromPallete(3, new PointF(252, 360), controlnet);

            // Insert HMI
            Node hmi = InsertNodeFromPallete(4, new PointF(880, 410), devicenet);
            // Insert Analog I/O Device
            Node analog = InsertNodeFromPallete(5, new PointF(250, 470), devicenet);
            // InsertSensor Device
            Node sensor = InsertNodeFromPallete(6, new PointF(450, 470), devicenet);
            //Insert DriveA
            Node driveA = InsertNodeFromPallete(7, new PointF(550, 470), devicenet);
            //Insert DriveB
            Node driveB = InsertNodeFromPallete(7, new PointF(650, 470), devicenet);
            //Insert DriveC
            Node driveC = InsertNodeFromPallete(7, new PointF(800, 470), devicenet);

            //Insert Arrows to indicate the Network Flow.
            InsertNodeFromPallete(8, new PointF(400, 170), 0, ethnet);

            InsertNodeFromPallete(8, new PointF(250, 170), 90, ethnet);
            InsertNodeFromPallete(8, new PointF(565, 340), 0, controlnet);
            InsertNodeFromPallete(8, new PointF(270, 420), 90, devicenet);
            InsertNodeFromPallete(8, new PointF(575, 420), 90, devicenet);
            InsertNodeFromPallete(8, new PointF(250, 280), 90, ethnet);

            // Create Links between the NetworkSymbols.
            ConnectNodes(server, server1);
            ConnectNodes(server1, ws);
            ConnectNodes(modem4, server1);
            ConnectNodes(server, clx1);
            ConnectNodes(ws, ws1);
            ConnectNodes(ws1, modem);
            ConnectNodes(ws1, modem4);
            ConnectNodes(modem1, modem);
            ConnectNodes(modem1, rc);
            ConnectNodes(modem2, rws);
            ConnectNodes(modem3, pws);
            ConnectNodes(modem3, modem4);
            ConnectNodes(modem2, modem4);
            ConnectNodes(clx, clx1);
            ConnectNodes(clx, driveB);
            ConnectNodes(clx1, analog);
            ConnectNodes(clx, driveA);
            ConnectNodes(clx1, sensor);
            ConnectNodes(hmi, driveC);
            ConnectNodes(hmi, driveB);
        }
        /// <summary>
        /// Creates the Layer with given name
        /// </summary>
        /// <param name="strName">Layer's Name</param>
        /// <returns>returns the layer</returns>
        private Layer CreateLayer(string strName)
        {
            Layer layer = new Layer(this.diagram1.Model, strName);

            this.diagram1.Controller.Model.Layers.Add(layer);
            return layer;
        }

        /// <summary>
        /// Connects the nodes
        /// </summary>
        /// <param name="parentNode">Parent node</param>
        /// <param name="subNode">Child node</param>
        private void ConnectNodes(Node parentNode, Node subNode)
        {
            if (parentNode.CentralPort == null || subNode.CentralPort == null)
                return;

            this.diagram1.Model.BeginUpdate();

            // Create directed link
            PointF[] pts = new PointF[] { PointF.Empty, new PointF(0, 1) };
            OrthogonalConnector line = new OrthogonalConnector(pts[0], pts[1]);
            line.HeadDecorator.DecoratorShape = DecoratorShape.Filled45Arrow;
            line.HeadDecorator.FillStyle.Color = Color.Black;
            line.LineStyle.LineColor = Color.Black;

            this.diagram1.Model.AppendChild(line);

            parentNode.CentralPort.TryConnect(line.TailEndPoint);
            subNode.CentralPort.TryConnect(line.HeadEndPoint);
            this.diagram1.Model.SendToBack(line);

            this.diagram1.Model.EndUpdate();
        }

        /// <summary>
        /// Insert the node from palette
        /// </summary>
        /// <param name="nNodeIndex">Index</param>
        /// <param name="ptPinPoint">Node's Location</param>
        /// <param name="angle">Rotation angle</param>
        /// <param name="layer">Layer</param>
        /// <returns></returns>
        private Node InsertNodeFromPallete(int nNodeIndex, PointF ptPinPoint, float angle, Layer layer)
        {
            Node node = InsertNodeFromPallete(nNodeIndex, ptPinPoint, layer);
            node.Rotate(angle);
            node.Size = new SizeF(20, 20);
            return node;
        }

        /// <summary>
        /// Insert the node from palette
        /// </summary>
        /// <param name="nNodeIndex">Index</param>
        /// <param name="ptPinPoint">Node's Location</param>
        /// <param name="layer">Layer</param>
        /// <returns>returns the node</returns>
        private Node InsertNodeFromPallete(int nNodeIndex, PointF ptPinPoint, Layer layer)
        {
            Node node = null;
            SymbolPalette pal = paletteGroupBar1.GetPalette(paletteNetworkIndex);
            NodeCollection nodes = pal.Nodes;

            if (nNodeIndex >= 0 && nNodeIndex < nodes.Count)
            {
                node = (Node)nodes[nNodeIndex].Clone();
                InsertNode(node, ptPinPoint, layer);
            }
            node.Size = new SizeF(50, 50);
            return node;
        }

        /// <summary>
        /// Insert Node into the diagram
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="ptPinPoint">Node's Location</param>
        /// <param name="layer">Layer</param>
        /// <returns>returns the node</returns>
        private Node InsertNode(Node node, PointF ptPinPoint, Layer layer)
        {
            MeasureUnits units = MeasureUnits.Pixel;

            SizeF szPinOffset = ((IUnitIndependent)node).GetPinPointOffset(units);
            ptPinPoint.X += szPinOffset.Width;
            ptPinPoint.Y += szPinOffset.Height;
            ((IUnitIndependent)node).SetPinPoint(ptPinPoint, units);

            node.EnableCentralPort = true;
            node.ShadowStyle.Visible = false;
            this.diagram1.Model.AppendChild(node);

            if (layer != null)
            {
                layer.Nodes.Add(node);
                node.Layers.Add(layer);
            }

            return node;
        }

        /// <summary>
        /// Insert the TextNode
        /// </summary>
        /// <param name="strText">TextNode's Text</param>
        /// <param name="ptPinPoint">Node's Location</param>
        /// <param name="layer">Layer</param>
        /// <returns>returns the text node</returns>
        private TextNode InsertTextNode(string strText, PointF ptPinPoint, Layer layer)
        {
            Syncfusion.Windows.Forms.Diagram.TextNode txtnode = new TextNode(strText);
            txtnode.FontStyle.Size = 15;
            txtnode.FontStyle.Family = "Arial";
            txtnode.FontColorStyle.Color = Color.Black;
            txtnode.LineStyle.LineColor = Color.Transparent;
            txtnode.SizeToText(SizeF.Empty);
            InsertNode(txtnode, ptPinPoint, layer);

            return txtnode;
        }
        /// <summary>
        /// Insert the TextNode
        /// </summary>
        /// <param name="strText">TextNode's Text</param>
        /// <param name="ptPinPoint">Node's Location</param>
        /// <param name="layer">Layer</param>
        /// <returns>returns the text node</returns>
        private TextNode InsertHighlightTextNode(string strText, PointF ptPinPoint, Layer layer)
        {
            Syncfusion.Windows.Forms.Diagram.TextNode txtnode = new TextNode(strText);
            txtnode.FontStyle.Size = 10;
            txtnode.FontStyle.Family = "Arial";
            txtnode.FontColorStyle.Color = Color.Black;
            txtnode.LineStyle.LineColor = Color.Transparent;
            txtnode.SizeToText(SizeF.Empty);

            InsertNode(txtnode, ptPinPoint, layer);

            return txtnode;
        }
    }
}
