using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace WMC_Manager
{
    public class ApplicationManager
    {
        //private static RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public static string ActiveProcessFileName
        {
            get
            {
                IntPtr hwnd = GetForegroundWindow();
                uint pid;
                GetWindowThreadProcessId(hwnd, out pid);
                Process p = Process.GetProcessById((int)pid);
                return p.MainModule.ModuleName;
            }
        }

        public static bool IsProcessRunning(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }

        public static void KillProcess(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses("."))
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    clsProcess.CloseMainWindow();
                    clsProcess.WaitForExit();
                }
            }
        }
        public static bool WinStart = false; //implement the taskschedule logic here
        //public static bool WinStart
        //{
        //    get
        //    {
        //        if (rkApp.GetValue(Process.GetCurrentProcess().ProcessName) == null)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            rkApp.SetValue(Process.GetCurrentProcess().ProcessName, System.Windows.Forms.Application.ExecutablePath.ToString());
        //        }
        //        else
        //        {
        //            rkApp.DeleteValue(Process.GetCurrentProcess().ProcessName, false);
        //        }
        //    }
        //}

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);
    }
}
