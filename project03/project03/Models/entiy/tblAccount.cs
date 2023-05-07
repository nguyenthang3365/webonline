namespace project03.Models.entiy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblAccount")]
    public partial class tblAccount
    {
        [Key]
        public int Uid { get; set; }

        [StringLength(50)]
        [Required]
        public string Username { get; set; }

        [StringLength(40)]
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
