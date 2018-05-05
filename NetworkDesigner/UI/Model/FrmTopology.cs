using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetworkDesigner.Beans.DataStruct;
using Syncfusion.Windows.Forms.Grid;
using NetworkDesigner.Service.Model;
using NetworkDesigner.Beans.Model;
using System.Collections.Specialized;
using NetworkDesigner.UI.Document;
namespace NetworkDesigner.UI.Model
{
    public partial class FrmTopology : Form
    {
        public TopoBase curTopo = null;
        public Dictionary<string, Type> topos = new Dictionary<string,Type>();
        private GridRangeInfo[] _gridRangeInfo = new GridRangeInfo[] { GridRangeInfo.Empty };
        private int grid2_preRow = 0;
        private int grid2_preCol = 0;
        private string preApplyCmd = "";
        private FrmDocBase docForm;
        //public TopoResult topoResult;
        public FrmTopology(FrmDocBase form)
        {
            InitializeComponent();

            docForm = form;
            topos.Add("格型", typeof(TopoGrid));
            topos.Add("随机", typeof(TopoRandom));
            topos.Add("星型", typeof(TopoStar));
            topos.Add("环型", typeof(TopoCircle));
            topos.Add("混合型", typeof(TopoMix));
            //topos.Add("树型", typeof(TopoTree));
            //其他拓扑模型暂时不做

            Grid1Setting();
            Grid2Setting();
            
            this.rtbCmd.LanguageOption = RichTextBoxLanguageOptions.AutoFontSizeAdjust;//不设置时输入字体始终不是宋体
        }
        private void Grid1Setting()
        {
            grid1.ColCount = 2;
            grid1.ColStyles[1].TextAlign = GridTextAlign.Left;
            grid1.ColStyles[1].ReadOnly = true;
            grid1.ColStyles[2].TextAlign = GridTextAlign.Left;
            grid1.RowHeights[0] = 3;
            grid1.ColWidths[1] = splitUp.Panel1.Width / 2;
            grid1.ColWidths[2] = splitUp.Panel1.Width - grid1.ColWidths[1] - 2;
            //Hides A, B, C in the column headers.
            grid1.Model.Options.NumberedColHeaders = false;
            //Hides 1, 2, 3 in the row headers.
            grid1.Model.Options.NumberedRowHeaders = false;
            grid1.TableStyle.Trimming = StringTrimming.EllipsisCharacter;//显示不下时用...
            //Enable Pixel Scrolling
            grid1.VScrollPixel = true;//滚动条出现时基于像素点而不是cell-width滑动，更合适
            grid1.HScrollPixel = true;

            grid1.CellButtonClicked += grid1_CellButtonClicked;
        }
        private void Grid2Setting()
        {
            grid2.ColCount = 3;
            grid2.ColStyles[1].TextAlign = GridTextAlign.Left;
            grid2.ColStyles[2].TextAlign = GridTextAlign.Left;
            grid2.ColStyles[3].TextAlign = GridTextAlign.Left;
            grid2.RowHeights[0] = 3;
            grid2.ColWidths[1] = splitUp.Panel2.Width / 3;
            grid2.ColWidths[2] = grid2.ColWidths[1];
            grid2.ColWidths[3] = splitUp.Panel2.Width - grid2.ColWidths[2] - grid2.ColWidths[1] - 2;
            //Hides A, B, C in the column headers.
            grid2.Model.Options.NumberedColHeaders = false;
            //Hides 1, 2, 3 in the row headers.
            grid2.Model.Options.NumberedRowHeaders = false;
            grid2.TableStyle.Trimming = StringTrimming.EllipsisCharacter;
            //Enable Pixel Scrolling
            grid2.VScrollPixel = true;
            grid2.HScrollPixel = true;

            grid2.CellButtonClicked += grid2_CellButtonClicked;
        }

