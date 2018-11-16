using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private IParkDAL parkDAL;
        private ISurveyDAL surveyDAL;
        public SurveyController(ISurveyDAL surveyDAL, IParkDAL parkDAL)
        {
            this.surveyDAL = surveyDAL;
            this.parkDAL = parkDAL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Dictionary<string, int> surveyCount = surveyDAL.GetSurveyPosts();
            IList<SurveyPark> surveyParks = new List<SurveyPark>();

            foreach (KeyValuePair<string, int> kvp in surveyCount)
            {
                var parkCount = parkDAL.GetPark(kvp.Key);
                SurveyPark surveyList = new SurveyPark();
                surveyList.surveyPark = parkCount;
                surveyList.Count = kvp.Value;
                surveyParks.Add(surveyList);
            }
            return View(surveyParks);
        }

        [HttpGet]
        public IActionResult AddSurveyPost()
        {
            var getParks = parkDAL.GetParks();
            IList<Park> parkList = new List<Park>();
            foreach (Park park in getParks)
            {
                parkList.Add(park);
            }
            SurveyPost surveyPost = new SurveyPost(); // Get all Parks using park Dal  ... Turn the List of Parks into a list of select list items ... Take the list of select list items and set a list of parks in the model
            List<SelectListItem> natParks = new List<SelectListItem>();

            foreach (Park park in parkList)
            {
                surveyPost.SurveyParks.Add(new SelectListItem() { Text = park.ParkName, Value = park.ParkCode });
            }
            return View(surveyPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSurveyPost(SurveyPost newSurvey)
        {
            if (ModelState.IsValid)
            {
           surveyDAL.SaveSurveyPost(newSurvey);
            return RedirectToAction("Index", "Survey", new { ParkCode = newSurvey.ParkCode, EmailAddress = newSurvey.EmailAddress, State = newSurvey.State, ActivityLevel = newSurvey.ActivityLevel });
            }
            else
            {
                return View(newSurvey);
            }
        }

        public IList<SelectListItem> GetParkName()
        {
            var parkList = parkDAL.GetParks();

            List<SelectListItem> natParks = new List<SelectListItem>();

            foreach(Park park in parkList)
            {
                natParks.Add(new SelectListItem() { Text = park.ParkName, Value = park.ParkCode });
            }
            return natParks;
        }

        public IList<SelectListItem> States = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Alabama", Value = "AL" },
                new SelectListItem() {Text = "Alaska", Value = "AK" },
                new SelectListItem() {Text = "Arizona", Value = "AZ" },
                new SelectListItem() {Text = "Arkansas", Value = "AR" },
                new SelectListItem() {Text = "California", Value = "CA" },
                new SelectListItem() {Text = "Colorado", Value = "CO" },
                new SelectListItem() {Text = "Connecticut", Value = "CT" },
                new SelectListItem() {Text = "Delaware", Value = "DE" },
                new SelectListItem() {Text = "Florida", Value = "FL" },
                new SelectListItem() {Text = "Georgia", Value = "GA" },
                new SelectListItem() {Text = "Hawaii", Value = "HI" },
                new SelectListItem() {Text = "Idaho", Value = "ID" },
                new SelectListItem() {Text = "Illinois", Value = "IL" },
                new SelectListItem() {Text = "Indiana", Value = "IN" },
                new SelectListItem() {Text = "Iowa", Value = "IA" },
                new SelectListItem() {Text = "Kansas", Value = "KS" },
                new SelectListItem() {Text = "Kentucky", Value = "KY" },
                new SelectListItem() {Text = "Louisiana", Value = "LA" },
                new SelectListItem() {Text = "Maine", Value = "ME" },
                new SelectListItem() {Text = "Maryland", Value = "MD" },
                new SelectListItem() {Text = "Massachusetts", Value = "MA" },
                new SelectListItem() {Text = "Michigan", Value = "MI" },
                new SelectListItem() {Text = "Minnesota", Value = "MN" },
                new SelectListItem() {Text = "Mississippi", Value = "MS" },
                new SelectListItem() {Text = "Missouri", Value = "MO" },
                new SelectListItem() {Text = "Montana", Value = "MT" },
                new SelectListItem() {Text = "Nebraska", Value = "NE" },
                new SelectListItem() {Text = "Nevada", Value = "NV" },
                new SelectListItem() {Text = "New Hampshire", Value = "NH" },
                new SelectListItem() {Text = "New Jersey", Value = "NJ" },
                new SelectListItem() {Text = "New Mexico", Value = "NM" },
                new SelectListItem() {Text = "New York", Value = "NY" },
                new SelectListItem() {Text = "North Carolina", Value = "NC" },
                new SelectListItem() {Text = "North Dakota", Value = "ND" },
                new SelectListItem() {Text = "Ohio", Value = "OH" },
                new SelectListItem() {Text = "Oklahoma", Value = "OK" },
                new SelectListItem() {Text = "Oregon", Value = "OR" },
                new SelectListItem() {Text = "Pennsylvania", Value = "PA" },
                new SelectListItem() {Text = "Rhode Island", Value = "RI" },
                new SelectListItem() {Text = "South Carolina", Value = "SC" },
                new SelectListItem() {Text = "South Dakota", Value = "SD" },
                new SelectListItem() {Text = "Tennessee", Value = "TN" },
                new SelectListItem() {Text = "Texas", Value = "TX" },
                new SelectListItem() {Text = "Utah", Value = "UT" },
                new SelectListItem() {Text = "Vermont", Value = "VT" },
                new SelectListItem() {Text = "Virginia", Value = "VA" },
                new SelectListItem() {Text = "Washington", Value = "WA" },
                new SelectListItem() {Text = "West Virginia", Value = "WV" },
                new SelectListItem() {Text = "Wisconsin", Value = "WI" },
                new SelectListItem() {Text = "Wyoming", Value = "WY`" },
            };

    }



}