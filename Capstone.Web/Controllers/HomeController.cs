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
        private readonly IClaimsDAL claimsDAL;
        private readonly IUserSQLDAL userDAL;

        public HomeController(IUserSQLDAL userDAL, IPotholeDAL potholeDAL, IClaimsDAL claimsDAL) : base(userDAL)
        {
            this.potholeDAL = potholeDAL;
            this.claimsDAL = claimsDAL;
            this.userDAL = userDAL;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult DetailHole(Pothole pothole)
        {
            if (CurrentUser == "EmptyUserName" || CurrentUser != "")
            {
                int potHoleID = potholeDAL.InsertPothole(pothole);
                Session["Pothole_id"] = potHoleID;
                return View("DetailHole", pothole);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public ActionResult DetailManualHole(decimal? latitude, decimal? longitude)
        {
            string lat = Request.Form.Get("Latitude");
            string lng = Request.Form.Get("Longitude");
            Pothole pothole = new Pothole();
            pothole.Latitude = Int32.Parse(lat);
            pothole.Longitude = Int32.Parse(lng);

            if (CurrentUser == "EmptyUserName" || CurrentUser != "")
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

        public ActionResult ViewPotholes(int? page, string id)
        {
            ViewBag.Sorting = id;
            int pageSize = 15;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Pothole> pagedPotholes = null;
            List<Pothole> potholeList = potholeDAL.SortedPotholeList(id);
            pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            if (IsEmployee())
            {
                return View("ViewPotholesForEmp", pagedPotholes);
            }

            return View("ViewPotholes", pagedPotholes);
        }

        public ActionResult ViewPotholesForEmp(int? page, string id)
        {
            int pageSize = 15;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Pothole> pagedPotholes = null;
            List<Pothole> potholeList = potholeDAL.SortedPotholeList(id);
            pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);

            if (IsEmployee())
            {
                return View("ViewPotholesForEmp", pagedPotholes);
            }
            else
            {
                return View("ViewPotholes", pagedPotholes);
            }
        }

        public ActionResult DeletePothole(string id, int? page)
        {
            int pageSize = 15;
            int pageIndex = 1;
            potholeDAL.DeletePothole(id);
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Pothole> pagedPotholes = null;
            List<Pothole> potholeList = potholeDAL.GetAllPotholes();
            pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            if (IsEmployee())
            {
                return View("ViewPotholesForEmp", pagedPotholes);
            }
            else
            {
                return View("ViewPotholes", pagedPotholes);
            }
        }

        public ActionResult UpdatePothole(string id)
        {
            if (IsEmployee())
            {
                return View("UpdatePothole", potholeDAL.GetOnePotholes(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult UpdatePothole(Pothole updatedPothole, int? page)
        {
            potholeDAL.UpdatePothole(updatedPothole);
            string id = updatedPothole.PotholeID.ToString();
            int pageSize = 15;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Pothole> pagedPotholes = null;
            List<Pothole> potholeList = potholeDAL.GetAllPotholes();
            pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            if (IsEmployee())
            {
                return View("ViewPotholesForEmp", pagedPotholes);
            }
            else
            {
                return View("ViewPotholes", pagedPotholes);
            }
        }

        public ActionResult SelectedPothole(string id)
        {

            return View("SinglePothole", potholeDAL.GetOnePotholes(id));

        }

        public ActionResult ViewAllClaims(int? page, string id)
        {
            int pageSize = 15;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<DamageClaimModel> pagedClaims = null;
            List<DamageClaimModel> claimList;
            if (id == null)
            {
                claimList = claimsDAL.AllClaims();
                pagedClaims = claimList.ToPagedList(pageIndex, pageSize);
            }
            //else if (id=="Status")
            //{
            //    claimList = claimsDAL.AllClaims().OrderBy(m => m.Status).ToList();
            //    pagedClaims = claimList.ToPagedList(pageIndex, pageSize);
            //}
            //else if (id=="RepairDate")
            //{
            //    potholeList = potholeDAL.GetAllPotholes().OrderBy(m => m.RepairDate).ToList();
            //    pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            //}
            //else if (id == "InspectionDate")
            //{
            //    potholeList = potholeDAL.GetAllPotholes().OrderBy(m => m.InspectDate).ToList();
            //    pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            //}
            //else if (id == "Severity")
            //{
            //    potholeList = potholeDAL.GetAllPotholes().OrderByDescending(m => m.Severity).ToList();
            //    pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            //}
            //else if (id == "Date")
            //{
            //    potholeList = potholeDAL.GetAllPotholes().OrderByDescending(m => m.DateReported).ToList();
            //    pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            //}
            if (IsEmployee())
            {
                return View("ViewClaims", pagedClaims);
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult ClaimSubmit()
        {
            if (CurrentUser == "EmptyUserName" || CurrentUser != "")
            {
                return View("ClaimSubmit");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }


        }

        [HttpPost]
        public ActionResult ClaimSubmit(DamageClaimModel claim)
        {
            if (CurrentUser == "EmptyUserName" || CurrentUser != "")
            {
                claim.Pothole_ID = (int)Session["Pothole_id"];
                Users user = userDAL.GetUser(CurrentUser);
                claim.UserID = user.UserID;
                claimsDAL.NewClaim(claim);
                int claimID = claimsDAL.NewClaim(claim);
                Session["claimID"] = claimID;
                return View("ClaimConfirmation", claim);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }


        }

        public ActionResult ClaimConfirmation(DamageClaimModel claim)
        {
            return View("ClaimConfirmation", claim);
        }

        public ActionResult AboutUs()
        {
            return View();
        }


    }
}