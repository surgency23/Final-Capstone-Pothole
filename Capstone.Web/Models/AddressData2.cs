using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class AddressData2
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public int Severity { get; set; }

        public static List<SelectListItem> SeverityLevels
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Minor", Value = "1" },
                    new SelectListItem { Text = "Moderate", Value = "2" },
                    new SelectListItem { Text = "Hazard", Value = "3" },
                    new SelectListItem { Text = "Severe", Value = "4" },
                    new SelectListItem { Text = "Extreme Danger", Value = "5" },
                };
            }
        }

        public override string ToString()
        {
            return String.Format(
                CultureInfo.InvariantCulture,
                "{0}{1}{2}{3}{4}",
                Address != null ? Address + ", " : "",
                City != null ? City + ", " : "",
                State != null ? State + ", " : "",
                Zip != null ? Zip + ", " : "",
                Country != null ? Country : "").TrimEnd(' ', ',');
        }
    }
}