namespace PharmaCore.POS.PL {
    partial class Frm_LoginEmployee {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LoginEmployee));
            txtUser = new TextBox();
            btnLogin = new Button();
            label2 = new Label();
            txtPassword = new TextBox();
            label5 = new Label();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            label1 = new Label();
            btnExit = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtUser
            // 
            txtUser.Location = new Point(31, 90);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(242, 26);
            txtUser.TabIndex = 30;
            txtUser.TextAlign = HorizontalAlignment.Center;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(63, 181);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(183, 38);
            btnLogin.TabIndex = 32;
            btnLogin.Text = "دخول";
            btnLogin.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Black;
            label2.Location = new Point(114, 127);
            label2.Name = "label2";
            label2.Size = new Size(69, 18);
            label2.TabIndex = 36;
            label2.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(31, 148);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(200, 26);
            txtPassword.TabIndex = 31;
            txtPassword.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Black;
            label5.Location = new Point(101, 66);
            label5.Name = "label5";
            label5.Size = new Size(109, 18);
            label5.TabIndex = 35;
            label5.Text = "Employee Code";
            // 
            // simpleButton1
            // 
            simpleButton1.ImageOptions.Image = (Image)resources.GetObject("simpleButton1.ImageOptions.Image");
            simpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            simpleButton1.Location = new Point(237, 148);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new Size(36, 27);
            simpleButton1.TabIndex = 33;
            // 
            // simpleButton2
            // 
            simpleButton2.ImageOptions.Image = (Image)resources.GetObject("simpleButton2.ImageOptions.Image");
            simpleButton2.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            simpleButton2.Location = new Point(237, 148);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new Size(36, 27);
            simpleButton2.TabIndex = 34;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(192, 0, 0);
            label1.Location = new Point(63, 8);
            label1.Name = "label1";
            label1.Size = new Size(193, 35);
            label1.TabIndex = 200;
            label1.Text = "Employee PIN";
            // 
            // btnExit
            // 
            btnExit.Location = new Point(63, 225);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(183, 38);
            btnExit.TabIndex = 202;
            btnExit.Text = "خروج";
            btnExit.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(295, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(253, 275);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 201;
            pictureBox1.TabStop = false;
            // 
            // Frm_LoginEmployee
            // 
            Appearance.Options.UseFont = true;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(560, 275);
            Controls.Add(btnExit);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(txtUser);
            Controls.Add(btnLogin);
            Controls.Add(label2);
            Controls.Add(txtPassword);
            Controls.Add(label5);
            Controls.Add(simpleButton1);
            Controls.Add(simpleButton2);
            Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Frm_LoginEmployee";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "شاشه دخول العميل";
            Load += Frm_LoginEmployee_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUser;
        private Button btnLogin;
        private Label label2;
        private TextBox txtPassword;
        private Label label5;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private Label label1;
        private Button btnExit;
        private PictureBox pictureBox1;
    }
}
