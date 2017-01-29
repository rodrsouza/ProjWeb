using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASPMVCShoppingCart.Models
{
    public class mLogin
    {
        public string User { get; set; }

        public string Password { get; set; }

        private static bool loginOk { get; set; }

        private static long loginTime { get; set; }

        public void setLoginOk ()
        {
            loginOk = true;
            loginTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public static bool IsLogged ()
        {
            long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            long elapsedLoginTime = now - loginTime;

            loginOk = loginOk && (elapsedLoginTime < 5 * 60 * 1000);

            return loginOk;
        }
    }
}
