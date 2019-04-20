using PN.Models;
using System.Linq;
using PN.Services;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace PN.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult RedirectToFullURL(string ShortURL)
        {
            if (!db.RandomLink.Any(m => m.ShortedLink.Contains(ShortURL))) return PageNotFound();

            var fullLink = db.RandomLink.First(m => m.ShortedLink.Contains(ShortURL)).FullLink;

            return RedirectToLocal(fullLink);
        }

        public ActionResult Index()
        {
            var isFirstInit = IsFirstInit();
            var model = new HomeViewModel { Forums = db.Forum.Take(7).ToList() };

            if (!isFirstInit && Request.IsAuthenticated)
            {
                using (var userService = new UserService())
                {
                    var userInfo = userService.GetUserInformation();
                    model.LastPostRead = db.Post.Find(userInfo.LastPostId);
                }
            }

            return View(model);
        }
    }
}