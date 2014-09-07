using System;
using System.Web;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Configuration;
using System.Globalization;
using System.Threading;
using PicklesAutomation.Common;
using Interceptor;

namespace PicklesAutomation
{
    public class ConfigManager
    {
        public static CultureInfo CurrentCulture
        {
            get { return System.Threading.Thread.CurrentThread.CurrentCulture; }
        }
        public static string ServerEnvironment
        {
            get { return System.Environment.MachineName; }
        }
        public static bool KeepThreadsAlive { get; set; }

        public static bool IsDev { get; set; }
        public static IntPtr WindowHandle { get; set; }
        public static Input HIDInput { get; set; }
        public static bool IsSystemLocked { get; set; }
        public static bool IsDisplayOn { get; set; }
        public static bool IsCursorVisible { get; set; }

        public static bool IsApplicationLocked { get; set; }
        public static readonly char[] UnlockSequenceChars = new char[] { '7', '3', '6', '6' };

        public const string CoreApp = "ehshell";
        public const string CoreAppName = CoreApp + ".exe";
        public static readonly string CoreAppPath = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\ehome\" + CoreAppName);
    }
}
