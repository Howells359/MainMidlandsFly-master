using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace MainMidlandsFly.Models
{
    [Authorize]
    public class Aircraft
    {
        public int ID { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 4)]
        [Display(Name = "Aircraft RegNo (3 Digits,3 Letters)")]
        [RegularExpression(@"^\d{3}[a-zA-Z]{3}$",
            ErrorMessage = "Aircraft Registration must be 3 numbers and 3 letters, without spaces - e.g. 123ABC")]
        public string AircraftRegNo { get; set; }

        //[Required]
        //public string MaxCarry { get; set; }

        //[Required]
        //public string MaxSeat { get; set; }


        [Required]
        [Display(Name = "AirCraft Category")]
        public string Type { get; set; }

        [Display(Name ="Seating Capacity")]
        public int Maximum_Seating_Capacity { get; set; }

        [Display(Name = "Cargo Capacity (Tons)")]
        public int Maximum_Cargo_Capacity { get; set; }

        //[Required]
        [Display(Name = "Status")]
        public string Status { get; set; }


        //[Required]
        [Display(Name = "Flying Hours")]
        public int FlyingHoursCount { get; set; }

        [NotMapped]
        [Display(Name = "AirCraft Categorys")]
        public List<SelectListItem> AircraftCategory { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Passenger", Text = "Passenger" },
            new SelectListItem { Value = "Cargo", Text = "Cargo" },
        };

    }
}
