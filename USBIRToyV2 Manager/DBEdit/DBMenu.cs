using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace USBIRToyv2
{
    public partial class DBMenu : Form
    {
        IRToyList myIRToyList;
        public DBMenu(IRToyList ir_toy_list)
        {
            myIRToyList = ir_toy_list;
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void DBEdit_Load(object sender, EventArgs e)
        {
            ShowDevices();
            if (DBManager.myRemotes == null)
            {
                DBManager.myRemotes = new DBRemoteList();
            }
            else
            {
                ShowRemotes();
                ShowRemoteKeys();
            }
        }

        private void ShowDevices()
        {
            cbDevices.Items.Clear();
            int count_devices = 0;
            foreach (IRToy myIRToy in myIRToyList)
            {
                cbDevices.Items.Insert(count_devices, myIRToy.IRToyDesc);
                count_devices++;
            }
            if (cbDevices.SelectedIndex == -1 && count_devices > 0)
            {
                cbDevices.SelectedIndex = count_devices - 1;
            }
            else if (cbDevices.SelectedIndex == -1)
            {
                btnAdd.Enabled =
                btnEdit.Enabled =
                btnRemove.Enabled =
                btnTransmit.Enabled = false;
            }
        }

        private void ShowRemotes()
        {
            cbRemotes.Items.Clear();
            int count_remotes = 0;
            foreach (DBRemote myRemote in DBManager.myRemotes)
            {
                cbRemotes.Items.Insert(count_remotes, myRemote.RemoteDesc);
                count_remotes++;
            }
            if (cbRemotes.SelectedIndex == -1)
            {
                int index = 0;
                if (count_remotes > 0)
                {
                    index = count_remotes - 1;
                    cbRemotes.SelectedIndex = index;
                }
                
            }
        }

        private void ShowRemoteKeys()
        {
            lbKeys.Items.Clear();
            if (cbRemotes.SelectedIndex != -1)
            {
                int count_keys = 0;
                foreach (DBRemoteKey myRemoteKey in DBManager.myRemotes[cbRemotes.SelectedIndex].Keys)
                {
                    lbKeys.Items.Insert(count_keys, myRemoteKey.KeyDesc);
                    count_keys++;
                }
            }
        }

        private void addNewRemoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBAddRemote myDBAddRemoteForm = new DBAddRemote();
            myDBAddRemoteForm.ShowDialog();
            if (myDBAddRemoteForm.DialogResult == DialogResult.OK)
            {
                ShowRemotes();
                ShowRemoteKeys();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbRemotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowRemoteKeys();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IRToy selectedIRToy = null;
            foreach (IRToy myIRToy in myIRToyList)
            {
                if ((string)cbDevices.SelectedItem == myIRToy.IRToyDesc)
                {
                    selectedIRToy = myIRToy;
                    break;
                }
            }
            if (selectedIRToy != null)
            {
                selectedIRToy.SetRecordMode(true);
                DBAddKey myAddKeyForm = new DBAddKey(selectedIRToy, (string)cbRemotes.SelectedItem);
                myAddKeyForm.ShowDialog();
                selectedIRToy.SetRecordMode(false);
                ShowRemoteKeys();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DBRemote myRemote in DBManager.myRemotes)
            {
                if (myRemote.RemoteDesc == (string)cbRemotes.SelectedItem)
                {
                    for (int i = 0; i < myRemote.Keys.Count; i++)
                    {
                        if (myRemote.Keys[i].KeyDesc == (string)lbKeys.SelectedItem)
                        {
                            myRemote.Keys.RemoveAt(i);
                            break;
                        }
                    }
                    break;
                }
            }
            ShowRemoteKeys();
        }

        private void btnTransmit_Click(object sender, EventArgs e)
        {
            IRToy selectedIRToy = null;
            foreach (IRToy myIRToy in myIRToyList)
            {
                if ((string)cbDevices.SelectedItem == myIRToy.IRToyDesc)
                {
                    selectedIRToy = myIRToy;
                    break;
                }
            }
            if (selectedIRToy != null)
            {
                try
                {
                    selectedIRToy.Transmit((string)lbKeys.SelectedItem, (string)cbRemotes.SelectedItem);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print(ex.Message);
                }
            }
        }

        private void cbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDevices.SelectedIndex == -1)
            {
                btnAdd.Enabled =
                btnEdit.Enabled =
                btnRemove.Enabled =
                btnTransmit.Enabled = true;
            }
        }

        private void saveChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.FileName = "Default.irdb";
                saveDialog.Filter = "IR Toy DB files (*.irdb)|*.irdb";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter myWriter = new StreamWriter(saveDialog.FileName, false))
                    {
                        XmlSerializer mySerializer = new XmlSerializer(typeof(DBRemoteList));
                        mySerializer.Serialize(myWriter, DBManager.myRemotes);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void openDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "IR Toy DB files (*.irdb)|*.irdb";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(DBRemoteList));
                    StreamReader reader = new StreamReader(openDialog.FileName);
                    DBRemoteList myLoadedRemoteList = (DBRemoteList)serializer.Deserialize(reader);
                    reader.Close();
                    DBManager.myRemotes = myLoadedRemoteList;
                    ShowRemotes();
                    ShowRemoteKeys();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DBMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    
    }
}
