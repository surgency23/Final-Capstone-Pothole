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
    public class UserController : TopController
    {
        private readonly IUserSQLDAL userDAL;

        public UserController(IUserSQLDAL userDAL)
            : base(userDAL)
        {
            this.userDAL = userDAL;
        }
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View("Login", model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = userDAL.GetUser(model.Username);

                if(currentUser == null)
                {
                    ModelState.AddModelError("invalid-user", "The username provided does not match an existing user");
                    return View("Login", model);
                }
                else if(currentUser.Password != model.Password)
                {
                    ModelState.AddModelError("invalid-password", "The password provided is not correct");
                    return View("Login", model);
                }

                base.LogUserIn(currentUser.Username);
                Session["isEmployee"] = currentUser.Is_Employee;

                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                return View("Login", model);
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            base.LogUserOut();

            return RedirectToAction("Index", "Home");
        }

        // GET: User
        // GET: User/Register
        public ActionResult Register()
        {
            var model = new Users();
            return View("Register", model);
        }

        // POST: User/Register
        [HttpPost]
        public ActionResult Register(Users model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = userDAL.GetUser(model.Username);

                if (currentUser != null)
                {
                    ViewBag.ErrorMessage = "This username is unavailable";
                    return View("Register", model);
                }

                userDAL.CreateUser(model);
                base.LogUserIn(model.Username);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Register", model);
            }
        }

    }
}