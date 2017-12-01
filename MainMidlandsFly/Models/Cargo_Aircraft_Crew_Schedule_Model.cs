using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MainMidlandsFly.Models
{
    public class Cargo_Aircraft_Crew_Schedule_Model
    {
        [Key]
        public int ScheduleId { get; set; }

        [Display(Name = "Flight Id")]
        [Required]
        public int FlightId { get; set; }

        [Display(Name = "Aircraft Id")]
        [Required]
        public int AircraftId { get; set; }

       

        public int FlightCrewId { get; set; }

       

        public int FlightCrewId2 { get; set; }

      

        public int FlightCrewId3 { get; set; }

        [Display(Name = "First Flight Crew Id")]
        public List<Crew> FlightCrewId_list { get; set; }

        [Display(Name = "Second Flight Crew Id")]
        public List<Crew> FlightCrewId2_list { get; set; }

        [Display(Name = "Third Flight Crew Id")]
        public List<Crew> FlightCrewId3_list { get; set; }
        public List<Aircraft> Aircraft_list { get; set; }

        public int Flying_Hours { get; set; }
    }
}
