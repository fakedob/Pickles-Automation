using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using USBIRToyv2;
using Interceptor;
using Microsoft.Win32;
using Common;
using System.Runtime.InteropServices;
using System.Diagnostics;
using SoundClips;
using CoreAudioApi;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.GPU;


namespace WMC_Manager
{
    public partial class Form1 : Form
    {
        static UnlockSequenceList myUnlockSequenceList = new UnlockSequenceList();
        int request_quit = 0;
        static ListBox myListBox;

        bool not_avr = true;
        DateTime last_repeat;
        DateTime last_avr;

        DateTime last_sensor_command_received;
        string last_receiver_name = "";
        static DBRemoteKey LastReceivedKey;
        
        static DateTime lastUserInteract = DateTime.Now;
        static DateTime last_command_time;
        bool first_repeat;

        #region FormHandle
        public Form1()
        {
            InitializeComponent();
            myListBox = lsbEvents;
            PicklesAutomation.ConfigManager.KeepThreadsAlive = true;

            PicklesAutomation.ConfigManager.WindowHandle = this.Handle;
            PicklesAutomation.ConfigManager.IsSystemLocked = false;
            PicklesAutomation.ConfigManager.IsDisplayOn = true;
            PicklesAutomation.ConfigManager.HIDInput = new Input();
            PicklesAutomation.ConfigManager.HIDInput.Load();

            last_sensor_command_received =
            last_command_time =
            last_repeat =
            last_avr = DateTime.Now;

            try
            {
                EventManager.Initialize();
                SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);


                InitializeIRDB();
                InitializeIRToys();
                InitializeAudio();

                InitializeCursor();
                InitializeMouse();
                InitializeKeyboard();
                InitializeTaskbar();

                InitializeUserSleepTimer();
                InitializeFocus();

                InitializeOSD();

                UnlockSequenceInitialize();
                PicklesAutomation.ConfigManager.IsApplicationLocked = true;

                autorunToolStripMenuItem.Checked = ApplicationManager.WinStart;

                WindowsSounds.WindowsNotifyMessaging.Play();


                if (!PicklesAutomation.ConfigManager.IsDev)
                {
                    if (!ApplicationManager.IsProcessRunning(PicklesAutomation.ConfigManager.CoreApp))
                    {
                        Process myCoreApp = new Process();
                        myCoreApp.StartInfo.FileName = PicklesAutomation.ConfigManager.CoreAppPath;
                        myCoreApp.StartInfo.Arguments = "/mediamode";
                        myCoreApp.Start();
                    }
                }
                InitializeFaceRecognition();
            }
            catch (Exception ex)
            {
                AppendTextList(ex.Message);
                if (EventManager.IsInitialized)
                {
                    EventManager.LogEvent(EventType.Exception, ex.Message, "OnLoad");
                }
                else
                {
                    MessageBox.Show(ex.Message, "Pickles OnLoad Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                WindowsSounds.WindowsPopupBlocked.Play();
            }
            if (!AllowFormVisibility)
            {
                WindowState = FormWindowState.Minimized;
                AllowFormVisibility = true;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PicklesAutomation.ConfigManager.KeepThreadsAlive)
            {
                Exit();
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Exit();
        }
        void Exit()
        {
            try
            {
                PicklesAutomation.ConfigManager.KeepThreadsAlive = false;
                DisposeIRToys();
                TaskbarManager.Taskbar.Visible = true;
                CursorManager.CursorVisible();
                notifyIcon1.Visible = false;
                MouseManager.Disconnect();
                KeyboardManager.Disconnect();
                PicklesAutomation.ConfigManager.HIDInput.Unload();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                EventManager.LogEvent(EventType.Exception, ex.Message, "Exit");
            }
            Close();
        }

        delegate void textAppendDelegate(Control ctrl, string text);
        static void HandleTextControl(Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(new textAppendDelegate(HandleTextControl), ctrl, text);
            }
            else
            {
                ((ListBox)ctrl).Items.Add(text);
            }
        }
        static void AppendTextList(string text)
        {
            HandleTextControl(myListBox, text);
        }

        #endregion

        #region UISetup

        static bool AllowFormVisibility = PicklesAutomation.ConfigManager.IsDev;
        protected override void SetVisibleCore(bool value)
        {
            if (!AllowFormVisibility)
            {
                value = false;
                if (!this.IsHandleCreated)
                {
                    CreateHandle();
                }
            }
            base.SetVisibleCore(value);
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            }
            else if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Minimized;
            }
        }
        #endregion

        #region Focus
        static void InitializeFocus()
        {
            FocusManager.Focus.Initialize();
            FocusManager.Focus.OnApplicationFocusChange += FocusChange;
        }
        static void FocusChange(string appName)
        {
            TaskbarManager.Taskbar.Visible = TaskbarManager.Taskbar.Visible;
        }

