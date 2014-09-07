using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace USBIRToyv2
{
    public partial class Form1 : Form
    {
        #region Form
        public Form1()
        {
            InitializeComponent();
            enableSoundsToolStripMenuItem.Checked = ConfigManager.PlaySound;
            SoundManager.Initialize();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //DBManager.InitializeDB();
                GetDevices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit(e);
        }
        #endregion Form
        #region MenuStrip
        private void enableSoundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableSoundsToolStripMenuItem.Checked =
            ConfigManager.PlaySound = !ConfigManager.PlaySound;
        }

        private void clearItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lsbReceivedKeys.Items.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void getDevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lsbReceivedKeys.Items.Clear();
            CloseDevices();
            GetDevices();
        }


        private void editDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DettachEventHandler();
            DBMenu myDBEditForm = new DBMenu(IRToy.IRToyList);
            myDBEditForm.ShowDialog();
            AttachEventHandler();
        }
        #endregion MenuStrip
        #region Code

        private void GetDevices()
        {
            IRToy.IRToyList = new IRToyList();
            foreach (string port_name in SerialPortManager.ComPortNames("04D8","FD08"))
            {
                try
                {
                    IRToy myIRToy = new IRToy(port_name);
                    IRToy.IRToyList.Add(myIRToy);
                    lsbReceivedKeys.Items.Add(port_name + " : Device connected.");
                }
                catch (Exception ex)
                {
                    lsbReceivedKeys.Items.Add(port_name +" : " + ex.Message);
                }
            }
            AttachEventHandler();
        }

        private void CloseDevices()
        {
            for(int i = 0; i < IRToy.IRToyList.Count; i ++)
            {
                string desc = "Unknown";
                try
                {
                    DettachEventHandler(IRToy.IRToyList[i]);
                    desc = IRToy.IRToyList[i].IRToyDesc;
                    IRToy.IRToyList[i].CloseDevice();
                    IRToy.IRToyList[i] = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(desc + ": " + ex.Message);
                }
                GC.Collect();
            }
        }

        private void AttachEventHandler(IRToy myIRToy = null)
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
        private void DettachEventHandler(IRToy myIRToy = null)
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


        private void Exit(FormClosingEventArgs e = null)
        {
            if (e != null)
            {
                e.Cancel = true;
            }
            CloseDevices();
            Environment.Exit(0);
        }
        private void KeyReceived(object sender, KeyReceivedEventArgs e)
        {
            DBRemoteKey myRemoteKey = e.Key;
            //if (e.DBMatchFound)
            //{
            //    try
            //    {
            //        IRToy.Transmit("COM6", myRemoteKey.KeyDesc, myRemoteKey.RemoteDesc);
            //    }
            //    catch (Exception ex) { SendKeyToListBox(ex.Message); }
            //}
            string key_desc = myRemoteKey.KeyDesc;
            string remote_key_desc = myRemoteKey.RemoteDesc;
            SendKeyToListBox(e.ComPortName + ": " + remote_key_desc + " " + key_desc);
        }

        delegate void SetTextCallback(string text);
        private void SendKeyToListBox(string text)
        {
            if (lsbReceivedKeys.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SendKeyToListBox);
                Invoke(d, new object[] { text });
            }
            else
            {
                lsbReceivedKeys.Items.Add(text);
                lsbReceivedKeys.SelectedIndex = lsbReceivedKeys.Items.Count - 1;
            }
        }
        #endregion Code
    }
}
