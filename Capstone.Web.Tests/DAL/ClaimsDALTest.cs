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

namespace Capstone.Web.Tests.DAL
{
    [TestClass]
    public class ClaimsDALTest
    {
        private TransactionScope tran;
        private string connectionString = ConfigurationManager.ConnectionStrings["Potholes"].ConnectionString;
        private int claimID = 0;
        private int claimsCount = 0;
        private int potHoleID = 0;
        private int potholeClaims = 0;


        [TestInitialize]
        public void TestInitialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand(@"Select COUNT(*) FROM CLAIMS", conn);
                claimsCount = (int)cmd.ExecuteScalar();


                //fake Pothole to test claims by pothole method
                cmd = new SqlCommand(@"INSERT INTO [dbo].[Pothole] ([Status],[Severity],[Date_Reported],[Picture],[User_ID],[Longitude],[Latitude]) VALUES('reported',3,GETDATE(),NULL,NULL,-83.045653,39.99753999999996); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                potHoleID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand($@"INSERT INTO [dbo].[Claims]
                             ([User_ID]
                            ,[Description]
                            ,[Estimated_Cost]
                            ,[Submission_Date]
                            ,[Status]
                            ,[PotHole_ID])
                            VALUES
                         ({potHoleID}, 'Test Claim', 500, GETDATE(), 'Submitted', 77); 
                            SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                claimID = (int)cmd.ExecuteScalar();


                //for some reason this causes all the other test initialize methods to fail.....
                cmd = new SqlCommand($@"Select Count(*) FROM CLAIMS WHERE {potHoleID} = Claims.PotHole_ID", conn);
                potholeClaims = (int)cmd.ExecuteScalar();





            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void NewClaimTest()
        {
            ClaimsSQLDAL sql = new ClaimsSQLDAL(connectionString);
            DamageClaimModel claim = new DamageClaimModel()
            {
                UserID = 2,
                Description = "testClaim",
                Estimated_cost = 200,
                SubmissionDate = DateTime.UtcNow.Date,
                Status = "Submitted",
                Pothole_ID = 77

            };
            Assert.AreEqual(claimID + 1, sql.NewClaim(claim));
        }
        
        [TestMethod]
        public void AllClaimsTest()
        {
            ClaimsSQLDAL sql = new ClaimsSQLDAL(connectionString);
            List<DamageClaimModel> claimList = sql.AllClaims();
            Assert.AreEqual(claimsCount + 1, claimList.Count);
        }


        // finding claim by pothole test does not work correctly with the other tests so lets just not use it 
        [TestMethod]
        public void ClaimsByPotHoleTest()
        {
            ClaimsSQLDAL sql = new ClaimsSQLDAL(connectionString);
            List<DamageClaimModel> claimsByPothole = sql.AllClaimsByPothole(potHoleID);
            Assert.AreEqual(potholeClaims, claimsByPothole.Count);
        }


    }
}
