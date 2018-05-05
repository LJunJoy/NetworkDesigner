using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Diagram.Controls;
using SWD = Syncfusion.Windows.Forms.Diagram;
using SDC = Syncfusion.Windows.Forms.Diagram.Controls;
using NetworkDesigner.Utils.Common;
using NetworkDesigner.Utils.DiagramUtil;
using NetworkDesigner.Utils.FileUtil;
using NetworkDesigner.UI.Dialog;
using NetworkDesigner.UI.Model;
using NetworkDesigner.Beans.DataStruct;
using NetworkDesigner.Service.Model;
using NetworkDesigner.Beans.Model;

namespace NetworkDesigner.UI.Document
{
    public partial class FrmSymbol : FrmDocBase
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool OpenClipboard(IntPtr hWndNewOwner);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool CloseClipboard();
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetClipboardData(uint format);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsClipboardFormatAvailable(uint format);

        public SDC.Diagram ActiveDiagram
        {
            get
            {
                return this.diagram1;
            }
        }

        public ModelInfo DesignModel
        {
            get;
            set;
        }

        public FrmSymbol(FrmMain frm)
        {
            InitializeComponent();

            this.mainForm = frm;
            topoEditor = new FrmTopology(this);
            InitForm();
        }
        /// <summary>
        /// 鼠标左键按下时记录的坐标，弹起时置空
        /// </summary>
        private Point m_drawingPoint = Point.Empty;
        private RectangleF m_drawingRect = RectangleF.Empty;
        private FrmTopology topoEditor;

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
            //this.Paint += FrmSymbol_Paint;
            //((DiagramViewerEventSink)diagram1.EventSink).ToolActivated +=
            //        new ToolEventHandler(EventSink_ToolActivated);
            //((DiagramViewerEventSink)diagram1.EventSink).ToolDeactivated +=
            //        new ToolEventHandler(EventSink_ToolDeactivated);
            //((DocumentEventSink)model1.EventSink).PropertyChanged +=
            //        new SWD.PropertyChangedEventHandler(Model_PropertyChanged);
            //((DiagramViewerEventSink)diagram1.EventSink).SelectionListChanged += new
            //        CollectionExEventHandler(EventSink_NodeSelectionListChanged);
            this.miEditSelectAll.Click += ItemEditClick;
            this.miEditCopy.Click += ItemEditClick;
            this.miEditCut.Click += ItemEditClick;
            this.miEditPaste.Click += ItemEditClick;
            this.miEditDelete.Click += ItemEditClick;
            this.miUndo.Click += ItemEditClick;
            this.miRedo.Click += ItemEditClick;
            //重要：屏蔽在初始化窗体时触发的Model.Modified
            ActiveDiagram.Model.Modified = false;
            ActiveDiagram.EndUpdate();
        }

        private void TextEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ActiveDiagram.Controller.TextEditor.EndEdit(true);
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

        /// <summary>
        /// 加载面板中的模型，若diagram上已有模型先清空
        /// </summary>
        /// <param name="palette"></param>
        public void LoadPalette(SymbolPalette palette)
        {
            this.ActiveDiagram.BeginUpdate();
            this.ActiveDiagram.Model.Clear();
            this.ActiveDiagram.View.Origin = new PointF(0, 0);//视图回到0点
            NodeCollection nodes = new NodeCollection();
            Node n=null;
            foreach (Node node in palette.Nodes)//查看源码可以发现palette.Nodes获得的是新的Collection，但是却装的原来node的引用
            {
                n = (Node)node.Clone();
                nodes.Add(n);
            }
            PaletteHelper.LayoutGrid(nodes, ActiveDiagram.View.ClientRectangle.Location, ActiveDiagram.View.ClientRectangle.Size);
            this.ActiveDiagram.Model.Nodes.AddRange(nodes); //每一个新添加的node都会触发NodeCollectionChanged，在那里深拷贝tag
            this.ActiveDiagram.View.SelectionList.Clear();
            this.ActiveDiagram.Model.Modified = false;//刚加载，屏蔽掉加载过程中引起的修改标志
            this.ActiveDiagram.EndUpdate();
        }

        #region 重写函数
        public override void LoadFile(DocInfo docInfo)
        {
            
        }

        public override void SaveFile()
        {
            //添加或更新到工具箱面板并保存文件
            //if (!ActiveDiagram.Model.Modified)
            //    return;
            if (this.docInfo.T != DocInfo.DType.BLANK)
            {
                miSaveToPalette_Click(null, null);
            }
            else
            {
                SaveAsFile();
            }
        }

        public override void SaveAsFile()
        {
            //添加到工具箱面板并另存为文件
            miSaveAsPalette_Click(null, null);
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
            ActiveDiagram.BeginUpdate();
            foreach (TopoNode tn in topo.nodes)
            {
                //model = ModelInfoHelper.FindModelInfo(tn.propSets["model"]);
                //if (model == null)
                //{
                //    MessageBox.Show("无法放置节点，找不到模型：" + tn.propSets["model"]);
                //    return;
                //}
                //node = (BitmapNode)model.DiagramNode.Clone();
                //node.PinPoint = new PointF(tn.x, tn.y);
                //tn.diagramNode = node;
                ////设置参数
                //nodes.Add(node);
            }
            //连接链路
            LineConnector link = null;
            TopoLink tlink = null;
            try
            {
                for (int i = 0; i < topo.links.Count; i++)
                {
                    tlink = topo.links[i];
                    link = NodeHelper.ConnectNodes(diagram1,
                        topo.nodes[tlink.srcNode].diagramNode, topo.nodes[tlink.desNode].diagramNode);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("无法添加链路：" + tlink.srcNode + "<->" + tlink.desNode + "\n" + e.Message);
                return;
            }

            ActiveDiagram.Model.Nodes.AddRange(nodes);
            ActiveDiagram.Controller.SelectionList.Clear();

            DiagramHelper.SetViewCenter(ActiveDiagram, topo.range);
            ActiveDiagram.EndUpdate();
        }
        #endregion

        /// <summary>
        /// 文档内容得到保存等处理后调用
        /// </summary>
        public void ResetTitleAfterSave()
        {
            if (ActiveDiagram.Model.Modified)
            {
                ActiveDiagram.Model.Modified = false;
            }
            if (this.Text.EndsWith("*"))
                this.Text = this.Text.Substring(0, this.Text.Length - 1);
        }

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
                    if (ActiveDiagram.Controller.CanPaste) //typeof(ClipboardNodeCollection))或者string
                    {
                        try
                        {
                            ActiveDiagram.Controller.Paste();
                        }
                        catch (Exception)
                        {
                            mainForm.Write("不支持粘贴该类型\r\n", Color.Red);
                        }
                    }
                    else if (Clipboard.ContainsFileDropList()) //从系统资源管理器中复制多个文件
                        DiagramPasteFiles();
                    else //内存中的图片
                        DiagramPasteOther();
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
                DialogResult dr = MessageBox.Show("文件" + this.docInfo.fileName + "\r\n已修改，是否保存？",
                        "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    miSaveToPalette_Click(null, null);                       
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
                    miSaveToPalette_Click(null, null);
                }
            }
            return true;
        }
        private void FrmSymbol_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.mainForm.m_isExiting) //主程序退出，在这之前已经处理过是否保存，并且这里不能再抛出异常
            {
                topoEditor.SafeClose();
                return;
            }
            else if (e.CloseReason == CloseReason.MdiFormClosing)//点击父窗体关闭时，子窗体没有取消选项，只有是和否
            {
                //this.FlushDocSafely();
                //这里不执行移除文档操作，由父窗体确定是否真正移除（确认退出程序时）
            }
            else //直接点击子窗体的关闭按钮，如果确定关闭要在这里移除文档
            {
                //if (!FlushDocYesNoCancel())
                //    e.Cancel = true;
                //else
                {
                    m_isDisposing = true;
                    topoEditor.SafeClose();
                    FactoryDocument.RemoveDocument(this.docInfo.filePath);
                }
            }
        }

        private void View_PropertyChanged(SWD.PropertyChangedEventArgs evtArgs)
        {
            if (evtArgs.PropertyName == DPN.Magnification)//更新放大缩小显示的比例
            {
                this.mainForm.UpdateZoomToText(this.ActiveDiagram);
            }
        }

        private void diagram1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mainForm.ResetBarItemTool(this);
            }
        }
        private void diagram1_Click(object sender, EventArgs e)
        {
            //if (ActiveDiagram.Model.Modified && !this.Text.EndsWith("*"))
            //{
            //    this.Text += "*";
            //}
            MouseEventArgs me = (MouseEventArgs)e;
            //if (diagram1.Controller.SelectionList.Count == 1)
            //{
            //    ModelInfo model = diagram1.Controller.SelectionList[0].Tag as ModelInfo;
            //    if (model != null)
            //    {
            //        mainForm.MainProperty.SelectedObject = model;
            //    }
            //    else
            //        mainForm.MainProperty.SelectedObject = null;
            //}
            //else
            //    mainForm.MainProperty.SelectedObject = null;

            if (mainForm.TextNodeToolChecked) //对文本节点工具作单独的定制，不用dll中原有的TextTool
            {
                RectangleF rec = new RectangleF(me.Location, new SizeF(100, 30));
                TextNode n = new TextNode("", rec);
                n.FontStyle.PointSize = 10;
                n.LineStyle.LineColor = Color.SkyBlue;
                ActiveDiagram.Model.AppendChild(n);
                ActiveDiagram.Controller.TextEditor.BeginEdit(n, false);
            }
        }

        private void diagram1_MouseDown(object sender, MouseEventArgs e)
        {
            //如果是点击右键，添加上下文菜单
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                NodeCollection nodes = this.ActiveDiagram.Controller.SelectionList;
                if (nodes.Count != 0)//判断你点的是不是一个节点
                {
                    this.miAddToCurrentPalette.Enabled = true;
                    this.miAddToSelectPalette.Enabled = true;
                    this.miDesignModel.Enabled = true;
                }
                else
                {
                    this.miAddToCurrentPalette.Enabled = false;
                    this.miAddToSelectPalette.Enabled = false;
                    this.miDesignModel.Enabled = false;
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
                if (!"SelectTool".Equals(this.preActiveTool)
                    && mainForm.m_BarName2DiagramTool.Values.Contains(this.preActiveTool))
                {
                    ActiveDiagram.Controller.ActivateTool(this.preActiveTool);
                }
                else if (this.preActiveTool.Equals("tsbTopo"))
                {
                    if (m_drawingRect.IsEmpty || m_drawingRect.Width < 1 || m_drawingRect.Height < 1)
                    {
                        MessageBox.Show("请按住左键拖放矩形用于部署节点");
                        return;
                    }
                    this.preActiveTool = "";
                    mainForm.ResetBarItemTool(this);
                    topoEditor.BeginEdit(NetworkDesigner.UI.ToolWindow.ActiveModelInfo.ActiveTopo, m_drawingRect);
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

        private void FrmSymbol_Activated(object sender, EventArgs e)
        {
            this.mainForm.UpdateWhenFrmDiagramFoucus(this);
        }
        /// <summary>
        /// 将diagram上的模型全部添加对应的面板中
        /// </summary>
        private void miSaveToPalette_Click(object sender, EventArgs e)
        {
            if (this.docInfo.T == DocInfo.DType.BLANK)
            {
                miSaveAsPalette_Click(null, null);
            }
            else
            {
                mainForm.m_toolBox.AddToPalette(ActiveDiagram, this.docInfo.filePath);
                mainForm.m_toolBox.SaveSinglePalette(this.docInfo.filePath);
                ResetTitleAfterSave();
            }
        }
        /// <summary>
        /// 将diagram上的模型全部添加到新的面板
        /// </summary>
        private void miSaveAsPalette_Click(object sender, EventArgs e)
        {
            DiaSaveToPalette dia = new DiaSaveToPalette();
            dia.PopulateList(mainForm.m_toolBox.GetPalettesPath());
            if (dia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dia.FilePath;
                if (!file.Contains(":")) //不是绝对路径
                    file = Path.Combine(NetworkDesigner.Beans.Common.AppSetting.DefaultModelDirPath, Path.GetFileName(file));
                this.docInfo = new DocInfo(file);
                this.ToolTipText = file;
                mainForm.m_toolBox.AddToPalette(ActiveDiagram, file);
                mainForm.m_toolBox.SaveSinglePalette(this.docInfo.filePath);
                ResetTitleAfterSave();
            }
            dia.Dispose();
        }

        private void miAddToCurrentPalette_Click(object sender, EventArgs e)
        {
            if (mainForm.MainGroupBar.SelectedItem < 0 ||
                mainForm.MainGroupBar.SelectedItem >= mainForm.MainGroupBar.GroupBarItems.Count)
            {
                mainForm.Write("未选择有效的工具箱面板", Color.Red);
                return;
            }
            Syncfusion.Windows.Forms.Tools.GroupBarItem item = 
                mainForm.MainGroupBar.GroupBarItems[mainForm.MainGroupBar.SelectedItem];
            DocInfo docInfo = null;
            if (item.Client is PaletteGroupView)
            {
                docInfo = item.Client.Tag as DocInfo;
                if(docInfo != null)
                    mainForm.m_toolBox.AddToPalette(ActiveDiagram.Controller.SelectionList, docInfo.filePath);
            }
        }

        private void miAddToSelectPalette_Click(object sender, EventArgs e)
        {
            DiaSaveToPalette dia = new DiaSaveToPalette();
            dia.PopulateList(mainForm.m_toolBox.GetPalettesPath());
            if (dia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dia.FilePath;
                if (!file.Contains(":")) //不是绝对路径
                    file = Path.Combine(NetworkDesigner.Beans.Common.AppSetting.DefaultModelDirPath, Path.GetFileName(file));
                mainForm.m_toolBox.AddToPalette(ActiveDiagram.Controller.SelectionList,file);
            }
            dia.Dispose();
        }

        private PointF GetPastePoint(int index)
        {
            float x = (float)this.Width / 4 + this.diagram1.View.Origin.X; //当前视窗的左上角1/4处
            float y = (float)this.Height / 4 + this.diagram1.View.Origin.Y;
            x += index * 15;//每次偏移像素点
            y += index * 15;//每次偏移像素点
            return new PointF(x, y);
        }

        private void DiagramPasteFiles()
        {
            //剪切板是可以复制的多个文件时
            System.Collections.Specialized.StringCollection sc = Clipboard.GetFileDropList();
            int index = 0;
            foreach (var item in sc)
            {
                if(ImageHelper.IsFileImage(item))
                {
                    Image im = Image.FromFile(item);
                    if (im != null)
                    {
                        AddBitmapNode(im,GetPastePoint(index++));
                        im.Dispose();
                    }
                }
            }
        }
        private void DiagramPasteOther()
        {
            //暂只允许复制图片
            if (OpenClipboard(IntPtr.Zero))
            {
                Image im = null;
                //从visio等复制来的metafile转成图片，若直接构造node，会在双击时报错，原因不明
                if (IsClipboardFormatAvailable((uint)ClipboardFormats.CF_ENHMETAFILE))
                {
                    var ptr = GetClipboardData((uint)ClipboardFormats.CF_ENHMETAFILE);
                    if (!ptr.Equals(IntPtr.Zero))
                    {
                        Metafile mf = new Metafile(ptr, true);
                        im = new Bitmap(mf);
                    }
                }
                else if (Clipboard.ContainsImage())
                {
                    im = Clipboard.GetImage();
                }
                if (im != null)
                {
                    AddBitmapNode(im,GetPastePoint(0));
                    im.Dispose();
                }
                CloseClipboard();// You must close it, or it will be locked
            }
        }
        
        private void AddBitmapNode(Image im,PointF location)
        {
            BitmapNode node = new BitmapNode(new Bitmap(im));//这里是坑，一定要new Bitmap包装一下
            node.EditStyle.HidePinPoint = true;
            node.LineStyle.LineColor = Color.Transparent;//经测试从visio复制会多个黑色边框，这里隐藏之
            node.PinPoint = location;

            ActiveDiagram.Model.AppendChild(node); 
        }

        private void EventSink_EditingCompleted(object sender, LabelEditingCompletedEventArgs evtArgs)
        {
            Node node = evtArgs.NodeAffected as Node;
            string text = evtArgs.Value.Trim();//不允许把Label名称前后有空格，并且为空时恢复默认值
            if (node != null)
            {
                if (text.Length > 0)
                    node.Name = evtArgs.Value; //保持Label与Name的一致性
                //不允许把Label编辑为空
                NodeHelper.SetNodeLabel0(node, node.Name);
            }
        }
        //private Dictionary<string,int> m_GroupChild = new Dictionary<string,int>();
        private void EventSink_NodeCollectionChanged(CollectionExEventArgs evtArgs)
        {
            //注意：经测试，当把多个node通过grouptool命令组合为一个group时(单个node时grouptool命令无效)，会多次触发此方法的调用，具体表现如下：
            //（一）首先会新增group触发一次，然后会把group里面的node都视作新增的分别触发，此时内部node通过owner-parent属性指向group
            //（二）而复制粘贴一个group时只触发一次，即只认为是新增一个group，并且只有一个node时点击grouptool也不会执行gruop，不会触发此方法
            //反之，ungrouptool命令执行时会触发一次clear一次remove删除掉group，然后把group原来的node都视作新增的而分别触发
            //另外很重要的一点，在新增节点并触发这个方法时，节点本身没有完成初始化，包括中心坐标、链路源/目的节点都未知，很多操作都不适合在这里！！！
            if (evtArgs.ChangeType == CollectionExChangeType.Insert)
            {
                //Group group = null;
                //if(evtArgs.Element is Node) //注意diagram中，link也被认为是Node
                //{
                //    if (evtArgs.Element is Group)
                //    {
                //        group = evtArgs.Element as Group;
                //        PaletteHelper.SetNodeInSymbol(group);
                //        NodeHelper.SetNodeLabel0(group, group.Name);
                //        //NodeHelper.SetNodeTag(group); //临时用一下，buildModel时会再生成

                //        if (group.Nodes.Count != 0) //说明是触发的（二）
                //        {
                //            ModelInfo groupModel = group.Tag as ModelInfo;
                //            if (groupModel != null) //可能是复制的另一个group，或者从面板拖的一个group，这时要深拷贝解除与原始tag的关联
                //            {
                //                groupModel = groupModel.DeepCopy();
                //                SetGroupModel(group, groupModel);
                //                group.Tag = groupModel;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        Node n = evtArgs.Element as Node;
                //        PaletteHelper.SetNodeInSymbol(n);
                //        NodeHelper.SetNodeLabel0(n, n.Name);
                //        NodeHelper.SetNodeTag(n, n);
                //    }
                //}
                //else if (evtArgs.Elements is NodeCollection)
                //{
                //    NodeCollection ns = evtArgs.Elements as NodeCollection;
                //    foreach (Node n in ns)
                //    {
                //        if (evtArgs.Owner is Group) //说明是触发的（一）
                //        {
                //            n.EditStyle.AllowSelect = false;
                //            continue; //由于链路等信息没有初始化完全，放弃在这里处理
                //        }
                //        else
                //        {
                //            PaletteHelper.SetNodeInSymbol(n);
                //            NodeHelper.SetNodeLabel0(n, n.Name);
                //            NodeHelper.SetNodeTag(n, n);
                //        }
                //    }
                //}
                
            }
        }
        private void SetGroupModel(Group group, ModelInfo groupModel)
        {
            groupModel.DiagramNode = group;
            groupModel.ModelName = group.Name;
            //groupModel.ModelImage = //?在属性浏览器中设置？
            groupModel.pinpoint = group.PinPoint;
            groupModel.range = group.Size;
            //groupModel.scale = ; //?在属性浏览器中设置？
        }
        private void BuildGroupModel(Group group)
        {
            ModelInfo groupModel = group.Tag as ModelInfo;
            if(groupModel != null && groupModel.properties.Count!=0 ) //可能是复制的另一个group，或者从面板拖的一个group
            {
                return; //这种情况下之前已经build过且EventSink_NodeCollectionChanged已经DeepCopy过了，所在不再操作
            }
            else
            {
                groupModel = new ModelInfo();
                SetGroupModel(group,groupModel);
                group.Tag = groupModel;
            }
            ModelInfo subModel;
            MyProperty prop;
            LineConnector link;
            MyLink myLink;
            foreach (Node node in group.Nodes)
            {
                subModel = node.Tag as ModelInfo;
                if (subModel == null)
                    continue;
                if (node is LineConnector) //子节点是链路时，写入Link标签---也许应该从subModel的type中判断
                {
                    link = node as LineConnector;
                    myLink = new MyLink();
                    //myLink.name = link.Name;
                    //myLink.src = ((Node)link.FromNode).Name;
                    //myLink.dest = ((Node)link.ToNode).Name;
                    //groupModel.links.Add(myLink);
                    //属性由link的modelInfo的property获取，不过既然不让修改（链路模型可固定化设计）也没必要添加
                }
                else
                {
                    prop = new MyProperty(); //其他子节点都视为属性，其类型为model
                    prop.name = node.Name;
                    prop.type = GDataType.Model_En;
                    prop.refModel = subModel.ModelID;
                    prop.display = prop.name;
                    prop.isDisplay = "y"; //这几个属性的设置参数 ModelInfo.ReadPropertyAttr()
                    prop.pos = node.PinPoint; //暂时不存偏移量，这个时候Group还没设置pinpoint坐标的
                    prop.property = subModel;
                    groupModel.properties[prop.name] = prop;
                }
            }
        }

        private void miDesignModel_Click(object sender, EventArgs e)
        {
            if (ActiveDiagram.Controller.SelectionList.Count == 0)
                return;
            if (ActiveDiagram.Controller.SelectionList.Count > 1)
            {
                MessageBox.Show("请选择单个模型，或者将多个模型组合为Group后选择");
                return;
            }
            Node node = ActiveDiagram.Controller.SelectionList[0];
            if (node is Group) //组合模型
            {
                Group group = node as Group;
                if (!CheckGroupModel(node))
                {
                    MessageBox.Show("编辑组合模型需要选择多个节点组成的Group");
                    return;
                }
                else
                    BuildGroupModel(group);
            }
            DesignModel = node.Tag as ModelInfo;
            if (DesignModel == null)//单个模型
            {
                MessageBox.Show("该节点不支持模型编辑");
                return;
            }

            mainForm.m_gridBox.BeginDesignModel(this);
        }

        public void UpdateDesignModelNode()
        {
            Node node = this.DesignModel.DiagramNode;
            if (node == null)
            {
                System.Diagnostics.Debug.WriteLine("******DiagramNode为空却要UpdateDesignModelNode");
                return;
            }
            this.DesignModel.pinpoint = node.PinPoint;
            node.Name = DesignModel.ModelName;
            NodeHelper.SetNodeLabel0(node, node.Name);
            if (node is Group) //是组合模型
            {
                //之前已经build过组合模型，所以这里只是更新一下子模型的坐标
                Group group = node as Group;
                foreach (Node n in group.Nodes)
                {
                    if (n is LineConnector)
                        continue;
                    if (DesignModel.properties.ContainsKey(n.Name))
                    {
                        DesignModel.properties[n.Name].pos = new PointF(n.PinPoint.X - node.PinPoint.X,
                            n.PinPoint.Y - node.PinPoint.Y);
                    }
                }
            }
        }
        private bool CheckGroupModel(Node node)
        {
            Group group = node as Group;
            ModelInfo model = null;
            if (group != null)
            {
                NodeCollection valid = new NodeCollection();
                foreach (Node n in group.Nodes)
                {
                    model = n.Tag as ModelInfo;
                    if (model != null)
                        valid.Add(n);
                }
                if (valid.Count > 0)
                    return true;
            }

            return false;
        }
    }

    public enum ClipboardFormats : uint
    {
        CF_TEXT = 1,
        CF_BITMAP = 2,
        CF_METAFILEPICT = 3,
        CF_SYLK = 4,
        CF_DIF = 5,
        CF_TIFF = 6,
        CF_OEMTEXT = 7,
        CF_DIB = 8,
        CF_PALETTE = 9,
        CF_PENDATA = 10,
        CF_RIFF = 11,
        CF_WAVE = 12,
        CF_UNICODETEXT = 13,
        CF_ENHMETAFILE = 14
    }
}
