using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDesigner.Beans.Model
{
    public class GridNodeState
    {
        public const int Opened = 0;
        public const int Closed = 1;
        public const int NoChildren = 2;
    }

    public enum ControlType
    {
        label,textbox,combox,combox_noEdit,checkbox
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public class GDataType
    {
        public const float MinFloatX = -1000000;

        public const string Int_En = "int";
        //public const string Int_Ch = "整数";
        public const string Double_En = "double";
        //public const string Double_Ch = "浮点数";
        public const string String_En = "string";
        //public const string String_Ch = "字符串";
        public const string Model_En = "model";
        //public const string Model_Ch = "模型";

        public const string ListInt_En = "list_int";
        //public const string ListInt_Ch = "整数集合";
        public const string ListDouble_En = "list_double";
        //public const string ListDouble_Ch = "浮点数集合";
        public const string ListString_En = "list_string";
        //public const string ListString_Ch = "字符串集合";
        public const string ListModel_En = "list_model";
        //public const string ListModel_Ch = "模型集合";

        public static List<string> dt_En;
        //public static List<string> dt_Ch;
        public static Dictionary<string, Type> dtMap;
        static GDataType()
        {
            dt_En = new List<string>();
            dt_En.Add(Int_En);
            dt_En.Add(Double_En);
            dt_En.Add(String_En);
            dt_En.Add(ListModel_En);
            //dt_En.Add(ListInt_En);
            //dt_En.Add(ListDouble_En);
            dt_En.Add(ListString_En);
            dt_En.Add(Model_En);
            
            //dt_Ch = new List<string>();
            //dt_Ch.Add(Int_Ch);
            //dt_Ch.Add(Double_Ch);
            //dt_Ch.Add(String_Ch);
            //dt_Ch.Add(ListModel_Ch);
            //dt_Ch.Add(ListInt_Ch);
            //dt_Ch.Add(ListDouble_Ch);
            //dt_Ch.Add(ListString_Ch);
            //dt_Ch.Add(Model_Ch);

            dtMap = new Dictionary<string, Type>();
            dtMap.Add(Int_En, typeof(int));
            //dtMap.Add(Int_Ch, typeof(int));
            dtMap.Add(Double_En, typeof(double));
            //dtMap.Add(Double_Ch, typeof(double));
            dtMap.Add(String_En, typeof(string));
            //dtMap.Add(String_Ch, typeof(string));
            dtMap.Add(Model_En, typeof(ModelInfo));
            //dtMap.Add(Model_Ch, typeof(ModelInfo));

            dtMap.Add(ListInt_En, typeof(List<int>));
            //dtMap.Add(ListInt_Ch, typeof(List<int>));
            dtMap.Add(ListDouble_En, typeof(List<double>));
            //dtMap.Add(ListDouble_Ch, typeof(List<double>));
            dtMap.Add(ListString_En, typeof(List<string>));
            //dtMap.Add(ListString_Ch, typeof(List<string>));
            //dtMap.Add(ListModel_En, typeof(List<ModelInfo>));
            //dtMap.Add(ListModel_Ch, typeof(List<ModelInfo>));
        }
        /// <summary>
        /// 判断num是否是预定义的值
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsDataDefMin(double num)
        {
            return Math.Abs(num - MinFloatX) < 0.00001;
        }

        public static bool IsDataDefNum(double data, int defNum)
        {
            return Math.Abs(data - defNum) < 0.00001;
        }
    }
    [Serializable]
    public class GridCellData
    {
        private int _expandState;
        private int _indentLevel;
        private string _rowPath;
        private string _name;
        private string _dtype;
        private string _disName;
        private string _disCategory;
        private string _disIsShow;
        private string _defValue;

        public int ExpandState
        {
            get { return _expandState; }
            set { _expandState = value; }
        }
        public int IndentLevel
        {
            get { return _indentLevel; }
            set { _indentLevel = value; }
        }
        /// <summary>
        /// 行的路径索引 1-1-2表示第一行的第一个子节点行的第2个子节点行
        /// </summary>
        public string RowPath
        {
            get { return _rowPath; }
            set { _rowPath = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Dtype
        {
            get { return _dtype; }
            set { _dtype = value; }
        }
        public string DisName
        {
            get { return _disName; }
            set { _disName = value; }
        }
        public string DisCategory
        {
            get { return _disCategory; }
            set { _disCategory = value; }
        }
        public string DisIsShow
        {
            get { return _disIsShow; }
            set { _disIsShow = value; }
        }
        public string DefValue
        {
            get { return _defValue; }
            set { _defValue = value; }
        }
    }

    public class FactoryGridData
    {
        /// <summary>
        /// TreeGrid控件的Column数，不包括rol_header，与GridCellData结构体保持一致
        /// </summary>
        public const int GridDataColsCount = 7;
        public const int iRowPath = 0;
        public const int iName = 1;
        public const int iType = 2;
        public const int iDisName = 3;
        public const int iDisCategory = 4;
        public const int iDisIsShow = 5;
        public const int iDefaultValue = 6;

        public static GridCellData NewGridCellData(string rowPath, string name)
        {
            GridCellData data = new GridCellData();

            data.IndentLevel = 0;
            data.ExpandState = GridNodeState.NoChildren;
            data.RowPath = rowPath;
            data.Name = name;
            data.Dtype = GDataType.String_En;

            data.DisCategory = "";
            data.DisName = data.Name;
            data.DisIsShow = "y";
            data.DefValue = "";
            return data;
        }

        public static GridCellData NewGridCellData(int indentLevel, string rowPath, string name, string dtype)
        {
            GridCellData data = new GridCellData();
            
            data.IndentLevel = indentLevel;
            data.ExpandState = GridNodeState.NoChildren;
            data.RowPath = rowPath;
            data.Name = name;
            data.Dtype = dtype;

            data.DisCategory = "";
            data.DisName = data.Name;
            data.DisIsShow = "y";
            data.DefValue = "";
            return data;
        }
        /// <summary>
        /// 刷新基准行的兄弟行的RowPath编号
        /// </summary>
        /// <param name="array"></param>
        public static void UpdateRowPath(List<GridCellData> array, int baseIndex)
        {
            if (baseIndex < 0 || baseIndex >= array.Count)
                return;
            //第0个元素的rowpath设置为1，之后+1
            int path = 1;
            int indent = array[baseIndex].IndentLevel;
            int start = baseIndex;
            for(;start > -1;start--)
            {
                if (array[start].IndentLevel < indent)
                    break;
            }
            if (start == -1)
                start = 0;
            for (int i=start;i<array.Count;i++)
            {
                if (array[i].IndentLevel == indent)
                {
                    array[i].RowPath = path.ToString();
                    path++;
                }
                else if (array[i].IndentLevel < indent)
                    break;
                //else continue
            }
        }
        /// <summary>
        /// 删除行及其子节点，并返回删除的行
        /// </summary>
        public static List<GridCellData> RemoveRowGrid(List<GridCellData> array, int index)
        {
            List<GridCellData> removed = new List<GridCellData>();
            if(index >= array.Count)
                return removed;
            int start = index;
            int count = 1;
            removed.Add(array[index]);
            int indent = array[index].IndentLevel;
            for (int i = start + 1; i < array.Count; i++)
            {
                if (array[i].IndentLevel > indent) //只有儿子节点才一块删除
                {
                    removed.Add(array[i]);
                    count++;
                }
                else
                    break;
            }
            array.RemoveRange(index, count);
            return removed;
        }

        /// <summary>
        /// 在指定行插入新行列表
        /// </summary>
        public static void InsertRowGrid(List<GridCellData> array, List<GridCellData> insert, int insertRow)
        {
            List<GridCellData> bakup = new List<GridCellData>(array);
            array.Clear();
            for (int i = 0; i < insertRow; i++)
                array.Add(bakup[i]);
            array.AddRange(insert);
            for (int i = insertRow; i < bakup.Count; i++)
                array.Add(bakup[i]);
            bakup.Clear();
        }

        /// <summary>
        /// 查找同层前一个兄弟节点的行号，找不到返回 -1
        /// </summary>
        public static int GetPreBrother(List<GridCellData> array, int index)
        {
            if (index >= array.Count)
                return -1;
            int indent = array[index].IndentLevel;
            int start = index - 1;
            for (; start >= 0; start--)
            {
                if (array[start].IndentLevel == indent)
                    return start;
                else if (array[start].IndentLevel > indent) //儿节点
                    continue;
                else //父节点
                    return -1;
            }
            return -1;
        }

        /// <summary>
        /// 查找下一个顶层属性的行号，找不到返回 -1
        /// </summary>
        public static int GetNextLevel0(List<GridCellData> array, int index)
        {
            if (index >= array.Count)
                return -1;
            for (int start = index + 1; start < array.Count; start++)
            {
                if (array[start].IndentLevel == 0)
                    return start;
            }
            return -1;
        }

        /// <summary>
        /// 查找同层后一个兄弟节点的行号，找不到返回 -1
        /// </summary>
        public static int GetNextBrother(List<GridCellData> array, int index)
        {
            if (index >= array.Count)
                return -1;
            int indent = array[index].IndentLevel;
            int start = index + 1;
            for (; start < array.Count; start++)
            {
                if (array[start].IndentLevel == indent)
                    return start;
                else if (array[start].IndentLevel > indent)
                    continue;
                else
                    return -1;
            }
            return -1;
        }

        public static bool SetGridCellData(GridCellData data, int index, string cellValue)
        {
            bool isSet = true;
            switch (index)
            {
                case iRowPath:
                    data.RowPath = cellValue;
                    break;
                case iName:
                    data.Name = cellValue;
                    break;
                case iType:
                    data.Dtype = cellValue;
                    break;
                case iDisName:
                    data.DisName = cellValue;
                    break;
                case iDisCategory:
                    data.DisCategory = cellValue;
                    break;
                case iDisIsShow:
                    data.DisIsShow = cellValue;
                    break;
                case iDefaultValue:
                    data.DefValue = cellValue;
                    break;
                default:
                    isSet = false;
                    break;
            }
            return isSet;
        }

        public static string GetGridCellData(GridCellData data, int index)
        {
            string result = "";
            switch (index)
            {
                case iRowPath:
                    result = data.RowPath;
                    break;
                case iName:
                    result = data.Name;
                    break;
                case iType:
                    result = data.Dtype;
                    break;
                case iDisName:
                    result = data.DisName;
                    break;
                case iDisCategory:
                    result = data.DisCategory;
                    break;
                case iDisIsShow:
                    result = data.DisIsShow.ToString();
                    break;
                case iDefaultValue:
                    result = data.DefValue;
                    break;
                default:
                    break;
            }
            return result;
        }

    }

    public class TreeGridDataSource
    {
        private int internalColCount;
        /// <summary>
        /// 底层列表，存储用于显示的行数据
        /// </summary>
        public List<GridCellData> data; //使用列表是因为目前程序中用到的行数不会很多，所以暂不考虑增删效率问题
        /// <summary>
        /// 用于快速访问的数组，由底层链表_data通过toArray得到
        /// </summary>
        private ArrayList visibleRows;

        public TreeGridDataSource()
        {
            internalColCount = FactoryGridData.GridDataColsCount;
            data = new List<GridCellData>();
            visibleRows = new ArrayList();
        }

        /// <summary>
        /// 全部行的个数
        /// </summary>
        public int RowCountTotal
        {
            get { return data.Count; }
        }

        /// <summary>
        /// 状态为显示的行的个数
        /// </summary>
        public int RowCountView
        {
            get { return visibleRows.Count; }
        }

        public int ColCount
        {
            get { return internalColCount; }
        }

        /// <summary>
        /// 填充测试数据
        /// </summary>
        /// <param name="rowCount"></param>
        public void InitTestData(int rowCount)
        {
            data = new List<GridCellData>(rowCount);
            for (int i = 0; i < rowCount; ++i)
            {
                int ii = i + 1;
                int jj = ii % 3;
                data.Add(FactoryGridData.NewGridCellData(jj,jj.ToString(),ii.ToString(), GDataType.Int_En));
            }
        }

        /// <summary>
        /// 从指定行开始检查行是否需要展开显示
        /// </summary>
        public void RefreshDisplayData(int start)
        {
            //decide which rows are visible and add then to visibleRows
            if (start > 0)
                visibleRows.RemoveRange(start, visibleRows.Count - start);
            else
                visibleRows = new ArrayList();

            int i = start;
            while (i < RowCountTotal)
            {
                i = ProcessNode(i);// will increment i at least once, maybe more
            }
        }
        /// <summary>
        /// 检查全部行是否需要展开显示
        /// </summary>
        public void RefreshDisplayData()
        {
            //decide which rows are visible and add then to visibleRows
            visibleRows = new ArrayList();

            int i = 0;
            while (i < RowCountTotal)
            {
                i = ProcessNode(i);// will increment i at least once, maybe more
            }
        }

        private int ProcessNode(int i)
        {
            if (i >= RowCountTotal)
                return i;

            int indent = data[i].IndentLevel;
            bool closed = (data[i].ExpandState == GridNodeState.Closed);
            //总是保证下一波的第一行可见，否则就无法展开了，这也说明
            //正确构造的data第一行总是intent=0且状态是非closed
            visibleRows.Add(i);

            //make sure parent is not a nocheck
            int k;
            if (visibleRows.Count > 1) //根据当前行与前一行的关系判定前一行是否是opened
            {
                k = (int)visibleRows[visibleRows.Count - 2]; //当前行的前一行
                if (data[k].ExpandState == GridNodeState.NoChildren
                    && indent > data[k].IndentLevel)
                {
                    data[k].ExpandState = GridNodeState.Opened;
                }
            }

            ++i;
            if (closed)
            {
                // since last node was not open, ignore all nodes indented further
                //当前行是关闭状态，那么缩进更远的，也就是当前行的子节点行，统统不显示
                while (i < RowCountTotal && data[i].IndentLevel > indent)
                    ++i;
                return i; // return & process next node at same level
            }

            // last node was open, so process all on same level
            while (i < RowCountTotal && data[i].IndentLevel == indent)
            {
                i = ProcessNode(i);
                return i;
            }

            return i;
        }
        public GridCellData this[int i]
        {
            get 
            {
                return data[(int)visibleRows[i]];
            }
        }

        public void ExpandAll()
        {
            for (int i = 0; i < RowCountTotal; ++i)
            {
                if (data[i].ExpandState == GridNodeState.Closed)
                    data[i].ExpandState = GridNodeState.Opened;
            }
        }

        public void CloseAll()
        {
            for (int i = 0; i < RowCountTotal; ++i)
            {
                if (data[i].ExpandState == GridNodeState.Opened)
                    data[i].ExpandState = GridNodeState.Closed;
            }
        }
    }
}
