using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCore.POS.Enums
{
    public enum ConnectionMode
    {
        Local = 0, // Windows Auth
        Network = 1, // SQL Auth (LAN)
        Online = 2, // SQL عبر IP:Port
        Hosting = 3, // ConnStr جاهز
        Api = 4  // عبر Web API
    }
}