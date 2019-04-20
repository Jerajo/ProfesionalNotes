using System;
using PN.Models;
using System.Web;
using PN.Services;
using System.Web.Mvc;

namespace PN.Controllers
{
    public class BaseController : Controller
    {
        #region SETTERS AND GETTERS

        protected AppDbContext db { get; set; } = new AppDbContext();

        protected LanguagesService Service { get; set; } = new LanguagesService();

        #endregion

        #region Methods

        public ActionResult PageNotFound()
        {
            return View("PageNotFound");
        }

        public ActionResult Policies()
        {
            return View("Policies");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
                if (Service != null)
                {
                    Service.Dispose();
                    Service = null;
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Auxiliary Methods

        public bool IsFirstInit(LanguagesService service = default)
        {
            try
            {
                if (CookieExits("Language")) return false;
                else
                {
                    SaveCookie("Language", Service.Language);
                    SaveCookie("Country", Service.Region);
                    SaveCookie("SelectedTheme", "Dark");
                    return true;
                }
            }
            catch (Exception ex) { throw new Exception($"Error al crear los cookies. \n{ex.Message}"); }
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index");
        }

        protected string GetIPAddress()
        {
            var context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        protected void SaveCookie(string cookieName, object cookieValue, DateTime? expiration = null)
        {
            if (CookieExits(cookieName)) UpdateCookie(cookieName, cookieValue, expiration);
            
            // Create Cookie
            var AppCookies = new HttpCookie(cookieName, cookieValue.ToString());

            // Add presistance for 50 Years
            var date = expiration ?? DateTime.Now.AddYears(50);
            AppCookies.Expires = date;

            // Save Cookie
            Response.Cookies.Add(AppCookies);
        }

        protected void UpdateCookie(string cookieName, object cookieValue, DateTime? expiration = null)
        {
            var AppCookies = Request.Cookies[cookieName];
            AppCookies.Value = cookieValue.ToString();

            var date = expiration ?? DateTime.Now.AddYears(50);
            AppCookies.Expires = date;

            Response.Cookies.Add(AppCookies);
        }

        protected bool CookieExits(string cookieName) => (Request.Cookies[cookieName] != null);

        #endregion
    }
}