using project03.Models.entiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project03.Controllers
{
    public class LoginController : Controller
    {
        Model1 model = new Model1();
        [HttpGet]
        public  ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(tblAccount account)
        {
            var user = account.Username;
            var pass = account.Password;
            var count = model.tblAccounts.FirstOrDefault(s => s.Username == user && s.Password == pass);
            if (count == null)
            {
                ViewBag.err = "sai user hoac pass";
                return View();
            }
            Session["id_account"] = count.Uid;
            Session["name_account"] = count.Username;
            return Redirect("Shop/Index");
        }

        public ActionResult logout()
        {
            Session["id_account"] = null;
            Session["name_account"] = null;
            return RedirectToAction("Index","shop");
        }





    }
}