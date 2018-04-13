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
        private IUserSQLDAL userDAL;

        public TopController(IUserSQLDAL userDAL)
        {
            this.userDAL = userDAL;
        }

        //Instantiates logged in session for user
        public void LogUserIn(string username)
        {
            Session["UsernameKey"] = username;
        }

        public string CurrentUser
        {
            get
            {
                string username = string.Empty;

                //Check to see if user cookie exists, if not create it
                if (Session["UsernameKey"] != null)
                {
                    username = (string)Session["UsernameKey"];
                }
                return username;
            }
        }
        
        protected bool IsEmployee()
        {
            bool result = false;

            if(Session["isEmployee"] == null)
            {
                result = false;
            }
            else if((int)Session["isEmployee"] == 1)
            {
                result = true;
            }
            else if ((int)Session["isEmployee"] == 0)
            {
                result = false;
            }

            return result;
        }
        public void LogUserOut()
        {
            Session.Abandon();
            //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        }
    }
}