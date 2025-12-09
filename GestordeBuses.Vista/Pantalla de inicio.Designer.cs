namespace GestordeBuses.Vista
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.close1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(53)))), ((int)(((byte)(138)))));
            this.panel1.Controls.Add(this.close1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1182, 66);
            this.panel1.TabIndex = 2;
            // 
            // close1
            // 
            this.close1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close1.Image = global::GestordeBuses.Vista.Properties.Resources.icons8_close_60;
            this.close1.Location = new System.Drawing.Point(1118, 8);
            this.close1.Name = "close1";
            this.close1.Size = new System.Drawing.Size(52, 51);
            this.close1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.close1.TabIndex = 2;
            this.close1.TabStop = false;
            this.close1.Click += new System.EventHandler(this.pictureBox2_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 36F);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(432, -4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(365, 70);
            this.label1.TabIndex = 1;
            this.label1.Text = "ControlBus";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackgroundImage = global::GestordeBuses.Vista.Properties.Resources.prueba2;
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Bookman Old Style", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.AliceBlue;
            this.btnLogin.Location = new System.Drawing.Point(440, 325);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(431, 64);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GestordeBuses.Vista.Properties.Resources.imagen_2025_08_16_211336799_removebg_preview;
            this.pictureBox1.Location = new System.Drawing.Point(-20, 118);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1067, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(204)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1182, 711);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.PictureBox close1;
    }
}

