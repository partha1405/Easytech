using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Easytech.Properties;
using System.Drawing.Printing; 

namespace Easytech
{
    public partial class Form7 : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand com;
        OleDbCommand com1;
        OleDbDataAdapter da;
        OleDbDataAdapter da1;
        DataSet ds;
        string pdfPrinter;
        public Form7()
        {
            InitializeComponent();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=techeasy.accdb; Jet OLEDB:Database Password=1234;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox13.Text == "" || this.textBox22.Text == "") MessageBox.Show("Your system is unable to add.");
            else
            {
                DialogResult dialog = MessageBox.Show("Do you want to add ?", "Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    if (this.textBox18.Text == "") this.textBox18.Text = "0.000";
                    if (this.textBox19.Text == "") this.textBox19.Text = "0.00";
                    if (this.textBox20.Text == "") this.textBox20.Text = "0.00";
                    if (this.textBox21.Text == "") this.textBox21.Text = "0.00";
                    da = new OleDbDataAdapter("SELECT * FROM enquiry WHERE qut_no=" + this.textBox13.Text + " AND item_no=" + this.textBox22.Text + "", con);
                    ds = new DataSet();
                    da.Fill(ds, "checkitem");
                    if (ds.Tables["checkitem"].Rows.Count > 0) MessageBox.Show("This item code is already entry against this quotation number.");
                    else
                    {                       
                        string sql = "INSERT INTO enquiry(qut_no,item_no,qty,rate,amnt,prgst) VALUES(" + this.textBox13.Text + "," + this.textBox22.Text + "," + this.textBox18.Text + "," + this.textBox19.Text + "," + Convert.ToDouble(this.textBox20.Text) + "," + this.textBox21.Text + ")";
                        con.Open();
                        com = new OleDbCommand(sql, con);
                        com.ExecuteNonQuery();
                        con.Close();
                        da1 = new OleDbDataAdapter("SELECT p.item_code,p.descrip,p.hsn,p.unit,e.qty,e.rate,e.amnt,e.prgst FROM product p,enquiry e WHERE p.item_no=e.item_no AND qut_no=" + this.textBox13.Text + "", con);
                        ds = new DataSet();
                        da1.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            this.dataGridView3.DataSource = ds.Tables[0];
                            this.dataGridView3.Columns[0].HeaderText = "Item Code";
                            this.dataGridView3.Columns[1].HeaderText = "Description";
                            this.dataGridView3.Columns[2].HeaderText = "HSN Code";
                            this.dataGridView3.Columns[3].HeaderText = "Unit";
                            this.dataGridView3.Columns[4].HeaderText = "Quantity";
                            this.dataGridView3.Columns[5].HeaderText = "Rate";
                            this.dataGridView3.Columns[6].HeaderText = "Amount";
                            this.dataGridView3.Columns[7].HeaderText = "GST (%)";
                        }
                    }
                    this.textBox22.Text = "";
                    this.textBox14.Text = "";
                    this.textBox15.Text = "";
                    this.textBox16.Text = "";
                    this.textBox17.Text = "";
                    this.textBox18.Text = "";
                    this.textBox19.Text = "";
                    this.textBox20.Text = "";
                    this.textBox21.Text = ""; 
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font fnt1 = new Font("Calibri", 12, FontStyle.Regular);
            Font fnt2 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt3 = new Font("Calibri", 10, FontStyle.Bold);
            Pen pn1 = new Pen(Color.Black, 1);
            Brush bsh = Brushes.Black;
            Graphics gf = e.Graphics;
            Image logo = Resources.Tree;
            gf.DrawImage(logo, 0, 0, logo.Width, logo.Height);
            gf.DrawString("Quotation NO: " + this.textBox1.Text.Trim(), fnt1, bsh, new Point(35, 245));
            gf.DrawString("Quotation Date: " + this.textBox3.Text.Trim(), fnt1, bsh, new Point(570, 245));
            gf.DrawLine(pn1, new Point(35, 270), new Point(780, 270));
            gf.DrawString("To", fnt1, bsh, new Point(35, 280));
            gf.DrawString( this.textBox11.Text.Trim(), fnt1, bsh, new Point(35, 300));
            gf.DrawString( this.textBox12.Text.Trim(), fnt1, bsh, new Point(35, 320));
            gf.DrawString("Mobile NO: " + this.textBox23.Text.Trim(), fnt1, bsh, new Point(35, 340));
            gf.DrawString("Email ID: " + this.textBox24.Text.Trim(), fnt1, bsh, new Point(35, 360));
            gf.DrawString("GSTIN: " + this.textBox25.Text.Trim(), fnt1, bsh, new Point(35, 380));
            gf.DrawString("GST State: " + this.textBox26.Text.Trim() + "    State Code: " + this.textBox27.Text.Trim(), fnt1, bsh, new Point(35, 400));
            gf.DrawString("Sub: " + this.textBox2.Text.Trim() + " " + this.textBox4.Text.Trim(), fnt1, bsh, new Point(100, 440));
            // For table creation
            gf.DrawLine(pn1, new Point(35, 480), new Point(780, 480));
            gf.DrawString("Sl.\nNO", fnt2, bsh, new Point(40, 480));
            gf.DrawString("Item\nCode", fnt2, bsh, new Point(85, 480));
            gf.DrawString("Description", fnt2, bsh, new Point(215, 480));
            gf.DrawString("HSN\nCode", fnt2, bsh, new Point(375, 480));
            gf.DrawString("Unit", fnt2, bsh, new Point(435, 480));
            gf.DrawString("Quantity", fnt2, bsh, new Point(487, 480));
            gf.DrawString("Rate", fnt2, bsh, new Point(590, 480));
            gf.DrawString("Amount", fnt2, bsh, new Point(685, 480));
            gf.DrawLine(pn1, new Point(35, 525), new Point(780, 525));
            int ypos = 525, sl = 1; ;
            foreach (DataGridViewRow sldata in this.dataGridView3.Rows)
            {
                 SizeF sf = gf.MeasureString(sldata.Cells[1].Value.ToString(), fnt3);
                 if (sf.Width > 227)
                 {
                     gf.DrawString(sl.ToString(), fnt3, bsh, new RectangleF(36, ypos, 32, fnt3.Height));
                     gf.DrawString(sldata.Cells[0].Value.ToString(), fnt3, bsh, new RectangleF(71, ypos, 67, fnt3.Height));
                     gf.DrawString(sldata.Cells[1].Value.ToString(), fnt3, bsh, new RectangleF(141, ypos, 227, fnt3.Height * 5));
                     gf.DrawString(sldata.Cells[2].Value.ToString(), fnt3, bsh, new RectangleF(371, ypos, 57, fnt3.Height));
                     gf.DrawString(sldata.Cells[3].Value.ToString(), fnt3, bsh, new RectangleF(431, ypos, 47, fnt3.Height));
                     gf.DrawString(sldata.Cells[4].Value.ToString(), fnt3, bsh, new RectangleF(481, ypos, 77, fnt3.Height));
                     gf.DrawString(sldata.Cells[5].Value.ToString(), fnt3, bsh, new RectangleF(561, ypos, 97, fnt3.Height));
                     gf.DrawString(sldata.Cells[6].Value.ToString(), fnt3, bsh, new RectangleF(661, ypos, 117, fnt3.Height));
                     ypos = ypos + fnt3.Height * 5 + 5;
                 }
                 else
                 {
                     gf.DrawString(sl.ToString(), fnt3, bsh, new RectangleF(36, ypos, 32, fnt3.Height));
                     gf.DrawString(sldata.Cells[0].Value.ToString(), fnt3, bsh, new RectangleF(71, ypos, 67, fnt3.Height));
                     gf.DrawString(sldata.Cells[1].Value.ToString(), fnt3, bsh, new RectangleF(141, ypos, 227, fnt3.Height));
                     gf.DrawString(sldata.Cells[2].Value.ToString(), fnt3, bsh, new RectangleF(371, ypos, 57, fnt3.Height));
                     gf.DrawString(sldata.Cells[3].Value.ToString(), fnt3, bsh, new RectangleF(431, ypos, 47, fnt3.Height));
                     gf.DrawString(sldata.Cells[4].Value.ToString(), fnt3, bsh, new RectangleF(481, ypos, 77, fnt3.Height));
                     gf.DrawString(sldata.Cells[5].Value.ToString(), fnt3, bsh, new RectangleF(561, ypos, 97, fnt3.Height));
                     gf.DrawString(sldata.Cells[6].Value.ToString(), fnt3, bsh, new RectangleF(661, ypos, 117, fnt3.Height));
                     ypos = ypos + 20;
                 }
                 sl++;
            }
            gf.DrawLine(pn1, new Point(35, ypos), new Point(780, ypos));
            gf.DrawLine(pn1, new Point(35, 480), new Point(35, ypos));
            gf.DrawLine(pn1, new Point(70, 480), new Point(70, ypos));
            gf.DrawLine(pn1, new Point(140, 480), new Point(140, ypos));
            gf.DrawLine(pn1, new Point(370, 480), new Point(370, ypos));
            gf.DrawLine(pn1, new Point(430, 480), new Point(430, ypos));
            gf.DrawLine(pn1, new Point(480, 480), new Point(480, ypos));
            gf.DrawLine(pn1, new Point(560, 480), new Point(560, ypos));
            gf.DrawLine(pn1, new Point(660, 480), new Point(660, ypos));
            gf.DrawLine(pn1, new Point(780, 480), new Point(780, ypos));
            // Part after table
            gf.DrawString("Terms & Conditions", new Font("Calibri", 12, FontStyle.Bold), bsh, new Point(35, ypos+20));
            gf.DrawString("Price:  " + this.textBox5.Text.Trim(), fnt1, bsh, new Point(35, ypos+40));
            gf.DrawString("Validity:  " +  this.textBox6.Text.Trim(), fnt1, bsh, new Point(35, ypos+60));
            gf.DrawString("Gurantee:  " + this.textBox7.Text.Trim(), fnt1, bsh, new Point(35, ypos+80));
            gf.DrawString("Inspection:  " + this.textBox8.Text.Trim(), fnt1, bsh, new Point(35, ypos+100));
            gf.DrawString("GST:  " + this.textBox9.Text.Trim(), fnt1, bsh, new Point(35, ypos+120));
            gf.DrawString("Discount:  " + this.textBox10.Text.Trim(), fnt1, bsh, new Point(35, ypos+140));
            gf.DrawString("For DVPL", new Font("Calibri", 16, FontStyle.Bold), bsh, new Point(550, ypos + 200));
            gf.DrawString("Authorised Signatory", new Font("Calibri", 14, FontStyle.Bold), bsh, new Point(570, ypos + 280));       
        }

