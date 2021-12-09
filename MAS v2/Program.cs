using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAS_v2
{
    static class Program
    {
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
            MenuSelector =new MenuSelector();
            SecurityManager = new Security.SecurityManager();
            SecurityManager.LoadCFG();
            switch (SecurityManager.settings.FakeMenu)
            {
                case (0):
                    Application.Run(MenuSelector = new MenuSelector());
                    break;
                case (1):
                    Application.Run(new Security.FakeForms.WinlockerForm());
                    break;
            }
        }
    }
}
