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

        public ActionResult ViewPotholes(int? page,string id)
        {
            int pageSize = 15;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Pothole> pagedPotholes = null;
            List<Pothole> potholeList;
            if (id ==null)
            {
                potholeList=potholeDAL.GetAllPotholes();
                pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            }
            else if (id=="Status")
            {
                potholeList = potholeDAL.GetAllPotholes().OrderBy(m => m.Status).ToList();
                pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            }
            else if (id=="RepairDate")
            {
                potholeList = potholeDAL.GetAllPotholes().OrderBy(m => m.RepairDate).ToList();
                pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            }
            else if (id == "InspectionDate")
            {
                potholeList = potholeDAL.GetAllPotholes().OrderBy(m => m.InspectDate).ToList();
                pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            }
            else if (id == "Severity")
            {
                potholeList = potholeDAL.GetAllPotholes().OrderByDescending(m => m.Severity).ToList();
                pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            }
            else if (id == "Date")
            {
                potholeList = potholeDAL.GetAllPotholes().OrderByDescending(m => m.DateReported).ToList();
                pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            }



            if (IsEmployee())
            {
                return View("ViewPotholesForEmp",pagedPotholes );
            }

            return View("ViewPotholes", pagedPotholes);
        }

        public ActionResult ViewPotholesForEmp(int? page,string id)
        {
            int pageSize = 15;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Pothole> pagedPotholes = null;
            List<Pothole> potholeList = potholeDAL.GetAllPotholes();
            if (id == null)
            {
                potholeList = potholeDAL.GetAllPotholes();
                pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            }
            else if (id == "Status")
            {
                potholeList = potholeDAL.GetAllPotholes().OrderBy(m => m.Status).ToList();
                pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            }
            else if (id == "RepairDate")
            {
                potholeList = potholeDAL.GetAllPotholes().OrderBy(m => m.RepairDate).ToList();
                pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            }
            else if (id == "InspectionDate")
            {
                potholeList = potholeDAL.GetAllPotholes().OrderBy(m => m.InspectDate).ToList();
                pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            }
            else if (id == "Severity")
            {
                potholeList = potholeDAL.GetAllPotholes().OrderByDescending(m => m.Severity).ToList();
                pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            }
            else if (id == "Date")
            {
                potholeList = potholeDAL.GetAllPotholes().OrderByDescending(m => m.DateReported).ToList();
                pagedPotholes = potholeList.ToPagedList(pageIndex, pageSize);
            }
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
        public ActionResult UpdatePothole(Pothole updatedPothole,int? page)
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
    }
}