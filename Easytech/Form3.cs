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
    public partial class Form3 : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand com;
        OleDbCommand com1;
        OleDbDataAdapter da;
        OleDbDataAdapter da1;
        DataSet ds;
        public Form3()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=techeasy.accdb; Jet OLEDB:Database Password=1234;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text=="") MessageBox.Show("Enter Item Code.");
            else if (this.textBox2.Text=="") MessageBox.Show("Enter Description.");
            else if (this.textBox3.Text=="") MessageBox.Show("Enter HSN Code.");
            else if (this.comboBox1.Text=="") MessageBox.Show("Select unit.");
            else
            {
            DialogResult dialog=MessageBox.Show("Do you want to insert ?","Insert",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dialog==DialogResult.Yes)
            {
            da = new OleDbDataAdapter("SELECT * FROM product WHERE item_code='" + this.textBox1.Text + "'", con);
            ds = new DataSet();
            da.Fill(ds, "code");
            if (ds.Tables["code"].Rows.Count > 0) MessageBox.Show("This Quotation No is already exist.");
            else
            {
                string sql = "INSERT INTO product(item_code,descrip,hsn,unit) VALUES('" + this.textBox1.Text + "','" + this.textBox2.Text + "','" + this.textBox3.Text + "','" + this.comboBox1.Text + "')";
                con.Open();
                com = new OleDbCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
                da1 = new OleDbDataAdapter("SELECT item_code,descrip,hsn,unit FROM product", con);
                product();
                MessageBox.Show("Record inserted successfully");
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.comboBox1.Text = "";
                this.textBox1.Select();
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
                da1 = new OleDbDataAdapter("SELECT item_code,descrip,hsn,unit FROM product WHERE item_code LIKE '" + this.textBox1.Text + "%'", con);
                product();
           }
        }

        public void product()
        {
            ds = new DataSet();
            da1.Fill(ds, "product");
            if (ds.Tables["product"].Rows.Count > 0)
            {
                this.dataGridView1.DataSource = ds.Tables["product"];
                this.dataGridView1.Columns[0].HeaderText = "Item Code";
                this.dataGridView1.Columns[1].HeaderText = "Description";
                this.dataGridView1.Columns[2].HeaderText = "HSN Code";
                this.dataGridView1.Columns[3].HeaderText = "Unit";
            }
            else this.dataGridView1.DataSource = "";
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
        
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
