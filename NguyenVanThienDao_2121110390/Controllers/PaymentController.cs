using NguyenVanThienDao_2121110390.Context;
using NguyenVanThienDao_2121110390.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenVanThienDao_2121110390.Controllers
{
    public class PaymentController : Controller
    {
        AspEntities obj = new AspEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //Lấy thông tin giỏ hàng từ session
                var lstCart = (List<CartModel>)Session["cart"];
                //Gán cho đối tượng Order
                Order objOrder = new Order();
                objOrder.name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.user_id = int.Parse(Session["idUser"].ToString());
                objOrder.created_date = DateTime.Now;
                objOrder.status = 1;

                //Thêm thông tin order vào bảng order
                obj.Orders.Add(objOrder);
                //Luu thông tin order vào bảng order
                obj.SaveChanges();
                //lấy OrderId vừa mới lưu vào bảng OrderDetail
                int OrderId = objOrder.id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
                
                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.quantity = item.Quantity;
                    obj.order_id = OrderId;
                    obj.product_id = item.Product.id;
                    lstOrderDetail.Add(obj);
                }
                obj.OrderDetails.AddRange(lstOrderDetail);
                obj.SaveChanges();
            }
            Session.Remove("cart");
            return View();
        }
    }
}