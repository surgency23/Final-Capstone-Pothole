using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class UserSQLDAL
    {
        private string connectionString;

        private const string SQL_CreateUser = @"
        INSERT INTO [dbo].[Users]
           ([Username]
           ,[Password]
           ,[FirstName]
           ,[LastName]
           ,[Is_Employee]
           ,[Email])
         VALUES
           (
           @Username,
           @Password,
           @FirstName,
           @LastName,
           @Is_Employee,
           @Email)";

        public UserSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CreateUser(Users user)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_CreateUser, conn);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Is_Employee", user.Is_Employee);
                    cmd.Parameters.AddWithValue("@Email", user.Email);

                    result = cmd.ExecuteNonQuery();
                }
            }catch(Exception ex)
            {
                throw;
            }
            return (result > 0);
        }
    }
}