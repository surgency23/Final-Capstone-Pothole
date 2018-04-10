using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class PotholeDAL
    {
        private string SQL_GetAllPotholes = "SELECT * FROM Pothole ORDER BY Date_Reported";

        string connectionString;

        public PotholeDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        List<Pothole> GetAllPotholes()
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
    }
}