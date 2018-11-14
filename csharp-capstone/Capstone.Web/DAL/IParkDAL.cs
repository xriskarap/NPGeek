using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IParkDAL
    {
        /// <summary>
        /// Gets all of the parks.
        /// </summary>
        /// <returns></returns>
        IList<Park> GetParks();

        /// <summary>
        /// Gets a single park.
        /// </summary>
        /// <param name="parkCode"></param>
        /// <returns></returns>
        Park GetPark(string parkCode);

    }
}
