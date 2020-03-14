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
    public partial class Form5 : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand com;
        OleDbDataAdapter da;
        DataSet ds;
        public Form5()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=techeasy.accdb; Jet OLEDB:Database Password=1234;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox5.Text == "") MessageBox.Show("Your system is unable edit.");
            else
            {
                DialogResult dialog = MessageBox.Show("Do you want to edit ?", "EDit Item Code", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    da = new OleDbDataAdapter("SELECT * FROM product WHERE item_code='" + this.textBox1.Text + "'", con);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0) MessageBox.Show("This Item Code is already exist. Please check it.");
                    else
                    {
                        con.Open();
                        com = new OleDbCommand("UPDATE product SET item_code='" + this.textBox1.Text + "' WHERE item_no=" + this.textBox5.Text + "", con);
                        com.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Record edited successfully.");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes) this.Close();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.textBox1.Text == "") this.dataGridView1.DataSource = "";
            else
            {
                da = new OleDbDataAdapter("SELECT item_code,descrip,hsn FROM product WHERE item_code LIKE '" + this.textBox1.Text + "%'", con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.dataGridView1.DataSource = ds.Tables[0];
                    this.dataGridView1.Columns[0].HeaderText = "Item Code";
                    this.dataGridView1.Columns[1].HeaderText = "Description";
                    this.dataGridView1.Columns[2].HeaderText = "HSN Code";
                }
                else this.dataGridView1.DataSource = "";
           }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rowdata = this.dataGridView1.Rows[e.RowIndex];
                da = new OleDbDataAdapter("SELECT * FROM product WHERE item_code='" + rowdata.Cells[0].Value.ToString() + "'", con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.textBox5.Text = ds.Tables[0].Rows[0][0].ToString();
                    this.textBox1.Text = ds.Tables[0].Rows[0][1].ToString();
                    this.textBox2.Text = ds.Tables[0].Rows[0][2].ToString();
                    this.textBox3.Text = ds.Tables[0].Rows[0][3].ToString();
                    this.textBox4.Text = ds.Tables[0].Rows[0][4].ToString();
                }
            }
            this.dataGridView1.DataSource = "";
        } 
    }
}
