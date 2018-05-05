using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using WeifenLuo.WinFormsUI.Docking;
using NetworkDesigner.Beans.Common;
using NetworkDesigner.UI.ToolWindow;
using NetworkDesigner.UI.Document;
using NetworkDesigner.UI.Dialog;
using NetworkDesigner.Utils.FileUtil;
using NetworkDesigner.Utils.DiagramUtil;

using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Diagram.Controls;
using SWD = Syncfusion.Windows.Forms.Diagram;
using SDC = Syncfusion.Windows.Forms.Diagram.Controls;
using NetworkDesigner.Service.Model;

namespace NetworkDesigner.UI
{
    public partial class FrmMain : Form
    {
        private DiaSplashScreen m_splashScreen;
        private FrmSolution m_solutionBox;
        public FrmProperty m_propertyBox;
        public FrmModelAttr m_modelBox;
        public FrmToolbox m_toolBox;
        private FrmOutput m_outputBox;
        public FrmTreeGrid m_gridBox;
        private DeserializeDockContent m_deserializeDock;
        public bool m_isExiting = false;
        private string tslActionDefault;
        private string tslPosInfoDefault;

        /// <summary>
        /// 工具栏按钮对应的Diagram-Tool，只包含那些想通过ActiveDiagram.Controller.ActivateTool的项
        /// </summary>
        public Dictionary<string, string> m_BarName2DiagramTool;
        /// <summary>
        /// 工具栏按钮名称与按钮，包含所有感兴趣的项
        /// </summary>
        public Dictionary<string, ToolStripButton> m_BarItems;
        public bool TextNodeToolChecked
        {
            get 
            {
                return this.tsbTextNode.Checked;
            }
        }
        /// <summary>
        /// 当前默认项目的目录，默认是“我的文档”
        /// </summary>
        public string GetProjectPath()
        {
            string path = this.m_solutionBox.GetDefaultProject();
            if(path.Equals(""))
                return AppSetting.MyDocumentPath;
            return path;
        }

        public DockPanel MainDock
        {
            get { return this.dockPanel; }
        }

        public PaletteGroupBar MainGroupBar
        {
            get { return this.m_toolBox.m_PaletteGroup; }
        }

        public PropertyGrid MainProperty
        {
            get { return this.m_propertyBox.mPropertyGrid; }
        }

        public FrmMain()
        {
            InitializeComponent();

            tslActionDefault = this.tslActionInfo.Text;
            tslPosInfoDefault = this.tslPosInfo.Text;
            //靠右显示状态栏
            this.ssMain.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            tslActionInfo.Alignment = ToolStripItemAlignment.Right;
            tslPosInfo.Alignment = ToolStripItemAlignment.Right;

            this.dockPanel.Theme = this.vS2012LightTheme1;
            m_deserializeDock = new DeserializeDockContent(GetContentFromPersistString);
            InitBarItemActiveTools();
            InitBarItemClickEventHandler();
            
            ShowSplashScreen();
        }

        /// <summary>
        /// 显示程序正在加载页面
        /// </summary>
        public void ShowSplashScreen()
        {
            m_splashScreen = new DiaSplashScreen();
            //m_splashScreen.Show();
        }
        /// <summary>
        /// 关闭程序正在加载页面
        /// </summary>
        public void CloseSplashScreen()
        {
            if (m_splashScreen != null && !m_splashScreen.IsDisposed)
                m_splashScreen.Close();
            m_splashScreen = null;
        }

