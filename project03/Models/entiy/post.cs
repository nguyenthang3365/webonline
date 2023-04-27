namespace project03.Models.entiy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public  class post
    {
        [Key]
        public int id_post { set; get; }
        public int id_account { get; set; }
        public  int id_sanpham { get; set; }
        public  string content_post { get; set; }
        public  DateTime ngay_post { get; set; }
   


    }
}
