using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkDesigner.UI.Dialog
{
    public partial class DiaSaveToPalette : Form
    {
        public string FilePath
        {
            get { return this.comboBox1.Text; }
        }

        private string SaveFileFilter = "工具面板(*.pat)|*.pat";

        public DiaSaveToPalette()
        {
            InitializeComponent();

            this.DialogResult = DialogResult.Cancel;
        }

        public void PopulateList(List<string> files)
        {
            foreach (string str in files)
            {
                this.comboBox1.Items.Add(str); 
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入正确的文件名称", "提示");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = SaveFileFilter;
                dialog.InitialDirectory = NetworkDesigner.Beans.Common.AppSetting.DefaultModelDirPath;
                if (DialogResult.OK == dialog.ShowDialog())
                {
                    this.comboBox1.Text = dialog.FileName;
                }
            }
        }
    }
}
