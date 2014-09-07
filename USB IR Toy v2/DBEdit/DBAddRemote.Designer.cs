namespace USBIRToyv2
{
    partial class DBAddRemote
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
            this.txtRemoteDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRemote = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtRemoteDesc
            // 
            this.txtRemoteDesc.Location = new System.Drawing.Point(232, 17);
            this.txtRemoteDesc.MaxLength = 100;
            this.txtRemoteDesc.Name = "txtRemoteDesc";
            this.txtRemoteDesc.Size = new System.Drawing.Size(193, 31);
            this.txtRemoteDesc.TabIndex = 0;
            this.txtRemoteDesc.TextChanged += new System.EventHandler(this.txtRemoteDesc_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Remote Description";
            // 
            // btnRemote
            // 
            this.btnRemote.Location = new System.Drawing.Point(230, 63);
            this.btnRemote.Name = "btnRemote";
            this.btnRemote.Size = new System.Drawing.Size(195, 49);
            this.btnRemote.TabIndex = 5;
            this.btnRemote.Text = "Add Remote";
            this.btnRemote.UseVisualStyleBackColor = true;
            this.btnRemote.Click += new System.EventHandler(this.btnRemote_Click);
            // 
            // DBAddRemote
            // 
            this.AcceptButton = this.btnRemote;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 125);
            this.Controls.Add(this.btnRemote);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRemoteDesc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBAddRemote";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Remote";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DBAddRemote_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRemoteDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemote;
    }
}