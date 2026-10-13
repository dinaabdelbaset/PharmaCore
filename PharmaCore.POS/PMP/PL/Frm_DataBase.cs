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
    public partial class Frm_DataBase : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DataBase()
        {
            InitializeComponent();
        }

        string x = Properties.Settings.Default.CheckPass;

        private void Frm_DataBase_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {

                MessageBox.Show("من فضلك أدخل كلمة المرور", "تأكيد");
                return;
            }
            if (textBox1.Text == x)
            {

                if (Properties.Settings.Default.Open_Form == 14)
                {
                    Frm_ServerSetting frm = new Frm_ServerSetting();
                    frm.ShowDialog();
                }
                
                Close();
            }
            else
            {
                MessageBox.Show("كلمة المرور خاطئة", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1_Click(null, null);
            }
        }
    }
}