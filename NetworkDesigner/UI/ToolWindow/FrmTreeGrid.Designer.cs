namespace NetworkDesigner.UI.ToolWindow
{
    partial class FrmTreeGrid
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
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle1 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle2 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle3 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle4 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridCellInfo gridCellInfo1 = new Syncfusion.Windows.Forms.Grid.GridCellInfo();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTreeGrid));
            this.tsbLoadDefModel = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.gridControl1 = new Syncfusion.Windows.Forms.Grid.GridControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsbAddProperty = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteProperty = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsbLoadRefModel = new System.Windows.Forms.ToolStripButton();
            this.tsbAcceptModify = new System.Windows.Forms.ToolStripButton();
            this.tsbLoadDefModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // tsbLoadDefModel
            // 
            this.tsbLoadDefModel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tsbLoadDefModel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.tsbAddProperty,
            this.tsbDeleteProperty,
            this.tsbMoveUp,
            this.tsbLoadRefModel,
            this.tsbAcceptModify});
            this.tsbLoadDefModel.Location = new System.Drawing.Point(0, 0);
            this.tsbLoadDefModel.Name = "tsbLoadDefModel";
            this.tsbLoadDefModel.Size = new System.Drawing.Size(649, 25);
            this.tsbLoadDefModel.TabIndex = 0;
            this.tsbLoadDefModel.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(380, 22);
            this.toolStripLabel1.Text = "展开标识，属性名称，类型，显示名称，显示类别，是否显示，默认值";
            this.toolStripLabel1.ToolTipText = "各列内容说明";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // gridControl1
            // 
            this.gridControl1.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.None;
            this.gridControl1.AllowSelection = ((Syncfusion.Windows.Forms.Grid.GridSelectionFlags)(((((((Syncfusion.Windows.Forms.Grid.GridSelectionFlags.Row | Syncfusion.Windows.Forms.Grid.GridSelectionFlags.Table) 
            | Syncfusion.Windows.Forms.Grid.GridSelectionFlags.Cell) 
            | Syncfusion.Windows.Forms.Grid.GridSelectionFlags.Multiple) 
            | Syncfusion.Windows.Forms.Grid.GridSelectionFlags.Shift) 
            | Syncfusion.Windows.Forms.Grid.GridSelectionFlags.Keyboard) 
            | Syncfusion.Windows.Forms.Grid.GridSelectionFlags.AlphaBlend)));
            this.gridControl1.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(94)))), ((int)(((byte)(171)))), ((int)(((byte)(222)))));
            this.gridControl1.BackColor = System.Drawing.SystemColors.Control;
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
            gridBaseStyle3.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center;
            gridBaseStyle3.StyleInfo.TextColor = System.Drawing.Color.Black;
            gridBaseStyle4.Name = "Row Header";
            gridBaseStyle4.StyleInfo.BaseStyle = "Header";
            gridBaseStyle4.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridBaseStyle4.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(199)))), ((int)(((byte)(184))))), System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(234)))), ((int)(((byte)(216))))));
            this.gridControl1.BaseStylesMap.AddRange(new Syncfusion.Windows.Forms.Grid.GridBaseStyle[] {
            gridBaseStyle1,
            gridBaseStyle2,
            gridBaseStyle3,
            gridBaseStyle4});
            this.gridControl1.ColCount = 6;
            this.gridControl1.ColWidthEntries.AddRange(new Syncfusion.Windows.Forms.Grid.GridColWidth[] {
            new Syncfusion.Windows.Forms.Grid.GridColWidth(0, 35),
            new Syncfusion.Windows.Forms.Grid.GridColWidth(1, 100)});
            this.gridControl1.DefaultColWidth = 100;
            this.gridControl1.DefaultRowHeight = 20;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Font = new System.Drawing.Font("宋体", 10F);
            this.gridControl1.ForeColor = System.Drawing.Color.Black;
            gridCellInfo1.Col = -1;
            gridCellInfo1.Row = -1;
            gridCellInfo1.StyleInfo.Font.Bold = false;
            gridCellInfo1.StyleInfo.Font.Facename = "宋体";
            gridCellInfo1.StyleInfo.Font.Italic = false;
            gridCellInfo1.StyleInfo.Font.Size = 10F;
            gridCellInfo1.StyleInfo.Font.Strikeout = false;
            gridCellInfo1.StyleInfo.Font.Underline = false;
            gridCellInfo1.StyleInfo.Font.Unit = System.Drawing.GraphicsUnit.Point;
            this.gridControl1.GridCells.AddRange(new Syncfusion.Windows.Forms.Grid.GridCellInfo[] {
            gridCellInfo1});
            this.gridControl1.Location = new System.Drawing.Point(0, 25);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Properties.ColHeaders = false;
            this.gridControl1.Properties.GridLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.gridControl1.Properties.RowHeaders = false;
            this.gridControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gridControl1.RowHeightEntries.AddRange(new Syncfusion.Windows.Forms.Grid.GridRowHeight[] {
            new Syncfusion.Windows.Forms.Grid.GridRowHeight(0, 21)});
            this.gridControl1.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeIntoCode;
            this.gridControl1.ShowColumnHeaders = false;
            this.gridControl1.ShowRowHeaders = false;
            this.gridControl1.Size = new System.Drawing.Size(649, 282);
            this.gridControl1.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.gridControl1.SmartSizeBox = false;
            this.gridControl1.TabIndex = 1;
            this.gridControl1.UseRightToLeftCompatibleTextBox = true;
            this.gridControl1.CellDoubleClick += new Syncfusion.Windows.Forms.Grid.GridCellClickEventHandler(this.gridControl1_CellDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // tsbAddProperty
            // 
            this.tsbAddProperty.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddProperty.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddProperty.Image")));
            this.tsbAddProperty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddProperty.Name = "tsbAddProperty";
            this.tsbAddProperty.Size = new System.Drawing.Size(23, 22);
            this.tsbAddProperty.Text = "toolStripButton1";
            this.tsbAddProperty.ToolTipText = "添加顶级属性项";
            this.tsbAddProperty.Click += new System.EventHandler(this.tsbAddProperty_Click);
            // 
            // tsbDeleteProperty
            // 
            this.tsbDeleteProperty.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDeleteProperty.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeleteProperty.Image")));
            this.tsbDeleteProperty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteProperty.Name = "tsbDeleteProperty";
            this.tsbDeleteProperty.Size = new System.Drawing.Size(23, 22);
            this.tsbDeleteProperty.Text = "toolStripButton1";
            this.tsbDeleteProperty.ToolTipText = "删除Tree选中的顶级属性";
            this.tsbDeleteProperty.Click += new System.EventHandler(this.tsbDeleteProperty_Click);
            // 
            // tsbMoveUp
            // 
            this.tsbMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveUp.Image")));
            this.tsbMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveUp.Name = "tsbMoveUp";
            this.tsbMoveUp.Size = new System.Drawing.Size(23, 22);
            this.tsbMoveUp.Text = "toolStripButton1";
            this.tsbMoveUp.ToolTipText = "在同层中向上移动Tree选中的属性";
            this.tsbMoveUp.Click += new System.EventHandler(this.tsbMoveUp_Click);
            // 
            // tsbLoadRefModel
            // 
            this.tsbLoadRefModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLoadRefModel.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadRefModel.Image")));
            this.tsbLoadRefModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadRefModel.Name = "tsbLoadRefModel";
            this.tsbLoadRefModel.Size = new System.Drawing.Size(23, 22);
            this.tsbLoadRefModel.Text = "toolStripButton1";
            this.tsbLoadRefModel.ToolTipText = "加载引用的模型信息，需选中类型为模型的行";
            this.tsbLoadRefModel.Click += new System.EventHandler(this.tsbLoadRefModel_Click);
            // 
            // tsbAcceptModify
            // 
            this.tsbAcceptModify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAcceptModify.Image = ((System.Drawing.Image)(resources.GetObject("tsbAcceptModify.Image")));
            this.tsbAcceptModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAcceptModify.Name = "tsbAcceptModify";
            this.tsbAcceptModify.Size = new System.Drawing.Size(23, 22);
            this.tsbAcceptModify.Text = "toolStripButton2";
            this.tsbAcceptModify.ToolTipText = "提交修改后的模型";
            this.tsbAcceptModify.Click += new System.EventHandler(this.tsbAcceptModify_Click);
            // 
            // FrmTreeGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 307);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tsbLoadDefModel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTreeGrid";
            this.Text = "FrmTreeGrid";
            this.tsbLoadDefModel.ResumeLayout(false);
            this.tsbLoadDefModel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsbLoadDefModel;
        private System.Windows.Forms.ToolStripButton tsbAddProperty;
        private Syncfusion.Windows.Forms.Grid.GridControl gridControl1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbDeleteProperty;
        private System.Windows.Forms.ToolStripButton tsbMoveUp;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton tsbLoadRefModel;
        private System.Windows.Forms.ToolStripButton tsbAcceptModify;
    }
}