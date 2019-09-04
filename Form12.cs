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
    public partial class Form12 : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand com;
        OleDbDataAdapter da;
        OleDbDataAdapter da1;
        OleDbDataAdapter da2;
        DataSet ds;
        public Form12()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=techeasy.accdb; Jet OLEDB:Database Password=1234;";
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            this.dateTimePicker2.CustomFormat = "dd-MM-yyyy";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox9.Text == "" || this.textBox10.Text=="") MessageBox.Show("Your system is unable to edit record.");
            else if (this.textBox1.Text == "") MessageBox.Show("Enter quotation no.");
             else if (this.textBox2.Text == "") MessageBox.Show("Enter enquiry no.");
             else
             {
                 DialogResult dialog = MessageBox.Show("Do you want to edit ?", "Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dialog == DialogResult.Yes)
                 {
                     da = new OleDbDataAdapter("SELECT * FROM term WHERE enqu_no='" + this.textBox2.Text + "'", con);
                     ds = new DataSet();
                     da.Fill(ds, "enquiry");
                    if (ds.Tables["enquiry"].Rows.Count > 0) MessageBox.Show("This Enquiry No is already exist.");
                     else
                    {
                       
                        string sql = "UPDATE term SET enqu_no='" + this.textBox2.Text + "',enqu_dt='" + this.dateTimePicker2.Text + "',cust_no=" + this.textBox10.Text +",price='" + this.textBox3.Text +"',validity='" + this.textBox4.Text +"',guran='" + this.textBox5.Text + "',inspect='" + this.textBox6.Text +"',gst='" + this.textBox7.Text + "',discount='" + this.textBox8.Text + "' WHERE qut_no=" + this.textBox9.Text +"";
                        con.Open();
                        com = new OleDbCommand(sql, con);
                        com.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data edited successfully.");
                     } 
                 }
             }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox9.Text == "" || this.textBox10.Text == "") MessageBox.Show("Your system is unable to delete record.");
            else
            {
                DialogResult dialog = MessageBox.Show("Do you want to delete ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    con.Open();
                    com = new OleDbCommand("DELETE FROM term WHERE qut_no=" + this.textBox9.Text + "", con);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record deleted");
                    this.textBox9.Text = "";
                    this.textBox10.Text = "";
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                    this.textBox3.Text = "";
                    this.textBox4.Text = "";
                    this.textBox5.Text = "";
                    this.textBox6.Text = "";
                    this.textBox7.Text = "";
                    this.textBox8.Text = "";
                    this.textBox11.Text = "";
                    this.textBox12.Text = "";
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
                da = new OleDbDataAdapter("SELECT quot_no,quot_dt,enqu_no,enqu_dt FROM term WHERE quot_no LIKE '" + this.textBox1.Text + "%'", con);
                quten();
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.textBox2.Text == "") this.dataGridView1.DataSource = "";
            else
            {
                da = new OleDbDataAdapter("SELECT quot_no,quot_dt,enqu_no,enqu_dt FROM term WHERE enqu_no LIKE '" + this.textBox2.Text + "%'", con);
                quten();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow endata = this.dataGridView1.Rows[e.RowIndex];
                da = new OleDbDataAdapter("SELECT t.qut_no,t.quot_no,t.quot_dt,t.enqu_no,t.enqu_dt,t.price,t.validity,t.guaran,t.inspect,t.gst,t.discount,c.cust_no,c.cust_name,c.cust_gst FROM term t,custdetail c WHERE c.cust_no=t.cust_no AND quot_no='" + endata.Cells[0].Value.ToString() + "' AND enqu_no='" + endata.Cells[2].Value.ToString() + "'", con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.textBox9.Text = ds.Tables[0].Rows[0][0].ToString();
                    this.textBox1.Text = ds.Tables[0].Rows[0][1].ToString();
                    this.dateTimePicker1.Text = ds.Tables[0].Rows[0][2].ToString();
                    this.textBox2.Text = ds.Tables[0].Rows[0][3].ToString();
                    this.dateTimePicker2.Text = ds.Tables[0].Rows[0][4].ToString();
                    this.textBox3.Text = ds.Tables[0].Rows[0][5].ToString();
                    this.textBox4.Text = ds.Tables[0].Rows[0][6].ToString();
                    this.textBox5.Text = ds.Tables[0].Rows[0][7].ToString();
                    this.textBox6.Text = ds.Tables[0].Rows[0][8].ToString();
                    this.textBox7.Text = ds.Tables[0].Rows[0][9].ToString();
                    this.textBox8.Text = ds.Tables[0].Rows[0][10].ToString();
                    this.textBox10.Text = ds.Tables[0].Rows[0][11].ToString();
                    this.textBox11.Text = ds.Tables[0].Rows[0][12].ToString();
                    this.textBox12.Text = ds.Tables[0].Rows[0][13].ToString();
                }
            }
            this.dataGridView1.DataSource = "";
        }

        private void textBox11_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.textBox11.Text == "") this.dataGridView2.DataSource = "";
            else
            {
                da = new OleDbDataAdapter("SELECT cust_name,cust_address,cust_gst FROM custdetail WHERE cust_name LIKE '" + this.textBox11.Text + "%'", con);
                custfill();
            }
        }

        private void textBox12_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.textBox12.Text == "") this.dataGridView2.DataSource = "";
            else
            {
                da = new OleDbDataAdapter("SELECT cust_name,cust_address,cust_gst FROM custdetail WHERE cust_gst LIKE '" + this.textBox12.Text + "%'", con);
                custfill();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rowdata = this.dataGridView2.Rows[e.RowIndex];
                da = new OleDbDataAdapter("SELECT cust_no,cust_name,cust_gst FROM custdetail WHERE cust_name='" + rowdata.Cells[0].Value.ToString() + "' AND cust_address='" + rowdata.Cells[1].Value.ToString() + "' AND cust_gst='" + rowdata.Cells[2].Value.ToString() + "'", con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.textBox10.Text = ds.Tables[0].Rows[0][0].ToString();
                    this.textBox11.Text = ds.Tables[0].Rows[0][1].ToString();
                    this.textBox12.Text = ds.Tables[0].Rows[0][2].ToString();
                }
            }
            this.dataGridView2.DataSource = "";
        }

        public void quten()
        {
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.dataGridView1.DataSource = ds.Tables[0];
                this.dataGridView1.Columns[0].HeaderText = "Quotation NO";
                this.dataGridView1.Columns[1].HeaderText = "Quotation Date";
                this.dataGridView1.Columns[2].HeaderText = "Enquiry NO";
                this.dataGridView1.Columns[3].HeaderText = "Enquiry Date";
            }
            else this.dataGridView1.DataSource = "";
        }
        public void custfill()
        {
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.dataGridView2.DataSource = ds.Tables[0];
                this.dataGridView2.Columns[0].HeaderText = "Customer Name";
                this.dataGridView2.Columns[1].HeaderText = "Customer Address";
                this.dataGridView2.Columns[2].HeaderText = "GSTIN";
            }
            else this.dataGridView2.DataSource = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox10_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
