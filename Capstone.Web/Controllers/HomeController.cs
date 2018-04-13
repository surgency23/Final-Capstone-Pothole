using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using GoogleMaps.LocationServices;
using PagedList;

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

        public ActionResult ViewPotholes(int? page)
        {
            int pageSize = 15;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Pothole> pagedPotholes = null;
            List<Pothole> potholeList = potholeDAL.GetAllPotholes();
            pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            if (IsEmployee())
            {
                return View("ViewPotholesForEmp",pagedPotholes );
            }

            return View("ViewPotholes", pagedPotholes);
        }

        public ActionResult ViewPotholesForEmp(int? page)
        {
            int pageSize = 15;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Pothole> pagedPotholes = null;
            List<Pothole> potholeList = potholeDAL.GetAllPotholes();
            pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            return View("ViewPotholesForEmp", pagedPotholes);
        }
        public ActionResult ViewPotholesForEmp2(string id, int? page)
        {
            int pageSize = 15;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Pothole> pagedPotholes = null;
            List<Pothole> potholeList = potholeDAL.GetAllPotholes();
            pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            potholeDAL.DeletePothole(id);

            return View("ViewPotholesForEmp", pagedPotholes);
        }

        public ActionResult UpdatePothole(string id)
        {
            return View("UpdatePothole", potholeDAL.GetOnePotholes(id));
        }
        [HttpPost]
        public ActionResult UpdatePothole(Pothole updatedPothole)
        {
            potholeDAL.UpdatePothole(updatedPothole);
            string id = updatedPothole.PotholeID.ToString();
            
            return View("UpdatePothole", potholeDAL.GetOnePotholes(id));
        }
    }
}