using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using NetworkDesigner.Utils.FileUtil;
using NetworkDesigner.Beans.Common;
using NetworkDesigner.UI.Document;
using Syncfusion.Windows.Forms.Diagram.Controls;
using NetworkDesigner.UI.ToolWindow;
using NetworkDesigner.Service.Model;

namespace NetworkDesigner.UI
{
    public partial class FrmMain : Form
    {
        // 处理最近打开文件的菜单项

        /// <summary>
        /// 添加新文件路径到最近打开项目菜单列表
        /// </summary>
        public void AddRecentProjectToMenu(string filePath)
        {
            AppSetting.LatestRecentProjects.Insert(0, filePath);//注意不能操作AppSetting.RecentProjects

            //从最后位置开始倒着找，如果找到一致名称，则移除旧记录
            for (int i = AppSetting.LatestRecentProjects.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (AppSetting.LatestRecentProjects[i] == AppSetting.LatestRecentProjects[j])
                    {
                        AppSetting.LatestRecentProjects.RemoveAt(i);
                        break;
                    }
                }
            }

            //最后，仅保留指定的文件列表数量
            for (int n = AppSetting.LatestRecentProjects.Count - 1; n > AppSetting.RecentMaxFiles - 1; n--)
            {
                AppSetting.LatestRecentProjects.RemoveAt(n);
            }

            UpdateRecentProjectMenu();
        }
        /// <summary>
        /// 动态更新最近文件菜单的子菜单项
        /// </summary>
        private void UpdateRecentProjectMenu()
        {
            int i;
            //清除当前菜单
            this.miRecentProject.DropDownItems.Clear();

            //检查所有最近文件是否依旧存在
            //CheckFi1es();

            //创建最近文件菜单项
            for (i = 0; i < AppSetting.LatestRecentProjects.Count; i++)
            {
                ToolStripItem menuItem = new ToolStripMenuItem();
                menuItem.Text = System.IO.Path.GetFileName(AppSetting.LatestRecentProjects[i]);
                menuItem.Tag = AppSetting.LatestRecentProjects[i];
                menuItem.ToolTipText = AppSetting.LatestRecentProjects[i];
                menuItem.Click += this.miRecentProjectItm_Click;
                this.miRecentProject.DropDownItems.Add(menuItem);
            }
            //插入清空列表菜单
            if (this.miRecentProject.DropDownItems.Count >= 1)
            {
                this.miRecentProject.Enabled = true;
                this.miRecentProject.DropDownItems.Add("-");

                ToolStripItem clearListItem = new ToolStripMenuItem("清空列表");
                clearListItem.Click += this.miClearRecentProject_Click;
                this.miRecentProject.DropDownItems.Add(clearListItem);
            }
            else
            {
                this.miRecentProject.Enabled = false;
            }
        }
        /// <summary>
        /// 打开最近打开的项目
        /// </summary>
        void miRecentProjectItm_Click(object sender, EventArgs e)
        {
            string path = (string)(((ToolStripMenuItem)sender).Tag);
            if (!Directory.Exists(path))
            {
                DialogResult dr = MessageBox.Show("指定路径找不到该项目！是否删除此目录？\r\n" + path,
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    AppSetting.LatestRecentProjects.Remove(path);
                    UpdateRecentProjectMenu();
                }
                return;
            }
            this.m_solutionBox.LoadProject(path);
        }
        /// <summary>
        /// 清空最近打开项目菜单列表的菜单项
        /// </summary>
        void miClearRecentProject_Click(object sender, EventArgs e)
        {
            AppSetting.LatestRecentProjects.Clear();
            UpdateRecentProjectMenu();
        }

