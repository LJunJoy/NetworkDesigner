namespace NetworkDesigner.UI.ToolWindow
{
    partial class FrmProperty
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
            this.propertyEditor1 = new Syncfusion.Windows.Forms.Diagram.Controls.PropertyEditor(this.components);
            this.SuspendLayout();
            // 
            // propertyEditor1
            // 
            this.propertyEditor1.BackColor = System.Drawing.SystemColors.Control;
            this.propertyEditor1.Diagram = null;
            this.propertyEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyEditor1.Location = new System.Drawing.Point(0, 0);
            this.propertyEditor1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.propertyEditor1.Name = "propertyEditor1";
            this.propertyEditor1.Size = new System.Drawing.Size(231, 247);
            this.propertyEditor1.TabIndex = 0;
            // 
            // FrmProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(231, 247);
            this.Controls.Add(this.propertyEditor1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmProperty";
            this.Text = "FrmProperty";
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Diagram.Controls.PropertyEditor propertyEditor1;
    }
}