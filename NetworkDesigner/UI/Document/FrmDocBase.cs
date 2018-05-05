using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NetworkDesigner.UI.Document
{
    /// <summary>
    /// <para>文档窗口的基类</para>
    /// 继承自DockContent，设置DockAreas不允许贴边，且一般要重写关闭窗口的响应
    /// </summary>
    public partial class FrmDocBase : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public DocInfo docInfo;
        public string preActiveTool="";

        private bool _isDisposing = false;
        /// <summary>
        /// 已调用过form_closing，正在执行disposing中，此时部分components例如diagram的view可能已经回收，会不安全
        /// </summary>
        public bool m_isDisposing
        {
            get { return _isDisposing; }
            protected set { _isDisposing = value; }
        }
        public NetworkDesigner.UI.FrmMain mainForm;
        public FrmDocBase()
        {
            InitializeComponent();

            docInfo = DocInfo.GetDefault();
        }

        #region 虚函数
        /// <summary>
        /// 保存文件
        /// </summary>
        public virtual void SaveFile()
        {

        }
        /// <summary>
        /// 另存文件
        /// </summary>
        public virtual void SaveAsFile()
        {

        }
        /// <summary>
        /// 保存文件
        /// </summary>
        public virtual void GenerateTopo(NetworkDesigner.Beans.DataStruct.TopoResult topo)
        {

        }
        /// <summary>
        /// 加载文件
        /// </summary>
        public virtual void LoadFile(DocInfo docInfo){ }

        public virtual Syncfusion.Windows.Forms.Diagram.Controls.Diagram GetActiveDiagram()
        {
            return null;
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public virtual void VEditUndo() { }
        /// <summary>
        /// 恢复
        /// </summary>
        public virtual void VEditRedo() { }
        /// <summary>
        /// 复制
        /// </summary>
        public virtual void VEditCopy() { }
        /// <summary>
        /// 剪切
        /// </summary>
        public virtual void VEditCut() { }
        /// <summary>
        /// 粘贴
        /// </summary>
        public virtual void VEditPaste() { }
        /// <summary>
        /// 删除
        /// </summary>
        public virtual void VEditDelete() { }
        #endregion

        private void cmiSave_Click(object sender, EventArgs e)
        {
            this.SaveFile();
        }

        private void miSaveAsFile_Click(object sender, EventArgs e)
        {
            this.SaveAsFile();
        }

        private void cmiClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void cmiCloseOther_Click(object sender, EventArgs e)
        {
            if (mainForm.MainDock.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Form activeMdi = ActiveMdiChild;
                foreach (Form form in MdiChildren)
                {
                    if (form != activeMdi)
                        form.Close();
                }
            }
            else
            {
                foreach (IDockContent document in mainForm.MainDock.DocumentsToArray())
                {
                    if (!document.DockHandler.IsActivated)
                        document.DockHandler.Close();
                }
            }
        }

        private void cmiCloseAll_Click(object sender, EventArgs e)
        {
            if (mainForm.MainDock.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Form activeMdi = ActiveMdiChild;
                foreach (Form form in MdiChildren)
                {
                    form.Close();
                }
            }
            else
            {
                foreach (IDockContent document in mainForm.MainDock.DocumentsToArray())
                {
                    document.DockHandler.Close();
                }
            }
        }

    }

    public class DocInfo
    {
        public enum DType
        {
            OTHER,TXT,XML,DIA,PAT,BLANK
        }
        public DType T = DType.OTHER;
        /// <summary>
        /// 文件全路径
        /// </summary>
        public string filePath="";
        /// <summary>
        /// 文件名称，含扩展名
        /// </summary>
        public string fileName="";
        /// <summary>
        /// 文件扩展名，含.
        /// </summary>
        public string fileExtension="";
        /// <summary>
        /// 文件名，不含扩展名
        /// </summary>
        public string fileNameNoExtension = "";

        /// <summary>
        /// 文件自上次保存后是否编辑过，注意不具有通用性，不同窗体可能有不同含义
        /// </summary>
        public bool isEdit = false;

        public DocInfo(string path)
        {
            filePath = path;
            fileName = System.IO.Path.GetFileName(path);
            fileExtension = System.IO.Path.GetExtension(path);
            fileNameNoExtension = System.IO.Path.GetFileNameWithoutExtension(path);
            switch (fileExtension.ToLower())
            {
                case ".dia":
                    T = DType.DIA;//代表仿真场景的图表diagram
                    break;
                case ".pat":
                    T = DType.PAT;//工具面板中的模型库
                    break;
                case ".txt":
                    T = DType.TXT;
                    break;
                case ".xml":
                    T = DType.XML;
                    break;
                case "":
                    T = DType.BLANK;
                    break;
                default:
                    T = DType.OTHER;
                    break;
            }
        }
        public static DocInfo GetDefault()
        {
            return new DocInfo("");
        }
    }
}
