using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Diagram.Controls;
using NetworkDesigner.Utils.DiagramUtil;
using NetworkDesigner.UI.Document;
using NetworkDesigner.Beans.DataStruct;
using NetworkDesigner.Beans.Model;
using NetworkDesigner.Service.Model;

namespace NetworkDesigner.UI.ToolWindow
{
    public partial class FrmToolbox : FrmBase
    {
        private Dictionary<string, NodeDelete> m_paletteDeleteNode;

        private class NodeDelete
        {
            public int index;
            public Node node;
            public SimModel model;
            public NodeDelete(int i,Node n,SimModel m)
            {
                index = i;
                node = n;
                model = m;
            }
        }

        public FrmToolbox(FrmMain _frmMain)
        {
            InitializeComponent();

            m_paletteDeleteNode = new Dictionary<string, NodeDelete>();
            this.mainForm = _frmMain;
        }
        
        public PaletteGroupBar m_PaletteGroup
        {
            get { return this.paletteGroup; }
        }

        private void InitPaletteGroupViewEvent(PaletteGroupView view)
        {
            view.MouseDown += paletteGroupView_MouseDown;
            view.MouseUp += paletteGroupView_MouseUp;
        }

        public void LoadPalettes(List<string> palettes)
        {
            foreach (string filePath in palettes)
            {
                LoadPaletteFile(filePath);
            }
            //todo:解决某些模型延迟加载的问题

        }

        public void LoadPaletteFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                NetworkDesigner.Utils.Common.LogHelper.LogInfo("未找到模型文件：" + filePath);
                return;
            }
            if (this.FindPaletteGroupView(filePath) != null)
            {
                MessageBox.Show("文件已加载：" + filePath);
                return;
            }

