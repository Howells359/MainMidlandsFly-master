using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainMidlandsFly.Models
{
    public class Flight
    {
        public int ID { get; set; }

        [StringLength(6, MinimumLength = 5)]
        [Display(Name = "Flight ID (3 Digits,3 Letters)")]
        [Required]
        public string FlightID { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Origin { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Destination { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Departure Time")]
        [DataType(DataType.Time)]
        [Required]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Arrival Time")]
        [DataType(DataType.Time)]
        [Required]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Distance Travelled")]
        public int DistanceTravelled { get; set; }
    }
}
