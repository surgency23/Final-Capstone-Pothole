using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;
using System.Data.SqlClient;
using System.Configuration;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Tests
{
    [TestClass]
    public class PotholeDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog = Potholes; Integrated Security = True";
        private int potholeCount = 0;

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
                cmd = new SqlCommand(@"INSERT INTO [dbo].[Pothole] ([Status],[Severity],[Date_Reported],[Picture],[User_ID],[Longitude],[Latitude]) VALUES('reported',3,GETDATE(),NULL,NULL,-83.045653,39.99753999999996)", conn);
                cmd.ExecuteNonQuery();

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
            Assert.AreEqual(true, sql.InsertPothole(pothole));
        }


    }
}
