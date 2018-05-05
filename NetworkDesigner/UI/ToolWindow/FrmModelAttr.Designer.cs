namespace NetworkDesigner.UI.ToolWindow
{
    partial class FrmModelAttr
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmModelAttr));
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle1 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle2 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle3 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle4 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridCellInfo gridCellInfo1 = new Syncfusion.Windows.Forms.Grid.GridCellInfo();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gridCtl = new Syncfusion.Windows.Forms.Grid.GridControl();
            this.panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gridCtl)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // gridCtl
            // 
            this.gridCtl.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.None;
            this.gridCtl.AllowSelection = ((Syncfusion.Windows.Forms.Grid.GridSelectionFlags)(((((((Syncfusion.Windows.Forms.Grid.GridSelectionFlags.Row | Syncfusion.Windows.Forms.Grid.GridSelectionFlags.Table) 
            | Syncfusion.Windows.Forms.Grid.GridSelectionFlags.Cell) 
            | Syncfusion.Windows.Forms.Grid.GridSelectionFlags.Multiple) 
            | Syncfusion.Windows.Forms.Grid.GridSelectionFlags.Shift) 
            | Syncfusion.Windows.Forms.Grid.GridSelectionFlags.Keyboard) 
            | Syncfusion.Windows.Forms.Grid.GridSelectionFlags.AlphaBlend)));
            this.gridCtl.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(94)))), ((int)(((byte)(171)))), ((int)(((byte)(222)))));
            this.gridCtl.BackColor = System.Drawing.Color.White;
            gridBaseStyle1.Name = "Header";
            gridBaseStyle1.StyleInfo.Borders.Bottom = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle1.StyleInfo.Borders.Left = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle1.StyleInfo.Borders.Right = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle1.StyleInfo.Borders.Top = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle1.StyleInfo.CellType = "Header";
            gridBaseStyle1.StyleInfo.Font.Bold = true;
            gridBaseStyle1.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(199)))), ((int)(((byte)(184))))), System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(234)))), ((int)(((byte)(216))))));
            gridBaseStyle1.StyleInfo.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridBaseStyle2.Name = "Standard";
            gridBaseStyle2.StyleInfo.Font.Facename = "Tahoma";
            gridBaseStyle2.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.SystemColors.Window);
            gridBaseStyle3.Name = "Column Header";
            gridBaseStyle3.StyleInfo.BaseStyle = "Header";
            gridBaseStyle3.StyleInfo.Borders.Bottom = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle3.StyleInfo.Borders.Left = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle3.StyleInfo.Borders.Right = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle3.StyleInfo.Borders.Top = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle3.StyleInfo.CellAppearance = Syncfusion.Windows.Forms.Grid.GridCellAppearance.Flat;
            gridBaseStyle3.StyleInfo.Clickable = true;
            gridBaseStyle3.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center;
            gridBaseStyle3.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.SystemColors.Control);
            gridBaseStyle3.StyleInfo.ShowButtons = Syncfusion.Windows.Forms.Grid.GridShowButtons.Show;
            gridBaseStyle3.StyleInfo.TextColor = System.Drawing.Color.Black;
            gridBaseStyle4.Name = "Row Header";
            gridBaseStyle4.StyleInfo.BaseStyle = "Header";
            gridBaseStyle4.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridBaseStyle4.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(199)))), ((int)(((byte)(184))))), System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(234)))), ((int)(((byte)(216))))));
            this.gridCtl.BaseStylesMap.AddRange(new Syncfusion.Windows.Forms.Grid.GridBaseStyle[] {
            gridBaseStyle1,
            gridBaseStyle2,
            gridBaseStyle3,
            gridBaseStyle4});
            this.gridCtl.ColCount = 2;
            this.gridCtl.ColWidthEntries.AddRange(new Syncfusion.Windows.Forms.Grid.GridColWidth[] {
            new Syncfusion.Windows.Forms.Grid.GridColWidth(0, 35),
            new Syncfusion.Windows.Forms.Grid.GridColWidth(1, 100)});
            this.gridCtl.DefaultColWidth = 100;
            this.gridCtl.DefaultRowHeight = 20;
            this.gridCtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCtl.Font = new System.Drawing.Font("宋体", 10F);
            this.gridCtl.ForeColor = System.Drawing.Color.Black;
            gridCellInfo1.Col = -1;
            gridCellInfo1.Row = -1;
            gridCellInfo1.StyleInfo.Font.Bold = false;
            gridCellInfo1.StyleInfo.Font.Facename = "宋体";
            gridCellInfo1.StyleInfo.Font.Italic = false;
            gridCellInfo1.StyleInfo.Font.Size = 10F;
            gridCellInfo1.StyleInfo.Font.Strikeout = false;
            gridCellInfo1.StyleInfo.Font.Underline = false;
            gridCellInfo1.StyleInfo.Font.Unit = System.Drawing.GraphicsUnit.Point;
            this.gridCtl.GridCells.AddRange(new Syncfusion.Windows.Forms.Grid.GridCellInfo[] {
            gridCellInfo1});
            this.gridCtl.Location = new System.Drawing.Point(0, 0);
            this.gridCtl.Name = "gridCtl";
            this.gridCtl.Properties.GridLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.gridCtl.Properties.PrintColHeader = false;
            this.gridCtl.Properties.ResizingCellsLinesColor = System.Drawing.Color.Blue;
            this.gridCtl.Properties.RowHeaders = false;
            this.gridCtl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gridCtl.RowCount = 5;
            this.gridCtl.RowHeightEntries.AddRange(new Syncfusion.Windows.Forms.Grid.GridRowHeight[] {
            new Syncfusion.Windows.Forms.Grid.GridRowHeight(0, 21)});
            this.gridCtl.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeIntoCode;
            this.gridCtl.ShowRowHeaders = false;
            this.gridCtl.Size = new System.Drawing.Size(373, 370);
            this.gridCtl.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.gridCtl.SmartSizeBox = false;
            this.gridCtl.TabIndex = 2;
            this.gridCtl.UseRightToLeftCompatibleTextBox = true;
            this.gridCtl.ResizingColumns += new Syncfusion.Windows.Forms.Grid.GridResizingColumnsEventHandler(this.gridCtl_ResizingColumns);
            this.gridCtl.CellDoubleClick += new Syncfusion.Windows.Forms.Grid.GridCellClickEventHandler(this.gridCtl_CellDoubleClick);
            this.gridCtl.CurrentCellKeyUp += new System.Windows.Forms.KeyEventHandler(this.gridCtl_CurrentCellKeyUp);
            this.gridCtl.SizeChanged += new System.EventHandler(this.gridCtl_SizeChanged);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.gridCtl);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(373, 370);
            this.panel.TabIndex = 3;
            // 
            // FrmModelAttr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(373, 370);
            this.Controls.Add(this.panel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Name = "FrmModelAttr";
            this.Text = "FrmModelAttr";
            ((System.ComponentModel.ISupportInitialize)(this.gridCtl)).EndInit();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Grid.GridControl gridCtl;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel;

    }
}