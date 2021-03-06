﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MainMidlandsFly.Models
{
    public class CrewViewModel
    {
        [Display(Name = "Employee ID")]
        //[Required]
        //[Range(10000, 99999, ErrorMessage = "Please enter valid Employee ID between 10000 & 99999")]
        public int CrewId { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Please supply the full name!")]
        public string Name { get; set; }

        [Display(Name = "Mobile Telephone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage="UK Mobile Number Required!")]
        [RegularExpression(@"^(\+44\s?7\d{3}|\(?07\d{3}\)?)\s?\d{3}\s?\d{3}$", ErrorMessage = "UK mobile number format required - 07xxx xxxxxx or  (07xxx) xxxxxx or +44 7xxx xxx xxx")]
        public string MobNo { get; set; }

        [Display(Name = "E-Mail")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Home Address")]
        public string Address { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Role")]
        [Required]
        public string Type { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }



        //Model items below are used for the Crew address lookup and aren't stored in the DB, hence NotMapped
        [NotMapped]
        [Display(Name = "Roles")]
        public List<SelectListItem> Roles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Ground", Text = "Ground" },
            new SelectListItem { Value = "Flight", Text = "Flight" },
            new SelectListItem { Value = "Cabin", Text = "Cabin"  },
            new SelectListItem { Value = "Scheduler", Text = "Scheduler"  },
        };

        [NotMapped]
        [Display(Name = "House Number")]
        [Required]
        public string HouseNo { get; set; }

        [NotMapped]
        [Display(Name = "Post Code (UK)")]
        [Required]
        //Need to modify expression below to add ability to input lowercase characters
        [RegularExpression(@"\b([a-pr-uwyzA-PR-UWYZ](?:(?:\d{1,2}|\d[a-hj-kstuwA-HJ-KSTUW])|(?:[a-hk-yA-HK-Y]\d(?:\d|[a-zA-Z])?)))\s?(\d[abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2})\b",
        //[RegularExpression(@"\b([A-PR-UWYZ](?:(?:\d{1,2}|\d[A-HJ-KSTUW])|(?:[A-HK-Y]\d(?:\d|[A-Z])?)))\s?(\d[ABD-HJLNP-UW-Z]{2})\b",
            ErrorMessage = "Please use UK postcode format- AA9A 9AA, A9A 9AA, A9 9AA, A99 9AA, AA9 9AA, AA99 9AA")]
        public string postcode { get; set; }

        [NotMapped]
        public string latitude { get; set; }

        [NotMapped]
        public string longitude { get; set; }

        [NotMapped]
        public List<string> addresses { get; set; }

        [NotMapped]
        public string formattedAddress { get; set; }

        

    }
}
