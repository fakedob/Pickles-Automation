using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace USBIRToyv2
{
    public class IRToyList : List<IRToy>{}
    public class IRToy
    {
        #region Public
        private bool keepAlive = true;
        private string deviceDescription = null;

        private List<byte[]> TransmitBuffer;
        public IRToy(string com_port)
        {
            deviceDescription = com_port;
            IRToySerialPort = new SerialPort(com_port);
            if (IRToySerialPort.IsOpen)
            {
                throw new Exception("Port is already being used!");
            }
            IRToySerialPort.BaudRate = 128000;
            IRToySerialPort.Parity = Parity.None;
            IRToySerialPort.StopBits = StopBits.One;
            IRToySerialPort.DataBits = 8;
            IRToySerialPort.Handshake = Handshake.None;
            //IRToySerialPort.RtsEnable = true;
            IRToySerialPort.Open();
            ResetDevice();
            if (!SetSamplingMode())
            {
                throw new Exception("Device not recognized!");
            }
            else
            {
                SetHandShake();
                TransmitBuffer = new List<byte[]>();
                StartDeviceThread();
            }
        }

        public delegate void KeyReceivedHandler(object sender, KeyReceivedEventArgs e);

        public event KeyReceivedHandler OnKeyReceived;

        public string IRToyDesc
        {
            get
            {
                return deviceDescription;
            }
        }

        private SerialPort IRToySerialPort { get; set; }

        private Thread IRToyRS { get; set; }
        private Thread IRToyTX { get; set; }

        public void Transmit(short[] KeyValue)
        {
            try
            {
                List<byte[]> tempPulseList = new List<byte[]>();
                foreach (short pulse in KeyValue)
                {
                    tempPulseList.Add(BitConverter.GetBytes(pulse));
                }
                byte[] key_to_send = new byte[tempPulseList.Count * 2];
                int count_pulses = 0;
                foreach (byte[] pulse in tempPulseList)
                {
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(pulse);
                    }
                    key_to_send[count_pulses] = pulse[0];
                    key_to_send[count_pulses + 1] = pulse[1];
                    count_pulses += 2;
                }
                TransmitBuffer.Add(key_to_send);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public void Transmit(string KeyName, string RemoteDesc = null)
        {
                try
                {
                    DBRemoteKey myRemoteKey = DBManager.GetDBRemoteKeyByDesc(KeyName, RemoteDesc);
                    Transmit(myRemoteKey.Value);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }

        public static void Transmit(string PortName, string KeyName, string RemoteDesc)
        {
            IRToy myToy = GetInstance(PortName);
            if (myToy != null)
            {
                myToy.Transmit(KeyName, RemoteDesc);
            }
        }

        public void SetRecordMode(bool status)
        {
            RecordMode = status;
        }

        public decimal GetLastKeyFQ()
        {
            decimal Result = 0;
            try
            {
                byte[] Timer = new byte[] { ConfigManager.Timer };
                IRToySerialPort.ReadExisting();
                IRToySerialPort.Write(Timer, 0, Timer.Length);
                while (IRToySerialPort.BytesToRead == 0)
                {
                }
                byte[] TimerResult = new byte[IRToySerialPort.BytesToRead];
                IRToySerialPort.Read(TimerResult, 0, TimerResult.Length);
                List<short> myTempList = new List<short>();
                for (int i = 0; i < 6; i += 2)
                {
                    byte[] myTempArray = new byte[2];
                    myTempArray[0] = TimerResult[i];
                    myTempArray[1] = TimerResult[i + 1];

                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(myTempArray);
                    }
                    myTempList.Add(BitConverter.ToInt16(myTempArray, 0));
                }

                decimal TimerTime = (myTempList[1] - myTempList[0]);//((myTempList[1] - myTempList[0]) + (myTempList[2] - myTempList[1])) / 2;

                ///FIX THE TIMER
                TimerTime += 16;
                ///

                decimal KeyFrequency = 1m / ((1m / 12000000m) * TimerTime);

                if (TimerResult[6] > 5)
                {
                    Result = Math.Round(KeyFrequency,2);
                }
                else
                {
                    throw new Exception("Too far or too close from device!");
                }
            }
            catch (Exception ex)
            {
                Debug.Print("FQ: " + ex.Message);
            }
            return Result;
        }

        public void CloseDevice()
        {
            keepAlive = false;
            try
            {
                if (this.IRToySerialPort != null)
                {
                    byte[] Reset = new byte[] { ConfigManager.Reset };
                    this.IRToySerialPort.Write(Reset, 0, Reset.Length);
                    this.IRToySerialPort.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //try
            //{
            //    if (this.IRToyRS != null)
            //    {
            //        this.IRToyRS.Abort();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.Print(ex.Message);
            //}
            //try
            //{
            //    if (this.IRToyTX != null)
            //    {
            //        this.IRToyTX.Abort();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.Print(ex.Message);
            //}
        }

        #endregion Public

        #region static

        public static IRToyList IRToyList;

        public static IRToy GetInstance(string port_name)
        {
            IRToy result = null;
            foreach (IRToy myToy in IRToyList)
            {
                if (myToy.IRToySerialPort.PortName == port_name)
                {
                    result = myToy;
                    break;
                }
            }
            return result;
        }

        #endregion static

        #region Private Methods
        private bool RecordMode = false;

        private void HandleTransmit()
        {
            byte[] ExitTransferMode = ConfigManager.TransmitEnd;
            DateTime handle_execute = DateTime.Now;
            bool wait_one = false;
            while (keepAlive)
            {
                try
                {
                    //if (TransmitBuffer.Count == 0)
                    //{
                    //    Thread.Sleep(10);
                    //    continue;
                    //}
                    //else
                    //{
                        int try_count = 0;
                        while (TransmitBuffer.Count != 0)
                        {
                            //if (!wait_one)
                            //{
                            //    IRToyRS.Abort();
                            //    wait_one = true;
                            //    //Thread.Sleep(10);
                            //}
                            //Thread.Sleep(20);
                            //byte PR2Code = 170;// 42;// 74;// 242;// myRemoteKey.PR2Code;
                            //byte[] SetTransferFQ = new byte[] { ConfigManager.SetTransmitFQ, PR2Code, ConfigManager.DontCareByte };
                            //IRToySerialPort.Write(SetTransferFQ, 0, SetTransferFQ.Length);

                            byte[] TransferMode = new byte[] { ConfigManager.TransmitStart };
                            IRToySerialPort.Write(TransferMode, 0, TransferMode.Length);

                            byte[] key_to_send = TransmitBuffer[0];
                            TransmitBuffer.Remove(key_to_send);

                            int count_sent = 0;
                            while (count_sent < key_to_send.Length)
                            {
                                if (IRToySerialPort.BytesToRead == 0)
                                {
                                    try_count += 1;
                                    if (try_count >= 50000)
                                    {
                                        TransmitBuffer = new List<byte[]>();
                                        break;
                                    }
                                }
                                else
                                {
                                    byte[] handshake_response = new byte[IRToySerialPort.BytesToRead];
                                    IRToySerialPort.Read(handshake_response, 0, handshake_response.Length);
                                    int requested_lenght = handshake_response[0];

                                    int bytes_left_in_key = key_to_send.Length - count_sent;
                                    if (bytes_left_in_key <= 0)
                                    {
                                        break;
                                    }
                                    else if (bytes_left_in_key < requested_lenght)
                                    {
                                        requested_lenght = bytes_left_in_key;
                                    }
                                    IRToySerialPort.Write(key_to_send, count_sent, requested_lenght);
                                    count_sent += requested_lenght;
                                }
                            }
                            IRToySerialPort.Write(ExitTransferMode, 0, ExitTransferMode.Length);
                            while (IRToySerialPort.BytesToRead == 0)
                            { }
                            IRToySerialPort.ReadExisting();
                            handle_execute = DateTime.Now;
                        }
                        //if (TransmitBuffer.Count == 0 && (DateTime.Now - handle_execute).TotalMilliseconds >= 300)
                        //{
                        //        IRToyRS = new Thread(() => Initialize());
                        //        IRToyRS.Start();
                        //        wait_one = false;
                        //}
                    //}
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                Thread.Sleep(10);
            }
        }


        private void StartDeviceThread()
        {
            IRToyRS = new Thread(() => Initialize());
            IRToyRS.Start();
            IRToyTX = new Thread(() => HandleTransmit());
            IRToyTX.Start();
        }

        private void Initialize()
        {
            IRToySerialPort.ReadExisting();
            List<short> myReceiveList = new List<short>();
            while (keepAlive)
            {
                try
                {
                    myReceiveList = new List<short>();
                    int ReadCount = IRToySerialPort.BytesToRead;
                    bool continiue = false;
                    if (ReadCount > 0)
                    {
                        continiue = true;
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                    
                    while (continiue)
                    {
                        try
                        {
                            ReadCount = IRToySerialPort.BytesToRead;
                            if (ReadCount == 0)
                            {
                                ReadCount = IRToySerialPort.BytesToRead;
                                if (ReadCount == 0)
                                {
                                    System.Threading.Thread.Sleep(30);
                                    ReadCount = IRToySerialPort.BytesToRead;
                                }
                                if (ReadCount == 0)
                                {
                                    ///"Nothing to do!"
                                    throw new Exception();
                                }
                            }

                            byte[] ReadArray = new byte[ReadCount];
                            
                            IRToySerialPort.Read(ReadArray, 0, ReadCount);


                            string bbbb = string.Empty;
                            foreach (byte bb in ReadArray)
                            {
                                bbbb += bb.ToString() + ", ";
                            }
                            Debug.Print(bbbb);
                            for (int b = 0; b < ReadCount; b += 2)
                            {
                                byte[] myPulseArray = new byte[2];
                                System.Buffer.BlockCopy(ReadArray, b, myPulseArray, 0, myPulseArray.Length);
                                if (BitConverter.IsLittleEndian)
                                {
                                    Array.Reverse(myPulseArray);
                                }
                                short i = BitConverter.ToInt16(myPulseArray, 0);
                                if (i > -1 && i < 1000)
                                {
                                    myReceiveList.Add(i);
                                }
                                else
                                {
                                    ///"Long space or end"
                                    throw new Exception();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message != new Exception().Message)
                            {
                                Debug.Print(ex.Message);
                            }
                            continiue = false;
                        }
                    }

                    if (myReceiveList.Count > 0)
                    {
                        try
                        {
                            //IRToy.GetInstance("COM6").Transmit(myReceiveList.ToArray());
                            KeyReceived(myReceiveList.ToArray());
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message != new Exception().Message)
                            {
                                Debug.Print(ex.Message);
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private void KeyReceived(short[] key_value)
        {
            DBRemoteKey myRemoteKey;
            bool DBMatchFound = false;
            if (!RecordMode && DBManager.myRemotes !=null)
            {
                myRemoteKey = DBManager.GetDBRemoteKeyByValue(key_value);
                if (myRemoteKey == null)
                {
                    myRemoteKey = new DBRemoteKey();
                    myRemoteKey.KeyDesc = key_value.Length.ToString();
                    myRemoteKey.RemoteDesc = "Not in DB";
                    myRemoteKey.Value = key_value;
                }
                else
                {
                    DBMatchFound = true;
                }
            }
            else
            {
                myRemoteKey = new DBRemoteKey();
                myRemoteKey.KeyDesc = key_value.Length.ToString();
                myRemoteKey.RemoteDesc = "Not in DB";
                myRemoteKey.Value = key_value;
            }
            //IRToy.Transmit("COM8", myRemoteKey.KeyDesc, myRemoteKey.RemoteDesc);
            //Thread myThread = new Thread(() => CallEventHandler(myRemoteKey, DBMatchFound));
            //myThread.Start();
            CallEventHandler(myRemoteKey, DBMatchFound);
            //Thread th = new Thread(new ParameterizedThreadStart(CallEventHandler));
            //th.Start(myRemoteKey);

            if (!RecordMode && ConfigManager.PlaySound)
            {
                //if (ConfigManager.PlaySound)
                //{
                    if (!DBMatchFound)
                    {
                        SoundManager.Null.Play();
                    }
                    else if (myRemoteKey.KeyDesc == "RepeatKey")
                    {
                        SoundManager.Repeat.Play();
                    }
                    else
                    {
                        SoundManager.Recognized.Play();
                    }
                //}
            }
        }

        //private void CallEventHandler(object o)
        private void CallEventHandler(DBRemoteKey Key, bool DBMatchFound)
        {
            if (OnKeyReceived != null)
            {
                KeyReceivedEventArgs args = new KeyReceivedEventArgs(Key, IRToySerialPort.PortName, DBMatchFound);
                OnKeyReceived(this, args);
            }
        }

        private void ResetDevice()
        {
            System.Threading.Thread.Sleep(3);
            byte[] Reset = new byte[] { ConfigManager.Reset };
            for (int i = 0; i < 5; i++)
            {
                IRToySerialPort.Write(Reset, 0, Reset.Length);
                System.Threading.Thread.Sleep(5);
            }
        }

        private bool SetSamplingMode()
        {
            Thread.Sleep(3);
            bool Result = false;
            byte[] SamplingMode = new byte[] { ConfigManager.SamplingMode };
            IRToySerialPort.Write(SamplingMode, 0, SamplingMode.Length);
            Thread.Sleep(5);
            if (IRToySerialPort.BytesToRead > 0)
            {
                byte[] Respnse = new byte[IRToySerialPort.BytesToRead];
                IRToySerialPort.Read(Respnse, 0, Respnse.Length);
                if (ConfigManager.SamplingModeProtocol == Encoding.UTF8.GetString(Respnse))
                {
                    Result = true;
                }
            }
            return Result;
        }

        private void SetHandShake()
        {
            IRToySerialPort.Write(ConfigManager.HandShake, 0, ConfigManager.HandShake.Length);
            Thread.Sleep(1);
        }

        #endregion Private Methods
    }
    public class KeyReceivedEventArgs : EventArgs
    {
        public DBRemoteKey Key { get; private set; }
        public string ComPortName { get; private set; }
        public bool DBMatchFound { get; private set; }

        public KeyReceivedEventArgs(DBRemoteKey key, string comportname, bool dbmatchfound)
        {
            Key = key;
            ComPortName = comportname;
            DBMatchFound = dbmatchfound;
        }
    }
}
