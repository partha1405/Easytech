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
    public partial class Form2 : Form
    {
        OleDbConnection con=new OleDbConnection();
        OleDbCommand com;
        OleDbDataAdapter da;
        DataSet ds;
        public Form2()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=techeasy.accdb; Jet OLEDB:Database Password=1234;";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.textBox1.Select();
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox13.Text == "") MessageBox.Show("Your system is unable to edit data.");
            else
            {
                DialogResult dialog = MessageBox.Show("Do you want to edit data ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    string sql = "UPDATE custdetail SET cust_name='" + this.textBox1.Text + "',cust_address='" + this.textBox2.Text + "',cust_phone='" + this.textBox3.Text + "',cust_fax='" + this.textBox4.Text + "',cust_mobile='" + this.textBox5.Text + "',cust_email='" + this.textBox6.Text + "',cust_pan='" + this.textBox8.Text + "',bank1='" + this.textBox11.Text + "',bank2='" + this.textBox12.Text + "' WHERE cust_no=" + this.textBox13.Text + "";
                    con.Open();
                    com = new OleDbCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record updated successfully.");
                    this.textBox1.Select();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox13.Text == "") MessageBox.Show("Your system is unable to delete the record.");
           else
            {
                DialogResult dialog = MessageBox.Show("Do you want to delete this record ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    con.Open();
                    com = new OleDbCommand("DELETE FROM custdetail where cust_no=" + this.textBox13.Text + "", con);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record deleted.");
                    this.textBox13.Text = "";
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                    this.textBox3.Text = "";
                    this.textBox4.Text = "";
                    this.textBox5.Text = "";
                    this.textBox6.Text = "";
                    this.textBox7.Text = "";
                    this.textBox8.Text = "";
                    this.textBox9.Text = "";
                    this.textBox10.Text = "";
                    this.textBox11.Text = "";
                    this.textBox12.Text = "";
                    this.textBox1.Select();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes) this.Close();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.textBox1.Text == "") this.dataGridView1.DataSource = "";              
            else
            {
                da = new OleDbDataAdapter("select cust_name,cust_address,cust_gst from custdetail where cust_name like '" + this.textBox1.Text + "%'", con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.dataGridView1.DataSource = ds.Tables[0];
                    this.dataGridView1.Columns[0].HeaderText = "Customer's Name";
                    this.dataGridView1.Columns[1].HeaderText = "Customer's Address";
                    this.dataGridView1.Columns[2].HeaderText = "GSTIN";
                }
                else this.dataGridView1.DataSource = "";
           }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rowdata = this.dataGridView1.Rows[e.RowIndex];
                da=new OleDbDataAdapter("select * from custdetail where cust_name='" + rowdata.Cells[0].Value.ToString() + "' AND cust_address='" + rowdata.Cells[1].Value.ToString() + "' AND cust_gst='" + rowdata.Cells[2].Value.ToString() + "'",con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.textBox13.Text = ds.Tables[0].Rows[0][0].ToString();
                    this.textBox1.Text = ds.Tables[0].Rows[0][1].ToString();
                    this.textBox2.Text = ds.Tables[0].Rows[0][2].ToString();
                    this.textBox3.Text = ds.Tables[0].Rows[0][3].ToString();
                    this.textBox4.Text = ds.Tables[0].Rows[0][4].ToString();
                    this.textBox5.Text = ds.Tables[0].Rows[0][5].ToString();
                    this.textBox6.Text = ds.Tables[0].Rows[0][6].ToString();
                    this.textBox7.Text = ds.Tables[0].Rows[0][7].ToString();
                    this.textBox8.Text = ds.Tables[0].Rows[0][8].ToString();
                    this.textBox9.Text = ds.Tables[0].Rows[0][9].ToString();
                    this.textBox10.Text = ds.Tables[0].Rows[0][10].ToString();
                    this.textBox11.Text = ds.Tables[0].Rows[0][11].ToString();
                    this.textBox12.Text = ds.Tables[0].Rows[0][12].ToString();
                }
            }
            this.dataGridView1.DataSource = "";
          }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            number_only(sender, e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            number_only(sender, e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            number_only(sender, e);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            this.textBox8.Text = this.textBox8.Text.ToUpper();
            this.textBox8.SelectionStart = this.textBox8.Text.Length;
        }

        public void number_only(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {e.Handled = true;MessageBox.Show("Enter number only.");}
        }

        private void textBox13_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox8_KeyUp(object sender, KeyEventArgs e)
        {

        } 
    }
}
