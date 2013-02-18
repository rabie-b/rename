namespace rename
{
    partial class AskToReplace
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Btn_No = new System.Windows.Forms.Button();
            this.Rtn_Yes = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(23, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 41);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.label1.Location = new System.Drawing.Point(310, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.Btn_No);
            this.panel2.Controls.Add(this.Rtn_Yes);
            this.panel2.Location = new System.Drawing.Point(0, 280);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(481, 56);
            this.panel2.TabIndex = 1;
            // 
            // Btn_No
            // 
            this.Btn_No.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Btn_No.Location = new System.Drawing.Point(382, 16);
            this.Btn_No.Name = "Btn_No";
            this.Btn_No.Size = new System.Drawing.Size(85, 26);
            this.Btn_No.TabIndex = 2;
            this.Btn_No.Text = "No";
            this.Btn_No.UseVisualStyleBackColor = true;
            this.Btn_No.Click += new System.EventHandler(this.Btn_No_Click);
            // 
            // Rtn_Yes
            // 
            this.Rtn_Yes.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Rtn_Yes.Location = new System.Drawing.Point(281, 16);
            this.Rtn_Yes.Name = "Rtn_Yes";
            this.Rtn_Yes.Size = new System.Drawing.Size(85, 26);
            this.Rtn_Yes.TabIndex = 1;
            this.Rtn_Yes.Text = "Yes";
            this.Rtn_Yes.UseVisualStyleBackColor = true;
            this.Rtn_Yes.Click += new System.EventHandler(this.Rtn_Yes_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBox1.Location = new System.Drawing.Point(75, 26);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(406, 248);
            this.textBox1.TabIndex = 4;
            // 
            // AskToReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(482, 336);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AskToReplace";
            this.Text = "Renamer";
            this.Load += new System.EventHandler(this.AskToReplace_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Btn_No;
        private System.Windows.Forms.Button Rtn_Yes;
        private System.Windows.Forms.TextBox textBox1;

    }
}