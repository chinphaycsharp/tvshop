using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TvShop.Models;

namespace TvShop.DAO
{
    public class OrderDAO
    {
        private readonly TvShopDbContext _dbContext;

        public OrderDAO()
        {
            _dbContext = new TvShopDbContext();
        }

        public List<order> GetAllOrders()
        {
            var result = _dbContext.orders;
            return result.ToList();
        }

        public order GetOrderById(int id)
        {
            var result = _dbContext.orders.FirstOrDefault(x => x.id == id);
            return result;
        }

        public bool AddOrder(order order)
        {
            try
            {
                _dbContext.orders.Add(order);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public order GetOrderMax()
        {
            try
            {
                return _dbContext.orders.OrderByDescending(x=>x.purchaseDate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public bool DeleteOrderById(int id)
        {
            try
            {
                var data = _dbContext.orders.Where(x => x.id == id).FirstOrDefault();
                _dbContext.orders.Remove(data);
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