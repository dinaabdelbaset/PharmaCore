using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PharmaCore.POS
{
    public partial class Form1
    {
        private Panel panelAi;
        private Panel panelChatContainer;
        private RichTextBox txtAiChat;
        private TextBox txtAiInput;
        private SimpleButton btnAiSend;
        private SimpleButton btnAiToggle;
        private FlowLayoutPanel panelQuickActions;
        private bool isAiExpanded = false;

        private class AiQueryResponse
        {
            public string Response { get; set; } = string.Empty;
            public List<string> Suggestions { get; set; } = new();
        }

        private void InitializeAiAssistant()
        {
            // Main AI Panel (Docked to Left)
            panelAi = new Panel();
            panelAi.Dock = DockStyle.Left;
            panelAi.Width = 45; // Start collapsed
            panelAi.BackColor = Color.FromArgb(30, 30, 30);
            panelAi.BorderStyle = BorderStyle.FixedSingle;

            // Toggle Button (Visible when collapsed / expanded)
            btnAiToggle = new SimpleButton();
            btnAiToggle.Text = "🤖";
            btnAiToggle.Size = new Size(43, 40);
            btnAiToggle.Location = new Point(0, 0);
            btnAiToggle.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnAiToggle.Click += BtnAiToggle_Click;
            btnAiToggle.ToolTip = "اسأل المساعد الذكي";
            btnAiToggle.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnAiToggle.Appearance.ForeColor = Color.DodgerBlue;
            panelAi.Controls.Add(btnAiToggle);

            // Chat Container (Visible when expanded)
            panelChatContainer = new Panel();
            panelChatContainer.Location = new Point(0, 45);
            panelChatContainer.Size = new Size(318, 550);
            panelChatContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelChatContainer.Visible = false;

            // Chat Display (RichTextBox)
            txtAiChat = new RichTextBox();
            txtAiChat.Dock = DockStyle.Fill;
            txtAiChat.ReadOnly = true;
            txtAiChat.BackColor = Color.FromArgb(20, 20, 20);
            txtAiChat.ForeColor = Color.LightGray;
            txtAiChat.BorderStyle = BorderStyle.None;
            txtAiChat.Font = new Font("Segoe UI", 10F);
            txtAiChat.RightToLeft = RightToLeft.Yes;
            panelChatContainer.Controls.Add(txtAiChat);

            // Quick Actions Panel
            panelQuickActions = new FlowLayoutPanel();
            panelQuickActions.Dock = DockStyle.Bottom;
            panelQuickActions.Height = 110;
            panelQuickActions.BackColor = Color.FromArgb(25, 25, 25);
            panelQuickActions.FlowDirection = FlowDirection.RightToLeft;
            panelQuickActions.Padding = new Padding(5);

            string[] quickPrompts = { "ملخص اليوم", "طلب بضاعة", "الأكثر مبيعاً", "الأدوية الراكدة", "الأدوية المنتهية" };
            foreach (var prompt in quickPrompts)
            {
                var btnAction = new SimpleButton();
                btnAction.Text = prompt;
                btnAction.Size = new Size(95, 30);
                btnAction.Appearance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btnAction.Click += (s, e) => SendAiPromptAsync(prompt);
                panelQuickActions.Controls.Add(btnAction);
            }
            panelChatContainer.Controls.Add(panelQuickActions);

            // Input Panel (TextBox + Send Button)
            Panel panelInput = new Panel();
            panelInput.Dock = DockStyle.Bottom;
            panelInput.Height = 60;
            panelInput.BackColor = Color.FromArgb(30, 30, 30);

            txtAiInput = new TextBox();
            txtAiInput.Multiline = true;
            txtAiInput.Size = new Size(245, 50);
            txtAiInput.Location = new Point(65, 5);
            txtAiInput.BackColor = Color.FromArgb(40, 40, 40);
            txtAiInput.ForeColor = Color.White;
            txtAiInput.BorderStyle = BorderStyle.FixedSingle;
            txtAiInput.Font = new Font("Segoe UI", 10F);
            txtAiInput.RightToLeft = RightToLeft.Yes;
            txtAiInput.KeyPress += TxtAiInput_KeyPress;
            panelInput.Controls.Add(txtAiInput);

            btnAiSend = new SimpleButton();
            btnAiSend.Text = "إرسال";
            btnAiSend.Size = new Size(55, 50);
            btnAiSend.Location = new Point(5, 5);
            btnAiSend.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAiSend.Click += BtnAiSend_Click;
            panelInput.Controls.Add(btnAiSend);

            panelChatContainer.Controls.Add(panelInput);
            panelAi.Controls.Add(panelChatContainer);

            // Add AI panel to Form1
            this.Controls.Add(panelAi);
            panelAi.BringToFront();

            // Print welcome message
            txtAiChat.AppendText("🤖 المساعد الذكي:\nأهلاً بك! أنا مساعدك الذكي المعتمد على بيانات الصيدلية.\nيمكنك كتابة أي سؤال أو الاختيار من الاقتراحات بالأسفل لمساعدتك.\n\n");
        }

        private void BtnAiToggle_Click(object sender, EventArgs e)
        {
            if (isAiExpanded)
            {
                panelAi.Width = 45;
                panelChatContainer.Visible = false;
                btnAiToggle.Text = "🤖";
                isAiExpanded = false;
            }
            else
            {
                panelAi.Width = 320;
                panelChatContainer.Visible = true;
                btnAiToggle.Text = "◀";
                isAiExpanded = true;
                txtAiInput.Focus();
            }
        }

        private void TxtAiInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                BtnAiSend_Click(sender, e);
            }
        }

        private async void BtnAiSend_Click(object sender, EventArgs e)
        {
            var prompt = txtAiInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(prompt)) return;

            txtAiInput.Clear();
            await SendAiPromptAsync(prompt);
        }

        private async Task SendAiPromptAsync(string prompt)
        {
            txtAiChat.SelectionColor = Color.LightSkyBlue;
            txtAiChat.AppendText($"👤 أنت:\n{prompt}\n\n");
            txtAiChat.ScrollToCaret();

            try
            {
                using (var http = Services.ConnectionManager.GetHttpClient())
                {
                    var req = new { prompt = prompt };
                    var response = await http.PostAsJsonAsync("/api/ai/query", req);
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadFromJsonAsync<AiQueryResponse>();
                        if (res != null)
                        {
                            txtAiChat.SelectionColor = Color.PaleGreen;
                            txtAiChat.AppendText($"🤖 المساعد الذكي:\n{res.Response}\n\n");
                        }
                    }
                    else
                    {
                        txtAiChat.SelectionColor = Color.OrangeRed;
                        txtAiChat.AppendText("🤖 المساعد الذكي:\nعذراً، فشل الاتصال بالسيرفر الذكي.\n\n");
                    }
                }
            }
            catch (Exception ex)
            {
                txtAiChat.SelectionColor = Color.OrangeRed;
                txtAiChat.AppendText($"🤖 المساعد الذكي:\nخطأ أثناء الاتصال: {ex.Message}\n\n");
            }
            txtAiChat.ScrollToCaret();
        }
    }
}
