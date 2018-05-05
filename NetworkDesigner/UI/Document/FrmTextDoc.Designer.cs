namespace NetworkDesigner.UI.Document
{
    partial class FrmTextDoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTextDoc));
            this.textEditorControl = new ICSharpCode.TextEditor.TextEditorControl();
            this.cmsEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.miToggleBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.miGoToNextBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.miGoToPrevBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.miOption = new System.Windows.Forms.ToolStripMenuItem();
            this.miSplitWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowLineNumbers = new System.Windows.Forms.ToolStripMenuItem();
            this.miHLCurRow = new System.Windows.Forms.ToolStripMenuItem();
            this.miBracketMatchingStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetFont = new System.Windows.Forms.ToolStripMenuItem();
            this.miFormatXml = new System.Windows.Forms.ToolStripMenuItem();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.cmsEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // textEditorControl
            // 
            this.textEditorControl.ContextMenuStrip = this.cmsEditor;
            this.textEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorControl.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.textEditorControl.IsReadOnly = false;
            this.textEditorControl.Location = new System.Drawing.Point(0, 0);
            this.textEditorControl.Name = "textEditorControl";
            this.textEditorControl.Size = new System.Drawing.Size(485, 440);
            this.textEditorControl.TabIndex = 0;
            this.textEditorControl.TextChanged += new System.EventHandler(this.textEditorControl_TextChanged);
            // 
            // cmsEditor
            // 
            this.cmsEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpenFile,
            this.miSave,
            this.miSaveAs,
            this.miViewMode,
            this.toolStripSeparator1,
            this.miEditCut,
            this.miEditCopy,
            this.miEditPaste,
            this.toolStripMenuItem2,
            this.miEditFind,
            this.miEditReplace,
            this.miToggleBookmark,
            this.miGoToNextBookmark,
            this.miGoToPrevBookmark,
            this.toolStripMenuItem3,
            this.miOption});
            this.cmsEditor.Name = "cmsEditor";
            this.cmsEditor.Size = new System.Drawing.Size(204, 308);
            // 
            // miOpenFile
            // 
            this.miOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("miOpenFile.Image")));
            this.miOpenFile.Name = "miOpenFile";
            this.miOpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.miOpenFile.Size = new System.Drawing.Size(203, 22);
            this.miOpenFile.Text = "打开(&O)...";
            this.miOpenFile.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miSave
            // 
            this.miSave.Image = ((System.Drawing.Image)(resources.GetObject("miSave.Image")));
            this.miSave.Name = "miSave";
            this.miSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSave.Size = new System.Drawing.Size(203, 22);
            this.miSave.Text = "保存(&S)";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miSaveAs
            // 
            this.miSaveAs.Image = global::NetworkDesigner.Properties.Resources.page_copy;
            this.miSaveAs.Name = "miSaveAs";
            this.miSaveAs.Size = new System.Drawing.Size(203, 22);
            this.miSaveAs.Text = "另存为(&A)...";
            this.miSaveAs.Click += new System.EventHandler(this.miSaveAs_Click);
            // 
            // miViewMode
            // 
            this.miViewMode.Name = "miViewMode";
            this.miViewMode.Size = new System.Drawing.Size(203, 22);
            this.miViewMode.Text = "查看方式(&M)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(200, 6);
            // 
            // miEditCut
            // 
            this.miEditCut.Image = ((System.Drawing.Image)(resources.GetObject("miEditCut.Image")));
            this.miEditCut.Name = "miEditCut";
            this.miEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.miEditCut.Size = new System.Drawing.Size(203, 22);
            this.miEditCut.Text = "剪切(&X)";
            this.miEditCut.Click += new System.EventHandler(this.miEditCut_Click);
            // 
            // miEditCopy
            // 
            this.miEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("miEditCopy.Image")));
            this.miEditCopy.Name = "miEditCopy";
            this.miEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miEditCopy.Size = new System.Drawing.Size(203, 22);
            this.miEditCopy.Text = "复制(&C)";
            this.miEditCopy.Click += new System.EventHandler(this.miEditCopy_Click);
            // 
            // miEditPaste
            // 
            this.miEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("miEditPaste.Image")));
            this.miEditPaste.Name = "miEditPaste";
            this.miEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.miEditPaste.Size = new System.Drawing.Size(203, 22);
            this.miEditPaste.Text = "粘贴(&V)";
            this.miEditPaste.Click += new System.EventHandler(this.miEditPaste_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(200, 6);
            // 
            // miEditFind
            // 
            this.miEditFind.Image = ((System.Drawing.Image)(resources.GetObject("miEditFind.Image")));
            this.miEditFind.Name = "miEditFind";
            this.miEditFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.miEditFind.Size = new System.Drawing.Size(203, 22);
            this.miEditFind.Text = "查找(&F)...";
            this.miEditFind.Click += new System.EventHandler(this.miEditFind_Click);
            // 
            // miEditReplace
            // 
            this.miEditReplace.Image = ((System.Drawing.Image)(resources.GetObject("miEditReplace.Image")));
            this.miEditReplace.Name = "miEditReplace";
            this.miEditReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.miEditReplace.Size = new System.Drawing.Size(203, 22);
            this.miEditReplace.Text = "替换(&R)...";
            this.miEditReplace.Click += new System.EventHandler(this.miEditReplace_Click);
            // 
            // miToggleBookmark
            // 
            this.miToggleBookmark.Image = ((System.Drawing.Image)(resources.GetObject("miToggleBookmark.Image")));
            this.miToggleBookmark.Name = "miToggleBookmark";
            this.miToggleBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
            this.miToggleBookmark.Size = new System.Drawing.Size(203, 22);
            this.miToggleBookmark.Text = "设置/取消书签";
            this.miToggleBookmark.Click += new System.EventHandler(this.miToggleBookmark_Click);
            // 
            // miGoToNextBookmark
            // 
            this.miGoToNextBookmark.Name = "miGoToNextBookmark";
            this.miGoToNextBookmark.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.miGoToNextBookmark.Size = new System.Drawing.Size(203, 22);
            this.miGoToNextBookmark.Text = "转到下一书签";
            this.miGoToNextBookmark.Click += new System.EventHandler(this.miGoToNextBookmark_Click);
            // 
            // miGoToPrevBookmark
            // 
            this.miGoToPrevBookmark.Name = "miGoToPrevBookmark";
            this.miGoToPrevBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F2)));
            this.miGoToPrevBookmark.Size = new System.Drawing.Size(203, 22);
            this.miGoToPrevBookmark.Text = "转到前一书签";
            this.miGoToPrevBookmark.Click += new System.EventHandler(this.miGoToPrevBookmark_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(200, 6);
            // 
            // miOption
            // 
            this.miOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSplitWindow,
            this.miShowLineNumbers,
            this.miHLCurRow,
            this.miBracketMatchingStyle,
            this.miSetFont,
            this.miFormatXml});
            this.miOption.Image = ((System.Drawing.Image)(resources.GetObject("miOption.Image")));
            this.miOption.Name = "miOption";
            this.miOption.Size = new System.Drawing.Size(203, 22);
            this.miOption.Text = "选项(&P)";
            // 
            // miSplitWindow
            // 
            this.miSplitWindow.Image = ((System.Drawing.Image)(resources.GetObject("miSplitWindow.Image")));
            this.miSplitWindow.Name = "miSplitWindow";
            this.miSplitWindow.Size = new System.Drawing.Size(164, 22);
            this.miSplitWindow.Text = "拆分窗口(&W)";
            this.miSplitWindow.Click += new System.EventHandler(this.miSplitWindow_Click);
            // 
            // miShowLineNumbers
            // 
            this.miShowLineNumbers.Name = "miShowLineNumbers";
            this.miShowLineNumbers.Size = new System.Drawing.Size(164, 22);
            this.miShowLineNumbers.Text = "显示行号(&L)";
            this.miShowLineNumbers.Click += new System.EventHandler(this.miShowLineNumbers_Click);
            // 
            // miHLCurRow
            // 
            this.miHLCurRow.Image = ((System.Drawing.Image)(resources.GetObject("miHLCurRow.Image")));
            this.miHLCurRow.Name = "miHLCurRow";
            this.miHLCurRow.Size = new System.Drawing.Size(164, 22);
            this.miHLCurRow.Text = "高亮当前行(&H)";
            this.miHLCurRow.Click += new System.EventHandler(this.miHLCurRow_Click);
            // 
            // miBracketMatchingStyle
            // 
            this.miBracketMatchingStyle.Name = "miBracketMatchingStyle";
            this.miBracketMatchingStyle.Size = new System.Drawing.Size(164, 22);
            this.miBracketMatchingStyle.Text = "高亮匹配括号(&A)";
            this.miBracketMatchingStyle.Visible = false;
            this.miBracketMatchingStyle.Click += new System.EventHandler(this.miBracketMatchingStyle_Click);
            // 
            // miSetFont
            // 
            this.miSetFont.Image = ((System.Drawing.Image)(resources.GetObject("miSetFont.Image")));
            this.miSetFont.Name = "miSetFont";
            this.miSetFont.Size = new System.Drawing.Size(164, 22);
            this.miSetFont.Text = "设置字体(&F)";
            this.miSetFont.Click += new System.EventHandler(this.miSetFont_Click);
            // 
            // miFormatXml
            // 
            this.miFormatXml.Name = "miFormatXml";
            this.miFormatXml.Size = new System.Drawing.Size(164, 22);
            this.miFormatXml.Text = "Xml格式化(&F)";
            // 
            // FrmTextDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 440);
            this.Controls.Add(this.textEditorControl);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmTextDoc";
            this.Text = "FrmTextDoc";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTextDoc_FormClosing);
            this.cmsEditor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControl textEditorControl;
        private System.Windows.Forms.ContextMenuStrip cmsEditor;
        private System.Windows.Forms.ToolStripMenuItem miOpenFile;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.ToolStripMenuItem miSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miEditCut;
        private System.Windows.Forms.ToolStripMenuItem miEditCopy;
        private System.Windows.Forms.ToolStripMenuItem miEditPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miEditFind;
        private System.Windows.Forms.ToolStripMenuItem miEditReplace;
        private System.Windows.Forms.ToolStripMenuItem miToggleBookmark;
        private System.Windows.Forms.ToolStripMenuItem miGoToNextBookmark;
        private System.Windows.Forms.ToolStripMenuItem miGoToPrevBookmark;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem miViewMode;
        private System.Windows.Forms.ToolStripMenuItem miOption;
        private System.Windows.Forms.ToolStripMenuItem miSplitWindow;
        private System.Windows.Forms.ToolStripMenuItem miShowLineNumbers;
        private System.Windows.Forms.ToolStripMenuItem miHLCurRow;
        private System.Windows.Forms.ToolStripMenuItem miBracketMatchingStyle;
        private System.Windows.Forms.ToolStripMenuItem miSetFont;
        private System.Windows.Forms.ToolStripMenuItem miFormatXml;
        private System.Windows.Forms.FontDialog fontDialog;
    }
}