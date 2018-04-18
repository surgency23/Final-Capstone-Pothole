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
        private const string SQL_InsertPothole = @"INSERT INTO [dbo].[Pothole] ([Status],[Severity],[Date_Reported],[Longitude],[Latitude]) VALUES('Reported', @severity,@dateReported,@longitude,@latitude); SELECT CAST(SCOPE_IDENTITY() as int);";
        private const string SQL_DeletePothole = @"Delete from Pothole where PotHole_ID = @potholeID";
        private const string SQL_UpdatePothole = @"UPDATE[dbo].[Pothole] SET[Status] = @status,[Severity] = @severity,[Repair_Date] = @repairDate,[Inspect_Date] = @inspectDate WHERE PotHole_ID = @potholeid";
        private const string SQL_GetSinglePothole = @"SELECT * FROM Pothole WHERE PotHole_ID = @id";


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
                        if (reader["Repair_Date"] is DBNull)
                        {
                            pothole.RepairDate = null;
                        }
                        else
                        {
                            pothole.RepairDate = Convert.ToDateTime(reader["Repair_Date"]);
                        }
                        if (reader["Inspect_Date"] is DBNull)
                        {
                            pothole.InspectDate = null;
                        }
                        else
                        {
                            pothole.InspectDate = Convert.ToDateTime(reader["Inspect_Date"]);
                        }

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

        public int InsertPothole(Pothole newPothole)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_InsertPothole, conn);
                    cmd.Parameters.AddWithValue("@severity", Convert.ToInt32(newPothole.Severity));
                    cmd.Parameters.AddWithValue("@dateReported", DateTime.UtcNow.Date);
                    cmd.Parameters.AddWithValue("@longitude", newPothole.Longitude);
                    cmd.Parameters.AddWithValue("@latitude", newPothole.Latitude);

                    int rowAffected = (int)cmd.ExecuteScalar();

                    return (rowAffected);
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

        public bool UpdatePothole(Pothole update)
        {

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_UpdatePothole, conn);

                    cmd.Parameters.AddWithValue("@potholeid", update.PotholeID);
                    cmd.Parameters.AddWithValue("@status", update.Status);
                    cmd.Parameters.AddWithValue("@severity", update.Severity);
                    if (update.RepairDate == null)
                    {
                        cmd.Parameters.AddWithValue("@repairDate", DBNull.Value).IsNullable = true;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@repairDate", update.RepairDate).IsNullable = true;
                    }
                    if (update.InspectDate == null)
                    {
                        cmd.Parameters.AddWithValue("@inspectDate", DBNull.Value).IsNullable = true;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@inspectDate", update.InspectDate).IsNullable = true;
                    }
                    int rowsAffected = cmd.ExecuteNonQuery();


                    return (rowsAffected > 0);

                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public Pothole GetOnePotholes(string id)
        {
            Pothole pothole = new Pothole();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetSinglePothole, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        pothole.PotholeID = Convert.ToInt32(reader["PotHole_ID"]);
                        pothole.Status = Convert.ToString(reader["Status"]);
                        pothole.Severity = Convert.ToInt32(reader["Severity"]);
                        pothole.DateReported = Convert.ToDateTime(reader["Date_Reported"]);
                        if (reader["Repair_Date"] is DBNull)
                        {
                            pothole.RepairDate = null;
                        }
                        else
                        {
                            pothole.RepairDate = Convert.ToDateTime(reader["Repair_Date"]);
                        }
                        if (reader["Inspect_Date"] is DBNull)
                        {
                            pothole.InspectDate = null;
                        }
                        else
                        {
                            pothole.InspectDate = Convert.ToDateTime(reader["Inspect_Date"]);
                        }

                        pothole.Longitude = Convert.ToDecimal(reader["Longitude"]);
                        pothole.Latitude = Convert.ToDecimal(reader["Latitude"]);

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return pothole;
        }

    }
    
}