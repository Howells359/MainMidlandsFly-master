using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MainMidlandsFly.Models
{
    public class Crew
    {
        [Display(Name = "Crew ID")]
        [Required]
        public int CrewId { get; set; }

        //[Display(Name = "Employee ID")]
        //[Required]
        //public string EmployeeID { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Mobile No")]
        [DataType(DataType.PhoneNumber)]
        [Required]//(ErrorMessage="UK Mobile Number Required!")]
        [RegularExpression(@"^(\+44\s?7\d{3}|\(?07\d{3}\)?)\s?\d{3}\s?\d{3}$", ErrorMessage = "UK mobile number format required - 07xxx xxxxxx or  (07xxx) xxxxxx or +44 7xxx xxx xxx")]
        public string MobNo { get; set; }

        [Display(Name = "E-Mail")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Address")]
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
