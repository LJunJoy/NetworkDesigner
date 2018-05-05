using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Syncfusion.Windows.Forms.Grid;
using NetworkDesigner.Beans.Model;
using NetworkDesigner.Service.Model;
using NetworkDesigner.UI.Document;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Diagram.Controls;

namespace NetworkDesigner.UI.ToolWindow
{
    public partial class FrmTreeGrid : FrmBase
    {
        static public int NoChildBMP = 2;
        static public int OpenedBMP = 0;
        static public int ClosedBMP = 1;
        //sample data members...
        public static TreeGridDataSource externalData;
        private StringCollection gridDataType;
        private StringCollection gridCategory;
        private FrmSymbol frmSymbol;
        /// <summary>
        /// 现在的顶级属性名-属性原始名称
        /// </summary>
        private Dictionary<string, string> propertyChange;

        public FrmTreeGrid(FrmMain _frmMain)
        {
            InitializeComponent();

            this.mainForm = _frmMain;
            gridDataType = new StringCollection();
            gridDataType.AddRange(GDataType.dt_En.ToArray());
            gridCategory = new StringCollection();

            GridSettings();
        }

        /// <summary>
        /// Grid settings for better look and feel. 
        /// </summary>
        private void GridSettings()
        {
            //Add a custom cell control
            gridControl1.CellModels.Add("TreeCell", new TreeCellModel(gridControl1.Model));
            gridControl1.ControllerOptions = GridControllerOptions.All & (~GridControllerOptions.OleDataSource);
            //设计器中设置不显示行和列的header
            //Hides A, B, C in the column headers.
            gridControl1.Model.Options.NumberedColHeaders = false;
            //Hides 1, 2, 3 in the row headers.
            gridControl1.Model.Options.NumberedRowHeaders = false;
            //Enable Pixel Scrolling
            gridControl1.VScrollPixel = true;//滚动条出现时基于像素点而不是cell-width滑动，更合适
            gridControl1.HScrollPixel = true;
            gridControl1.RowStyles[1].Borders.Top = new
            GridBorder(GridBorderStyle.Dotted, Color.DimGray, GridBorderWeight.Thin);
            gridControl1.ColStyles[1].Borders.Left = new
            GridBorder(GridBorderStyle.Dotted, Color.DimGray, GridBorderWeight.Thin);

            gridControl1.ColCount = 7; //不包含col_header列的列数
            //gridControl1.RowHeights[0] = 4;
            //gridControl1.ColWidths[0] = 3;//不显示各行的header，但要显示边框
            gridControl1.ColWidths[7] = 200;
            foreach (GridStyleInfo style in this.gridControl1.ColStyles)
            {
                style.TextColor = Color.Black;
            }
            //make the imagelist available thru the tablestyle
            gridControl1.TableStyle.ImageList = this.imageList1;
            gridControl1.TableStyle.Trimming = StringTrimming.EllipsisCharacter;
            //tab key navigation set as false to move the next control
            gridControl1.WantTabKey = false;

            externalData = new TreeGridDataSource();
            gridControl1.ChangeSelectionState(0, 0, _gridRangeInfo);

            gridControl1.ResetVolatileData(); //重置缓存数据
            gridControl1.QueryCellInfo += new GridQueryCellInfoEventHandler(GridQueryCellInfo);
            gridControl1.QueryRowCount += new GridRowColCountEventHandler(GridQueryRowCount);
            gridControl1.QueryColCount += new GridRowColCountEventHandler(GridQueryColCount);
            //gridControl1.QueryColWidth += new GridRowColSizeEventHandler(GridQueryColWidth);

            //handle saving data back to the data source...
            gridControl1.SaveCellInfo += new GridSaveCellInfoEventHandler(GridSaveCellInfo);
        }
        #region Grid Events
        void GridSaveCellInfo(object sender, GridSaveCellInfoEventArgs e) //从cell保存到内存
        {
            if (e.ColIndex <= 0 || e.RowIndex <= 0)
                return;

            int col0 = e.ColIndex - 1; //从0计的非header列数
            int row0 = e.RowIndex - 1; //从0计的非header行数
            if (row0 >= externalData.RowCountView)
                return;
            string str = "";
            GridCellData gridData = externalData[row0];
            switch (col0) //我们认为，同一列的cell是同一类
            {
                case FactoryGridData.iRowPath:
                    gridData.RowPath = (string)e.Style.CellValue;
                    gridData.IndentLevel = (int)e.Style.Tag;
                    gridData.ExpandState = e.Style.ImageIndex;
                    break;
                case FactoryGridData.iName:
                    str = (string)e.Style.CellValue;
                    if (gridData.IndentLevel != 0)
                    {
                        str = gridData.Name;
                        e.Style.CellValue = str;
                        MessageBox.Show("不允许直接修改引用模型的属性");
                    }
                    else //记录修改记录，供保存时查询 
                    {
                        if(propertyChange.ContainsKey(gridData.Name))
                        {
                            string origion = propertyChange[gridData.Name];
                            propertyChange.Remove(gridData.Name);
                            propertyChange[str] = origion;
                        }
                    }
                    gridData.Name = str;
                    break;
                case FactoryGridData.iType:
                    str = (string)e.Style.CellValue;
                    if (gridData.Dtype.Equals(GDataType.Model_En) && !str.Equals(GDataType.Model_En))
                    {
                        str = GDataType.Model_En; //不允许修改子模型属性
                        e.Style.CellValue = str;
                        MessageBox.Show("不允许修改类别为模型的属性类型");
                    }
                    else if (!gridData.Dtype.Equals(GDataType.Model_En) && str.Equals(GDataType.Model_En))
                    {
                        str = gridData.Dtype;
                        e.Style.CellValue = str;
                        MessageBox.Show("不允许将属性类型从非模型修改为模型");
                    }
                    gridData.Dtype = str;
                    break;
                case FactoryGridData.iDisName:
                    gridData.DisName = (string)e.Style.CellValue;
                    break;
                case FactoryGridData.iDisCategory:
                    gridData.DisCategory = (string)e.Style.CellValue;
                    break;
                case FactoryGridData.iDisIsShow:
                    gridData.DisIsShow = (string)e.Style.CellValue;
                    break;
                case FactoryGridData.iDefaultValue:
                    CheckGridCellValue(e.Style, gridData);
                    gridData.DefValue = (string)e.Style.CellValue;
                    break;
                default:
                    break;
            }
            
            //refresh this row so change is displayed
            externalData.RefreshDisplayData();
            this.gridControl1.Refresh();
            e.Handled = true;
        }
        private bool CheckGridCellValue(GridStyleInfo eStyle, GridCellData gridData)
        {
            bool result = true;
            string str = ((string)eStyle.CellValue).Trim();
            switch (gridData.Dtype)
            {
                case GDataType.Int_En:
                    try 
                    {
                        if (str.EndsWith(","))
                        {
                            str = str.Trim(',');
                            eStyle.CellValue = str;
                        }
                        int.Parse(str);
                    }
                    catch (Exception) 
                    {
                        MessageBox.Show("请输入有效的整数");
                        eStyle.CellValue = gridData.DefValue;
                        result = false;
                    }
                    break;
                case GDataType.Double_En:
                    try
                    {
                        if (str.EndsWith(","))
                        {
                            str = str.Trim(',');
                            eStyle.CellValue = str;
                        }
                        double.Parse(str);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("请输入有效的数字");
                        eStyle.CellValue = gridData.DefValue;
                        result = false;
                    }
                    break;
                case GDataType.ListDouble_En:
                    try
                    {
                        if (str.EndsWith(","))
                        {
                            str = str.Trim(',');
                            eStyle.CellValue = str;
                        }
                        ConvertHelper.ListDoubleConverter(str);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("请输入以逗号分隔的数字序列");
                        eStyle.CellValue = gridData.DefValue;
                        result = false;
                    }
                    break;
                case GDataType.ListInt_En:
                    try
                    {
                        if (str.EndsWith(","))
                        {
                            str = str.Trim(',');
                            eStyle.CellValue = str;
                        }
                        ConvertHelper.ListIntConverter(str);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("请输入以逗号分隔的整数序列");
                        eStyle.CellValue = gridData.DefValue;
                        result = false;
                    }
                    break;
                case GDataType.ListString_En:
                    try
                    {
                        ConvertHelper.ListStringConverter(str);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("请输入以{}分隔的字符串序列");
                        eStyle.CellValue = gridData.DefValue;
                        result = false;
                    }
                    break;
                case GDataType.Model_En:
                    if (!str.Equals(gridData.DefValue))
                    {
                        MessageBox.Show("不允许直接修改引用模型的名称");
                        eStyle.CellValue = gridData.DefValue;
                        result = false;
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
        void GridQueryRowCount(object sender, GridRowColCountEventArgs e)
        {
            e.Count = externalData.RowCountView;
            e.Handled = true;
        }

        void GridQueryColCount(object sender, GridRowColCountEventArgs e)
        {
            e.Count = externalData.ColCount;
            e.Handled = true;
        }
        void GridQueryCellInfo(object sender, GridQueryCellInfoEventArgs e)
        {
            if (e.RowIndex <= 0 || e.ColIndex <= 0)
                return;

            int col0 = e.ColIndex - 1; //从0计的非header列数
            int row0 = e.RowIndex - 1; //从0计的非header行数
            if (row0 >= externalData.RowCountView)
                return;

            GridCellData gridData = externalData[row0];
            switch(col0) //我们认为，同一列的cell是同一类
            {
                case FactoryGridData.iRowPath:
                    e.Style.CellType = "TreeCell";
                    e.Style.CellValue = gridData.RowPath;
                    e.Style.Tag = gridData.IndentLevel;
                    e.Style.ImageIndex = (int)gridData.ExpandState;
                    break;
                case FactoryGridData.iName:
                    e.Style.CellValue = gridData.Name;
                    break;
                case FactoryGridData.iType:
                    e.Style.CellType = GridCellTypeName.ComboBox;
                    e.Style.CellValue = gridData.Dtype;
                    e.Style.ChoiceList = this.gridDataType;
                    break;
                case FactoryGridData.iDisName:
                    e.Style.CellValue = gridData.DisName;
                    break;
                case FactoryGridData.iDisCategory:
                    e.Style.CellType = GridCellTypeName.ComboBox;
                    e.Style.CellValue = gridData.DisCategory;
                    e.Style.ChoiceList = this.gridCategory;
                    break;
                case FactoryGridData.iDisIsShow:
                    e.Style.CellType = GridCellTypeName.CheckBox;
                    e.Style.CellValue = gridData.DisIsShow;
                    e.Style.Description = "显示";
                    e.Style.CheckBoxOptions.CheckedValue = "y";
                    e.Style.CheckBoxOptions.UncheckedValue = "n";
                    break;
                case FactoryGridData.iDefaultValue:
                    e.Style.CellValue = gridData.DefValue;
                    break;
                default:
                    break;
            }
            e.Handled = true;
        }
        #endregion
        private void gridControl1_CellDoubleClick(object sender, GridCellClickEventArgs e)
        {
            gridControl1.CurrentCell.BeginEdit();
        }

        public void BeginDesignModel(FrmSymbol frm)
        {
            this.frmSymbol = frm;
            ModelInfo model = frmSymbol.DesignModel;
            propertyChange = new Dictionary<string, string>();
            List<GridCellData> datas = new List<GridCellData>(model.properties.Count);
            UpdateGridData(datas, 0, 0, 1, model);
            foreach (var data in datas)
            {
                if (data.IndentLevel == 0) //顶级属性的所有权是属于模型本身，当它的name修改后，要更新该模型的使用者
                {
                    propertyChange[data.Name] = data.Name;
                }
            }
            this.gridControl1.BeginUpdate();
            externalData.data = datas;
            externalData.RefreshDisplayData();
            this.gridControl1.EndUpdate();

            this.gridControl1.Refresh();
            if (externalData.RowCountView > 0)
                gridControl1.ChangeSelectionState(1, 1, _gridRangeInfo);
            else
                gridControl1.ChangeSelectionState(0, 1, _gridRangeInfo);
            this.Show();
        }

        /// <summary>
        /// 更新datas数据列表，其他参数为待插入行的索引、缩进量以及行标识，返回插入的行数
        /// </summary>
        public int UpdateGridData(List<GridCellData> datas, int index, int indent, int rowPath, ModelInfo model)
        {
            int index0 = index;
            Dictionary<string, MyProperty> properties = model.properties;
            GridCellData data;
            foreach (MyProperty property in properties.Values)
            {
                switch (property.type)
                {
                    case GDataType.Double_En:
                    case GDataType.Int_En:
                    case GDataType.String_En:
                        //单行显示
                        data = GetGridCellData0(property, indent, rowPath++,property.property.ToString());
                        datas.Insert(index, data);
                        index++;
                        break;
                    case GDataType.ListDouble_En:
                        data = GetGridCellData0(property, indent, rowPath++,
                            ConvertHelper.ListDoubleConverter(property.property as List<double>));
                        datas.Insert(index, data);
                        index++;
                        break;
                    case GDataType.ListInt_En:
                        data = GetGridCellData0(property, indent, rowPath++,
                            ConvertHelper.ListIntConverter(property.property as List<int>));
                        datas.Insert(index, data);
                        index++;
                        break;
                    case GDataType.ListString_En:
                        data = GetGridCellData0(property, indent, rowPath++,
                            ConvertHelper.ListStringConverter(property.property as List<string>));
                        datas.Insert(index, data);
                        index++;
                        break;
                    //case GDataType.Model_En:
                    //    SimModel curModel = (ModelInfo)property.property;
                    //    SimModel defModel = ModelInfoHelper.FindModelInfo(property.refModel);
                    //    if (defModel == null)
                    //    {
                    //        MessageBox.Show(model.ModelName + "的属性项未加载：" + property.name
                    //                    + "\n未找到引用模型：" + property.refModel);
                    //        break;
                    //    }
                    //    else //注意深拷贝后才能修改，这里只作只读
                    //    {
                    //        curModel.UpdateDefaultInfo(defModel);
                    //    }
                    //    //先显示model行，然后缩进显示其property行
                    //    data = GetGridCellData0(property, indent, rowPath++,property.refModel);
                    //    datas.Insert(index, data);
                    //    index++;
                    //    index += UpdateGridData(datas, index, indent + 1, 1, curModel);
                    //    break;
                    //case GDataType.ListModel_En:
                        //多行显示
                        //break;
                    default:
                        break;
                }
            }
            return index - index0;
        }

        public GridCellData GetGridCellData0(MyProperty property, int indent, int rowPath, string defValue)
        {
            GridCellData data = new GridCellData();
            data.DefValue = defValue;
            data.DisCategory = property.disCategory;
            data.DisIsShow = property.isDisplay;
            data.DisName = property.display;
            data.Dtype = property.type;
            data.Name = property.name;
            data.IndentLevel = indent;
            data.ExpandState = GridNodeState.NoChildren;
            data.RowPath = rowPath.ToString();
            
            return data;
        }

        /// <summary>
        /// 从index行开始读datas得到model的property，返回本次调用读了几行
        /// </summary>
        /// <param name="model"></param>
        /// <param name="datas"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetModelProperty(ModelInfo model, List<GridCellData> datas, int index, string modelPath="")
        {
            if (index >= datas.Count)
                return 0;
            int index0 = index;
            int indent0 = datas[index].IndentLevel;
            int indent;
            MyProperty prop;
            GridCellData data;
            while (true) //indent相同的都是此model的属性，一直要读到indent不相等
            {
                if (index >= datas.Count)
                    break;
                data = datas[index];
                indent = data.IndentLevel;
                if (indent != indent0)
                    break;
                prop = new MyProperty();
                prop.disCategory = data.DisCategory;
                prop.isDisplay = data.DisIsShow;
                prop.display = data.DisName;
                prop.type = data.Dtype;
                prop.name = data.Name;
                if (modelPath.Length == 0)
                    prop.path = prop.name;
                else
                    prop.path = modelPath + "." + prop.name;
                switch (data.Dtype)
                {
                    case GDataType.Double_En:
                        prop.property = double.Parse(data.DefValue);
                        model.properties[prop.name] = prop;
                        index++;
                        break;
                    case GDataType.Int_En:
                        prop.property = int.Parse(data.DefValue);
                        model.properties[prop.name] = prop;
                        index++;
                        break;
                    case GDataType.String_En:
                        prop.property = data.DefValue;
                        model.properties[prop.name] = prop;
                        index++;
                        break;
                    case GDataType.ListInt_En:
                        prop.property = ConvertHelper.ListIntConverter(data.DefValue);
                        model.properties[prop.name] = prop;
                        index++;
                        break;
                    case GDataType.ListDouble_En:
                        prop.property = ConvertHelper.ListDoubleConverter(data.DefValue);
                        model.properties[prop.name] = prop;
                        index++;
                        break;
                    case GDataType.ListString_En:
                        prop.property = ConvertHelper.ListStringConverter(data.DefValue);
                        model.properties[prop.name] = prop;
                        index++;
                        break;
                    case GDataType.Model_En: //接下来几行是子模型的属性，缩进量也必将增大
                        prop.refModel = data.DefValue;
                        model.properties[prop.name] = prop;
                        index++;
                        ModelInfo curModel = new ModelInfo();
                        prop.property = curModel;
                        index += GetModelProperty(curModel, datas, index, prop.path); //递归加载model的property
                        break;
                    default:
                        break;
                }
            }
            return index - index0;
        }
        /// <summary>
        /// 将GridData全部的行同步到ModelInfo
        /// </summary>
        public void SaveGridData()
        {
            this.gridControl1.BeginUpdate();
            this.frmSymbol.DesignModel.properties.Clear();
            int index=0;
            while (index < externalData.data.Count)
            {
                index += GetModelProperty(frmSymbol.DesignModel, externalData.data, index);
            }
            frmSymbol.DesignModel.UpdateDisplayProps();
            this.gridControl1.EndUpdate();
        }

        private void tsbAddProperty_Click(object sender, EventArgs e)
        {
            int curRow = gridControl1.CurrentCell.RowIndex;
            int curCol = gridControl1.CurrentCell.ColIndex;
            
            int row0 = curRow - 1; //从0计的非header行数
            if (row0 < 0)
            {
                curRow = 0;
                row0 = 0;
            }
            else if(externalData[row0].IndentLevel != 0)
            {
                MessageBox.Show("不允许增删引用模型的属性项");
                return;
            }
            if (curCol < 1)
                curCol = 1;

            int nextSelectRow = FactoryGridData.GetNextLevel0(externalData.data, row0);
            if (nextSelectRow == -1)
                nextSelectRow = externalData.RowCountTotal;
            this.gridControl1.BeginUpdate();

            string rowPath = (nextSelectRow + 1).ToString();
            string name = "";
            externalData.data.Insert(nextSelectRow, FactoryGridData.NewGridCellData(rowPath, name));//总是插入在cruRow之后
            FactoryGridData.UpdateRowPath(externalData.data, row0);
            externalData.RefreshDisplayData();

            this.gridControl1.EndUpdate();

            gridControl1.Refresh(); //经测试在refresh gridControl之后的changeSelectionState更可靠！！
            gridControl1.ChangeSelectionState(nextSelectRow + 1, curCol, _gridRangeInfo);//一定要注意增删行后要更新selection，不然出错
        }

        private void tsbDeleteProperty_Click(object sender, EventArgs e)
        {
            int curRow = gridControl1.CurrentCell.RowIndex; //为0的是设置为不可见的header行
            int curCol = gridControl1.CurrentCell.ColIndex;
            if (curCol != 1 || curRow < 1)
                return;
            int row0 = curRow - 1; //从0计的非header行数
            GridCellData gridData = externalData[row0];
            if (gridData.IndentLevel != 0)
            {
                MessageBox.Show("非顶级属性项应在其对应的模型中删除");
                return;
            }
            if (GDataType.Model_En.Equals(gridData.Dtype))
            {
                MessageBox.Show("模型属性项应在模型编辑器中删除对应模块");
                return;
            }

            this.gridControl1.BeginUpdate();

            if (propertyChange.ContainsKey(gridData.Name))
            {
                string origion = propertyChange[gridData.Name];
                propertyChange.Remove(gridData.Name); //这里认为被删除的属性行总是第一行为顶级属性
                propertyChange[origion] = "!deleted!";
            }

            List<GridCellData> removed = FactoryGridData.RemoveRowGrid(externalData.data, row0);
            FactoryGridData.UpdateRowPath(externalData.data, row0);
            externalData.RefreshDisplayData();

            this.gridControl1.EndUpdate();

            gridControl1.Refresh();

            int nextSelectRow = 0;
            if (row0 == 0) //从第一行删除的并且删掉之后还有可见行
            {
                if (externalData.data.Count > 0) //注意此时已经执行了RemoveRowGrid
                    nextSelectRow = 1;  //还有未删除的行，因此继续选中可见行
                //如果可见行删完了则是选中0行即header行，下次继续点删除时会判断到从而避免删除
            }
            else if (row0 == externalData.data.Count) //从最后一行可见行开始删除的
                nextSelectRow = externalData.data.Count; //注意heaer用的下标是0，可见行从1计算
            else
                nextSelectRow = curRow;
            gridControl1.ChangeSelectionState(nextSelectRow, curCol, _gridRangeInfo);//一定要注意增删行后要更新selection，不然出错
        }
        private GridRangeInfo[] _gridRangeInfo = new GridRangeInfo[] { GridRangeInfo.Empty };
        private void tsbMoveUp_Click(object sender, EventArgs e)
        {
            int curRow = gridControl1.CurrentCell.RowIndex;
            int curCol = gridControl1.CurrentCell.ColIndex;
            if (curCol != 1 || curRow <= 1)
                return;
            int row0 = curRow - 1; //从0计的非header行数
            //GridCellData gridData = externalData[row0];
            //if (gridData.IndentLevel != 0)
            //{
            //    MessageBox.Show("非顶级属性项应在其对应的模型中调整顺序");
            //    return;
            //}
            int preRow = FactoryGridData.GetPreBrother(externalData.data, row0);
            if (preRow == -1)
                return;

            this.gridControl1.BeginUpdate();

            var removed = FactoryGridData.RemoveRowGrid(externalData.data, row0);
            FactoryGridData.InsertRowGrid(externalData.data, removed, preRow);
            FactoryGridData.UpdateRowPath(externalData.data, preRow);
            externalData.RefreshDisplayData();

            this.gridControl1.EndUpdate();

            gridControl1.Refresh();
            gridControl1.ChangeSelectionState(preRow + 1, curCol, _gridRangeInfo);//一定要注意增删行后要更新selection，不然出错
        }

        private void tsbLoadRefModel_Click(object sender, EventArgs e)
        {
            //直接在加载时显示父模型的全部属性，不用在这里手动显示
        }

        private void tsbAcceptModify_Click(object sender, EventArgs e)
        {
            if (this.gridControl1.CurrentCell.IsEditing)
                gridControl1.CurrentCell.EndEdit();
            SaveGridData();
            frmSymbol.UpdateDesignModelNode();
            //ModelInfoHelper.UpdateModelInfo(frmSymbol.DesignModel.ModelID, frmSymbol.DesignModel);
            //说明：这里只是更新那些已经存在于模型库中的模型，如果是新建模型未提交到面板则不会更新，在添加到面板时更新

            //todo: 基本模型结构改变时提示更新其他引用它的模型
            if (this.frmSymbol.DesignModel.ModelID.Length == 0)
                return;
            Dictionary<string, string> changed = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> ss in propertyChange)
            {
                if(!ss.Key.Equals(ss.Value))
                    changed[ss.Value] = ss.Key;
            }
            propertyChange.Clear();
            if (changed.Count == 0)
                return;
            
            bool isEdit = false;
            List<SimModel> lmi = null;
            string newName = "";
            SimModel subm = null;
            //bool isNewModel = false;
            //FrmDocBase docfrm = null;
            //Diagram diagram = null;
            foreach (KeyValuePair<string, List<SimModel>> patModel in ModelInfoHelper.patModels)
            {
                isEdit = false;
                lmi = patModel.Value;

                foreach (SimModel mi in lmi) //模型自己是不会引用自己的，所以可以完全遍历
                {
                    foreach (SimAttr m in mi.attrs.Values)
                    {
                        if (m.type.Equals(GDataType.Model_En) && m.refModel.Equals(frmSymbol.DesignModel.ModelID))
                        {
                            subm = (SimModel)(m.data);
                            //isNewModel = false;
                            foreach (string sk in subm.attrs.Keys)
                            {
                                if (changed.ContainsKey(sk))
                                {
                                    newName = changed[sk];
                                    if (newName.Equals("!deleted!"))
                                        subm.attrs.Remove(sk);
                                    else
                                        subm.attrs[sk].name = newName;
                                    isEdit = true;
                                    //isNewModel = true;
                                }
                            }
                            //if (isNewModel) //模型已更新，更新那些已经放置在编辑器上的模型，代价略大
                            //{
                            //    //从这里可以看出，更新模型时会影响使用旧模型的模型或场景，尤其是删除属性
                            //    foreach (WeifenLuo.WinFormsUI.Docking.DockContent n in mainForm.MainDock.Documents)
                            //    {
                            //        docfrm = n as FrmDocBase;
                            //        if (docfrm != null)
                            //        {
                            //            diagram = docfrm.GetActiveDiagram();
                            //            if (diagram != null)
                            //            {
                            //                foreach (Node node in diagram.Model.Nodes)
                            //                {
                            //                    if (node.Tag is ModelInfo)
                            //                    {
                            //                        ((ModelInfo)node.Tag).UpdateRefModelProperty(modelDesign.ModelID, changed);
                            //                    }
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                        }
                    }
                }
                if (isEdit)
                {
                    mainForm.m_toolBox.SetPaletteModifyByText(patModel.Key);
                }
            }
        }

    }
}
