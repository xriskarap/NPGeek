using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {

        private IParkDAL dal;
        private IWeatherDAL weatherDal;

        public HomeController(IParkDAL dal, IWeatherDAL weatherDal)
        {
            this.dal = dal;
            this.weatherDal = weatherDal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var parks = dal.GetParks();
            return View(parks);
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            DetailView detailView = new DetailView();
            var park = dal.GetPark(id);
            detailView.Park = park;
            var weather = weatherDal.GetForecast(id);
            detailView.fiveDay = weather;
            return View(detailView);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