        private void SaveAsToolStripMenuItem(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string FileName = saveFileDialog.FileName;
                }
            }
        }

        private void tsbSimResult_Click(object sender, EventArgs e)
        {

        }

        private void tsbTraffic_Click(object sender, EventArgs e)
        {
            FrmDiagram doc = dockPanel.ActiveDocument as FrmDiagram;
            if (doc == null)
                return;
            doc.BeginSetTraffic();
        }
        private void tsbTopo_Click(object sender, EventArgs e)
        {
            FrmDocBase doc = dockPanel.ActiveDocument as FrmDocBase;
            if (doc == null)
                return;
            Diagram diagram = GetActiveDiagram();
            if (diagram == null)
                return;
            HighLightBarItem("tsbTopo");
            doc.preActiveTool = "tsbTopo";//这里只负责激活工具，监听mouseup及取消工具在其他窗体的diagram控件上
        }

        private void barItemActiveTool_Click(object sender, EventArgs e)
        {
            FrmDocBase doc = this.dockPanel.ActiveDocument as FrmDocBase;
            if (doc == null)
                return;
            Diagram diagram = doc.GetActiveDiagram();
            if (diagram == null)
                return;
            ToolStripButton barItem = (ToolStripButton)sender;
            HighLightBarItem(barItem.Name); //打上标记，在其他窗体的diagram_click中检测
            doc.preActiveTool = barItem.Name;
            //"tsbNode" 对节点工具作单独的定制
            //"tsbTextNode" 对文本节点工具作单独的定制，不用dll中原有的TextTool
            if (m_BarName2DiagramTool.ContainsKey(barItem.Name))
            {
                diagram.Controller.ActivateTool(m_BarName2DiagramTool[barItem.Name]);
            }
        }
        public void ResetBarItemTool(FrmDocBase doc)
        {
            if (!this.tsbSelect.Checked)
            {
                Diagram diagram = doc.GetActiveDiagram();
                if (diagram != null)
                {
                    HighLightBarItem("选择");
                    diagram.Controller.ActivateTool("SelectTool");
                    doc.preActiveTool = "SelectTool";
                }
            }
        }
        /// <summary>
        /// 按工具栏按钮名称高亮工具栏的图标
        /// </summary>
        /// <param name="name"></param>
        private void HighLightBarItem(string toolstripButton)
        {
            foreach (var item in m_BarItems.Values)
            {
                if (item.Name.Equals(toolstripButton))
                {
                    item.Checked = true;
                }
                else
                {
                    item.Checked = false;
                }
            }
        }
        /// <summary>
        /// 按工具名称高亮工具栏的图标
        /// </summary>
        /// <param name="name"></param>
        private void HightLightTool(string name)
        {
            HighLightBarItem(name);
        }
        private void InitBarItemClickEventHandler()
        {
            this.tsbSelect.Click += barItemActiveTool_Click;
            this.tsbPan.Click += barItemActiveTool_Click;
            this.tsbTextNode.Click += barItemActiveTool_Click;
            this.tsbNode.Click += barItemActiveTool_Click;
            this.tsbLinkDirect.Click += barItemActiveTool_Click;
            this.tsbPolyLine.Click += barItemActiveTool_Click;
            this.tsbCurve.Click += barItemActiveTool_Click;
            this.tsbRectangle.Click += barItemActiveTool_Click;
            this.tsbPolygon.Click += barItemActiveTool_Click;
            this.tsbEllipse.Click += barItemActiveTool_Click;

            this.tsbUndo.Click += barItemEdit_Click;
            this.tsbRedo.Click += barItemEdit_Click;
            this.tsbCopy.Click += barItemEdit_Click;
            this.tsbCut.Click += barItemEdit_Click;
            this.tsbPaste.Click += barItemEdit_Click;
            this.tsbDelete.Click += barItemEdit_Click;

            this.tsbGrid.Click += barItemView_Click;
            this.tsbRuler.Click += barItemView_Click;
            this.tsbZoomBig.Click += barItemView_Click;
            this.tsbZoomSmall.Click += barItemView_Click;

            this.tsbAlignLeft.Click += barItemAlign_Click;
            this.tsbAlignCenter.Click += barItemAlign_Click;
            this.tsbAlignBottom.Click += barItemAlign_Click;
            this.tsbAlignMiddle.Click += barItemAlign_Click;
            this.tsbAlignTop.Click += barItemAlign_Click;
            this.tsbAlignRight.Click += barItemAlign_Click;

            this.tsbSpaceAcross.Click += barItemSpace_Click;
            this.tsbSpaceDown.Click += barItemSpace_Click;
            this.tsbSpaceWidth.Click += barItemSpace_Click;
            this.tsbSpaceHeight.Click += barItemSpace_Click;
            this.tsbSpaceSize.Click += barItemSpace_Click;

            this.tsbRotateRight.Click += barItemRotateFlip_Click;
            this.tsbRotateLeft.Click += barItemRotateFlip_Click;
            this.tsbFlipUpDown.Click += barItemRotateFlip_Click;
            this.tsbFlipLeftRight.Click += barItemRotateFlip_Click;

            this.tsbMoveUp.Click += barItemMove_Click;
            this.tsbMoveDown.Click += barItemMove_Click;
            this.tsbMoveLeft.Click += barItemMove_Click;
            this.tsbMoveRight.Click += barItemMove_Click;

            this.tsbTextLeft.Click += barItemFont_Click;
            this.tsbTextCenter.Click += barItemFont_Click;
            this.tsbTextRight.Click += barItemFont_Click;
            this.tsbBold.Click += barItemFont_Click;
            this.tsbItalic.Click += barItemFont_Click;
            this.tsbUnderline.Click += barItemFont_Click;
            this.tsbFontUp.Click += barItemFont_Click;
            this.tsbFontDown.Click += barItemFont_Click;

        }

        private void InitBarItemActiveTools()
        {
            m_BarName2DiagramTool = new Dictionary<string, string>();
            m_BarItems = new Dictionary<string, ToolStripButton>();

            m_BarName2DiagramTool.Add("tsbSelect", "SelectTool");
            m_BarName2DiagramTool.Add("tsbPan", "PanTool");
            //m_BarName2DiagramTool.Add("tsbNode", "LineLinkTool");
            m_BarName2DiagramTool.Add("tsbLinkDirect", "LineLinkTool");
            m_BarName2DiagramTool.Add("tsbTraffic", "DirectedLineLinkTool");
            m_BarName2DiagramTool.Add("tsbPolyLine", "PolyLineTool");
            m_BarName2DiagramTool.Add("tsbCurve", "CurveTool");
            m_BarName2DiagramTool.Add("tsbRectangle", "RectangleTool");
            m_BarName2DiagramTool.Add("tsbPolygon", "PolygonTool");
            m_BarName2DiagramTool.Add("tsbEllipse", "EllipseTool");
            //没有放 textNodeTool,对文本节点工具作单独的定制

            m_BarItems.Add("tsbSelect", tsbSelect);
            m_BarItems.Add("选择", tsbSelect);
            m_BarItems.Add("tsbPan", tsbPan);
            m_BarItems.Add("节点", tsbNode);
            m_BarItems.Add("tsbLinkDirect", tsbLinkDirect);
            m_BarItems.Add("链路", tsbLinkDirect);
            m_BarItems.Add("tsbPolyLine", tsbPolyLine);
            m_BarItems.Add("tsbCurve", tsbCurve);
            m_BarItems.Add("tsbRectangle", tsbRectangle);
            m_BarItems.Add("tsbPolygon", tsbPolygon);
            m_BarItems.Add("tsbEllipse", tsbEllipse);
            m_BarItems.Add("拓扑", tsbTopo);
            //放了 topoTool
        }
        public string 激活工具 = "选择";
        public void TryActiveNodeTool()
        {
            HighLightBarItem(激活工具); //打上标记，在其他窗体的diagram_click中检测

            FrmDocBase doc = this.dockPanel.ActiveDocument as FrmDocBase;
            if (doc == null)
                return;
            doc.preActiveTool = 激活工具;

            Diagram diagram = doc.GetActiveDiagram();
            if (diagram == null)
                return;
            //激活链路工具
            if (激活工具.Equals("链路"))
            {
                diagram.Controller.ActivateTool("LineLinkTool");
            }
            else
                diagram.Controller.ActivateTool("SelectTool");
            //if(激活工具.Equals("业务"))
        }
    }
}