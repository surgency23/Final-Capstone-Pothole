using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class UserSQLDAL : IUserSQLDAL
    {
        private string connectionString;
        private const string SQL_GetUser = @"SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

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
            }
            catch (Exception ex)
            {
                throw;
            }
            return (result > 0);
        }

        public Users GetUser(string username, string password)
        {
            Users user = null;

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetUser, conn);
                    cmd.Parameters.AddWithValue("@Username", Convert.ToString(username));
                    cmd.Parameters.AddWithValue("@Password", Convert.ToString(password));
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new Users
                        {
                            Username = Convert.ToString(reader["Username"]),
                            Password = Convert.ToString(reader["Password"]),
                        };
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return user;
        }

        public bool ChangePassword(string username, string newPassword)
        {
            try
            {
                string SQL = $"UPDATE Users SET Password = '{newPassword}' WHERE Username = '{username}'";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL, conn);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public Users GetUser(string username)
        {
            Users user = null;

            try
            {
                string sql = $"SELECT TOP 1 * FROM users WHERE username = '{username}'";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new Users
                        {
                            Username = Convert.ToString(reader["Username"]),
                            Password = Convert.ToString(reader["password"]),
                            Is_Employee = Convert.ToInt32(reader["Is_Employee"]),
                            UserID = Convert.ToInt32(reader["User_ID"])
                        };
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return user;
        }
    }
}
