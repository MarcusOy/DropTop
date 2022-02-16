using DropTop.Renderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DropTop
{
    public partial class OverlayForm : Form
    {

        public OverlayForm()
        {
            InitializeComponent();
        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.TransparencyKey = this.BackColor = Color.Coral;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            this.TopMost = true;
            this.AllowTransparency = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
        }
    }
}
