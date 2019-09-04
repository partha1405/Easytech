using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Easytech
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string dateInString = "01.08.2019";
            DateTime startDate = DateTime.Parse(dateInString);
            DateTime expiryDate = startDate.AddDays(1000);
            DateTime alertDate = startDate.AddDays(985);
            if (DateTime.Now > expiryDate) MessageBox.Show("Application Expired.","",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            else if (DateTime.Now > alertDate)
            {
                MessageBox.Show("Your license will be expired soon. The expiry date is "+expiryDate.ToShortDateString()+".","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form8());
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form8());
            } 
        }
    }
}
