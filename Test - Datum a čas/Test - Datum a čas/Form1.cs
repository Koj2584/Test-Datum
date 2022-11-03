using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test___Datum_a_čas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random r = new Random();
        List<string> rcList = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            bool zena = radioButton1.Checked;
            DateTime date = dateTimePicker1.Value;
            string rc54 = "000000000";
            string rc = "0000000000";
            do
            {
                if (zena)
                    rc54 = date.Year.ToString().Substring(2) + (date.Month + 50).ToString() + (date.Day < 10 ? "0" : "") + date.Day.ToString();
                else 
                    rc54 = date.Year.ToString().Substring(2) + (date.Month < 10 ? "0" : "") + date.Month.ToString() + (date.Day < 10 ? "0" : "") + date.Day.ToString();
                rc54 += r.Next(1, 1000).ToString("000");
                rc = rc54 + 9;
                while (long.Parse(rc) % 11 != 0)
                    rc = (long.Parse(rc) - 1).ToString("0000000000");
            } while (rc54.Substring(6, 3) != rc.Substring(6, 3)||rcList.Contains(rc));
            rcList.Add(rc);
            if (rcList.Count >= 2)
                button2.Enabled = true;
            maskedTextBox1.Text = rc54;
            maskedTextBox2.Text = rc;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int m = 0, z = 0, max=0, min = 1000;
            foreach(string s in rcList)
            {
                listBox1.Items.Add(s.Substring(0,6)+"/"+s.Substring(6));
                if (int.Parse(s.Substring(2, 1)) > 1)
                    z++;
                else
                    m++;
                string datum = s.Substring(4, 2) + "."+(int.Parse(s.Substring(2, 2))>12? (int.Parse(s.Substring(2, 2))-50).ToString() : s.Substring(2, 2)) +"."+(int.Parse(s.Substring(0, 2)) <= DateTime.Now.Year - 2000 ? "20" + s.Substring(0, 2) : "19" + s.Substring(0, 2));
                DateTime d = new DateTime();
                d = DateTime.Parse(datum);
                listBox1.Items.Add(d.ToString());
                TimeSpan ts = DateTime.Now - d;
                if (ts.Days / 365.25 > max)
                    max = (int)(ts.Days / 365.25);
                if (ts.Days / 365.25 < min)
                    min = (int)(ts.Days / 365.25);
            }

            label3.Text = "Počet mužů: " + m + "\nPočet žen: " + z + "\nRozdil: " + (max-min)+" roků";
        }
    }
}
