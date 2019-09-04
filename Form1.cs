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
    public partial class Form1 : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand com;
        OleDbDataAdapter da;
        DataSet ds;
        public Form1()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=techeasy.accdb; Jet OLEDB:Database Password=1234;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "") MessageBox.Show("Enter Customer's Name.");
            else if (this.textBox2.Text == "") MessageBox.Show("Enter Customer's Address.");
            else if (this.textBox5.Text == "") MessageBox.Show("Enter Mobile NO.");
            else
            {
                DialogResult dialog = MessageBox.Show("All the data are correct?", "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    if (this.textBox8.Text != "")
                    {
                        da = new OleDbDataAdapter("SELECT * FROM custdetail WHERE cust_gst='" + this.textBox7.Text + "'", con);
                        ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0) MessageBox.Show("This GSTIN is already exist.");
                        else { insert(); }
                    }
                    else { insert(); }
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
                    this.dataGridView1.Columns[0].HeaderText = "Client's Name";
                    this.dataGridView1.Columns[1].HeaderText = "Client's Address";
                    this.dataGridView1.Columns[2].HeaderText = "GSTIN";
                }
                else this.dataGridView1.DataSource = "";
            }
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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox7.Text == "") {this.textBox8.Text = "";this.textBox9.Text = "";this.textBox10.Text = "";}
            this.textBox7.Text = this.textBox7.Text.ToUpper();
            this.textBox7.SelectionStart = this.textBox7.Text.Length;
            state();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            this.textBox8.Text = this.textBox8.Text.ToUpper();
            this.textBox8.SelectionStart = this.textBox8.Text.Length;
        }

        public void insert()
        {
            string sql = "INSERT INTO custdetail(cust_name,cust_address,cust_phone,cust_fax,cust_mobile,cust_email,cust_gst,cust_pan,gst_state,state_code,bank1,bank2) VALUES('" + this.textBox1.Text + "','" + this.textBox2.Text + "','" + this.textBox3.Text + "','" + this.textBox4.Text + "','" + this.textBox5.Text + "','" + this.textBox6.Text + "','" + this.textBox7.Text + "','" + this.textBox8.Text + "','" + this.textBox9.Text + "','" + this.textBox10.Text + "','" + this.textBox11.Text + "','" + this.textBox12.Text + "')";
            con.Open();
            com = new OleDbCommand(sql, con);
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record inserted successfully");
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

        public void state()
        {
            if (this.textBox7.Text.Length>=2)
            {
                switch (this.textBox7.Text.Substring(0, 2))
                {
                    case "35":this.textBox9.Text = "Andaman and Nicobar Islands";this.textBox10.Text = "35";break;
                    case "37":this.textBox9.Text = "Andhra Pradesh (New)"; this.textBox10.Text = "37"; break;
                    case "28":this.textBox9.Text = "Andhra Pradesh (Old)";this.textBox10.Text = "28";break;
                    case "12":this.textBox9.Text = "Arunachal Pradesh";this.textBox10.Text = "12";break;
                    case "18":this.textBox9.Text = "Assam";this.textBox10.Text = "18";break;
                    case "10":this.textBox9.Text = "Bihar";this.textBox10.Text = "10";break;
                    case "04":this.textBox9.Text = "Chandigarh";this.textBox10.Text = "04";break;
                    case "22":this.textBox9.Text = "Chhattisgarh";this.textBox10.Text = "22";break;
                    case "26":this.textBox9.Text = "Dadra and Nagar Haveli";this.textBox10.Text = "26";break;
                    case "25":this.textBox9.Text = "Daman and Diu";this.textBox10.Text = "25";break;
                    case "07":this.textBox9.Text = "Delhi";this.textBox10.Text = "07";break;
                    case "30":this.textBox9.Text = "Goa";this.textBox10.Text = "30";break;
                    case "24":this.textBox9.Text = "Gujarat";this.textBox10.Text = "24";break;
                    case "06":this.textBox9.Text = "Haryana";this.textBox10.Text = "06";break;
                    case "01":this.textBox9.Text = "Himachal Pradesh";this.textBox10.Text = "01";break;
                    case "02":this.textBox9.Text = "Jammu and Kashmir";this.textBox10.Text = "02";break;
                    case "20":this.textBox9.Text = "Jharkhand";this.textBox10.Text = "20";break;
                    case "29":this.textBox9.Text = "Karnataka";this.textBox10.Text = "29";break;
                    case "32":this.textBox9.Text = "Kerala";this.textBox10.Text = "32";break;
                    case "31":this.textBox9.Text = "Lakshadweep";this.textBox10.Text = "31";break;
                    case "23":this.textBox9.Text = "Madhya Pradesh";this.textBox10.Text = "23";break;
                    case "27":this.textBox9.Text = "Maharashtra";this.textBox10.Text = "27";break;
                    case "14":this.textBox9.Text = "Manipur";this.textBox10.Text = "14";break;
                    case "17":this.textBox9.Text = "Meghalaya";this.textBox10.Text = "17";break;
                    case "15":this.textBox9.Text = "Mizoram";this.textBox10.Text = "15";break;
                    case "13":this.textBox9.Text = "Nagaland";this.textBox10.Text = "13";break;
                    case "21":this.textBox9.Text = "Odisha";this.textBox10.Text = "21";break;
                    case "34":this.textBox9.Text = "Puduchery";this.textBox10.Text = "34";break;
                    case "03":this.textBox9.Text = "Punjab";this.textBox10.Text = "03";break;
                    case "08":this.textBox9.Text = "Rajasthan";this.textBox10.Text = "08";break;
                    case "11":this.textBox9.Text = "Sikkim";this.textBox10.Text = "11";break;
                    case "33":this.textBox9.Text = "Tamil Nadu";this.textBox10.Text = "33";break;
                    case "36":this.textBox9.Text = "Telangana";this.textBox10.Text = "36";break;
                    case "16":this.textBox9.Text = "Tripura";this.textBox10.Text = "16";break;
                    case "09":this.textBox9.Text = "Uttar Pradesh";this.textBox10.Text = "09";break;
                    case "05":this.textBox9.Text = "Uttarakhand";this.textBox10.Text = "05";break;
                    case "19":this.textBox9.Text = "West Bengal";this.textBox10.Text = "19";break;
                }
                this.textBox8.Text = this.textBox7.Text.Substring(2, this.textBox7.Text.Length - 2);
            }
        }
           
        public void number_only(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {e.Handled = true;MessageBox.Show("Enter number only.");}
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox7_Leave(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
