using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Common
{
    public class EventManager
    {
        static string fileName = "EventLog.txt";
        public static bool IsInitialized = false;
        static EventLogList myBuffer = new EventLogList();
        public static void Initialize()
        {
            new Thread(() =>
            {
                while (PicklesAutomation.ConfigManager.KeepThreadsAlive)
                {
                    if (myBuffer.Count > 0)
                    {
                        for (int i = 0; i < myBuffer.Count; i++)
                        {
                            LogEvent(myBuffer[i]);
                            myBuffer.RemoveAt(i);
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            }).Start();
            IsInitialized = true;
        }
        public static void LogEvent(EventType type, string message, string method = null)
        {
            if (method == null)
            {
                method = "Not suplied.";
            }
            myBuffer.Add(new EventLog()
            {
                Text = message,
                Time = DateTime.Now,
                Type = type,
                Delegate = method
            });
        }

        private static void LogEvent(EventLog myEvent)
        {
            using (StreamWriter sw = File.AppendText(fileName))
            {
                string eventMessage = string.Empty;
                switch (myEvent.Type)
                {
                    case EventType.Exception:
                        {
                            eventMessage += "!!! Exception on " + myEvent.Time + " ";
                            break;
                        }
                    case EventType.Message:
                        {
                            eventMessage += "Event on " + myEvent.Time + " ";
                            break;
                        }
                }
                eventMessage += myEvent.Text + Environment.NewLine + "Method: " + myEvent.Delegate + Environment.NewLine + "------------------------" + Environment.NewLine;
                sw.WriteLine(eventMessage);
            }
        }
    }
    class EventLog
    {
        public string Text { get; set; }
        public string Delegate { get; set; }
        public DateTime Time { get; set; }
        public EventType Type { get; set; }
    }

    class EventLogList : List<EventLog> { }

    public enum EventType
    {
        Exception = 1,
        Message = 2
    }
}
