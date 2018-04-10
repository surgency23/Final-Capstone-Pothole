using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IPotholeDAL potholeDAL;

        public HomeController(IPotholeDAL potholeDAL)
        {
            this.potholeDAL = potholeDAL;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult ViewPotholes()
        {
            return View("ViewPotholes", potholeDAL.GetAllPotholes());
        }
    }
}