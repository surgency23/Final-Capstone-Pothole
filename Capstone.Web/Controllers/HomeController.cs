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
    public class HomeController : TopController
    {
        private readonly IPotholeDAL potholeDAL;

        public HomeController(IUserSQLDAL userDAL, IPotholeDAL potholeDAL) : base(userDAL)
        {
            this.potholeDAL = potholeDAL;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult DetailHole(Pothole pothole)
        {
            if(CurrentUser == "EmptyUserName" || CurrentUser != "")
            {
                potholeDAL.InsertPothole(pothole);
                return View("DetailHole", pothole);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult ManualPotHoleEntry()
        {
            if (CurrentUser != "EmptyUserName" || CurrentUser != "")
            {
                return View();
            }
            else
            {
                return View("Login", "User");
            }
        }

        [HttpPost]
        public ActionResult ManualPotHoleEntry(AddressData2 location)
        {
            Pothole pothole = new Pothole();
            GoogleLocationService gls = new GoogleLocationService();
            //"AIzaSyDYwiD - MW959R9rMr0_if1ULhHvYs03Q38" -- Google Key
            MapPoint latlong = gls.GetLatLongFromAddress(location.ToString());
            pothole.Latitude = (decimal)latlong.Latitude;
            pothole.Longitude = (decimal)latlong.Longitude;
            pothole.Severity = location.Severity;
            potholeDAL.InsertPothole(pothole);

            return View("DetailHole", pothole);
        }

        public ActionResult ViewPotholes()
        {
         
            if (IsEmployee())
            {
                return View("ViewPotholesForEmp", potholeDAL.GetAllPotholes());
            }
            return View("ViewPotholes",potholeDAL.GetAllPotholes());
        }

        public ActionResult ViewPotholesForEmp()
        {
            return View("ViewPotholesForEmp", potholeDAL.GetAllPotholes());
        }
        public ActionResult ViewPotholesForEmp2(string id)
        {
            potholeDAL.DeletePothole(id);
            return View("ViewPotholesForEmp", potholeDAL.GetAllPotholes());
        }
    }
}