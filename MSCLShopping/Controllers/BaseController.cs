using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MSCLShopping.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // code involving this.Session // edited to simplify
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            // If the browser session or authentication session has expired...
            if (session.IsNewSession || Session["Pk_userId"] == null)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                     new { action = "Login", Controller = "Home" }));
            }
            else
            {
            }
            base.OnActionExecuting(filterContext); 
        }

    }
}
