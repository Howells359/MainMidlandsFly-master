using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainMidlandsFly.Models;

namespace MainMidlandsFly.Models
{
    public class AircraftMaintenanceContext : DbContext
    {
        public AircraftMaintenanceContext(DbContextOptions<AircraftMaintenanceContext> options)
            : base(options)

        { 

        }


        public DbSet<MainMidlandsFly.Models.Aircraft> Aircraft { get; set; }

        public DbSet<MainMidlandsFly.Models.Crew> Crew { get; set; }

        public DbSet<MainMidlandsFly.Models.AircraftMaintenance> AircraftMaintenance { get; set; }

        public DbSet<MainMidlandsFly.Models.AircraftMaintenanceModel> AircraftMaintenanceModel { get; set; }
    }
   
}
