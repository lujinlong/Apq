using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Apq_MP4
{
    public partial class FormMain : Apq.Windows.Forms.ImeForm
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void 批量重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rename Rename = new Rename();
            Rename.MdiParent = this;
            Rename.Show();
        }
    }
}