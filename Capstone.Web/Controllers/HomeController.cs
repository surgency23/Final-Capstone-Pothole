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
    public class HomeController : Controller
    {
        private readonly IPotholeDAL potholeDAL;

        public HomeController(IPotholeDAL potholeDAL) 
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
            potholeDAL.InsertPothole(pothole);
            return View("DetailHole", pothole);
        }

        public ActionResult ManualPotHoleEntry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManualPotHoleEntry(AddressData2 location)
        {
            Pothole pothole = new Pothole();
            GoogleLocationService gls = new GoogleLocationService("AIzaSyDYwiD - MW959R9rMr0_if1ULhHvYs03Q38");
            MapPoint latlong = gls.GetLatLongFromAddress(location.ToString());
            pothole.Latitude = (decimal)latlong.Latitude;
            pothole.Longitude = (decimal)latlong.Longitude;
            pothole.Severity = location.Severity;
            potholeDAL.InsertPothole(pothole);

            return View("DetailHole", pothole);
        }

        public ActionResult ViewPotholes()
        {
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