        private void CreateStandardWindows()
        {
            m_solutionBox = new FrmSolution(this);
            m_solutionBox.Text = "设计方案";
            m_solutionBox.TabText = m_solutionBox.Text;
            this.miSolutionBox.Click += (sender, e) =>
            {
                if (!m_solutionBox.Visible)
                    m_solutionBox.Show();
                else
                    m_solutionBox.Select();
            };

            m_propertyBox = new FrmProperty(this);
            m_propertyBox.Text = "查看属性";
            m_propertyBox.TabText = m_propertyBox.Text;
            this.miPropertyBox.Click += (sender, e) =>
            {
                if (!m_propertyBox.Visible)
                    m_propertyBox.Show();
                else
                    m_propertyBox.Select();
            };

            m_modelBox = new FrmModelAttr(this);
            m_modelBox.Text = "设置参数";
            m_modelBox.TabText = m_modelBox.Text;
            this.miPropertyBox.Click += (sender, e) =>
            {
                if (!m_modelBox.Visible)
                    m_modelBox.Show();
                else
                    m_modelBox.Select();
            };

            m_toolBox = new FrmToolbox(this);
            m_toolBox.Text = "工具箱";
            m_toolBox.TabText = m_toolBox.Text;
            this.miToolBox.Click += (sender, e) =>
            {
                if (!m_toolBox.Visible)
                    m_toolBox.Show();
                else
                    m_toolBox.Select();
            };

            m_outputBox = new FrmOutput(this);
            m_outputBox.Text = "输出";
            m_outputBox.TabText = m_outputBox.Text;
            this.miOutputBox.Click += (sender, e) =>
            {
                if (!m_outputBox.Visible)
                    m_outputBox.Show();
                else
                    m_outputBox.Select();
            };

            m_gridBox = new FrmTreeGrid(this);
            m_gridBox.Text = "属性编辑";
            m_gridBox.TabText = m_gridBox.Text;
            this.miTreeGridBox.Click += (sender, e) =>
            {
                if (!m_gridBox.Visible)
                    m_gridBox.Show();
                else
                    m_gridBox.Select();
            };
        }
        private void DisposeStandardWindows()
        {
            if (m_solutionBox != null)
            {
                m_solutionBox.Dispose();
                m_solutionBox = null;
            }
            if (m_propertyBox != null)
            {
                m_propertyBox.Dispose();
                m_propertyBox = null;
            }
            if (m_modelBox != null)
            {
                m_modelBox.Dispose();
                m_modelBox = null;
            }
            if (m_toolBox != null)
            {
                m_toolBox.Dispose();
                m_toolBox = null;
            }
            if (m_outputBox != null)
            {
                m_outputBox.Dispose();
                m_outputBox = null;
            }
            if (m_gridBox != null)
            {
                m_gridBox.Dispose();
                m_gridBox = null;
            }
        }
        private void CloseAllContents()
        {
            // we don't want to create another instance of tool window, set DockPanel to null
            m_solutionBox.DockPanel = null;
            m_propertyBox.DockPanel = null;
            m_modelBox.DockPanel = null;
            m_toolBox.DockPanel = null;
            m_outputBox.DockPanel = null;
            m_gridBox.DockPanel = null;

            // Close all other document windows
            CloseAllDocuments();
        }
        public void CloseAllDocuments()
        {
            foreach (FrmDocBase doc in FactoryDocument.allDocs)
                doc.Close();
        }

