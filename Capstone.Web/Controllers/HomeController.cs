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

        [HttpPost]
        public ActionResult DetailHole(Pothole pothole)
        {
            return View("DetailHole", pothole);
        }

        public ActionResult ManualPotHoleEntry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManualPotHoleEntry(AddressData address)
        {
            Pothole pothole = new Pothole();
            GoogleLocationService gls = new GoogleLocationService();
            MapPoint latlong = gls.GetLatLongFromAddress(address);
            pothole.Latitude = (decimal)latlong.Latitude;
            pothole.Longitude = (decimal)latlong.Longitude;

            return View("DetailHole", pothole);
        }
    }
}