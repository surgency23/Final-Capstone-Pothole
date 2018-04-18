using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Configuration;

namespace Capstone.Web.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        //[TestMethod()]
        //public void HomeController_IndexAction_ReturnIndexView()
        //{
        //    PotholeDAL potholeDAL = new PotholeDAL(ConfigurationManager.ConnectionStrings["PotHoles"].ConnectionString);
        //    UserSQLDAL userDAL = new UserSQLDAL(ConfigurationManager.ConnectionStrings["PotHoles"].ConnectionString);
        //    ClaimsSQLDAL claimsDAL = new ClaimsSQLDAL(ConfigurationManager.ConnectionStrings["PotHoles"].ConnectionString);
        //    //Arrange
        //    HomeController controller = new HomeController(userDAL, potholeDAL, claimsDAL);

        //    //Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    //Assert
        //    Assert.AreEqual("Index", result.ViewName);
        //}
    }
}