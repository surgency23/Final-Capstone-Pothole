using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IClaimsDAL
    {
        List<DamageClaimModel> AllClaims();
        List<DamageClaimModel> AllClaimsByPothole(string Pothole_ID);
        bool NewClaim(DamageClaimModel newClaim);

    }
}
