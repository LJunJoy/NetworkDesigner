namespace NetworkDesigner.UI.Dialog
{
    partial class DiaNewProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiaNewProject));
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.gbParam = new System.Windows.Forms.GroupBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.tbProjectName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbProjectPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbParam.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(341, 134);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 26);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(444, 134);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 26);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // gbParam
            // 
            this.gbParam.Controls.Add(this.btBrowse);
            this.gbParam.Controls.Add(this.tbProjectName);
            this.gbParam.Controls.Add(this.label1);
            this.gbParam.Controls.Add(this.tbProjectPath);
            this.gbParam.Controls.Add(this.label3);
            this.gbParam.Location = new System.Drawing.Point(22, 12);
            this.gbParam.Name = "gbParam";
            this.gbParam.Size = new System.Drawing.Size(502, 116);
            this.gbParam.TabIndex = 3;
            this.gbParam.TabStop = false;
            this.gbParam.Text = "参数";
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(421, 65);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(75, 26);
            this.btBrowse.TabIndex = 4;
            this.btBrowse.Text = "浏览";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // tbProjectName
            // 
            this.tbProjectName.Location = new System.Drawing.Point(98, 26);
            this.tbProjectName.Name = "tbProjectName";
            this.tbProjectName.Size = new System.Drawing.Size(306, 23);
            this.tbProjectName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "项目名称";
            // 
            // tbProjectPath
            // 
            this.tbProjectPath.Location = new System.Drawing.Point(98, 67);
            this.tbProjectPath.Name = "tbProjectPath";
            this.tbProjectPath.Size = new System.Drawing.Size(306, 23);
            this.tbProjectPath.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "项目路径";
            // 
            // DiaNewProject
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 178);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.gbParam);
            this.Controls.Add(this.btOK);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DiaNewProject";
            this.Text = "新建";
            this.gbParam.ResumeLayout(false);
            this.gbParam.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.GroupBox gbParam;
        private System.Windows.Forms.TextBox tbProjectPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbProjectName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btBrowse;
    }
}