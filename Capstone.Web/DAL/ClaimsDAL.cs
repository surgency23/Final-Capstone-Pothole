using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class ClaimsDAL
    {
        private const string SQL_FIND_CERTAIN_CLAIM = @"SELECT claims.User_ID, claims.Claims_ID, claims.Description, claims.Estimated_Cost, claims.PotHole_ID, claims.Status, claims.Submission_Date, Pothole.Picture  
                                FROM Claims 
                                JOIN Pothole on Pothole.PotHole_ID = claims.PotHole_ID";
    }
}