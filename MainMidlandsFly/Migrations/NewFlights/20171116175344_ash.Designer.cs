﻿// <auto-generated />
using MainMidlandsFly.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace MainMidlandsFly.Migrations.NewFlights
{
    [DbContext(typeof(NewFlightsContext))]
    [Migration("20171116175344_ash")]
    partial class ash
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

                    b.Property<DateTime>("ArrivalTime");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DepartureTime");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("DistanceTravelled");

                    b.Property<string>("FlightNo")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("FlightId");

                    b.ToTable("Flight");
                });
#pragma warning restore 612, 618
        }
    }
}
