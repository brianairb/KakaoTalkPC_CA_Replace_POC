namespace KakaoTalkPC_CA_Replacer
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
            this.readBtn = new System.Windows.Forms.Button();
            this.origTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.writeBtn = new System.Windows.Forms.Button();
            this.newTextBox = new System.Windows.Forms.TextBox();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // readBtn
            // 
            this.readBtn.Enabled = false;
            this.readBtn.Location = new System.Drawing.Point(516, 19);
            this.readBtn.Name = "readBtn";
            this.readBtn.Size = new System.Drawing.Size(75, 120);
            this.readBtn.TabIndex = 0;
            this.readBtn.Text = "Read";
            this.readBtn.UseVisualStyleBackColor = true;
            this.readBtn.Click += new System.EventHandler(this.readBtn_Click);
            // 
            // origTextBox
            // 
            this.origTextBox.Enabled = false;
            this.origTextBox.Location = new System.Drawing.Point(6, 19);
            this.origTextBox.Multiline = true;
            this.origTextBox.Name = "origTextBox";
            this.origTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.origTextBox.Size = new System.Drawing.Size(504, 120);
            this.origTextBox.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.origTextBox);
            this.groupBox1.Controls.Add(this.readBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 150);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Original CA Cert";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.writeBtn);
            this.groupBox2.Controls.Add(this.newTextBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(597, 150);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "New CA Cert";
            // 
            // writeBtn
            // 
            this.writeBtn.Enabled = false;
            this.writeBtn.Location = new System.Drawing.Point(516, 19);
            this.writeBtn.Name = "writeBtn";
            this.writeBtn.Size = new System.Drawing.Size(75, 120);
            this.writeBtn.TabIndex = 1;
            this.writeBtn.Text = "Write";
            this.writeBtn.UseVisualStyleBackColor = true;
            this.writeBtn.Click += new System.EventHandler(this.writeBtn_Click);
            // 
            // newTextBox
            // 
            this.newTextBox.Enabled = false;
            this.newTextBox.Location = new System.Drawing.Point(6, 19);
            this.newTextBox.Multiline = true;
            this.newTextBox.Name = "newTextBox";
            this.newTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.newTextBox.Size = new System.Drawing.Size(504, 120);
            this.newTextBox.TabIndex = 0;
            // 
            // refreshBtn
            // 
            this.refreshBtn.Location = new System.Drawing.Point(528, 350);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(75, 23);
            this.refreshBtn.TabIndex = 4;
            this.refreshBtn.Text = "Refresh";
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 355);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 13);
            this.statusLabel.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 385);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.refreshBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "KakaoTalk PC CA Cert Replacer (POC) -- http://www.bpak.org";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button readBtn;
        private System.Windows.Forms.TextBox origTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button writeBtn;
        private System.Windows.Forms.TextBox newTextBox;
        private System.Windows.Forms.Button refreshBtn;
        private System.Windows.Forms.Label statusLabel;
    }
}

