using System;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;

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
        private string GetApplicationName()
        {
            string[] path = Application.ExecutablePath.Split('\\');
            if (path.Length > 0)
            {
                return path.Last();
            }
            else
            {
                return Application.ExecutablePath;
            }
        }

        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            Task Prefetch = Task.Factory.StartNew(() =>
            {
                if (Directory.Exists("C:\\Windows\\Prefetch"))
                {
                    string[] files = Directory.GetFiles("C:\\Windows\\Prefetch");
                    foreach (string file in files)
                    {
                        if (file.Contains(GetApplicationName().ToUpper()))
                        {
                            try { File.Delete(file); } catch { }
                        }
                    }
                }
            });
            Task Settings = Task.Factory.StartNew(() =>
            {

                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey software = currentUserKey.OpenSubKey("SOFTWARE", true);
                software.DeleteSubKey("MAS");

            });
            await Prefetch;
            await Settings;
            Program.Exit();
        }
    }
}
