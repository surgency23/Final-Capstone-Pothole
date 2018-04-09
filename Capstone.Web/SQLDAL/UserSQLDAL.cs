using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Capstone.Web.Models;

namespace Capstone.Web.SQLDAL
{
    public class UserSQLDAL
    {
        private string connectionString;

        private string SQL_CreateUser =
            @"USE [Potholes]
            GO
            INSERT INTO [dbo].[Users]
           ([Username]
           ,[Password]
           ,[FirstName]
           ,[LastName]
           ,[Is_Employee])
            VALUES
           (@Username, 
           @Password,
           @FirstName,
           @LastName,
           @Is_Employee,)
             GO";

        public UserSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }



        public bool RegisterNewUser(Users user)
        {
            int result = 0;

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(RegisterNewUser, conn);
                    cmd.Parameter.AddWithValue("@Username", user.Username);
                    cmd.Parameter.AddWithValue("@Password", user.Password);
                    cmd.Parameter.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameter.AddWithValue("@LastName", user.LastName);
                    cmd.Parameter.AddWithValue("@Is_Employee", user.Is_Employee);
                    result = cmd.ExecuteScalar();

                }

            }catch()
        }
    }
}