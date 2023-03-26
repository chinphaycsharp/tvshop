namespace TvShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product
    {
        public int id { get; set; }

        public int idCategories { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(500)]
        public string description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? importDate { get; set; }

        public decimal price { get; set; }

        public bool? status { get; set; }

        [StringLength(255)]
        public string imgSrc { get; set; }
    }
}
