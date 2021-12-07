using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAS_v2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        AutoSprint auto = new AutoSprint();
        AutoClicker clicker = new AutoClicker();
        ChestStealer stealer = new ChestStealer();
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            guna2ComboBox1.Items.Add("Auto Clicker");
            guna2ComboBox1.Items.Add("Auto Sprint");
            guna2ComboBox1.Items.Add("Chest Stealer");
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedItem != null)
            {
                string item = guna2ComboBox1.SelectedItem.ToString();
                if (item == "Auto Clicker")
                {
                    clicker.Show();
                }
                else if (item == "Auto Sprint")
                {
                    auto.Show();
                }
                else if (item == "Chest Stealer")
                {
                    stealer.Show();
                }
            }
        }
    }
}
