using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Users
    {
        private string Username { get; set; }
        private string Password { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private bool Is_Employee { get; set; } //bit identifier

        public string username;
        public string password;
        public string firstName;
        public string lastName;
        public int is_Employee; // bit pulled from DATABASE

    }
}