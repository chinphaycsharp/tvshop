using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TvShop.Models;

namespace TvShop.DAO
{
    public class OrderDetailDAO
    {
        private readonly TvShopDbContext _dbContext;

        public OrderDetailDAO()
        {
            _dbContext = new TvShopDbContext();
        }

        public bool AddOrderDetail(orderDetail orderDetail)
        {
            try
            {
                _dbContext.orderDetails.Add(orderDetail);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public orderDetail GetOrderDetailByOrderId(int id)
        {
            var result = _dbContext.orderDetails.FirstOrDefault(x => x.orderId == id);
            return result;
        }

        public bool DeleteOrderDetailByOrderId(int id)
        {
            try
            {
                var data = _dbContext.orderDetails.Where(x => x.orderId == id).FirstOrDefault();
                _dbContext.orderDetails.Remove(data);
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