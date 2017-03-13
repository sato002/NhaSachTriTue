﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models;
using Model.DAO;
using System.Web.Script.Serialization;
using Model.EF;
using OnlineBookStore.Assets.Constant;

namespace OnlineBookStore.Controllers
{
    public class CartController : Controller
    {
        private const string CARTSESSION = "CARTSESSION";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CARTSESSION];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult AddItem(long productID)
        {
            var product = new ProductDAO().GetByID(productID);
            var cart = Session[CARTSESSION];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.product.ProductID == productID))
                {
                    foreach (var i in list)
                    {
                        if (i.product.ProductID == productID)
                        {
                            i.quanlity += 1;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.product = product;
                    item.quanlity = 1;
                    list.Add(item);
                }

                Session[CARTSESSION] = list;
            }
            else
            {
                var item = new CartItem();
                item.product = product;
                item.quanlity = 1;
                var list = new List<CartItem>();
                list.Add(item);

                Session[CARTSESSION] = list;

            }
            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel) {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CARTSESSION];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.product.ProductID == item.product.ProductID);
                if (jsonItem != null) {
                    item.quanlity = jsonItem.quanlity;
                }
            }
            Session[CARTSESSION] = sessionCart;
            return Json(new {
                Status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll() {
            Session[CARTSESSION] = null;
            return Json(new {
                Status = true
            });
        }

        [HttpPost]
        public JsonResult Delete(long id) {
            var sessionCart = (List<CartItem>)Session[CARTSESSION];
            sessionCart.RemoveAll(x => x.product.ProductID == id);
            Session[CARTSESSION] = sessionCart;
            return Json(new {
                Status = true
            });
        }

        public ActionResult Message(string msg)
        {
            ViewBag.msgStatus = msg;
            return View();
        }

        public ActionResult Payment() {
            return View();
        }

        [HttpGet]
        public ActionResult PaymentWithLogin()
        {
            if (ModelState.IsValid)
            {
                var user = new UserDAO().GetByUsername((string)Session[Constant.CUSTOMER_SESSION]);
                var orderInfo = new Order();
                orderInfo.shipAddress = user.Address;
                orderInfo.shipEmail = user.Email;
                orderInfo.shipPhone = user.Phone;
                orderInfo.shipName = user.Name;
                var res = new OrderDAO().Create(orderInfo);
                if (res == -1)
                {
                    return RedirectToAction("Message", new { msg = "error" });
                }
                else {
                    setOrdelDetail(res);
                    return RedirectToAction("Message", new { msg = "success" });
                }
            }
            else{
                return RedirectToAction("Message", new { msg = "error" });
            }
        }

        [HttpPost]
        public ActionResult Payment(Order order) {
            if (ModelState.IsValid)
            {
                var dao = new OrderDAO();
                var res = dao.Create(order);
                if (res == -1)
                {
                    return RedirectToAction("Message", new { msg = "error" });
                }
                else {
                    setOrdelDetail(res);
                    return RedirectToAction("Message", new { msg = "success" });
                }

            }
            else {
                return RedirectToAction("Message", new { msg = "error" });
            }

        }

        void setOrdelDetail(long res) {
            var items = (List<CartItem>)Session[CARTSESSION];
            var orderDetailDAO = new OrderDetailDAO();
            foreach (var item in items)
            {
                var orderDetail = new OrderDetail();
                orderDetail.ProductID = item.product.ProductID;
                orderDetail.OrderID = res;
                orderDetail.Quanlity = item.quanlity;
                if (item.product.PromotionPrice == 0)
                {
                    orderDetail.Price = item.product.Price;
                }
                else {
                    orderDetail.Price = item.product.PromotionPrice;
                }
                orderDetailDAO.Create(orderDetail);
            }
            Session[CARTSESSION] = null;
        }
    }
}