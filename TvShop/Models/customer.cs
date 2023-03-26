namespace TvShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class customer
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        public string fullname { get; set; }

        [StringLength(255)]
        public string adddress { get; set; }

        [StringLength(15)]
        public string phone { get; set; }

        [Column(TypeName = "date")]
        public DateTime? birthday { get; set; }
    }
}
