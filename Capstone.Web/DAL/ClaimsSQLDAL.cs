using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class ClaimsSQLDAL : IClaimsDAL
    {
        private const string SQL_FIND_ALL_CLAIMS = @"SELECT claims.User_ID, claims.Claims_ID, claims.Description, claims.Estimated_Cost, claims.PotHole_ID, claims.Status, claims.Submission_Date, Pothole.Picture  
                                FROM Claims 
                                JOIN Pothole on Pothole.PotHole_ID = claims.PotHole_ID";
        private const string SQL_ALL_CLAIMS_AT_POTHOLE = @"SELECT claims.User_ID, claims.Claims_ID, claims.Description, claims.Estimated_Cost, claims.PotHole_ID, claims.Status, claims.Submission_Date, Pothole.Picture  
                                FROM Claims 
                                JOIN Pothole on Pothole.PotHole_ID = claims.PotHole_ID
                                WHERE @pothole_ID = Claims.PotHole_ID";

        private const string SQL_Create_New_Damage_Claim = @"INSERT INTO [dbo].[Claims]
                             ([User_ID]
                            ,[Description]
                            ,[Estimated_Cost]
                            ,[Submission_Date]
                            ,[Status]
                            ,[PotHole_ID])
                            VALUES
                         (@User_ID, @Description, @Estimated_Cost, @Submission_Date, @Status, @PotHole_ID); SELECT CAST(SCOPE_IDENTITY() as int);";


        string connectionString;

        public ClaimsSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<DamageClaimModel> AllClaims()
        {
            List<DamageClaimModel> claimList = new List<DamageClaimModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_FIND_ALL_CLAIMS, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {


                        DamageClaimModel newClaim = new DamageClaimModel();

                        newClaim.Claim_ID = Convert.ToInt32(reader["Claims_ID"]);
                        newClaim.UserID = Convert.ToInt32(reader["User_ID"]);
                        newClaim.SubmissionDate = Convert.ToDateTime(reader["Submission_Date"]);
                        newClaim.Status = Convert.ToString(reader["Status"]);
                        newClaim.Pothole_ID = Convert.ToInt32(reader["PotHole_ID"]);
                        newClaim.Estimated_cost = Convert.ToDecimal(reader["Estimated_Cost"]);
                        newClaim.Description = Convert.ToString(reader["Description"]);

                        claimList.Add(newClaim);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return claimList;
        }
        
        public List<DamageClaimModel> AllClaimsByPothole (int Pothole_ID)
        {

            List<DamageClaimModel> claimList = new List<DamageClaimModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_ALL_CLAIMS_AT_POTHOLE, conn);
                    cmd.Parameters.AddWithValue("@pothole_ID", Pothole_ID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {


                        DamageClaimModel newClaim = new DamageClaimModel();

                        newClaim.UserID = Convert.ToInt32(reader["claims.User_ID"]);
                        newClaim.SubmissionDate = Convert.ToDateTime(reader["claims.Submission_Date"]);
                        newClaim.Status = Convert.ToString(reader["claims.Status"]);
                        newClaim.Estimated_cost = Convert.ToDecimal(reader["claims.Estimated_Cost"]);
                        newClaim.Description = Convert.ToString(reader["claims.Description"]);

                        claimList.Add(newClaim);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return claimList;
        }

        public int NewClaim(DamageClaimModel newClaim)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_Create_New_Damage_Claim, conn);
                    cmd.Parameters.AddWithValue("@User_ID", Convert.ToInt32(newClaim.UserID));
                    cmd.Parameters.AddWithValue("@Estimated_Cost", Convert.ToDecimal(newClaim.Estimated_cost));
                    cmd.Parameters.AddWithValue("@Description", Convert.ToString(newClaim.Description));
                    cmd.Parameters.AddWithValue("@Submission_Date", Convert.ToDateTime(newClaim.SubmissionDate));
                    cmd.Parameters.AddWithValue("@Status","Reported");
                    cmd.Parameters.AddWithValue("@PotHole_ID", Convert.ToInt32(newClaim.Pothole_ID));

                    int rowAffected = (int)cmd.ExecuteScalar();

                    return rowAffected;

                }
            }catch(Exception e)
            {
                throw;
            }

        }


    }
}