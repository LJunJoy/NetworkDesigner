using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetworkDesigner.Beans.Model;
using Syncfusion.Windows.Forms.Grid;
using NetworkDesigner.Utils.Common;
using NetworkDesigner.Service.Model;
using Syncfusion.Windows.Forms.Diagram;
namespace NetworkDesigner.UI.ToolWindow
{
    public partial class FrmModelAttr : FrmBase
    {
        static public int NoChildBMP = 2;
        static public int OpenedBMP = 0;
        static public int ClosedBMP = 1;
        private GridRangeInfo[] _gridRangeInfo = new GridRangeInfo[] { GridRangeInfo.Empty };
        //sample data members...
        public static TreeModelAttr extData = new TreeModelAttr();
        private Node curNode = null;
        public FrmModelAttr(FrmMain _frmMain)
        {
            InitializeComponent();

            this.mainForm = _frmMain;
            GridSettings();
        }

        /// <summary>
        /// Grid settings for better look and feel. 
        /// </summary>
        private void GridSettings()
        {
            //Add a custom cell control
            gridCtl.CellModels.Add("TreeCell", new TreeCellModel(gridCtl.Model));
            gridCtl.ControllerOptions = GridControllerOptions.All & (~GridControllerOptions.OleDataSource);
            //设计器中设置不显示行和列的header
            //Hides A, B, C in the column headers.
            gridCtl.Model.Options.NumberedColHeaders = false;
            gridCtl.Model.Options.NumberedRowHeaders = false;
            gridCtl.VScrollPixel = true;//滚动条出现时基于像素点而不是cell-width滑动，更合适
            gridCtl.HScrollPixel = true;
            
            gridCtl.ColCount = 2; //不包含col_header列的列数
            gridCtl.ColStyles[1].TextAlign = GridTextAlign.Left;
            gridCtl.ColStyles[2].TextAlign = GridTextAlign.Left;
            gridCtl.ColStyles[1].ReadOnly = true;
            gridCtl.RowHeights[0] = 3;//让header行有3像素高，方便拖动边线
            gridCtl.ColWidths[0] = 1;//不显示各行的header，但要显示边框
            //要在gridCtl_SizeChanged中设置才起作用
            gridCtl.ColWidths[1] = 120;//panel.Size.Width / 2 - 1;
            gridCtl.ColWidths[2] = panel.Size.Width - gridCtl.ColWidths[1] - 1;
            //foreach (GridStyleInfo style in this.gridCtl.ColStyles)
            //{
            //    style.TextColor = Color.Black;
            //}
            //make the imagelist available thru the tablestyle
            gridCtl.TableStyle.ImageList = this.imageList1;
            gridCtl.TableStyle.Trimming = StringTrimming.EllipsisCharacter;
            //tab key navigation set as false to move the next control
            gridCtl.WantTabKey = false;

            gridCtl.ChangeSelectionState(0, 0, _gridRangeInfo);

            gridCtl.ResetVolatileData(); //重置缓存数据
            gridCtl.QueryCellInfo += new GridQueryCellInfoEventHandler(GridQueryCellInfo);
            gridCtl.QueryRowCount += new GridRowColCountEventHandler(GridQueryRowCount);
            gridCtl.QueryColCount += new GridRowColCountEventHandler(GridQueryColCount);
            //gridCtl.QueryColWidth += new GridRowColSizeEventHandler(GridQueryColWidth);

            //handle saving data back to the data source...
            gridCtl.SaveCellInfo += new GridSaveCellInfoEventHandler(GridSaveCellInfo);
        }

        public void ShowSimModel(SimModel model,Node node=null)
        {
            if (model == null)
            {
                this.gridCtl.BeginUpdate();
                extData.Reset();
                this.gridCtl.EndUpdate();
                this.gridCtl.Refresh();
                curNode = null;
                return;
            }
            else
                curNode = node;
            List<SimAttr> datas = new List<SimAttr>();
            AddSimAttr(datas, model, 0);
            this.gridCtl.BeginUpdate();
            extData.data = datas;
            extData.RefreshDisplayData();
            this.gridCtl.EndUpdate();
            this.gridCtl.Refresh();
            gridCtl.ChangeSelectionState(0, 1, _gridRangeInfo); //选中0，1格，相当于隐藏
            if(!this.Visible)
                this.Show();
        }
        private void AddSimAttr(List<SimAttr> datas, SimModel model,int indent)
        {
            foreach (SimAttr attr in model.attrs.Values)
            {
                if (attr.type == AttrType.模型)
                {
                    attr.IndentLevel = indent;
                    attr.ExpandState = GridNodeState.Opened;
                    datas.Add(attr);
                    AddSimAttr(datas, (SimModel)attr.data, indent + 1);
                }
                else if (attr.type == AttrType.模型列表 || attr.type== AttrType.接口列表)
                {
                    attr.IndentLevel = indent;
                    attr.ExpandState = GridNodeState.Opened;
                    datas.Add(attr);
                    List<SimModel> ms = attr.data as List<SimModel>;
                    foreach (SimModel m in ms)
                    {
                        AddSimAttr(datas, m, indent + 1);
                    }
                }
                else
                {
                    attr.IndentLevel = indent;
                    attr.ExpandState = GridNodeState.NoChildren;
                    datas.Add(attr);
                }
            }
        }
        #region Grid Events
        void GridQueryRowCount(object sender, GridRowColCountEventArgs e)
        {
            e.Count = extData.RowCountView;
            e.Handled = true;
        }

