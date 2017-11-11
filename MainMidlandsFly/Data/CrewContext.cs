using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MainMidlandsFly.Models
{
    public class CrewContext : DbContext
    {
        public CrewContext (DbContextOptions<CrewContext> options)
            : base(options)
        {
        }

        public DbSet<MainMidlandsFly.Models.Crew> Crew { get; set; }
    }
}
