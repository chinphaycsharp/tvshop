using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TvShop.Areas.Admin.Models;
using TvShop.Models;

namespace TvShop.DAO
{
    public class CategorieDAO
    {
        private readonly TvShopDbContext _dbContext;

        public CategorieDAO()
        {
            _dbContext = new TvShopDbContext();
        }

        public List<category> GetAllCategories()
        {
            var result = _dbContext.categories;
            return result.ToList();
        }

        public bool AddCategory(category category)
        {
            try
            {
                _dbContext.categories.Add(category);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public category GetCategoryById(int id)
        {
            try
            {
                var result = _dbContext.categories.FirstOrDefault(x => x.id == id);
                return result;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public bool UpdateCategory(UpdateCategoryModel model)
        {
            try
            {
                var result = _dbContext.categories.FirstOrDefault(x => x.id == model.id);
                result.status= model.status;
                result.createdDate = DateTime.Now;
                result.title = model.title;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public bool DeleteCategory(int id)
        {
            try
            {
                var data = _dbContext.categories.Where(x => x.id == id).FirstOrDefault();
                _dbContext.categories.Remove(data);
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