using MacrosAPI_v2;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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
        private MacrosManager Lefmanager = null;
        private MacrosManager Rightmanager = null;

        private LeftClicker leftclicker = new LeftClicker();
        private RightClicker rightclicker = new RightClicker();
        private void AutoClicker_Load(object sender, EventArgs e)
        {
            MacrosUpdater Leftupdater = new MacrosUpdater();
            MacrosUpdater Rightupdater = new MacrosUpdater();
            Lefmanager = new MacrosManager(Leftupdater);
            Rightmanager = new MacrosManager(Rightupdater);
            /* Добавление клавиш активаций */
            string[] keys = Enum.GetNames(typeof(Key));
            foreach (string key in keys)
            {
                if (key != "Esc" || key != "LWin" || key != "RWin" || key != "F4" || key != "LAlt" || key != "RAlt")
                {
                    guna2ComboBox1.Items.Add(key);
                    guna2ComboBox2.Items.Add(key);
                }
            }

            keys = Enum.GetNames(typeof(MouseKey));
            foreach (string key in keys)
            {
                if (key != "None" || key != "Left" || key != "Right")
                {
                    guna2ComboBox1.Items.Add(key);
                    guna2ComboBox2.Items.Add(key);
                }
            }

            /* Загрузка левого кликера */
            Task.Factory.StartNew(() =>
            {
                try
                {
                    leftclicker.LoadCFG();
                }
                catch
                {
                    leftclicker.SaveCFG();
                }

                if (leftclicker.settings.key != Key.None)
                {
                    guna2ComboBox1.Invoke((MethodInvoker)(() => guna2ComboBox1.Text = leftclicker.settings.key.ToString()));
                }
                else if (leftclicker.settings.mouseKey != MouseKey.None)
                {
                    guna2ComboBox1.Invoke((MethodInvoker)(() => guna2ComboBox1.Text = leftclicker.settings.mouseKey.ToString()));
                }
                guna2TextBox1.Invoke((MethodInvoker)(() => guna2TextBox1.Text = leftclicker.settings.delay.ToString()));

                guna2TrackBar1.Invoke((MethodInvoker)(() => guna2TrackBar1.Value = leftclicker.settings.JiterPower));
                label11.Invoke((MethodInvoker)(() => label11.Text = leftclicker.settings.JiterPower.ToString() + " p"));

                guna2TrackBar2.Invoke((MethodInvoker)(() => guna2TrackBar2.Value = leftclicker.settings.Jiterdelay));
                label10.Invoke((MethodInvoker)(() => label10.Text = leftclicker.settings.Jiterdelay.ToString() + " ms"));

                guna2TrackBar3.Invoke((MethodInvoker)(() => guna2TrackBar3.Value = leftclicker.settings.Randomize));
                label12.Invoke((MethodInvoker)(() => label12.Text = leftclicker.settings.Randomize.ToString() + " ms"));
            });

            /* Загрузка правого кликера */
            Task.Factory.StartNew(() =>
            {
                try
                {
                    rightclicker.LoadCFG();
                }
                catch
                {
                    rightclicker.SaveCFG();
                }

                /* Загрузка правого кликера */
                if (rightclicker.settings.key != Key.None)
                {
                    guna2ComboBox2.Invoke((MethodInvoker)(() => rightclicker.settings.key.ToString()));
                }
                else if (rightclicker.settings.mouseKey != MouseKey.None)
                {
                    guna2ComboBox2.Invoke((MethodInvoker)(() => guna2ComboBox2.Text = rightclicker.settings.mouseKey.ToString()));
                }
                guna2TextBox2.Invoke((MethodInvoker)(() => guna2TextBox2.Text = rightclicker.settings.delay.ToString()));
            });


            /* Загрузка макроса */
            Lefmanager.LoadMacros(leftclicker);
            Rightmanager.LoadMacros(rightclicker);
        }

        private void AutoClicker_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        public class RightClicker : Macros
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
                if (mas.GetValue("Right Auto Clicker") != null)
                {
                    settings = JsonConvert.DeserializeObject<Settings>(mas.GetValue("Right Auto Clicker").ToString());
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
                    mas.SetValue("Right Auto Clicker", sr.ReadToEnd());
                }
                File.Delete("temp.txt");
            }
            public override void Update()
            {
                if (activate)
                {
                    MouseDown(MouseKey.Right);
                    Sleep(5);
                    MouseUp(MouseKey.Right);
                    Sleep(settings.delay - 5);
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
                [JsonConverter(typeof(StringEnumConverter))]
                public Key key;
                [JsonConverter(typeof(StringEnumConverter))]
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
        public class LeftClicker : Macros
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
                if (mas.GetValue("Left Auto Clicker") != null)
                {
                    settings = JsonConvert.DeserializeObject<Settings>(mas.GetValue("Left Auto Clicker").ToString());
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
                    mas.SetValue("Left Auto Clicker", sr.ReadToEnd());
                }
                File.Delete("temp.txt");
            }
            public override void Update()
            {
                if (activate)
                {
                    int noice = 0; 
                    if (settings.Randomize > 0)
                    {
                        noice = (int)Noice(settings.Randomize);
                    }
                    MouseDown(MouseKey.Left);
                    Sleep(5);
                    MouseUp(MouseKey.Left);   
                    if (noice >= 0)
                    { 
                        Sleep(settings.delay + noice - 5); 
                    }
                    else 
                    { 
                        Sleep(settings.delay - 5); 
                    }
                }
            }
            private readonly Random random = new Random();
            public float Noice(int multiply = 0, int x = 100, int y = 100)
            {
                switch (multiply)
                {
                    case (0):
                        return 0;
                    default:
                        Utils.Perlin2D perlin = new Utils.Perlin2D(random.Next());
                        float Phi = 0.70710678118f;
                        float noice = perlin.Noise(x, y) + perlin.Noise((x - y) * Phi, (y + x) * Phi) * -1;
                        return noice * multiply;
                }
            }
            public Thread shaking_thread = null;
            public override void Initialize()
            {
                if (shaking_thread != null)
                {
                    shaking_thread.Abort();
                }
                shaking_thread = new Thread(() =>
                {
                    while (true)
                    {
                        Shaking();
                    }
                });
                shaking_thread.Priority = ThreadPriority.Normal;
                shaking_thread.Start();
            }
            public void Shaking()
            {
                if (activate && settings.JiterPower > 0)
                {
                    float noice_x = Noice(settings.JiterPower, 100, 100);
                    float noice_y = Noice(settings.JiterPower, 100, 100);
                    MouseMove((int)Math.Round(noice_x, 0), (int)Math.Round(noice_y, 0));
                    Sleep(5);
                    Sleep(settings.Jiterdelay - 5);
                }
                else { Sleep(15); }
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
                [JsonConverter(typeof(StringEnumConverter))]
                public Key key = Key.None;
                [JsonConverter(typeof(StringEnumConverter))]
                public MouseKey mouseKey = MouseKey.None;
                public int delay = 40;

                public Mode mode = Mode.Hold;


                [JsonProperty("Jiter power")]
                public int JiterPower = 0;
                [JsonProperty("Jiter delay")]
                public int Jiterdelay = 5;
                [JsonProperty("Randomize")]
                public int Randomize = 5;

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
                leftclicker.settings.delay = delay;
                leftclicker.SaveCFG();
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            leftclicker.settings.mouseKey = MouseKey.None;
            leftclicker.settings.key = Key.None;
            if (Enum.TryParse(guna2ComboBox1.Text, out Key key))
            {
                leftclicker.settings.key = key;
                leftclicker.SaveCFG();
            }
            else if (Enum.TryParse(guna2ComboBox1.Text, out MouseKey mkey))
            {
                leftclicker.settings.mouseKey = mkey;
                leftclicker.SaveCFG();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            rightclicker.settings.mouseKey = MouseKey.None;
            rightclicker.settings.key = Key.None;
            if (Enum.TryParse(guna2ComboBox2.Text, out Key key))
            {
                rightclicker.settings.key = key;
                rightclicker.SaveCFG();
            }
            else if (Enum.TryParse(guna2ComboBox2.Text, out MouseKey mkey))
            {
                rightclicker.settings.mouseKey = mkey;
                rightclicker.SaveCFG();
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(guna2TextBox2.Text, out int delay))
            {
                rightclicker.settings.delay = delay;
                rightclicker.SaveCFG();
            }
        }

        private void guna2TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            label11.Text = guna2TrackBar1.Value.ToString();
            label11.Text += " p";
            leftclicker.settings.JiterPower = guna2TrackBar1.Value;
            leftclicker.SaveCFG();
        }

        private void guna2TrackBar2_ValueChanged(object sender, EventArgs e)
        {
            label10.Text = guna2TrackBar2.Value.ToString();
            label10.Text += " ms";
            leftclicker.settings.Jiterdelay = guna2TrackBar2.Value;
            leftclicker.SaveCFG();
        }

        private void guna2TrackBar3_ValueChanged(object sender, EventArgs e)
        {
            label12.Text = guna2TrackBar3.Value.ToString();
            label12.Text += " ms";
            leftclicker.settings.Randomize = guna2TrackBar3.Value;
            leftclicker.SaveCFG();
        }
    }
}
