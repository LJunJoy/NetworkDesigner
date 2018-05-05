using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetworkDesigner.Beans.Model;

namespace NetworkDesigner.UI.ToolWindow
{
    public class TreeModelAttr
    {
        /// <summary>
        /// 底层列表，存储用于显示的行数据
        /// </summary>
        public List<SimAttr> data; //使用列表是因为目前程序中用到的行数不会很多，所以暂不考虑增删效率问题
        /// <summary>
        /// 用于快速访问的数组，由底层链表_data通过toArray得到
        /// </summary>
        private ArrayList visibleRows;
        public void Reset()
        {
            data.Clear();
            visibleRows.Clear();
        }
        public TreeModelAttr()
        {
            data = new List<SimAttr>();
            visibleRows = new ArrayList();
        }

        public SimAttr this[int i]
        {
            get
            {
                return data[(int)visibleRows[i]];
            }
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
        //TreeGrid控件的Column数，不包括rol_header
        public int ColCount
        {
            get { return 2; }
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
    }
}
