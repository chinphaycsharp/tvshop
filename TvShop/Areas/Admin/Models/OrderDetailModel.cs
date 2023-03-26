using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvShop.Areas.Admin.Models
{
    public class OrderDetailModel
    {
        public DateTime? purchaseDate { get; set; }
        public decimal totalPrice { get; set; }
        public int? status { get; set; }
        public string nameCustomer { get; set; }
        public string emailCustomer { get; set; }
        public string phoneCustomer { get; set; }
        public string addressCustomer { get; set; }
        public int? quantity { get; set; }
    }
}