﻿using MacrosAPI_v2;
using System;
using System.Windows.Forms;

namespace MAS_v2.Security.FakeForms
{
    public partial class WinlockerForm : Form
    {
        public WinlockerForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }
        MacrosManager manager;
        private void WinlockerForm_Load(object sender, EventArgs e)
        {
            MacrosUpdater updater = new MacrosUpdater();
            manager = new MacrosManager(updater);
            manager.LoadMacros(new Locker());
        }

        public class Locker : Macros
        {
            public override bool OnKeyDown(Key key, bool repeat)
            {
                return true;
            }
            public override bool OnKeyUp(Key key)
            {
                return true;
            }
            public override bool OnMouseDown(MouseKey key)
            {
                if (key == MouseKey.Right || key == MouseKey.Middle || key == MouseKey.Button1 || key == MouseKey.Button2)
                {
                    return true;
                }
                return false;
            }
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == Program.SecurityManager.settings.Password)
            {
                manager.Quit();
                this.Hide();
                Program.MenuSelector.Show();
            }
            else
            {
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = "";
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "1";
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "2";
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "3";
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "4";
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "5";
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "6";
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "7";
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "8";
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "9";
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "0";
        }
    }
}