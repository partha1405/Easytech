using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Easytech
{
    public partial class Form8 : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand com;
        OleDbDataAdapter da;
        DataSet ds;
        public Form8()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=techeasy.accdb; Jet OLEDB:Database Password=1234;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            testpass();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {testpass();}
        }

        public void testpass()
        {
            da = new OleDbDataAdapter("SELECT * FROM passwizard WHERE user_pass='" + this.textBox1.Text + "'", con);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0) {this.Hide();MDIParent1 mdi1 = new MDIParent1();mdi1.Show();}
            else { MessageBox.Show("Enter valid password."); this.textBox1.Text = ""; this.textBox1.Select(); }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide(); Form9 frm9 = new Form9(); frm9.Show();
        }
    }
}
