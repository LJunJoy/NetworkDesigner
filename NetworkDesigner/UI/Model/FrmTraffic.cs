using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Syncfusion.Windows.Forms.Grid;
using NetworkDesigner.UI.Document;
namespace NetworkDesigner.UI.Model
{
    public partial class FrmTraffic : Form
    {
        private FrmDiagram frmDiagram = null;
        public FrmTraffic(FrmDiagram form)
        {
            InitializeComponent();

            Grid1Setting();
            TestInput();

            this.frmDiagram = form;
        }

        private void Grid1Setting()
        {
            grid1.ColCount = 2;
            grid1.ColStyles[1].TextAlign = GridTextAlign.Left;
            grid1.ColStyles[1].ReadOnly = true;
            grid1.ColStyles[2].TextAlign = GridTextAlign.Left;
            grid1.HideCols[0] = true;
            grid1.HideRows[0] = true;
            grid1.ColWidths[1] = panel1.Width / 2;
            grid1.ColWidths[2] = panel1.Width - grid1.ColWidths[1];
            //Hides A, B, C in the column headers.
            grid1.Model.Options.NumberedColHeaders = false;
            //Hides 1, 2, 3 in the row headers.
            grid1.Model.Options.NumberedRowHeaders = false;
            grid1.TableStyle.Trimming = StringTrimming.EllipsisCharacter;//显示不下时用...
            //Enable Pixel Scrolling
            grid1.VScrollPixel = true;//滚动条出现时基于像素点而不是cell-width滑动，更合适
            grid1.HScrollPixel = true;

            //grid1.CellButtonClicked += grid1_CellButtonClicked;
            grid1.ResizingColumns += grid1_ResizingColumns;
        }

        private void TestInput()
        {
            string[] c1 = 
            { 
                "源节点",
                "目的节点",
                "包个数",
                "包大小",
                "包间隔",
                "开始时间",
                "结束时间",
                 };
            string[] c2 = 
            { 
                "11",
                "16",
                "100",
                "512",
                "1",
                "1",
                "300",
                 };
            grid1.RowCount = c1.Length;
            int curRow;
            for (int i = 0; i < grid1.RowCount; i++)
            {
                curRow = i + 1;
                grid1[curRow, 1].Text = c1[i];
                grid1[curRow, 1].CellType = GridCellTypeName.Static;
                grid1[curRow, 2].CellValue = c2[i];
            }
        }

        private void grid1_ResizingColumns(object sender, GridResizingColumnsEventArgs e)
        {
            if (grid1.ColWidths[1] < 3)
                grid1.ColWidths[1] = 3;
            grid1.ColWidths[2] = panel1.Width - grid1.ColWidths[1];
            if (grid1.ColWidths[2] < 3)
                grid1.ColWidths[2] = 3;
        }

        public void SafeClose()
        {
            this.Dispose();
        }
        private void FrmTraffic_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void RefreshNodes()
        {
            var scenario = frmDiagram.SaveToScenario("Qualnet");
            foreach (var host in scenario.hosts)
            {
                this.lbSrcNodes.Items.Add(host["name"]);
                this.lbDestNodes.Items.Add(host["name"]);
            }
            this.tvAllTraffic.Nodes[0].Nodes.Add("host6->host11");
            this.tvAllTraffic.Nodes[0].Nodes.Add("host18->host15");

        }

        private void btClearTraffic_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in this.tvAllTraffic.Nodes)
                node.Nodes.Clear();
        }
    }
}
