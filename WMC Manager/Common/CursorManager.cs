using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;

public static class CursorManager
{
    private const string blankCursor = "blank.cur";
    private const string registryPath = @"Control Panel\Cursors\";
    private const string registrySection = @"HKEY_CURRENT_USER\" + registryPath;

    private static Dictionary<string, object> myOgirinalValues;
    private static bool initialized = false;
    public static void CursorVisible(bool visible = true)
    {
        Array Cursors = Enum.GetValues(typeof(Cursors));
        if (!initialized)
        {
            myOgirinalValues = new Dictionary<string, object>();
            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(registryPath))
            {
                string[] myValues = regKey.GetValueNames();
                for (int i = 0; i < myValues.Length; i++)
                {
                    myOgirinalValues.Add(myValues[i], regKey.GetValue(myValues[i]));
                }
                initialized = true;
            }
        }
        if (visible)
        {
            foreach (KeyValuePair<string, object> myInitialRegistry in myOgirinalValues)
            {
                Registry.SetValue(registrySection, myInitialRegistry.Key, myInitialRegistry.Value);
            }
        }
        else
        {
            
            for (int i = 0; i < Cursors.Length; i++)
            {
                Registry.SetValue(registrySection, ((Cursors)Cursors.GetValue(i)).ToString(), blankCursor);
            }
        }
        SystemParametersInfo(SPI_SETCURSORS, 0, 0, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
        PicklesAutomation.ConfigManager.IsCursorVisible = visible;
    }

    public enum Cursors
    {
        Arrow,
        Wait,
        SizeNS,
        SizeWE,
        SizeAll,
        IBeam,
        AppStarting,
        Hand,
        Help,
        Crosshair,
        SizeNWSE,
        SizeNESW
    }

    const uint SPI_SETCURSORS = 0x0057;
    const uint SPIF_UPDATEINIFILE = 0x01;
    const uint SPIF_SENDCHANGE = 0x02;

    [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
    public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);
}

