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
    public partial class Form9 : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand com;
        OleDbDataAdapter da;
        DataSet ds;
        public Form9()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=techeasy.accdb; Jet OLEDB:Database Password=1234;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "") MessageBox.Show("New Password field cannot be blanked.");
            else if (this.textBox2.Text == "") MessageBox.Show("Confirm Password field cannot be blanked.");
            else
            {
                DialogResult dialog = MessageBox.Show("Do you want to change password ?", "Password Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    if (this.textBox1.Text == this.textBox2.Text)
                    {
                        con.Open();
                        com = new OleDbCommand("UPDATE passwizard SET user_pass='" + this.textBox1.Text + "'", con);
                        com.ExecuteNonQuery();
                        con.Close();
                        this.textBox1.Text = "";
                        this.textBox2.Text = "";
                        MessageBox.Show("Password updated");
                        this.Close();
                        Form8 frm8 = new Form8();
                        frm8.Show();
                    }
                    else { MessageBox.Show("Confirm Password is not same with Password."); this.textBox2.Text = ""; this.textBox2.Select();}
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes) {this.Close();Form8 frm8 = new Form8();frm8.Show();}
        }
    }
}
