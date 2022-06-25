using MacrosAPI_v3;
using System;
using System.Windows.Forms;

namespace MAS_v2
{
    public partial class AutoSprint : Form
    {
        private readonly Sprint sprint = new Sprint();

        public AutoSprint()
        {
            InitializeComponent();
        }

        private void AutoSprint_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            Program.manager.LoadMacros(sprint);
        }

        private void AutoSprint_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            switch (guna2CheckBox1.Checked)
            {
                case (false):
                    Program.manager.UnLoadMacros(sprint);
                    break;
                default:
                    Program.manager.UnLoadMacros(sprint);
                    Program.manager.LoadMacros(sprint);
                    break;
            }
            sprint.activate = guna2CheckBox1.Checked;
        }

        public class Sprint : Macros
        {
            public bool activate;
            private bool enabled;

            public override void Update()
            {
                if (enabled && activate) KeyDown(Key.LControl);
            }

            public override bool OnKeyDown(Key key, bool repeat)
            {
                switch (key)
                {
                    case Key.W:
                        enabled = true;
                        break;
                }

                return false;
            }

            public override bool OnKeyUp(Key key)
            {
                switch (key)
                {
                    case Key.W:
                        switch (activate)
                        {
                            case true:
                                enabled = false;
                                KeyUp(Key.LControl);
                                break;
                        }

                        break;
                }

                return false;
            }
        }
    }
}