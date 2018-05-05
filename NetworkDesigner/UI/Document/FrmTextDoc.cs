using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using NetworkDesigner.Beans.Common;
using System.Xml;
using NetworkDesigner.Utils.Common;

namespace NetworkDesigner.UI.Document
{
    /// <summary>
    /// TODO:实现关闭前询问保存
    /// </summary>
    public partial class FrmTextDoc : FrmDocBase
    {
        const string SharpPadFileFilter = "xml文件(*.xml)|*.xml|全部类型(*.*)|*.*";
        const string SaveAsFileFilter = "全部类型(*.*)|*.*|xml文件(*.xml)|*.xml";
        FindAndReplaceForm _findForm = new FindAndReplaceForm();
        
        private bool _IsEditorModified;
        public bool IsEditorModified 
        {
            get { return _IsEditorModified; }
            set { _IsEditorModified = value; }
        }
        /// <summary>
        /// 用于屏蔽在加载文件过程中触发 textchanged 事件
        /// </summary>
        private bool isFileLoading = false;

        /// <summary>Returns the currently displayed editor, or null if none are open</summary>
        private TextEditorControl ActiveEditor
        {
            get
            {
                //if (fileTabs.TabPages.Count == 0) return null;
                //return fileTabs.SelectedTab.Controls.OfType<TextEditorControl>().FirstOrDefault();
                return this.textEditorControl;
            }
        }

        /// <summary>This variable holds the settings (whether to show line numbers, 
        /// etc.) that all editor controls share.</summary>
        ITextEditorProperties _editorSettings;

        public FrmTextDoc(FrmMain frm)
        {
            InitializeComponent();

            this.mainForm = frm;
            InitForm();
        }

        private void InitForm()
        {
            ActiveEditor.Encoding = new System.Text.UTF8Encoding(false);//不带bom的utf-8 //Encoding.GetEncoding("GB2312");
            ActiveEditor.Font = new Font(ActiveEditor.Font.FontFamily, 11);

            List<string> syntaxModes = AppSetting.EditorSyntaxModes;

            foreach (string key in syntaxModes)
            {
                ToolStripMenuItem miNewMode = new ToolStripMenuItem();
                miNewMode.Name = "mi" + key;
                //this.miNewMode.Size = new System.Drawing.Size(174, 22);
                miNewMode.Text = key;
                miNewMode.Tag = key;
                miNewMode.Click += new System.EventHandler(
                    delegate(object sender, EventArgs e)
                    {
                        ToolStripMenuItem miThis = sender as ToolStripMenuItem;
                        if (miThis != null && miThis.Tag != null)
                        {
                            ToolStripDropDownMenu owner = miThis.Owner as ToolStripDropDownMenu;
                            if (owner != null && owner.Items.Count > 0)
                            {
                                foreach (ToolStripMenuItem mi in owner.Items)
                                {
                                    mi.Checked = false;
                                }
                                miThis.Checked = true;
                            }
                            if (ActiveEditor != null)
                            {
                                ActiveEditor.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(miThis.Tag as string);
                            }
                        }
                    });
                //if (key == "XML")
                //{
                //    miNewMode.Checked = true;
                //    ActiveEditor.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
                //}
                this.miViewMode.DropDownItems.Add(miNewMode);
            }

            if (_editorSettings == null)
            {
                _editorSettings = ActiveEditor.TextEditorProperties;
                OnSettingsChanged();
            }
            else
                ActiveEditor.TextEditorProperties = _editorSettings;

            //if (!(ActiveEditor.Document.FoldingManager.FoldingStrategy is XmlFoldingStrategy))
            //{
            //    ActiveEditor.Document.FoldingManager.FoldingStrategy = new XmlFoldingStrategy();
            //}
            //UpdateFolding();
        }
        
        #region 重写函数
        //这里不会执行更新DocInfo的操作，也即要么是全新加载，要么是在外面自行更新
        public override void LoadFile(DocInfo docInfo)
        {
            if (docInfo.T == DocInfo.DType.TXT
                || docInfo.T == DocInfo.DType.XML)
            {
                isFileLoading = true;
                if(!File.Exists(docInfo.filePath))
                {
                    MessageBox.Show("指定路径找不到该文件！\r\n" + docInfo.filePath);
                    return;
                } 
                try
                {
                    ActiveEditor.LoadFile(docInfo.filePath);
                }
                catch (Exception e)
                {
                    MessageBox.Show("文件加载失败，详情查看日志！");
                    LogHelper.LogError(e);
                    return;
                }
                CheckCurrentViewMode(ActiveEditor.Document.HighlightingStrategy.Name);
                if (docInfo.T == DocInfo.DType.XML)
                {
                    if (!(ActiveEditor.Document.FoldingManager.FoldingStrategy is XmlFoldingStrategy))
                    {
                        ActiveEditor.Document.FoldingManager.FoldingStrategy = new XmlFoldingStrategy();
                    }
                    UpdateFolding();
                }

                ResetTitleAfterSave();
                isFileLoading = false;
            }
            //else if (docInfo.T == DocInfo.DType.BLANK)
            //{
                //如果是空白文档，不作处理，由调用者自行显示空窗口
            //}
        }

