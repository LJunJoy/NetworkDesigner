using NetworkDesigner.Utils.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkDesigner.UI.ToolWindow
{
    public partial class FrmOutput : FrmBase
    {
        string filepath = "";
        const string SaveAsFileFilter = "文本(*.txt)|*.txt|(*.*)|*.*";
        public FrmOutput(FrmMain _frmMain)
        {
            InitializeComponent();
            this.rtx.LanguageOption = RichTextBoxLanguageOptions.AutoFontSizeAdjust;//统一字体

            this.mainForm = _frmMain;
        }

        public void AppendText(string tx,Font font = null)
        {
            int p1 = this.rtx.Text.Length;
            this.rtx.AppendText(tx);
            this.rtx.ScrollToCaret();
            if (font != null)
            {
                int p2 = this.rtx.Text.Length;
                SetSelecTextFont(p1, p2 - p1, font);
            }
        }

        public void SetSelecTextFont(int start, int length, Font font)
        {
            try
            {
                Font cur = rtx.SelectionFont;
                rtx.Select(start, length);
                rtx.SelectionFont = font;

                rtx.Select(start + length, 0);
                rtx.SelectionFont = cur;
            }
            catch (Exception e)
            {
                LogHelper.LogError(e);
            }
        }

        public void AppendText(string tx, Color color)
        {
            int p1 = this.rtx.Text.Length;
            this.rtx.AppendText(tx);
            this.rtx.ScrollToCaret();
            int p2 = this.rtx.Text.Length;
            SetSelecTextColor(p1, p2 - p1, color);
        }
        public void SetSelecTextColor(int start, int length, Color color)
        {
            try
            {
                Color cur = rtx.SelectionColor;
                rtx.Select(start, length);
                rtx.SelectionColor = color;
                
                rtx.Select(start + length, 0);
                rtx.SelectionColor = cur;
            }
            catch (Exception e)
            {
                LogHelper.LogError(e);
            }
        }

        /// <summary>
        /// 追加保存那些未保存的部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miAppendFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO");//按颜色或字体区分哪些已经保存，哪些没有保存
        }

        private void miSaveFile_Click(object sender, EventArgs e)
        {
            if ("".Equals(filepath))
            {
                miSaveAsFile_Click(null, null);
            }
            else
            {
                try
                {
                    System.IO.File.WriteAllText(filepath, rtx.Text);
                    SetSelecTextColor(0, rtx.Text.Length, Color.DarkBlue);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存文件失败，详情查看日志\r\n" + filepath);
                    LogHelper.LogError(ex);
                }
            }
        }

        private void miSaveAsFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = SaveAsFileFilter;
                dialog.InitialDirectory = this.mainForm.GetProjectPath();

                if (DialogResult.OK == dialog.ShowDialog())
                {
                    filepath = dialog.FileName;
                    try
                    {
                        System.IO.File.WriteAllText(filepath, rtx.Text);
                        SetSelecTextColor(0, rtx.Text.Length, Color.DarkBlue);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("保存文件失败，详情查看日志\r\n" + filepath);
                        LogHelper.LogError(ex);
                    }
                }
            }
        }

        private void miClear_Click(object sender, EventArgs e)
        {
            this.rtx.Clear();
        }

        private void miSelectAll_Click(object sender, EventArgs e)
        {
            this.rtx.SelectAll();
        }

        private void miEditCut_Click(object sender, EventArgs e)
        {
            if (this.rtx.SelectedText.Length > 0)
            {
                this.rtx.Cut();
            }
        }

        private void miEditCopy_Click(object sender, EventArgs e)
        {
            // 判断是否选中文本
            if (this.rtx.SelectedText.Equals(""))
                return;
            Clipboard.SetDataObject(this.rtx.SelectedText, true);
        }

        private void miEditPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                this.rtx.Paste();
            }
        }

        private void miUndo_Click(object sender, EventArgs e)
        {
            this.rtx.Undo();
        }

        private void miRedo_Click(object sender, EventArgs e)
        {
            this.rtx.Redo();
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            if (this.rtx.SelectedText.Length > 0)
            {
                this.rtx.SelectedText = "";
            }
        }
    }
}
