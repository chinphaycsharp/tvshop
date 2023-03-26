using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvShop.Areas.Admin.Models
{
    [Serializable]
    public class userLogin
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public DateTime? loginDate{ get; set; }
    }
}