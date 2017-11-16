using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MainMidlandsFly.Models
{
    public class NewAircraftContext : DbContext
    {
        public NewAircraftContext (DbContextOptions<NewAircraftContext> options)
            : base(options)
        {
        }

        public DbSet<MainMidlandsFly.Models.Aircraft> Aircraft { get; set; }
    }
}
