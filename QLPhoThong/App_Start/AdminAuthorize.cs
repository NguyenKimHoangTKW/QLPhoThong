using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace QLPhoThong.App_Start
{
    public class AdminAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            User user1 = (User)HttpContext.Current.Session["User"];
            User user2 = (User)HttpContext.Current.Session["UserAdmin"];
            if (user1 != null || user2 != null)
            {
                return;
            }
            else
            {
                var returnURL = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Error", area = "", returnURL = returnURL.ToString() }));
            }
        }
    }
}