        #endregion

        #region Taskbar
        static void InitializeTaskbar()
        {
            TaskbarManager.Taskbar.Hide();
        }
        private void taskbarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TaskbarManager.Taskbar.Visible = !TaskbarManager.Taskbar.Visible;
            taskbarToolStripMenuItem1.Checked = TaskbarManager.Taskbar.Visible;
        }
        #endregion

        #region Cursor
        static void InitializeCursor()
        {
            if (!PicklesAutomation.ConfigManager.IsDev)
            {
                new Thread(() =>
                {
                    //Thread.CurrentThread.IsBackground = true;
                    while (PicklesAutomation.ConfigManager.KeepThreadsAlive)
                    {
                        if (PicklesAutomation.ConfigManager.IsApplicationLocked)
                        {
                            bool UserIdle = (DateTime.Now - lastUserInteract).TotalSeconds >= 3;
                            if (PicklesAutomation.ConfigManager.IsCursorVisible && UserIdle)
                            {
                                CursorManager.CursorVisible(false);
                            }
                            else if (!PicklesAutomation.ConfigManager.IsCursorVisible && !UserIdle)
                            {
                                CursorManager.CursorVisible();
                            }
                        }
                        Thread.Sleep(100);
                    }
                }).Start();
            }
        }
        #endregion

        #region FaceRecognition
        static Capture myCapture;
        static DateTime lastFaceRecognisedTime = DateTime.Now;
        static DateTime firstFaceRecognisedTime = DateTime.Now;
        static DateTime firstChanseFaceFound = DateTime.Now;
        static void InitializeFaceRecognition()
        {
            new Thread(() =>
            {
                //Thread.CurrentThread.IsBackground = true;
                while (PicklesAutomation.ConfigManager.KeepThreadsAlive)
                {
                    if (ScreenSaverManager.GetScreenSaverRunning() && (DateTime.Now - lastFaceRecognisedTime).TotalSeconds >= 10)
                    {
                        if (myCapture == null)
                        {
                            myCapture = new Capture();
                        }

                        firstFaceRecognisedTime = DateTime.Now;
                        Image<Bgr, Byte> image = myCapture.QueryFrame(); 
                        if (image != null)
                        {
                            long detectionTime;
                            List<Rectangle> faces = new List<Rectangle>();
                            List<Rectangle> eyes = new List<Rectangle>();
                            DetectFaceManager.Detect(image, "haarcascade_frontalface_default2.xml", "haarcascade_eye.xml", faces, eyes, out detectionTime);
                            if (faces.Count > 0 && eyes.Count > 0)
                            {
                                PowerOn();
                                lastFaceRecognisedTime = DateTime.Now;
                            }
                            else if (faces.Count > 0)
                            {
                                if ((DateTime.Now - firstChanseFaceFound).TotalMilliseconds >= 1000)
                                {
                                    firstChanseFaceFound = DateTime.Now;
                                }
                                else
                                {
                                    PowerOn();
                                    lastFaceRecognisedTime = DateTime.Now;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (myCapture != null)
                        {
                            myCapture = null;
                        }
                    }
                    Thread.Sleep(100);
                }
            }).Start();
        }
        #endregion

        #region Mouse
        public static void InitializeMouse()
        {
            MouseManager.Connect();

            MouseManager.OnMouseEvent += mouseEvent;
        }
        static void mouseEvent(MouseManager.MouseMessages wParam, int x, int y)
        {
            PicklesAutomation.ConfigManager.IsDisplayOn = true;
            lastUserInteract = DateTime.Now;
        }

        #endregion

        #region Keyboard
        public static void InitializeKeyboard()
        {
            KeyboardManager.Connect();
            KeyboardManager.OnKeyDown += keyDown;
        }
        static void keyDown (System.Windows.Forms.Keys myKey)
        {
            for (int i = 0; i < myUnlockSequenceList.Count;i++ )
            {
                string keyName = myKey.ToString();
                if (myUnlockSequenceList[i].Matched)
                {
                    continue;
                }
                if (myUnlockSequenceList[i].Char.Equals(keyName[keyName.Length-1]))
                {
                    myUnlockSequenceList[i].Matched = true;
                    if (i >= myUnlockSequenceList.Count -1)
                    {
                        for (int ii = 0; ii < myUnlockSequenceList.Count; ii++)
                        {
                            myUnlockSequenceList[ii].Matched = false;
                        }
                        if (PicklesAutomation.ConfigManager.IsApplicationLocked)
                        {
                            TaskbarManager.Taskbar.Visible = true;
                            PicklesAutomation.ConfigManager.IsApplicationLocked = false;
                        }
                        else
                        {
                            TaskbarManager.Taskbar.Visible = false;
                            PicklesAutomation.ConfigManager.IsApplicationLocked = true;
                        }
                    }
                    break;
                }
                else
                {
                    for (int ii = 0; ii < myUnlockSequenceList.Count; ii++)
                    {
                        myUnlockSequenceList[ii].Matched = false;
                    }
                    break;
                }
            }

            PicklesAutomation.ConfigManager.IsDisplayOn = true;
            //lastUserInteract = DateTime.Now;
        }
        #endregion

        #region UnlockSequence

        static void UnlockSequenceInitialize()
        {
            for(int i=0; i < PicklesAutomation.ConfigManager.UnlockSequenceChars.Length; i++ )
            {
                myUnlockSequenceList.Add(new UnlockSequence() { Matched = false, Char = PicklesAutomation.ConfigManager.UnlockSequenceChars[i] });
            }
        }
        #endregion

        #region Audio
        static MMDevice device;
        static DBRemoteKey VolumeUpIRKey;
        static DBRemoteKey VolumeDownIRKey;
        static DBRemoteKey MuteIRKey;
        static int CurrentVolumeLevel;
        static DBRemoteKeyList bufferSync;
        static void InitializeAudio()
        {
            VolumeUpIRKey = DBManager.GetDBRemoteKeyByDesc("Volume Up");
            VolumeDownIRKey = DBManager.GetDBRemoteKeyByDesc("Volume Down");
            MuteIRKey = DBManager.GetDBRemoteKeyByDesc("Mute");
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            CurrentVolumeLevel = (int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);

            //tbMaster.Value = (int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            device.AudioEndpointVolume.OnVolumeNotification += new AudioEndpointVolumeNotificationDelegate(AudioEndpointVolume_OnVolumeNotification);

            bufferSync = new DBRemoteKeyList();
            new Thread(() =>
            {
                //Thread.CurrentThread.IsBackground = true;
                while (PicklesAutomation.ConfigManager.KeepThreadsAlive)
                {
                    if (bufferSync.Count > 0)
                    {
                        SendIR(bufferSync[0]);
                        bufferSync.RemoveAt(0);
                    }
                    Thread.Sleep(150);
                }
            }).Start();

            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    while (PicklesAutomation.ConfigManager.KeepThreadsAlive)
            //    {
            //        AppendTextList(bufferSync.Count.ToString());
            //        Thread.Sleep(3000);
            //    }
            //}).Start();

            //for (int i = 0; i < 100; i++)
            //{
            //    SetAudioLevel(i);
            //    Thread.Sleep(300);
            //}
        }

        static void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            int UpdatedVolumeLevel = (int)(data.MasterVolume * 100);
            if (data.Muted)
            {
                ShowOSD("Mute On");
            }
            else
            {
                ShowOSD(UpdatedVolumeLevel.ToString());
            }
            if (UpdatedVolumeLevel != CurrentVolumeLevel && (DateTime.Now - last_command_time).TotalMilliseconds >= 500)
            {
                DBRemoteKey myVolumeKey;
                int difference;
                if (UpdatedVolumeLevel > CurrentVolumeLevel)
                {
                    myVolumeKey = VolumeUpIRKey;
                    difference = UpdatedVolumeLevel - CurrentVolumeLevel;
                }
                else
                {
                    myVolumeKey = VolumeDownIRKey;
                    difference = CurrentVolumeLevel - UpdatedVolumeLevel;
                }
                for (int i = 0; i < difference; i++)
                {
                    bufferSync.Add(myVolumeKey);
                }
                CurrentVolumeLevel = UpdatedVolumeLevel;
            }
        }

        static void SetAudioLevel(int level)
        {
            if (level > 100)
            {
                level = 100;
            }
            else if (level < 0)
            {
                level = 0;
            }
            CurrentVolumeLevel = level;
            device.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)level / 100.0f);
        }

        static void ToggleMute()
        {
            //if (!device.AudioEndpointVolume.Mute)
            //{
            //    SetAudioLevel(0);
            //}
            //else
            //{
            //    SetAudioLevel(CurrentVolumeLevel);
            //}
            device.AudioEndpointVolume.Mute = !device.AudioEndpointVolume.Mute;
            //SendIR(MuteIRKey);
        }
        //detect headphones key and change default device :)
        #endregion

        #region UserSleepTimer

        static int UserSleepTimeOut = 0;
        static int UserSleepTimeOutBy = 30;
        static int UserSleepTimeOutTimesCount = 4;
        static DateTime UserSleepTimeOfCreation;
        static DateTime UserSleepTimeOfExecution;

        static void InitializeUserSleepTimer()
        {
            new Thread(() =>
            {
                //Thread.CurrentThread.IsBackground = true;
                while (PicklesAutomation.ConfigManager.KeepThreadsAlive)
                {
                    if (UserSleepTimeOut != 0 && DateTime.Now >= UserSleepTimeOfExecution)
                    {
                        SoundClips.WindowsSounds.WindowsProximityNotification.Play();
                        SendMessageToMCE("Auto Sleep", "The system will Auto Sleep in 5 seconds.", 5);
                        int CurrentUserTimeOut = UserSleepTimeOut;
                        Thread.Sleep(5000);
                        if (CurrentUserTimeOut == UserSleepTimeOut)
                        {
                            UserSleepTimeOut = 0;
                            SleepWorkStation();
                        }
                    }
                    Thread.Sleep(1000);
                }
            }).Start();
        }

        static void UserSleepChange()
        {
            if ((DateTime.Now - last_command_time).TotalMilliseconds >= 3000)
            {
                if (UserSleepTimeOut == 0)
                {
                    SendMessageToMCE("Auto Sleep", "The Auto Sleep is off.");
                }
                else
                {
                    int minutesLeft = (int)(UserSleepTimeOfExecution - DateTime.Now).TotalMinutes;
                    SendMessageToMCE("Auto Sleep", "The system will turn off in " + minutesLeft + " minutes. (" + UserSleepTimeOfExecution.ToShortTimeString() + ")");
                }
                last_command_time = DateTime.Now;
                return;
            }
            if (UserSleepTimeOut == 0)
            {
                UserSleepTimeOfCreation = DateTime.Now;
            }
            UserSleepTimeOut += UserSleepTimeOutBy;
            if (UserSleepTimeOut > UserSleepTimeOutBy * UserSleepTimeOutTimesCount)
            {
                UserSleepTimeOut = 0;
                SendMessageToMCE("Auto Sleep", "The Auto Sleep has been turned off.");
                SoundClips.WindowsSounds.WindowsProximityNotification.Play();
            }
            else
            {
                SoundClips.WindowsSounds.WindowsPrintcomplete.Play();
                SendMessageToMCE("Auto Sleep", "The system will sleep in " + UserSleepTimeOut + " minutes. (" + DateTime.Now.AddMinutes(UserSleepTimeOut).ToShortTimeString() + ")");
                UserSleepTimeOfExecution = UserSleepTimeOfCreation.AddMinutes(UserSleepTimeOut);
            }
            last_command_time = DateTime.Now;
        }

        #endregion

        #region MCEInform
        static DateTime lastPreviewedMessage = DateTime.Now;
        static int lastPreviewedMessageTimeOut = 0;
        static void SendMessageToMCE(string title, string message, int timeoutSeconds = 5)
        {
            if (!ApplicationManager.ActiveProcessFileName.Equals(PicklesAutomation.ConfigManager.CoreAppName, StringComparison.OrdinalIgnoreCase))
            {
                osd.OSDText = message;
            }
            else
            {
                if ((DateTime.Now - lastPreviewedMessage).TotalSeconds <= lastPreviewedMessageTimeOut)
                {
                    SendKeys.SendWait("{ENTER}");
                }
                lastPreviewedMessageTimeOut = timeoutSeconds;
                lastPreviewedMessage = DateTime.Now;
                Process MCEPopup = new Process();
                MCEPopup.StartInfo.FileName = "MCPopupSend.exe";
                MCEPopup.StartInfo.Arguments = "192.168.1.255 " + timeoutSeconds + " \"" + title + "\" \"" + message + "\"";
                MCEPopup.StartInfo.UseShellExecute = false;
                MCEPopup.StartInfo.CreateNoWindow = true;
                MCEPopup.Start();
            }
        }
        #endregion

        #region UserSession
        static DateTime sessionLockTime = DateTime.Now;
        static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            bool doubleExecutetionPrevent = (DateTime.Now - sessionLockTime).TotalMilliseconds >= 1000;
            sessionLockTime = DateTime.Now;
            if (doubleExecutetionPrevent && (e.Reason == SessionSwitchReason.SessionLogon || e.Reason == SessionSwitchReason.SessionUnlock))
            {
                PicklesAutomation.ConfigManager.IsSystemLocked = false;
                ScreenSaverManager.SetScreenSaverActive(0);
                WindowsSounds.WindowsNotifyMessaging.Play();
            }
            else if (doubleExecutetionPrevent)
            {
                PicklesAutomation.ConfigManager.IsSystemLocked = true;
                
                ScreenSaverManager.SetScreenSaverTimeout(10);
                ScreenSaverManager.SetScreenSaverActive(1);
                lastFaceRecognisedTime = DateTime.Now;
                WindowsSounds.WindowsNotifySystemGeneric.Play();
                //DisplayHandle();
            }
        }
        //static bool displayIsBussy = false;
        //static void DisplayHandle()
        //{
        //    new Thread(() =>
        //    {
        //        //Thread.CurrentThread.IsBackground = true;
        //        int waitTime = 20000;
        //        while (PicklesAutomation.ConfigManager.IsSystemLocked)
        //        {
        //            if (!displayIsBussy && (DateTime.Now - sessionLockTime).TotalMilliseconds >= waitTime)
        //            {
        //                displayIsBussy = true;
        //                //DisplayManager.TurnOff();
        //                WindowsSounds.WindowsBalloon.Play();
        //            //}
        //            //else if ((DateTime.Now - sessionLockTime).TotalMilliseconds >= waitTime * 2)
        //            //{
        //                PowerOffTV();
        //                displayIsBussy = false;
        //                break;
        //            }
        //            Thread.Sleep(1000);
        //        }
        //    }).Start();
        //}

        static void SleepWorkStation()
        {
            if (!ApplicationManager.ActiveProcessFileName.Equals(System.IO.Path.GetFileName(Application.ExecutablePath), StringComparison.OrdinalIgnoreCase))
            {
                if (!ApplicationManager.ActiveProcessFileName.Equals(PicklesAutomation.ConfigManager.CoreAppName, StringComparison.OrdinalIgnoreCase))
                {
                    SendKeys.SendWait("%{F4}");
                }
                else
                {
                    SendKeys.SendWait("^+(s)");
                }
            }
            PicklesAutomation.ConfigManager.HIDInput.MoveMouseTo(Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 4);
            last_command_time = DateTime.Now;
            DisplayManager.LockWorkStation();
        }
        #endregion

        #region IRIO
        void InitializeIRDB()
        {
            string fName = @"Default.irdb";
            DBManager.InitializeDB(fName);
        }

        void InitializeIRToys()
        {
            IRToy.IRToyList = new IRToyList();
            foreach (string port_name in SerialPortManager.ComPortNames("04D8", "FD08"))
            {
                try
                {
                    IRToy myIRToy = new IRToy(port_name);
                    IRToy.IRToyList.Add(myIRToy);
                    EventManager.LogEvent(EventType.Message, port_name + " : Device connected.", "InitializeIRToys");
                }
                catch (Exception ex)
                {
                    AppendTextList(ex.Message);
                    EventManager.LogEvent(EventType.Exception, port_name + " : " + ex.Message, "InitializeIRToys");
                }
            }
            AttachEventHandler();
        }
        void DisposeIRToys()
        {
            if (IRToy.IRToyList != null && IRToy.IRToyList.Count > 0)
            {
                for (int i = 0; i < IRToy.IRToyList.Count; i++)
                {
                    string desc = "Unknown";
                    try
                    {
                        
                        DettachEventHandler(IRToy.IRToyList[i]);
                        desc = IRToy.IRToyList[i].IRToyDesc;
                        IRToy.IRToyList[i].CloseDevice();
                        //IRToy.IRToyList[i] = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(desc + ": " + ex.Message);
                    }
                }
            }
        }

        static void SendIR(DBRemoteKey myRemoteKey, string device = "COM5")
        {
            //if (FilterKeyPresses())
            //{
            IRToy.Transmit(device, myRemoteKey.KeyDesc, myRemoteKey.RemoteDesc);
                //last_command_time = DateTime.Now;
            //}
        }

        static bool FilterKeyPresses(int delay = 10)
        {
            TimeSpan span = DateTime.Now - last_command_time;
            if ((int)span.TotalMilliseconds > delay)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region IREventHandle

        void AttachEventHandler(IRToy myIRToy = null)
        {
            if (myIRToy != null)
            {
                myIRToy.OnKeyReceived += new IRToy.KeyReceivedHandler(KeyReceived);
            }
            else
            {
                foreach (IRToy myTempIRToy in IRToy.IRToyList)
                {
                    myTempIRToy.OnKeyReceived += new IRToy.KeyReceivedHandler(KeyReceived);
                }
            }
        }
        void DettachEventHandler(IRToy myIRToy = null)
        {
            if (myIRToy != null)
            {
                myIRToy.OnKeyReceived -= new IRToy.KeyReceivedHandler(KeyReceived);
            }
            else
            {
                foreach (IRToy myTempIRToy in IRToy.IRToyList)
                {
                    myTempIRToy.OnKeyReceived -= new IRToy.KeyReceivedHandler(KeyReceived);
                }
            }
        }

        void KeyReceived(object sender, KeyReceivedEventArgs e)
        {
            DBRemoteKey myRemoteKey = e.Key;
            if (e.DBMatchFound)
            {
                try
                {
                    bool allow = true;
                    bool is_repeat = false;
                    bool is_key_number = false;

                    if (PicklesAutomation.ConfigManager.IsSystemLocked)
                    {
                        if (myRemoteKey.KeyDesc == "RepeatKey" && (DateTime.Now - last_command_time).TotalMilliseconds <=1000)
                        {
                            return;
                        }
                        
                        PicklesAutomation.ConfigManager.HIDInput.MoveMouseTo(Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 4);

                        PicklesAutomation.ConfigManager.HIDInput.SendLeftClick();
                        //if (!PicklesAutomation.ConfigManager.IsDisplayOn)
                        //{
                        //    PowerOn();
                        //    //DisplayManager.TurnOn();
                        //    sessionLockTime = DateTime.Now;
                        //    //DisplayHandle();
                        //}
                        //else
                        //{
                        //    PicklesAutomation.ConfigManager.HIDInput.SendLeftClick();
                        //}

                        last_command_time = DateTime.Now;
                        return;
                    }
                    DateTime current_sensor_command = DateTime.Now;
                    TimeSpan last_sensor_command_span = current_sensor_command - last_sensor_command_received;
                    if ((int)last_sensor_command_span.TotalMilliseconds < 200 && e.ComPortName != last_receiver_name)
                    {
                        return;
                    }
                    else
                    {
                        last_receiver_name = e.ComPortName;
                    }
                    last_sensor_command_received = DateTime.Now;

                    if (myRemoteKey.KeyDesc == "TVPower")
                    {
                        SoundClips.WindowsSounds.WindowsMessageNudge.Play();
                    }
                    if (myRemoteKey.KeyDesc == "Z2On")
                    {
                        PowerOnTV();
                        return;
                    }
                    else if (myRemoteKey.KeyDesc == "Z2Off")
                    {
                        PowerOffTV();
                        return;
                    }
                    else if (myRemoteKey.KeyDesc == "Z2Source")
                    {
                        not_avr = !not_avr;
                        return;
                    }
                    else if (myRemoteKey.KeyDesc == "Z2OK")
                    {
                        InputChangeTV();
                        return;
                    }
                    else if (myRemoteKey.KeyDesc == "On")
                    {
                        PowerOn();
                        return;
                    }
                    else if (myRemoteKey.KeyDesc == "Off")
                    {
                        PowerOff();
                        return;
                    }
                    else if (myRemoteKey.KeyDesc == "Sleep")
                    {
                        UserSleepChange();
                        return;
                    }
                    else if (myRemoteKey.KeyDesc == "Mute")
                    {
                        ToggleMute();
                        last_command_time = DateTime.Now;
                        return;
                    }

                    if (not_avr)
                    {
                        string send_key = null;
                        if (myRemoteKey.KeyDesc == "0")
                        {
                            is_key_number = true;
                            allow = false;
                            send_key = "{0}";
                        }
                        else if (myRemoteKey.KeyDesc == "1")
                        {
                            is_key_number = true;
                            allow = false;
                            send_key = "{1}";
                        }
                        else if (myRemoteKey.KeyDesc == "2")
                        {
                            is_key_number = true;
                            allow = false;
                            send_key = "{2}";
                        }
                        else if (myRemoteKey.KeyDesc == "3")
                        {
                            is_key_number = true;
                            allow = false;
                            send_key = "{3}";
                        }
                        else if (myRemoteKey.KeyDesc == "4")
                        {
                            is_key_number = true;
                            allow = false;
                            send_key = "{4}";
                        }
                        else if (myRemoteKey.KeyDesc == "5")
                        {
                            is_key_number = true;
                            allow = false;
                            send_key = "{5}";
                        }
                        else if (myRemoteKey.KeyDesc == "6")
                        {
                            is_key_number = true;
                            allow = false;
                            send_key = "{6}";
                        }
                        else if (myRemoteKey.KeyDesc == "7")
                        {
                            is_key_number = true;
                            allow = false;
                            send_key = "{7}";
                        }
                        else if (myRemoteKey.KeyDesc == "8")
                        {
                            is_key_number = true;
                            allow = false;
                            send_key = "{8}";
                        }
                        else if (myRemoteKey.KeyDesc == "9")
                        {
                            is_key_number = true;
                            allow = false;
                            send_key = "{9}";
                        }

                        if (myRemoteKey.KeyDesc == "Revert" || myRemoteKey.KeyDesc == "RepeatKey")
                        {
                            if (myRemoteKey.KeyDesc == "Revert")
                            {
                                LastReceivedKey = e.Key;
                            }
                            else if (myRemoteKey.KeyDesc == "RepeatKey")
                            {
                                if (LastReceivedKey != null)
                                {
                                    if (LastReceivedKey.KeyDesc == "Revert")
                                    {
                                        return;
                                    }
                                }
                            }
                            if (myRemoteKey.KeyDesc != "RepeatKey")
                            {
                                DateTime current_repeat = DateTime.Now;
                                TimeSpan last_repeat_span = current_repeat - last_repeat;
                                if ((int)last_repeat_span.TotalMilliseconds < 1000)
                                {
                                    last_repeat = current_repeat;
                                    if (request_quit < 1)
                                    {
                                        request_quit += 1;
                                    }
                                    else
                                    {
                                        if (!ApplicationManager.ActiveProcessFileName.Equals(PicklesAutomation.ConfigManager.CoreAppName, StringComparison.OrdinalIgnoreCase))
                                        {
                                            send_key = "%{F4}";
                                            SendKeys.SendWait(send_key);
                                            Thread.Sleep(100);
                                            PicklesAutomation.ConfigManager.HIDInput.SendLeftClick();
                                            //brada
                                        }
                                        last_command_time = DateTime.Now;
                                        request_quit = 0;
                                    }
                                }
                                else
                                {
                                    last_repeat = DateTime.Now;
                                    request_quit = 1;
                                }
                                return;
                            }
                        }

                        if (myRemoteKey.KeyDesc == "RepeatKey" && !is_key_number)
                        {
                            DateTime current_repeat = DateTime.Now;
                            TimeSpan last_repeat_span = current_repeat - last_repeat;
                            if ((int)last_repeat_span.TotalMilliseconds < 800)
                            {
                                last_repeat = current_repeat;
                                myRemoteKey = LastReceivedKey;
                                is_repeat = true;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            last_repeat = DateTime.Now;
                            LastReceivedKey = myRemoteKey;
                            first_repeat = true;
                        }

                        if (myRemoteKey.KeyDesc == "OK")
                        {
                            allow = false;
                            send_key = "{ENTER}";
                        }
                        else if (myRemoteKey.KeyDesc == "Up")
                        {
                            allow = false;
                            send_key = "{UP}";
                        }
                        else if (myRemoteKey.KeyDesc == "Down")
                        {
                            allow = false;
                            send_key = "{DOWN}";
                        }
                        else if (myRemoteKey.KeyDesc == "Left")
                        {
                            allow = false;
                            send_key = "{LEFT}";
                        }
                        else if (myRemoteKey.KeyDesc == "Right")
                        {
                            allow = false;
                            send_key = "{RIGHT}";
                        }
                        else if (myRemoteKey.KeyDesc == "Menu")
                        {
                            allow = false;
                            send_key = "^(g)";
                        }
                        else if (myRemoteKey.KeyDesc == "Pause")
                        {
                            allow = false;
                            send_key = "^(p)";
                        }
                        else if (myRemoteKey.KeyDesc == "Play")
                        {
                            allow = false;
                            send_key = "^+(p)";
                        }
                        else if (myRemoteKey.KeyDesc == "Stop")
                        {
                            allow = false;
                            send_key = "^+(s)";
                        }
                        else if (myRemoteKey.KeyDesc == "Previous")
                        {
                            allow = false;
                            send_key = "^(b)";
                        }
                        else if (myRemoteKey.KeyDesc == "Next")
                        {
                            allow = false;
                            send_key = "^(f)";
                        }
                        else if (myRemoteKey.KeyDesc == "Backward")
                        {
                            allow = false;
                            send_key = "^+(b)";
                        }
                        else if (myRemoteKey.KeyDesc == "Forward")
                        {
                            allow = false;
                            send_key = "^+(f)";
                        }
                        else if (myRemoteKey.KeyDesc == "ChannelUp")
                        {
                            allow = false;
                            send_key = "{PGUP}";
                        }
                        else if (myRemoteKey.KeyDesc == "ChannelDown")
                        {
                            allow = false;
                            send_key = "{PGDN}";
                        }
                        else if (myRemoteKey.KeyDesc == "Info")
                        {
                            allow = false;
                            send_key = "^ESC%ENTER";
                        }
                        else if (myRemoteKey.KeyDesc == "Back")
                        {
                            allow = false;
                            send_key = "{BACKSPACE}";
                        }

                        else if (myRemoteKey.KeyDesc == "Back")
                        {
                            allow = false;
                            send_key = "{BACKSPACE}";
                        }

                        if (send_key != null)
                        {
                            if (!is_repeat)
                            {
                                if (is_key_number)
                                {
                                    SendKeys.SendWait(send_key);
                                    last_command_time = DateTime.Now;
                                }
                                else if (FilterKeyPresses(100))
                                {
                                    SendKeys.SendWait(send_key);
                                    last_command_time = DateTime.Now;
                                }
                            }
                            else
                            {
                                int repeat_time = 100;
                                if (first_repeat)
                                {
                                    repeat_time = 600;
                                }
                                if (FilterKeyPresses(repeat_time) && first_repeat)
                                {
                                    SendKeys.SendWait(send_key);
                                    last_command_time = DateTime.Now;
                                    first_repeat = false;
                                }
                                else if (!first_repeat)
                                {
                                    SendKeys.SendWait(send_key);
                                    last_command_time = DateTime.Now;
                                }
                            }
                        }
                    }
                    if (allow)
                    {
                        
                        if (FilterKeyPresses(100))
                        {
                            last_command_time = DateTime.Now;
                            SendIR(myRemoteKey);
                            if (myRemoteKey.KeyDesc == "Volume Up")
                            {
                                if (!device.AudioEndpointVolume.Mute || CurrentVolumeLevel == 0)
                                {
                                    SetAudioLevel(CurrentVolumeLevel + 1);
                                }
                                else
                                {
                                    ToggleMute();
                                }
                            }
                            else if (myRemoteKey.KeyDesc == "Volume Down")
                            {
                                if (!device.AudioEndpointVolume.Mute)
                                {
                                    SetAudioLevel(CurrentVolumeLevel - 1);
                                }
                                else
                                {
                                    ToggleMute();
                                }
                            }
                        }
                        //if (!is_repeat)
                        //{
                        //    SendIR(myRemoteKey);
                        //}
                        //else
                        //{
                        //    SendIR(e.Key);
                        //    //textApend(lsbEvents, e.Key.KeyDesc);

                        //    //SendIR(DBManager.GetDBRemoteKeyByDesc("RepeatKey"));
                        //}
                    }
                }
                catch (Exception ex)
                {
                    EventManager.LogEvent(EventType.Exception, ex.Message, "KeyReceived");
                }
            }
        }

        static void PowerOn()
        {
            //SendIR(newAVDeviceDB.GetIRPacket("New AVDevice", "On"));
            //ApplicationManager.SetFormFocus(this);
            //last_command_time = DateTime.Now;
            //if (ApplicationManager.is_screen_saver_running)
            //{
            //    //ApplicationManager.StopScreenSaver();
            //}
            PowerOnTV();
            //Thread.Sleep(8000);
            //InputChangeTV();
        }
        static void PowerOff()
        {

            SleepWorkStation();
            //SendIR(newAVDeviceDB.GetIRPacket("New AVDevice", "Off"));
            //SendIR(newAVDeviceDB.GetIRPacket("New AVDevice", "Tv Power"), true);
            //System.Threading.Thread.Sleep(2000);
            //ApplicationManager.StartScreenSaver();
        }

        static DateTime tvOffTime = DateTime.Now;
        static void PowerOnTV()
        {
            if ((DateTime.Now - tvOffTime).TotalSeconds > 1)
            {
                SoundClips.WindowsSounds.WindowsFeedDiscovered.Play();
                //DBRemoteKey myTVOnKey = DBManager.GetDBRemoteKeyByDesc("TVOn");
                //new Thread(() =>
                //{
                //    //Thread.CurrentThread.IsBackground = true;
                //    for (int i = 0; i < 10; i++)
                //    {
                //        SendIR(myTVOnKey, "COM4");
                //        Thread.Sleep(100);
                //    }
                //    Thread.Sleep(6000);
                //    InputChangeTV();
                //}).Start();
                
                PicklesAutomation.ConfigManager.HIDInput.SendLeftClick();
                
                last_command_time = DateTime.Now;
            }
        }
        static void PowerOffTV()
        {
            //DBRemoteKey myTVOnKey = DBManager.GetDBRemoteKeyByDesc("TVOn");
            //new Thread(() =>
            //{
            //    //Thread.CurrentThread.IsBackground = true;
            //    for (int i = 0; i < 2; i++)
            //    {
            //        SendIR(myTVOnKey, "COM4");
            //        Thread.Sleep(100);
            //    }
            //}).Start();
            tvOffTime = DateTime.Now;
            last_command_time = DateTime.Now;
        }

        static void InputChangeTV()
        {
            SendIR(DBManager.GetDBRemoteKeyByDesc("TVInput"),"COM4");
            last_command_time = DateTime.Now;
        }

        #endregion

        #region OSD

        static OSD osd;
        static void InitializeOSD()
        {
            osd = new OSD();
        }
        static void ShowOSD(string text, int timeout = 3000)
        {
            if (!ApplicationManager.ActiveProcessFileName.Equals(PicklesAutomation.ConfigManager.CoreAppName, StringComparison.OrdinalIgnoreCase))
            {
                osd.OSDText = text;
            }
            //lastOSDPreviewed = DateTime.Now;
            //lastOSDTimeout = timeout;
            //_osd.Show(_pnt, _alpha, _textColor, _textFont, timeout, _mode, _animateTime, text);
            //Screen.PrimaryScreen.WorkingArea.Height;
            
           
           // _osd.Hide();
        }
        #endregion

        #region Autorun
        private void autorunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationManager.WinStart = !ApplicationManager.WinStart;
            autorunToolStripMenuItem.Checked = ApplicationManager.WinStart;
        }
        #endregion

    }

    public class UnlockSequenceList : List<UnlockSequence> { };
    public class UnlockSequence
    {
        public bool Matched { get; set; }
        public char Char { get; set; }
    }
}
