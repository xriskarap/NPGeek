using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class DetailView
    {
        public Park Park { get; set; }

        public Weather Forecast { get; set; }

        public IList<Park> ParkList = new List<Park>();


        public IList<Weather> fiveDay = new List<Weather>();
    }
}