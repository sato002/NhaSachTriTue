﻿using OnlineBookStore.Assets.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookStore.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = Session[Constant.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary(new
                        {
                            controller = "Login",
                            action = "Index",
                            Area = "Admin"
                        })
                    );
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string msg,string type)
        {
            TempData["AlertMessage"] = msg;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}