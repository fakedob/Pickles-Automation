using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AppManager
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null)
            {
                try
                {
                    Process[] processes = Process.GetProcessesByName(args[0]);
                    foreach (Process proc in processes)
                    {
                        proc.CloseMainWindow();
                        proc.WaitForExit();
                    }
                }
                catch { }
            }
        }
    }
}
