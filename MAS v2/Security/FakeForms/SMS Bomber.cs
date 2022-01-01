using System;
using System.Windows.Forms;

namespace MAS_v2.Security.FakeForms
{
    public partial class SMS_Bomber : Form
    {
        public SMS_Bomber()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == Program.SecurityManager.settings.Password)
            {
                this.Hide();
                Program.MenuSelector.Show();
            }
        }

        private void SMS_Bomber_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void SMS_Bomber_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
