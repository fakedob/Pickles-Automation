using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.IO;
using System.Media;

namespace SoundClips
{
    public static class WindowsSounds
    {
        public static SoundPlayer Alarm01
        {
            get
            {
                //System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\mywavfile.wav");
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Alarm01.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Alarm02
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Alarm02.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Alarm03
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Alarm03.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Alarm04
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Alarm04.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Alarm05
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Alarm05.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Alarm06
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Alarm06.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Alarm07
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Alarm07.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Alarm08
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Alarm08.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Alarm09
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Alarm09.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Alarm10
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Alarm10.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer chimes
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\chimes.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer chord
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\chord.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer ding
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\ding.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer notify
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\notify.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer recycle
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\recycle.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Ring01
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Ring01.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Ring02
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Ring02.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Ring03
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Ring03.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Ring04
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Ring04.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Ring05
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Ring05.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Ring06
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Ring06.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Ring07
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Ring07.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Ring08
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Ring08.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Ring09
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Ring09.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer Ring10
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Ring10.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer ringout
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\ringout.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer tada
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\tada.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsBackground
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Background.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsBalloon
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Balloon.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsBatteryCritical
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Battery Critical.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsBatteryLow
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Battery Low.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsCriticalStop
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Critical Stop.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsDefault
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Default.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsDing
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Ding.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsError
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Error.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsExclamation
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Exclamation.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsFeedDiscovered
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Feed Discovered.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsForeground
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Foreground.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsHardwareFail
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Hardware Fail.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsHardwareInsert
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Hardware Insert.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsHardwareRemove
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Hardware Remove.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsInformationBar
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Information Bar.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsLogoffSound
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Logoff Sound.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsLogon
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Logon.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsMenuCommand
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Menu Command.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsMessageNudge
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Message Nudge.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsMinimize
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Minimize.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsNavigationStart
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Navigation Start.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsNotifyCalendar
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Notify Calendar.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsNotifyEmail
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Notify Email.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsNotifyMessaging
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Notify Messaging.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsNotifySystemGeneric
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Notify System Generic.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsNotify
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Notify.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsPopupBlocked
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Pop-up Blocked.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsPrintcomplete
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Print complete.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsProximityConnection
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Proximity Connection.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsProximityNotification
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Proximity Notification.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsRecycle
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Recycle.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsRestore
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Restore.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsRingin
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Ringin.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsRingout
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Ringout.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsShutdown
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Shutdown.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsStartup
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Startup.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsUnlock
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows Unlock.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
        public static SoundPlayer WindowsUserAccountControl
        {
            get
            {
                SoundPlayer mySoundPlayer = new SoundPlayer();
                mySoundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"media\Windows User Account Control.wav";
                mySoundPlayer.Load();
                return mySoundPlayer;
            }
        }
    }
}