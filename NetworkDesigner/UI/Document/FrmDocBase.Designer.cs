namespace NetworkDesigner.UI.Document
{
    partial class FrmDocBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDocBase));
            this.cmsDoc = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiCloseOther = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDoc.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsDoc
            // 
            this.cmsDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSaveFile,
            this.miSaveAsFile,
            this.cmiClose,
            this.cmiCloseOther,
            this.cmiCloseAll});
            this.cmsDoc.Name = "cmsDoc";
            this.cmsDoc.Size = new System.Drawing.Size(153, 136);
            // 
            // miSaveFile
            // 
            this.miSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("miSaveFile.Image")));
            this.miSaveFile.Name = "miSaveFile";
            this.miSaveFile.Size = new System.Drawing.Size(152, 22);
            this.miSaveFile.Text = "保存(&S)";
            this.miSaveFile.Click += new System.EventHandler(this.cmiSave_Click);
            // 
            // miSaveAsFile
            // 
            this.miSaveAsFile.Image = global::NetworkDesigner.Properties.Resources.open;
            this.miSaveAsFile.Name = "miSaveAsFile";
            this.miSaveAsFile.Size = new System.Drawing.Size(152, 22);
            this.miSaveAsFile.Text = "另存(&A)";
            this.miSaveAsFile.Click += new System.EventHandler(this.miSaveAsFile_Click);
            // 
            // cmiClose
            // 
            this.cmiClose.Image = ((System.Drawing.Image)(resources.GetObject("cmiClose.Image")));
            this.cmiClose.Name = "cmiClose";
            this.cmiClose.Size = new System.Drawing.Size(152, 22);
            this.cmiClose.Text = "关闭(&C)";
            this.cmiClose.Click += new System.EventHandler(this.cmiClose_Click);
            // 
            // cmiCloseOther
            // 
            this.cmiCloseOther.Image = ((System.Drawing.Image)(resources.GetObject("cmiCloseOther.Image")));
            this.cmiCloseOther.Name = "cmiCloseOther";
            this.cmiCloseOther.Size = new System.Drawing.Size(152, 22);
            this.cmiCloseOther.Text = "关闭其他(&O)";
            this.cmiCloseOther.Click += new System.EventHandler(this.cmiCloseOther_Click);
            // 
            // cmiCloseAll
            // 
            this.cmiCloseAll.Image = ((System.Drawing.Image)(resources.GetObject("cmiCloseAll.Image")));
            this.cmiCloseAll.Name = "cmiCloseAll";
            this.cmiCloseAll.Size = new System.Drawing.Size(152, 22);
            this.cmiCloseAll.Text = "关闭所有(&A)";
            this.cmiCloseAll.Click += new System.EventHandler(this.cmiCloseAll_Click);
            // 
            // FrmDocBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 371);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmDocBase";
            this.TabPageContextMenuStrip = this.cmsDoc;
            this.Text = "FrmDocBase";
            this.cmsDoc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmsDoc;
        private System.Windows.Forms.ToolStripMenuItem cmiClose;
        private System.Windows.Forms.ToolStripMenuItem cmiCloseOther;
        private System.Windows.Forms.ToolStripMenuItem cmiCloseAll;
        private System.Windows.Forms.ToolStripMenuItem miSaveFile;
        private System.Windows.Forms.ToolStripMenuItem miSaveAsFile;
    }
}