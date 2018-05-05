using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Diagram.Controls;

using SWD = Syncfusion.Windows.Forms.Diagram;
using SDC = Syncfusion.Windows.Forms.Diagram.Controls;
using NetworkDesigner.Utils.Common;
using NetworkDesigner.UI.Model;
using NetworkDesigner.Beans.DataStruct;
using NetworkDesigner.Utils.DiagramUtil;
using NetworkDesigner.Service.Model;
using NetworkDesigner.Beans.Model;
using NetworkDesigner.UI.ToolWindow;

namespace NetworkDesigner.UI.Document
{
    public partial class FrmDiagram : FrmDocBase
    {
        const string SaveAsFileFilter = "网络场景(*.dia)|*.dia|全部类型(*.*)|*.*";
        //"场景文件(*.dia)|*.dia|模型文件(*.mod)|*.mod";
        public SDC.Diagram ActiveDiagram
        {
            get
            {
                return this.diagram1;
            }
        }

        /// <summary>
        /// 鼠标左键按下时记录的坐标，弹起时置空
        /// </summary>
        private Point m_drawingPoint = Point.Empty;
        private RectangleF m_drawingRect = RectangleF.Empty;
        private FrmTopology topoEditor;
        private FrmTraffic trafficEditor;

        public FrmDiagram(FrmMain frm)
        {
            InitializeComponent();

            this.mainForm = frm;
            topoEditor = new FrmTopology(this);
            trafficEditor = new FrmTraffic(this);
            InitForm();
        }

