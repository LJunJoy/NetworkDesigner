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
    public partial class DiaNewProject : Form
    {
        public bool IsCancelled = true;
        /// <summary>
        /// 项目文件夹名
        /// </summary>
        public string ProjectName="";
        /// <summary>
        /// 项目保存路径，注意不包含项目文件夹名
        /// </summary>
        public string ProjectPath="";

        public DiaNewProject()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            IsCancelled = true;
            this.Close();
        }
        private bool CheckInsert()
        {
            if (String.IsNullOrWhiteSpace(this.tbProjectName.Text))
            {
                MessageBox.Show("请输入项目名称");
                this.tbProjectName.Select();
                return false;
            }
            if (String.IsNullOrWhiteSpace(this.tbProjectPath.Text))
            {
                MessageBox.Show("请输入项目路径");
                this.tbProjectPath.Select();
                return false;
            }
            if (System.IO.Directory.Exists(this.tbProjectPath.Text + "\\"
                + this.tbProjectName.Text))
            {
                MessageBox.Show("此目录下已有同名文件夹，请重新输入");
                this.tbProjectName.Select();
                return false;
            }
            return true;
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            if(!CheckInsert())
                return;
            this.ProjectName = this.tbProjectName.Text;
            this.ProjectPath = this.tbProjectPath.Text;
            this.IsCancelled = false;
            this.Close();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "请选择保存项目的目录";
                fbd.SelectedPath = NetworkDesigner.Beans.Common.AppSetting.MyDocumentPath;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.tbProjectPath.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
