using Kara.Framework.Common.Entity;
using Kara.Framework.Common.Enums;
using Kara.Framework.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace Kara.Framework.Common.Mvc.Security
{
    public static class FormAuthenticationHelper 
    {
        private static readonly HttpCookiesSection _httpCookieSection = (HttpCookiesSection)ConfigurationManager.GetSection("system.web/httpCookies");
        private static string userDataKeyValueDelimiter = ":";
        private static string userDataFieldDelimiter = "|";

        public static void Login(string userName, bool createPersistentCookie, List<CookieUserData> userData)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Invalid username", "userName");
            GenerateAuthenticationTicket(userName, createPersistentCookie, userData);
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Cookies.Clear();
            var expiredCoookie = CreateCookie(true);
            HttpContext.Current.Response.Cookies.Add(expiredCoookie);
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return;
        }

        private static HttpCookie CreateCookie(bool isExpired)
        {
            var expiredCoookie = new HttpCookie(FormsAuthentication.FormsCookieName)
            {
                HttpOnly = _httpCookieSection.HttpOnlyCookies,
                Secure = _httpCookieSection.RequireSSL,
                Domain = _httpCookieSection.Domain
            };
            if (isExpired)
                expiredCoookie.Expires = DateTime.Now.AddYears(-1);

            return expiredCoookie;
        }


        private static void GenerateAuthenticationTicket(string userName, bool persistentCookie, List<CookieUserData> userData)
        {
            var authCookie = FormsAuthentication.GetAuthCookie(userName, persistentCookie);
            var authenticationTicket = FormsAuthentication.Decrypt(authCookie.Value);           
            var ticket = new FormsAuthenticationTicket(authenticationTicket.Version, authenticationTicket.Name, 
                authenticationTicket.IssueDate, authenticationTicket.Expiration, authenticationTicket.IsPersistent,
                FormatCookieUserData(userData), authenticationTicket.CookiePath);
            authCookie.Value = FormsAuthentication.Encrypt(ticket);
            authCookie.Name = FormsAuthentication.FormsCookieName;
            authCookie.Secure = _httpCookieSection.RequireSSL;
            authCookie.HttpOnly = _httpCookieSection.HttpOnlyCookies;
            authCookie.Domain = _httpCookieSection.Domain;
            HttpContext.Current.Response.AppendCookie(authCookie);           
        }

        public static string FormatCookieUserData(List<CookieUserData> userDatas)
        {
            var userData = new StringBuilder();
            foreach (var item in userDatas)
                userData.AppendFormat("{0}{2}{1}{3}", item.Key, item.Value, userDataKeyValueDelimiter, userDataFieldDelimiter);

            return userData.ToString();
        }

        public static List<CookieUserData> GetUserData(HttpCookie cookie)
        {
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var userDatas = new List<CookieUserData>();
            var splittedUserData = ticket.UserData.Split(Char.Parse(userDataFieldDelimiter));
            for (int i = 0; i < splittedUserData.Length; i++)
            {
                var userData = splittedUserData[i];
                if (!string.IsNullOrWhiteSpace(userData))
                {
                    var keyValue = userData.Split(Char.Parse(userDataKeyValueDelimiter));
                    userDatas.Add(new CookieUserData() { Key = (CookieUserDataKeys)Enum.Parse(typeof(CookieUserDataKeys), keyValue[0], true), Value = keyValue[1] });
                }
            }
            return userDatas;
        }
       
    }
}
