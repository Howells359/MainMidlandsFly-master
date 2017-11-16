using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainMidlandsFly.Models
{
    [Authorize]
    public class Aircraft
    {
        public int ID { get; set; }

        [StringLength(6, MinimumLength = 4)]
        [Display(Name = "Aircraft RegNO (3 Digits,3 Letters)")]
        public string AircraftRegNo { get; set; }

        //[Required]
        //public string MaxCarry { get; set; }

        //[Required]
        //public string MaxSeat { get; set; }

      
        [Required]
        public string Type { get; set; }

        
        [Required]
        public string Status { get; set; }

        
        [Required]
        public string FlyingHoursCount { get; set; }

    }
}
