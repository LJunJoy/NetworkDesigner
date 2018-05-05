using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Syncfusion.Windows.Forms.Tools;
using System.IO;
using NetworkDesigner.Utils.FileUtil;
using NetworkDesigner.UI.Dialog;
using NetworkDesigner.UI.Document;
using System.Collections;
using NetworkDesigner.Utils.Common;

namespace NetworkDesigner.UI.ToolWindow
{
    public partial class FrmSolution : FrmBase
    {
        private ContextMenuStrip m_proj;
        private ContextMenuStrip m_dir;
        private ContextMenuStrip m_file;

        private string m_preNodeName = "";
        /// <summary>
        /// 上一次选中的节点，用于copy、cut，为简化实现，暂只允许只选中一个节点
        /// </summary>
        private TreeNodeAdv m_preCCNode = null;
        private string m_preCCType = "NONE";//"Cut" "Copy" "NONE"

        public FrmSolution(FrmMain _frmMain)
        {
            InitializeComponent();

            this.mainForm = _frmMain;
            this.tvaProject.Nodes[0].Tag = new SolutionTreeTag(SolutionTreeTag.NodeType.SOLU,"");
            UpdateSolutionText();

            CreateContexMenu();
        }
        /// <summary>
        /// 更新设计方案显示的项目数
        /// </summary>
        private void UpdateSolutionText()
        {
            this.tvaProject.Nodes[0].Text = string.Format("设计方案（{0}个项目）", this.tvaProject.Nodes.Count - 1);
        }
        private void CreateContexMenu()
        {
            ToolStripMenuItem add1 = new ToolStripMenuItem();
            add1.Text = cmiAdd.Text;
            add1.Image = cmiAdd.Image;
            add1.ShortcutKeys = cmiAdd.ShortcutKeys;
            add1.DropDownItems.AddRange(new ToolStripItem[]{
                new ToolStripMenuItem(cmiNewFile.Text, cmiNewFile.Image, cmiNewFile_Click, cmiNewFile.ShortcutKeys),
                new ToolStripMenuItem(cmiNewDir.Text, cmiNewDir.Image, cmiNewDir_Click, cmiNewDir.ShortcutKeys),
                new ToolStripMenuItem(cmiFromWizard.Text, cmiFromWizard.Image, cmiNewWizard_Click, cmiFromWizard.ShortcutKeys)
            });

            m_proj = new ContextMenuStrip();
            m_proj.Items.Add(add1);
            m_proj.Items.Add(new ToolStripMenuItem(cmiCut.Text, cmiCut.Image, cmiCut_Click, cmiCut.ShortcutKeys));
            m_proj.Items.Add(new ToolStripMenuItem(cmiPaste.Text, cmiPaste.Image, cmiPaste_Click, cmiPaste.ShortcutKeys));
            m_proj.Items.Add(new ToolStripMenuItem(cmiDelet.Text, cmiDelet.Image, cmiDelet_Click, cmiDelet.ShortcutKeys));
            m_proj.Items.Add(new ToolStripMenuItem(cmiRename.Text, cmiRename.Image, cmiRename_Click, cmiRename.ShortcutKeys));
            m_proj.Items.Add(new ToolStripMenuItem(cmiCloseP.Text, cmiCloseP.Image, cmiCloseP_Click, cmiCloseP.ShortcutKeys));
            m_proj.Items.Add(new ToolStripMenuItem(cmiReloadP.Text, cmiReloadP.Image, cmiReloadP_Click, cmiReloadP.ShortcutKeys));
            m_proj.Items.Add(new ToolStripMenuItem(cmiSortUp.Text, cmiSortUp.Image, cmiSortUp_Click, cmiSortUp.ShortcutKeys));
            m_proj.Items.Add(new ToolStripMenuItem(cmiOpenDir.Text, cmiOpenDir.Image, cmiOpenDir_Click, cmiOpenDir.ShortcutKeys));
            m_proj.Items.Add(new ToolStripMenuItem(cmiSetDefaultProj.Text, cmiSetDefaultProj.Image, cmiSetDefaultProj_Click, cmiSetDefaultProj.ShortcutKeys));

            ToolStripMenuItem add2 = new ToolStripMenuItem();
            add2.Text = cmiAdd.Text;
            add2.Image = cmiAdd.Image;
            add2.ShortcutKeys = cmiAdd.ShortcutKeys;
            add2.DropDownItems.AddRange(new ToolStripItem[]{
                new ToolStripMenuItem(cmiNewFile.Text, cmiNewFile.Image, cmiNewFile_Click, cmiNewFile.ShortcutKeys),
                new ToolStripMenuItem(cmiNewDir.Text, cmiNewDir.Image, cmiNewDir_Click, cmiNewDir.ShortcutKeys),
                new ToolStripMenuItem(cmiFromWizard.Text, cmiFromWizard.Image, cmiNewWizard_Click, cmiFromWizard.ShortcutKeys)
            });

            m_dir = new ContextMenuStrip();
            m_dir.Items.Add(add2);
            m_dir.Items.Add(new ToolStripMenuItem(cmiCut.Text, cmiCut.Image, cmiCut_Click, cmiCut.ShortcutKeys));
            m_dir.Items.Add(new ToolStripMenuItem(cmiCopy.Text, cmiCopy.Image, cmiCopy_Click, cmiCopy.ShortcutKeys));
            m_dir.Items.Add(new ToolStripMenuItem(cmiPaste.Text, cmiPaste.Image, cmiPaste_Click, cmiPaste.ShortcutKeys));
            m_dir.Items.Add(new ToolStripMenuItem(cmiDelet.Text, cmiDelet.Image, cmiDelet_Click, cmiDelet.ShortcutKeys));
            m_dir.Items.Add(new ToolStripMenuItem(cmiRename.Text, cmiRename.Image, cmiRename_Click, cmiRename.ShortcutKeys));
            m_dir.Items.Add(new ToolStripMenuItem(cmiReloadP.Text, cmiReloadP.Image, cmiReloadP_Click, cmiReloadP.ShortcutKeys));
            m_dir.Items.Add(new ToolStripMenuItem(cmiSortUp.Text, cmiSortUp.Image, cmiSortUp_Click, cmiSortUp.ShortcutKeys));
            m_dir.Items.Add(new ToolStripMenuItem(cmiOpenDir.Text, cmiOpenDir.Image, cmiOpenDir_Click, cmiOpenDir.ShortcutKeys));

            m_file = new ContextMenuStrip();
            m_file.Items.Add(new ToolStripMenuItem(cmiCut.Text, cmiCut.Image, cmiCut_Click, cmiCut.ShortcutKeys));
            m_file.Items.Add(new ToolStripMenuItem(cmiCopy.Text, cmiCopy.Image, cmiCopy_Click, cmiCopy.ShortcutKeys));
            m_file.Items.Add(new ToolStripMenuItem(cmiPaste.Text, cmiPaste.Image, cmiPaste_Click, cmiPaste.ShortcutKeys));
            m_file.Items.Add(new ToolStripMenuItem(cmiDelet.Text, cmiDelet.Image, cmiDelet_Click, cmiDelet.ShortcutKeys));
            m_file.Items.Add(new ToolStripMenuItem(cmiRename.Text, cmiRename.Image, cmiRename_Click, cmiRename.ShortcutKeys));
            m_file.Items.Add(new ToolStripMenuItem(cmiOpenDir.Text, cmiOpenDir.Image, cmiOpenDir_Click, cmiOpenDir.ShortcutKeys));
        }
        private void SetProjectNode(TreeNodeAdv node,string path)
        {
            node.Tag = new SolutionTreeTag(SolutionTreeTag.NodeType.PROJ, path);
            node.Text = Path.GetFileNameWithoutExtension(path);
            node.LeftImageIndices = new int[] { 1 };
            node.Expand();
        }
        private void SetDirectoryNode(TreeNodeAdv node,string path)
        {
            node.Tag = new SolutionTreeTag(SolutionTreeTag.NodeType.DIR, path);
            node.Text = Path.GetFileNameWithoutExtension(path);
            node.LeftImageIndices = new int[] { 4 };
        }
        private void SetFileNode(TreeNodeAdv node,string path)
        {
            node.Tag = new SolutionTreeTag(SolutionTreeTag.NodeType.FILE, path);
            string extension = Path.GetExtension(path);
            node.Text = Path.GetFileName(path);
            switch (extension)
            {
                case ".txt":
                    node.LeftImageIndices = new int[] { 5 };
                    break;
                case ".doc":
                case ".docx":
                    node.LeftImageIndices = new int[] { 6 };
                    break;
                case ".xml":
                    node.LeftImageIndices = new int[] { 7 };
                    break;
                case ".c":
                case ".c++":
                case ".cs":
                case ".java":
                    node.LeftImageIndices = new int[] { 9 };
                    break;
                default:
                    node.LeftImageIndices = new int[] { 8 };
                    break;
            }
            
        }
        /// <summary>
        /// 打开项目，加载项目文件夹并添加到解决方案管理器
        /// </summary>
        public void LoadProject(string path)
        {
            SolutionTreeTag solutionTag = (SolutionTreeTag)this.tvaProject.Nodes[0].Tag;

            for (int i=1;i<tvaProject.Nodes.Count;i++)
            {
                var t = (SolutionTreeTag)tvaProject.Nodes[i].Tag;
                if (path.Equals(t.Path))
                {
                    MessageBox.Show("该项目已经打开！");
                    return;
                }
            }
            if (!Directory.Exists(path))
            {
                MessageBox.Show("指定路径找不到该项目！\r\n" + path);
                return;
            }
            this.mainForm.AddRecentProjectToMenu(path);

            //add project treenode
            TreeNodeAdv node = new TreeNodeAdv();
            this.SetProjectNode(node,path);
            this.tvaProject.Nodes.Add(node);
            UpdateSolutionText();

            this.tvaProject.BeginUpdate();
            LoadDirectoryFileTree(path, node);
            this.tvaProject.EndUpdate();
        }
        /// <summary>
        /// 递归加载指定目录的子目录和文件到node下，若node为null则加到treeview下
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node"></param>
        private void LoadDirectoryFileTree(string path, TreeNodeAdv node = null)
        {
            //先加载该目录下的子目录
            string[] dirs = null;
            try
            {
                dirs = Directory.GetDirectories(path);
            }
            catch (Exception)
            {
                LogHelper.LogInfo("当前目录无访问权限：\r\n"+path);
                return;
            }
            foreach (string dir in dirs)
            {
                TreeNodeAdv nodeDir = new TreeNodeAdv();
                this.SetDirectoryNode(nodeDir, dir);
                if (node == null)// 如果是null将指定目录下的子目录直接加到TreeView下 
                {
                    this.tvaProject.Nodes.Add(nodeDir);
                }
                else//否则将节点加载到传进来的节点下面.
                {
                    node.Nodes.Add(nodeDir);
                }
                LoadDirectoryFileTree(dir, nodeDir);//递归加载当前目录可能存在的子目录和文件
            }

            //再加载该目录下的文件
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                TreeNodeAdv nodeFile = new TreeNodeAdv();
                this.SetFileNode(nodeFile, file);
                if (node == null)// 如果是null，将指定目录下的所有文件直接加到TreeView下 
                {
                    this.tvaProject.Nodes.Add(nodeFile);
                }
                else//否则将节点加载到传进来的节点下面
                {
                    node.Nodes.Add(nodeFile);
                }
            }
        }
        
