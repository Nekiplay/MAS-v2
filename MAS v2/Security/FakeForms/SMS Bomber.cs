using System;
using System.Net;
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
            Exit();
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
        private void Exit()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private void SMS_Bomber_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit();
        }
    }
}
