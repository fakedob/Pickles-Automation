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
    public partial class DBAddRemote : Form
    {
        public DBAddRemote()
        {
            InitializeComponent();
            this.KeyPreview = true;
            btnRemote.Enabled = false;
        }

        private void btnRemote_Click(object sender, EventArgs e)
        {
            DBManager.AddRemote(txtRemoteDesc.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtRemoteDesc_TextChanged(object sender, EventArgs e)
        {
            if (txtRemoteDesc.Text.Count() > 0)
            {
                btnRemote.Enabled = true;
            }
            else
            {
                btnRemote.Enabled = false;
            }
        }

        private void DBAddRemote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