        public override void SaveFile()
        {
            miSave_Click(null, null);
        }

        public override void SaveAsFile()
        {
            miSaveAs_Click(null, null);
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public override void VEditUndo()
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                editor.Undo();
            }
        }
        /// <summary>
        /// 恢复
        /// </summary>
        public override void VEditRedo()
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                editor.Redo();
            }
        }
        /// <summary>
        /// 复制
        /// </summary>
        public override void VEditCopy()
        {
            miEditCopy_Click(null, null);
        }
        /// <summary>
        /// 剪切
        /// </summary>
        public override void VEditCut()
        {
            miEditCut_Click(null,null);
        }
        /// <summary>
        /// 粘贴
        /// </summary>
        public override void VEditPaste()
        {
            miEditPaste_Click(null,null);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public override void VEditDelete() 
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                this.ActiveEditor.Focus();
                SendKeys.SendWait("{DELETE}");
            }
        }

        #endregion
        /// <summary>
        /// 检查文档是否修改，若修改提示是否保存
        /// </summary>
        private void FlushDocSafely()
        {
            if (this.IsEditorModified)
            {
                this.Select();
                DialogResult dr = MessageBox.Show("文件：" + this.docInfo.fileName + "\r\n已修改，是否保存？",
                     "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    TextEditorControl editor = ActiveEditor;
                    if (editor.FileName != null)
                    {
                        try
                        {
                            editor.SaveFile(editor.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("文件保存失败，详情查看日志！");
                            LogHelper.LogError(ex);
                            return;
                        }
                        ResetTitleAfterSave();
                    }
                    else
                    {
                        SaveAs();
                    }
                }
            }
        }
        /// <summary>
        /// 检查文档是否修改，若修改提示是否保存或者取消，若取消返回false
        /// </summary>
        /// <returns></returns>
        private bool FlushDocYesNoCancel()
        {
            if (this.IsEditorModified)
            {
                DialogResult dr = MessageBox.Show("文件已修改，是否保存？", "提示",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                    return false;
                if (dr == DialogResult.Yes)
                {
                    TextEditorControl editor = ActiveEditor;
                    if (editor.FileName != null)
                    {
                        try
                        {
                            editor.SaveFile(editor.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("文件保存失败，详情查看日志！");
                            LogHelper.LogError(ex);
                            return true;//不返回false是为了防止关闭不了窗口
                        }
                        ResetTitleAfterSave();
                    }
                    else
                    {
                        SaveAs();
                    }
                }
            }
            return true;
        }
        #region 菜单响应函数
        void miOpen_Click(object sender, EventArgs e)
        {
            if (!this.FlushDocYesNoCancel())
                return;
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = SharpPadFileFilter;
                    dialog.FilterIndex = 0;
                    if (DialogResult.OK == dialog.ShowDialog())
                    {
                        //这里认为通过菜单打开文档时总是要执行更新DocInfo的操作，也即不可能是全新加载
                        FactoryDocument.UpdateDocumentInfo(this.docInfo.filePath, dialog.FileName);
                        LoadFile(this.docInfo);
                    }
                }
            }
        }
        /// <summary>
        /// 保存文件，若已保存过直接返回
        /// </summary>
        void miSave_Click(object sender, System.EventArgs e)
        {
            if (!this.IsEditorModified)
                return;
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                if (editor.FileName != null && this.docInfo.T != DocInfo.DType.BLANK)
                {
                    try
                    {
                        editor.SaveFile(editor.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("文件保存失败，详情查看日志！");
                        LogHelper.LogError(ex);
                        return;
                    }
                    ResetTitleAfterSave();
                }
                else
                {
                    SaveAs();
                }
            }
        }
        void miSaveAs_Click(object sender, System.EventArgs e)
        {
            SaveAs();
        }

        private void miEditCut_Click(object sender, EventArgs e)
        {
            if (HaveSelection())
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Cut());
        }

        private void miEditCopy_Click(object sender, EventArgs e)
        {
            if (HaveSelection())
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Copy());
        }

        private void miEditPaste_Click(object sender, EventArgs e)
        {
            DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Paste());
        }

        private void miEditFind_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            _findForm.ShowFor(editor, false);
        }

        private void miEditReplace_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            _findForm.ShowFor(editor, true);
        }

        private void miToggleBookmark_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.ToggleBookmark());
                editor.IsIconBarVisible = editor.Document.BookmarkManager.Marks.Count > 0;
            }
        }

        private void miGoToNextBookmark_Click(object sender, EventArgs e)
        {
            DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.GotoNextBookmark(new Predicate<Bookmark>(delegate(Bookmark bookmark)
            {
                return true;
            })));
        }

        private void miGoToPrevBookmark_Click(object sender, EventArgs e)
        {
            DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.GotoPrevBookmark(new Predicate<Bookmark>(delegate(Bookmark bookmark)
            {
                return true;
            })));
        }

        private void miSplitWindow_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.Split();
            OnSettingsChanged();
        }

        private void miShowLineNumbers_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.ShowLineNumbers = !editor.ShowLineNumbers;
            OnSettingsChanged();
        }

        private void miHLCurRow_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.LineViewerStyle = editor.LineViewerStyle == LineViewerStyle.None
                ? LineViewerStyle.FullRow : LineViewerStyle.None;
            OnSettingsChanged();
        }

        private void miBracketMatchingStyle_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.BracketMatchingStyle = editor.BracketMatchingStyle == BracketMatchingStyle.After
                ? BracketMatchingStyle.Before : BracketMatchingStyle.After;
            OnSettingsChanged();
        }

        private void miSetFont_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                fontDialog.Font = editor.Font;
                if (fontDialog.ShowDialog(this) == DialogResult.OK)
                {
                    editor.Font = fontDialog.Font;
                    OnSettingsChanged();
                }
            }
        }

        private void miFormatXml_Click(object sender, EventArgs e)
        {
            try
            {
                FormatXml(ActiveEditor.Document.TextContent);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(String.Format("Xml格式化失败：{0}", ex.ToString()));
            }
        }

        private void textEditorControl_TextChanged(object sender, EventArgs e)
        {
            if (isFileLoading) //加载文档时会触发 textEditorControl_TextChanged
                return;
            if (!IsEditorModified)
            {
                IsEditorModified = true;

                this.Text += "*";      //窗体标题
            }
        }

        private void FrmTextDoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.mainForm.m_isExiting) //主程序退出，在这之前已经处理过是否保存，并且这里不能再抛出异常
            {
                return;
            }
            else if (e.CloseReason == CloseReason.MdiFormClosing)//点击父窗体关闭时，子窗体没有取消选项，只有是和否
            {
                this.FlushDocSafely();
                //这里不执行移除文档操作，由父窗体确定是否真正移除（确认退出程序时）
            }
            else //直接点击子窗体的关闭按钮，如果确定关闭要在这里移除文档
            {
                if (!FlushDocYesNoCancel())
                    e.Cancel = true;
                else
                {
                    this.m_isDisposing = true;
                    FactoryDocument.RemoveDocument(this.docInfo.filePath);
                }
            }
        }

        #endregion

        #region 功能函数
        /// <summary>Show current settings on the Options menu</summary>
        /// <remarks>We don't have to sync settings between the editors because 
        /// they all share the same DefaultTextEditorProperties object.</remarks>
        private void OnSettingsChanged()
        {
            this.miSplitWindow.Checked = ActiveEditor.IsSplited;
            this.miHLCurRow.Checked = _editorSettings.LineViewerStyle == LineViewerStyle.FullRow;
            this.miBracketMatchingStyle.Checked = _editorSettings.BracketMatchingStyle == BracketMatchingStyle.After;
            this.miShowLineNumbers.Checked = _editorSettings.ShowLineNumbers;
        }
        /// <summary>
        /// Replaces the entire text of the xml view with the xml in the
        /// specified. The xml will be formatted.
        /// </summary>
        public void FormatXml(string xml)
        {
            string formattedXml = IndentedFormat(SimpleFormat(IndentedFormat(xml)));
            ActiveEditor.Document.Replace(0, ActiveEditor.Document.TextLength, formattedXml);
            UpdateFolding();
        }
        /// <summary>
        /// Returns a formatted xml string using a simple formatting algorithm.
        /// </summary>
        static string SimpleFormat(string xml)
        {
            return xml.Replace("><", ">\r\n<");
        }
        /// <summary>
        /// Returns a pretty print version of the given xml.
        /// </summary>
        /// <param name="xml">Xml string to pretty print.</param>
        /// <returns>A pretty print version of the specified xml.  If the
        /// string is not well formed xml the original string is returned.
        /// </returns>
        string IndentedFormat(string xml)
        {
            string indentedText = String.Empty;

            try
            {
                XmlTextReader reader = new XmlTextReader(new StringReader(xml));
                reader.WhitespaceHandling = WhitespaceHandling.None;

                StringWriter indentedXmlWriter = new StringWriter();
                XmlTextWriter writer = CreateXmlTextWriter(indentedXmlWriter);
                writer.WriteNode(reader, false);
                writer.Flush();

                indentedText = indentedXmlWriter.ToString();
            }
            catch (Exception)
            {
                indentedText = xml;
            }

            return indentedText;
        }

        XmlTextWriter CreateXmlTextWriter(TextWriter textWriter)
        {
            XmlTextWriter writer = new XmlTextWriter(textWriter);
            if (ActiveEditor.TextEditorProperties.ConvertTabsToSpaces)
            {
                writer.Indentation = ActiveEditor.TextEditorProperties.IndentationSize;
                writer.IndentChar = ' ';
            }
            else
            {
                writer.Indentation = 1;
                writer.IndentChar = '\t';
            }
            writer.Formatting = Formatting.Indented;
            return writer;
        }
        /// <summary>Performs an action encapsulated in IEditAction.</summary>
        /// <remarks>
        /// There is an implementation of IEditAction for every action that 
        /// the user can invoke using a shortcut key (arrow keys, Ctrl+X, etc.)
        /// The editor control doesn't provide a public funciton to perform one
        /// of these actions directly, so I wrote DoEditAction() based on the
        /// code in TextArea.ExecuteDialogKey(). You can call ExecuteDialogKey
        /// directly, but it is more fragile because it takes a Keys value (e.g.
        /// Keys.Left) instead of the action to perform.
        /// <para/>
        /// Clipboard commands could also be done by calling methods in
        /// editor.ActiveTextAreaControl.TextArea.ClipboardHandler.
        /// </remarks>
        private void DoEditAction(TextEditorControl editor, ICSharpCode.TextEditor.Actions.IEditAction action)
        {
            if (editor != null && action != null)
            {
                TextArea area = editor.ActiveTextAreaControl.TextArea;
                editor.BeginUpdate();
                try
                {
                    lock (editor.Document)
                    {
                        action.Execute(area);
                        if (area.SelectionManager.HasSomethingSelected && area.AutoClearSelection /*&& caretchanged*/)
                        {
                            if (area.Document.TextEditorProperties.DocumentSelectionMode == DocumentSelectionMode.Normal)
                            {
                                area.SelectionManager.ClearSelection();
                            }
                        }
                    }
                }
                finally
                {
                    editor.EndUpdate();
                    area.Caret.UpdateCaretPosition();
                }
            }
        }
        private bool HaveSelection()
        {
            TextEditorControl editor = ActiveEditor;
            return editor != null &&
                editor.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected;
        }
        private void CheckCurrentViewMode(string modeName)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                if (editor.Document.HighlightingStrategy != null && this.miViewMode.DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem mi in this.miViewMode.DropDownItems)
                    {
                        if (mi.Tag != null && mi.Tag.ToString().Equals(modeName, StringComparison.OrdinalIgnoreCase))
                        {
                            mi.Checked = true;
                        }
                        else
                        {
                            mi.Checked = false;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Forces the editor to update its folds.
        /// </summary>
        void UpdateFolding()
        {
            ActiveEditor.Document.FoldingManager.UpdateFoldings(String.Empty, null);
            ActiveEditor.ActiveTextAreaControl.TextArea.Refresh();
        }
        private void SaveAs()
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = SaveAsFileFilter;
                    //dialog.FilterIndex = 0;
                    dialog.InitialDirectory = this.mainForm.GetProjectPath();
                    dialog.FileName = this.docInfo.fileName;
                    if (DialogResult.OK == dialog.ShowDialog())
                    {
                        try
                        {
                            editor.SaveFile(dialog.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("文件另存失败，详情查看日志！");
                            LogHelper.LogError(ex);
                            return;
                        }
                        editor.FileName = dialog.FileName;
                        
                        FactoryDocument.UpdateDocumentInfo(this.docInfo.filePath, editor.FileName);
                        ResetTitleAfterSave();
                    }
                }
            }
        }

        /// <summary>
        /// 文档内容得到保存等处理后调用
        /// </summary>
        public void ResetTitleAfterSave()
        {
            IsEditorModified = false;
            if (this.Text.EndsWith("*"))
                this.Text = this.Text.Substring(0, this.Text.Length - 1);
        }

        #endregion
    }
}
