using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Far8 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Far8()
        {
            InitializeComponent();
        }

        private void checkDf3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDf3.Checked == true)
            {
                btnEnter.Enabled = true;
            }
            if (checkDf3.Checked == false)
            {
                btnEnter.Enabled = false;
            }
        }

        private void Frm_Far8_Load(object sender, EventArgs e)
        {
            try
            {

                label2.Text = (Properties.Settings.Default.far8).ToString();

            }
            catch (Exception) { }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CheckDf3 = false;
            Properties.Settings.Default.Save();
            Close();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CheckDf3 = true;
            Properties.Settings.Default.Save();
            Close();
        }

        private void Frm_Far8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Properties.Settings.Default.CheckDf3 = true;
                Properties.Settings.Default.Save();
                Close();
            }
            else if (e.KeyCode == Keys.F12)
            {
                Properties.Settings.Default.CheckDf3 = false;
                Properties.Settings.Default.Save();
                Close();
            }

        }
    }
}