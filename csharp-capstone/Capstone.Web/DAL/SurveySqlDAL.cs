using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAL : ISurveyDAL
    {

        private string connectionString;
        public SurveySqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SaveSurveyPost(SurveyPost surveyPost)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Insert Into survey_result Values (@parkCode, @emailAddress, @state, @activityLevel)", conn);
                    cmd.Parameters.AddWithValue("@parkCode", surveyPost.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", surveyPost.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", surveyPost.State);
                    cmd.Parameters.AddWithValue("@activityLevel", surveyPost.ActivityLevel);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, int> GetSurveyPosts()
        {
            Dictionary<string, int> surveyResults = new Dictionary<string, int>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT parkCode, count(*) AS votes FROM survey_result GROUP BY parkCode ORDER BY votes desc, parkCode;", conn);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        surveyResults.Add(Convert.ToString(reader["parkCode"]),
                            Convert.ToInt32(reader["votes"]));
                    };
                }
                return surveyResults;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
