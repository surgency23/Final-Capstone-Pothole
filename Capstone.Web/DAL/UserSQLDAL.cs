using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class UserSQLDAL
    {
        private string connectionString;

        public UserSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

    }
}