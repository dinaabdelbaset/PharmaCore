using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PharmaCore.POS.Helpers
{
    public class FormScaler
    {
        private Form _form;
        private List<Control> _excludedControls = new List<Control>();

        private Size _originalFormSize;
        private Dictionary<Control, Rectangle> _originalBounds = new Dictionary<Control, Rectangle>();
        private Dictionary<Control, float> _originalFonts = new Dictionary<Control, float>();
        
        // DataGridView specifics
        private Dictionary<DataGridView, int> _originalDgvRowHeights = new Dictionary<DataGridView, int>();
        private Dictionary<DataGridView, int> _originalDgvColHeaderHeights = new Dictionary<DataGridView, int>();
        private Dictionary<DataGridViewColumn, int> _originalDgvColWidths = new Dictionary<DataGridViewColumn, int>();

        private bool _isInitialized = false;

        public FormScaler(Form form, params Control[] excludedControls)
        {
            _form = form;
            if (excludedControls != null)
                _excludedControls.AddRange(excludedControls);

            if (_form.IsHandleCreated)
            {
                InitializeLayout();
            }
            else
            {
                _form.HandleCreated += (s, e) => InitializeLayout();
            }

            _form.Resize += Form_Resize;
        }

        private void InitializeLayout()
        {
            if (_isInitialized) return;
            _isInitialized = true;
            _originalFormSize = _form.ClientSize;
            SaveOriginalLayout(_form);
        }

        private void SaveOriginalLayout(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (_excludedControls.Contains(parent) || _excludedControls.Contains(c)) continue;

                // Save bounds only if the control itself is not docked (WinForms handles docked layouts automatically)
                if (c.Dock == DockStyle.None)
                {
                    _originalBounds[c] = c.Bounds;
                    // Prevent WinForms anchors from fighting manual math scaling
                    c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                }

                _originalFonts[c] = c.Font.Size;

                if (c is DataGridView dgv)
                {
                    _originalDgvRowHeights[dgv] = dgv.RowTemplate.Height;
                    _originalDgvColHeaderHeights[dgv] = dgv.ColumnHeadersHeight;
                    
                    if (dgv.ColumnHeadersHeightSizeMode != DataGridViewColumnHeadersHeightSizeMode.AutoSize)
                        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (col.AutoSizeMode == DataGridViewAutoSizeColumnMode.NotSet || 
                            col.AutoSizeMode == DataGridViewAutoSizeColumnMode.None ||
                            col.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill)
                        {
                            _originalDgvColWidths[col] = col.Width;
                        }
                    }
                }

                SaveOriginalLayout(c);
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            if (!_isInitialized || _originalFormSize.Width == 0 || _form.WindowState == FormWindowState.Minimized) return;

            float globalScaleX = (float)_form.ClientSize.Width / _originalFormSize.Width;
            float globalScaleY = (float)_form.ClientSize.Height / _originalFormSize.Height;
            
            _form.SuspendLayout();
            ApplyScale(_form, globalScaleX, globalScaleY);
            _form.ResumeLayout();
        }

        private void GetScaleFactorsForControl(Control c, float globalScaleX, float globalScaleY, out float scaleX, out float scaleY)
        {
            scaleX = globalScaleX;
            scaleY = globalScaleY;

            Control current = c.Parent;
            while (current != null && current != _form)
            {
                if (current.Dock == DockStyle.Top || current.Dock == DockStyle.Bottom)
                {
                    scaleY = 1.0f; // Height should maintain proportions for Top/Bottom docked containers
                }
                else if (current.Dock == DockStyle.Left || current.Dock == DockStyle.Right)
                {
                    scaleX = 1.0f; // Width should maintain proportions for Left/Right docked containers
                }
                current = current.Parent;
            }
        }

        private void ApplyScale(Control parent, float globalScaleX, float globalScaleY)
        {
            foreach (Control c in parent.Controls)
            {
                if (_excludedControls.Contains(parent) || _excludedControls.Contains(c)) continue;

                GetScaleFactorsForControl(c, globalScaleX, globalScaleY, out float scaleX, out float scaleY);

                if (c.Dock == DockStyle.None && _originalBounds.TryGetValue(c, out Rectangle ob))
                {
                    c.Bounds = new Rectangle(
                        (int)(ob.X * scaleX),
                        (int)(ob.Y * scaleY),
                        Math.Max(1, (int)(ob.Width * scaleX)),
                        Math.Max(1, (int)(ob.Height * scaleY))
                    );
                }

                // Proportional font scaling using the minimum scale factor
                if (_originalFonts.TryGetValue(c, out float origFont))
                {
                    float newFontSize = origFont * Math.Min(scaleX, scaleY);
                    if (newFontSize > 4 && newFontSize < 72)
                    {
                        c.Font = new Font(c.Font.FontFamily, newFontSize, c.Font.Style);
                    }
                }

                // DataGridView scaling
                if (c is DataGridView dgv)
                {
                    if (_originalDgvRowHeights.TryGetValue(dgv, out int rowH))
                        dgv.RowTemplate.Height = Math.Max(1, (int)(rowH * scaleY));
                        
                    if (_originalDgvColHeaderHeights.TryGetValue(dgv, out int colH))
                        dgv.ColumnHeadersHeight = Math.Max(1, (int)(colH * scaleY));

                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (_originalDgvColWidths.TryGetValue(col, out int w))
                        {
                            col.Width = Math.Max(1, (int)(w * scaleX));
                        }
                    }
                    
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (_originalDgvRowHeights.TryGetValue(dgv, out int rh))
                        {
                            row.Height = Math.Max(1, (int)(rh * scaleY));
                        }
                    }
                }

                ApplyScale(c, globalScaleX, globalScaleY);
            }
        }
    }
}
