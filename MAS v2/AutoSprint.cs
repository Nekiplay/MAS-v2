using MacrosAPI_v2;
using System;
using System.Windows.Forms;

namespace MAS_v2
{
    public partial class AutoSprint : Form
    {
        public AutoSprint()
        {
            InitializeComponent();
        }
        static MacrosUpdater updater = new MacrosUpdater();
        static MacrosManager manager = new MacrosManager(updater);
        Sprint sprint = new Sprint();
        private void AutoSprint_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            manager.LoadMacros(sprint);
        }

        public class Sprint : Macros
        {
            private bool enabled = false;
            public bool activate = false;
            public override void Update()
            {
                if (enabled && activate)
                {
                    KeyDown(Key.LControl);
                }
            }

            public override bool OnKeyDown(Key key, bool repeat)
            {
                if (key == Key.W)
                {
                    enabled = true;
                }
                return false;
            }
            public override bool OnKeyUp(Key key)
            {
                if (key == Key.W && activate)
                {
                    enabled = false;
                    KeyUp(Key.LControl);
                }
                return false;
            }
        }

        private void AutoSprint_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            sprint.activate = guna2CheckBox1.Checked;
        }
    }
}
