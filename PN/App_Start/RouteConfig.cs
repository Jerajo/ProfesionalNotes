using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PN
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ForumsName",
                url: "Foros/{action}/{forumName}",
                defaults: new { controller = "Forum", action = "Index", forumName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TagsName",
                url: "Temas/{action}/{forumName}/{tagName}",
                defaults: new { controller = "Tag", action = "Index", tagName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PostsName",
                url: "Articulos/{action}/{forumName}/{tagName}/{postName}",
                defaults: new { controller = "Post", action = "Index", postName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UserList",
                url: "Usuarios/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
