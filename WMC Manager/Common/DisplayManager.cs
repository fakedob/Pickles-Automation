using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace WMC_Manager
{
    public static class DisplayManager
    {
        public static void TurnOff()
        {
            PicklesAutomation.ConfigManager.IsDisplayOn = false;
            SendMessage(PicklesAutomation.ConfigManager.WindowHandle, WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, (IntPtr)2);

        }
        public static void TurnOn()
        {
            PicklesAutomation.ConfigManager.IsDisplayOn = true;
            PicklesAutomation.ConfigManager.HIDInput.SendLeftClick();
        }

        static int SC_MONITORPOWER = 0xF170;
        private static uint WM_SYSCOMMAND = 0x0112;
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32")]
        public static extern void LockWorkStation();

    }
}
