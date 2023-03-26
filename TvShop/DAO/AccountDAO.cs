using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TvShop.Models;

namespace TvShop.DAO
{
    public class AccountDAO
    {
        private readonly TvShopDbContext _dbContext;

        public AccountDAO()
        {
            _dbContext = new TvShopDbContext();
        }

        public account CheckInforLogin(string userName, string passWord)
        {
            try
            {
                var result = _dbContext.accounts.FirstOrDefault(x => x.username == userName && x.password == passWord);
                return result;
            }
            catch(Exception ex)
            {
                return default;
            }
        }
    }
}