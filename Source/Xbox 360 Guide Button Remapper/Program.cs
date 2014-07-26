using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xbox_360_Guide_Button_Remapper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (File.Exists("Keys_config.bin"))
            {
                Application.Run(new frmMain(false));
            }
            else
            {
                Application.Run(new frmMain(true));
            }
        }
    }
}
