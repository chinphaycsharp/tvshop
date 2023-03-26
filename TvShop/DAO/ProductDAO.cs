using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TvShop.Areas.Admin.Models;
using TvShop.Models;

namespace TvShop.DAO
{
    public class ProductDAO
    {
        private readonly TvShopDbContext _dbContext;

        public ProductDAO()
        {
            _dbContext = new TvShopDbContext();
        }

        public List<product> GetAllProducts()
        {
            var result = _dbContext.products.Where(x=>x.status == true);
            return result.ToList();
        }

        public bool AddProduct(product product)
        {
            try
            {
                _dbContext.products.Add(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public product GetProductById(int id)
        {
            try
            {
                var result = _dbContext.products.FirstOrDefault(x => x.id == id);
                return result;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public List<product> GetProductByCategoryId(int id)
        {
            try
            {
                var result = _dbContext.products.Where(x => x.idCategories == id).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public bool UpdateProduct(UpdateProductModel model)
        {
            try
            {
                var result = _dbContext.products.FirstOrDefault(x => x.id == model.id);
                result.status = model.status;
                result.name = model.name;
                result.description = model.description;
                result.description = model.description;
                result.idCategories = model.idCategories;
                result.imgSrc = model.imgSrc;
                result.importDate = DateTime.Now;
                result.price = model.price;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var data = _dbContext.products.Where(x => x.id == id).FirstOrDefault();
                _dbContext.products.Remove(data);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}