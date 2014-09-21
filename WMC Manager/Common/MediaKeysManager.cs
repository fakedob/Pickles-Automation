using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace WMC_Manager.Common
{
    public static class MediaKeysManager
    {
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;
        private const int APPCOMMAND_MEDIA_PLAY_PAUSE = 0xE0000;


        [DllImport("user32.dll")]
        private static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);

        public static void PlayPause()
        {
            SendMessageW(PicklesAutomation.ConfigManager.WindowHandle, WM_APPCOMMAND, PicklesAutomation.ConfigManager.WindowHandle,
                (IntPtr)APPCOMMAND_MEDIA_PLAY_PAUSE);
        }

        public static void Mute()
        {
            SendMessageW(PicklesAutomation.ConfigManager.WindowHandle, WM_APPCOMMAND, PicklesAutomation.ConfigManager.WindowHandle,
                (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }

        public static void VolumeDecrease()
        {
            SendMessageW(PicklesAutomation.ConfigManager.WindowHandle, WM_APPCOMMAND, PicklesAutomation.ConfigManager.WindowHandle,
                (IntPtr)APPCOMMAND_VOLUME_DOWN);
        }

        public static void VolumeIncrease()
        {
            SendMessageW(PicklesAutomation.ConfigManager.WindowHandle, WM_APPCOMMAND, PicklesAutomation.ConfigManager.WindowHandle,
                (IntPtr)APPCOMMAND_VOLUME_UP);
        }
    }
}
