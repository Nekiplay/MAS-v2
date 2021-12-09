using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (int.TryParse(guna2TextBox1.Text, out int password))
                {
                    Program.SecurityManager.settings.Password = password.ToString();
                    Program.SecurityManager.SaveCFG();
                }
                else
                {

                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
