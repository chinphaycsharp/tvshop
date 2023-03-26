using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using TvShop.Areas.Admin.Models;
using TvShop.DAO;
using TvShop.Models;

namespace TvShop.Areas.Admin.Controllers
{
    public class AdminController : baseController
    {
        private readonly CategorieDAO _categorieDAO;
        private readonly ProductDAO _productDAO;
        public AdminController()
        {
            _productDAO = new ProductDAO();
            _categorieDAO = new CategorieDAO();
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Phần danh mục sản phẩm
        public ActionResult GetAllCategories(string sortOrder, string currentFilter, string searchString, int? page)
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
            var result = _categorieDAO.GetAllCategories();
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.title.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    result = result.OrderByDescending(s => s.title).ToList();
                    break;
                case "Date":
                    result = result.OrderBy(s => s.createdDate).ToList();
                    break;
                case "date_desc":
                    result = result.OrderByDescending(s => s.createdDate).ToList();
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

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AddCategoryModel model)
        {
            if(model != null)
            {
                category category = new category();
                category.title = model.title;
                category.createdDate = System.DateTime.Now;
                category.status = true;

                try
                {
                    if(_categorieDAO.AddCategory(category))
                    {
                        ViewBag.Message = "Data Insert Successfully";
                        TempData["result"] = "Thêm danh mục thành công";
                        return RedirectToAction("GetAllCategories", "Admin");
                    }
                    else
                    {
                        ViewBag.Message = "Data Insert Error";
                        return RedirectToAction("Create", "Admin");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return RedirectToAction("Create", "Admin");
                }
            }
            return RedirectToAction("Create", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var result = _categorieDAO.GetCategoryById(id);
            UpdateCategoryModel model = new UpdateCategoryModel()
            {
                id = id,
                createdDate = result.createdDate,
                status = result.status,
                title = result.title
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UpdateCategoryModel model)
        {
            if (model != null)
            {
                try
                {
                    if(_categorieDAO.UpdateCategory(model))
                    {
                        ViewBag.Message = "Data Update Successfully";
                        TempData["result"] = "Sửa danh mục thành công";
                        return RedirectToAction("GetAllCategories", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Edit", "Admin");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return RedirectToAction("Edit", "Admin");
                }
            }
            return RedirectToAction("Edit", "Admin");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                if(_categorieDAO.DeleteCategory(id))
                {
                    TempData["result"] = "Xóa danh mục thành công";
                    ViewBag.Message = "Data Remove Successfully";
                }
                else
                {
                    ViewBag.Message = "Data Remove Error";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return RedirectToAction("GetAllCategories");
        }
        #endregion

        #region Phần sản phẩm
        public ActionResult GetAllProducts(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
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
            var result = _productDAO.GetAllProducts();
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.name.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    result = result.OrderByDescending(s => s.name).ToList();
                    break;
                case "Date":
                    result = result.OrderBy(s => s.importDate).ToList();
                    break;
                case "date_desc":
                    result = result.OrderByDescending(s => s.importDate).ToList();
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if(TempData["result"] != null)
            {
                ViewBag.Success = TempData["result"];
            }
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(AddProductModel model)
        {
            if (model != null)
            {
                product product = new product();
                product.idCategories= model.idCategories;
                product.price= model.price;
                product.name = model.name;
                product.description= model.description;
                product.importDate = System.DateTime.Now;
                product.status = true;
                try
                {
                    var soureImage = model.ImageFile;
                    string fileExtention = Path.GetExtension(model.ImageFile.FileName);
                    product.imgSrc = "/Image/"+ Path.GetFileNameWithoutExtension(model.ImageFile.FileName) + fileExtention;
                }
                catch(Exception ex)
                {

                }
                try
                {
                    if(_productDAO.AddProduct(product))
                    {
                        ViewBag.Message = "Data Insert Successfully";
                        TempData["result"] = "Thêm sản phẩm thành công";
                        return RedirectToAction("GetAllProducts", "Admin");
                    }
                    else
                    {
                        ViewBag.Message = "Data Insert Error";
                        return RedirectToAction("CreateProduct", "Admin");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return RedirectToAction("CreateProduct", "Admin");
                }
            }
            return RedirectToAction("CreateProduct", "Admin");
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var result = _productDAO.GetProductById(id);
            SetViewBag(result.idCategories);
            UpdateProductModel model = new UpdateProductModel()
            {
                id = id,
                description = result.description,
                status = result.status,
                name = result.name,
                price= result.price,
                imgSrc = result.imgSrc,
                importDate = result.importDate,
                idCategories = result.idCategories
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProduct(UpdateProductModel model)
        {
            if (model != null)
            {
                try
                {
                    try
                    {
                        var soureImage = model.ImageFile;
                        if(soureImage != null)
                        {
                            string fileExtention = Path.GetExtension(model.ImageFile.FileName);
                            model.imgSrc = Path.GetFileNameWithoutExtension(model.ImageFile.FileName) + fileExtention;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    if (_productDAO.UpdateProduct(model))
                    {
                        ViewBag.Message = "Data Update Successfully";
                        TempData["result"] = "Sửa sản phẩm thành công";
                        return RedirectToAction("GetAllProducts", "Admin");
                    }
                    else 
                    {
                        return RedirectToAction("EditProduct", "Admin");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return RedirectToAction("EditProduct", "Admin");
                }
            }
            return RedirectToAction("EditProduct", "Admin");
        }


        public ActionResult DeleteProduct(int id)
        {
            try
            {
                if (_productDAO.DeleteProduct(id))
                {
                    TempData["result"] = "Xóa sản phẩm thành công";
                    ViewBag.Message = "Data Remove Successfully";
                }
                else
                {
                    ViewBag.Message = "Data Remove Error";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return RedirectToAction("GetAllProducts");
        }

        public void SetViewBag(int? selectedId = null)
        {
            ViewBag.idCategories = new SelectList(_categorieDAO.GetAllCategories(), "id", "title",selectedId    );
        }
        #endregion
    }
}