        #region 菜单点击响应函数

        /// <summary>
        /// 重新加载项目或文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiReloadP_Click(object sender, EventArgs e)
        {
            SolutionTreeTag tag = (SolutionTreeTag)this.tvaProject.SelectedNode.Tag;

            //处理那些已经打开的文件：这里暂简化处理，要求先关闭
            foreach (var frm in FactoryDocument.allDocs)
            {
                DocInfo doc = frm.docInfo;
                if (doc.filePath.StartsWith(tag.Path))
                {
                    MessageBox.Show("请先关闭已打开的项目文件");
                    return;
                }
            }
            TreeNodeAdv selectNode = this.tvaProject.SelectedNode;
            TreeNodeAdv node = null;

            this.tvaProject.BeginUpdate();
            //清空节点并重新加载
            switch(tag.Type)
            {
                case SolutionTreeTag.NodeType.PROJ:
                    this.tvaProject.Nodes.Remove(selectNode);
                    //重新加载
                    node = new TreeNodeAdv();
                    this.SetProjectNode(node, tag.Path);
                    this.tvaProject.Nodes.Add(node);
                    UpdateSolutionText();
                    LoadDirectoryFileTree(tag.Path, node);
                    node.Nodes.Sort(SolutionTreeComparer.DefaultOne);
                    node.Expand();
                    break;
                case SolutionTreeTag.NodeType.DIR:
                    node = new TreeNodeAdv();
                    this.SetDirectoryNode(node, tag.Path);
                    selectNode.Parent.Nodes.Add(node);
                    selectNode.Parent.Nodes.Remove(selectNode);//注意顺序
                    LoadDirectoryFileTree(tag.Path, node);
                    node.Parent.Nodes.Sort(SolutionTreeComparer.DefaultOne);
                    node.Nodes.Sort(SolutionTreeComparer.DefaultOne);
                    node.Expand();
                    break;
                default:
                    break;
            }
            this.tvaProject.EndUpdate();
        }

