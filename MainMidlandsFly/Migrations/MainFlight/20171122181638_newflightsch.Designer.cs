﻿// <auto-generated />
using MainMidlandsFly.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace MainMidlandsFly.Migrations.MainFlight
{
    [DbContext(typeof(MainFlightContext))]
    [Migration("20171122181638_newflightsch")]
    partial class newflightsch
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MainMidlandsFly.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ArrivalDate");

                    b.Property<DateTime>("ArrivalTime");

                    b.Property<DateTime>("DepartureTime");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<DateTime>("LeavingDate");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("FlightId");

                    b.ToTable("Flight");
                });

            modelBuilder.Entity("MainMidlandsFly.Models.Flight_Aircraft_Crew_Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AircraftId");

                    b.Property<int>("CabinCrewId");

                    b.Property<int>("CabinCrewId2");

                    b.Property<int>("CabinCrewId3");

                    b.Property<int>("FlightCrewId1");

                    b.Property<int>("FlightCrewId2");

                    b.Property<int>("FlightCrewId3");

                    b.Property<int>("FlightId");

                    b.Property<int>("Flying_Hours");

                    b.HasKey("ScheduleId");

                    b.ToTable("Flight_Aircraft_Crew_Schedule");
                });
#pragma warning restore 612, 618
        }
    }
}
