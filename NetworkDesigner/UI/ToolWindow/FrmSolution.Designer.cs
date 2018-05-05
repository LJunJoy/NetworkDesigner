namespace NetworkDesigner.UI.ToolWindow
{
    partial class FrmSolution
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
            Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
            Syncfusion.Windows.Forms.Tools.TreeNodeAdv treeNodeAdv1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdv();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSolution));
            this.tvaProject = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
            this.imagesLeft = new System.Windows.Forms.ImageList(this.components);
            this.cmsCopCutPasDelRen = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiDelet = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiRename = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsOpenClose = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiNewDir = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiFromWizard = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiOpenDir = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiCloseP = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiReloadP = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiSortUp = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiSetDefaultProj = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tvaProject)).BeginInit();
            this.cmsCopCutPasDelRen.SuspendLayout();
            this.cmsOpenClose.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvaProject
            // 
            this.tvaProject.BackColor = System.Drawing.Color.White;
            treeNodeAdvStyleInfo1.EnsureDefaultOptionedChild = true;
            treeNodeAdvStyleInfo1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tvaProject.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1)});
            this.tvaProject.BeforeTouchSize = new System.Drawing.Size(251, 439);
            this.tvaProject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvaProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvaProject.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tvaProject.FullRowSelect = true;
            // 
            // 
            // 
            this.tvaProject.HelpTextControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvaProject.HelpTextControl.Location = new System.Drawing.Point(0, 0);
            this.tvaProject.HelpTextControl.Name = "helpText";
            this.tvaProject.HelpTextControl.Size = new System.Drawing.Size(61, 14);
            this.tvaProject.HelpTextControl.TabIndex = 0;
            this.tvaProject.HelpTextControl.Text = "help text";
            this.tvaProject.InactiveSelectedNodeForeColor = System.Drawing.SystemColors.ControlText;
            this.tvaProject.LeftImageList = this.imagesLeft;
            this.tvaProject.Location = new System.Drawing.Point(0, 0);
            this.tvaProject.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.tvaProject.MouseWheelScrollLines = 10;
            this.tvaProject.Name = "tvaProject";
            treeNodeAdv1.ChildStyle.EnsureDefaultOptionedChild = true;
            treeNodeAdv1.EnsureDefaultOptionedChild = true;
            treeNodeAdv1.Font = new System.Drawing.Font("微软雅黑", 9F);
            treeNodeAdv1.LeftImageIndices = new int[] {
        0};
            treeNodeAdv1.MultiLine = true;
            treeNodeAdv1.PlusMinusSize = new System.Drawing.Size(9, 9);
            treeNodeAdv1.ShowLine = true;
            treeNodeAdv1.Text = "设计方案（0个项目）";
            this.tvaProject.Nodes.AddRange(new Syncfusion.Windows.Forms.Tools.TreeNodeAdv[] {
            treeNodeAdv1});
            this.tvaProject.SelectedNodeBackground = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220))))));
            this.tvaProject.SelectedNodeForeColor = System.Drawing.SystemColors.HighlightText;
            this.tvaProject.ShouldSelectNodeOnEnter = false;
            this.tvaProject.ShowLines = false;
            this.tvaProject.Size = new System.Drawing.Size(251, 439);
            this.tvaProject.TabIndex = 0;
            this.tvaProject.Text = "tvaProject";
            // 
            // 
            // 
            this.tvaProject.ToolTipControl.BackColor = System.Drawing.SystemColors.Info;
            this.tvaProject.ToolTipControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvaProject.ToolTipControl.Location = new System.Drawing.Point(0, 0);
            this.tvaProject.ToolTipControl.Name = "toolTip";
            this.tvaProject.ToolTipControl.Size = new System.Drawing.Size(49, 14);
            this.tvaProject.ToolTipControl.TabIndex = 1;
            this.tvaProject.ToolTipControl.Text = "toolTip";
            this.tvaProject.NodeMouseDoubleClick += new Syncfusion.Windows.Forms.Tools.TreeNodeAdvMouseClickArgs(this.tvaProject_NodeMouseDoubleClick);
            this.tvaProject.NodeEditorValidated += new Syncfusion.Windows.Forms.Tools.TreeNodeAdvEditEventHandler(this.tvaProject_NodeEditorValidated);
            this.tvaProject.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvaProject_KeyUp);
            this.tvaProject.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvaProject_MouseDown);
            // 
            // imagesLeft
            // 
            this.imagesLeft.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesLeft.ImageStream")));
            this.imagesLeft.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesLeft.Images.SetKeyName(0, "hand_point.png");
            this.imagesLeft.Images.SetKeyName(1, "category.png");
            this.imagesLeft.Images.SetKeyName(2, "book.png");
            this.imagesLeft.Images.SetKeyName(3, "function_recently_used.png");
            this.imagesLeft.Images.SetKeyName(4, "folder.png");
            this.imagesLeft.Images.SetKeyName(5, "document_font.png");
            this.imagesLeft.Images.SetKeyName(6, "file_extension_doc.png");
            this.imagesLeft.Images.SetKeyName(7, "file_xml.png");
            this.imagesLeft.Images.SetKeyName(8, "page.png");
            this.imagesLeft.Images.SetKeyName(9, "code.png");
            this.imagesLeft.Images.SetKeyName(10, "document_image_ver.png");
            this.imagesLeft.Images.SetKeyName(11, "module.png");
            this.imagesLeft.Images.SetKeyName(12, "document_spacing.png");
            // 
            // cmsCopCutPasDelRen
            // 
            this.cmsCopCutPasDelRen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiCut,
            this.cmiCopy,
            this.cmiPaste,
            this.cmiDelet,
            this.cmiRename});
            this.cmsCopCutPasDelRen.Name = "cmsCopCutPasDelRen";
            this.cmsCopCutPasDelRen.Size = new System.Drawing.Size(133, 114);
            // 
            // cmiCut
            // 
            this.cmiCut.Image = ((System.Drawing.Image)(resources.GetObject("cmiCut.Image")));
            this.cmiCut.Name = "cmiCut";
            this.cmiCut.Size = new System.Drawing.Size(132, 22);
            this.cmiCut.Text = "剪切(&X)";
            this.cmiCut.Click += new System.EventHandler(this.cmiCut_Click);
            // 
            // cmiCopy
            // 
            this.cmiCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmiCopy.Image")));
            this.cmiCopy.Name = "cmiCopy";
            this.cmiCopy.Size = new System.Drawing.Size(132, 22);
            this.cmiCopy.Text = "复制(&C)";
            this.cmiCopy.Click += new System.EventHandler(this.cmiCopy_Click);
            // 
            // cmiPaste
            // 
            this.cmiPaste.Image = ((System.Drawing.Image)(resources.GetObject("cmiPaste.Image")));
            this.cmiPaste.Name = "cmiPaste";
            this.cmiPaste.Size = new System.Drawing.Size(132, 22);
            this.cmiPaste.Text = "粘贴(&P)";
            this.cmiPaste.Click += new System.EventHandler(this.cmiPaste_Click);
            // 
            // cmiDelet
            // 
            this.cmiDelet.Image = ((System.Drawing.Image)(resources.GetObject("cmiDelet.Image")));
            this.cmiDelet.Name = "cmiDelet";
            this.cmiDelet.Size = new System.Drawing.Size(132, 22);
            this.cmiDelet.Text = "删除(&D)";
            this.cmiDelet.Click += new System.EventHandler(this.cmiDelet_Click);
            // 
            // cmiRename
            // 
            this.cmiRename.Image = ((System.Drawing.Image)(resources.GetObject("cmiRename.Image")));
            this.cmiRename.Name = "cmiRename";
            this.cmiRename.Size = new System.Drawing.Size(132, 22);
            this.cmiRename.Text = "重命名(&M)";
            this.cmiRename.Click += new System.EventHandler(this.cmiRename_Click);
            // 
            // cmsOpenClose
            // 
            this.cmsOpenClose.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiAdd,
            this.cmiOpenFile,
            this.cmiOpenDir,
            this.cmiCloseP,
            this.cmiReloadP,
            this.cmiSortUp,
            this.cmiSetDefaultProj});
            this.cmsOpenClose.Name = "cmsOpenClose";
            this.cmsOpenClose.Size = new System.Drawing.Size(164, 158);
            // 
            // cmiAdd
            // 
            this.cmiAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiNewFile,
            this.cmiNewDir,
            this.cmiFromWizard});
            this.cmiAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmiAdd.Image")));
            this.cmiAdd.Name = "cmiAdd";
            this.cmiAdd.Size = new System.Drawing.Size(163, 22);
            this.cmiAdd.Text = "添加(&A)";
            // 
            // cmiNewFile
            // 
            this.cmiNewFile.Image = global::NetworkDesigner.Properties.Resources.newfile;
            this.cmiNewFile.Name = "cmiNewFile";
            this.cmiNewFile.Size = new System.Drawing.Size(156, 22);
            this.cmiNewFile.Text = "文件(&F)";
            this.cmiNewFile.Click += new System.EventHandler(this.cmiNewFile_Click);
            // 
            // cmiNewDir
            // 
            this.cmiNewDir.Image = ((System.Drawing.Image)(resources.GetObject("cmiNewDir.Image")));
            this.cmiNewDir.Name = "cmiNewDir";
            this.cmiNewDir.Size = new System.Drawing.Size(156, 22);
            this.cmiNewDir.Text = "文件夹(&D)";
            this.cmiNewDir.Click += new System.EventHandler(this.cmiNewDir_Click);
            // 
            // cmiFromWizard
            // 
            this.cmiFromWizard.Image = ((System.Drawing.Image)(resources.GetObject("cmiFromWizard.Image")));
            this.cmiFromWizard.Name = "cmiFromWizard";
            this.cmiFromWizard.Size = new System.Drawing.Size(156, 22);
            this.cmiFromWizard.Text = "从向导添加(&W)";
            this.cmiFromWizard.Click += new System.EventHandler(this.cmiNewWizard_Click);
            // 
            // cmiOpenFile
            // 
            this.cmiOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("cmiOpenFile.Image")));
            this.cmiOpenFile.Name = "cmiOpenFile";
            this.cmiOpenFile.Size = new System.Drawing.Size(163, 22);
            this.cmiOpenFile.Text = "打开(&O)";
            this.cmiOpenFile.Click += new System.EventHandler(this.cmiOpenFile_Click);
            // 
            // cmiOpenDir
            // 
            this.cmiOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("cmiOpenDir.Image")));
            this.cmiOpenDir.Name = "cmiOpenDir";
            this.cmiOpenDir.Size = new System.Drawing.Size(163, 22);
            this.cmiOpenDir.Text = "打开文件夹(&W)";
            this.cmiOpenDir.Click += new System.EventHandler(this.cmiOpenDir_Click);
            // 
            // cmiCloseP
            // 
            this.cmiCloseP.Image = ((System.Drawing.Image)(resources.GetObject("cmiCloseP.Image")));
            this.cmiCloseP.Name = "cmiCloseP";
            this.cmiCloseP.Size = new System.Drawing.Size(163, 22);
            this.cmiCloseP.Text = "卸载项目(&U)";
            this.cmiCloseP.Click += new System.EventHandler(this.cmiCloseP_Click);
            // 
            // cmiReloadP
            // 
            this.cmiReloadP.Image = global::NetworkDesigner.Properties.Resources.reload;
            this.cmiReloadP.Name = "cmiReloadP";
            this.cmiReloadP.Size = new System.Drawing.Size(163, 22);
            this.cmiReloadP.Text = "重新加载(&R)";
            this.cmiReloadP.Click += new System.EventHandler(this.cmiReloadP_Click);
            // 
            // cmiSortUp
            // 
            this.cmiSortUp.Image = ((System.Drawing.Image)(resources.GetObject("cmiSortUp.Image")));
            this.cmiSortUp.Name = "cmiSortUp";
            this.cmiSortUp.Size = new System.Drawing.Size(163, 22);
            this.cmiSortUp.Text = "升序排序(&S)";
            this.cmiSortUp.Click += new System.EventHandler(this.cmiSortUp_Click);
            // 
            // cmiSetDefaultProj
            // 
            this.cmiSetDefaultProj.Image = ((System.Drawing.Image)(resources.GetObject("cmiSetDefaultProj.Image")));
            this.cmiSetDefaultProj.Name = "cmiSetDefaultProj";
            this.cmiSetDefaultProj.Size = new System.Drawing.Size(163, 22);
            this.cmiSetDefaultProj.Text = "设为默认项目(&T)";
            this.cmiSetDefaultProj.Click += new System.EventHandler(this.cmiSetDefaultProj_Click);
            // 
            // FrmSolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(251, 439);
            this.Controls.Add(this.tvaProject);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmSolution";
            this.Text = "设计方案管理器";
            ((System.ComponentModel.ISupportInitialize)(this.tvaProject)).EndInit();
            this.cmsCopCutPasDelRen.ResumeLayout(false);
            this.cmsOpenClose.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TreeViewAdv tvaProject;
        private System.Windows.Forms.ImageList imagesLeft;
        private System.Windows.Forms.ContextMenuStrip cmsCopCutPasDelRen;
        private System.Windows.Forms.ToolStripMenuItem cmiCut;
        private System.Windows.Forms.ToolStripMenuItem cmiCopy;
        private System.Windows.Forms.ToolStripMenuItem cmiPaste;
        private System.Windows.Forms.ToolStripMenuItem cmiDelet;
        private System.Windows.Forms.ToolStripMenuItem cmiRename;
        private System.Windows.Forms.ContextMenuStrip cmsOpenClose;
        private System.Windows.Forms.ToolStripMenuItem cmiOpenFile;
        private System.Windows.Forms.ToolStripMenuItem cmiOpenDir;
        private System.Windows.Forms.ToolStripMenuItem cmiCloseP;
        private System.Windows.Forms.ToolStripMenuItem cmiAdd;
        private System.Windows.Forms.ToolStripMenuItem cmiReloadP;
        private System.Windows.Forms.ToolStripMenuItem cmiSortUp;
        private System.Windows.Forms.ToolStripMenuItem cmiNewFile;
        private System.Windows.Forms.ToolStripMenuItem cmiNewDir;
        private System.Windows.Forms.ToolStripMenuItem cmiFromWizard;
        private System.Windows.Forms.ToolStripMenuItem cmiSetDefaultProj;
    }
}