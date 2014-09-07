using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicklesAutomation
{
    [Serializable]
    public class Configuration
    {
        public Configuration()
        {
            SettingsRefreshRate = 1;
            USBFilterSeconds = 5;
        }
        public int SettingsRefreshRate { get; set; }
        public int USBFilterSeconds { get; set; }
    }

    public class Parameters
    {
        public Parameters()
        {
        }
        public const string ServiceName = "Pickles Automation";
        public const string ServiceDesc = "This is a template service by HEDWIN for Pickles Automation";
    }
}
