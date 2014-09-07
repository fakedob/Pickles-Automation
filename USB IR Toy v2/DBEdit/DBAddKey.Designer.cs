namespace USBIRToyv2
{
    partial class DBAddKey
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
            this.txtKeyDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkOne = new System.Windows.Forms.CheckBox();
            this.chkTwo = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRepeatFound = new System.Windows.Forms.Label();
            this.lblFQ = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtKeyDesc
            // 
            this.txtKeyDesc.Location = new System.Drawing.Point(234, 32);
            this.txtKeyDesc.Name = "txtKeyDesc";
            this.txtKeyDesc.Size = new System.Drawing.Size(228, 31);
            this.txtKeyDesc.TabIndex = 0;
            this.txtKeyDesc.TextChanged += new System.EventHandler(this.txtKeyDesc_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Key Description";
            // 
            // chkOne
            // 
            this.chkOne.AutoCheck = false;
            this.chkOne.AutoSize = true;
            this.chkOne.Location = new System.Drawing.Point(234, 81);
            this.chkOne.Name = "chkOne";
            this.chkOne.Size = new System.Drawing.Size(151, 29);
            this.chkOne.TabIndex = 2;
            this.chkOne.Text = "Check One";
            this.chkOne.UseVisualStyleBackColor = true;
            // 
            // chkTwo
            // 
            this.chkTwo.AutoCheck = false;
            this.chkTwo.AutoSize = true;
            this.chkTwo.Location = new System.Drawing.Point(234, 134);
            this.chkTwo.Name = "chkTwo";
            this.chkTwo.Size = new System.Drawing.Size(151, 29);
            this.chkTwo.TabIndex = 3;
            this.chkTwo.Text = "Check Two";
            this.chkTwo.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "FQ: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Repeat: ";
            // 
            // lblRepeatFound
            // 
            this.lblRepeatFound.AutoSize = true;
            this.lblRepeatFound.Location = new System.Drawing.Point(94, 134);
            this.lblRepeatFound.Name = "lblRepeatFound";
            this.lblRepeatFound.Size = new System.Drawing.Size(0, 25);
            this.lblRepeatFound.TabIndex = 7;
            // 
            // lblFQ
            // 
            this.lblFQ.AutoSize = true;
            this.lblFQ.Location = new System.Drawing.Point(96, 98);
            this.lblFQ.Name = "lblFQ";
            this.lblFQ.Size = new System.Drawing.Size(0, 25);
            this.lblFQ.TabIndex = 6;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(234, 192);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(195, 49);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add Key";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(29, 192);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(195, 49);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // AddKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 270);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblRepeatFound);
            this.Controls.Add(this.lblFQ);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkTwo);
            this.Controls.Add(this.chkOne);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtKeyDesc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddKey";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Remote Key";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddKey_FormClosed);
            this.Load += new System.EventHandler(this.AddKey_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddKey_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKeyDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkOne;
        private System.Windows.Forms.CheckBox chkTwo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRepeatFound;
        private System.Windows.Forms.Label lblFQ;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnReset;
    }
}