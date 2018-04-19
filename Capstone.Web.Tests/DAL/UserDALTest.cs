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
    [TestClass()]
    public class UserDALTest
    {
        private TransactionScope tran;
        private string connectionString = ConfigurationManager.ConnectionStrings["Potholes"].ConnectionString;
        private int userID;


        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand(@"
                     INSERT INTO [dbo].[Users]
                     ([Username]
                     ,[Password]
                     ,[FirstName]
                     ,[LastName]
                     ,[Is_Employee]
                     ,[Email])
                        VALUES
                    ('testUser',
                    'Password123',
                    'FirstName',
                    'LastName',
                    1,
                    'TestEmail@test.com')", conn);
                userID = cmd.ExecuteNonQuery();

            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void FindingANDCreatingTestUser()
        {
            UserSQLDAL userDAL = new UserSQLDAL(connectionString);
            Users user = new Users
            {
                Username = "testUser",
                Password = "Password123",
                FirstName = "Test",
                LastName = "LastName",
                Is_Employee = 1,
                Email = "TestEmail@test.com"

            };
            Assert.AreEqual(true, userDAL.CreateUser(user));
        }

        [TestMethod]
        public void TestingChangePassword()
        {
            UserSQLDAL userDAL = new UserSQLDAL(connectionString);
            Users user = new Users
            {
                Username = "testUser",
                Password = "Password123",
                FirstName = "Test",
                LastName = "LastName",
                Is_Employee = 1,
                Email = "TestEmail@test.com"
            };
            

            Assert.AreEqual(true, userDAL.ChangePassword("testUser", "NewPassword"));
        }

        [TestMethod]
        public void IsRegisteredUser()
        {
            UserSQLDAL userDAL = new UserSQLDAL(connectionString);
            Users user = new Users
            {
                Username = "testUser",
                Password = "Password123",
                FirstName = "Test",
                LastName = "LastName",
                Is_Employee = 1,
                Email = "TestEmail@test.com"
            };
            Assert.ReferenceEquals(user, userDAL.GetUser(user.Username, user.Password));
        }

        [TestMethod]
        public void IsRegisteredUserByUsername()
        {
            UserSQLDAL userDAL = new UserSQLDAL(connectionString);
            Users user = new Users
            {
                Username = "testUser",
                Password = "Password123",
                FirstName = "Test",
                LastName = "LastName",
                Is_Employee = 1,
                Email = "TestEmail@test.com"
            };
            Assert.ReferenceEquals(user, userDAL.GetUser(user.Username));
        }
    }
}

