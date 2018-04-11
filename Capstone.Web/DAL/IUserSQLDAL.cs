using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Controllers;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IUserSQLDAL
    {
        bool CreateUser(Users user);
        Users GetUser(string username, string password);
        bool ChangePassword(string username, string newPassword);
        Users GetUser(string username);
    }
}
