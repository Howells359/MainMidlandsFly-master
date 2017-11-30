using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MainMidlandsFly.Models
{
    public class Cargo_Aircraft_Crew_Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Display(Name = "Flight Id")]
        [Required]
        public int FlightId { get; set; }

        [Display(Name = "Aircraft Id")]
        [Required]
        public int AircraftId { get; set; }

        [Display(Name = "Enter First Cabin Crew Id")]

        public int CabinCrewId { get; set; }

        [Display(Name = "Enter Second Cabin Crew Id")]

        public int CabinCrewId2 { get; set; }


        [Display(Name = "Enter third Cabin Crew Id")]
        public int CabinCrewId3 { get; set; }

        

        public int Flying_Hours { get; set; }

    }
}
