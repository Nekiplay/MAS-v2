using System;
using System.Windows.Forms;

namespace MAS_v2
{
    public partial class MenuSelector : Form
    {
        public MenuSelector()
        {
            InitializeComponent();
        }
        private AutoSprint auto = new AutoSprint();
        private AutoClicker clicker = new AutoClicker();
        private ChestStealer stealer = new ChestStealer();
        private Security.SettingsForm settingsForm = new Security.SettingsForm();
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            guna2ComboBox1.Items.Add("Auto Clicker");
            guna2ComboBox1.Items.Add("Auto Sprint");
            guna2ComboBox1.Items.Add("Chest Stealer");
            guna2ComboBox1.Items.Add("Settings");
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
                else if (item == "Settings")
                {
                    settingsForm.Show();
                }
            }
        }
    }
}
