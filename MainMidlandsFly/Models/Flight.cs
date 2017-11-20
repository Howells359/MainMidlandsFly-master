using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainMidlandsFly.Models
{
    [Authorize]
    public class Flight
    {
        public int FlightId { get; set; }

        [Display(Name = "Leaving Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime LeavingDate { get; set; }

        [Display(Name = "Departure Time")]
        [DataType(DataType.Time)]
        [Required]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Arriving Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime ArrivalDate { get; set; }

        [Display(Name = "Arrival Time")]
        [DataType(DataType.Time)]
        [Required]
        public DateTime ArrivalTime { get; set; }

        [StringLength(60)]
        [Required]
        public string Origin { get; set; }

        [StringLength(60)]
        [Required]
        public string Destination { get; set; }

    }
}
