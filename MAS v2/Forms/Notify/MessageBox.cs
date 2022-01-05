using System;
using System.Windows.Forms;

namespace MAS_v2.Forms.Notify
{
    public partial class MessageBox : Form
    {
        public MessageBox(string caption, string text)
        {
            InitializeComponent();
            label4.Text = caption;
            label1.Text = text;
        }

        private void MessageBox_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
