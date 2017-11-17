using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MainMidlandsFly.Models
{
    public class Crew
    {
        [Display(Name = "Employee ID")]
        [Required]
        public int CrewId { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Mobile No")]
        [Required]
        public string MobNo { get; set; }

        [Display(Name = "E-Mail")]
        [Required]
        public string Email { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Role")]
        public string Type { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

    }
}
