using PN.Services;
using System.Web.Mvc;
using System.Web.Routing;

namespace PN
{
    public class RouteConfig // TODO: Las rutas se acumulan /es/Inicio/es/Inicio/...
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            foreach (var language in LanguagesService.Languages)
            {
                routes.MapRoute(
                    name: $"Forums{language}",
                    url: $"{language}/{LanguagesService.ForumTitles[language]}" + "/{forumName}/{action}",
                    defaults: new { controller = "Forum", action = "Index", forumName = UrlParameter.Optional }
                );

                routes.MapRoute(
                    name: $"Tags{language}",
                    url: $"{language}/{LanguagesService.TagTitles[language]}" + "/{forumName}/{tagName}/{action}",
                    defaults: new { controller = "Tag", action = "Index", tagName = UrlParameter.Optional }
                );

                routes.MapRoute(
                    name: $"Posts{language}",
                    url: $"{language}/{LanguagesService.PostTitles[language]}" + "/{forumName}/{tagName}/{postTitle}/{action}",
                    defaults: new { controller = "Post", action = "Index", postTitle = UrlParameter.Optional }
                );

                routes.MapRoute(
                    name: $"Users{language}",
                    url: $"{language}/{LanguagesService.UserTitles[language]}" + "/{action}",
                    defaults: new { controller = "Account", action = "Index" }
                );

                routes.MapRoute(
                    name: $"Account{language}",
                    url: $"{language}/{LanguagesService.AccountTitles[language]}" + "/{userName}",
                    defaults: new { controller = "Manage", action = "Index", userName = UrlParameter.Optional }
                );

                routes.MapRoute(
                    name: $"Home{language}",
                    url: $"{language}/{LanguagesService.HomeTitles[language]}" + "/{action}",
                    defaults: new { controller = "Home", action = "Index" }
                );

                routes.MapRoute(
                    name: $"Policies{language}",
                    url: $"{language}/{LanguagesService.PoliciesTitles[language]}",
                    defaults: new { controller = "Home", action = "Policies", userName = UrlParameter.Optional }
                );
            }

            routes.MapRoute(
                name: "rootWhitOptionalLanguage",
                url: "{language}",
                defaults: new { controller = "Home", action = "Index", language = "en", }
            );

            // And finally, the 404 action.
            routes.MapRoute(
                name: "ChactBadPaths",
                url: "{language}/{*catchall}",
                defaults: new { controller = "Home", action = "PageNotFound", language = "en", }
            );

            // For the folks who din't enter the languaje
            routes.MapRoute(
                name: "ChactHackers",
                url: "{*catchall}",
                defaults: new { controller = "Home", action = "PageNotFound" }
            );

            // The default route deprecate for security purposes
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{lang}/{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", language = "en", id = UrlParameter.Optional }
            //);
        }
    }
}
