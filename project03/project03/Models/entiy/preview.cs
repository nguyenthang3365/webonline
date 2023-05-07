namespace project03.Models.entiy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

   
    public partial class preview
    {
        public HangHoa hanghoa { set; get; }
        public List<post> post { set; get; }
    }
}
