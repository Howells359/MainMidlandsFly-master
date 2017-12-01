using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainMidlandsFly.Models
{
    public class Passenger_Flight_Crew_Schedule_Model
    {
        [Key]
        public int ScheduleId { get; set; }

        [Display(Name = "Flight Id")]
    
        public int FlightId { get; set; }

        [Display(Name = "Aircraft Id")]
        [Required]
        public int AircraftId { get; set; }
        public List<Aircraft> Aircraft_list { get; set; }


        public int CabinCrewId { get; set; }
        [Display(Name = "First Cabin Crew Id")]
        public List<Crew> CabinCrewId_list { get; set; }

       
        public int CabinCrewId2 { get; set; }

        [Display(Name = "Second Cabin Crew Id")]
        public List<Crew> CabinCrewId2_list { get; set; }

        public int FlightCrewId1 { get; set; }

        [Display(Name = "First Flight Crew Id")]
        public List<Crew> FlightCrewId1_list { get; set; }


        public int FlightCrewId2 { get; set; }

        [Display(Name = "Second Flight Crew Id")]
        public List<Crew> FlightCrewId2_list { get; set; }

        public int CabinCrewId3 { get; set; }

        [Display(Name = "Third Cabin Crew Id")]
        public List<Crew> CabinCrewId3_list { get; set; }

        public int Flying_Hours { get; set; }

    }
}
