using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project03.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["user"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new { Controller = "Home", Action = "login" })
                    );
            }
            base.OnActionExecuting(filterContext);
        }
    }
}