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

        public void AddSurveyPost(SurveyPost surveyPost)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
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
            catch(SqlException ex)
            {
                throw ex;
            }
        }

        public IList<SurveyPost> GetSurveyPosts()
        {
            List<SurveyPost> surveys = new List<SurveyPost>();

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT parkCode count(*) as votes FROM survey_result GROUP BY votes descending;", conn);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var surveyPost = new SurveyPost()
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            EmailAddress = Convert.ToString(reader["emailAddress"]),
                            State = Convert.ToString(reader["state"]),
                            ActivityLevel = Convert.ToString(reader["activityLevel"])
                        };
                        surveys.Add(surveyPost);
                    }
                }
                return surveys;
            }
            catch(SqlException ex)
            {
                throw ex;
            }
        }
    }
}
