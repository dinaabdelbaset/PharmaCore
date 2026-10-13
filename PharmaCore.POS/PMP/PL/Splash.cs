using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Splash : SplashScreen
    {

        public Splash()
        {
            InitializeComponent();
            //this.labelCopyright.Text = "Copyright ©2021-" + DateTime.Now.Year.ToString();
            //devexpress  اه لكت شغلها تاني يشغل عادي
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private void Splash_Load(object sender, EventArgs e)
        {

        }

        private void peImage_EditValueChanged(object sender, EventArgs e)
        {

        }

      
    }
}