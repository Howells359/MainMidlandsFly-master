﻿// <auto-generated />
using MainMidlandsFly.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace MainMidlandsFly.Migrations
{
    [DbContext(typeof(AircraftContext))]
    [Migration("20171127122949_AircraftSeatsCArgoAddition")]
    partial class AircraftSeatsCArgoAddition
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MainMidlandsFly.Models.Aircraft", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AircraftRegNo")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<int>("FlyingHoursCount");

                    b.Property<int>("Maximum_Cargo_Capacity");

                    b.Property<int>("Maximum_Seating_Capacity");

                    b.Property<string>("Status");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Aircraft");
                });
#pragma warning restore 612, 618
        }
    }
}