        #region 窗口布局
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(FrmSolution).ToString())
                return m_solutionBox;
            else if (persistString == typeof(FrmProperty).ToString())
                return m_propertyBox;
            else if (persistString == typeof(FrmModelAttr).ToString())
                return m_modelBox;
            else if (persistString == typeof(FrmToolbox).ToString())
                return m_toolBox;
            else if (persistString == typeof(FrmOutput).ToString())
                return m_outputBox;
            else if (persistString == typeof(FrmTreeGrid).ToString())
                return m_gridBox;
            //为简化实现，这里暂不对Document的布局进行反序列化，因此在关闭程序时也不对Document的布局序列化
            return null;
        }
        /// <summary>
        /// 若DockPanel.config存在则从中加载标准窗口布局，否则使用代码布局，不含文档窗口
        /// </summary>
        public void LayoutToolWindow()
        {
            string configFile = AppSetting.DockPanelFile;
            if (File.Exists(configFile))
            {
                CloseAllContents();
                dockPanel.LoadFromXml(configFile, m_deserializeDock);
            }
            else
            {
                LayoutToolWindowByCode();
            }
        }
        public void LayoutToolWindowByCode()
        {
            //this.SuspendLayout();
            m_solutionBox.Show(this.dockPanel, DockState.DockLeft);
            m_toolBox.Show(this.dockPanel, DockState.DockLeft);
            m_solutionBox.Show();//调整显示顺序
            m_propertyBox.Show(this.dockPanel, DockState.DockRight);
            m_modelBox.Show(this.dockPanel, DockState.DockRight);
            m_outputBox.Show(this.dockPanel, DockState.DockBottom);
            m_gridBox.Show(this.dockPanel, DockState.DockBottom);
            m_outputBox.Show();
            //this.ResumeLayout();
        }
        #endregion
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            //加载程序配置文件UserSetting.Xml
            if (!AppSetting.LoadUserSetting())
                this.Dispose();//Dispose不会进入form_closing事件
            
            //创建面板
            CreateStandardWindows();
            LayoutToolWindow();
            this.WindowState = FormWindowState.Maximized;

            //填充菜单
            //最近打开文件菜单
            AppSetting.LatestRecentProjects = new List<string>();
            foreach (var path in AppSetting.RecentProjects)
                this.AddRecentProjectToMenu(path);

            //读取模型库，填充工具箱面板
            this.m_toolBox.LoadPalettes(AppSetting.GetSymbolPalettes());
            

            CloseSplashScreen();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定退出程序？", "提示",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
            else 
            {
                m_isExiting = true;
                //倒序安全地删除List元素
                for (int i = FactoryDocument.allDocs.Count - 1; i >= 0; i--)
                {
                    var form = FactoryDocument.allDocs[i];
                    FactoryDocument.RemoveDocument(form);
                    form.Close();//在点击父窗体关闭按钮时，子窗体已经收到过closing事件，因此不再提示是否关闭，直接销毁
                }
                try
                {
                    this.m_toolBox.SaveAllPalettes(true);
                    if (this.miSaveLayout.Checked)
                        dockPanel.SaveAsXml(AppSetting.DockPanelFile);
                    AppSetting.SaveUserSetting();
                    DisposeStandardWindows();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("程序退出异常："+ex);
                }
            }
        }

        #region 生成的事件响应函数

        private void miNewProject_Click(object sender, EventArgs e)
        {
            DiaNewProject dp = new DiaNewProject();
            dp.StartPosition = FormStartPosition.CenterParent;
            dp.ShowDialog();
            if (dp.IsCancelled)
            {
                dp.Dispose();
                return;
            }
            string project = dp.ProjectPath + "\\" + dp.ProjectName;
            if (Directory.Exists(project))
            {
                NetworkDesigner.Utils.Common.LogHelper.LogInfo(
                    "新建项目文件夹已存在：" + project);
                return;
            }
            Directory.CreateDirectory(project);
            this.m_solutionBox.LoadProject(project);
        }
        private void miOpenProject_Click(object sender, System.EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "请选择设计项目的根目录";
                fbd.SelectedPath = AppSetting.MyDocumentPath;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.m_solutionBox.LoadProject(fbd.SelectedPath);
                }
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void miSaveLayout_Click(object sender, EventArgs e)
        {
            this.miSaveLayout.Checked = !this.miSaveLayout.Checked;
        }

        private void miNewTextFile_Click(object sender, EventArgs e)
        {
            FrmDocBase doc = FactoryDocument.CreateBlankTextDocument(this,this.GetProjectPath());
            
            if (doc != null)
            {
                doc.LoadFile(doc.docInfo);
                doc.Show(this.MainDock);
            }
        }

        private void miNewDiagram_Click(object sender, EventArgs e)
        {
            FrmDocBase doc = FactoryDocument.CreateBlankDiagramDocument(this, this.GetProjectPath());

            if (doc != null)
            {
                this.m_toolBox.Show();
                doc.LoadFile(doc.docInfo);
                doc.Show(this.MainDock);
            }
        }

        private void miNewModel_Click(object sender, EventArgs e)
        {
            FrmDocBase doc = FactoryDocument.CreateBlankSymBolDocument(this, this.GetProjectPath());

            if (doc != null)
            {
                doc.LoadFile(doc.docInfo);
                this.m_toolBox.Show();
                doc.Show(this.MainDock);
            }
        }

        private void miOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "(*.*)|*.*";
                dialog.FilterIndex = 0;
                if (DialogResult.OK == dialog.ShowDialog())
                {
                    DocInfo docInfo = new DocInfo(dialog.FileName);
                    if (docInfo.T == DocInfo.DType.PAT)
                    {
                        var view = this.m_toolBox.FindPaletteGroupView(docInfo.filePath);
                        if (view == null)
                        {
                            this.m_toolBox.LoadPaletteFile(docInfo.filePath);
                        }
                        return;
                    }

                    bool alreadyOpen;
                    FrmDocBase doc = FactoryDocument.CreateDocument(this, dialog.FileName, out alreadyOpen);
                    if (alreadyOpen)
                        doc.Select();
                    else if (doc != null)
                    {
                        doc.Show(this.MainDock);
                        doc.LoadFile(doc.docInfo);
                    }
                }
            }
        }

        private void tsbSaveFile_Click(object sender, EventArgs e)
        {
            //注意当Dockpanel的DocumentStyle要设置为DockingMdi时才可以

            //foreach (IDockContent document in dockPanel.DocumentsToArray())
            //{
            //    if (!document.DockHandler.IsActivated)
            //        document.DockHandler.Close();
            //}
            IDockContent con = dockPanel.ActiveDocument;
            if (con != null && con is FrmDocBase)
            {
                FrmDocBase doc = (FrmDocBase)con;
                doc.SaveFile();
            }
        }

        #endregion

        private Diagram GetActiveDiagram()
        {
            Diagram diagram = null;
            if (this.dockPanel.ActiveDocument != null)
            {
                if (dockPanel.ActiveDocument is FrmDiagram)
                {
                    return ((FrmDiagram)dockPanel.ActiveDocument).ActiveDiagram;
                }
                if (dockPanel.ActiveDocument is FrmSymbol)
                {
                    return ((FrmSymbol)dockPanel.ActiveDocument).ActiveDiagram;
                }
            }
            return diagram;
        }

        public void Write(string text, Font font = null)
        {
            this.m_outputBox.AppendText(text, font);
        }

        public void WriteLine(string text, Font font = null)
        {
            this.m_outputBox.AppendText(text, font);
            this.m_outputBox.AppendText("\r\n");
        }

        public void Write(string tx, Color color)
        {
            this.m_outputBox.AppendText(tx, color);
        }

        public void WriteLine(string tx, Color color)
        {
            this.m_outputBox.AppendText(tx, color);
            this.m_outputBox.AppendText("\r\n");
        }

        private void barItemEditDelete_Click(object sender, EventArgs e)
        {
            Diagram diagram = GetActiveDiagram();
            if (diagram != null)
            {
                diagram.Controller.Delete();
            }
        }

        private void barItemAlign_Click(object sender, EventArgs e)
        {
            Diagram diagram = GetActiveDiagram();
            if (diagram == null)
                return;
            string name = "";
            if (sender is ToolStripButton)
                name = ((ToolStripButton)sender).Name;
            switch (name)
            {
                case "tsbAlignLeft":
                    diagram.AlignLeft();
                    break;
                case "tsbAlignCenter":
                    diagram.AlignCenter();
                    break;
                case "tsbAlignBottom":
                    diagram.AlignBottom();
                    break;
                case "tsbAlignMiddle":
                    diagram.AlignMiddle();
                    break;
                case "tsbAlignTop":
                    diagram.AlignTop();
                    break;
                case "tsbAlignRight":
                    diagram.AlignRight();
                    break;
                default:
                    break;
            }

        }

        private void barItemRotateFlip_Click(object sender, EventArgs e)
        {
            Diagram diagram = GetActiveDiagram();
            if (diagram == null)
                return;
            string name = "";
            if (sender is ToolStripButton)
                name = ((ToolStripButton)sender).Name;
            switch (name)
            {
                case "tsbRotateRight":
                    diagram.Rotate(90);
                    break;
                case "tsbRotateLeft":
                    diagram.Rotate(-90);
                    break;
                case "tsbFlipUpDown":
                    diagram.FlipHorizontal();
                    break;
                case "tsbFlipLeftRight":
                    diagram.FlipVertical();
                    break;
                default:
                    break;
            }
        }

        private void barItemSpace_Click(object sender, EventArgs e)
        {
            Diagram diagram = GetActiveDiagram();
            if (diagram == null)
                return;
            string name = "";
            if (sender is ToolStripButton)
                name = ((ToolStripButton)sender).Name;
            switch (name)
            {
                case "tsbSpaceAcross":
                    diagram.SpaceAcross();
                    break;
                case "tsbSpaceDown":
                    diagram.SpaceDown();
                    break;
                case "tsbSpaceWidth":
                    diagram.SameWidth();
                    break;
                case "tsbSpaceHeight":
                    diagram.SameHeight();
                    break;
                case "tsbSpaceSize":
                    diagram.SameSize();
                    break;
                default:
                    break;
            }
        }

        private void barItemMove_Click(object sender, EventArgs e)
        {
            Diagram diagram = GetActiveDiagram();
            if (diagram == null)
                return;
            string name = "";
            if (sender is ToolStripButton)
                name = ((ToolStripButton)sender).Name;
            switch (name)
            {
                case "tsbMoveUp":
                    diagram.NudgeUp();
                    break;
                case "tsbMoveDown":
                    diagram.NudgeDown();
                    break;
                case "tsbMoveLeft":
                    diagram.NudgeLeft();
                    break;
                case "tsbMoveRight":
                    diagram.NudgeRight();
                    break;
                default:
                    break;
            }
        }

        private void barItemView_Click(object sender, EventArgs e)
        {
            Diagram diagram = GetActiveDiagram();
            if (diagram == null)
                return;
            string name = "";
            if (sender is ToolStripButton)
                name = ((ToolStripButton)sender).Name;
            switch (name)
            {
                case "tsbGrid":
                    diagram.View.Grid.Visible = !diagram.View.Grid.Visible;
                    break;
                case "tsbRuler":
                    diagram.ShowRulers = !diagram.ShowRulers;
                    break;
                case "tsbZoomBig":
                    diagram.View.ZoomIn();
                    UpdateZoomToText(diagram);
                    break;
                case "tsbZoomSmall":
                    diagram.View.ZoomOut();
                    UpdateZoomToText(diagram);
                    break;
                default:
                    break;
            }
        }

        private void barItemEdit_Click(object sender, EventArgs e)
        {
            if (MainDock.ActiveDocument == null)
                return;
            FrmDocBase docForm = MainDock.ActiveDocument as FrmDocBase;
            if (docForm == null)
                return;

            string name = "";
            if (sender is ToolStripButton)
                name = ((ToolStripButton)sender).Name;
            switch (name)
            {
                case "tsbUndo":
                    docForm.VEditUndo();
                    //propertyEditor.PropertyGrid.Refresh( );
                    break;
                case "tsbRedo":
                    docForm.VEditRedo();
                    break;
                case "tsbCopy":
                    docForm.VEditCopy();
                    break;
                case "tsbCut":
                    docForm.VEditCut();
                    break;
                case "tsbPaste":
                    docForm.VEditPaste();
                    break;
                case "tsbDelete":
                    docForm.VEditDelete();
                    break;
                default:
                    break;
            }
        }

        private void barItemFont_Click(object sender, EventArgs e)
        {
            Diagram diagram = GetActiveDiagram();
            if (diagram == null)
                return;
            string name = "";
            if (sender is ToolStripButton)
                name = ((ToolStripButton)sender).Name;
            switch (name)
            {
                case "tsbTextLeft":
                    diagram.Controller.TextEditor.HorizontalAlignment = StringAlignment.Near;
                    break;
                case "tsbTextCenter":
                    diagram.Controller.TextEditor.HorizontalAlignment = StringAlignment.Center;
                    break;
                case "tsbTextRight":
                    diagram.Controller.TextEditor.HorizontalAlignment = StringAlignment.Far;
                    break;
                case "tsbBold":
                    if ( !DiagramHelper.CheckTextSelecionNode(diagram) )
                        break;
                    diagram.Controller.TextEditor.Bold = !(diagram.Controller.TextEditor.Bold);
                    break;
                case "tsbItalic":
                    if ( !DiagramHelper.CheckTextSelecionNode(diagram) )
                        break;
                    diagram.Controller.TextEditor.Italic = !(diagram.Controller.TextEditor.Italic);
                    break;
                case "tsbUnderline":
                    if ( !DiagramHelper.CheckTextSelecionNode(diagram) )
                        break;
                    diagram.Controller.TextEditor.Underline = !(diagram.Controller.TextEditor.Underline);
                    break;
                case "tsbFontUp":
                    diagram.Controller.TextEditor.PointSize++;
                    break;
                case "tsbFontDown":
                    diagram.Controller.TextEditor.PointSize--;
                    break;
                default:
                    break;
            }
        }

        private void tsbZoomTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Diagram diagram = GetActiveDiagram();
            if (diagram == null)
                return;
            string strMagValue = this.tsbZoomTo.Text;
            int idxPctSign = strMagValue.IndexOf('%');
            try
            {
                int magVal = Convert.ToInt32(this.tsbZoomTo.Text.Remove(idxPctSign, 1));
                diagram.View.Magnification = magVal;
            }
            catch (Exception)
            {
                this.tsbZoomTo.Text = "100%";
                diagram.View.Magnification = 100;
            }
        }

        public void UpdateZoomToText(Diagram diagram)
        {
            if (diagram!=null && diagram.View != null)
                this.tsbZoomTo.Text = diagram.View.Magnification + "%";
        }

        public void UpdateWhenFrmDiagramFoucus(FrmDocBase form)
        {
            if (form.m_isDisposing || this.m_isExiting)
            {
                return;
            }
            else
            {
                Diagram diagram = form.GetActiveDiagram();
                UpdateZoomToText(diagram);
                //HightLightTool(diagram.Controller.ActiveTool.Name);
                HightLightTool(form.preActiveTool);
            }
        }
        private void tsbZoomTo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tsbZoomTo_SelectedIndexChanged(null,null);
            }
        }

        public PaletteGroupBar PaletteGroupBar()
        {
            return this.m_toolBox.GetPaletteGroupBar();
        }

        private void tsbFontColor_SelectedColorChanged(object sender, EventArgs e)
        {
            Diagram diagram = this.GetActiveDiagram();
            if(diagram!=null)
                diagram.Controller.TextEditor.TextColor = this.tsbFontColor.Color;
        }

        private void tsbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Diagram diagram = GetActiveDiagram();
            if (diagram != null)
            {
                try
                {
                    float ptSize = Convert.ToSingle(this.tsbFontSize.Text);
                    diagram.Controller.TextEditor.PointSize = ptSize;
                }
                catch (Exception)
                {
                    
                }
            }
        }

        private void tsbFontSize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Diagram diagram = GetActiveDiagram();
                if (diagram != null)
                {
                    try
                    {
                        float ptSize = Convert.ToSingle(this.tsbFontSize.Text);
                        diagram.Controller.TextEditor.PointSize = ptSize;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void tsbGroup_Click(object sender, EventArgs e)
        {
            FrmSymbol doc = this.dockPanel.ActiveDocument as FrmSymbol;
            if (doc == null) //只对模型编辑器有效
                return;
            doc.ActiveDiagram.Controller.Group();
        }

        private void tsbUngroup_Click(object sender, EventArgs e)
        {
            FrmSymbol doc = this.dockPanel.ActiveDocument as FrmSymbol;
            if (doc == null) //只对模型编辑器有效
                return;
            doc.ActiveDiagram.Controller.UnGroup();
        }

        public void SetStatusLabelPosInfo(Point point)
        {
            if (point.IsEmpty)
                this.tslPosInfo.Text = tslPosInfoDefault;
            else
                this.tslPosInfo.Text = "X=" + point.X + "," + "Y=" + point.Y;
        }
        /// <summary>
        /// point1-point0得到长和宽，调用者自行确定大小关系
        /// </summary>
        /// <param name="point0"></param>
        /// <param name="point1"></param>
        public void SetStatusLabelPosInfo(System.Drawing.Rectangle rect)
        {
            this.tslPosInfo.Text = string.Format("X={0},Y={1},W={2},H={3}",
                rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void SetStatusLabelPosInfo(System.Drawing.RectangleF rect)
        {
            this.tslPosInfo.Text = string.Format("X={0},Y={1},W={2},H={3}",
                rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void SetStatusLabelPosInfo(string text="")
        {
            if (text.Length == 0)
                this.tslPosInfo.Text = tslPosInfoDefault;
            else
                this.tslPosInfo.Text = text;
        }

        public void SetStatusLabelActionInfo(string text = "")
        {
            if (text.Length == 0)
                this.tslActionInfo.Text = tslActionDefault;
            else
                this.tslActionInfo.Text = text;
        }

    }
}
