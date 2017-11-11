using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainMidlandsFly.Models
{
    public class Crew
    {
        public int ID { get; set; }

        [Display(Name = "Crew ID (Has to be a 5 Digit Number)")]
        [StringLength(4, MinimumLength = 5)]
        [Required]
        public string CrewID { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
