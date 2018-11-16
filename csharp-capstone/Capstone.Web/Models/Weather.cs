using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }
        public string Degree { get; set; }

        public int ConvertToCelsius(double temp)
        {
            double celsius = 0.0;
            celsius = (((temp - 32) * (5.0 / 9.0)));
            return (int)celsius;
        }
    }



}
