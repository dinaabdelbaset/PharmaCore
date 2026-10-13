using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCore.POS.Helpers
{
    public static class AppConfig
    {
        public static int LoginMethod => Properties.Settings.Default.LoginMethod;
        public static string ApiBase => Properties.Settings.Default.ApiBase;
        public static string BranchId => Properties.Settings.Default.BranchId;
        public static string ApiUser => Properties.Settings.Default.ApiUser;
        public static string ApiPass => Properties.Settings.Default.ApiPass;
        public static string ApiToken => Properties.Settings.Default.ApiToken;
        public static void Save() => Properties.Settings.Default.Save();
    }
}

