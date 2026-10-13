namespace PharmaCore.POS.PL
{
    partial class Frm_ServerSetting
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
            btnSave = new Button();
            label1 = new Label();
            btnTestApi = new Button();
            btnLogin = new Button();
            label17 = new Label();
            chkSkipCert = new CheckBox();
            numTimeout = new NumericUpDown();
            txtBranchId = new DevExpress.XtraEditors.TextEdit();
            label2 = new Label();
            txtApiBase = new DevExpress.XtraEditors.TextEdit();
            label3 = new Label();
            btnExport = new Button();
            btnImport = new Button();
            btnDiag = new Button();
            cboProfile = new ComboBox();
            lblProfile = new Label();
            btnReset = new Button();
            ((System.ComponentModel.ISupportInitialize)numTimeout).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBranchId.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtApiBase.Properties).BeginInit();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Location = new Point(139, 272);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(183, 38);
            btnSave.TabIndex = 288;
            btnSave.Text = "حفظ الإعدادات";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Tahoma", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(178, 21);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(290, 33);
            label1.TabIndex = 295;
            label1.Text = "إعدادات الإتصال بالسيرفر";
            // 
            // btnTestApi
            // 
            btnTestApi.Location = new Point(328, 272);
            btnTestApi.Name = "btnTestApi";
            btnTestApi.Size = new Size(191, 38);
            btnTestApi.TabIndex = 291;
            btnTestApi.Text = "إختبار الإتصال";
            btnTestApi.UseVisualStyleBackColor = true;
            btnTestApi.Click += btnTestApi_Click;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(231, 326);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(183, 38);
            btnLogin.TabIndex = 290;
            btnLogin.Text = "الدخول";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.BackColor = Color.Transparent;
            label17.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label17.ForeColor = Color.Black;
            label17.Location = new Point(209, 212);
            label17.Name = "label17";
            label17.RightToLeft = RightToLeft.No;
            label17.Size = new Size(113, 20);
            label17.TabIndex = 308;
            label17.Text = "Timeout (sec) :";
            // 
            // chkSkipCert
            // 
            chkSkipCert.AutoSize = true;
            chkSkipCert.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkSkipCert.Location = new Point(326, 170);
            chkSkipCert.Name = "chkSkipCert";
            chkSkipCert.RightToLeft = RightToLeft.No;
            chkSkipCert.Size = new Size(87, 23);
            chkSkipCert.TabIndex = 307;
            chkSkipCert.Text = "SkipCert";
            chkSkipCert.UseVisualStyleBackColor = true;
            // 
            // numTimeout
            // 
            numTimeout.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numTimeout.Location = new Point(328, 210);
            numTimeout.Name = "numTimeout";
            numTimeout.Size = new Size(88, 27);
            numTimeout.TabIndex = 306;
            numTimeout.TextAlign = HorizontalAlignment.Center;
            numTimeout.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // txtBranchId
            // 
            txtBranchId.EditValue = "";
            txtBranchId.Location = new Point(178, 126);
            txtBranchId.Name = "txtBranchId";
            txtBranchId.Properties.Appearance.BorderColor = Color.Gray;
            txtBranchId.Properties.Appearance.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBranchId.Properties.Appearance.Options.UseBorderColor = true;
            txtBranchId.Properties.Appearance.Options.UseFont = true;
            txtBranchId.Properties.Appearance.Options.UseTextOptions = true;
            txtBranchId.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtBranchId.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtBranchId.Properties.AppearanceDisabled.BackColor = Color.Silver;
            txtBranchId.Properties.AppearanceDisabled.BorderColor = Color.Black;
            txtBranchId.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            txtBranchId.Properties.AppearanceDisabled.Options.UseBackColor = true;
            txtBranchId.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            txtBranchId.Properties.AppearanceDisabled.Options.UseFont = true;
            txtBranchId.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            txtBranchId.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtBranchId.Properties.AppearanceFocused.BackColor = Color.Cyan;
            txtBranchId.Properties.AppearanceFocused.BorderColor = Color.Teal;
            txtBranchId.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            txtBranchId.Properties.AppearanceFocused.Options.UseBackColor = true;
            txtBranchId.Properties.AppearanceFocused.Options.UseBorderColor = true;
            txtBranchId.Properties.AppearanceFocused.Options.UseFont = true;
            txtBranchId.Properties.AppearanceFocused.Options.UseTextOptions = true;
            txtBranchId.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtBranchId.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            txtBranchId.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            txtBranchId.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            txtBranchId.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            txtBranchId.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            txtBranchId.Properties.AppearanceReadOnly.Options.UseFont = true;
            txtBranchId.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            txtBranchId.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtBranchId.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtBranchId.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            txtBranchId.RightToLeft = RightToLeft.No;
            txtBranchId.Size = new Size(399, 26);
            txtBranchId.TabIndex = 297;
            txtBranchId.KeyPress += txtBranchId_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(44, 129);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.No;
            label2.Size = new Size(128, 20);
            label2.TabIndex = 295;
            label2.Text = "Activation Code :";
            // 
            // txtApiBase
            // 
            txtApiBase.EditValue = "";
            txtApiBase.Location = new Point(178, 83);
            txtApiBase.Name = "txtApiBase";
            txtApiBase.Properties.Appearance.BorderColor = Color.Gray;
            txtApiBase.Properties.Appearance.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtApiBase.Properties.Appearance.Options.UseBorderColor = true;
            txtApiBase.Properties.Appearance.Options.UseFont = true;
            txtApiBase.Properties.Appearance.Options.UseTextOptions = true;
            txtApiBase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtApiBase.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtApiBase.Properties.AppearanceDisabled.BackColor = Color.Silver;
            txtApiBase.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            txtApiBase.Properties.AppearanceDisabled.Options.UseBackColor = true;
            txtApiBase.Properties.AppearanceDisabled.Options.UseFont = true;
            txtApiBase.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            txtApiBase.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtApiBase.Properties.AppearanceFocused.BackColor = Color.Cyan;
            txtApiBase.Properties.AppearanceFocused.BorderColor = Color.Teal;
            txtApiBase.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            txtApiBase.Properties.AppearanceFocused.Options.UseBackColor = true;
            txtApiBase.Properties.AppearanceFocused.Options.UseBorderColor = true;
            txtApiBase.Properties.AppearanceFocused.Options.UseFont = true;
            txtApiBase.Properties.AppearanceFocused.Options.UseTextOptions = true;
            txtApiBase.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtApiBase.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            txtApiBase.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            txtApiBase.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            txtApiBase.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            txtApiBase.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            txtApiBase.Properties.AppearanceReadOnly.Options.UseFont = true;
            txtApiBase.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            txtApiBase.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtApiBase.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtApiBase.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            txtApiBase.Properties.Name = "textEdit1";
            txtApiBase.RightToLeft = RightToLeft.No;
            txtApiBase.Size = new Size(399, 26);
            txtApiBase.TabIndex = 298;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(51, 86);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.No;
            label3.Size = new Size(121, 20);
            label3.TabIndex = 296;
            label3.Text = "API Base URL :";
            // 
            // btnExport
            // 
            btnExport.Location = new Point(46, 326);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(170, 38);
            btnExport.TabIndex = 310;
            btnExport.Text = "Export Settings";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(420, 326);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(170, 38);
            btnImport.TabIndex = 309;
            btnImport.Text = "Import Settings";
            btnImport.UseVisualStyleBackColor = true;
            // 
            // btnDiag
            // 
            btnDiag.Location = new Point(477, 218);
            btnDiag.Name = "btnDiag";
            btnDiag.Size = new Size(170, 38);
            btnDiag.TabIndex = 311;
            btnDiag.Text = "Diagnostics";
            btnDiag.UseVisualStyleBackColor = true;
            // 
            // cboProfile
            // 
            cboProfile.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProfile.Items.AddRange(new object[] { "Production", "Staging" });
            cboProfile.Location = new Point(178, 169);
            cboProfile.Name = "cboProfile";
            cboProfile.Size = new Size(130, 26);
            cboProfile.TabIndex = 312;
            // 
            // lblProfile
            // 
            lblProfile.AutoSize = true;
            lblProfile.BackColor = Color.Transparent;
            lblProfile.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblProfile.ForeColor = Color.Black;
            lblProfile.Location = new Point(115, 175);
            lblProfile.Name = "lblProfile";
            lblProfile.RightToLeft = RightToLeft.No;
            lblProfile.Size = new Size(57, 20);
            lblProfile.TabIndex = 313;
            lblProfile.Text = "Profile:";
            // 
            // btnReset
            // 
            btnReset.Location = new Point(231, 376);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(183, 38);
            btnReset.TabIndex = 314;
            btnReset.Text = "Reset Settings";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // Frm_ServerSetting
            // 
            Appearance.BackColor = Color.FromArgb(240, 240, 240);
            Appearance.Options.UseBackColor = true;
            Appearance.Options.UseFont = true;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(659, 426);
            Controls.Add(btnDiag);
            Controls.Add(btnImport);
            Controls.Add(btnExport);
            Controls.Add(btnTestApi);
            Controls.Add(label17);
            Controls.Add(btnLogin);
            Controls.Add(label1);
            Controls.Add(chkSkipCert);
            Controls.Add(btnSave);
            Controls.Add(numTimeout);
            Controls.Add(txtBranchId);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(txtApiBase);
            Controls.Add(cboProfile);
            Controls.Add(lblProfile);
            Controls.Add(btnReset);
            Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Frm_ServerSetting";
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "إعدادت الشبكة";
            Load += Frm_ServerSetting_Load;
            ((System.ComponentModel.ISupportInitialize)numTimeout).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBranchId.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtApiBase.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtBranchId;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtApiBase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnTestApi;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.CheckBox chkSkipCert;
        private Label label17;
        private Button btnExport;
        private Button btnImport;
        private Button btnDiag;
        private ComboBox cboProfile;
        private Label lblProfile;
        private Button btnReset;
    }
}