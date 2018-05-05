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
    public partial class DiaSplashScreen : Form
    {
        public DiaSplashScreen()
        {
            InitializeComponent();
            this.ClientSize = this.BackgroundImage.Size;
        }
    }
}
