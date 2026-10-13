using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraTab;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace PharmaCore.POS.Helpers
{
    public static class ThemeHelper
    {
        private static readonly string ThemeConfigPath = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "PharmaCorePOS",
            "theme.cfg"
        );

        private static readonly System.Collections.Generic.HashSet<Form> ScaledForms = new System.Collections.Generic.HashSet<Form>();

        public static void InitializeGlobalTheme()
        {
            WindowsFormsSettings.ForceDirectXPaint();
            WindowsFormsSettings.SetDPIAware();

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();

            // Default to Light Mode for the Glassmorphism theme
            SetThemeMode(false);
        }

        public static void SetThemeMode(bool darkMode)
        {
            try
            {
                string dir = System.IO.Path.GetDirectoryName(ThemeConfigPath);
                if (!System.IO.Directory.Exists(dir)) System.IO.Directory.CreateDirectory(dir);
                System.IO.File.WriteAllText(ThemeConfigPath, darkMode ? "Dark" : "Light");
            }
            catch { }

            // Use WXI Compact Basic skin (light blue/grey tones)
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXICompact);
            UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXICompact, "Basic");
        }

        public static bool IsDarkMode()
        {
            return false; // Force Light/Glass Mode
        }

        public static void PaintGradientBackground(object sender, PaintEventArgs e)
        {
            if (sender is Form form)
            {
                try
                {
                    using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                        form.ClientRectangle,
                        Color.FromArgb(215, 228, 248),  // Light blue-indigo
                        Color.FromArgb(243, 229, 245),  // Soft pastel purple
                        45F))                            // 45 degrees angle
                    {
                        e.Graphics.FillRectangle(brush, form.ClientRectangle);
                    }
                }
                catch { }
            }
        }

        public static void PaintModernCard(object sender, PaintEventArgs e)
        {
            if (sender is GroupBox gb)
            {
                try
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    string title = gb.Tag as string ?? "";

                    int radius = 16; // Modern rounded corner radius
                    var rect = gb.ClientRectangle;
                    rect.Width -= 1;
                    rect.Height -= 1;

                    using (var path = GetRoundedRectanglePath(rect, radius))
                    {
                        // 1. Fill background with translucent frosted white (acrylic look)
                        using (var fillBrush = new SolidBrush(Color.FromArgb(115, 255, 255, 255)))
                        {
                            e.Graphics.FillPath(fillBrush, path);
                        }

                        // 2. Draw thin subtle white border
                        using (var pen = new Pen(Color.FromArgb(160, 255, 255, 255), 1.5f))
                        {
                            e.Graphics.DrawPath(pen, path);
                        }
                    }

                    // 3. Draw original text manually
                    if (!string.IsNullOrEmpty(title))
                    {
                        using (var titleFont = new Font("Segoe UI", 11.5F, FontStyle.Bold))
                        {
                            // Shift slightly to align beautifully in RTL layout
                            TextRenderer.DrawText(e.Graphics, title, titleFont,
                                new Rectangle(15, 12, gb.Width - 30, 28),
                                Color.FromArgb(30, 41, 59),
                                TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                        }
                    }
                }
                catch { }
            }
        }

        private static System.Drawing.Drawing2D.GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void ApplyModernTheme(Form form)
        {
            form.Paint -= PaintGradientBackground;
            form.Paint += PaintGradientBackground;
            form.Invalidate();

            lock (ScaledForms)
            {
                if (!ScaledForms.Contains(form))
                {
                    try
                    {
                        _ = new FormScaler(form);
                        ScaledForms.Add(form);
                        form.FormClosed += (s, e) => {
                            lock (ScaledForms)
                            {
                                ScaledForms.Remove(form);
                            }
                        };
                    }
                    catch { }
                }
            }

            StyleControls(form);
        }

        private static void StyleControls(Control parent)
        {
            try
            {
                System.IO.File.AppendAllText("theme_debug.log", $"StyleControls parent: {parent.Name} ({parent.GetType().FullName}), controls: {parent.Controls.Count}\n");
            }
            catch {}

            foreach (Control c in parent.Controls)
            {
                try
                {
                    System.IO.File.AppendAllText("theme_debug.log", $" - Child: {c.Name} ({c.GetType().FullName})\n");
                }
                catch {}

                // Clean Segoe UI font override
                if (c.Font != null && (c.Font.Name == "Tahoma" || c.Font.Name == "Microsoft Sans Serif" || c.Font.Name == "Simplified Arabic" || c.Font.Name == "Calibri"))
                {
                    c.Font = new Font("Segoe UI", c.Font.Size, c.Font.Style);
                }

                if (c is System.Windows.Forms.Label lbl)
                {
                    lbl.BackColor = Color.Transparent;
                    lbl.ForeColor = Color.FromArgb(30, 41, 59); // Slate dark text
                }
                else if (c is System.Windows.Forms.GroupBox gb)
                {
                    gb.FlatStyle = FlatStyle.Flat; // Turn off DevExpress skinning intercept
                    gb.BackColor = Color.Transparent;
                    gb.ForeColor = Color.FromArgb(30, 41, 59);
                    if (gb.Tag == null && !string.IsNullOrEmpty(gb.Text))
                    {
                        gb.Tag = gb.Text;
                        gb.Text = "";
                    }
                    if (!(gb is ModernGroupBox))
                    {
                        gb.Paint -= PaintModernCard;
                        gb.Paint += PaintModernCard;
                    }
                }
                else if (c is System.Windows.Forms.Panel pnl)
                {
                    if (pnl.Name != "panelAi" && pnl.Name != "panelChatContainer" && pnl.Name != "panelCustomizer" && pnl.Name != "panelQuickActions" && pnl.Name != "gradientPanel")
                    {
                        pnl.BackColor = Color.FromArgb(115, 255, 255, 255);
                        pnl.ForeColor = Color.FromArgb(30, 41, 59);
                    }
                }
                else if (c.GetType().Name.Contains("Chart"))
                {
                    c.BackColor = Color.Transparent;
                }
                else if (c is DataGridView dgv)
                {
                    StyleDataGridView(dgv);
                }
                else if (c is System.Windows.Forms.TextBox tb)
                {
                    tb.BackColor = Color.White;
                    tb.ForeColor = Color.Black;
                    tb.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (c is System.Windows.Forms.Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = Color.FromArgb(14, 165, 233); // Light Blue Sky accent color
                    btn.ForeColor = Color.White;
                    btn.Font = new Font("Segoe UI", btn.Font.Size, FontStyle.Bold);
                }
                else if (c is System.Windows.Forms.CheckBox chk)
                {
                    chk.ForeColor = Color.FromArgb(30, 41, 59);
                    chk.BackColor = Color.Transparent;
                }
                else if (c is System.Windows.Forms.RadioButton rdb)
                {
                    rdb.ForeColor = Color.FromArgb(30, 41, 59);
                    rdb.BackColor = Color.Transparent;
                }
                else if (c is System.Windows.Forms.ComboBox cb)
                {
                    cb.BackColor = Color.White;
                    cb.ForeColor = Color.Black;
                }
                else if (c is System.Windows.Forms.TabPage tp)
                {
                    tp.BackColor = Color.FromArgb(248, 250, 252);
                    tp.ForeColor = Color.FromArgb(30, 41, 59);
                }

                StyleControls(c);
            }
        }

        public static void StyleDataGridView(DataGridView dgv)
        {
            typeof(DataGridView)
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(dgv, true, null);

            Color backColor = Color.White;
            Color altBackColor = Color.FromArgb(248, 250, 252);
            Color foreColor = Color.FromArgb(30, 41, 59);
            Color gridColor = Color.FromArgb(226, 232, 240);
            Color headerBackColor = Color.FromArgb(241, 245, 249);
            Color headerForeColor = Color.FromArgb(71, 85, 105);
            Color selectionBackColor = Color.FromArgb(224, 242, 254);
            Color selectionForeColor = Color.FromArgb(3, 105, 161);

            dgv.BackgroundColor = Color.FromArgb(245, 248, 253); // Blends with the frosted glass cards
            dgv.GridColor = gridColor;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            
            dgv.RowTemplate.Height = 40;
            dgv.ColumnHeadersHeight = 42;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.DefaultCellStyle.BackColor = backColor;
            dgv.DefaultCellStyle.ForeColor = foreColor;
            dgv.DefaultCellStyle.SelectionBackColor = selectionBackColor;
            dgv.DefaultCellStyle.SelectionForeColor = selectionForeColor;
            
            dgv.AlternatingRowsDefaultCellStyle.BackColor = altBackColor;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = foreColor;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = selectionBackColor;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = selectionForeColor;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = headerBackColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = headerForeColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = headerForeColor;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToResizeRows = false;
        }
    }

    public class ModernGroupBox : GroupBox
    {
        public ModernGroupBox()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | 
                           ControlStyles.AllPaintingInWmPaint | 
                           ControlStyles.ResizeRedraw | 
                           ControlStyles.UserPaint | 
                           ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.FlatStyle = FlatStyle.Flat;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Do not call base.OnPaint(e) to completely disable native drawing
            if (this.Tag == null && !string.IsNullOrEmpty(this.Text))
            {
                this.Tag = this.Text;
                this.Text = "";
            }
            ThemeHelper.PaintModernCard(this, e);
        }
    }
}
