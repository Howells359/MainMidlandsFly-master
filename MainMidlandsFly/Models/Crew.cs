using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MainMidlandsFly.Models
{
    public class Crew
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Range(10000, 99999, ErrorMessage = "Please enter valid Employee ID between 10000 & 99999")]
        //[Required]
        //Ended up running SQL command "DBCC checkident ('Crew', reseed, 10000)" in SQL manager to set start No
        [Display(Name = "Employee ID")]
        public int CrewId { get; set; }

        [Display(Name = "Full Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Mobile Telephone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "UK Mobile Number Required!")]
        [RegularExpression(@"^(\+44\s?7\d{3}|\(?07\d{3}\)?)\s?\d{3}\s?\d{3}$", ErrorMessage = "UK mobile number format required - 07xxx xxxxxx or  (07xxx) xxxxxx or +44 7xxx xxx xxx")]
        public string MobNo { get; set; }

        [Display(Name = "E-Mail")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Home Address")]
        [MaxLength(300)]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Date of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Role")]
        [Required]
        public string Type { get; set; }

        public string Status { get; set; }

        //Model item below is to populate list therefore is not mapped to DB
        [NotMapped]
        [Display(Name = "Roles")]
        public List<SelectListItem> Roles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Ground Crew", Text = "Ground Crew" },
            new SelectListItem { Value = "Flight Deck Crew", Text = "Flight Deck Crew" },
            new SelectListItem { Value = "Cabin Crew", Text = "Cabin Crew"  },
            new SelectListItem { Value = "Flight Scheduler", Text = "Flight Scheduler"  },
        };

    }
}