        private void InitForm()
        {
            ActiveDiagram.BeginUpdate();

            DiagramAppearance();

            ActiveDiagram.Font = new Font("宋体", 10);
            ActiveDiagram.Controller.TextEditor.KeyDown += TextEditor_KeyDown;
            ActiveDiagram.Controller.InPlaceEditor.LabelEditingCompleted +=
                    new LabelEditingCompletedEventHandler(EventSink_EditingCompleted);
            ActiveDiagram.Model.EventSink.NodeCollectionChanged += new
                    CollectionExEventHandler(EventSink_NodeCollectionChanged);
            ActiveDiagram.EventSink.PropertyChanged +=
                    new Syncfusion.Windows.Forms.Diagram.PropertyChangedEventHandler(View_PropertyChanged);
            //ActiveDiagram.Model.EventSink.NodeCollectionChanged += new
            //CollectionExEventHandler(EventSink_NodeCollectionChanged);
            //((DiagramViewerEventSink)ActiveDiagram.EventSink).SelectionListChanged += new
            //CollectionExEventHandler(EventSink_NodeSelectionListChanged);

            this.miEditSelectAll.Click += ItemEditClick;
            this.miEditCopy.Click += ItemEditClick;
            this.miEditCut.Click += ItemEditClick;
            this.miEditPaste.Click += ItemEditClick;
            this.miEditDelete.Click += ItemEditClick;
            this.miUndo.Click += ItemEditClick;
            this.miRedo.Click += ItemEditClick;

            this.cmsTransQualnet.Click += cmsTransOutput_Click;
            this.cmsTransOPNET.Click += cmsTransOutput_Click;
            this.cmsTransMininet.Click += cmsTransOutput_Click;
            //重要：屏蔽在初始化窗体时触发的Model.Modified
            ActiveDiagram.Model.Modified = false;
            ActiveDiagram.EndUpdate();
        }
        private void cmsTransOutput_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem cms = sender as ToolStripMenuItem;
            if (cms != null)
            {
                SimScenario scenario = SaveToScenario(cms.Text);

                ScenarioRange range = scenario["SimRange"] as ScenarioRange;
                range["LefUp"] = new PointF(0, 0);
                range["RightUp"] = new PointF(ActiveDiagram.Width, 0);
                range["LeftDown"] = new PointF(0, ActiveDiagram.Height);
                range["RightDown"] = new PointF(ActiveDiagram.Width, ActiveDiagram.Height);
                range["Width"] = ActiveDiagram.Model.DocumentSize.Width;
                range["Height"] = ActiveDiagram.Model.DocumentSize.Height;

                scenario["SimName"] = cms.Text + "-" + SimScenario.counter;//SimScenario.counter++;
                scenario["SimTime"] = "1000";
                scenario["SimSeed"] = "1";

                scenario.traffics.Add(SimTraffic.GenerateTraffic("CBR",scenario.hosts));
                scenario.traffics.Add(SimTraffic.GenerateTraffic("CBR", scenario.hosts));
                scenario.traffics.Add(SimTraffic.GenerateTraffic("FTP", scenario.hosts));

                scenario.TransOutput(cms.Text);
            }
        }
        /// <summary>
        /// 将节点、链路、业务等信息保存至SimScenario中，方便后续映射转换---转换前必须先调用此方法
        /// </summary>
        public SimScenario SaveToScenario(string simulator)
        {
            SimModel model = null;
            SimAttr attr = null;
            List<SimModel> mods = null;
            SimScenario scenario = new SimScenario();
            string flag;
            foreach (Node node in ActiveDiagram.Model.Nodes)
            {
                model = node.Tag as SimModel;
                if (model == null)
                    continue;
                UpdateNodeModel(node);
                SimModelType type = ModelInfoHelper.GetModelType(model.modelID);
                switch (type)
                {
                    case SimModelType.节点:
                        scenario.nodes.Add(model["name"] as string, model);
                        model.ID = scenario.nodes.Count.ToString(); //从1开始计
                        attr = model.GetAttrIntfs();
                        if(attr!=null && attr.data is List<SimModel>)
                        {
                            mods = attr.data as List<SimModel>;
                            foreach (SimModel intf in mods) //接口都是在节点上，链路只是引用这些接口
                                scenario.intfs[intf["IF"] as string] = intf;
                        }
                        flag = model["flag"] as string;
                        if (flag!=null)
                        {
                            if(flag == "host")
                                scenario.hosts.Add(model);
                            else if(flag == "switch")
                                scenario.switchs.Add(model);
                            else if (flag == "route")
                                scenario.routes.Add(model);
                        }
                        if (simulator.Equals("Qualnet"))
                        {
                            model["qX"] = model["X"];
                            double p = double.Parse(model["Y"] as string);
                            model["qY"] = ActiveDiagram.Model.DocumentSize.Height - p;
                            model["qZ"] = model["Z"];
                        }
                        break;
                    case SimModelType.链路:
                        scenario.links.Add(model);
                        if(simulator.Equals("Qualnet"))
                        {
                            if (model.modelCate.Contains("有线链路") || model.modelCate.Contains("wiredLink"))
                                model["linkType"] = "WIRELESS";
                            else
                                model["linkType"] = "WIRED";
                            model["routeType"] = "OSPFv2";
                            model["bandwidth"] = "10000000";
                            model["dropProbability"] = "0.0";
                        }
                        
                        break;
                    //case SimModelType.业务:
                    //    break;
                    //case SimModelType.节点:
                    //    break;
                    default:
                        break;
                }
            }
            return scenario;
        }

