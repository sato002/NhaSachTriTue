using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookStore.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index(string searchString,int page = 1, int pageS = 10)
        {
            ViewBag.searchString = searchString;
            return View(new OrderDAO().GetListWithSearchString(searchString, page, pageS));
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            var dao = new OrderDAO();
            dao.DeleteOrderDetail(id);
            dao.Delete(id);
            return Json(new
            {
                Status = true
            });
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var res = new OrderDAO().ChangeStatus(id);
            return Json(new
            {
                Status = res
            });
        }

        public ActionResult ViewDetail(long id)
        {
            return View(new OrderDAO().ViewDetail(id));
        }
    }
}