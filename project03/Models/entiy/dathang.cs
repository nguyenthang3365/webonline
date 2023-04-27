namespace project03.Models.entiy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dathang")]
    public partial class dathang
    {
        [Key]
        public int id_hang { get; set; }

        public int id_account { get; set; }

   
        public int id_sanpham { get; set; }

        public int quantity { get; set; }
    }
}