            DocInfo docInfo = new DocInfo(filePath);
            foreach (GroupBarItem barItem in paletteGroup.GroupBarItems)
            {
                if (barItem.Text.Equals(docInfo.fileNameNoExtension))
                {
                    MessageBox.Show("已加载同名的模型库：" + docInfo.fileNameNoExtension);
                    return;
                }
            }
            SymbolPalette palette = ModelInfoHelper.LoadPaletteFromFile(filePath);
            this.paletteGroup.AddPalette(palette);
            var item = paletteGroup.GroupBarItems[paletteGroup.GroupBarItems.Count - 1];
            item.Text = docInfo.fileNameNoExtension;
            PaletteGroupView view = item.Client as PaletteGroupView;
            view.Font = new System.Drawing.Font("宋体", 9);
            view.Tag = docInfo;//DocInfo没有加 Serializable 特性标记，因此不会序列化，也不需要序列化
            Utils.DiagramUtil.PaletteHelper.SetPaletteView(view);
            InitPaletteGroupViewEvent(view);
            m_paletteDeleteNode.Add(docInfo.filePath, null);
        }

        /// <summary>
        /// 保存所有未保存的工具箱面板，并且是否提示
        /// </summary>
        public void SaveAllPalettes(bool needAsk = true)
        {
            foreach (GroupBarItem item in paletteGroup.GroupBarItems)
            {
                PaletteGroupView view = item.Client as PaletteGroupView;
                if (view == null)
                    continue;
                DocInfo docInfo = (DocInfo)view.Tag;
                if (docInfo.isEdit)
                {
                    if (needAsk)
                    {
                        if (MessageBox.Show("工具面板 " + docInfo.fileName + " 已修改，是否保存\r\n", "提示",
                            MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                            continue;
                    }
                    try
                    {
                        ModelInfoHelper.SavePaletteToFile(view.Palette, docInfo.filePath);
                        docInfo.isEdit = false;
                    }
                    catch (Exception)
                    {
                        mainForm.WriteLine("面板模型保存失败,请检查相关文件是否占用：" + docInfo.filePath, Color.Red);
                    }
                }
            }
        }
        /// <summary>
        /// 保存对应路径的工具箱面板到文件
        /// </summary>
        public void SaveSinglePalette(string filePath)
        {
            PaletteGroupView view = FindPaletteGroupView(filePath);
            if (view == null)
            {
                mainForm.WriteLine("面板模型保存失败,找不到文件对应的面板：" + filePath, Color.Red);
                return;
            }
            DocInfo docInfo = (DocInfo)view.Tag;
            if (docInfo.isEdit)
            {
                try
                {
                    ModelInfoHelper.SavePaletteToFile(view.Palette, docInfo.filePath);
                    docInfo.isEdit = false;
                }
                catch (Exception)
                {
                    mainForm.WriteLine("面板模型保存失败,请检查文件是否占用：" + docInfo.filePath, Color.Red);
                }
                
            }
        }

        /// <summary>
        /// 将diagram中全部模型的副本添加到面板中,若没有该面板则新建，若面板已有模型会先清空
        /// </summary>
        public void AddToPalette(Diagram diagram,string filePath)
        {
            //bool needAdd = diagram.Model.Modified;
            //PaletteGroupView view = FindPaletteGroupView(filePath);
            //if (view == null)//没建立过palette，则无论model是否修改都要保存；已建立过则看model是否修改
            //{
            //    needAdd = true;
            //    paletteGroup.AddPalette(System.IO.Path.GetFileNameWithoutExtension(filePath));
            //    GroupBarItem itemAdd = paletteGroup.GroupBarItems[paletteGroup.GroupBarItems.Count - 1];
            //    view = itemAdd.Client as PaletteGroupView;
            //    DocInfo info = new DocInfo(filePath);
            //    info.isEdit = true; //新增
            //    view.Tag = info;
            //    PaletteHelper.SetPaletteView(view);
            //    InitPaletteGroupViewEvent(view);
            //    m_paletteDeleteNode.Add(info.filePath, null);
            //}
            //if (needAdd)
            //{
            //    //查看源码可以发现palette.Nodes获得的是新的Collection，但是却装的原来node的引用，因此注意会影响原来的值
            //    //另外实测发现palette.RemoveChild(node)也并没有删掉对应的paletteGroupViewItem，仍然显示在GroupView中
            //    //因此删除Node的工作暂时通过新生成一个Palette并设置到 PaletteGroupView.Palette 属性来实现
            //    DocInfo info = (DocInfo)view.Tag;
            //    SymbolPalette symbolPalette = new SymbolPalette();
            //    symbolPalette.Name = Path.GetFileNameWithoutExtension(info.fileName);
            //    List<ModelInfo> models = new List<ModelInfo>();
            //    ModelInfoHelper.patModels[info.fileNameNoExtension] = models;
            //    Node n=null;
            //    foreach (Node node in diagram.Model.Nodes)
            //    {
            //        if (node.Tag == null)
            //            continue;
            //        ModelInfo curModel = (ModelInfo)node.Tag;
            //        if (curModel.modType.IsDefaultType())
            //        {
            //            mainForm.WriteLine("默认类型的模型节点不保存：" + node.Name);
            //            continue;
            //        }
            //        curModel.ModelID = info.fileNameNoExtension + "." + curModel.ModelName;
            //        ModelInfo model = curModel.DeepCopy();
            //        if (node is Group) //组合模型
            //        {
            //            //使用bitmapNode替换掉group方便后续使用，组合模型的信息由modelInfo表达
            //            string file = "";
            //            if (File.Exists(model.ModelImage))
            //                file = model.ModelImage;
            //            else
            //                file = ModelInfoHelper.GetPaletteNodeImage(filePath, "default.png");
            //            n = new BitmapNode(new Bitmap(file));
            //            n.Name = node.Name;
            //            n.EditStyle.HidePinPoint = true;
            //            n.LineStyle.LineColor = Color.Transparent;//隐藏黑色边框
            //        }
            //        else
            //        {
            //            n = (Node)node.Clone();
            //            NodeHelper.ClearNodeLabel(n);//有label添加到面板时图片显示不好
            //        }
            //        n.Tag = model;
            //        model.DiagramNode = n; //仅在palette内部有效
            //        models.Add(model);
            //        //todo:更新到模型库
            //        PaletteHelper.SetNodeInPalette(n);
            //        symbolPalette.AppendChild(n);
            //        mainForm.WriteLine("成功添加节点：" + n.Name + " 模型：" + curModel.ModelID, Color.Blue);
            //    }
            //    view.Palette = symbolPalette;
            //    info.isEdit = true;
            //}
        }
        
        /// <summary>
        /// 将NodeCollection中模型的副本添加到面板中,若没有该面板则新建，已添加的模型会先删除再添加
        /// </summary>
        public void AddToPalette(NodeCollection nodes, string filePath)
        {
            //PaletteGroupView view = FindPaletteGroupView(filePath);
            //DocInfo info = null;
            //if (view == null)
            //{
            //    paletteGroup.AddPalette(System.IO.Path.GetFileNameWithoutExtension(filePath));
            //    GroupBarItem itemAdd = paletteGroup.GroupBarItems[paletteGroup.GroupBarItems.Count - 1];
            //    view = itemAdd.Client as PaletteGroupView;
            //    info = new DocInfo(filePath);
            //    info.isEdit = true;
            //    view.Tag = info;
            //    PaletteHelper.SetPaletteView(view);
            //    InitPaletteGroupViewEvent(view);
            //    m_paletteDeleteNode.Add(info.filePath, null);
            //}
            //else
            //{
            //    info = view.Tag as DocInfo;
            //}
            //bool isAdd = false;
            //int nodeExist = -1;
            //Node n = null;
            //string modelID = "";
            //foreach (Node node in nodes)
            //{
            //    if (node.Tag == null)
            //        continue;
            //    modelID = node.Tag as string;
            //    if (!ModelInfoHelper.allModels.ContainsKey(modelID))
            //        continue;
            //    SimModel curModel = ModelInfoHelper.allModels[node.Tag as string];
            //    if (curModel.modType.IsDefaultType())
            //    {
            //        mainForm.WriteLine("默认类型的模型节点不保存：" + node.Name);
            //        continue;
            //    }
            //    nodeExist = PaletteHelper.RemoveNodeByName(view.Palette, node.Name);
            //    var models = ModelInfoHelper.patModels[info.fileNameNoExtension];

            //    if (nodeExist != -1) //面板中已存在同名模型
            //    {
            //        models.RemoveAll(m => m.modelName.Equals(node.Name));
            //    }
            //    curModel.ModelID = info.fileNameNoExtension + "." + curModel.ModelName;
            //    SimModel model = curModel.DeepCopy();
            //    if (node is Group) //组合模型
            //    {
            //        //使用bitmapNode替换掉group方便后续使用，组合模型的信息由modelInfo表达
            //        string file = "";
            //        if(File.Exists(model.ModelImage))
            //            file = model.ModelImage;
            //        else
            //            file = ModelInfoHelper.GetPaletteNodeImage(filePath,"default.png");
            //        n = new BitmapNode(new Bitmap(file));
            //        n.Name = node.Name;
            //        n.EditStyle.HidePinPoint = true;
            //        n.LineStyle.LineColor = Color.Transparent;//隐藏黑色边框
            //    }
            //    else
            //    {
            //        n = (Node)node.Clone();
            //        NodeHelper.ClearNodeLabel(n);//有label添加到面板时图片显示不好
            //    }
            //    n.Tag = model;
            //    model.DiagramNode = n; //仅在palette内部有效
            //    models.Add(model);
            //    //todo:更新到模型库
            //    PaletteHelper.SetNodeInPalette(n);
            //    view.Palette.AppendChild(n);
            //    if(nodeExist != -1)
            //        mainForm.WriteLine("成功更新节点：" + n.Name + " 模型：" + curModel.ModelID, Color.Blue);
            //    else
            //        mainForm.WriteLine("成功添加节点：" + n.Name + " 模型：" + curModel.ModelID, Color.Blue);
            //    isAdd = true;
            //    info.isEdit = true;
            //}
            //if (isAdd)
            //    view.BringItemIntoView(view.GroupViewItems.Count - 1);
        }

        /// <summary>
        /// 工具箱中各面板对应的文件路径，应该是没有filePath为空的
        /// </summary>
        /// <returns></returns>
        public List<string> GetPalettesPath()
        {
            List<string> files = new List<string>(this.paletteGroup.GroupBarItems.Count);
            foreach (GroupBarItem item in this.paletteGroup.GroupBarItems)
            {
                if (item.Client is PaletteGroupView)
                {
                    PaletteGroupView view = item.Client as PaletteGroupView;
                    if (view.Tag is DocInfo)
                    {
                        DocInfo docInfo = (DocInfo)view.Tag;
                        if(docInfo.T == DocInfo.DType.PAT)
                            files.Add(docInfo.filePath);
                    }
                }
            }
            return files;
        }

        /// <summary>
        /// 返回工具箱中所有面板的Palette引用
        /// </summary>
        /// <returns></returns>
        public List<SymbolPalette> GetPalettes()
        {
            var palettes = new List<SymbolPalette>(paletteGroup.GroupBarItems.Count);
            foreach (GroupBarItem item in this.paletteGroup.GroupBarItems)
            {
                if (item.Client is PaletteGroupView)
                {
                    PaletteGroupView view = item.Client as PaletteGroupView;
                    palettes.Add(view.Palette);
                }
            }
            return palettes;
        }
        /// <summary>
        /// 根据面板名称设置该面板已修改
        /// </summary>
        /// <param name="paletteName"></param>
        public void SetPaletteModifyByText(string paletteName)
        {
            foreach (GroupBarItem item in this.paletteGroup.GroupBarItems)
            {
                if (item.Text.Equals(paletteName) && item.Client is PaletteGroupView)
                {
                    PaletteGroupView view = item.Client as PaletteGroupView;
                    if (view.Tag is DocInfo)
                    {
                        DocInfo docInfo = (DocInfo)view.Tag;
                        docInfo.isEdit = true;
                    }
                }
            }
        }
        /// <summary>
        /// 根据保存文件的路径查找GroupBarItem关联的PaletteGroupView，找不到时返回null
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public PaletteGroupView FindPaletteGroupView(string filePath)
        {
            foreach (GroupBarItem item in this.paletteGroup.GroupBarItems)
            {
                if (item.Client is PaletteGroupView)
                {
                    PaletteGroupView view = item.Client as PaletteGroupView;
                    if (view.Tag is DocInfo)
                    {
                        DocInfo docInfo = (DocInfo)view.Tag;
                        if (docInfo.filePath.Equals(filePath))
                            return view;
                    }
                }
            }
            return null;
        }

        public Node FindNodeByModelID(string modelID)
        {
            string model = "";
            foreach (GroupBarItem item in this.paletteGroup.GroupBarItems)
            {
                if (item.Client is PaletteGroupView)
                {
                    PaletteGroupView view = item.Client as PaletteGroupView;
                    foreach(Node node in view.Palette.Nodes)
                    {
                        model = node.Tag as string;
                        if (model != null && model.Equals(modelID))
                            return node;
                    }
                }
            }
            return null;
        }

        public void SetPaletteGroupViewDiagram(Diagram diagram)
        {
            foreach (GroupBarItem item in paletteGroup.GroupBarItems)
            {
                if (item.Client is PaletteGroupView)
                {
                    PaletteGroupView view = item.Client as PaletteGroupView;
                    view.Diagram = diagram;
                }
            }
        }

        public PaletteGroupBar GetPaletteGroupBar()
        {
            return this.paletteGroup;
        }
                
        private void miDeleteModel_Click(object sender, EventArgs e)
        {
            GroupBarItem item = paletteGroup.GroupBarItems[paletteGroup.SelectedItem];
            PaletteGroupView view = item.Client as PaletteGroupView;
            Node node = view.SelectedNode;
            if (view != null)
            {
                if (node != null)
                {
                    string id = view.SelectedNode.Tag as string;
                    DocInfo docInfo = (DocInfo)view.Tag;
                    if (ModelInfoHelper.allModels.ContainsKey(id))
                    {
                        m_paletteDeleteNode[docInfo.filePath] = new NodeDelete(view.Palette.Nodes.IndexOf(node),
                            node, ModelInfoHelper.allModels[id]);
                    }
                    List<SimModel> models = ModelInfoHelper.patModels[docInfo.fileNameNoExtension];
                    
                    foreach (var m in models)
                    {
                        if (m.modelID.Equals(id))
                        {
                            models.Remove(m);
                            ModelInfoHelper.UpdateChoices();
                            break;
                        }
                    }
                    view.Palette.RemoveChild(view.SelectedNode);
                    docInfo.isEdit = true;
                    UpdateActiveModelInfo();
                }
            }
        }

        private void miCancelDelete_Click(object sender, EventArgs e)
        {
            GroupBarItem item = paletteGroup.GroupBarItems[paletteGroup.SelectedItem];
            PaletteGroupView view = item.Client as PaletteGroupView;
            if (view != null)
            {
                DocInfo docInfo = (DocInfo)view.Tag;
                if (!m_paletteDeleteNode.ContainsKey(docInfo.filePath))
                {
                    NetworkDesigner.Utils.Common.LogHelper.LogInfo("不存在恢复项或已删除");
                    return;
                }
                NodeDelete nodeDelete = m_paletteDeleteNode[docInfo.filePath];
                m_paletteDeleteNode.Remove(docInfo.filePath);
                List<SimModel> models = ModelInfoHelper.patModels[docInfo.fileNameNoExtension];
                models.Add(nodeDelete.model);
                if(nodeDelete.index >=0 && nodeDelete.index <= view.Palette.Nodes.Count)
                    view.Palette.InsertChild(nodeDelete.node, nodeDelete.index);
                else
                    view.Palette.AppendChild(nodeDelete.node);
                UpdateActiveModelInfo();
            }
        }

        private void cmiLoadPalette_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "(*.pat)|*.pat";
                dialog.InitialDirectory = NetworkDesigner.Beans.Common.AppSetting.DefaultModelDirPath;
                if (DialogResult.OK == dialog.ShowDialog())
                {
                    LoadPaletteFile(dialog.FileName);
                }
            }
            UpdateActiveModelInfo();
        }

        private void cmiUnloadPalette_Click(object sender, EventArgs e)
        {
            GroupBarItem item = this.paletteGroup.GroupBarItems[paletteGroup.SelectedItem];
            PaletteGroupView view = item.Client as PaletteGroupView;
            if (view != null)
            {
                DocInfo docInfo = (DocInfo)view.Tag;
                m_paletteDeleteNode.Remove(docInfo.filePath);
                this.paletteGroup.GroupBarItems.RemoveAt(paletteGroup.SelectedItem);
                if(ModelInfoHelper.patModels.ContainsKey(docInfo.fileNameNoExtension))
                    ModelInfoHelper.patModels.Remove(docInfo.fileNameNoExtension);
            }
            UpdateActiveModelInfo();
        }

        private void miEditPalette_Click(object sender, EventArgs e)
        {
            GroupBarItem item = this.paletteGroup.GroupBarItems[paletteGroup.SelectedItem];
            PaletteGroupView view = item.Client as PaletteGroupView;
            DocInfo docInfo = (DocInfo)view.Tag;
            bool alreadyOpen;
            FrmDocBase doc = FactoryDocument.CreateDocument(mainForm, docInfo.filePath, out alreadyOpen);
            if (alreadyOpen)
                doc.Select();
            else if (doc != null)
            {
                FrmSymbol docSymbol = doc as FrmSymbol;
                if (docSymbol != null)
                {
                    docSymbol.Show(mainForm.MainDock);//只有先show出来才能获得正确的diagram.view.rectangle视窗属性值
                    docSymbol.LoadPalette(view.Palette);
                }
                else 
                {
                    FactoryDocument.RemoveDocument(doc);
                    mainForm.WriteLine("面板打开失败", Color.Red);
                }
            }
        }

        private void miSavePalette_Click(object sender, EventArgs e)
        {
            GroupBarItem item = this.paletteGroup.GroupBarItems[paletteGroup.SelectedItem];
            PaletteGroupView view = item.Client as PaletteGroupView;
            if (view != null)
            {
                DocInfo docInfo = (DocInfo)view.Tag;
                if (docInfo.isEdit)
                {
                    ModelInfoHelper.SavePaletteToFile(view.Palette, docInfo.filePath);
                    docInfo.isEdit = false;
                }
            }
        }

        private void miShowProperty_Click(object sender, EventArgs e)
        {
            GroupBarItem item = paletteGroup.GroupBarItems[paletteGroup.SelectedItem];
            PaletteGroupView view = item.Client as PaletteGroupView;
            this.mainForm.MainProperty.SelectedObject = view;
        }

        private void paletteGroupView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.cmsEditor.Show(Cursor.Position.X, Cursor.Position.Y);
            }
            //UpdateActiveModelInfo();//鼠标未弹起，有些动作还未完成，这里处理有问题
        }
        private void paletteGroupView_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateActiveModelInfo();
        }
        private void UpdateActiveModelInfo()
        {
            GroupBarItem item = paletteGroup.GroupBarItems[paletteGroup.SelectedItem];
            PaletteGroupView view = item.Client as PaletteGroupView;
            var node = view.SelectedNode;
            if (node != null)
            {
                string modelID = node.Tag as string;
                ActiveModelInfo.paletteNode = node; //待测试删除面板模型时会不会跳到下一个激活的模型
                switch (ModelInfoHelper.GetModelType(modelID))
                {
                    case SimModelType.节点:
                        ActiveModelInfo.ActiveType = SimModelType.节点;
                        ActiveModelInfo.ActiveNode = modelID;
                        mainForm.激活工具 = "节点";
                        break;
                    case SimModelType.链路:
                        ActiveModelInfo.ActiveType = SimModelType.链路;
                        ActiveModelInfo.ActiveLink = modelID;
                        mainForm.激活工具 = "链路";
                        break;
                    case SimModelType.业务:
                        ActiveModelInfo.ActiveType = SimModelType.业务;
                        ActiveModelInfo.ActiveTraffic = modelID;
                        mainForm.激活工具 = "节点";
                        break;
                    //case SimModelType.结果:
                    //    ActiveModelInfo.ActiveType = SimModelType.结果;
                    //    ActiveModelInfo.ActiveResult = modelID;
                    //    break;
                    case SimModelType.拓扑:
                        ActiveModelInfo.ActiveType = SimModelType.拓扑;
                        ActiveModelInfo.ActiveTopo = modelID;
                        mainForm.激活工具 = "拓扑";
                        break;
                    default:
                        ActiveModelInfo.ActiveType = SimModelType.未知;
                        mainForm.激活工具 = "选择";
                        break;
                }
                mainForm.TryActiveNodeTool();
            }
        }
        private void miSaveAllPalette_Click(object sender, EventArgs e)
        {
            SaveAllPalettes(true);
        }

    }
    public class ActiveModelInfo
    {
        public static SimModelType ActiveType = SimModelType.未知;
        public static string ActiveNode = "PIM.实体.链路.host";
        public static string ActiveLink = "PIM.实体.链路.wiredLink";
        public static string ActiveTraffic = "PIM.实体.业务.CBR";
        //public static string ActiveResult = "PIM.实体.仿真结果";
        public static string ActiveTopo = "格型";
        public static Node paletteNode = null;
    }
}
