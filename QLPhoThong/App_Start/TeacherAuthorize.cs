using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLPhoThong.App_Start
{
    public class TeacherAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            User teacher = (User)HttpContext.Current.Session["User"];
            if (teacher != null)
            {
                return;
            }
            else
            {
                var returnURL = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "", returnURL = returnURL.ToString() }));
            }
        }
    }
}