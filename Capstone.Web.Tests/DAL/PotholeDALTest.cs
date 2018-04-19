using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;
using System.Data.SqlClient;
using System.Configuration;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.Tests
{
    [TestClass]
    public class PotholeDALTest
    {
        private TransactionScope tran;
        private string connectionString = ConfigurationManager.ConnectionStrings["Potholes"].ConnectionString;
        private int potholeCount = 0;
        private int updateId = 0;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize a new transaction scope. This automatically begins the transaction.
            tran = new TransactionScope();

            // Open a SqlConnection object using the active transaction
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand(@"SELECT COUNT(*) FROM Pothole", conn);
                potholeCount = (int)cmd.ExecuteScalar();

                //Insert a Dummy Record for pothole               
                cmd = new SqlCommand(@"INSERT INTO [dbo].[Pothole] ([Status],[Severity],[Date_Reported],[Picture],[User_ID],[Longitude],[Latitude]) VALUES('reported',3,GETDATE(),NULL,NULL,-83.045653,39.99753999999996); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                updateId = (int)cmd.ExecuteScalar();

                //cmd = new SqlCommand($@"Delete from Pothole where PotHole_ID = {updateId}", conn);


            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
            //tran.Complete();
        }

        [TestMethod]
        public void GetAllPotHolesTest()
        {
            PotholeDAL sql = new PotholeDAL(connectionString);
            List<Pothole> potholeList = sql.GetAllPotholes();
            Assert.AreEqual(potholeCount + 1, potholeList.Count);
        }

        [TestMethod]
        public void InsertPotholeTest()
        {
            PotholeDAL sql = new PotholeDAL(connectionString);
            Pothole pothole = new Pothole
            {
                Status = "Reported",
                Severity = 4,
                DateReported = DateTime.UtcNow.Date,
                Longitude = -83.045653M,
                Latitude= 39.99753999999996M,
            };
            Assert.AreEqual(updateId + 1, sql.InsertPothole(pothole));
        }

        [TestMethod]
        public void UpdatePotholeTest()
        {
            DateTime repair = new DateTime(2018, 5, 30);
            DateTime inspect = new DateTime(2018, 4, 30);

            PotholeDAL sql = new PotholeDAL(connectionString);
            Pothole pothole = new Pothole
            {
                PotholeID = updateId,
                Status = "Inspected",
                Severity = 5,
                RepairDate = null,
                InspectDate = inspect,
            };
            Assert.AreEqual(true, sql.UpdatePothole(pothole));
        }



    }
}
