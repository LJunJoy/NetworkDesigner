using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkDesigner.UI.ToolWindow
{
    /// <summary>
    /// <para>标准窗口的基类，例如解决方案管理器、属性窗口、输出窗口、工具箱等</para>
    /// 继承自DockContent，设置DockAreas只允许贴边，并且设置了HideOnClose，不会真正关闭
    /// </summary>
    public partial class FrmBase : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        protected NetworkDesigner.UI.FrmMain mainForm;
        
        public FrmBase()
        {
            InitializeComponent();
        }
    }
}
