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
    public partial class Form4 : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand com;
        OleDbCommand com1;
        OleDbDataAdapter da;
        OleDbDataAdapter da1;
        DataSet ds;
        public Form4()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=techeasy.accdb; Jet OLEDB:Database Password=1234;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox4.Text == "") MessageBox.Show("Your system is unable to edit.");
            else
            {
                DialogResult dialog = MessageBox.Show("Do you want to edit ?", "Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dialog==DialogResult.Yes)
                {              
                    string sql = "UPDATE product SET descrip='" + this.textBox2.Text + "',hsn='" + this.textBox3.Text + "',unit='" + this.comboBox1.Text + "' WHERE item_no=" + this.textBox4.Text + "";
                    con.Open();
                    com = new OleDbCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                    da = new OleDbDataAdapter("SELECT item_code,descrip,hsn,unit FROM product WHERE item_no=" + this.textBox4.Text + "", con);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.dataGridView2.DataSource = ds.Tables[0];
                        this.dataGridView2.Columns[0].HeaderText = "Item Code";
                        this.dataGridView2.Columns[1].HeaderText = "Description";
                        this.dataGridView2.Columns[2].HeaderText = "HSN Code";
                        this.dataGridView2.Columns[3].HeaderText = "Unit";
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes) this.Close();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.textBox1.Text == "") this.dataGridView1.DataSource = "";
            else
            {
                da = new OleDbDataAdapter("SELECT item_code,descrip FROM product WHERE item_code LIKE '" + this.textBox1.Text + "%'", con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.dataGridView1.DataSource = ds.Tables[0];
                    this.dataGridView1.Columns[0].HeaderText = "Item Code";
                    this.dataGridView1.Columns[1].HeaderText = "Description";
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
                    this.textBox4.Text = ds.Tables[0].Rows[0][0].ToString();
                    this.textBox1.Text = ds.Tables[0].Rows[0][1].ToString();
                    this.textBox2.Text = ds.Tables[0].Rows[0][2].ToString();
                    this.textBox3.Text = ds.Tables[0].Rows[0][3].ToString();
                    this.comboBox1.Text = ds.Tables[0].Rows[0][4].ToString();
                }
            }
            this.dataGridView1.DataSource = "";
         }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

       private void textBox8_KeyUp(object sender, KeyEventArgs e)
       {

       }

       private void textBox8_Layout(object sender, LayoutEventArgs e)
       {

       }
    }
}
