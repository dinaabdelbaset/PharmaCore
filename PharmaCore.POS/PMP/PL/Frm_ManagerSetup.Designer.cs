namespace PharmaCore.POS.PL
{
    partial class Frm_ManagerSetup
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ── Panel ───────────────────────────────────────────
            var panelMain = new System.Windows.Forms.Panel
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                BackColor = System.Drawing.Color.FromArgb(30, 30, 46),
                Padding = new System.Windows.Forms.Padding(40)
            };

            // ── Title ───────────────────────────────────────────
            var lblTitle = new System.Windows.Forms.Label
            {
                Text = "إعداد مدير الصيدلية",
                Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(205, 214, 244),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = System.Windows.Forms.DockStyle.Top,
                Height = 60,
                RightToLeft = System.Windows.Forms.RightToLeft.Yes
            };

            var lblSubtitle = new System.Windows.Forms.Label
            {
                Text = "هذه هي المرة الأولى — يرجى تسجيل حساب المدير",
                Font = new System.Drawing.Font("Segoe UI", 11F),
                ForeColor = System.Drawing.Color.FromArgb(166, 173, 200),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = System.Windows.Forms.DockStyle.Top,
                Height = 35,
                RightToLeft = System.Windows.Forms.RightToLeft.Yes
            };

            // ── Form Panel ──────────────────────────────────────
            var formPanel = new System.Windows.Forms.Panel
            {
                Width = 420,
                Height = 400,
                BackColor = System.Drawing.Color.FromArgb(45, 45, 65),
                Anchor = System.Windows.Forms.AnchorStyles.None
            };
            formPanel.Paint += (s, e) =>
            {
                var rect = new System.Drawing.Rectangle(0, 0, formPanel.Width - 1, formPanel.Height - 1);
                using var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(80, 80, 120), 1);
                var radius = 12;
                using var path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            };

            int y = 20;
            var lblFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            var txtFont = new System.Drawing.Font("Segoe UI", 11F);
            var lblColor = System.Drawing.Color.FromArgb(205, 214, 244);
            var txtBg = System.Drawing.Color.FromArgb(55, 55, 80);
            var txtFg = System.Drawing.Color.White;

            // Full Name
            var lblName = new System.Windows.Forms.Label { Text = "الاسم بالكامل", Font = lblFont, ForeColor = lblColor, Location = new System.Drawing.Point(20, y), Size = new System.Drawing.Size(380, 22), RightToLeft = System.Windows.Forms.RightToLeft.Yes, TextAlign = System.Drawing.ContentAlignment.MiddleRight };
            y += 24;
            txtFullName = new System.Windows.Forms.TextBox { Font = txtFont, BackColor = txtBg, ForeColor = txtFg, BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle, Location = new System.Drawing.Point(20, y), Size = new System.Drawing.Size(380, 30), RightToLeft = System.Windows.Forms.RightToLeft.Yes };
            y += 40;

            // Employee Code Digit Count
            var lblCode = new System.Windows.Forms.Label { Text = "عدد خانات كود الموظف", Font = lblFont, ForeColor = lblColor, Location = new System.Drawing.Point(20, y), Size = new System.Drawing.Size(380, 22), RightToLeft = System.Windows.Forms.RightToLeft.Yes, TextAlign = System.Drawing.ContentAlignment.MiddleRight };
            y += 24;
            nudDigits = new System.Windows.Forms.NumericUpDown { Font = txtFont, BackColor = txtBg, ForeColor = txtFg, Minimum = 2, Maximum = 7, Value = 4, Location = new System.Drawing.Point(20, y), Size = new System.Drawing.Size(380, 30), TextAlign = System.Windows.Forms.HorizontalAlignment.Center, BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle };
            y += 32;
            lblCodePreview = new System.Windows.Forms.Label
            {
                Font = new System.Drawing.Font("Segoe UI", 9F),
                ForeColor = System.Drawing.Color.FromArgb(137, 180, 250),
                Location = new System.Drawing.Point(20, y),
                Size = new System.Drawing.Size(380, 20),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            UpdateCodePreview();
            nudDigits.ValueChanged += (_, __) => UpdateCodePreview();
            y += 28;

            // PIN
            var lblPin = new System.Windows.Forms.Label { Text = "الرقم السري (PIN)", Font = lblFont, ForeColor = lblColor, Location = new System.Drawing.Point(20, y), Size = new System.Drawing.Size(380, 22), RightToLeft = System.Windows.Forms.RightToLeft.Yes, TextAlign = System.Drawing.ContentAlignment.MiddleRight };
            y += 24;
            txtPin = new System.Windows.Forms.TextBox { Font = txtFont, BackColor = txtBg, ForeColor = txtFg, BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle, UseSystemPasswordChar = true, Location = new System.Drawing.Point(20, y), Size = new System.Drawing.Size(380, 30), RightToLeft = System.Windows.Forms.RightToLeft.Yes };
            y += 40;

            // Confirm PIN
            var lblConfirm = new System.Windows.Forms.Label { Text = "تأكيد الرقم السري", Font = lblFont, ForeColor = lblColor, Location = new System.Drawing.Point(20, y), Size = new System.Drawing.Size(380, 22), RightToLeft = System.Windows.Forms.RightToLeft.Yes, TextAlign = System.Drawing.ContentAlignment.MiddleRight };
            y += 24;
            txtConfirmPin = new System.Windows.Forms.TextBox { Font = txtFont, BackColor = txtBg, ForeColor = txtFg, BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle, UseSystemPasswordChar = true, Location = new System.Drawing.Point(20, y), Size = new System.Drawing.Size(380, 30), RightToLeft = System.Windows.Forms.RightToLeft.Yes };
            y += 45;

            // Register Button
            btnRegister = new System.Windows.Forms.Button
            {
                Text = "تسجيل المدير",
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(137, 180, 250),
                ForeColor = System.Drawing.Color.FromArgb(30, 30, 46),
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Location = new System.Drawing.Point(20, y),
                Size = new System.Drawing.Size(380, 42)
            };
            btnRegister.FlatAppearance.BorderSize = 0;

            // Status label
            lblStatus = new System.Windows.Forms.Label
            {
                Text = "",
                Font = new System.Drawing.Font("Segoe UI", 9F),
                ForeColor = System.Drawing.Color.FromArgb(243, 139, 168),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Location = new System.Drawing.Point(20, y + 48),
                Size = new System.Drawing.Size(380, 25),
                RightToLeft = System.Windows.Forms.RightToLeft.Yes
            };

            formPanel.Controls.AddRange(new System.Windows.Forms.Control[] { lblName, txtFullName, lblCode, nudDigits, lblCodePreview, lblPin, txtPin, lblConfirm, txtConfirmPin, btnRegister, lblStatus });

            // Center the form panel
            var centerPanel = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Fill };
            centerPanel.Controls.Add(formPanel);
            centerPanel.Resize += (s, e) =>
            {
                formPanel.Left = (centerPanel.Width - formPanel.Width) / 2;
                formPanel.Top = (centerPanel.Height - formPanel.Height) / 2;
            };

            panelMain.Controls.Add(centerPanel);
            panelMain.Controls.Add(lblSubtitle);
            panelMain.Controls.Add(lblTitle);
            this.Controls.Add(panelMain);

            // ── Form Settings ─────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 560);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إعداد مدير الصيدلية";
            this.Name = "Frm_ManagerSetup";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.NumericUpDown nudDigits;
        private System.Windows.Forms.Label lblCodePreview;
        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.TextBox txtConfirmPin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblStatus;
    }
}