        void GridQueryColCount(object sender, GridRowColCountEventArgs e)
        {
            e.Count = extData.ColCount;
            e.Handled = true;
        }
        void GridQueryCellInfo(object sender, GridQueryCellInfoEventArgs e)
        {
            if (e.RowIndex <= 0 || e.ColIndex <= 0)
                return;

            int col0 = e.ColIndex - 1; //从0计的非header列数
            int row0 = e.RowIndex - 1; //从0计的非header行数
            if(row0>=extData.RowCountView)
                return;

            SimAttr attr = extData[row0];
            switch (col0) //我们认为，同一列的cell是同一类
            {
                case 0:
                    e.Style.CellType = "TreeCell";
                    e.Style.CellValue = attr.name;
                    e.Style.Tag = attr.IndentLevel;
                    e.Style.ImageIndex = (int)attr.ExpandState;
                    break;
                case 1:
                    switch (attr.type)
                    {
                        case AttrType.模型:
                        case AttrType.模型列表:
                        case AttrType.接口列表:
                            //e.Style.CellValue = attr.refModel;
                            break;
                        case AttrType.字符串:
                            e.Style.CellValue = (string)attr.data;
                            break;
                        case AttrType.选择器:
                            e.Style.CellType = GridCellTypeName.ComboBox;
                            e.Style.CellValue = (string)attr.data;
                            e.Style.ChoiceList = attr.options;
                            break;
                        default:
                            e.Style.CellValue = attr.data.ToString();
                            break;
                    }
                    break;
                default:
                    break;
            }

            e.Handled = true;
        }

        void GridSaveCellInfo(object sender, GridSaveCellInfoEventArgs e) //从cell保存到内存
        {
            if (e.ColIndex <= 0 || e.RowIndex <= 0)
                return;

            int col0 = e.ColIndex - 1; //从0计的非header列数
            int row0 = e.RowIndex - 1; //从0计的非header行数
            if (row0 >= extData.RowCountView)
                return;

            string str = "";
            SimAttr attr = extData[row0];
            switch (col0) //我们认为，同一列的cell是同一类
            {
                case 0:
                    attr.IndentLevel = (int)e.Style.Tag;
                    attr.ExpandState = e.Style.ImageIndex;
                    break;
                case 1:
                    object preData = attr.data;
                    switch (attr.type)
                    {
                        case AttrType.模型:
                        case AttrType.模型列表:
                        case AttrType.接口列表:
                            //e.Style.CellValue = attr.refModel;
                            break;
                        case AttrType.字符串:
                            attr.data = (string)(e.Style.CellValue);
                            break;
                        case AttrType.选择器:
                            attr.data = (string)(e.Style.CellValue);
                            break;
                        default:
                            attr.data = (string)(e.Style.CellValue);
                            break;
                    }
                    if(curNode != null)
                    {
                        string data;
                        string curpin;
                        float toset = 0;
                        switch(attr.name)
                        {
                            case "名称":
                            case "name":
                                data = (string)attr.data;
                                if (!data.Equals(curNode.Name))
                                {
                                    curNode.Name = data;//Diagram会自己检测是否有重名的节点，若有会重命名
                                    if (!curNode.Name.Equals(data))
                                    {
                                        mainForm.WriteLine("已有同名节点，已自动更正", Color.Blue);
                                        data = (string)preData;
                                        attr.data = data;
                                        e.Style.CellValue = data;
                                    }
                                    else
                                        NetworkDesigner.Utils.DiagramUtil.NodeHelper.SetNodeLabel0(curNode, data);
                                }
                                break;
                            case "X":
                                data = (string)attr.data;
                                curpin = curNode.PinPoint.X.ToString();
                                if (!data.Equals(curpin))
                                {
                                    if (!float.TryParse(data, out toset))
                                    {
                                        attr.data = curpin;
                                        e.Style.CellValue = curpin;
                                    }
                                    else
                                        curNode.PinPoint = new PointF(toset, curNode.PinPoint.Y);
                                }
                                break;
                            case "Y":
                                data = (string)attr.data;
                                curpin = curNode.PinPoint.Y.ToString();
                                if (!data.Equals(curpin))
                                {
                                    if (!float.TryParse(data, out toset))
                                    {
                                        attr.data = curpin;
                                        e.Style.CellValue = curpin;
                                    }
                                    else
                                        curNode.PinPoint = new PointF(curNode.PinPoint.X, toset);
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    break;
                default:
                    break;
            }
            try
            {
                extData.RefreshDisplayData();
                this.gridCtl.Refresh();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
            }

            e.Handled = true;
        }
        #endregion
        private void gridCtl_CellDoubleClick(object sender, GridCellClickEventArgs e)
        {
            gridCtl.CurrentCell.BeginEdit();
        }

        private void gridCtl_ResizingColumns(object sender, GridResizingColumnsEventArgs e)
        {
            gridCtl.ColWidths[2] = panel.Size.Width - gridCtl.ColWidths[1] - 1;
            //最后一列要留有余量，防止点击时出现scroll bar
        }

        private void gridCtl_SizeChanged(object sender, EventArgs e)
        {
            //gridCtl.ColWidths[1] = panel.Size.Width / 2;
            gridCtl.ColWidths[2] = panel.Size.Width - gridCtl.ColWidths[1] - 1;
        }

        private void gridCtl_CurrentCellKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gridCtl.CurrentCell.IsEditing)
                    gridCtl.CurrentCell.EndEdit();
                else
                {
                    if (gridCtl.CurrentCell.ColIndex > 2) //从1计，第1列为row header，第2列为name项，第3列为value项
                    {
                        gridCtl.CurrentCell.BeginEdit();
                    }
                }
            }
        }
    }
}
