using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class DamageClaimModel
    {
        

        public decimal Estimated_cost { get; set; }

        public DateTime SubmissionDate { get; set; }

        public int UserID { get; set; }/*not sure if we need specific user ID*/
        public string Description { get; set; }
        public string Status { get; set; }
        public int Pothole_ID { get; set; }
        public int Claim_ID { get; set; }

        public static List<SelectListItem> StatusUpdate
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Submitted", Value = "Submitted" },
                    new SelectListItem { Text = "Reviewed", Value = "Reviewed" },
                    new SelectListItem { Text = "Approved", Value = "Approved" },
                    new SelectListItem { Text = "Denied", Value = "Denied" },
                    new SelectListItem { Text = "Completed", Value = "Completed" },
                };
            }
        }
    }

}