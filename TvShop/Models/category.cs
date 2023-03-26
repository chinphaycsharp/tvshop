namespace TvShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class category
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        [Column(TypeName = "date")]
        public DateTime createdDate { get; set; }

        public bool? status { get; set; }
    }
}
