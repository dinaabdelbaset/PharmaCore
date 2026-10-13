using System;
using System.Data;
using System.Windows.Forms;
using PharmaCore.POS.Enums;

namespace PharmaCore.POS.BL
{
    internal class DDatabase : IDisposable
    {
        // Legacy SQL removed; class retained to avoid breaking form code references.
        public DDatabase() { }
        public void con() { }
        public DataTable readData(string stmt, string message)
        {
            // Return empty table; legacy SQL not supported.
            if (!string.IsNullOrEmpty(message)) MessageBox.Show("تم تعطيل محرك SQL القديم", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return new DataTable();
        }
        public bool exceuteData(string stmt, string message)
        {
            if (!string.IsNullOrEmpty(message)) MessageBox.Show("تم تعطيل محرك SQL القديم", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        public void open() { }
        public void close() { }
        public void Dispose() { }
    }
}
