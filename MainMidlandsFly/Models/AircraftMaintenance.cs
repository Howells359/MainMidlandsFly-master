using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MainMidlandsFly.Models
{
    public class AircraftMaintenance
    {
        public int ScheduleId { get; set; }

        [Display(Name = "Aircraft Id")]
        [Required]
        public int AircraftId { get; set; }

        [Display(Name = "Ground Crew Id")]
        [Required]
        public int Ground_Crew_Id { get; set; }

        [Display(Name = "Maintenance History")]
        [Required]
        public string MaintenanceHistory { get; set; }

        [Display(Name = "Date ")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }


    }
}
