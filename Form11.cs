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
    public partial class Form11 : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand com;
        OleDbDataAdapter da;
        OleDbDataAdapter da1;
        DataSet ds;
        public Form11()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=techeasy.accdb; Jet OLEDB:Database Password=1234;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog==DialogResult.Yes) this.Close();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.textBox1.Text == "") this.dataGridView1.DataSource = "";
            else
            {
                da = new OleDbDataAdapter("SELECT item_no,item_code,descrip,hsn FROM product WHERE item_code like '" + this.textBox1.Text + "%'", con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.dataGridView1.DataSource = ds.Tables[0];
                    this.dataGridView1.Columns[0].HeaderText = "Item No";
                     this.dataGridView1.Columns[1].HeaderText = "Item Code";
                    this.dataGridView1.Columns[2].HeaderText = "Description";
                    this.dataGridView1.Columns[3].HeaderText = "HSN Code";
                }
                else this.dataGridView1.DataSource = "";
            }  
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rowdata = this.dataGridView1.Rows[e.RowIndex];
                da = new OleDbDataAdapter("SELECT t.quot_no,t.quot_dt,t.enqu_no,t.enqu_dt,e.qty,e.rate,e.amnt,e.prgst,c.cust_name,c.cust_gst FROM term t,enquiry e,custdetail c WHERE t.qut_no=e.qut_no AND c.cust_no=t.cust_no AND e.item_no=" + rowdata.Cells[0].Value + "", con);
                da1 = new OleDbDataAdapter("SELECT item_code,descrip,hsn,unit FROM product WHERE item_no=" + rowdata.Cells[0].Value + "", con);
                ds = new DataSet();
                da.Fill(ds,"record");
                da1.Fill(ds, "item");
                if (ds.Tables["record"].Rows.Count > 0)
                {
                    this.dataGridView2.DataSource = ds.Tables[0];
                    this.dataGridView2.Columns[0].HeaderText = "Quotation NO";
                    this.dataGridView2.Columns[1].HeaderText = "Quotation Date";
                    this.dataGridView2.Columns[2].HeaderText = "Enquiry NO";
                    this.dataGridView2.Columns[3].HeaderText = "Enquiry Date";
                    this.dataGridView2.Columns[4].HeaderText = "Quantity";
                    this.dataGridView2.Columns[5].HeaderText = "Rate";
                    this.dataGridView2.Columns[6].HeaderText = "Amount";
                    this.dataGridView2.Columns[7].HeaderText = "GST (%)";
                    this.dataGridView2.Columns[8].HeaderText = "Customer's Name";
                    this.dataGridView2.Columns[9].HeaderText = "QCustomer's GSTIN";
                }
                if (ds.Tables["item"].Rows.Count > 0)
                {
                    this.textBox2.Text = ds.Tables["item"].Rows[0][0].ToString();
                    this.textBox3.Text = ds.Tables["item"].Rows[0][1].ToString();
                    this.textBox4.Text = ds.Tables["item"].Rows[0][2].ToString();
                    this.textBox5.Text = ds.Tables["item"].Rows[0][3].ToString();
                }
            }
            this.dataGridView1.DataSource = "";
            this.textBox1.Text = "";
        }

        private void Unit(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
