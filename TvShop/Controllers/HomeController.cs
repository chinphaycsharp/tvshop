using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TvShop.DAO;
using TvShop.Models;

namespace TvShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductDAO _productDAO;
        private readonly CategorieDAO _categoryDAO;
        public HomeController()
        {
            _productDAO = new ProductDAO();
            _categoryDAO = new CategorieDAO();
        }
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            var result = _productDAO.GetAllProducts();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.name.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "Name":
                    result = result.OrderBy(s => s.name).ToList();
                    break;
                case "name_desc":
                    result = result.OrderByDescending(s => s.name).ToList();
                    break;
                case "Price":
                    result = result.OrderBy(s => s.price).ToList();
                    break;
                case "price_desc":
                    result = result.OrderByDescending(s => s.price).ToList();
                    break;
                default:
                    result = result.OrderBy(s => s.id).ToList();
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            if(TempData["result"] != null)
            {
                ViewBag.Success = TempData["result"];
            }
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Header()
        {
            var list = new List<category>();
            list = _categoryDAO.GetAllCategories();
            return PartialView(list);
        }

        public ActionResult GetProductByCateId(int id, string sortOrder="", string currentFilter="", string searchString="", int? page=1)
        {
            ViewBag.CateId = id;
            var result = _productDAO.GetProductByCategoryId(id);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.name.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "Name":
                    result = result.OrderBy(s => s.name).ToList();
                    break;
                case "name_desc":
                    result = result.OrderByDescending(s => s.name).ToList();
                    break;
                case "Price":
                    result = result.OrderBy(s => s.price).ToList();
                    break;
                case "price_desc":
                    result = result.OrderByDescending(s => s.price).ToList();
                    break;
                default:
                    result = result.OrderBy(s => s.id).ToList();
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }
    }
}