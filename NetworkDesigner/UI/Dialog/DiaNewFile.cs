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
    public partial class DiaNewFile : Form
    {
        public bool IsCancelled = true;
        /// <summary>
        /// 新建类别：文件夹或文件
        /// </summary>
        public string Category="";
        /// <summary>
        /// 新建文件类型，仅在新建类别为文件时有效
        /// </summary>
        public string FileType="";
        /// <summary>
        /// 文件或文件夹名
        /// </summary>
        public string ParamName="";
        public DiaNewFile()
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
            if (this.tbFileName.Text.Equals(""))
            {
                MessageBox.Show("请输入文件或文件夹名称");
                this.tbFileName.Select();
                return false;
            }
            this.ParamName = this.tbFileName.Text;
            return true;
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            if(!CheckInsert())
                return;
            foreach (Control c in this.gbType.Controls)
            {
                if (c is RadioButton)
                {
                    if ((c as RadioButton).Checked)
                    {
                        Category = c.Text;
                        break;
                    }
                }
            }
            foreach (Control c in this.gbType.Controls)
            {
                if (c is RadioButton)
                {
                    if ((c as RadioButton).Checked)
                    {
                        FileType = c.Text;
                        break;
                    }
                }
            }
            this.IsCancelled = false;
            this.Close();
        }
    }
}
