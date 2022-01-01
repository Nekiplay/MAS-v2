using System;
using System.Windows.Forms;

namespace MAS_v2.Security
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            guna2ComboBox1.Items.Add("Winlocker");
            guna2ComboBox1.Items.Add("SMS Bomber");

            try { guna2ComboBox1.SelectedIndex = Program.SecurityManager.settings.FakeMenu; } catch { }
            guna2TextBox1.Text = Program.SecurityManager.settings.Password;
            guna2TextBox2.Text = Program.hardware.GetID();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedItem != null)
            {
                string item = guna2ComboBox1.SelectedItem.ToString();
                if (item == "None")
                {
                    Program.SecurityManager.settings.FakeMenu = 0;
                }
                else if (item == "Winlocker")
                {
                    Program.SecurityManager.settings.FakeMenu = 1;
                }
                else if (item == "SMS Bomber")
                {
                    Program.SecurityManager.settings.FakeMenu = 2;
                }
                Program.SecurityManager.SaveCFG();
            }
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
            else
            {
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(guna2TextBox1.Text, out int password))
            {
                Program.SecurityManager.settings.Password = password.ToString();
                Program.SecurityManager.SaveCFG();
            }
            else
            {

            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/im?sel=-209453266");
        }
    }
}
