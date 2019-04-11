using System.Web;
using PN.Services;
using System.Web.Mvc;
using System.Web.Routing;
using System.Security.Principal;

namespace PN.Controllers.Helpers
{
    public static class HTTPHelpers
    {
        #region ImageLInk

        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, string action, string controller, string imageSrc)
        {
            // build the <img> tag
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var img = new TagBuilder("img");
            img.Attributes.Add("src", VirtualPathUtility.ToAppRelative(imageSrc));

            // build the <a> tag
            var anchor = new TagBuilder("a") { InnerHtml = img.ToString(TagRenderMode.SelfClosing) };
            anchor.Attributes["href"] = urlHelper.Action(action, controller);

            return MvcHtmlString.Create(anchor.ToString());
        }

        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, string action, string controller, object routeValues, object htmlAttributes, string imageSrc)
        {
            // build the <img> tag
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var img = new TagBuilder("img");
            img.Attributes.Add("src", VirtualPathUtility.ToAbsolute(imageSrc));

            // build the <a> tag
            var anchor = new TagBuilder("a") { InnerHtml = img.ToString(TagRenderMode.SelfClosing) };
            anchor.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(anchor.ToString());
        }

        #endregion

        #region SVGActionLink

        public static IHtmlString SVGActionLink(this HtmlHelper htmlHelper, string action, string controller, string imageSrc, string width, string heght)
        {
            // build the <img> tag
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var img = new TagBuilder("img");
            img.Attributes.Add("src", VirtualPathUtility.ToAppRelative(imageSrc));
            img.Attributes.Add("width", width);
            img.Attributes.Add("heght", heght);

            // build the <a> tag
            var anchor = new TagBuilder("a") { InnerHtml = img.ToString(TagRenderMode.SelfClosing) };
            anchor.Attributes["href"] = urlHelper.Action(action, controller);

            return MvcHtmlString.Create(anchor.ToString());
        }

        public static IHtmlString SVGActionLink(this HtmlHelper htmlHelper, string action, string controller, object routeValues, object htmlAttributes, string imageSrc, string width, string heght)
        {
            // build the <img> tag
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var img = new TagBuilder("object");
            img.Attributes.Add("data", VirtualPathUtility.ToAppRelative(imageSrc));
            img.Attributes.Add("type", "image/svg+xml");
            img.Attributes.Add("width", width);
            img.Attributes.Add("heght", heght);

            // build the <a> tag
            var anchor = new TagBuilder("a") { InnerHtml = img.ToString(TagRenderMode.SelfClosing) };
            anchor.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(anchor.ToString());
        }

        #endregion

        #region User.Identity

        public static string GetUserImagePath(this IIdentity identity)
        {
            var userService = new UserService();
            var user = userService.GetUserInformation();

            return user.ImagePath;
        }

        public static bool IsUserInRole(this IIdentity identity, string role)
        {
            var userService = new UserService();
            return userService.IsUserInRole(role);
        }

        #endregion

        #region MyRegion

        #endregion
    }
}