using project03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project03.Controllers
{
    public class AddUserController : Controller
    {
        // GET: AddUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult check_info(User user)
        {
            if (user.name == "ha" && user.adress == "hanoi")
            {
                string msg = "wel come ha from ha noi";
                return Content(msg);
            }
            else
            {
                return View();
            }

        }
    }
}