using MacrosAPI_v3;
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
        public static MacrosUpdater updater = new MacrosUpdater();
        public static MacrosManager manager = new MacrosManager(updater);

        public static MenuSelector MenuSelector;
        public static Security.SecurityManager SecurityManager;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
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
            Process.GetCurrentProcess().Kill();
        }
    }
}