        private void TextEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ActiveDiagram.Controller.TextEditor.EndEdit(true);
            }
        }

        private void EventSink_EditingCompleted(object sender, LabelEditingCompletedEventArgs evtArgs)
        {
            Node node = evtArgs.NodeAffected as Node;
            string text = evtArgs.Value.Trim();//不允许把Label名称前后有空格，并且为空时恢复默认值
            if (node != null)
            {
                if (text.Length > 0)
                {
                    node.Name = evtArgs.Value; //保持Label与Name的一致性
                }
                //不允许把Label编辑为空
                NodeHelper.SetNodeLabel0(node, node.Name);

            }
        }
        private void DealInsertLine(LineConnector line)
        {
            SimModel model = line.Tag as SimModel;
            if (model == null)
            {
                model = SimModel.CreateValModel(ActiveModelInfo.ActiveLink);
            }
            else
            {
                model = model.DeepCopy();
            }
            line.Tag = model;
            //line.FromNode as Node; //暂时访问不了，要NodeCollectionChanged执行完才能初始化此变量！
            NodeHelper.SetNodeLabel0(line, line.Name);
        }
        private void DealInsertNode(Node node)
        {
            SimModel model = null;
            string modelID = "";
            if (node.Tag is string) //从工具箱面板中拖放产生的节点或链路
            {
                modelID = node.Tag as string;
                if (ModelInfoHelper.allModels.ContainsKey(modelID))
                {
                    node.Tag = SimModel.CreateValModel(modelID);
                }
                else
                {
                    node.Tag = null;
                    LogHelper.LogInfo("新插入modelID不存在于ModelInfoHelper.allModels：" + modelID);
                }
            }
            else if (node.Tag is SimModel) //可能是从Diagram上复制粘贴的节点或链路
            {
                model = node.Tag as SimModel;
                node.Tag = model.DeepCopy();
            }
            else //非网络元素，例如仅仅是注释或画图产生的线
            {

            }

            NodeHelper.SetNodeLabel0(node, node.Name);
        }
        private void EventSink_NodeCollectionChanged(CollectionExEventArgs evtArgs)
        {
            //FrmDiagram不会有group命令---FrmSymbol才会有，这个命令会导致很多麻烦，详见FrmSymbol的NodeCollectionChanged
            if (evtArgs.ChangeType == CollectionExChangeType.Insert)
            {
                if (evtArgs.Element is LineConnector)//先判断链路，因为diagram中，link也被认为是Node
                {
                    DealInsertLine(evtArgs.Element as LineConnector);
                }
                else if (evtArgs.Element is Node)
                {
                    DealInsertNode(evtArgs.Element as Node);
                }
                else if (evtArgs.Elements is NodeCollection)
                {
                    NodeCollection ns = evtArgs.Elements as NodeCollection;
                    foreach (Node n in ns)
                    {
                        if (n is LineConnector)//先判断链路，因为diagram中，link也被认为是Node
                        {
                            DealInsertLine(n as LineConnector);
                        }
                        else if (n is Node)
                        {
                            DealInsertNode(n as Node);
                        }
                    }
                }
                //ActiveDiagram.Controller.SelectionList.Clear();
            }
        }

        /// <summary>
        /// Sets the Appearance of Diagram
        /// </summary>
        private void DiagramAppearance()
        {
            this.diagram1.Model.BoundaryConstraintsEnabled = false;
            this.diagram1.HorizontalRuler.BackgroundColor = Color.White;
            this.diagram1.VerticalRuler.BackgroundColor = Color.White;
            this.diagram1.View.Grid.GridStyle = GridStyle.Line;
            this.diagram1.View.Grid.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.diagram1.View.Grid.Color = Color.LightGray;
            this.diagram1.View.Grid.VerticalSpacing = 15;
            this.diagram1.View.Grid.HorizontalSpacing = 15;
            this.diagram1.Model.BackgroundStyle.GradientCenter = 0.5f;
            this.diagram1.View.HandleRenderer.HandleColor = Color.AliceBlue;
            this.diagram1.View.HandleRenderer.HandleOutlineColor = Color.SkyBlue;
            this.diagram1.Model.RenderingStyle.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.diagram1.View.BackgroundColor = Color.White;
            this.diagram1.Model.DocumentSize = new PageSize(1000, 800);
            this.diagram1.View.SelectionList.Clear();
        }

        #region 重写函数
        //这里不会执行更新DocInfo的操作，也即要么是全新加载，要么是在外面自行更新
        public override void LoadFile(DocInfo docInfo)
        {
            if (docInfo.T == DocInfo.DType.BLANK)
            {

            }
            else if (docInfo.T == DocInfo.DType.DIA)
            {
                if (!File.Exists(docInfo.filePath))
                {
                    MessageBox.Show("指定路径找不到该文件！\r\n" + docInfo.filePath);
                    return;
                }
                try
                {
                    ActiveDiagram.LoadBinary(docInfo.filePath);
                }
                catch (Exception e)
                {
                    MessageBox.Show("文件加载失败，详情查看日志！");
                    LogHelper.LogError(e);
                    return;
                }
            }
        }

        public override void SaveFile()
        {
            miSave_Click(null, null);
        }

        public override void SaveAsFile()
        {
            miSaveAs_Click(null, null);
        }

        public override Diagram GetActiveDiagram()
        {
            return ActiveDiagram;
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public override void VEditUndo()
        {
            ItemEditClick(this.miUndo, null);
        }
        /// <summary>
        /// 恢复
        /// </summary>
        public override void VEditRedo()
        {
            ItemEditClick(this.miRedo, null);
        }
        /// <summary>
        /// 复制
        /// </summary>
        public override void VEditCopy()
        {
            ItemEditClick(miEditCopy, null);
        }
        /// <summary>
        /// 剪切
        /// </summary>
        public override void VEditCut()
        {
            ItemEditClick(miEditCut, null);
        }
        /// <summary>
        /// 粘贴
        /// </summary>
        public override void VEditPaste()
        {
            ItemEditClick(miEditPaste, null);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public override void VEditDelete()
        {
            ItemEditClick(miEditDelete, null);
        }

        public override void GenerateTopo(TopoResult topo)
        {
            NodeCollection nodes = new NodeCollection();
            Node node;
            SimModel model;
            string modelID = "";
            Dictionary<string, Node> modID2Node = new Dictionary<string, Node>();
            foreach (TopoNode tn in topo.nodes)
            {
                modelID = tn.propSets["model"];
                if (ModelInfoHelper.allModels.ContainsKey(modelID))
                    model = ModelInfoHelper.allModels[modelID];
                else
                {
                    throw new Exception("未找到模型：" + tn.propSets["model"]);
                }
                if(modID2Node.ContainsKey(modelID))
                    node = modID2Node[modelID];
                else
                {
                    node = mainForm.m_toolBox.FindNodeByModelID(modelID);
                    modID2Node[modelID] = node;
                }
                node = (BitmapNode)node.Clone();
                node.PinPoint = new PointF(tn.x, tn.y);
                tn.diagramNode = node;
                //设置参数
                nodes.Add(node);
            }

            ActiveDiagram.BeginUpdate();
            ActiveDiagram.Model.Nodes.AddRange(nodes);

            //连接链路
            LineConnector link = null;
            TopoLink tlink = null;
            try
            {
                for (int i = 0; i < topo.links.Count;i++ )
                {
                    tlink = topo.links[i];
                    link = NodeHelper.ConnectNodes(diagram1,
                        topo.nodes[tlink.srcNode].diagramNode, topo.nodes[tlink.desNode].diagramNode);
                }
            }
            catch (Exception e)
            {
                ActiveDiagram.EndUpdate();
                throw new Exception("无法添加链路：" + tlink.srcNode + "<->" + tlink.desNode + e.Message + "\n"
                    +"请尝试检查当前激活的链路模型" +ActiveModelInfo.ActiveLink + "是否正确");
            }
            
            ActiveDiagram.Controller.SelectionList.Clear();
            DiagramHelper.SetViewCenter(ActiveDiagram, topo.range);
            ActiveDiagram.EndUpdate();
        }
        #endregion

        private void ItemEditClick(object sender, System.EventArgs e)
        {
            string name = "";
            if (sender is ToolStripMenuItem)
                name = ((ToolStripMenuItem)sender).Name;
            switch (name)
            {
                case "miEditSelectAll":
                    ActiveDiagram.Controller.SelectAll();
                    break;
                case "miEditCopy":
                    ActiveDiagram.Controller.Copy();
                    break;
                case "miEditCut":
                    ActiveDiagram.Controller.Cut();
                    break;
                case "miEditPaste":
                    try
                    {
                        ActiveDiagram.Controller.Paste();
                    }
                    catch (Exception)
                    {
                        mainForm.Write("不支持粘贴该类型\r\n", Color.Red);
                    }
                    break;
                case "miEditDelete":
                    ActiveDiagram.Controller.Delete();
                    break;
                case "miUndo":
                    ActiveDiagram.Model.HistoryManager.Undo();
                    break;
                case "miRedo":
                    ActiveDiagram.Model.HistoryManager.Redo();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 检查文档是否修改，若修改提示是否保存
        /// </summary>
        private void FlushDocSafely()
        {
            if (ActiveDiagram.Model.Modified)
            {
                this.Select();
                DialogResult dr = MessageBox.Show("文件：" + this.docInfo.fileName + "\r\n已修改，是否保存？",
                     "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (this.docInfo.T != DocInfo.DType.BLANK)
                    {
                        try
                        {
                            ActiveDiagram.SaveBinary(this.docInfo.filePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("文件保存失败，详情查看日志！");
                            LogHelper.LogError(ex);
                            return;
                        }
                        ResetTitleAfterSave();
                    }
                    else
                    {
                        SaveAs();
                    }
                }
            }
        }
        /// <summary>
        /// 检查文档是否修改，若修改提示是否保存或者取消，若取消返回false
        /// </summary>
        /// <returns></returns>
        private bool FlushDocYesNoCancel()
        {
            if (ActiveDiagram.Model.Modified)
            {
                DialogResult dr = MessageBox.Show("文件已修改，是否保存？", "提示",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                    return false;
                if (dr == DialogResult.Yes)
                {
                    if (this.docInfo.T != DocInfo.DType.BLANK)
                    {
                        try
                        {
                            ActiveDiagram.SaveBinary(this.docInfo.filePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("文件保存失败，详情查看日志！");
                            LogHelper.LogError(ex);
                            return true;//不返回false是为了防止关闭不了窗口
                        }
                        ResetTitleAfterSave();
                    }
                    else
                    {
                        SaveAs();
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 保存文件，若已保存过直接返回；若有文件名保存，否则执行另存
        /// </summary>
        void miSave_Click(object sender, System.EventArgs e)
        {
            if (!ActiveDiagram.Model.Modified)
                return;
            if (this.docInfo.T != DocInfo.DType.BLANK)
            {
                try
                {
                    ActiveDiagram.SaveBinary(this.docInfo.fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("文件保存失败，详情查看日志！");
                    LogHelper.LogError(ex);
                    return;
                }
                ResetTitleAfterSave();
            }
            else
            {
                SaveAs();
            }
        }
        void miSaveAs_Click(object sender, System.EventArgs e)
        {
            SaveAs();
        }

        private void SaveAs()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = SaveAsFileFilter;
                dialog.InitialDirectory = this.mainForm.GetProjectPath();
                dialog.FileName = this.docInfo.fileName;
                if (DialogResult.OK == dialog.ShowDialog())
                {
                    try
                    {
                        ActiveDiagram.SaveBinary(dialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("文件另存失败，详情查看日志！");
                        LogHelper.LogError(ex);
                        return;
                    }

                    FactoryDocument.UpdateDocumentInfo(this.docInfo.filePath, dialog.FileName);
                    ResetTitleAfterSave();
                }
            }
        }
        /// <summary>
        /// 文档内容得到保存等处理后调用
        /// </summary>
        public void ResetTitleAfterSave()
        {
            if (ActiveDiagram.Model.Modified)
            {
                System.Diagnostics.Debug.WriteLine("错误：ResetTitleAfterSave时Model.Modified仍然为true");
            }
            if (this.Text.EndsWith("*"))
                this.Text = this.Text.Substring(0, this.Text.Length - 1);
        }

        private void FrmDiagram_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.mainForm.m_isExiting) //主程序退出，在这之前已经处理过是否保存，并且这里不能再抛出异常
            {
                topoEditor.SafeClose();
                trafficEditor.SafeClose();
                return;
            }
            else if (e.CloseReason == CloseReason.MdiFormClosing)//点击父窗体关闭时，子窗体没有取消选项，只有是和否
            {
                this.FlushDocSafely();
                //这里不执行移除文档操作，由父窗体确定是否真正移除（确认退出程序时）
            }
            else //直接点击子窗体的关闭按钮，如果确定关闭要在这里移除文档
            {
                if (!FlushDocYesNoCancel())
                    e.Cancel = true;
                else
                {
                    this.m_isDisposing = true;
                    topoEditor.SafeClose();
                    trafficEditor.SafeClose();
                    FactoryDocument.RemoveDocument(this.docInfo.filePath);
                }
            }
        }
        private void diagram1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mainForm.ResetBarItemTool(this);
            }
        }
        /// <summary>
        /// 更新Node和其绑定Model之间的信息，如坐标、名称、接口等，目前认为所有节点的接口必须和其他节点有连接关系，不允许接口单独存在
        /// </summary>
        /// <param name="node"></param>
        private void UpdateNodeModel(Node node)
        {
            SimModel model = node.Tag as SimModel;
            if (model == null)
                return;
            model["名称"] = node.Name;
            model["X"] = node.PinPoint.X.ToString();
            model["Y"] = node.PinPoint.Y.ToString();
            SimAttr attrIntf = model.GetAttrIntfs();
            List<SimModel> intfs = null;
            if(attrIntf!=null)
                intfs = attrIntf.data as List<SimModel>;
            if (intfs == null)
                return; //??

            SimModelType type = ModelInfoHelper.GetModelType(model.modelID);
            //处理链路连线---貌似有链路、业务等类型的SimModel
            if (node is LineConnector || type== SimModelType.链路)
            {
                LineConnector line = node as LineConnector;
                if (line.FromNode == null || line.ToNode == null)
                {
                    ActiveDiagram.Model.Nodes.Remove(line);//链路必须有始有终
                    return;
                }
                if (intfs.Count == 0) //尚未初始化接口信息 //设置默认ip都是针对链路设置，或者网段
                {
                    List<SimModel> subIntfs = null;
                    Node from = line.FromNode as Node;
                    Node to = line.ToNode as Node;
                    SimModel fromModel = null;
                    SimModel toModel = null;
                    SimModel fromIntf = null;
                    SimModel toIntf = null;
                    if (from.Tag is SimModel)
                    {
                        UpdateNodeModel(from);
                        fromModel = from.Tag as SimModel;
                        model["SrcNode"] = fromModel;
                        subIntfs = fromModel.GetAttrIntfs().data as List<SimModel>;
                        foreach(SimModel subIntf in subIntfs)
                        {
                            if (line.Name.Equals(subIntf.intf2LinkName))
                            {
                                fromIntf = subIntf;
                                model["SrcIF"] = fromIntf["IF"];
                                intfs.Add(subIntf);
                                break;
                            }
                        }
                    }
                    if (to.Tag is SimModel)
                    {
                        UpdateNodeModel(to);
                        toModel = to.Tag as SimModel;
                        model["DestNode"] = toModel;
                        subIntfs = toModel.GetAttrIntfs().data as List<SimModel>;
                        foreach(SimModel subIntf in subIntfs)
                        {
                            if (line.Name.Equals(subIntf.intf2LinkName))
                            {
                                toIntf = subIntf;
                                model["DestIF"] = toIntf["IF"];
                                intfs.Add(subIntf);
                                break;
                            }
                        }
                    }
                    //目前只支持自动分配8位主机号的网段，且不对删除的节点进行回收处理
                    string net = "";
                    List<int> hostUsed = null;
                    bool fromLan = ModelInfoHelper.IsLanNet(fromModel.modelID);
                    bool toLan = ModelInfoHelper.IsLanNet(toModel.modelID);
                    if (fromLan)
                    {
                        net = fromModel["net"] as string;
                        if (net != null)
                        {
                            if(toLan)
                                toModel["net"] = net;
                        }
                        else if (toLan)
                        {
                            net = toModel["net"] as string;
                            if (net != null)
                                fromModel["net"] = net;
                            else
                            {
                                net = ModelInfoHelper.GetNextLanNet();
                                fromModel["net"] = net;
                                toModel["net"] = net;
                            }
                        }
                        else
                        {
                            net = ModelInfoHelper.GetNextLanNet();
                            fromModel["net"] = net;
                        }
                    }
                    else if (toLan)
                    {
                        net = toModel["net"] as string;
                        if (net == null)
                        {
                            net = ModelInfoHelper.GetNextLanNet();
                            toModel["net"] = net;
                        }
                    } 
                    else //from to 都不是网段型节点，则针对此链路新建网段
                        net = ModelInfoHelper.GetNextLanNet();
                    model["net"] = net;
                    model["nNet"] = "8";
                    object attr = fromIntf["IP"];
                    if(attr is SimModel)
                    {
                        ((SimModel)attr).attrs["ip"].data = ModelInfoHelper.GetNextLanHost(net);
                    }
                    attr = toIntf["IP"];
                    if (attr is SimModel)
                    {
                        ((SimModel)attr).attrs["ip"].data = ModelInfoHelper.GetNextLanHost(net);
                    }
                }
            }
            else if (node is BitmapNode || type == SimModelType.节点)
            {
                bool needUpdate = (intfs.Count != node.Edges.Count);
                LineConnector line = null;
                List<string> names = null;
                foreach (var link in node.Edges)
                {
                    line = link as LineConnector;
                    if (line.FromNode == null || line.ToNode == null)
                    {
                        ActiveDiagram.Model.Nodes.Remove(line);//链路必须有始有终
                        needUpdate = true;
                    }
                    if (!needUpdate)
                    {
                        if (names == null)
                            names = new List<string>();
                        names.Add(line.Name);
                    }
                }
                if (!needUpdate)
                {
                    foreach (SimModel intf in intfs)
                    {
                        if(!names.Contains(intf.intf2LinkName))
                        {
                            needUpdate = true;
                            break;
                        }
                    }
                }
                if(needUpdate)
                {
                    intfs.Clear();
                    foreach (var link in node.Edges)
                    {
                        line = link as LineConnector;
                        if (line != null)
                        {
                            SimModel simIntf = SimModel.CreateValModel(attrIntf.refModel);
                            simIntf.intf2LinkName = line.Name;
                            simIntf["IF"] = intfs.Count.ToString();
                            intfs.Add(simIntf);
                        }
                    }
                }
            }
        }
        private void diagram1_MouseDown(object sender, MouseEventArgs e)
        {
            if (ActiveDiagram.Model.Modified
                && !this.Text.EndsWith("*"))
            {
                this.Text += "*";
            }

            if (e.Button == MouseButtons.Right)
            {
                if (this.preActiveTool != "选择" && this.preActiveTool != "SelectTool")
                {
                    ActiveDiagram.Controller.ActivateTool("SelectTool");
                    this.preActiveTool = "选择";

                    //本次右击只是为了不再继续放置节点或链路，因此屏蔽掉右键菜单，在鼠标弹起时恢复
                    ActiveDiagram.ContextMenuStrip = this.cmsEmpty;
                }
            }

            m_drawingPoint = new Point(e.X + (int)ActiveDiagram.View.Origin.X,
                e.Y + (int)ActiveDiagram.View.Origin.Y); //注意：diagram放大时也会影响坐标值
            mainForm.SetStatusLabelPosInfo(m_drawingPoint);
        }
        private void diagram1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point curPoint = new Point(e.X + (int)ActiveDiagram.View.Origin.X,
                e.Y + (int)ActiveDiagram.View.Origin.Y);
                m_drawingRect = DiagramHelper.GetRectangle(m_drawingPoint, curPoint);
                mainForm.SetStatusLabelPosInfo(m_drawingRect);
            }
        }
        private void diagram1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) //继续激活上次激活的Tool，这样可以连续操作，直到按escape回到SelectTool
            {
                switch (this.preActiveTool)
                {
                    case "选择":
                    case "SelectTool":
                        //在属性编辑窗口显示模型属性
                        if (ActiveDiagram.Controller.SelectionList.Count > 0)
                        {
                            Node node = ActiveDiagram.Controller.SelectionList[0];
                            SimModel model = node.Tag as SimModel;
                            UpdateNodeModel(node);
                            mainForm.m_modelBox.ShowSimModel(model, node);
                        }
                        else
                            mainForm.m_modelBox.ShowSimModel(null);
                        break;
                    case "节点":
                        //根据选中模型的类型放置节点
                        if (ActiveModelInfo.ActiveType == SimModelType.节点)
                        {
                            Node n = (Node)ActiveModelInfo.paletteNode.Clone();
                            n.PinPoint = new PointF(e.Location.X + ActiveDiagram.View.Origin.X,
                                e.Location.Y + ActiveDiagram.View.Origin.Y);
                            ActiveDiagram.Model.Nodes.Add(n); //每个新添加的node都会触发NodeCollectionChanged，在那里处理tag
                            ActiveDiagram.View.SelectionList.Clear();
                        }
                        else
                            this.preActiveTool = "SelectTool";
                        break;
                    case "链路":
                        ActiveDiagram.Controller.ActivateTool("LineLinkTool");
                        break;
                    case "拓扑":
                    case "tsbTopo":
                        if (m_drawingRect.IsEmpty || m_drawingRect.Width < 1 || m_drawingRect.Height < 1)
                        {
                            MessageBox.Show("请按住左键拖放矩形用于部署节点");
                            return;
                        }
                        this.preActiveTool = "选择";
                        mainForm.ResetBarItemTool(this);
                        topoEditor.BeginEdit(ActiveModelInfo.ActiveTopo, m_drawingRect);
                        break;
                    default:
                        this.preActiveTool = "选择";
                        break;
                }
                //if(mainForm.m_BarName2DiagramTool.Values.Contains(this.preActiveTool))
                //{
                //    ActiveDiagram.Controller.ActivateTool(this.preActiveTool);
                //}
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (ActiveDiagram.ContextMenuStrip != this.cmsEditor)
                {
                    //本次右击只是为了不再继续放置节点或链路，因此屏蔽掉右键菜单，在鼠标弹起时恢复
                    ActiveDiagram.ContextMenuStrip = this.cmsEditor;
                }
            }
            m_drawingPoint = Point.Empty;
            m_drawingRect = RectangleF.Empty;
            //mainForm.SetStatusLabelPosInfo(m_drawingPoint);
        }
        private void diagram1_MouseWheelZoom(object sender, Syncfusion.Windows.Forms.MouseWheelZoomEventArgs e)
        {
            this.mainForm.UpdateZoomToText(this.ActiveDiagram);
        }

        private void View_PropertyChanged(SWD.PropertyChangedEventArgs evtArgs)
        {
            if (evtArgs.PropertyName == DPN.Magnification)//更新放大缩小显示的比例
            {
                this.mainForm.UpdateZoomToText(this.ActiveDiagram);
            }
        }

        private void FrmDiagram_Activated(object sender, EventArgs e)
        {
            this.mainForm.UpdateWhenFrmDiagramFoucus(this);
        }

        #region 临时功能
        private void miAllowResizeNode_Click(object sender, EventArgs e) //此功能可以放到属性面板中去
        {
            foreach (Node n in this.ActiveDiagram.Controller.SelectionList)
            {
                n.EditStyle.AllowChangeHeight = !n.EditStyle.AllowChangeHeight;
                n.EditStyle.AllowChangeWidth = !n.EditStyle.AllowChangeWidth;
            }
        }

        public void BeginSetTraffic()
        {
            if (!this.trafficEditor.Visible)
                trafficEditor.RefreshNodes();
            this.trafficEditor.Show();
        }
        #endregion

        private void cmsSetTraffic_Click(object sender, EventArgs e)
        {
            BeginSetTraffic();
        }

    }
}
