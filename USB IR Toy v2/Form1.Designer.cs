namespace USBIRToyv2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lsbReceivedKeys = new System.Windows.Forms.ListBox();
            this.msmain = new System.Windows.Forms.MenuStrip();
            this.clearAllItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableSoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msmain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsbReceivedKeys
            // 
            this.lsbReceivedKeys.FormattingEnabled = true;
            this.lsbReceivedKeys.ItemHeight = 25;
            this.lsbReceivedKeys.Location = new System.Drawing.Point(0, 43);
            this.lsbReceivedKeys.Name = "lsbReceivedKeys";
            this.lsbReceivedKeys.Size = new System.Drawing.Size(531, 254);
            this.lsbReceivedKeys.TabIndex = 0;
            // 
            // msmain
            // 
            this.msmain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllItemsToolStripMenuItem,
            this.editDBToolStripMenuItem,
            this.getDevicesToolStripMenuItem});
            this.msmain.Location = new System.Drawing.Point(0, 0);
            this.msmain.Name = "msmain";
            this.msmain.Size = new System.Drawing.Size(531, 40);
            this.msmain.TabIndex = 1;
            this.msmain.Text = "menuStrip1";
            // 
            // clearAllItemsToolStripMenuItem
            // 
            this.clearAllItemsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableSoundsToolStripMenuItem,
            this.clearItemsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.clearAllItemsToolStripMenuItem.Name = "clearAllItemsToolStripMenuItem";
            this.clearAllItemsToolStripMenuItem.Size = new System.Drawing.Size(90, 36);
            this.clearAllItemsToolStripMenuItem.Text = "Menu";
            // 
            // enableSoundsToolStripMenuItem
            // 
            this.enableSoundsToolStripMenuItem.Name = "enableSoundsToolStripMenuItem";
            this.enableSoundsToolStripMenuItem.Size = new System.Drawing.Size(247, 36);
            this.enableSoundsToolStripMenuItem.Text = "Enable Sounds";
            this.enableSoundsToolStripMenuItem.Click += new System.EventHandler(this.enableSoundsToolStripMenuItem_Click);
            // 
            // clearItemsToolStripMenuItem
            // 
            this.clearItemsToolStripMenuItem.Name = "clearItemsToolStripMenuItem";
            this.clearItemsToolStripMenuItem.Size = new System.Drawing.Size(247, 36);
            this.clearItemsToolStripMenuItem.Text = "Clear Items";
            this.clearItemsToolStripMenuItem.Click += new System.EventHandler(this.clearItemsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(247, 36);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editDBToolStripMenuItem
            // 
            this.editDBToolStripMenuItem.Name = "editDBToolStripMenuItem";
            this.editDBToolStripMenuItem.Size = new System.Drawing.Size(105, 36);
            this.editDBToolStripMenuItem.Text = "Edit DB";
            this.editDBToolStripMenuItem.Click += new System.EventHandler(this.editDBToolStripMenuItem_Click);
            // 
            // getDevicesToolStripMenuItem
            // 
            this.getDevicesToolStripMenuItem.Name = "getDevicesToolStripMenuItem";
            this.getDevicesToolStripMenuItem.Size = new System.Drawing.Size(153, 36);
            this.getDevicesToolStripMenuItem.Text = "Get Devices";
            this.getDevicesToolStripMenuItem.Click += new System.EventHandler(this.getDevicesToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 293);
            this.Controls.Add(this.lsbReceivedKeys);
            this.Controls.Add(this.msmain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.msmain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IR Toy v2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.msmain.ResumeLayout(false);
            this.msmain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lsbReceivedKeys;
        private System.Windows.Forms.MenuStrip msmain;
        private System.Windows.Forms.ToolStripMenuItem clearAllItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableSoundsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getDevicesToolStripMenuItem;
    }
}

