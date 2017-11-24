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
        [Key]
        public int CrewId { get; set; }
        public string Name { get; set; }        
        public string MobNo { get; set; }        
        public string Email { get; set; }
        [MaxLength(300)]
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
