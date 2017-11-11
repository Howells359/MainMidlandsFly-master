using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MainMidlandsFly.Models
{
    public class FlightContext : DbContext
    {
        public FlightContext (DbContextOptions<FlightContext> options)
            : base(options)
        {
        }

        public DbSet<MainMidlandsFly.Models.Flight> Flight { get; set; }
    }
}
