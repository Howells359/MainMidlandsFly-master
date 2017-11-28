using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;



namespace MainMidlandsFly.Models
{
    [Authorize]
    public class Aircraft
    {
        public int ID { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 4)]
        [Display(Name = "Aircraft Registration ID")]
        //Utilise remote validation from Microsoft.AspNetCore.Mvc which calls a controller method to validate input value against DB field
        //RegIdValidatoris the method, Aircraft is the controller
        [Remote("RegIdValidator", "Aircraft", AdditionalFields = "InitialRegId", ErrorMessage = "Registration ID already exists")]        
        [RegularExpression(@"^\d{3}[a-zA-Z]{3}$",
            ErrorMessage = "Aircraft Registration must be 3 numbers and 3 letters, without spaces - e.g. 123ABC")]
        public string AircraftRegNo { get; set; }

        [Required(ErrorMessage = "Please select Passenger or Cargo")]
        [Display(Name = "AirCraft Category")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Please input a numeric value for the seating capacity")]
        [Display(Name ="Seating Capacity")]
        public int Maximum_Seating_Capacity { get; set; }

        [Required(ErrorMessage = "Please input a numeric value for the cargo capacity in tons")]
        [Display(Name = "Cargo Capacity (Tons)")]
        public int Maximum_Cargo_Capacity { get; set; }

        //[Required]
        [Display(Name = "Status")]
        public string Status { get; set; }


        //[Required]
        [Display(Name = "Flying Hours")]
        public int FlyingHoursCount { get; set; }

        [NotMapped]
        [Display(Name = "AirCraft Categories")]
        public List<SelectListItem> AircraftCategory { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Passenger", Text = "Passenger" },
            new SelectListItem { Value = "Cargo", Text = "Cargo" },
        };

    }
}
