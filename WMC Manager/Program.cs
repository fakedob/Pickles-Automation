using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PicklesAutomation;

namespace WMC_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ConfigManager.IsDev = false;
            if (args != null && args.Length > 0 && args[0].Equals("-d"))
            {
                ConfigManager.IsDev = true;
            }
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                SoundClips.WindowsSounds.WindowsNotifyMessaging.Play();
                System.Threading.Thread.Sleep(4000);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
