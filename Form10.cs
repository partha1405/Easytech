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
    public partial class Form10 : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand com;
        OleDbDataAdapter da;
        OleDbDataAdapter da1;
        DataSet ds;
        public Form10()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=techeasy.accdb; Jet OLEDB:Database Password=1234;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox7.Text=="") MessageBox.Show("Your system is unable edit.");
            else
            {
                DialogResult dialog = MessageBox.Show("Do you want to edit ?", "EDit GSTIN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    if (this.textBox3.Text != "")
                    {
                        da = new OleDbDataAdapter("SELECT * FROM custdetail WHERE cust_gst='" + this.textBox3.Text + "'", con);
                        ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0) MessageBox.Show("The GSTIN is already exist. Please check it.");
                        else
                        {
                            con.Open();
                            com = new OleDbCommand("UPDATE custdetail SET cust_gst='" + this.textBox3.Text + "',cust_pan='" + this.textBox4.Text + "',gst_state='" + this.textBox5.Text + "',state_code='" + this.textBox6.Text + "' WHERE cust_no=" + this.textBox7.Text + "", con);
                            com.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Record edited successfully.");
                        } 
                    }
                    else  MessageBox.Show("Please enter GST NO.");
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
                da = new OleDbDataAdapter("SELECT cust_name,cust_address,cust_gst FROM custdetail WHERE cust_name LIKE '" + this.textBox1.Text + "%'", con);
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
                da = new OleDbDataAdapter("SELECT cust_no,cust_name,cust_address,cust_gst,cust_pan,gst_state,state_code FROM custdetail WHERE cust_name='" + rowdata.Cells[0].Value.ToString() + "' AND cust_address='" + rowdata.Cells[1].Value.ToString() + "' AND cust_gst='" + rowdata.Cells[2].Value.ToString() + "'", con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                   this.textBox7.Text = ds.Tables[0].Rows[0][0].ToString();
                   this.textBox1.Text = ds.Tables[0].Rows[0][1].ToString();
                   this.textBox2.Text = ds.Tables[0].Rows[0][2].ToString();
                   this.textBox3.Text = ds.Tables[0].Rows[0][3].ToString();
                   this.textBox4.Text = ds.Tables[0].Rows[0][4].ToString();
                   this.textBox5.Text = ds.Tables[0].Rows[0][5].ToString();
                   this.textBox6.Text = ds.Tables[0].Rows[0][6].ToString();
                }              
            }
            this.dataGridView1.DataSource="";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox3.Text == "") {this.textBox4.Text = "";this.textBox5.Text = "";this.textBox6.Text = "";}
            this.textBox3.Text = this.textBox3.Text.ToUpper();
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            state();
        }

        public void state()
        {
            if (this.textBox3.Text.Length >= 2)
            {
                switch (this.textBox3.Text.Substring(0, 2))
                {
                    case "35":this.textBox5.Text = "Andaman and Nicobar Islands";this.textBox6.Text = "35";break;
                    case "37":this.textBox5.Text = "Andhra Pradesh (New)";this.textBox6.Text = "37";break;
                    case "28":this.textBox5.Text = "Andhra Pradesh (Old)";this.textBox6.Text = "28";break;
                    case "12":this.textBox5.Text = "Arunachal Pradesh";this.textBox6.Text = "12";break;
                    case "18":this.textBox5.Text = "Assam";this.textBox6.Text = "18";break;
                    case "10":this.textBox5.Text = "Bihar";this.textBox6.Text = "10";break;
                    case "04":this.textBox5.Text = "Chandigarh";this.textBox6.Text = "04";break;
                    case "22":this.textBox5.Text = "Chhattisgarh";this.textBox6.Text = "22";break;
                    case "26":this.textBox5.Text = "Dadra and Nagar Haveli";this.textBox6.Text = "26";break;
                    case "25":this.textBox5.Text = "Daman and Diu";this.textBox6.Text = "25";break;
                    case "07":this.textBox5.Text = "Delhi";this.textBox6.Text = "07";break;
                    case "30":this.textBox5.Text = "Goa";this.textBox6.Text = "30";break;
                    case "24":this.textBox5.Text = "Gujarat";this.textBox6.Text = "24";break;
                    case "06":this.textBox5.Text = "Haryana";this.textBox6.Text = "06";break;
                    case "01":this.textBox5.Text = "Himachal Pradesh";this.textBox6.Text = "01";break;
                    case "02":this.textBox5.Text = "Jammu and Kashmir";this.textBox6.Text = "02";break;
                    case "20":this.textBox5.Text = "Jharkhand";this.textBox6.Text = "20";break;
                    case "29":this.textBox5.Text = "Karnataka";this.textBox6.Text = "29";break;
                    case "32":this.textBox5.Text = "Kerala";this.textBox6.Text = "32";break;
                    case "31":this.textBox5.Text = "Lakshadweep";this.textBox6.Text = "31";break;
                    case "23":this.textBox5.Text = "Madhya Pradesh";this.textBox6.Text = "23";break;
                    case "27":this.textBox5.Text = "Maharashtra";this.textBox6.Text = "27";break;
                    case "14":this.textBox5.Text = "Manipur";this.textBox6.Text = "14";break;
                    case "17":this.textBox5.Text = "Meghalaya";this.textBox6.Text = "17";break;
                    case "15":this.textBox5.Text = "Mizoram";this.textBox6.Text = "15";break;
                    case "13":this.textBox5.Text = "Nagaland";this.textBox6.Text = "13";break;
                    case "21":this.textBox5.Text = "Odisha";this.textBox6.Text = "21";break;
                    case "34":this.textBox5.Text = "Puduchery";this.textBox6.Text = "34";break;
                    case "03":this.textBox5.Text = "Punjab";this.textBox6.Text = "03";break;
                    case "08":this.textBox5.Text = "Rajasthan";this.textBox6.Text = "08";break;
                    case "11":this.textBox5.Text = "Sikkim";this.textBox6.Text = "11";break;
                    case "33":this.textBox5.Text = "Tamil Nadu";this.textBox6.Text = "33";break;
                    case "36":this.textBox5.Text = "Telangana";this.textBox6.Text = "36";break;
                    case "16":this.textBox5.Text = "Tripura";this.textBox6.Text = "16";break;
                    case "09":this.textBox5.Text = "Uttar Pradesh";this.textBox6.Text = "09";break;
                    case "05":this.textBox5.Text = "Uttarakhand";this.textBox6.Text = "05";break;
                    case "19":this.textBox5.Text = "West Bengal";this.textBox6.Text = "19";break;
                }
                this.textBox4.Text = this.textBox3.Text.Substring(2, this.textBox3.Text.Length - 2);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {

        }
    }
}
