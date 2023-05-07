namespace project03.Models.entiy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("lichsumuahang")]
    public partial class lichsumuahang
    {
        [Key]
        public int id_hang { get; set; }

        public int id_account { get; set; }

   
        public int id_sanpham { get; set; }

        public int quantity { get; set; }

        public DateTime thoigianmua { get; set; }

        public string sodienthoai { set; get; }

        public string email { set; get; }
        public string diachi { set; get; }
    }
}
