using MAS_v2.Forms;
using MAS_v2.Security;
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
        private NinjaBridge ninjaBridge = new NinjaBridge();
        private SettingsForm settingsForm = new SettingsForm();
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            guna2ComboBox1.Items.Add("Auto Clicker");
            guna2ComboBox1.Items.Add("Auto Sprint");
            guna2ComboBox1.Items.Add("Chest Stealer");
            guna2ComboBox1.Items.Add("Ninja Bridge");
            guna2ComboBox1.Items.Add("Settings");
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.Exit();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Program.Exit();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedItem != null)
            {

                string item = guna2ComboBox1.SelectedItem.ToString();
                switch (item)
                {
                    case "Auto Clicker":
                        clicker.Show();
                        break;
                    case "Auto Sprint":
                        auto.Show();
                        break;
                    case "Chest Stealer":
                        stealer.Show();
                        break;
                    case "Ninja Bridge":
                        ninjaBridge.Show();
                        break;
                    case "Settings":
                        settingsForm.Show();
                        break;
                }
            }
        }
    }
}
