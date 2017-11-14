﻿// <auto-generated />
using MainMidlandsFly.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace MainMidlandsFly.Migrations.Employees
{
    [DbContext(typeof(EmployeesContext))]
    [Migration("20171114153424_addemployeeroles")]
    partial class addemployeeroles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MainMidlandsFly.Models.Employees", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("EmployeesID")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("Role")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}