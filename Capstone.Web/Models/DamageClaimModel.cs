using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class DamageClaimModel
    {
        public int UserID { get; set; }/*not sure if we need specific user ID*/
        public string Description { get; set; }
        public decimal Estimated_cost { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Status { get; set; }
        public int Pothole_ID { get; set; }
    }
}