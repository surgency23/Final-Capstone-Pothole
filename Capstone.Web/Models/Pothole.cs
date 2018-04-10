using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Pothole
    {
        public int PotholeID { get; set; }
        public int Severity { get; set; }
        public DateTime DateReported { get; set; }
        public int UserID { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

    }
}