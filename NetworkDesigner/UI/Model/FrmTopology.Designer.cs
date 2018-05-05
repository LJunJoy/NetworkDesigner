namespace NetworkDesigner.UI.Model
{
    partial class FrmTopology
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
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle17 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle18 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle19 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle20 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridCellInfo gridCellInfo5 = new Syncfusion.Windows.Forms.Grid.GridCellInfo();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle21 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle22 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle23 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle24 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridCellInfo gridCellInfo6 = new Syncfusion.Windows.Forms.Grid.GridCellInfo();
            this.splitBottom = new System.Windows.Forms.SplitContainer();
            this.splitMiddle = new System.Windows.Forms.SplitContainer();
            this.splitUp = new System.Windows.Forms.SplitContainer();
            this.grid1 = new Syncfusion.Windows.Forms.Grid.GridControl();
            this.grid2 = new Syncfusion.Windows.Forms.Grid.GridControl();
            this.rtbCmd = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btCancel = new System.Windows.Forms.Button();
            this.btYes = new System.Windows.Forms.Button();
            this.btPreview = new System.Windows.Forms.Button();
            this.btReset = new System.Windows.Forms.Button();
            this.btApply = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitBottom)).BeginInit();
            this.splitBottom.Panel1.SuspendLayout();
            this.splitBottom.Panel2.SuspendLayout();
            this.splitBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMiddle)).BeginInit();
            this.splitMiddle.Panel1.SuspendLayout();
            this.splitMiddle.Panel2.SuspendLayout();
            this.splitMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitUp)).BeginInit();
            this.splitUp.Panel1.SuspendLayout();
            this.splitUp.Panel2.SuspendLayout();
            this.splitUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitBottom
            // 
            this.splitBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBottom.Location = new System.Drawing.Point(0, 0);
            this.splitBottom.Name = "splitBottom";
            this.splitBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitBottom.Panel1
            // 
            this.splitBottom.Panel1.Controls.Add(this.splitMiddle);
            // 
            // splitBottom.Panel2
            // 
            this.splitBottom.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitBottom.Size = new System.Drawing.Size(490, 381);
            this.splitBottom.SplitterDistance = 347;
            this.splitBottom.TabIndex = 1;
            // 
            // splitMiddle
            // 
            this.splitMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMiddle.Location = new System.Drawing.Point(0, 0);
            this.splitMiddle.Name = "splitMiddle";
            this.splitMiddle.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMiddle.Panel1
            // 
            this.splitMiddle.Panel1.Controls.Add(this.splitUp);
            // 
            // splitMiddle.Panel2
            // 
            this.splitMiddle.Panel2.Controls.Add(this.rtbCmd);
            this.splitMiddle.Size = new System.Drawing.Size(490, 347);
            this.splitMiddle.SplitterDistance = 203;
            this.splitMiddle.TabIndex = 1;
            // 
            // splitUp
            // 
            this.splitUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitUp.Location = new System.Drawing.Point(0, 0);
            this.splitUp.Name = "splitUp";
            // 
            // splitUp.Panel1
            // 
            this.splitUp.Panel1.Controls.Add(this.grid1);
            // 
            // splitUp.Panel2
            // 
            this.splitUp.Panel2.Controls.Add(this.grid2);
            this.splitUp.Size = new System.Drawing.Size(490, 203);
            this.splitUp.SplitterDistance = 196;
            this.splitUp.TabIndex = 10;
            // 
            // grid1
            // 
            this.grid1.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.None;
            this.grid1.AllowSelection = Syncfusion.Windows.Forms.Grid.GridSelectionFlags.None;
            this.grid1.BackColor = System.Drawing.SystemColors.Control;
            gridBaseStyle17.Name = "Header";
            gridBaseStyle17.StyleInfo.Borders.Bottom = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle17.StyleInfo.Borders.Left = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle17.StyleInfo.Borders.Right = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle17.StyleInfo.Borders.Top = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle17.StyleInfo.CellType = "Header";
            gridBaseStyle17.StyleInfo.Font.Bold = true;
            gridBaseStyle17.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(199)))), ((int)(((byte)(184))))), System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(234)))), ((int)(((byte)(216))))));
            gridBaseStyle17.StyleInfo.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridBaseStyle18.Name = "Standard";
            gridBaseStyle18.StyleInfo.Font.Facename = "Tahoma";
            gridBaseStyle18.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.SystemColors.Window);
            gridBaseStyle19.Name = "Column Header";
            gridBaseStyle19.StyleInfo.BaseStyle = "Header";
            gridBaseStyle19.StyleInfo.Borders.Bottom = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle19.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center;
            gridBaseStyle19.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Transparent);
            gridBaseStyle20.Name = "Row Header";
            gridBaseStyle20.StyleInfo.BaseStyle = "Header";
            gridBaseStyle20.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridBaseStyle20.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(199)))), ((int)(((byte)(184))))), System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(234)))), ((int)(((byte)(216))))));
            this.grid1.BaseStylesMap.AddRange(new Syncfusion.Windows.Forms.Grid.GridBaseStyle[] {
            gridBaseStyle17,
            gridBaseStyle18,
            gridBaseStyle19,
            gridBaseStyle20});
            this.grid1.ColCount = 2;
            this.grid1.ColWidthEntries.AddRange(new Syncfusion.Windows.Forms.Grid.GridColWidth[] {
            new Syncfusion.Windows.Forms.Grid.GridColWidth(0, 35)});
            this.grid1.DefaultRowHeight = 20;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.ForeColor = System.Drawing.SystemColors.ControlText;
            gridCellInfo5.Col = -1;
            gridCellInfo5.Row = -1;
            this.grid1.GridCells.AddRange(new Syncfusion.Windows.Forms.Grid.GridCellInfo[] {
            gridCellInfo5});
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.Name = "grid1";
            this.grid1.Properties.RowHeaders = false;
            this.grid1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grid1.RowCount = 2;
            this.grid1.RowHeightEntries.AddRange(new Syncfusion.Windows.Forms.Grid.GridRowHeight[] {
            new Syncfusion.Windows.Forms.Grid.GridRowHeight(0, 25)});
            this.grid1.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeIntoCode;
            this.grid1.ShowRowHeaders = false;
            this.grid1.Size = new System.Drawing.Size(196, 203);
            this.grid1.SmartSizeBox = false;
            this.grid1.TabIndex = 0;
            this.grid1.UseRightToLeftCompatibleTextBox = true;
            this.grid1.ResizingColumns += new Syncfusion.Windows.Forms.Grid.GridResizingColumnsEventHandler(this.grid1_ResizingColumns);
            this.grid1.CurrentCellEditingComplete += new System.EventHandler(this.grid1_CurrentCellEditingComplete);
            this.grid1.CellDoubleClick += new Syncfusion.Windows.Forms.Grid.GridCellClickEventHandler(this.grid1_CellDoubleClick);
            this.grid1.SizeChanged += new System.EventHandler(this.grid1_SizeChanged);
            // 
            // grid2
            // 
            this.grid2.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.None;
            this.grid2.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(235)))));
            this.grid2.BackColor = System.Drawing.SystemColors.Control;
            gridBaseStyle21.Name = "Header";
            gridBaseStyle21.StyleInfo.Borders.Bottom = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle21.StyleInfo.Borders.Left = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle21.StyleInfo.Borders.Right = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle21.StyleInfo.Borders.Top = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle21.StyleInfo.CellType = "Header";
            gridBaseStyle21.StyleInfo.Font.Bold = true;
            gridBaseStyle21.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(199)))), ((int)(((byte)(184))))), System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(234)))), ((int)(((byte)(216))))));
            gridBaseStyle21.StyleInfo.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridBaseStyle22.Name = "Standard";
            gridBaseStyle22.StyleInfo.Font.Facename = "Tahoma";
            gridBaseStyle22.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.SystemColors.Window);
            gridBaseStyle23.Name = "Column Header";
            gridBaseStyle23.StyleInfo.BaseStyle = "Header";
            gridBaseStyle23.StyleInfo.Borders.Bottom = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle23.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center;
            gridBaseStyle23.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Transparent);
            gridBaseStyle24.Name = "Row Header";
            gridBaseStyle24.StyleInfo.BaseStyle = "Header";
            gridBaseStyle24.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridBaseStyle24.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(199)))), ((int)(((byte)(184))))), System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(234)))), ((int)(((byte)(216))))));
            this.grid2.BaseStylesMap.AddRange(new Syncfusion.Windows.Forms.Grid.GridBaseStyle[] {
            gridBaseStyle21,
            gridBaseStyle22,
            gridBaseStyle23,
            gridBaseStyle24});
            this.grid2.ColCount = 2;
            this.grid2.ColWidthEntries.AddRange(new Syncfusion.Windows.Forms.Grid.GridColWidth[] {
            new Syncfusion.Windows.Forms.Grid.GridColWidth(0, 35)});
            this.grid2.DefaultRowHeight = 20;
            this.grid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid2.ForeColor = System.Drawing.SystemColors.ControlText;
            gridCellInfo6.Col = -1;
            gridCellInfo6.Row = -1;
            this.grid2.GridCells.AddRange(new Syncfusion.Windows.Forms.Grid.GridCellInfo[] {
            gridCellInfo6});
            this.grid2.Location = new System.Drawing.Point(0, 0);
            this.grid2.Name = "grid2";
            this.grid2.Properties.RowHeaders = false;
            this.grid2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grid2.RowCount = 3;
            this.grid2.RowHeightEntries.AddRange(new Syncfusion.Windows.Forms.Grid.GridRowHeight[] {
            new Syncfusion.Windows.Forms.Grid.GridRowHeight(0, 25)});
            this.grid2.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeIntoCode;
            this.grid2.ShowRowHeaders = false;
            this.grid2.Size = new System.Drawing.Size(290, 203);
            this.grid2.SmartSizeBox = false;
            this.grid2.TabIndex = 0;
            this.grid2.UseRightToLeftCompatibleTextBox = true;
            this.grid2.ResizingColumns += new Syncfusion.Windows.Forms.Grid.GridResizingColumnsEventHandler(this.grid2_ResizingColumns);
            this.grid2.CurrentCellActivated += new System.EventHandler(this.grid2_CurrentCellActivated);
            this.grid2.CurrentCellShowingDropDown += new Syncfusion.Windows.Forms.Grid.GridCurrentCellShowingDropDownEventHandler(this.grid2_CurrentCellShowingDropDown);
            this.grid2.CurrentCellEditingComplete += new System.EventHandler(this.grid2_CurrentCellEditingComplete);
            this.grid2.CellDoubleClick += new Syncfusion.Windows.Forms.Grid.GridCellClickEventHandler(this.grid2_CellDoubleClick);
            this.grid2.SizeChanged += new System.EventHandler(this.grid2_SizeChanged);
            // 
            // rtbCmd
            // 
            this.rtbCmd.AcceptsTab = true;
            this.rtbCmd.BackColor = System.Drawing.SystemColors.Window;
            this.rtbCmd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbCmd.Location = new System.Drawing.Point(0, 0);
            this.rtbCmd.Name = "rtbCmd";
            this.rtbCmd.Size = new System.Drawing.Size(490, 140);
            this.rtbCmd.TabIndex = 3;
            this.rtbCmd.Text = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btCancel, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btYes, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btPreview, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btReset, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btApply, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(490, 30);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(422, 3);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(65, 24);
            this.btCancel.TabIndex = 19;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btYes
            // 
            this.btYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btYes.Location = new System.Drawing.Point(324, 3);
            this.btYes.Name = "btYes";
            this.btYes.Size = new System.Drawing.Size(65, 24);
            this.btYes.TabIndex = 20;
            this.btYes.Text = "确定";
            this.btYes.UseVisualStyleBackColor = true;
            this.btYes.Click += new System.EventHandler(this.btYes_Click);
            // 
            // btPreview
            // 
            this.btPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btPreview.Location = new System.Drawing.Point(226, 3);
            this.btPreview.Name = "btPreview";
            this.btPreview.Size = new System.Drawing.Size(65, 24);
            this.btPreview.TabIndex = 21;
            this.btPreview.Text = "预览";
            this.btPreview.UseVisualStyleBackColor = true;
            // 
            // btReset
            // 
            this.btReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btReset.Location = new System.Drawing.Point(30, 3);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(65, 24);
            this.btReset.TabIndex = 18;
            this.btReset.Text = "重置";
            this.btReset.UseVisualStyleBackColor = true;
            // 
            // btApply
            // 
            this.btApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btApply.Location = new System.Drawing.Point(128, 3);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(65, 24);
            this.btApply.TabIndex = 17;
            this.btApply.Text = "应用";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmTopology
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 381);
            this.Controls.Add(this.splitBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.Name = "FrmTopology";
            this.Text = "拓扑编辑";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTopology_FormClosing);
            this.splitBottom.Panel1.ResumeLayout(false);
            this.splitBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitBottom)).EndInit();
            this.splitBottom.ResumeLayout(false);
            this.splitMiddle.Panel1.ResumeLayout(false);
            this.splitMiddle.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMiddle)).EndInit();
            this.splitMiddle.ResumeLayout(false);
            this.splitUp.Panel1.ResumeLayout(false);
            this.splitUp.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitUp)).EndInit();
            this.splitUp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitBottom;
        private System.Windows.Forms.SplitContainer splitMiddle;
        private System.Windows.Forms.SplitContainer splitUp;
        private System.Windows.Forms.RichTextBox rtbCmd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.Button btYes;
        private Syncfusion.Windows.Forms.Grid.GridControl grid1;
        private Syncfusion.Windows.Forms.Grid.GridControl grid2;
        private System.Windows.Forms.Button btPreview;
        private System.Windows.Forms.Timer timer1;
    }
}