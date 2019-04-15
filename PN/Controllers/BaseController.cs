using System;
using PN.Models;
using System.Web;
using PN.Services;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace PN.Controllers
{
    public class BaseController : Controller
    {
        #region SETTERS AND GETTERS

        protected AppDbContext db { get; set; } = new AppDbContext();
        public bool IsLoading { get; protected set; }
        public bool FirstInit { get; protected set; }

        #endregion

        public ActionResult PageNotFound()
        {
            return View("PageNotFound");
        }

        public ActionResult Policies()
        {
            return View("Policies");
        }

        #region Auxiliary Methods

        public async Task<bool> Loading()
        {
            IsLoading = true;
            using (var service = new LanguagesService())
            {
                FirstInit = await IsFirstInit(service);
            }
            return false;
        }

        public Task<bool> IsFirstInit(LanguagesService service = default)
        {
            try
            {
                if (Request.Cookies["cookiesPN"] != null) return Task.FromResult(false);
                else
                {
                    // Create Cookie
                    var AppCookies = new HttpCookie("cookiesPN");

                    // Add Values
                    AppCookies["Language"] = service.Language;
                    AppCookies["SelectedTheme"] = "Dark";

                    // Add presistance for 50 Years
                    AppCookies.Expires = DateTime.Now.AddYears(50);

                    // Save Cookie
                    Response.Cookies.Add(AppCookies);
                    return Task.FromResult(true);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}