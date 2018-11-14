using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class ParkSqlDal : IParkDAL
    {

        private string connectionString;
        public ParkSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Park GetPark(string parkCode)
        {
            Park park = new Park();
            return GetParks().FirstOrDefault(p => park.ParkCode == parkCode);
        }

        public IList<Park> GetParks()
        {
            IList<Park> parks = new List<Park>();

            try
            {

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select * From park", conn);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park park = new Park()
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            State = Convert.ToString(reader["state"]),
                            Acreage = Convert.ToInt32(reader["acreage"]),
                            ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]),
                            MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]),
                            NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                            Climate = Convert.ToString(reader["climate"]),
                            YearFounded = Convert.ToInt32(reader["yearFounded"]),
                            AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                            Quote = Convert.ToString(reader["inspirationalQuote"]),
                            QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                            Description = Convert.ToString(reader["parkDescription"]),
                            EntryFee = Convert.ToDecimal(reader["entryFee"]),
                            NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"])
                        };
                        parks.Add(park);
                    }
                }

            }
            catch(SqlException ex)
            {
                throw ex;
            }
            return parks;
        }
    }
}
