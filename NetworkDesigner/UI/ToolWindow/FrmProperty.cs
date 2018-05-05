using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SWD = Syncfusion.Windows.Forms.Diagram;
using SDC = Syncfusion.Windows.Forms.Diagram.Controls;

namespace NetworkDesigner.UI.ToolWindow
{
    public partial class FrmProperty : FrmBase
    {
        public PropertyGrid mPropertyGrid
        {
            get { return this.propertyEditor1.PropertyGrid; }
        }

        public FrmProperty(FrmMain _frmMain)
        {
            InitializeComponent();

            this.mainForm = _frmMain;
        }

        //public void SetPropertyDiagram(SDC.Diagram diagram)
        //{
        //    this.propertyEditor1.Diagram = diagram;
        //}

        public void SetSelectObject(Object obj)
        {
            this.mPropertyGrid.SelectedObject = obj;
        }
    }
}
