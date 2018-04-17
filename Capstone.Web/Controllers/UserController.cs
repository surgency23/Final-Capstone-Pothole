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

        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View("Login", model);
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            string pw = Request.Form.Get("Password");
            string user = Request.Form.Get("Username");
            LoginModel model = new LoginModel();
            model.Username = user;
            model.Password = pw;
            if (ModelState.IsValid)
            {
                var currentUser = userDAL.GetUser(model.Username);

                if (currentUser == null)
                {
                    ModelState.AddModelError("invalid-user", "The username provided does not match an existing user");
                    return RedirectToAction("ViewPotholes", "Home", model);
                }
                else if (currentUser.Password != model.Password)
                {
                    ModelState.AddModelError("invalid-password", "The password provided is not correct");
                    return RedirectToAction("ViewPotholes","Home", model);
                }

                base.LogUserIn(currentUser.Username);
                Session["isEmployee"] = currentUser.Is_Employee;
                if (currentUser.Is_Employee == 1)
                {
                    return RedirectToAction("ViewPotholesForEmp", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            else
            {
                return RedirectToAction("ViewPotholes");
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {

            base.LogUserOut();

            return RedirectToAction("ViewPotholes", "Home");
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
        public ActionResult Register(string fname,string lname, string uname, string Pw,string cpw,string Email, string cemail)
        {
            var model = new Users();
            string fName = Request.Form.Get("Firstname");
            string lName = Request.Form.Get("Lastname");
            string uName = Request.Form.Get("RegUsername");
            string pw = Request.Form.Get("RegPassword");
            string confirmPw = Request.Form.Get("RegPasswordConfirm");
            string email = Request.Form.Get("Email");
            string confirmEmail = Request.Form.Get("ConfirmEmail");
            model.FirstName = fName;
            model.LastName = lName;
            model.Username = uName;
            model.Password = pw;
            model.Email = email;
            model.ConfirmPassword = confirmPw;
            model.ConfirmEmail = confirmEmail;
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

        [HttpGet]
        public ActionResult ChangePassword(string username)
        {
            if (CurrentUser != username)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ChangePasswordModel();
            return View("ChangePassword", model);

        }

        [HttpPost]
        public ActionResult ChangePassword(string username, ChangePasswordModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("ChangePassword", model);
            }

            userDAL.ChangePassword(username, model.NewPassword);

            return RedirectToAction("Login");
                
        }
    }
}