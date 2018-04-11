using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using GoogleMaps.LocationServices;


namespace Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserSQLDAL userDAL;

        public UserController(IUserSQLDAL userDAL)
        {
            this.userDAL = userDAL;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users currentUser)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View();
        }

        // GET: User
        // GET: User/Register
        public ActionResult Register()
        {
            return View("Register");
        }

        // POST: User/Register
        [HttpPost]
        public ActionResult Register(Users model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}