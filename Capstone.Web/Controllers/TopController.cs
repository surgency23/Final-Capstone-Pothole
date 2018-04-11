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
        private IUserSQLDAL userDAL;

        public TopController(IUserSQLDAL userDAL)
        {
            this.userDAL = userDAL;
        }

        // GET: Top
        public bool IsAuthenticated
        {
            get
            {
                return Session[UsernameKey] != null;
            }
        }

        //Instantiates logged in session for user
        public void LogUserIn(string username)
        {
            Session[UsernameKey] = username;
        }

        public string CurrentUser
        {
            get
            {
                string username = string.Empty;

                //Check to see if user cookie exists, if not create it
                if (Session[UsernameKey] != null)
                {
                    username = (string)Session[UsernameKey];
                }
                return username;
            }
        }

        public void LogUserOut()
        {
            Session.Abandon();
            //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        }
    }
}