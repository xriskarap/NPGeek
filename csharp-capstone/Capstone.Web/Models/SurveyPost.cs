using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class SurveyPost
    {

        public int SurveyId { get; set; }
        [Required(ErrorMessage ="*")]
        public string ParkCode { get; set; }
        [Required(ErrorMessage ="*")]
        [EmailAddress(ErrorMessage ="Please enter a valid email")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage ="*")]
        public string State { get; set; }
        [Required(ErrorMessage ="*")]
        public string ActivityLevel { get; set; }


    }
}
