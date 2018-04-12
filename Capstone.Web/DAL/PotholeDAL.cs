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


        private const string SQL_GetAllPotholes = @"SELECT * FROM Pothole ORDER BY Date_Reported";
        private const string SQL_InsertPothole = @"INSERT INTO [dbo].[Pothole] ([Status],[Severity],[Date_Reported],[Longitude],[Latitude]) VALUES('Reported', @severity,@dateReported,@longitude,@latitude)";
        private const string SQL_DeletePothole = @"Delete from Pothole where PotHole_ID = @potholeID";

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


                        Pothole pothole = new Pothole();

                        pothole.PotholeID = Convert.ToInt32(reader["PotHole_ID"]);
                        pothole.Status = Convert.ToString(reader["Status"]);
                        pothole.Severity = Convert.ToInt32(reader["Severity"]);
                        pothole.DateReported = Convert.ToDateTime(reader["Date_Reported"]);
                        pothole.Longitude = Convert.ToDecimal(reader["Longitude"]);
                        pothole.Latitude = Convert.ToDecimal(reader["Latitude"]);

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
                    cmd.Parameters.AddWithValue("@severity", Convert.ToInt32(newPothole.Severity));


                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool DeletePothole(string id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_DeletePothole, conn);
                    cmd.Parameters.AddWithValue("@potholeID", id);

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