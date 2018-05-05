namespace NetworkDesigner.UI.Document
{
    partial class FrmDiagram
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
            Syncfusion.Windows.Forms.Diagram.Binding binding1 = new Syncfusion.Windows.Forms.Diagram.Binding();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDiagram));
            this.diagram1 = new Syncfusion.Windows.Forms.Diagram.Controls.Diagram(this.components);
            this.cmsEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.miRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllowResizeNode = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsSetTraffic = new System.Windows.Forms.ToolStripMenuItem();
            this.输出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTransOPNET = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTransQualnet = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTransMininet = new System.Windows.Forms.ToolStripMenuItem();
            this.model1 = new Syncfusion.Windows.Forms.Diagram.Model(this.components);
            this.cmsEmpty = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.diagram1)).BeginInit();
            this.cmsEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.model1)).BeginInit();
            this.SuspendLayout();
            // 
            // diagram1
            // 
            binding1.DefaultConnector = null;
            binding1.DefaultNode = null;
            binding1.Diagram = this.diagram1;
            binding1.Id = null;
            binding1.Label = ((System.Collections.Generic.List<string>)(resources.GetObject("binding1.Label")));
            binding1.ParentId = null;
            this.diagram1.Binding = binding1;
            this.diagram1.ContextMenuStrip = this.cmsEditor;
            this.diagram1.Controller.Constraint = Syncfusion.Windows.Forms.Diagram.Constraints.PageEditable;
            this.diagram1.Controller.DefaultConnectorTool = Syncfusion.Windows.Forms.Diagram.ConnectorTool.OrgLineConnectorTool;
            this.diagram1.Controller.PasteOffset = new System.Drawing.SizeF(10F, 10F);
            this.diagram1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagram1.EnableTouchMode = false;
            this.diagram1.HScroll = true;
            this.diagram1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.diagram1.LayoutManager = null;
            this.diagram1.Location = new System.Drawing.Point(0, 0);
            this.diagram1.Model = this.model1;
            this.diagram1.Name = "diagram1";
            this.diagram1.ScrollVirtualBounds = ((System.Drawing.RectangleF)(resources.GetObject("diagram1.ScrollVirtualBounds")));
            this.diagram1.Size = new System.Drawing.Size(516, 415);
            this.diagram1.SmartSizeBox = false;
            this.diagram1.TabIndex = 0;
            this.diagram1.Text = "diagram1";
            // 
            // 
            // 
            this.diagram1.View.ClientRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.diagram1.View.Controller = this.diagram1.Controller;
            this.diagram1.View.Grid.MinPixelSpacing = 4F;
            this.diagram1.View.ScrollVirtualBounds = ((System.Drawing.RectangleF)(resources.GetObject("resource.ScrollVirtualBounds")));
            this.diagram1.View.ZoomType = Syncfusion.Windows.Forms.Diagram.ZoomType.Center;
            this.diagram1.VScroll = true;
            this.diagram1.MouseWheelZoom += new Syncfusion.Windows.Forms.MouseWheelZoomEventHandler(this.diagram1_MouseWheelZoom);
            this.diagram1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.diagram1_KeyDown);
            this.diagram1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.diagram1_MouseDown);
            this.diagram1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.diagram1_MouseMove);
            this.diagram1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.diagram1_MouseUp);
            // 
            // cmsEditor
            // 
            this.cmsEditor.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.cmsEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEdit,
            this.miUndo,
            this.miRedo,
            this.toolStripSeparator1,
            this.miSaveFile,
            this.miSaveAsFile,
            this.miAllowResizeNode,
            this.cmsSetTraffic,
            this.输出ToolStripMenuItem});
            this.cmsEditor.Name = "cmsEditor";
            this.cmsEditor.Size = new System.Drawing.Size(160, 208);
            // 
            // miEdit
            // 
            this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEditSelectAll,
            this.miEditCopy,
            this.miEditCut,
            this.miEditPaste,
            this.miEditDelete});
            this.miEdit.Image = global::NetworkDesigner.Properties.Resources.write;
            this.miEdit.Name = "miEdit";
            this.miEdit.Size = new System.Drawing.Size(159, 22);
            this.miEdit.Text = "编辑(&E)";
            // 
            // miEditSelectAll
            // 
            this.miEditSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("miEditSelectAll.Image")));
            this.miEditSelectAll.Name = "miEditSelectAll";
            this.miEditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.miEditSelectAll.Size = new System.Drawing.Size(163, 22);
            this.miEditSelectAll.Text = "全选(&A)";
            // 
            // miEditCopy
            // 
            this.miEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("miEditCopy.Image")));
            this.miEditCopy.Name = "miEditCopy";
            this.miEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miEditCopy.Size = new System.Drawing.Size(163, 22);
            this.miEditCopy.Text = "复制(&C)";
            // 
            // miEditCut
            // 
            this.miEditCut.Image = ((System.Drawing.Image)(resources.GetObject("miEditCut.Image")));
            this.miEditCut.Name = "miEditCut";
            this.miEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.miEditCut.Size = new System.Drawing.Size(163, 22);
            this.miEditCut.Text = "剪切(&X)";
            // 
            // miEditPaste
            // 
            this.miEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("miEditPaste.Image")));
            this.miEditPaste.Name = "miEditPaste";
            this.miEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.miEditPaste.Size = new System.Drawing.Size(163, 22);
            this.miEditPaste.Text = "粘贴(&V)";
            // 
            // miEditDelete
            // 
            this.miEditDelete.Image = global::NetworkDesigner.Properties.Resources.font_red_delete;
            this.miEditDelete.Name = "miEditDelete";
            this.miEditDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.miEditDelete.Size = new System.Drawing.Size(163, 22);
            this.miEditDelete.Text = "删除(&D)";
            // 
            // miUndo
            // 
            this.miUndo.Image = ((System.Drawing.Image)(resources.GetObject("miUndo.Image")));
            this.miUndo.Name = "miUndo";
            this.miUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.miUndo.Size = new System.Drawing.Size(159, 22);
            this.miUndo.Text = "撤销(&Z)";
            // 
            // miRedo
            // 
            this.miRedo.Image = ((System.Drawing.Image)(resources.GetObject("miRedo.Image")));
            this.miRedo.Name = "miRedo";
            this.miRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.miRedo.Size = new System.Drawing.Size(159, 22);
            this.miRedo.Text = "恢复(&Y)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            // 
            // miSaveFile
            // 
            this.miSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("miSaveFile.Image")));
            this.miSaveFile.Name = "miSaveFile";
            this.miSaveFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSaveFile.Size = new System.Drawing.Size(159, 22);
            this.miSaveFile.Text = "保存(&S)";
            this.miSaveFile.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miSaveAsFile
            // 
            this.miSaveAsFile.Image = global::NetworkDesigner.Properties.Resources.open;
            this.miSaveAsFile.Name = "miSaveAsFile";
            this.miSaveAsFile.Size = new System.Drawing.Size(159, 22);
            this.miSaveAsFile.Text = "另存为(&T)...";
            this.miSaveAsFile.Click += new System.EventHandler(this.miSaveAs_Click);
            // 
            // miAllowResizeNode
            // 
            this.miAllowResizeNode.Image = ((System.Drawing.Image)(resources.GetObject("miAllowResizeNode.Image")));
            this.miAllowResizeNode.Name = "miAllowResizeNode";
            this.miAllowResizeNode.Size = new System.Drawing.Size(159, 22);
            this.miAllowResizeNode.Text = "调整节点大小";
            this.miAllowResizeNode.Click += new System.EventHandler(this.miAllowResizeNode_Click);
            // 
            // cmsSetTraffic
            // 
            this.cmsSetTraffic.Name = "cmsSetTraffic";
            this.cmsSetTraffic.Size = new System.Drawing.Size(159, 22);
            this.cmsSetTraffic.Text = "设置业务";
            this.cmsSetTraffic.Click += new System.EventHandler(this.cmsSetTraffic_Click);
            // 
            // 输出ToolStripMenuItem
            // 
            this.输出ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsTransOPNET,
            this.cmsTransQualnet,
            this.cmsTransMininet});
            this.输出ToolStripMenuItem.Name = "输出ToolStripMenuItem";
            this.输出ToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.输出ToolStripMenuItem.Text = "输出";
            // 
            // cmsTransOPNET
            // 
            this.cmsTransOPNET.Name = "cmsTransOPNET";
            this.cmsTransOPNET.Size = new System.Drawing.Size(121, 22);
            this.cmsTransOPNET.Text = "OPNET";
            // 
            // cmsTransQualnet
            // 
            this.cmsTransQualnet.Name = "cmsTransQualnet";
            this.cmsTransQualnet.Size = new System.Drawing.Size(121, 22);
            this.cmsTransQualnet.Text = "Qualnet";
            // 
            // cmsTransMininet
            // 
            this.cmsTransMininet.Name = "cmsTransMininet";
            this.cmsTransMininet.Size = new System.Drawing.Size(121, 22);
            this.cmsTransMininet.Text = "Mininet";
            // 
            // model1
            // 
            this.model1.AlignmentType = AlignmentType.SelectedNode;
            this.model1.BackgroundStyle.PathBrushStyle = Syncfusion.Windows.Forms.Diagram.PathGradientBrushStyle.RectangleCenter;
            this.model1.DocumentScale.DisplayName = "No Scale";
            this.model1.DocumentScale.Height = 1F;
            this.model1.DocumentScale.Width = 1F;
            this.model1.DocumentSize.Height = 1169F;
            this.model1.DocumentSize.Width = 827F;
            this.model1.LineStyle.DashPattern = null;
            this.model1.LineStyle.LineColor = System.Drawing.Color.Black;
            this.model1.LogicalSize = new System.Drawing.SizeF(827F, 1169F);
            this.model1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.model1.ShadowStyle.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.model1.ShadowStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            // 
            // cmsEmpty
            // 
            this.cmsEmpty.Name = "cmsEmpty";
            this.cmsEmpty.Size = new System.Drawing.Size(61, 4);
            // 
            // FrmDiagram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 415);
            this.Controls.Add(this.diagram1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Name = "FrmDiagram";
            this.Text = "FrmDiagram";
            this.Activated += new System.EventHandler(this.FrmDiagram_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDiagram_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.diagram1)).EndInit();
            this.cmsEditor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.model1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Diagram.Controls.Diagram diagram1;
        private Syncfusion.Windows.Forms.Diagram.Model model1;
        private System.Windows.Forms.ContextMenuStrip cmsEditor;
        private System.Windows.Forms.ToolStripMenuItem miEdit;
        private System.Windows.Forms.ToolStripMenuItem miEditSelectAll;
        private System.Windows.Forms.ToolStripMenuItem miEditCopy;
        private System.Windows.Forms.ToolStripMenuItem miEditCut;
        private System.Windows.Forms.ToolStripMenuItem miEditPaste;
        private System.Windows.Forms.ToolStripMenuItem miEditDelete;
        private System.Windows.Forms.ToolStripMenuItem miUndo;
        private System.Windows.Forms.ToolStripMenuItem miRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miSaveFile;
        private System.Windows.Forms.ToolStripMenuItem miSaveAsFile;
        private System.Windows.Forms.ToolStripMenuItem cmsSetTraffic;
        private System.Windows.Forms.ToolStripMenuItem miAllowResizeNode;
        private System.Windows.Forms.ContextMenuStrip cmsEmpty;
        private System.Windows.Forms.ToolStripMenuItem 输出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmsTransOPNET;
        private System.Windows.Forms.ToolStripMenuItem cmsTransQualnet;
        private System.Windows.Forms.ToolStripMenuItem cmsTransMininet;
    }
}