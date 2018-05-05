namespace NetworkDesigner.UI.Model
{
    partial class FrmTraffic
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("CBR");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("VBR");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("FTP");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("HTTP");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("语音");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("视频");
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle1 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle2 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle3 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle4 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridCellInfo gridCellInfo1 = new Syncfusion.Windows.Forms.Grid.GridCellInfo();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTraffic));
            this.tvAllTraffic = new System.Windows.Forms.TreeView();
            this.lbSrcNodes = new System.Windows.Forms.ListBox();
            this.lbDestNodes = new System.Windows.Forms.ListBox();
            this.cbSrcSelct = new System.Windows.Forms.ComboBox();
            this.cbDestSelct = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btRemove = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btRandom = new System.Windows.Forms.Button();
            this.grid1 = new Syncfusion.Windows.Forms.Grid.GridControl();
            this.btClearTraffic = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btDelTraffic = new System.Windows.Forms.Button();
            this.btAddTraffic = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvAllTraffic
            // 
            this.tvAllTraffic.Location = new System.Drawing.Point(2, 12);
            this.tvAllTraffic.Name = "tvAllTraffic";
            treeNode1.Name = "节点0";
            treeNode1.Text = "CBR";
            treeNode2.Name = "节点1";
            treeNode2.Text = "VBR";
            treeNode3.Name = "节点2";
            treeNode3.Text = "FTP";
            treeNode4.Name = "节点3";
            treeNode4.Text = "HTTP";
            treeNode5.Name = "节点4";
            treeNode5.Text = "语音";
            treeNode6.Name = "节点5";
            treeNode6.Text = "视频";
            this.tvAllTraffic.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.tvAllTraffic.Size = new System.Drawing.Size(136, 320);
            this.tvAllTraffic.TabIndex = 0;
            // 
            // lbSrcNodes
            // 
            this.lbSrcNodes.FormattingEnabled = true;
            this.lbSrcNodes.ItemHeight = 12;
            this.lbSrcNodes.Location = new System.Drawing.Point(165, 38);
            this.lbSrcNodes.Name = "lbSrcNodes";
            this.lbSrcNodes.Size = new System.Drawing.Size(89, 292);
            this.lbSrcNodes.TabIndex = 1;
            // 
            // lbDestNodes
            // 
            this.lbDestNodes.FormattingEnabled = true;
            this.lbDestNodes.ItemHeight = 12;
            this.lbDestNodes.Location = new System.Drawing.Point(265, 38);
            this.lbDestNodes.Name = "lbDestNodes";
            this.lbDestNodes.Size = new System.Drawing.Size(89, 292);
            this.lbDestNodes.TabIndex = 2;
            // 
            // cbSrcSelct
            // 
            this.cbSrcSelct.FormattingEnabled = true;
            this.cbSrcSelct.Location = new System.Drawing.Point(165, 12);
            this.cbSrcSelct.Name = "cbSrcSelct";
            this.cbSrcSelct.Size = new System.Drawing.Size(89, 20);
            this.cbSrcSelct.TabIndex = 3;
            this.cbSrcSelct.Text = "主机";
            // 
            // cbDestSelct
            // 
            this.cbDestSelct.FormattingEnabled = true;
            this.cbDestSelct.Location = new System.Drawing.Point(265, 12);
            this.cbDestSelct.Name = "cbDestSelct";
            this.cbDestSelct.Size = new System.Drawing.Size(89, 20);
            this.cbDestSelct.TabIndex = 4;
            this.cbDestSelct.Text = "主机";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(363, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 320);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "业务参数";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(417, 349);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "保存业务";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btRemove
            // 
            this.btRemove.Location = new System.Drawing.Point(93, 349);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(75, 23);
            this.btRemove.TabIndex = 9;
            this.btRemove.Text = "刷新";
            this.btRemove.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(255, 349);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "重置参数";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(336, 349);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 11;
            this.button6.Text = "查看汇总";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // btRandom
            // 
            this.btRandom.Location = new System.Drawing.Point(174, 349);
            this.btRandom.Name = "btRandom";
            this.btRandom.Size = new System.Drawing.Size(75, 23);
            this.btRandom.TabIndex = 12;
            this.btRandom.Text = "随机生成";
            this.btRandom.UseVisualStyleBackColor = true;
            // 
            // grid1
            // 
            this.grid1.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.DblClickOnCell;
            this.grid1.AllowColumnResizeUsingCellBoundaries = true;
            this.grid1.AllowSelection = Syncfusion.Windows.Forms.Grid.GridSelectionFlags.None;
            this.grid1.BackColor = System.Drawing.SystemColors.Control;
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
            gridBaseStyle3.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center;
            gridBaseStyle3.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Transparent);
            gridBaseStyle4.Name = "Row Header";
            gridBaseStyle4.StyleInfo.BaseStyle = "Header";
            gridBaseStyle4.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridBaseStyle4.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(199)))), ((int)(((byte)(184))))), System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(234)))), ((int)(((byte)(216))))));
            this.grid1.BaseStylesMap.AddRange(new Syncfusion.Windows.Forms.Grid.GridBaseStyle[] {
            gridBaseStyle1,
            gridBaseStyle2,
            gridBaseStyle3,
            gridBaseStyle4});
            this.grid1.ColCount = 2;
            this.grid1.ColWidthEntries.AddRange(new Syncfusion.Windows.Forms.Grid.GridColWidth[] {
            new Syncfusion.Windows.Forms.Grid.GridColWidth(0, 35)});
            this.grid1.DefaultRowHeight = 20;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.ForeColor = System.Drawing.SystemColors.ControlText;
            gridCellInfo1.Col = -1;
            gridCellInfo1.Row = -1;
            this.grid1.GridCells.AddRange(new Syncfusion.Windows.Forms.Grid.GridCellInfo[] {
            gridCellInfo1});
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.Name = "grid1";
            this.grid1.Properties.RowHeaders = false;
            this.grid1.ResizeRowsBehavior = Syncfusion.Windows.Forms.Grid.GridResizeCellsBehavior.None;
            this.grid1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grid1.RowCount = 2;
            this.grid1.RowHeightEntries.AddRange(new Syncfusion.Windows.Forms.Grid.GridRowHeight[] {
            new Syncfusion.Windows.Forms.Grid.GridRowHeight(0, 25)});
            this.grid1.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeIntoCode;
            this.grid1.ShowRowHeaders = false;
            this.grid1.Size = new System.Drawing.Size(212, 300);
            this.grid1.SmartSizeBox = false;
            this.grid1.TabIndex = 1;
            this.grid1.UseRightToLeftCompatibleTextBox = true;
            // 
            // btClearTraffic
            // 
            this.btClearTraffic.Location = new System.Drawing.Point(12, 349);
            this.btClearTraffic.Name = "btClearTraffic";
            this.btClearTraffic.Size = new System.Drawing.Size(75, 23);
            this.btClearTraffic.TabIndex = 13;
            this.btClearTraffic.Text = "清空业务";
            this.btClearTraffic.UseVisualStyleBackColor = true;
            this.btClearTraffic.Click += new System.EventHandler(this.btClearTraffic_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grid1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 300);
            this.panel1.TabIndex = 0;
            // 
            // btDelTraffic
            // 
            this.btDelTraffic.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDelTraffic.ForeColor = System.Drawing.Color.Blue;
            this.btDelTraffic.Location = new System.Drawing.Point(139, 163);
            this.btDelTraffic.Name = "btDelTraffic";
            this.btDelTraffic.Size = new System.Drawing.Size(23, 37);
            this.btDelTraffic.TabIndex = 14;
            this.btDelTraffic.Text = ">>";
            this.btDelTraffic.UseVisualStyleBackColor = true;
            // 
            // btAddTraffic
            // 
            this.btAddTraffic.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btAddTraffic.ForeColor = System.Drawing.Color.Blue;
            this.btAddTraffic.Location = new System.Drawing.Point(139, 78);
            this.btAddTraffic.Name = "btAddTraffic";
            this.btAddTraffic.Size = new System.Drawing.Size(23, 37);
            this.btAddTraffic.TabIndex = 15;
            this.btAddTraffic.Text = "<<";
            this.btAddTraffic.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(498, 349);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "完成";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FrmTraffic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 384);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btAddTraffic);
            this.Controls.Add(this.btDelTraffic);
            this.Controls.Add(this.btClearTraffic);
            this.Controls.Add(this.btRandom);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btRemove);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbDestSelct);
            this.Controls.Add(this.cbSrcSelct);
            this.Controls.Add(this.lbDestNodes);
            this.Controls.Add(this.lbSrcNodes);
            this.Controls.Add(this.tvAllTraffic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTraffic";
            this.Text = "业务配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTraffic_FormClosing);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvAllTraffic;
        private System.Windows.Forms.ListBox lbSrcNodes;
        private System.Windows.Forms.ListBox lbDestNodes;
        private System.Windows.Forms.ComboBox cbSrcSelct;
        private System.Windows.Forms.ComboBox cbDestSelct;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btRemove;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btRandom;
        private Syncfusion.Windows.Forms.Grid.GridControl grid1;
        private System.Windows.Forms.Button btClearTraffic;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btDelTraffic;
        private System.Windows.Forms.Button btAddTraffic;
        private System.Windows.Forms.Button button2;
    }
}