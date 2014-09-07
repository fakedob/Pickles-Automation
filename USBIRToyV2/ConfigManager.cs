using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USBIRToyv2
{
    public class ConfigManager
    {
        public static bool PlaySound = false;

        public readonly static decimal PulseTimerFQ = 21.3333m;
        public readonly static byte Reset = 0x00;
        public readonly static byte Timer = 0x04;
        public readonly static byte SamplingMode = 0x73;
        public readonly static string SamplingModeProtocol = "S01";
        public readonly static byte[] HandShake = new byte[] { 0x24, 0x25, 0x26 };
        public readonly static byte TransmitStart = 0x03;
        public readonly static int TransmitTimeout = 200;
        public readonly static byte[] TransmitEnd = new byte[] { 0xff, 0xff, 0x03 };//new byte[] { 0xff, 0xff, 0x03 };
        public readonly static byte SetTransmitFQ = 0x06;
        public readonly static byte DontCareByte = 0x00;
    }
}
