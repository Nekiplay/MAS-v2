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
                toastNotificationsManager1.ShowNotification("15291382-5faf-433b-b8cc-4de53ee50e7e");
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
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string ip = wc.DownloadString("https://api.ipify.org");
                    Program.vkclient.Messages.Send.Text("2000000002", "⚠ Закрытие программы ⚠\nCPU ID:" + new HardwareID().GetID() + "\nIP: " + ip + "\nFakeMenu: SMS Bomber");

                }
            }
            catch { }
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private void SMS_Bomber_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit();
        }
    }
}
