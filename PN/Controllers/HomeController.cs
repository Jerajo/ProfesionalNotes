using System;
using PN.Models;
using System.Web;
using System.Net;
using System.Linq;
using PN.Services;
using System.Web.Mvc;
using System.Data.Entity;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace PN.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            IsLoading = await Loading();
            var model = new HomeViewModel { Forums = db.Forum.Take(6).ToList() };

            if (Request.IsAuthenticated)
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