        private void button3_Click(object sender, EventArgs e)
        {  
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {               
                if (strPrinter.Contains("PDF")) pdfPrinter = strPrinter;
                printDocument1.PrinterSettings.PrinterName = pdfPrinter;
            }
            printDocument1.Print();
        }

        private void button4_Click(object sender, EventArgs e)
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
                DataGridViewRow rowdata = this.dataGridView1.Rows[e.RowIndex];
                da = new OleDbDataAdapter("SELECT t.qut_no,t.quot_no,t.quot_dt,t.enqu_no,t.enqu_dt,t.price,t.validity,t.guaran,t.inspect,t.gst,t.discount,c.cust_name,c.cust_address,c.cust_mobile,c.cust_email,c.cust_gst,c.gst_state,c.state_code FROM term t,custdetail c WHERE c.cust_no=t.cust_no AND quot_no='" + rowdata.Cells[0].Value.ToString() + "' AND enqu_no='" + rowdata.Cells[2].Value.ToString() + "'", con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.textBox13.Text = ds.Tables[0].Rows[0][0].ToString();
                    this.textBox1.Text = ds.Tables[0].Rows[0][1].ToString();
                    this.textBox3.Text = ds.Tables[0].Rows[0][2].ToString();
                    this.textBox2.Text = ds.Tables[0].Rows[0][3].ToString();
                    this.textBox4.Text = ds.Tables[0].Rows[0][4].ToString();
                    this.textBox5.Text = ds.Tables[0].Rows[0][5].ToString();
                    this.textBox6.Text = ds.Tables[0].Rows[0][6].ToString();
                    this.textBox7.Text = ds.Tables[0].Rows[0][7].ToString();
                    this.textBox8.Text = ds.Tables[0].Rows[0][8].ToString();
                    this.textBox9.Text = ds.Tables[0].Rows[0][9].ToString();
                    this.textBox10.Text = ds.Tables[0].Rows[0][10].ToString();
                    this.textBox11.Text = ds.Tables[0].Rows[0][11].ToString();
                    this.textBox12.Text = ds.Tables[0].Rows[0][12].ToString();
                    this.textBox23.Text = ds.Tables[0].Rows[0][13].ToString();
                    this.textBox24.Text = ds.Tables[0].Rows[0][14].ToString();
                    this.textBox25.Text = ds.Tables[0].Rows[0][15].ToString();
                    this.textBox26.Text = ds.Tables[0].Rows[0][16].ToString();
                    this.textBox27.Text = ds.Tables[0].Rows[0][17].ToString();
               }
            }
            this.dataGridView1.DataSource = "";
        }
      
        private void textBox14_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.textBox14.Text == "") this.dataGridView2.DataSource = "";
            else
            {
                da = new OleDbDataAdapter("SELECT item_code,descrip,hsn FROM product WHERE item_code like '" + this.textBox14.Text + "%'", con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.dataGridView2.DataSource = ds.Tables[0];
                    this.dataGridView2.Columns[0].HeaderText = "Item Code";
                    this.dataGridView2.Columns[1].HeaderText = "Description";
                    this.dataGridView2.Columns[2].HeaderText = "HSN Code";
                }
                else this.dataGridView2.DataSource = "";
           }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rowdata = this.dataGridView2.Rows[e.RowIndex];
                da = new OleDbDataAdapter("SELECT * FROM product WHERE item_code='" + rowdata.Cells[0].Value.ToString() + "'", con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.textBox22.Text = ds.Tables[0].Rows[0][0].ToString();              
                    this.textBox14.Text = ds.Tables[0].Rows[0][1].ToString();
                    this.textBox15.Text = ds.Tables[0].Rows[0][2].ToString();
                    this.textBox16.Text = ds.Tables[0].Rows[0][3].ToString();
                    this.textBox17.Text = ds.Tables[0].Rows[0][4].ToString();
                }
            }
            this.dataGridView2.DataSource = "";
        }

        private void textBox18_KeyUp(object sender, KeyEventArgs e)
        {
            calcu();
        }

        private void textBox19_KeyUp(object sender, KeyEventArgs e)
        {
            calcu();
        }

        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            num_point(sender, e);
        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            num_point(sender, e);
        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            num_point(sender, e);
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

        public void calcu()
        {
            if (this.textBox18.Text == "" || this.textBox19.Text == "") this.textBox20.Text = "0.00";
            else
            {
                decimal num1 = Convert.ToDecimal(this.textBox18.Text);
                decimal num2 = Convert.ToDecimal(this.textBox19.Text);
                decimal mul = Math.Round(Convert.ToDecimal(num1 * num2),2);
                this.textBox20.Text = mul.ToString();
            }
        }

        public void num_point(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') {e.Handled = true;MessageBox.Show("Enter number and point only'");}
            else if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1) { e.Handled = true; MessageBox.Show("Enter one decimal point only.");}
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox13_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
