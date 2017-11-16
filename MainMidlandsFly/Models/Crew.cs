using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MainMidlandsFly.Models
{
    public class Crew
    {
        public int CrewId { get; set; }

        [Display(Name = "Employee ID")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Employee Mobile Num")]
        [Required]
        public string MobNo { get; set; }

        [Display(Name = "Employee Email Id")]
        [Required]
        public string Email { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

    }
}
