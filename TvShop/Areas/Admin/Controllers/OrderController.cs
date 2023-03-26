using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TvShop.Areas.Admin.Models;
using TvShop.DAO;

namespace TvShop.Areas.Admin.Controllers
{
    public class OrderController : baseController
    {
        private readonly OrderDAO _orderDAO;
        private readonly OrderDetailDAO _orderDetailDAO;
        public OrderController()
        {
            _orderDAO = new OrderDAO();
            _orderDetailDAO = new OrderDetailDAO();
        }


        public ActionResult GetAllOrder(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var result = _orderDAO.GetAllOrders();
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.nameCustomer.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    result = result.OrderByDescending(s => s.nameCustomer).ToList();
                    break;
                case "Date":
                    result = result.OrderBy(s => s.purchaseDate).ToList();
                    break;
                case "date_desc":
                    result = result.OrderByDescending(s => s.purchaseDate).ToList();
                    break;
                default:
                    result = result.OrderBy(s => s.id).ToList();
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (TempData["result"] != null)
            {
                ViewBag.Success = TempData["result"];
            }
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Detail(int id)
        {
            var order = _orderDAO.GetOrderById(id);
            if(order != null)
            {
                var orderDetail = _orderDetailDAO.GetOrderDetailByOrderId(order.id);
                OrderDetailModel model = new OrderDetailModel()
                {
                    addressCustomer = order.addressCustomer,
                    emailCustomer = order.emailCustomer,
                    phoneCustomer = order.phoneCustomer,
                    nameCustomer = order.nameCustomer,
                    purchaseDate = order.purchaseDate,
                    status = order.status,
                    quantity = orderDetail.quantity,
                    totalPrice = order.totalPrice,
                };
                return View(model);
            }
            return RedirectToAction("GetAllOrder");
        }

        public ActionResult Delete(int id)
        {
            if (_orderDetailDAO.DeleteOrderDetailByOrderId(id))
            {
                if(_orderDAO.DeleteOrderById(id))
                {
                    TempData["result"] = "Xóa đơn hàng thành công";
                    return RedirectToAction("GetAllOrder");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
    }
}