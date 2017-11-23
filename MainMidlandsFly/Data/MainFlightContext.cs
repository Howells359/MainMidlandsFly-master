using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MainMidlandsFly.Models;

namespace MainMidlandsFly.Models
{
    public class MainFlightContext : DbContext
    {
        public MainFlightContext (DbContextOptions<MainFlightContext> options)
            : base(options)
        {
        }

        public DbSet<MainMidlandsFly.Models.Flight> Flight { get; set; }

        public DbSet<MainMidlandsFly.Models.Flight_Aircraft_Crew_Schedule> Flight_Aircraft_Crew_Schedule { get; set; }
    }
}
