using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TvShop.Common;
using TvShop.DAO;
using TvShop.Models;

namespace TvShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly OrderDAO _orderDAO;
        private readonly OrderDetailDAO _orderDetailDAO;
        private readonly ProductDAO _productDAO;
        public ProductController()
        {
            _orderDAO = new OrderDAO();
            _orderDetailDAO = new OrderDetailDAO();
            _productDAO = new ProductDAO();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewProduct(int id)
        {
            var result = _productDAO.GetProductById(id);
            return View(result);
        }

        public JsonResult GetProductById(int id)
        {
            var result = _productDAO.GetProductById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PurchaseProduct(string data)
        { 
            if(data != null)
            {
                JsonData result = JsonConvert.DeserializeObject<JsonData>(data);
                order order = new order();
                order.idCustomer = 0;
                order.purchaseDate = DateTime.Now;
                order.status = 1;
                order.totalPrice = result.totalPrice;
                order.emailCustomer = result.email;
                order.nameCustomer = result.name;
                order.phoneCustomer = result.phone;
                order.addressCustomer = result.address;
                if(_orderDAO.AddOrder(order))
                {
                    orderDetail orderDetail = new orderDetail();
                    orderDetail.orderId = _orderDAO.GetOrderMax().id;
                    orderDetail.productId = result.idProduct;
                    orderDetail.quantity = result.quantity;
                    if(_orderDetailDAO.AddOrderDetail(orderDetail))
                    {
                        //ViewBag.Success = ;
                        TempData["result"] = "Đặt mua hàng thành công";
                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                else
                {
                    return Json(false);
                }
            }

            return Json(false);
        }
    }
}