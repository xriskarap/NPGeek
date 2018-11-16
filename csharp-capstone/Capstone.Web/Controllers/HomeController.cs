using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Capstone.Web.Extensions;

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


        [HttpGet]
        public IActionResult Weather(string id)
        {
            var currentWeather = weatherDal.GetForecast(id);
            var degree = GetCurrentDegree();

            if (degree == "C")
            {
                foreach(var weather in currentWeather)
                {
                    weather.Degree = "C";
                    weather.High = weather.ConvertToCelsius(weather.High);
                    weather.Low = weather.ConvertToCelsius(weather.Low);
                }
            }
            return View("Detail");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Weather(string id, string degree)
        {
            SaveCurrentDegree(degree);
            return RedirectToAction("Weather", new { id });
        }


        private void SaveCurrentDegree(string degree)
        {
            HttpContext.Session.Set<string>("Degree", degree);
        }


        private string GetCurrentDegree()
        {
            string currentDegree = HttpContext.Session.Get<string>("Degree");
            if(currentDegree == null)
            {
                currentDegree = "F";
                HttpContext.Session.Set("Degree", currentDegree);
            }
            return currentDegree;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
