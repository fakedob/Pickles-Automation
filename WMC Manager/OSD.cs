using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace WMC_Manager
{
    public partial class OSD : Form
    {
        static OSD formObject;
        static DateTime lastShow = DateTime.Now;
        public string OSDText
        { 
            set 
            {
                lastShow = DateTime.Now;
                SetText(value);
            } 
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                textBox1.Text = text;
            }
        }

        delegate void SetOpacityCallback(double value);

        private void SetOpacity(double value)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (textBox1.InvokeRequired)
            {
                SetOpacityCallback d = new SetOpacityCallback(SetOpacity);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                Opacity = value;
            }
        }

        public OSD()
        {
            InitializeComponent();
            new Thread(() =>
            {
                while (PicklesAutomation.ConfigManager.KeepThreadsAlive)
                {
                    while (Opacity != 0d && (DateTime.Now - lastShow).TotalSeconds >= 2)
                    {
                        SetOpacity(Opacity-0.01d);
                        Thread.Sleep(2);
                    }
                    while (Opacity != 1.00d && (DateTime.Now - lastShow).TotalSeconds < 2)
                    {
                        SetOpacity(Opacity + 0.01d);
                        Thread.Sleep(2);
                    }
                    Thread.Sleep(10);
                }
            }).Start();
            TopMost = true;
            formObject = this;
            Show();
        }

        public enum GWL
        {
            ExStyle = -20
        }

        public enum WS_EX
        {
            Transparent = 0x20,
            Layered = 0x80000
        }

        public enum LWA
        {
            ColorKey = 0x1,
            Alpha = 0x2
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, GWL nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);

        private void OSD_Shown(object sender, EventArgs e)
        {
            Opacity = 0;
           

            int wl = GetWindowLong(this.Handle, GWL.ExStyle);
            wl = wl | 0x80000 | 0x20;
            SetWindowLong(this.Handle, GWL.ExStyle, wl);
            SetLayeredWindowAttributes(this.Handle, 0, 128, LWA.Alpha);

            //BackColor = Color.FromArgb(62,62,62);
            textBox1.BackColor = Color.FromArgb(62, 62, 62);
            textBox1.ForeColor = Color.White;
            TransparencyKey = Color.Gray;

            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - 350, 50);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

    }
}
