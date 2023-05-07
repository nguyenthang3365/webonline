using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project03.Controllers
{
    public class registerController : Controller
    {
        // GET: login
        public ActionResult Index()
        {

           // Session["user"] = "nam";
            return View();
        }

        [HttpGet()]
        public ActionResult aboad(string username,string password)
        {
            ViewBag.Username = username;
            ViewBag.Password = password;
            // Session["user"] = "nam";
           return View();
        }


        public ActionResult check_info()
        {
            return View();
        }


        public ActionResult info(string user,string pass)
        {
            TempData["messa"] = "luong ngoc quang";
            ViewData["user"] = user;
            ViewBag.user = user;
            ViewBag.pass= pass;
            return View();
        }


        public ActionResult check_haha()
        {
            return View();
        }



    }
}