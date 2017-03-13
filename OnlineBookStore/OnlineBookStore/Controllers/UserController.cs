using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Assets.Constant;

namespace OnlineBookStore.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        [HttpGet]
        public ActionResult viewProfile() {
            var session = Session[Constant.CUSTOMER_SESSION];
            if (session != null) {
                var model = new UserDAO().GetByUsername((string)session);
                return View(model);
            }
            else {
                return View();
            }
        }

        //Edit profile
        [HttpPost, ValidateInput(false)]
        public ActionResult viewProfile(User model) {
            var dao = new UserDAO();
            var user = dao.GetByUsername((string)Session[Constant.CUSTOMER_SESSION]);
            model.UserName = user.UserName;
            model.UserID = user.UserID;
            model.Status = user.Status;
            model.CreatedDate = user.CreatedDate;
            model.RoleID = user.RoleID;
            var res = dao.Edit(model);
            if (res) {
                Response.Write("<script>alert('Thông tin của bạn đã được cập nhật')</script>");
            }
            else
            {
                Response.Write("<script>alert('Cập nhật thất bại, đã có lỗi xảy ra')</script>");
            }
            return View(model);
        }
    }
}