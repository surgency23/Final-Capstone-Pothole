using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    public class Users
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be 8 or more characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Compare("Email")]
        public string ConfirmEmail { get; set; }

        /// <summary>
        /// not sure how to set this correctly
        /// currently not required
        /// </summary>
        public int Is_Employee { get; set; } //bit identifier

    }
}