        private void UpdateRichText()
        {
            this.rtbCmd.Text = curTopo.GetCmdText();
        }
        private void SetGrid2LastRow()
        {
            grid2[grid2.RowCount, 1].CellType = GridCellTypeName.PushButton;
            grid2[grid2.RowCount, 1].Description = "增加";
            grid2[grid2.RowCount, 1].TextColor = Color.Blue;
            grid2[grid2.RowCount, 2].CellType = GridCellTypeName.PushButton;
            grid2[grid2.RowCount, 2].Description = "删除";
            grid2[grid2.RowCount, 2].TextColor = Color.Blue;
            grid2[grid2.RowCount, 3].CellType = GridCellTypeName.PushButton;
            grid2[grid2.RowCount, 3].Description = "确定";
            grid2[grid2.RowCount, 3].TextColor = Color.Blue;
        }
        private void UpdateGrid2()
        {
            grid2.BeginUpdate();
            if(grid2.RowCount > 0)
                grid2.Rows.RemoveRange(1, grid2.RowCount);
            grid2.RowCount = curTopo.pSets.Count + 2; //额外添加两行：模型库选择和确认按钮
            grid2[1, 1].Text = "选择";
            grid2[1, 1].CellType = GridCellTypeName.Static;
            grid2[1, 2].CellType = GridCellTypeName.ComboBox;
            grid2[1, 2].ChoiceList = ModelInfoHelper.modelCates;
            grid2[1, 2].CellValue = "PIM.实体.节点";
            grid2[1, 2].DropDownStyle = GridDropDownStyle.Exclusive;//只允许选择

            grid2[1, 3].Text = "设置属性";
            grid2[1, 3].CellType = GridCellTypeName.Static;

            int num = curTopo.GetTotalRow();
            StringCollection selector = new StringCollection();
            for (int i = 0; i < num; i++)
                selector.Add("[" + i + "]");
            num = 2;
            foreach (SetParam param in curTopo.pSets)
            {
                grid2[num, 1].CellType = GridCellTypeName.ComboBox;
                grid2[num, 1].CellValue = param.selectStr;
                grid2[num, 1].ChoiceList = selector;

                grid2[num, 2].CellType = GridCellTypeName.ComboBox;
                grid2[num, 2].CellValue = param.model;
                grid2[num, 2].ChoiceList = ModelInfoHelper.GetModelCateChoice((string)grid2[1, 2].CellValue);
                //属性项暂没处理
                num++;
            }
            SetGrid2LastRow();
            grid2.EndUpdate();
            grid2.Refresh();
        }
        private void grid2_CurrentCellEditingComplete(object sender, EventArgs e)
        {
            //将行的改动同步设置到SetParam参数
            int row = grid2.CurrentCellInfo.RowIndex - 2;
            if (row >= 0 && row < curTopo.pSets.Count)
            {
                SetParam param = curTopo.pSets[row];
                param.selectStr = grid2[grid2.CurrentCellInfo.RowIndex, 1].Text;
                string mstr = grid2[grid2.CurrentCellInfo.RowIndex, 2].Text;
                if (!mstr.Contains("."))//加上模型库名称
                {
                    mstr = grid2[1, 2].Text + "." + mstr;
                    grid2[grid2.CurrentCellInfo.RowIndex, 2].CellValue = mstr;
                }
                param.attrs["model"] = mstr;
                param.model = mstr;
                //param.attrs = (string)grid2[grid2.CurrentCellInfo.RowIndex, 2].Text;
                //属性项暂没处理
            }
        }
        private void grid2_CellButtonClicked(object sender, GridCellButtonClickedEventArgs e)
        {           
            if (grid2.CurrentCell.IsEditing)
                grid2.CurrentCell.EndEdit();

            if (e.RowIndex == grid2.RowCount)
            {
                if (e.Button.Text.Equals("增加"))
                {
                    int addRowIndex = grid2_preRow + 1;
                    if (addRowIndex <= 1 || addRowIndex > grid2.RowCount)
                        addRowIndex = grid2.RowCount;
                    if (grid2_preCol < 1 || grid2_preCol > grid2.ColCount)
                        grid2_preCol = 1;
                    SetParam param = new SetParam();
                    int insert = addRowIndex - 2;
                    if (insert >= 0 && insert <= curTopo.pSets.Count)
                        curTopo.pSets.Insert(insert, param);
                    else
                        MessageBox.Show("索引不对"+insert);
                    UpdateGrid2();
                    grid2_preRow = addRowIndex;
                    grid2.ChangeSelectionState(addRowIndex, grid2_preCol, _gridRangeInfo);//一定要注意更新selection
                }
                else if (e.Button.Text.Equals("删除"))
                {
                    if (grid2.RowCount == 2)
                        return; //这两行不能删除
                    if (grid2_preRow < 1 || grid2_preRow >= grid2.RowCount)
                        grid2_preRow = grid2.RowCount - 1;
                    if (grid2_preCol < 1 || grid2_preCol > grid2.ColCount)
                        grid2_preCol = 1;
                    int remove = grid2_preRow - 2;
                    if (remove >= 0 && remove < curTopo.pSets.Count)
                        curTopo.pSets.RemoveAt(remove);
                    else
                        MessageBox.Show("索引不对" + remove);
                    UpdateGrid2();
                    grid2.ChangeSelectionState(grid2_preRow, grid2_preCol, _gridRangeInfo);//一定要注意更新selection
                }
                else if (e.Button.Text.Equals("确定"))
                {
                    SetParam param;
                    StringBuilder sbd = new StringBuilder();
                    for (int i=0;i<curTopo.pSets.Count;i++)
                    {
                        param = curTopo.pSets[i];
                        if(!curTopo.ValidateSetParam(param.selectStr))
                        {
                            MessageBox.Show("请检查第" + (i + 2) + "行的选择项内容");
                            return;
                        }
                        sbd.Append("{").Append(param.FormatString()).Append("}");
                    }
                    foreach(ArgsParam arg in curTopo.args)
                    {
                        if(arg.name.Equals("-p"))
                            arg.val = sbd.ToString();
                    }
                    try
                    {
                        curTopo.InitParam(); //grid2只能修改参数、增加链路等，参数修改后要再次initparam，更安全
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    UpdateRichText();
                }
            }
        }

        public void BeginEdit(string topoName, RectangleF rect)
        {
            if (rect.Width == 0 || rect.Height == 0)
            {
                MessageBox.Show("请输入有效的范围");
                return;
            }
            if (!topos.ContainsKey(topoName))
            {
                MessageBox.Show("暂不支持此布局方式");
                return;
            }
            if (rect.Width <= 0 || rect.Height <= 0)
            {
                MessageBox.Show("请输入非空区域作布局范围");
                return;
            }
            this.curTopo = (TopoBase)Activator.CreateInstance(topos[topoName]);
            curTopo.Range = rect;
            this.InitControl();
            this.rtbCmd.Clear();
            if(!this.Visible)
                this.Show();
            else
                this.Focus();
        }

        public void InitControl()
        {
            grid1.BeginUpdate();
            grid2.BeginUpdate();

            if (grid1.RowCount > 0)
                grid1.Rows.RemoveRange(1, grid1.RowCount);
            if (grid2.RowCount > 0)
                grid2.Rows.RemoveRange(1, grid2.RowCount);

            grid1.RowCount = curTopo.args.Count + 2; //额外添加一行布局类型，一行确认按钮
            grid1[1, 1].Text = "布局类型*";
            grid1[1, 1].CellType = GridCellTypeName.Static;
            grid1[1, 2].CellType = GridCellTypeName.ComboBox;
            grid1[1, 2].ChoiceList = ModelInfoHelper.GetModelCateChoice("功能模型.拓扑生成");
            grid1[1, 2].CellValue = curTopo.Name;
            grid1[1, 2].DropDownStyle = GridDropDownStyle.Exclusive;//只允许选择

            int row0 = 2;
            int curRow = this.grid1.RowCount;

            grid1[curRow, 1].CellType = GridCellTypeName.PushButton;
            grid1[curRow, 1].Description = "重置";
            grid1[curRow, 1].TextColor = Color.Blue;
            grid1[curRow, 2].CellType = GridCellTypeName.PushButton;
            grid1[curRow, 2].Description = "确定";
            grid1[curRow, 2].TextColor = Color.Blue;

            ArgsParam arg;
            GridStyleInfo style;
            for (int i=0; i<curTopo.args.Count;i++)
            {
                arg = curTopo.args[i];
                curRow = row0 + i;
                grid1[curRow, 1].Text = arg.note;
                grid1[curRow, 1].CellType = GridCellTypeName.Static;
                style = grid1[curRow, 2];
                switch (arg.cType)
                {
                    case ControlType.label:
                        style.CellType = GridCellTypeName.Static;
                        style.CellValue = arg.val;
                        style.Tag = arg;
                        break;
                    case ControlType.textbox:
                        style.CellType = GridCellTypeName.TextBox;
                        style.CellValue = arg.val;
                        style.Tag = arg;
                        break;
                    case ControlType.combox:
                        style.CellType = GridCellTypeName.ComboBox;
                        style.CellValue = arg.val;
                        if (arg.selector != null)
                            style.ChoiceList = arg.selector;
                        style.Tag = arg;
                        break;
                    case ControlType.combox_noEdit:
                        style.CellType = GridCellTypeName.ComboBox;
                        style.CellValue = arg.val;
                        style.DropDownStyle = GridDropDownStyle.Exclusive;//只允许选择
                        if (arg.selector != null)
                            style.ChoiceList = arg.selector;
                        style.Tag = arg;
                        break;
                    case ControlType.checkbox:
                        style.CellType = GridCellTypeName.CheckBox;
                        style.CellValue = arg.val;
                        style.CheckBoxOptions = new GridCheckBoxCellInfo("y","n", "", true);
                        style.Tag = arg;
                        break;
                    default:
                        MessageBox.Show("类型未知的Grid" + arg.cType);
                        break;
                }
            }
            //grid1.Model.ColWidths.ResizeToFit(GridRangeInfo.Col(1),
                //GridResizeToFitOptions.NoShrinkSize);
            grid1.EndUpdate();
            grid2.EndUpdate();
            grid1.Refresh();
            grid2.Refresh();
        }
        private void grid1_CellButtonClicked(object sender, GridCellButtonClickedEventArgs e)
        {
            if (grid1.CurrentCell.IsEditing)
                grid1.CurrentCell.EndEdit();
            if (e.Button.Text.Equals("确定"))
            {
                ArgsParam param;
                for (int i = 2; i < grid1.RowCount - 1; i++) //第一行是布局类型，最后一行是确定取消，注意Grid-content下标从1计
                {
                    param = grid1[i, 2].Tag as ArgsParam;
                    if (param != null)
                        param.val = grid1[i, 2].Text;
                }
                try
                {
                    curTopo.InitParam(); //grid1的确定点击时总节点数就确定了，grid2只能修改参数、增加链路等
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                UpdateRichText();
                UpdateGrid2();
            }
            else if (e.Button.Text.Equals("重置"))
            {
                this.BeginEdit(curTopo.Name, curTopo.Range);
            }
        }
        public void SafeClose()
        {
            this.Dispose();
        }
        private void grid1_CurrentCellEditingComplete(object sender, EventArgs e)
        {
            int row = grid1.CurrentCellInfo.RowIndex;
            int col = grid1.CurrentCellInfo.ColIndex;
            if (row == 1 && col == 2)
            {
                string newTopo = grid1[row,col].Text;
                if (!newTopo.Equals(curTopo.Name))
                {
                    if (!preApplyCmd.Equals(this.rtbCmd.Text))
                    {
                        DialogResult dr = MessageBox.Show("当前布局命令修改后未执行\n是否放弃并进入新的布局命令?",
                            "提示", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            if (!topos.ContainsKey(newTopo))
                                MessageBox.Show("暂不支持此布局方式");
                            else
                            {
                                this.newTopo = newTopo;
                                this.timer1.Enabled = true;
                                //this.BeginEdit(newTopo, curTopo.Range);//直接调用好像grid要抛些警告
                            }
                        }
                        else if (dr == DialogResult.No)
                        {
                            grid1[row, col].CellValue = curTopo.Name;
                        }
                    }
                    else
                    {
                        this.newTopo = newTopo;
                        this.timer1.Enabled = true;
                        //this.BeginEdit(newTopo, curTopo.Range);//直接调用好像grid要抛些警告
                    }
                }
            }
        }
        private string newTopo;
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.BeginEdit(newTopo, curTopo.Range);
        }
        private void grid1_SizeChanged(object sender, EventArgs e)
        {
            grid2.Size = splitUp.Panel2.ClientSize;
            grid1.ColWidths[2] = splitUp.Panel1.Width - grid1.ColWidths[1] - 2;
            //最后一列要留有余量，防止点击时出现scroll bar
        }

        private void grid2_SizeChanged(object sender, EventArgs e)
        {
            grid2.Size = splitUp.Panel2.ClientSize;
            grid2.ColWidths[3] = splitUp.Panel2.Width - grid2.ColWidths[2] - grid2.ColWidths[1] - 2;
            //最后一列要留有余量，防止点击时出现scroll bar
        }

        private void FrmTopology_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void grid1_ResizingColumns(object sender, GridResizingColumnsEventArgs e)
        {
            grid1.ColWidths[2] = splitUp.Panel1.Width - grid1.ColWidths[1] - 2;
        }

        private void grid2_ResizingColumns(object sender, GridResizingColumnsEventArgs e)
        {
            grid2.ColWidths[3] = splitUp.Panel2.Width - grid2.ColWidths[2] - grid2.ColWidths[1] - 2;
        }
        /// <summary>
        /// 下拉框快要弹出时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid2_CurrentCellShowingDropDown(object sender, GridCurrentCellShowingDropDownEventArgs e)
        {
            GridCurrentCell cc = grid2.CurrentCell;
            if (cc.RowIndex > 1 && cc.ColIndex == 2) //是设置模型的combox
            {
                GridStyleInfo style = grid2[cc.RowIndex, cc.ColIndex];
                style.ChoiceList = ModelInfoHelper.GetModelCateChoice((string)grid2[1, 2].CellValue);
            }
        }

        private void grid2_CurrentCellActivated(object sender, EventArgs e)
        {
            if (grid2.CurrentCell.RowIndex != grid2.RowCount)
            {
                grid2_preRow = grid2.CurrentCell.RowIndex;
                grid2_preCol = grid2.CurrentCell.ColIndex;
            }
        }

        private void grid2_CellDoubleClick(object sender, GridCellClickEventArgs e)
        {
            grid2.CurrentCell.BeginEdit();
        }

        private void grid1_CellDoubleClick(object sender, GridCellClickEventArgs e)
        {
            grid1.CurrentCell.BeginEdit();
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            string curCmd = this.rtbCmd.Text;
            if (curCmd.Equals(preApplyCmd))
                return;
            preApplyCmd = curCmd;
            //两个grid点了确定之后就可以到这里，这只是完成了从两个grid得到rtbCmd的过程，应当还要支持直接修改rtbCmd然后反过去
            //生成两个grid的内容，也就是根据rtbCmd的text提取TopoBase.args和pSets的过程，这里暂时从略
        }

        private void btYes_Click(object sender, EventArgs e)
        {
            preApplyCmd = this.rtbCmd.Text;
            TopoResult result = curTopo.DoLayout();
            try
            {
                docForm.GenerateTopo(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("拓扑生成失败：" + ex);
            }
        }

    }
}
