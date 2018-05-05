namespace NetworkDesigner.UI.ToolWindow
{
    partial class FrmToolbox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmToolbox));
            this.paletteGroup = new Syncfusion.Windows.Forms.Diagram.Controls.PaletteGroupBar(this.components);
            this.cmsEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miDeleteModel = new System.Windows.Forms.ToolStripMenuItem();
            this.miCancelDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditPalette = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiLoadPalette = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiUnloadPalette = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miSavePalette = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAllPalette = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowProperty = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.paletteGroup)).BeginInit();
            this.cmsEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // paletteGroup
            // 
            this.paletteGroup.AllowDrop = true;
            this.paletteGroup.AnimatedSelection = false;
            this.paletteGroup.BackColor = System.Drawing.SystemColors.Control;
            this.paletteGroup.BeforeTouchSize = new System.Drawing.Size(225, 262);
            this.paletteGroup.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(146)))), ((int)(((byte)(206)))));
            this.paletteGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paletteGroup.CollapseImage = ((System.Drawing.Image)(resources.GetObject("paletteGroup.CollapseImage")));
            this.paletteGroup.Diagram = null;
            this.paletteGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paletteGroup.EditMode = false;
            this.paletteGroup.ExpandButtonToolTip = null;
            this.paletteGroup.ExpandImage = ((System.Drawing.Image)(resources.GetObject("paletteGroup.ExpandImage")));
            this.paletteGroup.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.paletteGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(77)))), ((int)(((byte)(140)))));
            this.paletteGroup.GroupBarDropDownToolTip = null;
            this.paletteGroup.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.paletteGroup.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(65)))), ((int)(((byte)(140)))));
            this.paletteGroup.HeaderHeight = 0;
            this.paletteGroup.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.paletteGroup.IndexOnVisibleItems = true;
            this.paletteGroup.Location = new System.Drawing.Point(0, 0);
            this.paletteGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.paletteGroup.MinimizeButtonToolTip = null;
            this.paletteGroup.Name = "paletteGroup";
            this.paletteGroup.NavigationPaneTooltip = null;
            this.paletteGroup.PopupClientSize = new System.Drawing.Size(0, 0);
            this.paletteGroup.ShowChevron = false;
            this.paletteGroup.ShowNavigationPane = false;
            this.paletteGroup.Size = new System.Drawing.Size(225, 262);
            this.paletteGroup.Splittercolor = System.Drawing.Color.White;
            this.paletteGroup.StackedMode = true;
            this.paletteGroup.TabIndex = 0;
            this.paletteGroup.Text = "paletteGroupBar1";
            this.paletteGroup.VisualStyle = Syncfusion.Windows.Forms.VisualStyle.Office2007;
            // 
            // cmsEditor
            // 
            this.cmsEditor.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.cmsEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDeleteModel,
            this.miCancelDelete,
            this.miEditPalette,
            this.cmiLoadPalette,
            this.cmiUnloadPalette,
            this.toolStripSeparator1,
            this.miSavePalette,
            this.miSaveAllPalette,
            this.miShowProperty});
            this.cmsEditor.Name = "cmsEditor";
            this.cmsEditor.Size = new System.Drawing.Size(149, 186);
            // 
            // miDeleteModel
            // 
            this.miDeleteModel.Image = global::NetworkDesigner.Properties.Resources.cut;
            this.miDeleteModel.Name = "miDeleteModel";
            this.miDeleteModel.Size = new System.Drawing.Size(148, 22);
            this.miDeleteModel.Text = "删除模型(&D)";
            this.miDeleteModel.Click += new System.EventHandler(this.miDeleteModel_Click);
            // 
            // miCancelDelete
            // 
            this.miCancelDelete.Image = global::NetworkDesigner.Properties.Resources.reload;
            this.miCancelDelete.Name = "miCancelDelete";
            this.miCancelDelete.Size = new System.Drawing.Size(148, 22);
            this.miCancelDelete.Text = "撤销删除(&Z)";
            this.miCancelDelete.Click += new System.EventHandler(this.miCancelDelete_Click);
            // 
            // miEditPalette
            // 
            this.miEditPalette.Image = global::NetworkDesigner.Properties.Resources.write;
            this.miEditPalette.Name = "miEditPalette";
            this.miEditPalette.Size = new System.Drawing.Size(148, 22);
            this.miEditPalette.Text = "编辑面板(&E)";
            this.miEditPalette.Click += new System.EventHandler(this.miEditPalette_Click);
            // 
            // cmiLoadPalette
            // 
            this.cmiLoadPalette.Image = global::NetworkDesigner.Properties.Resources.open;
            this.cmiLoadPalette.Name = "cmiLoadPalette";
            this.cmiLoadPalette.Size = new System.Drawing.Size(148, 22);
            this.cmiLoadPalette.Text = "加载面板(&O)";
            this.cmiLoadPalette.Click += new System.EventHandler(this.cmiLoadPalette_Click);
            // 
            // cmiUnloadPalette
            // 
            this.cmiUnloadPalette.Image = ((System.Drawing.Image)(resources.GetObject("cmiUnloadPalette.Image")));
            this.cmiUnloadPalette.Name = "cmiUnloadPalette";
            this.cmiUnloadPalette.Size = new System.Drawing.Size(148, 22);
            this.cmiUnloadPalette.Text = "卸载面板(&U)";
            this.cmiUnloadPalette.Click += new System.EventHandler(this.cmiUnloadPalette_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // miSavePalette
            // 
            this.miSavePalette.Image = ((System.Drawing.Image)(resources.GetObject("miSavePalette.Image")));
            this.miSavePalette.Name = "miSavePalette";
            this.miSavePalette.Size = new System.Drawing.Size(148, 22);
            this.miSavePalette.Text = "保存面板(&S)";
            this.miSavePalette.ToolTipText = "将面板的全部模型保存到文件";
            this.miSavePalette.Click += new System.EventHandler(this.miSavePalette_Click);
            // 
            // miSaveAllPalette
            // 
            this.miSaveAllPalette.Name = "miSaveAllPalette";
            this.miSaveAllPalette.Size = new System.Drawing.Size(148, 22);
            this.miSaveAllPalette.Text = "保存全部面板";
            this.miSaveAllPalette.ToolTipText = "提示保存已修改的面板";
            this.miSaveAllPalette.Click += new System.EventHandler(this.miSaveAllPalette_Click);
            // 
            // miShowProperty
            // 
            this.miShowProperty.Image = global::NetworkDesigner.Properties.Resources.open_in_app;
            this.miShowProperty.Name = "miShowProperty";
            this.miShowProperty.Size = new System.Drawing.Size(148, 22);
            this.miShowProperty.Text = "显示属性";
            this.miShowProperty.Click += new System.EventHandler(this.miShowProperty_Click);
            // 
            // FrmToolbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 262);
            this.Controls.Add(this.paletteGroup);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmToolbox";
            this.Text = "FrmToolbox";
            ((System.ComponentModel.ISupportInitialize)(this.paletteGroup)).EndInit();
            this.cmsEditor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Diagram.Controls.PaletteGroupBar paletteGroup;
        private System.Windows.Forms.ContextMenuStrip cmsEditor;
        private System.Windows.Forms.ToolStripMenuItem miEditPalette;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miSavePalette;
        private System.Windows.Forms.ToolStripMenuItem miShowProperty;
        private System.Windows.Forms.ToolStripMenuItem cmiUnloadPalette;
        private System.Windows.Forms.ToolStripMenuItem miSaveAllPalette;
        private System.Windows.Forms.ToolStripMenuItem cmiLoadPalette;
        private System.Windows.Forms.ToolStripMenuItem miDeleteModel;
        private System.Windows.Forms.ToolStripMenuItem miCancelDelete;
    }
}