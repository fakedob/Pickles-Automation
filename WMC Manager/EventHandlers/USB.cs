using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace PicklesAutomation
{
    public class USB
    {
        static ManagementEventWatcher w = null;
        static DateTime lastDeviceChangeTime = DateTime.Now;
        public delegate void Callback();
        static Callback myCallback;
        static int myFilterSeconds;
        public static void OnUSBChanged(Callback callback, int filterSeconds)
        {
            myCallback = callback;
            myFilterSeconds = filterSeconds;
            AddInsertUSBHandler(USBChanged);
            AddRemoveUSBHandler(USBChanged);
        }

        static void AddRemoveUSBHandler(EventArrivedEventHandler myHandler)
        {
            WqlEventQuery q;
            ManagementScope scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;
            try
            {
                q = new WqlEventQuery();
                q.EventClassName = "__InstanceDeletionEvent";
                q.WithinInterval = new TimeSpan(0, 0, 3);
                q.Condition = "TargetInstance ISA 'Win32_USBControllerdevice'";
                w = new ManagementEventWatcher(scope, q);
                w.EventArrived += myHandler;
                w.Start();
            }
            catch (Exception e)
            {
                if (w != null)
                {
                    w.Stop();

                }
            }

        }

        static void AddInsertUSBHandler(EventArrivedEventHandler myHandler)
        {
            WqlEventQuery q;
            ManagementScope scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;
            try
            {
                q = new WqlEventQuery();
                q.EventClassName = "__InstanceCreationEvent";
                q.WithinInterval = new TimeSpan(0, 0, 3);
                q.Condition = "TargetInstance ISA 'Win32_USBControllerdevice'";
                w = new ManagementEventWatcher(scope, q);
                w.EventArrived += myHandler;
                w.Start();
            }
            catch (Exception e)
            {
                if (w != null)
                {
                    w.Stop();
                }
            }
        }
        
        static void USBChanged(object sender, EventArgs e)
        {
            if ((DateTime.Now - lastDeviceChangeTime).TotalSeconds >= myFilterSeconds)
            {
                myCallback();
            }
            lastDeviceChangeTime = DateTime.Now;
        }
    }
}
