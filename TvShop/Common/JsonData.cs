using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvShop.Common
{
    public class JsonData
    {
        public decimal totalPrice { get; set; }
        public int idProduct { get; set; }
        public int quantity { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone{ get; set; }
        public string address { get; set; }
    }
}