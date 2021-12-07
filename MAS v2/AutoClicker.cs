using MacrosAPI_v2;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Windows.Forms;

namespace MAS_v2
{
    public partial class AutoClicker : Form
    {
        public AutoClicker()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void AutoClicker_Load(object sender, EventArgs e)
        {
            /* Добавление клавишы активаций */

            try
            {
                clicker.LoadCFG();
            }
            catch
            {
                clicker.SaveCFG();
            }

            string[] keys = Enum.GetNames(typeof(Key));
            foreach (string key in keys)
            {
                if (key != "Esc" || key != "LWin" || key != "RWin" || key != "F4" || key != "LAlt" || key != "RAlt")
                {
                    guna2ComboBox1.Items.Add(key); 
                }
            }

            keys = Enum.GetNames(typeof(MouseKey));
            foreach (string key in keys)
            {
                if (key != "None" || key != "Left" || key != "Right") { guna2ComboBox1.Items.Add(key); }
            }


            guna2ComboBox1.Text = clicker.settings.key.ToString();
            guna2TextBox1.Text = clicker.settings.delay.ToString();

            /* Загрузка макроса */
            manager.LoadMacros(clicker);
        }
        private static MacrosUpdater updater = new MacrosUpdater();
        private static MacrosManager manager = new MacrosManager(updater);
        private Clicker clicker = new Clicker();

        private void AutoClicker_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        public class Clicker : Macros
        {
            private bool activate = false;
            public Settings settings = new Settings();
            public void LoadCFG()
            {
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey software = currentUserKey.OpenSubKey("SOFTWARE", true);
                if (currentUserKey.OpenSubKey("MAS") == null)
                {
                    software.CreateSubKey("MAS");
                }
                RegistryKey mas = software.OpenSubKey("MAS", true);
                if (mas.GetValue("Auto Clicker") != null)
                {
                    settings = JsonConvert.DeserializeObject<Settings>(mas.GetValue("Auto Clicker").ToString());
                }
            }
            public void SaveCFG()
            {
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey software = currentUserKey.OpenSubKey("SOFTWARE", true);
                if (currentUserKey.OpenSubKey("MAS") == null)
                {
                    software.CreateSubKey("MAS");
                }
                RegistryKey mas = software.OpenSubKey("MAS", true);

                File.Create("temp.txt").Close();

                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.Formatting = Formatting.Indented;
                using (StreamWriter sw = new StreamWriter("temp.txt"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, settings);
                }
                using (StreamReader sr = new StreamReader("temp.txt"))
                {
                    mas.SetValue("Auto Clicker", sr.ReadToEnd());
                }
                File.Delete("temp.txt");
            }
            public override void Update()
            {
                if (activate)
                {
                    MouseDown(MouseKey.Left);
                    MouseUp(MouseKey.Left);
                    Sleep(settings.delay);
                }
            }
            public override bool OnMouseDown(MouseKey key)
            {
                if (settings.mouseKey == key)
                {
                    activate = true;
                }
                return false;
            }
            public override bool OnMouseUp(MouseKey key)
            {
                if (settings.mouseKey == key)
                {
                    activate = false;
                }
                return false;
            }
            public override bool OnKeyDown(Key key, bool repeat)
            {
                if (settings.key == key)
                {
                    activate = true;
                }
                return false;
            }
            public override bool OnKeyUp(Key key)
            {
                if (settings.key == key)
                {
                    activate = false;
                }
                return false;
            }

            public class Settings
            {
                public Key key;
                public MouseKey mouseKey;
                public int delay = 40;

                public Mode mode = Mode.Hold;

                public enum Mode
                {
                    Hold,
                    Toggle,
                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(guna2TextBox1.Text, out int delay))
            {
                if (delay < 3)
                {
                    delay = 3;
                    guna2TextBox1.Text = delay.ToString();
                }
                clicker.settings.delay = delay;
                clicker.SaveCFG();
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            clicker.settings.mouseKey = MouseKey.None;
            clicker.settings.key = Key.None;
            if (Enum.TryParse(guna2ComboBox1.Text, out Key key))
            {
                clicker.settings.key = key;
                clicker.SaveCFG();
            }
            else if (Enum.TryParse(guna2ComboBox1.Text, out MouseKey mkey))
            {
                clicker.settings.mouseKey = mkey;
                clicker.SaveCFG();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
