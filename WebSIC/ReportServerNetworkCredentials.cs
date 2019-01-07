using System;
using System.Configuration;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.Net;

namespace WebSIC
{

    [Serializable]
    public sealed class ReportServerNetworkCredentials : IReportServerCredentials
    {
        #region IReportServerCredentials Members

        private string username, password;

        public ReportServerNetworkCredentials(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
        {
            //throw new NotImplementedException(); 
            userName = password = authority = null;

            // The cookie name is specified in the <forms> element in Web.config (.ASPXAUTH by default)
            HttpCookie cookie = HttpContext.Current.Request.Cookies[".ASPXAUTH"];

            if (cookie == null)
            {
                authCookie = null;
                return false;
            }

            Cookie netCookie = new Cookie(cookie.Name, cookie.Value);
            if (cookie.Domain == null)
            {
                netCookie.Domain = HttpContext.Current.Request.ServerVariables["s-cassaapp26"].ToUpper();
            }

            netCookie.Expires = cookie.Expires;
            netCookie.Path = cookie.Path;
            netCookie.Secure = cookie.Secure;
            authCookie = netCookie;
            return true;
        }

        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                return new System.Net.NetworkCredential(this.username, this.password);
            }
        }

        #endregion
    }
}
