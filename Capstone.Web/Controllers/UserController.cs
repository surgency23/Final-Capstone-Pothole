using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class UserController : Controller
    {
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