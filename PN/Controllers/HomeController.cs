using PN.Models;
using System.Linq;
using PN.Services;
using System.Web.Mvc;

namespace PN.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new AppDbContext())
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userService = new UserService();
                    var userInfo = userService.GetUserInformation();
                    ViewBag.LastRead = userInfo.LastPostId;

                }

                var forums = db.Forum.Take(6).ToList();
                return View(forums);
            }
        }

        // GET: Home/Subscribe/5
        [Authorize]
        public ActionResult Subscribe(int? id)
        {
            if (id == null) return HttpNotFound();

            var userService = new UserService();
            var userInfo = userService.GetUserInformation();

            using (var db = new AppDbContext())
            {
                if (!db.UserForumSubscription.Any(o => o.UserId == userInfo.Id && o.ForumId == id.Value))
                {
                    var subscription = new UserForumSubscription
                    {
                        UserId = userInfo.Id,
                        ForumId = id.Value,
                        Score = 0
                    };

                    db.UserForumSubscription.Add(subscription);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return HttpNotFound();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }
    }
}