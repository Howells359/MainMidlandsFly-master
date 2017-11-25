using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MainMidlandsFly.Models
{
    public class CrewContext : DbContext
    {
        public CrewContext (DbContextOptions<CrewContext> options)
            : base(options)
        {
        }

        public DbSet<MainMidlandsFly.Models.Crew> Crew { get; set; }

        //Fluent API code addedto try and overide generated CrewID values....currently not working as CrewID is a key which must be read only after it's been saved.
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    #region CrewId
        //    modelBuilder.Entity<Crew>()
        //        .Property(b => b.CrewId)
        //        .ValueGeneratedOnAddOrUpdate();

        //    modelBuilder.Entity<Crew>()
        //        .Property(b => b.CrewId)
        //        .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
        //    #endregion
        //}

        //**
        //* Manually running "DBCC checkident ('Crew', reseed, 10000)" in SQL manager started seed value at desired start point, need to script that here somehow.
        //**
    }
}
