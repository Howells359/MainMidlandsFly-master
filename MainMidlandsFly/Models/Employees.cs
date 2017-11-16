using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainMidlandsFly.Models
{
    [Authorize]
    public class Employees
    {
        public int ID { get; set; }

        [Display(Name = "Employee ID")]
        [Required]
        public string EmployeeID { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Role")]
        [Required]
        public string Role { get; set; }
    }
}
