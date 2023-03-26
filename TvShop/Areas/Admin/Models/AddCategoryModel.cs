using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvShop.Areas.Admin.Models
{
    public class AddCategoryModel
    {
        public string title { get; set; }

        public DateTime createdDate { get; set; }

        public bool? status { get; set; }
    }
}