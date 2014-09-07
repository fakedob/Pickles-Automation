using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace WMC_Manager
{
    public class SystemManager
    {
        private bool IsReceiverOn()
        {
            string default_monitor = "HCG0163";
            bool connected = false;
            string query = string.Format("Select DeviceID from Win32_PnPEntity where DeviceID like '%{0}%'", default_monitor);
            using (ManagementObjectSearcher wmiSearcher = new ManagementObjectSearcher("\\root\\cimv2", query))
            {
                if (wmiSearcher.Get().Count > 0)
                {
                    connected = true;
                }
            }
            return connected;
            //if (!connected)
            //{
            //    SendIR(newAVDeviceDB.GetIRPacket("New AVDevice", "On"));
            //    PowerOnTV();
            //}
            //else
            //{
            //    SendIR(newAVDeviceDB.GetIRPacket("New AVDevice", "Tv Input"), true);
            //    System.Threading.Thread.Sleep(500);
            //}
        }
        public static void IsAudioDevicePresent()
        {
            ManagementObjectSearcher mo = new ManagementObjectSearcher("select * from Win32_SoundDevice");

            foreach (ManagementObject soundDevice in mo.Get())
            {
                //debugPrint((string)soundDevice.GetPropertyValue("DeviceId"));
                //debugPrint((string)soundDevice.GetPropertyValue("Manufacturer"));
                // etc                       
            }
        }

    }

}
