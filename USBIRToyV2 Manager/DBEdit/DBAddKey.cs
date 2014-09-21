using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace USBIRToyv2
{
    public partial class DBAddKey : Form
    {
        string remote_desc;
        IRToy myIRToy;
        short[] tempKey = null;
        decimal tempFQ = 0;
        byte tempPR2 = 0;
        DBRemoteKey tempRemoteKey = null;
        DateTime last_key_received_time;
        public DBAddKey(IRToy IRToy, string RemoteDesc)
        {
            myIRToy = IRToy;
            remote_desc = RemoteDesc;
            InitializeComponent();
            this.KeyPreview = true;
            btnAdd.Enabled = false;
            last_key_received_time = DateTime.Now;
        }
        private void AddKey_Load(object sender, EventArgs e)
        {
            myIRToy.OnKeyReceived += new IRToy.KeyReceivedHandler(KeyReceived);
        }
        private void AddKey_FormClosed(object sender, FormClosedEventArgs e)
        {
            myIRToy.OnKeyReceived -= new IRToy.KeyReceivedHandler(KeyReceived);
        }

        private void txtKeyDesc_TextChanged(object sender, EventArgs e)
        {
            if (txtKeyDesc.Text.Length > 0 && chkOne.Checked && chkTwo.Checked)
            {
                btnAdd.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
            }
        }
        
        private void KeyReceived(object sender, KeyReceivedEventArgs e)
        {
            try
            {
                if ((DateTime.Now - last_key_received_time).TotalMilliseconds >= 300)
                {
                    DBRemoteKey myRemoteKey = e.Key;
                    if (tempKey == null)
                    {
                        tempKey = myRemoteKey.Value;
                        tempFQ = myIRToy.GetLastKeyFQ();
                        if (tempFQ == 0)
                        {
                            throw new Exception("Unable to get FQ!");
                        }
                        changelblFQ(tempFQ.ToString());
                        changeChkOne(true);
                    }
                    else
                    {
                        decimal secondTempFQ = (myIRToy.GetLastKeyFQ() + tempFQ) / 2;
                        if (secondTempFQ == 0)
                        {
                            throw new Exception("Unable to get FQ!");
                        }
                        changelblFQ(tempFQ.ToString());
                        tempPR2 = PR2.GetPR2Code(tempFQ);

                        if (DBManager.CompareTwoKeys(tempKey, myRemoteKey.Value))
                        {
                            tempRemoteKey = myRemoteKey;
                            tempRemoteKey.Frequency = tempFQ;
                            tempRemoteKey.PR2Code = tempPR2;
                            changeChkTwo(true);
                            if (txtKeyDesc.Text.Length > 0)
                            {
                                changeBtnAdd(true);
                            }
                        }
                        else
                        {
                            Reset();
                        }
                    }
                }
                last_key_received_time = DateTime.Now;
            }
            catch(Exception ex)
            {
                Reset();
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }
        delegate void changeBtnAddCallback(bool add_button);
        private void changeBtnAdd(bool add_button)
        {
            if (btnAdd.InvokeRequired)
            {
                changeBtnAddCallback d = new changeBtnAddCallback(changeBtnAdd);
                Invoke(d, new object[] { add_button });
            }
            else
            {
                btnAdd.Enabled = add_button;
            }
        }

        delegate void changeChkOneCallback(bool chk_one);
        private void changeChkOne(bool chk_one)
        {
            if (chkOne.InvokeRequired)
            {
                changeChkOneCallback d = new changeChkOneCallback(changeChkOne);
                Invoke(d, new object[] { chk_one});
            }
            else
            {
                chkOne.Checked = chk_one;
            }
        }

        delegate void changeChkTwoCallback(bool chk_two);
        private void changeChkTwo(bool chk_two)
        {
            if (chkTwo.InvokeRequired)
            {
                changeChkTwoCallback d = new changeChkTwoCallback(changeChkTwo);
                Invoke(d, new object[] { chk_two });
            }
            else
            {
                chkTwo.Checked = chk_two;
            }
        }

        delegate void changelblFQCallback(string fq);
        private void changelblFQ(string fq)
        {
            if (chkTwo.InvokeRequired)
            {
                changelblFQCallback d = new changelblFQCallback(changelblFQ);
                Invoke(d, new object[] { fq });
            }
            else
            {
                lblFQ.Text = fq;
            }
        }


        private void Reset()
        {
            tempKey = null;
            tempFQ = 0;
            tempPR2 = 0;
            changelblFQ("0");
            changeBtnAdd(false);
            changeChkOne(false);
            changeChkTwo(false);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (DBRemote myRemote in DBManager.myRemotes)
            {
                if (remote_desc == myRemote.RemoteDesc)
                {
                    tempRemoteKey.KeyDesc = txtKeyDesc.Text;
                    tempRemoteKey.RemoteDesc = myRemote.RemoteDesc;
                    myRemote.Keys.Add(tempRemoteKey);
                    break;
                }
            }
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void AddKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
