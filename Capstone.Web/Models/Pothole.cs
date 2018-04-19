using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class Pothole
    {
        public int PotholeID { get; set; }
        public string Status { get; set; }
        public int Severity { get; set; }
        public DateTime DateReported { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime? InspectDate { get; set; }
        public DateTime? RepairDate { get; set; }

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

        public static List<SelectListItem> StatusUpdate
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Reported", Value = "Reported" },
                    new SelectListItem { Text = "Inspection Scheduled", Value = "Inspection Scheduled" },
                    new SelectListItem { Text = "Inspected", Value = "Inspected" },
                    new SelectListItem { Text = "Repair Scheduled", Value = "Repair Scheduled" },
                    new SelectListItem { Text = "Repaired", Value = "Repaired" },
                };
            }
        }

        public string ReturnSeverity(int severity)
        {
            string severityString = "";
            switch (severity)
            {
                case 1:
                    severityString = "Minor";
                    break;
                case 2:
                    severityString = "Moderate";
                    break;
                case 3:
                    severityString = "Hazard";
                    break;
                case 4:
                    severityString = "Severe";
                    break;
                case 5:
                    severityString = "Extreme Danger";
                    break;
            }
            return severityString;
        }
    }
}