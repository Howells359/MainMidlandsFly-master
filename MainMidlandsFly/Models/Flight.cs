using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMidlandsFly.Models
{
    [Authorize]
    public class Flight
    {
        public int FlightId { get; set; }

        public string AircraftRegNo { get; set; }
        

        [Required]
        public string FlightType { get; set; }


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

        [NotMapped]
        public List<SelectListItem> Airports { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "New York", Text = "New York" },
            new SelectListItem { Value = "Paris", Text = "Paris" },
            new SelectListItem { Value = "Tokyo", Text = "Tokyo"  },
            new SelectListItem { Value = "London", Text = "London"  },
            new SelectListItem { Value = "Los Angeles", Text = "Los Angeles"  },
            new SelectListItem { Value = "Berlin", Text = "Berlin"  },
            new SelectListItem { Value = "Madrid", Text = "Madrid"  },
            new SelectListItem { Value = "Mumbai", Text = "Mumbai"  },
        };


        [NotMapped]
        public List<SelectListItem> FlightTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Passenger", Text = "Passenger" },
            new SelectListItem { Value = "Cargo", Text = "Cargo" },
         };
    }
}
