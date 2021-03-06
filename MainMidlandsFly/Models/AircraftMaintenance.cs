﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MainMidlandsFly.Models
{
    public class AircraftMaintenance
    {
        [Required]
        public int ID { get; set; }

        [Display(Name = "Aircraft Id")]

        public int AircraftId { get; set; }


        [Display(Name = "Ground Crew Id")]
        [Required]
        public int Ground_Crew_Id { get; set; }


        //[Display(Name = "Aircraft Reg Num")]
        //public string AircraftRegNum { get; set; }

        //[Display(Name = "Name")]
        //public string CrewMemberName { get; set; }

        [Display(Name = "Maintenance History")]
        [Required]
        public string Maintenance_History { get; set; }

        [Display(Name = "Date ")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Job Status")]
 
        public string Job_Status { get; set; }


    }
}