        private void cmiSortUp_Click(object sender, EventArgs e)
        {
            this.tvaProject.BeginUpdate();
            this.tvaProject.SelectedNode.Nodes.Sort(SolutionTreeComparer.DefaultOne);
            this.tvaProject.SelectedNode.Expand();
            this.tvaProject.EndUpdate();
        }
        private void cmiCut_Click(object sender, EventArgs e)
        {
            this.m_preCCNode = this.tvaProject.SelectedNode;
            this.m_preCCType = "Cut";
        }

        private void cmiCopy_Click(object sender, EventArgs e)
        {
            this.m_preCCNode = this.tvaProject.SelectedNode;
            this.m_preCCType = "Copy";
        }

        private void cmiPaste_Click(object sender, EventArgs e)
        {
            if (this.m_preCCNode == null)
            {
                MessageBox.Show("没有复制文件或文件夹", "提示");
                return;
            }
            SolutionTreeTag tagNow = (SolutionTreeTag)this.tvaProject.SelectedNode.Tag;
            SolutionTreeTag tagPre = (SolutionTreeTag)this.m_preCCNode.Tag;
            
            //处理那些已经打开的文件：这里暂简化处理，要求先关闭
            foreach (var frm in FactoryDocument.allDocs)
            {
                DocInfo doc = frm.docInfo;
                if (doc.filePath.StartsWith(tagPre.Path) || doc.filePath.StartsWith(tagNow.Path))
                {
                    MessageBox.Show("请先关闭已打开的项目文件");
                    return;
                }
            }
            TreeNodeAdv select = this.tvaProject.SelectedNode;
            string filePath = "";

            //目前只处理文件夹->文件夹；文件->文件夹(不允许目的地为文件)；并且项目节点不会被复制
            switch (tagNow.Type)
            {
                case SolutionTreeTag.NodeType.PROJ:
                case SolutionTreeTag.NodeType.DIR:
                    if (tagPre.Type == SolutionTreeTag.NodeType.DIR)
                    {
                        if (tagNow.Path.StartsWith(tagPre.Path))
                        {
                            MessageBox.Show("不能从上层目录复制或移动到下层目录");
                            return;
                        }

                        int n = 1;
                        filePath = Path.Combine(tagNow.Path, this.m_preCCNode.Text);
                        while (File.Exists(filePath) || Directory.Exists(filePath))
                        {
                            filePath = Path.Combine(tagNow.Path, this.m_preCCNode.Text + n);
                            n++;
                            if (n == 100)
                                break;
                        }
                        if (n == 100)
                        {
                            MessageBox.Show("当前目录下默认命名的文件夹过多\n请整理并重新命名后再添加");
                            return;
                        }

                        try
                        {
                            if(this.m_preCCType.Equals("Copy"))
                                FileHelper.CopyDir(tagPre.Path, filePath);
                            if (this.m_preCCType.Equals("Cut")) //cut之后source一定要置空
                            {
                                FileHelper.MoveDir(tagPre.Path, filePath);
                                this.m_preCCNode.Parent.Nodes.Remove(m_preCCNode);
                                this.m_preCCNode = null;
                                this.m_preCCType = "NONE";
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("复制或移动文件夹失败，请检查其他进程占用");
                            return;
                        }
                        TreeNodeAdv nodeDir = new TreeNodeAdv();
                        this.SetDirectoryNode(nodeDir, filePath);
                        select.Nodes.Add(nodeDir);//复制到当前节点之下
                        LoadDirectoryFileTree(filePath, nodeDir);
                    }
                    else if (tagPre.Type == SolutionTreeTag.NodeType.FILE)
                    {
                        int n = 1;
                        filePath = Path.Combine(tagNow.Path, Path.GetFileName(tagPre.Path));
                        while (File.Exists(filePath) || Directory.Exists(filePath))
                        {
                            filePath = Path.Combine(tagNow.Path, Path.GetFileNameWithoutExtension(tagPre.Path)
                                + n + Path.GetExtension(tagPre.Path));
                            n++;
                            if(n == 100)
                                break;
                        }
                        if (n == 100)
                        {
                            MessageBox.Show("当前目录下默认命名的文件夹过多\n请整理并重新命名后再添加");
                            return;
                        }
                        try
                        {
                            if (this.m_preCCType.Equals("Copy"))
                                FileHelper.Copy(tagPre.Path, filePath);
                            if (this.m_preCCType.Equals("Cut")) //cut之后source一定要置空
                            {
                                FileHelper.Move(tagPre.Path, filePath);
                                this.m_preCCNode.Parent.Nodes.Remove(m_preCCNode);
                                this.m_preCCNode = null;
                                this.m_preCCType = "NONE";
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("复制或移动文件失败，请检查其他进程占用");
                            return;
                        }
                        TreeNodeAdv nodeDir = new TreeNodeAdv();
                        this.SetFileNode(nodeDir, filePath);
                        select.Nodes.Add(nodeDir);//复制到当前节点之下
                    }
                    break;
                case SolutionTreeTag.NodeType.FILE:
                    MessageBox.Show("不允许复制或移动到文件");
                    break;
                default:
                    break;
            }

        }

        private void cmiDelet_Click(object sender, EventArgs e)
        {
            SolutionTreeTag tag = (SolutionTreeTag)this.tvaProject.SelectedNode.Tag;
            if (DialogResult.No ==
                MessageBox.Show("确定从磁盘永久删除吗？\r\n" + this.tvaProject.SelectedNode.Text,
                "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                )
            {
                return;
            }
            //处理那些已经打开的文件：这里暂简化处理，要求先关闭
            foreach (var frm in FactoryDocument.allDocs)
            {
                DocInfo doc = frm.docInfo;
                if (doc.filePath.StartsWith(tag.Path))
                {
                    MessageBox.Show("请先关闭已打开的项目文件");
                    return;
                }
            }
            switch (tag.Type)
            {
                case SolutionTreeTag.NodeType.PROJ:
                case SolutionTreeTag.NodeType.DIR:
                    if (Directory.Exists(tag.Path))
                    {
                        try
                        {
                            Directory.Delete(tag.Path, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("未能成功删除文件夹，请检查是否其他进程占用，并重新加载项目\n"
                                + ex.Message);
                            return;
                        }
                    }
                    if (tag.Type == SolutionTreeTag.NodeType.PROJ)
                    {
                        this.tvaProject.Nodes.Remove(tvaProject.SelectedNode);
                        this.UpdateSolutionText();
                    }
                    else
                    {
                        tvaProject.SelectedNode.Parent.Nodes.Remove(tvaProject.SelectedNode);
                    }
                    break;
                case SolutionTreeTag.NodeType.FILE:
                    if(File.Exists(tag.Path))
                    {
                        try
                        {
                            File.Delete(tag.Path);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("未能成功删除文件，请检查是否其他进程占用，并重新加载项目\n"
                                + ex.Message);
                            return;
                        }
                    }
                    tvaProject.SelectedNode.Parent.Nodes.Remove(tvaProject.SelectedNode);
                    break;
                default:
                    break;
            }
        }

        private void cmiRename_Click(object sender, EventArgs e)
        {
            SolutionTreeTag tag = (SolutionTreeTag)this.tvaProject.SelectedNode.Tag;
            //处理那些已经打开的文件：这里暂简化处理，要求先关闭
            foreach (var frm in FactoryDocument.allDocs)
            {
                DocInfo doc = frm.docInfo;
                if (doc.filePath.StartsWith(tag.Path))
                {
                    MessageBox.Show("请先关闭已打开的项目文件");
                    return;
                }
            }
            m_preNodeName = this.tvaProject.SelectedNode.Text;
            this.tvaProject.LabelEdit = true;//允许修改
            this.tvaProject.BeginEdit();
            //开始等待输入，在NodeEditorValidted（标准treeview控件的AfterLabelEdit）事件中处理
        }
        
        private void tvaProject_NodeEditorValidated(object sender, TreeNodeAdvEditEventArgs e)
        {
            TreeNodeAdv selectNode = this.tvaProject.SelectedNode;
            if (e.Label.Trim().Length == 0)
            {
                MessageBox.Show("不允许名称为空");
                selectNode.Text = m_preNodeName;
                this.tvaProject.BeginEdit();
                return;
            }
            if (e.Label.Equals(m_preNodeName))
            {
                this.tvaProject.EndEdit();
                this.tvaProject.LabelEdit = false;
                return;
            }
            SolutionTreeTag tag = (SolutionTreeTag)selectNode.Tag;
            TreeNodeAdv node = null;
            string parentFile = "";
            string newFile = "";

            this.tvaProject.BeginUpdate();//警告：最后要EndUpdate，不能中间return

            switch(tag.Type)
            {
                case SolutionTreeTag.NodeType.FILE:
                    parentFile = Path.GetDirectoryName(tag.Path);
                    newFile = Path.Combine(parentFile, e.Label);
                    if (File.Exists(newFile) || Directory.Exists(newFile))
                    {
                        MessageBox.Show("已存在同名文件或文件夹");
                        selectNode.Text = m_preNodeName;
                        this.tvaProject.BeginEdit();
                    }
                    else 
                    {
                        File.Move(tag.Path, newFile);
                        tag.Path = newFile;
                        selectNode.Text = e.Label;
                        selectNode.Nodes.Sort(SolutionTreeComparer.DefaultOne);
                        this.tvaProject.EndEdit();
                        this.tvaProject.LabelEdit = false;
                    }
                    break;
                case SolutionTreeTag.NodeType.DIR:
                case SolutionTreeTag.NodeType.PROJ:
                    newFile = new DirectoryInfo(string.Format(@"{0}\..\{1}",tag.Path,e.Label)).FullName;
                    if (File.Exists(newFile) || Directory.Exists(newFile))
                    {
                        MessageBox.Show("已存在同名文件或文件夹");
                        selectNode.Text = m_preNodeName;
                        this.tvaProject.BeginEdit();
                    }
                    else 
                    {
                        Directory.Move(tag.Path, newFile);
                        tag.Path = newFile;
                        //清空当前节点并重新加载
                        if (tag.Type == SolutionTreeTag.NodeType.PROJ)
                        {
                            this.tvaProject.Nodes.Remove(selectNode);
                            node = new TreeNodeAdv();
                            this.SetProjectNode(node, newFile);
                            this.tvaProject.Nodes.Add(node);
                            UpdateSolutionText();
                            LoadDirectoryFileTree(newFile, node);
                            node.Expand();
                        }
                        else
                        {
                            node = new TreeNodeAdv();
                            this.SetDirectoryNode(node, tag.Path);
                            selectNode.Parent.Nodes.Add(node);
                            selectNode.Parent.Nodes.Remove(selectNode);//注意顺序
                            LoadDirectoryFileTree(tag.Path, node);
                            node.Parent.Nodes.Sort(SolutionTreeComparer.DefaultOne);
                            node.Nodes.Sort(SolutionTreeComparer.DefaultOne);
                            node.Expand();
                        }
                        this.tvaProject.EndEdit();
                        this.tvaProject.LabelEdit = false;
                    }
                    break;
                default:
                    this.tvaProject.EndEdit();
                    this.tvaProject.LabelEdit = false;
                    break;
            }

            this.tvaProject.EndUpdate();//警告：最后要EndUpdate
        }
        private void cmiOpenFile_Click(object sender, EventArgs e)
        {

        }

        private void cmiOpenDir_Click(object sender, EventArgs e)
        {
            SolutionTreeTag tag = (SolutionTreeTag)this.tvaProject.SelectedNode.Tag;
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/select," + tag.Path;
            System.Diagnostics.Process.Start(psi);
        }
        private void cmiNewFile_Click(object sender, EventArgs e)
        {
            SolutionTreeTag tag = (SolutionTreeTag)this.tvaProject.SelectedNode.Tag;
            int n=1;
            string filePath = "";
            while (n < 100)
            {
                filePath = Path.Combine(tag.Path, "新建文件" + n);
                if (!Directory.Exists(filePath) && !File.Exists(filePath))//windows不允许文件夹和文件的全路径完全同名
                {
                    FileStream fs = File.Create(filePath);
                    fs.Close();//新创建文件必须注意close，否则此时立即move重命名会“另一进程占用”
                    TreeNodeAdv nodeFile = new TreeNodeAdv();
                    this.SetFileNode(nodeFile, filePath);
                    tvaProject.SelectedNode.Nodes.Add(nodeFile);
                    tvaProject.SelectedNode = nodeFile;
                    this.cmiRename_Click(null,null);
                    
                    break;
                }
                n++;
            }
            if (n == 100)
            {
                MessageBox.Show("当前目录下默认命名的新建文件过多\n请整理并重新命名后再添加");
            }
        }

        private void cmiNewDir_Click(object sender, EventArgs e)
        {
            SolutionTreeTag tag = (SolutionTreeTag)this.tvaProject.SelectedNode.Tag;
            int n = 1;
            string filePath = "";
            while (n < 100)
            {
                filePath = Path.Combine(tag.Path, "新建文件夹" + n);
                if (!Directory.Exists(filePath) && !File.Exists(filePath))//windows不允许文件夹和文件的全路径完全同名
                {
                    Directory.CreateDirectory(filePath);
                    TreeNodeAdv nodeDir = new TreeNodeAdv();
                    this.SetDirectoryNode(nodeDir, filePath);
                    tvaProject.SelectedNode.Nodes.Add(nodeDir);
                    tvaProject.SelectedNode = nodeDir;
                    this.cmiRename_Click(null, null);

                    break;
                }
                n++;
            }
            if (n == 100)
            {
                MessageBox.Show("当前目录下默认命名的新建文件夹过多\n请整理并重新命名后再添加");
            }
        }

        private void cmiNewWizard_Click(object sender, EventArgs e)
        {
            DiaNewFile df = new DiaNewFile();
            df.StartPosition = FormStartPosition.CenterParent;
            df.ShowDialog();
            if (df.IsCancelled)
            {
                df.Dispose();
                return;
            }
            TreeNodeAdv node = this.tvaProject.SelectedNode;
            SolutionTreeTag tag = (SolutionTreeTag)node.Tag;
            string filePath = tag.Path + "\\" + df.ParamName;
            if (df.Category.Equals("文件夹"))
            {
                if (File.Exists(filePath) || Directory.Exists(filePath))
                {
                    MessageBox.Show("同名文件或文件夹已经存在，请重新检查或加载项目");
                    return;
                }
                TreeNodeAdv nodeDir = new TreeNodeAdv();
                this.SetDirectoryNode(nodeDir, filePath);
                node.Nodes.Add(nodeDir);
                Directory.CreateDirectory(filePath);
            }
            else if (df.Category.Equals("文件"))
            {
                if (File.Exists(filePath) || Directory.Exists(filePath))
                {
                    MessageBox.Show("同名文件或文件夹已经存在，请重新检查或加载项目");
                    return;
                }
                TreeNodeAdv nodeFile = new TreeNodeAdv();
                this.SetFileNode(nodeFile, filePath);
                node.Nodes.Add(nodeFile);
                FileStream fs = File.Create(filePath);
                fs.Close();
            }
            //http://www.cnblogs.com/yieryi/p/4616610.html 在以下两种情况下调用Close不会释放窗体??
            if(!df.IsDisposed)
                df.Dispose();
        }
        /// <summary>
        /// 关闭并卸载项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiCloseP_Click(object sender, EventArgs e)
        {
            SolutionTreeTag prjTag = (SolutionTreeTag)this.tvaProject.SelectedNode.Tag;
            
            //处理那些已经打开的文件：这里暂简化处理，要求先关闭已打开的当前项目下的文件
            foreach (var frm in FactoryDocument.allDocs)
            {
                DocInfo doc = frm.docInfo;
                if (doc.filePath.StartsWith(prjTag.Path))
                {
                    MessageBox.Show("请先关闭已打开的项目文件");
                    return;
                }
            }

            var solutionTag = (SolutionTreeTag)this.tvaProject.Nodes[0].Tag;
            this.tvaProject.Nodes.Remove(this.tvaProject.SelectedNode);
            UpdateSolutionText();
        }
        #endregion

        private void tvaProject_MouseDown(object sender, MouseEventArgs e)
        {
            //如果是点击右键，添加上下文菜单
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                Point clickPoint = new Point(e.X, e.Y);
                TreeNodeAdv currentNode = this.tvaProject.GetNodeAtPointEx(clickPoint);
                if (currentNode != null)//判断你点的是不是一个节点
                {
                    SolutionTreeTag tag = (SolutionTreeTag)currentNode.Tag;
                    switch (tag.Type)//根据不同节点显示不同的右键菜单，当然你可以让它显示一样的菜单
                    {
                        case SolutionTreeTag.NodeType.PROJ:
                            this.tvaProject.ContextMenuStrip = this.m_proj;
                            break;
                        case SolutionTreeTag.NodeType.DIR:
                            this.tvaProject.ContextMenuStrip = this.m_dir;
                            break;
                        case SolutionTreeTag.NodeType.FILE:
                            this.tvaProject.ContextMenuStrip = this.m_file;
                            break;
                        default:
                            this.tvaProject.ContextMenuStrip = null;
                            break;
                    }
                }
                else
                {
                    this.tvaProject.ContextMenuStrip = null;
                }
            }
        }
        //键盘事件详解 https://www.cnblogs.com/rainbow70626/p/4671540.html
        private void tvaProject_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.tvaProject.LabelEdit)
                return;//当前正在修改节点text
            if (this.tvaProject.SelectedNode == null)
                return;
            if (e.KeyCode == Keys.Delete)
                this.cmiDelet_Click(null, null);
            else if (e.KeyCode == Keys.Oemplus)
                this.tvaProject.SelectedNode.Expand();
            else if(e.KeyCode == Keys.OemMinus)
                this.tvaProject.SelectedNode.CollapseAll();
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
            {
                SolutionTreeTag tag = (SolutionTreeTag)this.tvaProject.SelectedNode.Tag;
                if (tag.Type == SolutionTreeTag.NodeType.DIR || tag.Type == SolutionTreeTag.NodeType.FILE)
                {
                    this.m_preCCNode = this.tvaProject.SelectedNode;
                    this.m_preCCType = "Copy";
                }
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.X)
            {
                SolutionTreeTag tag = (SolutionTreeTag)this.tvaProject.SelectedNode.Tag;
                if (tag.Type == SolutionTreeTag.NodeType.DIR || tag.Type == SolutionTreeTag.NodeType.FILE)
                {
                    this.m_preCCNode = this.tvaProject.SelectedNode;
                    this.m_preCCType = "Cut";
                }
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
            {
                SolutionTreeTag tag = (SolutionTreeTag)this.tvaProject.SelectedNode.Tag;
                cmiPaste_Click(null, null);
            }
        }

        private void tvaProject_NodeMouseDoubleClick(object sender, TreeViewAdvMouseClickEventArgs e)
        {
            if (this.tvaProject.SelectedNode == null)
                return;
            TreeNodeAdv currentNode = this.tvaProject.SelectedNode;
            SolutionTreeTag tag = (SolutionTreeTag)currentNode.Tag;
            switch (tag.Type)//根据不同节点显示不同的右键菜单，当然你可以让它显示一样的菜单
            {
                case SolutionTreeTag.NodeType.FILE:
                    bool alreadyOpen;
                    FrmDocBase doc = FactoryDocument.CreateDocument(mainForm, tag.Path, out alreadyOpen);
                    if (alreadyOpen)
                        doc.Select();
                    else if (doc != null)
                    {
                        doc.LoadFile(doc.docInfo);
                        doc.Show(mainForm.MainDock);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取默认项目的目录路径，也即字体加粗的树节点
        /// </summary>
        /// <returns></returns>
        public string GetDefaultProject()
        {
            foreach (TreeNodeAdv node in this.tvaProject.Nodes)
            {
                if (node.Font.Bold)
                    return ((SolutionTreeTag)node.Tag).Path;
            }
            return "";
        }

        private void cmiSetDefaultProj_Click(object sender, EventArgs e)
        {
            TreeNodeAdv currentNode = this.tvaProject.SelectedNode;
            foreach (TreeNodeAdv node in this.tvaProject.Nodes)
            {
                if (node == currentNode)
                    node.Font = new Font(node.Font, FontStyle.Bold);
                else if(node.Font.Bold)
                    node.Font = new Font(node.Font, FontStyle.Regular);
            }
        }
    }
    public class SolutionTreeTag
    {
        public enum NodeType
        {
            SOLU,PROJ,DIR,FILE
        }
        public NodeType Type = NodeType.SOLU;
        public string Path="";

        public SolutionTreeTag(NodeType t, string p)
        {
            Type = t;
            Path = p;
            switch(t)
            {
                case NodeType.SOLU:
                    break;
                case NodeType.PROJ:
                    break;
                case NodeType.FILE:
                    break;
                default:
                    break;
            }
        }
    }
    class SolutionTreeComparer : IComparer
    {
        public int Compare(object ox, object oy)//此时是升序  
        {
            TreeNodeAdv x = (TreeNodeAdv)ox;
            TreeNodeAdv y = (TreeNodeAdv)oy;
            SolutionTreeTag tx = (SolutionTreeTag)x.Tag;
            SolutionTreeTag ty = (SolutionTreeTag)y.Tag;
            if (tx.Type < ty.Type)
                return -1;
            if (tx.Type > ty.Type)
                return 1;
            return x.Text.CompareTo(y.Text);
        }
        private static SolutionTreeComparer _DefaultOne = new SolutionTreeComparer();
        public static SolutionTreeComparer DefaultOne
        {
            get { return _DefaultOne; }
        }
    }
}
