using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using NetworkDesigner.Beans.Model;

namespace NetworkDesigner.UI.ToolWindow
{
    //we override the Model to create the create a new cell renderer for our control
    public class TreeCellModel : GridStaticCellModel
    {
        protected TreeCellModel(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public TreeCellModel(GridModel grid)
            : base(grid)
        {

        }

        //note that this method create our new derived renderer
        public override GridCellRendererBase CreateRenderer(GridControlBase control)
        {
            return new TreeCellRenderer(control, this);
        }
    }

    // handles drawing our indented cell
    public class TreeCellRenderer : GridStaticCellRenderer
    {
        private int _indentSize;
        public int bitmapWidth;
        public int bitmapHeight;

        public int IndentSize
        {
            get { return _indentSize; }
            set
            {
                if (value >= 0)
                    _indentSize = value;
                else
                    _indentSize = 0;
            }
        }
        public TreeCellRenderer(GridControlBase grid, GridCellModelBase cellModel)
            : base(grid, cellModel)
        {
            _indentSize = NetworkDesigner.Beans.Common.AppSetting.TreeGridIndentSize;
        }

        protected override void OnDraw(System.Drawing.Graphics g, System.Drawing.Rectangle clientRectangle, int rowIndex, int colIndex, Syncfusion.Windows.Forms.Grid.GridStyleInfo style)
        {
            if (clientRectangle.IsEmpty)
                return;

            ImageList imageList = style.ImageList;
            int indent = (int)style.Tag;

            Rectangle rect = GetCellBoundsCore(rowIndex, colIndex);
            int X = rect.X + indent * IndentSize;
            if (style.ImageIndex < imageList.Images.Count)
            {
                g.DrawImage((Bitmap)imageList.Images[style.ImageIndex], X, rect.Y);
            }
            X += imageList.ImageSize.Width + 2;

            //now draw text past the image...
            bool drawDisabled = false;
            string displayText = String.Empty;

            Rectangle textRectangle = RemoveMargins(clientRectangle, style);
            int shift = X - textRectangle.X;
            textRectangle.X = X;
            textRectangle.Width -= shift;
            if (textRectangle.IsEmpty)
                return;
            try
            {
                displayText = Model.GetFormattedOrActiveTextAt(rowIndex, colIndex, style);
            }
            catch
            {
                displayText = style.Text;
                //style.ToolTip = ex.Message;
                drawDisabled = true;
            }

            if (style.HasError)
            {
                displayText = style.Error;
                drawDisabled = true;
            }

            if (displayText.Length > 0)
            {
                Font font = style.Font.GdipFont;
                Color textColor = Grid.Model.Properties.BlackWhite ? Color.Black : style.TextColor;
                DrawText(g, displayText, font, textRectangle, style, textColor, drawDisabled);
            }
        }
        /// <summary>
        /// 根据缩进量判断是否有子节点，注意传入的colIndex应始终指向TreeNode所在列
        /// </summary>
        /// <returns></returns>
        public bool HasChildren(int rowIndex, int colIndex)
        {
            int curIndent = (int)Grid.Model[rowIndex, colIndex].Tag;
            if (rowIndex + 1 < this.Grid.Model.RowCount)
            {
                int nextIndent = (int)Grid.Model[rowIndex + 1, colIndex].Tag; ;
                if (nextIndent > curIndent)
                    return true;
            }
            
            return false;
        }
        /// <summary>
        /// 根据缩进量判断是否有父节点，注意传入的colIndex应始终指向TreeNode所在列</para>
        /// 若有返回父节点所在行，若没有返回 -1
        /// </summary>
        /// <returns></returns>
        public int HasParent(int rowIndex, int colIndex)
        {
            int curIndent = (int)Grid.Model[rowIndex, colIndex].Tag;
            int preIndent = 0;
            while (rowIndex > 1) //警告：这里要区分header行是否显示带来的影响，暂认为不会循环到header行
            {
                rowIndex = rowIndex - 1;
                preIndent = (int)Grid.Model[rowIndex, colIndex].Tag; ;
                if (preIndent < curIndent)
                    return rowIndex;
                if (preIndent > curIndent)
                    break;
            }

            return -1;
        }

        private void ClickTreeNode(int rowIndex, int colIndex, System.Windows.Forms.MouseEventArgs e)
        {
            Rectangle rect = GetCellBoundsCore(rowIndex, colIndex);
            GridStyleInfo gridInfo = this.Grid.Model[rowIndex, colIndex];
            int indent = (int)gridInfo.Tag;
            int X = rect.X + indent * IndentSize;
            
            rect.X = X;
            rect.Width = gridInfo.ImageList.ImageSize.Width;
            rect.Height = gridInfo.ImageList.ImageSize.Height;

            if (rect.Contains(new Point(e.X, e.Y)))
            {
                if (gridInfo.ImageIndex == (int)GridNodeState.Opened)
                {
                    if(HasChildren(rowIndex,colIndex))
                        gridInfo.ImageIndex = (int)GridNodeState.Closed;
                    else
                        gridInfo.ImageIndex = (int)GridNodeState.NoChildren;
                }
                else if (gridInfo.ImageIndex == (int)GridNodeState.Closed)
                {
                    if (HasChildren(rowIndex, colIndex))
                        gridInfo.ImageIndex = (int)GridNodeState.Opened;
                    else
                        gridInfo.ImageIndex = (int)GridNodeState.NoChildren;
                }
            }
        }

        protected override void OnMouseDown(int rowIndex, int colIndex, System.Windows.Forms.MouseEventArgs e)
        {
            //if (colIndex == 1) //因为这是在TreeGridCell上的OnMouseDown，因此其colIndex一直是1
            ClickTreeNode(rowIndex, colIndex, e);
            base.OnMouseDown(rowIndex, colIndex, e);
        }

        protected override int OnHitTest(int rowIndex, int colIndex, MouseEventArgs e, IMouseController controller)
        {
            if (e.Button != MouseButtons.None)
            {
                //return a cell hit so OnMouseDown will be called later
                return GridHitTestContext.Cell;
            }
            else
                return 0;
        }
    }
}
