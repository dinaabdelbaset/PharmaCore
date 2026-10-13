using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PharmaCore.POS
{
    public partial class Form1
    {
        private Panel panelCustomizer;
        private Label lblNotificationBanner;
        private float currentFontScale = 1.0f;

        private void InitializeUiCustomizer()
        {
            // 1. Floating Banner Notification Label at the Top of Form1
            lblNotificationBanner = new Label();
            lblNotificationBanner.Dock = DockStyle.Top;
            lblNotificationBanner.Height = 35;
            lblNotificationBanner.BackColor = Color.FromArgb(220, 53, 69); // Red alert color
            lblNotificationBanner.ForeColor = Color.White;
            lblNotificationBanner.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblNotificationBanner.TextAlign = ContentAlignment.MiddleCenter;
            lblNotificationBanner.Text = "🔔 تنبيه: يوجد بعض النواقص والأدوية التي تقترب صلاحيتها على الانتهاء! يرجى مراجعة مؤشرات صحة الصيدلية.";
            lblNotificationBanner.Visible = true;
            
            // Add banner to Form1 controls
            this.Controls.Add(lblNotificationBanner);
            lblNotificationBanner.BringToFront();

            // 2. Initialize UI Customizer panel inside AI panelChatContainer
            panelCustomizer = new Panel();
            panelCustomizer.Dock = DockStyle.Top;
            panelCustomizer.Height = 70;
            panelCustomizer.BackColor = Color.FromArgb(35, 35, 38);
            panelCustomizer.BorderStyle = BorderStyle.FixedSingle;

            // Customizer Title Label
            Label lblThemeTitle = new Label();
            lblThemeTitle.Text = "🎨 تخصيص المظهر والخطوط:";
            lblThemeTitle.ForeColor = Color.LightGray;
            lblThemeTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblThemeTitle.Location = new Point(120, 5);
            lblThemeTitle.Size = new Size(180, 20);
            lblThemeTitle.RightToLeft = RightToLeft.Yes;
            panelCustomizer.Controls.Add(lblThemeTitle);

            // Font Increase (A+) Button
            SimpleButton btnFontInc = new SimpleButton();
            btnFontInc.Text = "A+";
            btnFontInc.Size = new Size(35, 25);
            btnFontInc.Location = new Point(10, 32);
            btnFontInc.Click += (s, e) => ChangeGlobalFontScale(1.1f);
            panelCustomizer.Controls.Add(btnFontInc);

            // Font Decrease (A-) Button
            SimpleButton btnFontDec = new SimpleButton();
            btnFontDec.Text = "A-";
            btnFontDec.Size = new Size(35, 25);
            btnFontDec.Location = new Point(50, 32);
            btnFontDec.Click += (s, e) => ChangeGlobalFontScale(0.9f);
            panelCustomizer.Controls.Add(btnFontDec);

            // Color preset buttons:
            // Preset 1: Dark Mode
            SimpleButton btnThemeDark = new SimpleButton();
            btnThemeDark.Text = "داكن";
            btnThemeDark.Size = new Size(50, 25);
            btnThemeDark.Location = new Point(95, 32);
            btnThemeDark.Click += (s, e) => ApplyColorPreset("Dark");
            panelCustomizer.Controls.Add(btnThemeDark);

            // Preset 2: Blue Theme
            SimpleButton btnThemeBlue = new SimpleButton();
            btnThemeBlue.Text = "أزرق";
            btnThemeBlue.Size = new Size(50, 25);
            btnThemeBlue.Location = new Point(150, 32);
            btnThemeBlue.Click += (s, e) => ApplyColorPreset("Blue");
            panelCustomizer.Controls.Add(btnThemeBlue);

            // Preset 3: Green Theme
            SimpleButton btnThemeGreen = new SimpleButton();
            btnThemeGreen.Text = "أخضر";
            btnThemeGreen.Size = new Size(50, 25);
            btnThemeGreen.Location = new Point(205, 32);
            btnThemeGreen.Click += (s, e) => ApplyColorPreset("Green");
            panelCustomizer.Controls.Add(btnThemeGreen);

            // Add panelCustomizer at the top of panelChatContainer
            if (panelChatContainer != null)
            {
                panelChatContainer.Controls.Add(panelCustomizer);
                panelCustomizer.BringToFront();
            }
        }

        private void ChangeGlobalFontScale(float factor)
        {
            currentFontScale *= factor;
            if (currentFontScale < 0.7f) currentFontScale = 0.7f;
            if (currentFontScale > 1.6f) currentFontScale = 1.6f;

            // Update fonts recursively
            ScaleFontsRecursive(this, factor);
        }

        private void ScaleFontsRecursive(Control parent, float factor)
        {
            try
            {
                if (parent.Font != null)
                {
                    parent.Font = new Font(parent.Font.FontFamily, parent.Font.Size * factor, parent.Font.Style);
                }
            }
            catch { }

            foreach (Control child in parent.Controls)
            {
                ScaleFontsRecursive(child, factor);
            }
        }

        private void ApplyColorPreset(string theme)
        {
            Color primaryBg = Color.FromArgb(32, 32, 35);
            Color textCol = Color.White;
            Color accentColor = Color.DodgerBlue;

            if (theme == "Blue")
            {
                primaryBg = Color.FromArgb(15, 32, 67);
                accentColor = Color.FromArgb(41, 128, 185);
            }
            else if (theme == "Green")
            {
                primaryBg = Color.FromArgb(20, 47, 30);
                accentColor = Color.FromArgb(39, 174, 96);
            }

            // Set main background colors
            this.BackColor = primaryBg;
            groupBox5.BackColor = primaryBg;
            
            if (panelAi != null)
            {
                panelAi.BackColor = primaryBg;
            }
            if (panelCustomizer != null)
            {
                panelCustomizer.BackColor = Color.FromArgb(primaryBg.R + 10, primaryBg.G + 10, primaryBg.B + 10);
            }

            MessageBox.Show($"تم تغيير مظهر الصيدلية إلى اللون الـ {theme} بنجاح!", "تأكيد المظهر", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
