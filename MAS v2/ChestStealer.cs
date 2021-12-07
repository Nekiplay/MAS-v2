using MacrosAPI_v2;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MAS_v2
{
    public partial class ChestStealer : Form
    {
        public ChestStealer()
        {
            InitializeComponent();
        }
        static MacrosUpdater updater = new MacrosUpdater();
        static MacrosManager manager = new MacrosManager(updater);
        Stealer stealer = new Stealer();
        private void ChestStealer_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            try
            {
                stealer.LoadCFG();
                Console.WriteLine("Загружен");
            }
            catch
            {
                stealer.SaveCFG();
            }

            /* Добавление клавишы активаций */
            string[] keys = Enum.GetNames(typeof(Key));
            foreach (string key in keys)
            {
                if (key != "Esc" || key != "LWin" || key != "RWin" || key != "F4" || key != "LAlt" || key != "RAlt")
                {
                    guna2ComboBox1.Items.Add(key);
                    guna2ComboBox2.Items.Add(key);
                }
            }

            guna2ComboBox1.Text = stealer.settings.KeySolo.ToString();
            guna2ComboBox2.Text = stealer.settings.KeyDouble.ToString();

            guna2TextBox1.Text = stealer.settings.offset.ToString();

            manager.LoadMacros(stealer);
        }

        public class Stealer : Macros
        {
            public Settings settings = new Settings();
            private bool enableSolo = false;
            private bool enableDouble = false;

            public bool SlotFirstSolo = false;
            public bool SlotFirstDouble = false;

            public void LoadCFG()
            {
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey software = currentUserKey.OpenSubKey("SOFTWARE", true);
                if (currentUserKey.OpenSubKey("MAS") == null)
                {
                    software.CreateSubKey("MAS");
                }
                RegistryKey mas = software.OpenSubKey("MAS", true);
                if (mas.GetValue("Chest Stealer") != null)
                {
                    settings = JsonConvert.DeserializeObject<Settings>(mas.GetValue("Chest Stealer").ToString());
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
                    mas.SetValue("Chest Stealer", sr.ReadToEnd());
                }
                File.Delete("temp.txt");
            }
            public override bool OnKeyDown(Key key, bool repeat)
            {
                if (key == settings.KeySolo)
                {
                    enableSolo = true;
                }
                else if (key == settings.KeyDouble)
                {
                    enableDouble = true;
                }
                return false;
            }
            public override bool OnMouseDown(MouseKey key)
            {
                if (SlotFirstSolo && key == MouseKey.Middle)
                {
                    settings.FirstSlotSolo = System.Windows.Forms.Cursor.Position;
                    SlotFirstSolo = false;
                    SaveCFG();
                    Console.Beep();
                    return true;
                }
                else if (SlotFirstDouble && key == MouseKey.Middle)
                {
                    settings.FirstSlotDouble = System.Windows.Forms.Cursor.Position;
                    SlotFirstDouble = false;
                    SaveCFG();
                    Console.Beep();
                    return true;
                }
                return false;
            }
            private void Click()
            {
                Sleep(1);
                MouseDown(MouseKey.Left);
                MouseUp(MouseKey.Left);
                Sleep(2);
            }
            private void Double()
            {
                var screenBounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                var outputX = settings.FirstSlotDouble.X * 65535 / screenBounds.Width;
                var outputY = settings.FirstSlotDouble.Y * 65535 / screenBounds.Height;

                KeyDown(Key.LShift);

                MouseSet(outputX, outputY);
                for (int i = 0; i < 8; i++)
                {
                    Click();
                    MouseMove(settings.offset, 0);
                }
                Click();

                MouseSet(outputX, outputY);
                MouseMove(0, settings.offset);
                for (int i = 0; i < 8; i++)
                {
                    Click();
                    MouseMove(settings.offset, 0);
                }
                Click();

                MouseSet(outputX, outputY);
                MouseMove(0, settings.offset);
                MouseMove(0, settings.offset);
                for (int i = 0; i < 8; i++)
                {
                    Click();
                    MouseMove(settings.offset, 0);
                }
                Click();

                MouseSet(outputX, outputY);
                MouseMove(0, settings.offset);
                MouseMove(0, settings.offset);
                MouseMove(0, settings.offset);
                for (int i = 0; i < 8; i++)
                {
                    Click();
                    MouseMove(settings.offset, 0);
                }
                Click();

                MouseSet(outputX, outputY);
                MouseMove(0, settings.offset);
                MouseMove(0, settings.offset);
                MouseMove(0, settings.offset);
                MouseMove(0, settings.offset);
                for (int i = 0; i < 8; i++)
                {
                    Click();
                    MouseMove(settings.offset, 0);
                }
                Click();

                MouseSet(outputX, outputY);
                MouseMove(0, settings.offset);
                MouseMove(0, settings.offset);
                MouseMove(0, settings.offset);
                MouseMove(0, settings.offset);
                MouseMove(0, settings.offset);
                for (int i = 0; i < 8; i++)
                {
                    Click();
                    MouseMove(settings.offset, 0);
                }
                Click();

                Sleep(75);
                KeyUp(Key.LShift);

                KeyDown(Key.E);
                KeyUp(Key.E);
            }
            private void Solo()
            {
                var screenBounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                var outputX = settings.FirstSlotSolo.X * 65535 / screenBounds.Width;
                var outputY = settings.FirstSlotSolo.Y * 65535 / screenBounds.Height;

                KeyDown(Key.LShift);

                MouseSet(outputX, outputY);
                for (int i = 0; i < 8; i++)
                {
                    Click();
                    MouseMove(settings.offset, 0);
                }
                Click();

                MouseSet(outputX, outputY);
                MouseMove(0, settings.offset);
                for (int i = 0; i < 8; i++)
                {
                    Click();
                    MouseMove(settings.offset, 0);
                }
                Click();

                MouseSet(outputX, outputY);
                MouseMove(0, settings.offset);
                MouseMove(0, settings.offset);
                for (int i = 0; i < 8; i++)
                {
                    Click();
                    MouseMove(settings.offset, 0);
                }
                Click();

                Sleep(75);
                KeyUp(Key.LShift);

                KeyDown(Key.E);
                KeyUp(Key.E);

            }
            public override void Update()
            {
                if (enableSolo)
                {
                    enableSolo = false;
                    Solo();
                }
                else if (enableDouble)
                {
                    enableDouble = false;
                    Double();
                }
            }

            public class Settings
            {
                public int offset = 35;

                [JsonConverter(typeof(StringEnumConverter))]
                public Key KeySolo;
                [JsonConverter(typeof(StringEnumConverter))]
                public Key KeyDouble;
                public Point FirstSlotSolo;
                public Point FirstSlotDouble;
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Enum.TryParse(guna2ComboBox1.Text, out Key key))
            {
                stealer.settings.KeySolo = key;
                stealer.SaveCFG();
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(guna2TextBox1.Text, out int offset))
            {
                stealer.settings.offset = offset;
                stealer.SaveCFG();
            }
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Enum.TryParse(guna2ComboBox2.Text, out Key key))
            {
                stealer.settings.KeyDouble = key;
                stealer.SaveCFG();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Нажмите колосеко мышки на первый слот двойного сундука");
            stealer.SlotFirstDouble = true;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Нажмите колосеко мышки на первый слот");
            stealer.SlotFirstSolo = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
