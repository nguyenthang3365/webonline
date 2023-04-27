using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project03.Models.entiy;

namespace project03.Controllers
{
    public class MyController : Controller
    {
        // GET: My
        public ActionResult Index()
        {
            Model1 model1 = new Model1();   
          var loaihang = model1.LoaiHangs.ToList();
            return PartialView("Header",loaihang);
        }
    }
}