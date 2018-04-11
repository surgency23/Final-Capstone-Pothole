using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class TopController : Controller
    {
        private const string UsernameKey = "EmptyUserName";
        private IPotholeDAL potholeDAL;

        public TopController(IPotholeDAL potholeDAL)
        {
            this.potholeDAL = potholeDAL;
        }
        // GET: Top
        public bool IsAuthenticated
        {
            get
            {
                return Session[UsernameKey] != null;
            }
        }
    }
}