using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MainMidlandsFly.Models
{
    public class Passenger_Aircraft_Crew_Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Display(Name = "Flight Id")]
        [Required]
        public int FlightId { get; set; }

        [Display(Name = "Aircraft Id")]
        [Required]
        public int AircraftId { get; set; }

        [Display(Name = "First Cabin Crew Id")]
       
        public int CabinCrewId { get; set; }

        [Display(Name = "Second Cabin Crew Id")]
        
        public int CabinCrewId2 { get; set; }

        [Display(Name = "Third Cabin Crew Id")]
        public int CabinCrewId3 { get; set; }

        [Display(Name = "First Flight Crew Id")]
        
        public int FlightCrewId1 { get; set; }

        [Display(Name = "Second Flight Crew Id")]
        
        public int FlightCrewId2 { get; set; }

      
        
       

        public int Flying_Hours { get; set; }

    }
}
