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
    public partial class Frm_ShowDetils : DevExpress.XtraEditors.XtraForm
    {
        public Frm_ShowDetils()
        {
            InitializeComponent();
        }

        private void Frm_ShowDetils_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DgvDteils.DataSource != null)
            {
                this.DgvDteils.DataSource = null;
            }
            else
            {
                this.DgvDteils.Rows.Clear();
            }
        }

        private void Frm_ShowDetils_Load(object sender, EventArgs e)
        {

        }
    }
}