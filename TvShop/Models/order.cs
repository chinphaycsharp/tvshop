namespace TvShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order
    {
        public int id { get; set; }

        public int idCustomer { get; set; }

        public DateTime? purchaseDate { get; set; }

        public decimal totalPrice { get; set; }

        public int? status { get; set; }

        [StringLength(255)]
        public string nameCustomer { get; set; }

        [StringLength(255)]
        public string emailCustomer { get; set; }

        [StringLength(255)]
        public string phoneCustomer { get; set; }

        [StringLength(255)]
        public string addressCustomer { get; set; }
    }
}
