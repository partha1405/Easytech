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
    public partial class Form6 : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand com;
        OleDbDataAdapter da;
        OleDbDataAdapter da1;
        OleDbDataAdapter da2;
        OleDbDataAdapter da3;
        DataSet ds;
        public Form6()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=techeasy.accdb; Jet OLEDB:Database Password=1234;";
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            this.dateTimePicker2.CustomFormat = "dd-MM-yyyy";
            lastquot();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox9.Text == "") MessageBox.Show("Your system is unable to add record. Please select the customer's name from dropdown choice.");
            else if (this.textBox1.Text == "") MessageBox.Show("Enter quotation no.");
            else if (this.textBox2.Text == "") MessageBox.Show("Enter enquiry no.");
            else
            {
                DialogResult dialog = MessageBox.Show("Do you want to insert ?", "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                        da=new OleDbDataAdapter("SELECT * FROM term WHERE quot_no='" + this.textBox1.Text + "'",con);
                        da1 = new OleDbDataAdapter("SELECT * FROM term WHERE enqu_no='" + this.textBox2.Text + "'", con);
                        ds=new DataSet();
                        da.Fill(ds, "quotation");
                        da1.Fill(ds, "enquiry");
                        if(ds.Tables["quotation"].Rows.Count>0) MessageBox.Show("This Quotation No is already exist.");
                        else if (ds.Tables["enquiry"].Rows.Count > 0) MessageBox.Show("This Enquiry No is already exist.");
                        else
                        {
                            string sql = "INSERT INTO term(quot_no,quot_dt,enqu_no,enqu_dt,cust_no,price,validity,guaran,inspect,gst,discount) VALUES('" + this.textBox1.Text + "','" + this.dateTimePicker1.Text + "','" + this.textBox2.Text + "','" + this.dateTimePicker2.Text + "'," + this.textBox9.Text + ",'" + this.textBox3.Text + "','" + this.textBox4.Text + "','" + this.textBox5.Text + "','" + this.textBox6.Text + "','" + this.textBox7.Text + "','" + this.textBox8.Text + "')";
                            con.Open();
                            com = new OleDbCommand(sql, con);
                            com.ExecuteNonQuery();
                            con.Close();
                            da2 = new OleDbDataAdapter("SELECT t.quot_no,t.quot_dt,t.enqu_no,t.enqu_dt,c.cust_name,c.cust_gst,t.price,t.validity,t.guaran,t.inspect,t.gst,t.discount FROM term t,custdetail c WHERE t.cust_no=c.cust_no", con);
                            ds = new DataSet();
                            da2.Fill(ds, "term");
                            if (ds.Tables["term"].Rows.Count > 0)
                            {
                                this.dataGridView2.DataSource = ds.Tables["term"];
                                this.dataGridView2.Columns[0].HeaderText = "Quotation NO";
                                this.dataGridView2.Columns[1].HeaderText = "Quotation Date";
                                this.dataGridView2.Columns[2].HeaderText = "Enquiry NO";
                                this.dataGridView2.Columns[3].HeaderText = "Enquiry Date";
                                this.dataGridView2.Columns[4].HeaderText = "Customer's Name";
                                this.dataGridView2.Columns[5].HeaderText = "GSTIN";
                                this.dataGridView2.Columns[6].HeaderText = "Price";
                                this.dataGridView2.Columns[7].HeaderText = "Validity";
                                this.dataGridView2.Columns[8].HeaderText = "Gurantee";
                                this.dataGridView2.Columns[9].HeaderText = "Inspection";
                                this.dataGridView2.Columns[10].HeaderText = "GST";
                                this.dataGridView2.Columns[11].HeaderText = "Discount";
                            }
                            MessageBox.Show("Data inserted successfully");
                            this.textBox9.Text = "";
                            this.textBox1.Text = "";
                            this.textBox2.Text = "";
                            this.textBox3.Text = "";
                            this.textBox4.Text = "";
                            this.textBox5.Text = "";
                            this.textBox6.Text = "";
                            this.textBox7.Text = "";
                            this.textBox8.Text = "";
                            this.textBox10.Text = "";
                            this.textBox11.Text = "";
                            this.textBox1.Select();
                        }
                        lastquot();                           
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes) this.Close();
        }

        private void textBox10_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.textBox10.Text == "") this.dataGridView1.DataSource = "";
            else
            {
                da = new OleDbDataAdapter("SELECT cust_name,cust_address,cust_gst FROM custdetail WHERE cust_name LIKE '" + this.textBox10.Text + "%'", con);
                custfill();
            }
        }

        private void textBox11_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.textBox11.Text == "") this.dataGridView1.DataSource = "";
            else
            {
                da = new OleDbDataAdapter("SELECT cust_name,cust_address,cust_gst FROM custdetail WHERE cust_gst LIKE '" + this.textBox11.Text + "%'", con);
                custfill();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rowdata = this.dataGridView1.Rows[e.RowIndex];
                da = new OleDbDataAdapter("SELECT cust_no,cust_name,cust_gst FROM custdetail WHERE cust_name='" + rowdata.Cells[0].Value.ToString() + "' AND cust_address='" + rowdata.Cells[1].Value.ToString() +"' AND cust_gst='" + rowdata.Cells[2].Value.ToString() + "'", con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.textBox9.Text = ds.Tables[0].Rows[0][0].ToString();
                    this.textBox10.Text = ds.Tables[0].Rows[0][1].ToString();
                    this.textBox11.Text = ds.Tables[0].Rows[0][2].ToString();
                }
            }
            this.dataGridView1.DataSource = "";
        }

        public void lastquot()
        {
            da3 = new OleDbDataAdapter("SELECT quot_no FROM term WHERE qut_no=(SELECT MAX(qut_no) FROM term)", con);
            ds = new DataSet();
            da3.Fill(ds,"lastquot");
            if (ds.Tables["lastquot"].Rows.Count > 0) this.textBox12.Text = ds.Tables["lastquot"].Rows[0][0].ToString();
        }

        public void custfill()
        {
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.dataGridView1.DataSource = ds.Tables[0];
                this.dataGridView1.Columns[0].HeaderText = "Customer Name";
                this.dataGridView1.Columns[1].HeaderText = "Customer Address";
                this.dataGridView1.Columns[2].HeaderText = "GSTIN";
            }
            else this.dataGridView1.DataSource = "";
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }   
    }
}
