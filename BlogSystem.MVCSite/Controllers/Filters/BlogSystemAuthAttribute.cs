using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogSystem.MVCSite.Controllers.Filters
{
    public class BlogSystemAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //当用户存储在cookie中且session数据为空时，把cookie数据同步到session中，好处：直接从session中取值
            if (filterContext.HttpContext.Session["loginName"] 
                != null && filterContext.HttpContext.Request.Cookies["loginName"] == null)
            {
                filterContext.HttpContext.Session["loginName"] = filterContext.HttpContext.Request.Cookies["loginName"];
                filterContext.HttpContext.Session["userId"] = filterContext.HttpContext.Request.Cookies["userId"];
            }

            //session和cookie都为空
            //base.OnAuthorization(filterContext);
            if (!(filterContext.HttpContext.Session["loginName"] != null
                || filterContext.HttpContext.Request.Cookies["loginName"] != null))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    { "controller","Home"},
                    {"action","Login" }
                });
            }
        }
    }
}