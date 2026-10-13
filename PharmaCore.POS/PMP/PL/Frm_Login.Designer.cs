namespace PharmaCore.POS.PL
{
    partial class Frm_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Login));
            txtPassword = new TextBox();
            labUserName = new Label();
            pictureBox1 = new PictureBox();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            label2 = new Label();
            label4 = new Label();
            btnExit = new Button();
            btnLogin = new Button();
            txtUser = new TextBox();
            label1 = new Label();
            label6 = new Label();
            label7 = new Label();
            labMacName = new Label();
            labMacID = new Label();
            labBrCode = new Label();
            labOrgName = new Label();
            label12 = new Label();
            label13 = new Label();
            labBrName = new Label();
            label15 = new Label();
            labBrUser = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(243, 179);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(200, 26);
            txtPassword.TabIndex = 2;
            txtPassword.TextAlign = HorizontalAlignment.Center;
            txtPassword.TextChanged += txtPassword_TextChanged;
            txtPassword.KeyPress += txtPassword_KeyPress;
            // 
            // labUserName
            // 
            labUserName.AutoSize = true;
            labUserName.Cursor = Cursors.Hand;
            labUserName.ForeColor = Color.Black;
            labUserName.Location = new Point(243, 98);
            labUserName.Name = "labUserName";
            labUserName.Size = new Size(107, 18);
            labUserName.TabIndex = 18;
            labUserName.Text = "اسم المستخدم";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(523, 36);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(253, 176);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 25;
            pictureBox1.TabStop = false;
            // 
            // simpleButton1
            // 
            simpleButton1.ImageOptions.Image = (Image)resources.GetObject("simpleButton1.ImageOptions.Image");
            simpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            simpleButton1.Location = new Point(449, 178);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new Size(36, 27);
            simpleButton1.TabIndex = 5;
            simpleButton1.Click += simpleButton1_Click;
            // 
            // simpleButton2
            // 
            simpleButton2.ImageOptions.Image = (Image)resources.GetObject("simpleButton2.ImageOptions.Image");
            simpleButton2.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            simpleButton2.Location = new Point(449, 178);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new Size(36, 27);
            simpleButton2.TabIndex = 6;
            simpleButton2.Click += simpleButton2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Black;
            label2.Location = new Point(243, 158);
            label2.Name = "label2";
            label2.Size = new Size(77, 18);
            label2.TabIndex = 29;
            label2.Text = "كلمة المرور";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Cursor = Cursors.No;
            label4.Font = new Font("Tahoma", 8.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Blue;
            label4.Location = new Point(679, 285);
            label4.Name = "label4";
            label4.Size = new Size(97, 13);
            label4.TabIndex = 33;
            label4.Text = "فتح إعدادت الشبكة";
            label4.Click += btnServerSetting_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(569, 240);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(183, 38);
            btnExit.TabIndex = 4;
            btnExit.Text = "خروج";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(263, 232);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(183, 38);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "دخول";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtUser
            // 
            txtUser.Location = new Point(243, 119);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(242, 26);
            txtUser.TabIndex = 1;
            txtUser.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(192, 0, 0);
            label1.Location = new Point(263, 31);
            label1.Name = "label1";
            label1.Size = new Size(180, 35);
            label1.TabIndex = 199;
            label1.Text = "Branch Login";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Tahoma", 11.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(12, 17);
            label6.Name = "label6";
            label6.Size = new Size(115, 18);
            label6.TabIndex = 200;
            label6.Text = "Machine Name :";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Tahoma", 11.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(12, 64);
            label7.Name = "label7";
            label7.Size = new Size(92, 18);
            label7.TabIndex = 201;
            label7.Text = "Machine ID :";
            // 
            // labMacName
            // 
            labMacName.AutoSize = true;
            labMacName.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labMacName.ForeColor = Color.FromArgb(192, 0, 0);
            labMacName.Location = new Point(12, 40);
            labMacName.Name = "labMacName";
            labMacName.Size = new Size(47, 16);
            labMacName.TabIndex = 202;
            labMacName.Text = "..........";
            // 
            // labMacID
            // 
            labMacID.AutoSize = true;
            labMacID.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labMacID.ForeColor = Color.FromArgb(192, 0, 0);
            labMacID.Location = new Point(12, 87);
            labMacID.Name = "labMacID";
            labMacID.Size = new Size(47, 16);
            labMacID.TabIndex = 203;
            labMacID.Text = "..........";
            // 
            // labBrCode
            // 
            labBrCode.AutoSize = true;
            labBrCode.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labBrCode.ForeColor = Color.FromArgb(192, 0, 0);
            labBrCode.Location = new Point(12, 184);
            labBrCode.Name = "labBrCode";
            labBrCode.Size = new Size(47, 16);
            labBrCode.TabIndex = 207;
            labBrCode.Text = "..........";
            // 
            // labOrgName
            // 
            labOrgName.AutoSize = true;
            labOrgName.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labOrgName.ForeColor = Color.FromArgb(192, 0, 0);
            labOrgName.Location = new Point(12, 137);
            labOrgName.Name = "labOrgName";
            labOrgName.Size = new Size(47, 16);
            labOrgName.TabIndex = 206;
            labOrgName.Text = "..........";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Tahoma", 11.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(12, 161);
            label12.Name = "label12";
            label12.Size = new Size(101, 18);
            label12.TabIndex = 205;
            label12.Text = "Branch Code :";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Tahoma", 11.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.Black;
            label13.Location = new Point(12, 114);
            label13.Name = "label13";
            label13.Size = new Size(86, 18);
            label13.TabIndex = 204;
            label13.Text = "Org Name :";
            // 
            // labBrName
            // 
            labBrName.AutoSize = true;
            labBrName.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labBrName.ForeColor = Color.FromArgb(192, 0, 0);
            labBrName.Location = new Point(12, 234);
            labBrName.Name = "labBrName";
            labBrName.Size = new Size(47, 16);
            labBrName.TabIndex = 209;
            labBrName.Text = "..........";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Tahoma", 11.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label15.ForeColor = Color.Black;
            label15.Location = new Point(12, 211);
            label15.Name = "label15";
            label15.Size = new Size(107, 18);
            label15.TabIndex = 208;
            label15.Text = "Branch Name :";
            // 
            // labBrUser
            // 
            labBrUser.AutoSize = true;
            labBrUser.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labBrUser.ForeColor = Color.FromArgb(192, 0, 0);
            labBrUser.Location = new Point(12, 279);
            labBrUser.Name = "labBrUser";
            labBrUser.Size = new Size(47, 16);
            labBrUser.TabIndex = 211;
            labBrUser.Text = "..........";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 11.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(12, 256);
            label5.Name = "label5";
            label5.Size = new Size(135, 18);
            label5.TabIndex = 210;
            label5.Text = "Branch Username :";
            // 
            // Frm_Login
            // 
            Appearance.Options.UseFont = true;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(806, 307);
            Controls.Add(labBrUser);
            Controls.Add(label5);
            Controls.Add(labBrName);
            Controls.Add(label15);
            Controls.Add(labBrCode);
            Controls.Add(labOrgName);
            Controls.Add(label12);
            Controls.Add(label13);
            Controls.Add(labMacID);
            Controls.Add(labMacName);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label1);
            Controls.Add(txtUser);
            Controls.Add(btnLogin);
            Controls.Add(btnExit);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(txtPassword);
            Controls.Add(labUserName);
            Controls.Add(simpleButton1);
            Controls.Add(simpleButton2);
            Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Frm_Login";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "شاشة الدخول للنظام";
            Load += Frm_Login_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label labUserName;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtUser;
        private Label label1;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label labBrName;
        private Label label15;
        // Add missing field declarations
        private System.Windows.Forms.Label labMacName;
        private System.Windows.Forms.Label labMacID;
        private System.Windows.Forms.Label labBrCode;
        private System.Windows.Forms.Label labOrgName;
        private Label labBrUser;
        private Label label5;
    }
}