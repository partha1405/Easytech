using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Easytech
{
    public partial class MDIParent1 : Form
    {
        public MDIParent1()
        {
            InitializeComponent();
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void nEWREGISTRATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();frm1.MdiParent = this;frm1.Show();
        }

        private void sEARCHCUSTOMERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();frm2.MdiParent = this;frm2.Show();
        }

        private void eDITGSTNOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form10 frm10 = new Form10();frm10.MdiParent = this;frm10.Show();
        }

        private void nEWPRODUCTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();frm3.MdiParent = this;frm3.Show();
        }

        private void eDITPRODUCTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();frm4.MdiParent = this;frm4.Show();
        }

        private void eDITITEMCODEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5();frm5.MdiParent = this;frm5.Show();
        }

        private void nEWQUOTATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();frm6.MdiParent = this;frm6.Show();
        }

        private void nEWQUOTATIONENTRYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7();frm7.MdiParent = this;frm7.Show();
        }

        private void eDITQUOTATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form12 frm12 = new Form12();frm12.MdiParent = this;frm12.Show();
        }

        private void eDITQUOTATIONENTRYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form13 frm13 = new Form13();frm13.MdiParent = this;frm13.Show();
        }

        private void oLDRECORDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form11 frm11 = new Form11();frm11.MdiParent = this;frm11.Show();
        }

        private void nEWORDERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();Form8 frm8 = new Form8();frm8.Show();
        }
    }
}
