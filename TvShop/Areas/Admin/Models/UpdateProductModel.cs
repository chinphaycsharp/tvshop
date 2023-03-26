using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvShop.Areas.Admin.Models
{
    public class UpdateProductModel
    {
        public int id { get; set; }

        public int idCategories { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public DateTime? importDate { get; set; }

        public decimal price { get; set; }

        public bool? status { get; set; }

        public string imgSrc { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}