using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.IO.Ports;

namespace USBIRToyv2
{
    public class SerialPortManager
    {
        /// <summary>
        /// Compile an array of COM port names associated with given VID and PID
        /// </summary>
        /// http://www.codeproject.com/Tips/349002/Select-a-USB-Serial-Device-via-its-VID-PID
        /// <param name="VID"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public static List<string> ComPortNames(String VID, String PID)
        {
            String pattern = String.Format("^VID_{0}.PID_{1}", VID, PID);
            Regex _rx = new Regex(pattern, RegexOptions.IgnoreCase);
            List<string> comports = new List<string>();
            RegistryKey rk1 = Registry.LocalMachine;
            RegistryKey rk2 = rk1.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum");
            foreach (String s3 in rk2.GetSubKeyNames())
            {
                RegistryKey rk3 = rk2.OpenSubKey(s3);
                foreach (String s in rk3.GetSubKeyNames())
                {
                    if (_rx.Match(s).Success)
                    {
                        RegistryKey rk4 = rk3.OpenSubKey(s);
                        foreach (String s2 in rk4.GetSubKeyNames())
                        {
                            RegistryKey rk5 = rk4.OpenSubKey(s2);
                            RegistryKey rk6 = rk5.OpenSubKey("Device Parameters");
                            //foreach (string port_name in SerialPort.GetPortNames())
                            //{
                            string port_name = (string)rk6.GetValue("PortName");
                            if (SerialPort.GetPortNames().Contains(port_name))
                            //if (names.Contains(port_name))
                            {
                                comports.Add(port_name);
                            }
                            //}
                        }
                    }
                }
            }
            return comports;
        } 
    }
}
