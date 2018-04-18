using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;


namespace Capstone.Web.DAL
{
    public interface IPotholeDAL
    {
        List<Pothole> GetAllPotholes();
        int InsertPothole(Pothole newPothole);
        bool DeletePothole(string id);
        bool UpdatePothole(Pothole update);
        Pothole GetOnePotholes(string id);
        List<Pothole> SortedPotholeList(string id);
    }
}
