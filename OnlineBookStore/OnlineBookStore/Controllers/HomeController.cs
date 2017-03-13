using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookStore.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int top = 4)
        {
            ViewBag.newProducts = new ProductDAO().GetListOrderByCreatedDate(top);
            ViewBag.promotionProducts = new ProductDAO().GetListOrderByPrice(top);
            ViewBag.hotProducts = new ProductDAO().GetListOrderByView(top);
            ViewBag.buyProducts = new ProductDAO().GetHotList(top);
            return View();
        }

        [ChildActionOnly]
        public ActionResult _MainMenu()
        {
            var model = new MenuDAO().GetListByID(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult _Categorys()
        {
            var model = new ProductCategoryDAO().GetList(null);
            return PartialView(model);
        }

    }
}