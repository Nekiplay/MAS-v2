using MacrosAPI_v3;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAS_v2.Forms
{
    public partial class NinjaBridge : Form
    {
        public NinjaBridge()
        {
            InitializeComponent();
        }

        private void NinjaBridge_Load(object sender, EventArgs e)
        {
            try { macro.LoadCFG(); } catch { }


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
                if (key != "None" || key != "Left" || key != "Right")
                {
                    guna2ComboBox1.Items.Add(key);
                }
            }

            guna2ComboBox2.Items.Add("1 block");
            guna2ComboBox2.Items.Add("2 block");

            /* Загрузка CFG */
            if (macro.settings.key != null)
                guna2ComboBox1.Text = macro.settings.key.ToString();
            else if (macro.settings.mouseKey != null)
                guna2ComboBox1.Text = macro.settings.mouseKey.ToString();

            switch (macro.settings.mode)
            {
                case (NinjaBridgeMacro.Settings.Mode.block1):
                    guna2ComboBox2.Text = "1 block";
                    break;
                case (NinjaBridgeMacro.Settings.Mode.block2):
                    guna2ComboBox2.Text = "2 block";
                    break;
                case (NinjaBridgeMacro.Settings.Mode.block3):
                    guna2ComboBox2.Text = "3 block";
                    break;
            }
        }

        public class NinjaBridgeMacro : Macros
        {
            private void RightClick()
            {
                MouseDown(MouseKey.Right);
                Sleep(5);
                MouseUp(MouseKey.Right);
            }
            public Settings settings = new Settings();
            public override void Update()
            {
                switch (activate)
                {
                    case (true):

                        switch (settings.mode)
                        {
                            case (Settings.Mode.block1):
                                RightClick();
                                RightClick();
                                KeyDown(Key.S);
                                Sleep(210);
                                KeyDown(Key.LShift);
                                Sleep(155);
                                KeyUp(Key.S);
                                Sleep(85);
                                KeyUp(Key.LShift);
                                RightClick();
                                Sleep(25);
                                RightClick();
                                break;
                            case (Settings.Mode.block2):
                                RightClick();
                                RightClick();
                                KeyDown(Key.S);
                                Sleep(235);
                                RightClick();
                                for (int i = 0; i < 8; i++)
                                {
                                    Sleep(2);
                                    RightClick();
                                }
                                Sleep(140);
                                KeyDown(Key.LShift);
                                Sleep(145);
                                KeyUp(Key.S);
                                Sleep(106);
                                KeyUp(Key.LShift);
                                RightClick();
                                Sleep(50);
                                break;
                        }
                        break;
                }
            }
            private bool activate = false;
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

            public void LoadCFG()
            {
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey software = currentUserKey.OpenSubKey("SOFTWARE", true);
                if (currentUserKey.OpenSubKey("MAS") == null)
                {
                    software.CreateSubKey("MAS");
                }
                RegistryKey mas = software.OpenSubKey("MAS", true);
                if (mas.GetValue("Ninja Bridge") != null)
                {
                    settings = JsonConvert.DeserializeObject<Settings>(mas.GetValue("Ninja Bridge").ToString());
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
                    mas.SetValue("Ninja Bridge", sr.ReadToEnd());
                }
                File.Delete("temp.txt");
            }
            public class Settings
            {
                [JsonConverter(typeof(StringEnumConverter))]
                public Key key;
                [JsonConverter(typeof(StringEnumConverter))]
                public MouseKey mouseKey;
                [JsonConverter(typeof(StringEnumConverter))]
                public Mode mode;


                public enum Mode
                {
                    block1,
                    block2,
                    block3
                }
            }
        }
        private NinjaBridgeMacro macro = new NinjaBridgeMacro();
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            macro.settings.mouseKey = MouseKey.None;
            macro.settings.key = Key.None;
            if (Enum.TryParse(guna2ComboBox1.Text, out Key key))
            {
                if (key == Key.None)
                {
                    Program.manager.UnLoadMacros(macro);
                }
                else
                {
                    Program.manager.UnLoadMacros(macro);
                    Program.manager.LoadMacros(macro);
                }
                macro.settings.key = key;
                macro.SaveCFG();
            }
            else if (Enum.TryParse(guna2ComboBox1.Text, out MouseKey mkey))
            {
                macro.settings.mouseKey = mkey;
                macro.SaveCFG();
            }
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox2.SelectedItem != null)
            {
                switch (guna2ComboBox2.SelectedItem.ToString())
                {
                    case ("1 block"):
                        macro.settings.mode = NinjaBridgeMacro.Settings.Mode.block1;
                        macro.SaveCFG();
                        break;
                    case ("2 block"):
                        macro.settings.mode = NinjaBridgeMacro.Settings.Mode.block2;
                        macro.SaveCFG();
                        break;
                    case ("3 block"):
                        macro.settings.mode = NinjaBridgeMacro.Settings.Mode.block3;
                        macro.SaveCFG();
                        break;
                }
            }
        }

        private void NinjaBridge_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
