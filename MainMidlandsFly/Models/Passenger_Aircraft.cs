using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MainMidlandsFly.Models
{
    public class Passenger_Aircraft
    {

        public int ID { get; set; }
        
        [Required]
        public string MaxSeat { get; set; }

    }
}
