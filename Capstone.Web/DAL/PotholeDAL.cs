using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class PotholeDAL : IPotholeDAL
    {
        private string SQL_GetAllPotholes = @"SELECT * FROM Pothole ORDER BY Date_Reported";
        private string SQL_InsertPothole = @"INSERT INTO [dbo].[Pothole] ([Status],[Severity],[Date_Reported],[User_ID],[Longitude],[Latitude]) VALUES('Reported', @severity,@dateReported,@userid,@longitude,@latitude)";

        string connectionString;

        public PotholeDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Pothole> GetAllPotholes()
        {
            List<Pothole> potholeList = new List<Pothole>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllPotholes, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Pothole pothole = new Pothole
                        {
                            PotholeID = Convert.ToInt32(reader["PotHole_ID"]),
                            Status = Convert.ToString(reader["Status"]),
                            Severity = Convert.ToInt32(reader["Severity"]),
                            DateReported = Convert.ToDateTime(reader["Date_Reported"]),
                            UserID = Convert.ToInt32(reader["User_ID"]),
                            Longitude = Convert.ToDecimal(reader["Longitude"]),
                            Latitude = Convert.ToDecimal(reader["Latitude"])
                        };
                        potholeList.Add(pothole);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return potholeList;
        }

        public bool InsertPothole(Pothole newPothole)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_InsertPothole, conn);
                    cmd.Parameters.AddWithValue("@severity", newPothole.Severity);
                    cmd.Parameters.AddWithValue("@dateReported", DateTime.UtcNow.Date);
                    cmd.Parameters.AddWithValue("@userid", newPothole.UserID);
                    cmd.Parameters.AddWithValue("@longitude", newPothole.Longitude);
                    cmd.Parameters.AddWithValue("@latitude", newPothole.Latitude);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}