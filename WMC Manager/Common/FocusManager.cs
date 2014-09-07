using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace FocusManager
{
    public class Focus
    {
        public delegate void ApplicationFocusChangeEvent(string activeApplication);
        public static ApplicationFocusChangeEvent OnApplicationFocusChange = null;

        public static void Initialize()
        {
            string previousApp = string.Empty;
            new Thread(() =>
            {
                while (PicklesAutomation.ConfigManager.KeepThreadsAlive)
                {
                    if (OnApplicationFocusChange != null)
                    {
                        string currentApp = GetActiveProcessFileName();
                        if (!currentApp.Equals(previousApp))
                        {
                            previousApp = currentApp;
                            OnApplicationFocusChange(currentApp);
                        }
                    }
                    Thread.Sleep(500);
                }
            }).Start();
        }

        public static bool SetProcessFocus(string processName)
        {
            Process[] myProc = Process.GetProcessesByName(processName);
            for (int i = 0; i < myProc.Length; i++)
            {
                SetForegroundWindow(myProc[i].MainWindowHandle);
                return true;
            }
            return false;
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        static string GetActiveProcessFileName()
        {
            IntPtr hWnd = GetForegroundWindow();
            uint procId = 0;
            GetWindowThreadProcessId(hWnd, out procId);
            return Process.GetProcessById((int)procId).ProcessName;
        }

    }

    //public class Focus
    //{
    //    delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType,
    //    IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

    //    public delegate void FocusChange();
    //    public static FocusChange OnFocusChange;

    //    [DllImport("user32.dll")]
    //    static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr
    //       hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess,
    //       uint idThread, uint dwFlags);

    //    [DllImport("user32.dll")]
    //    static extern bool UnhookWinEvent(IntPtr hWinEventHook);

    //    const uint EVENT_OBJECT_NAMECHANGE = 0x800C;
    //    const uint WINEVENT_OUTOFCONTEXT = 0;

    //    // Need to ensure delegate is not collected while we're using it,
    //    // storing it in a class field is simplest way to do this.
    //    static WinEventDelegate procDelegate = new WinEventDelegate(WinEventProc);

    //    public static void Initialize()
    //    {
    //        // Listen for name change changes across all processes/threads on current desktop...
    //        IntPtr hhook = SetWinEventHook(EVENT_OBJECT_NAMECHANGE, EVENT_OBJECT_NAMECHANGE, IntPtr.Zero,
    //                procDelegate, 0, 0, WINEVENT_OUTOFCONTEXT);

    //        // MessageBox provides the necessary mesage loop that SetWinEventHook requires.
    //        // In real-world code, use a regular message loop (GetMessage/TranslateMessage/
    //        // DispatchMessage etc or equivalent.)
    //        //MessageBox.Show("Tracking name changes on HWNDs, close message box to exit.");

    //        //UnhookWinEvent(hhook);
    //    }

    //    static void WinEventProc(IntPtr hWinEventHook, uint eventType,
    //        IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
    //    {
    //        // filter out non-HWND namechanges... (eg. items within a listbox)
    //        if (idObject != 0 || idChild != 0)
    //        {
    //            return;
    //        }
    //        if (OnFocusChange != null)
    //        {
    //            OnFocusChange();
    //        }
    //        //Console.WriteLine("Text of hwnd changed {0:x8}", hwnd.ToInt32());
    //    }
    //}
}
