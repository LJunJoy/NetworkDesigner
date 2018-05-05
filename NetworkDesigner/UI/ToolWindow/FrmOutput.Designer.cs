namespace NetworkDesigner.UI.ToolWindow
{
    partial class FrmOutput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOutput));
            this.cmsEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miClear = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.miUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.miRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miAppendFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.rtx = new System.Windows.Forms.RichTextBox();
            this.cmsEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsEditor
            // 
            this.cmsEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClear,
            this.miDelete,
            this.miSelectAll,
            this.miEditCut,
            this.miEditCopy,
            this.miEditPaste,
            this.miUndo,
            this.miRedo,
            this.toolStripSeparator1,
            this.miAppendFile,
            this.miSaveFile,
            this.miSaveAsFile});
            this.cmsEditor.Name = "cmsEditor";
            this.cmsEditor.Size = new System.Drawing.Size(164, 252);
            // 
            // miClear
            // 
            this.miClear.Image = ((System.Drawing.Image)(resources.GetObject("miClear.Image")));
            this.miClear.Name = "miClear";
            this.miClear.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.miClear.Size = new System.Drawing.Size(163, 22);
            this.miClear.Text = "清除(&R)";
            this.miClear.Click += new System.EventHandler(this.miClear_Click);
            // 
            // miDelete
            // 
            this.miDelete.Image = global::NetworkDesigner.Properties.Resources.font_red_delete;
            this.miDelete.Name = "miDelete";
            this.miDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.miDelete.Size = new System.Drawing.Size(163, 22);
            this.miDelete.Text = "删除(&D)";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // miSelectAll
            // 
            this.miSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("miSelectAll.Image")));
            this.miSelectAll.Name = "miSelectAll";
            this.miSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.miSelectAll.Size = new System.Drawing.Size(163, 22);
            this.miSelectAll.Text = "全选(&A)";
            this.miSelectAll.Click += new System.EventHandler(this.miSelectAll_Click);
            // 
            // miEditCut
            // 
            this.miEditCut.Image = ((System.Drawing.Image)(resources.GetObject("miEditCut.Image")));
            this.miEditCut.Name = "miEditCut";
            this.miEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.miEditCut.Size = new System.Drawing.Size(163, 22);
            this.miEditCut.Text = "剪切(&X)";
            this.miEditCut.Click += new System.EventHandler(this.miEditCut_Click);
            // 
            // miEditCopy
            // 
            this.miEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("miEditCopy.Image")));
            this.miEditCopy.Name = "miEditCopy";
            this.miEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miEditCopy.Size = new System.Drawing.Size(163, 22);
            this.miEditCopy.Text = "复制(&C)";
            this.miEditCopy.Click += new System.EventHandler(this.miEditCopy_Click);
            // 
            // miEditPaste
            // 
            this.miEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("miEditPaste.Image")));
            this.miEditPaste.Name = "miEditPaste";
            this.miEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.miEditPaste.Size = new System.Drawing.Size(163, 22);
            this.miEditPaste.Text = "粘贴(&V)";
            this.miEditPaste.Click += new System.EventHandler(this.miEditPaste_Click);
            // 
            // miUndo
            // 
            this.miUndo.Image = ((System.Drawing.Image)(resources.GetObject("miUndo.Image")));
            this.miUndo.Name = "miUndo";
            this.miUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.miUndo.Size = new System.Drawing.Size(163, 22);
            this.miUndo.Text = "撤销(&Z)";
            this.miUndo.Click += new System.EventHandler(this.miUndo_Click);
            // 
            // miRedo
            // 
            this.miRedo.Image = ((System.Drawing.Image)(resources.GetObject("miRedo.Image")));
            this.miRedo.Name = "miRedo";
            this.miRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.miRedo.Size = new System.Drawing.Size(163, 22);
            this.miRedo.Text = "恢复(&Y)";
            this.miRedo.Click += new System.EventHandler(this.miRedo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(160, 6);
            // 
            // miAppendFile
            // 
            this.miAppendFile.Image = global::NetworkDesigner.Properties.Resources.write;
            this.miAppendFile.Name = "miAppendFile";
            this.miAppendFile.Size = new System.Drawing.Size(163, 22);
            this.miAppendFile.Text = "追加写入(&P)";
            this.miAppendFile.Click += new System.EventHandler(this.miAppendFile_Click);
            // 
            // miSaveFile
            // 
            this.miSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("miSaveFile.Image")));
            this.miSaveFile.Name = "miSaveFile";
            this.miSaveFile.Size = new System.Drawing.Size(163, 22);
            this.miSaveFile.Text = "覆盖保存(&S)";
            this.miSaveFile.Click += new System.EventHandler(this.miSaveFile_Click);
            // 
            // miSaveAsFile
            // 
            this.miSaveAsFile.Image = global::NetworkDesigner.Properties.Resources.open;
            this.miSaveAsFile.Name = "miSaveAsFile";
            this.miSaveAsFile.Size = new System.Drawing.Size(163, 22);
            this.miSaveAsFile.Text = "另存为(&T)...";
            this.miSaveAsFile.Click += new System.EventHandler(this.miSaveAsFile_Click);
            // 
            // rtx
            // 
            this.rtx.BackColor = System.Drawing.SystemColors.Control;
            this.rtx.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtx.ContextMenuStrip = this.cmsEditor;
            this.rtx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtx.Font = new System.Drawing.Font("宋体", 10F);
            this.rtx.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.rtx.Location = new System.Drawing.Point(0, 0);
            this.rtx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtx.Name = "rtx";
            this.rtx.Size = new System.Drawing.Size(317, 292);
            this.rtx.TabIndex = 1;
            this.rtx.Text = "";
            // 
            // FrmOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 292);
            this.Controls.Add(this.rtx);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOutput";
            this.Text = "FrmOutput";
            this.cmsEditor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmsEditor;
        private System.Windows.Forms.ToolStripMenuItem miAppendFile;
        private System.Windows.Forms.ToolStripMenuItem miSaveFile;
        private System.Windows.Forms.ToolStripMenuItem miSaveAsFile;
        private System.Windows.Forms.RichTextBox rtx;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miClear;
        private System.Windows.Forms.ToolStripMenuItem miSelectAll;
        private System.Windows.Forms.ToolStripMenuItem miEditCut;
        private System.Windows.Forms.ToolStripMenuItem miEditCopy;
        private System.Windows.Forms.ToolStripMenuItem miEditPaste;
        private System.Windows.Forms.ToolStripMenuItem miUndo;
        private System.Windows.Forms.ToolStripMenuItem miRedo;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
    }
}