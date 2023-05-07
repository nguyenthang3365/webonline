using project03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project03.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var user = new User();
            user.name = "jonh";
            user.adress = "ha noi";
            user.email = "john@gmail.com";
            ViewBag.user = user;
            return View();
        }

        public ActionResult listUser()
        {
            var user = new List<User>();

            var user1 = new User();
            user1.name = "jonh";
            user1.adress = "ha noi";
            user1.email = "john@gmail.com";

            var user2 = new User();
            user2.name = "jonh";
            user2.adress = "ha noi";
            user2.email = "john@gmail.com";

            user.Add(user1);user.Add(user2);
            ViewBag.user = user;
            return View();
        }
        public ActionResult modelUser()
        {
            var user = new List<User>();

            var user1 = new User();
            user1.name = "jonh";
            user1.adress = "ha noi";
            user1.email = "john@gmail.com";

            var user2 = new User();
            user2.name = "nam";
            user2.adress = "ha noi";
            user2.email = "john@gmail.com";

            user.Add(user1); user.Add(user2);
           
            return View(user1);
        }


        public ActionResult listUserModel()
        {
            var user = new List<User>();

            var user1 = new User();
            user1.name = "jonh";
            user1.adress = "ha noi";
            user1.email = "john@gmail.com";

            var user2 = new User();
            user2.name = "nam";
            user2.adress = "ha noi";
            user2.email = "john@gmail.com";

            user.Add(user1); user.Add(user2);

            return View(user);
        }
    }
}