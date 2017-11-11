using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainMidlandsFly.Models
{
    public class Aircraft
    {
        public int ID { get; set; }

        [StringLength(6, MinimumLength = 5)]
        [Display(Name = "Aircraft ID (3 Digits,3 Letters)")]
        [Required]
        public string AircraftID { get; set; }

        [Required]
        public string Colour { get; set; }

        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string Type { get; set; }

        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string MaxSeat { get; set; }

        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string MaxCarry { get; set; }

    }
}
