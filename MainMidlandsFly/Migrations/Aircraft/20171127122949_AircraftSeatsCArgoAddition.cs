using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MainMidlandsFly.Migrations
{
    public partial class AircraftSeatsCArgoAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AircraftRegNo",
                table: "Aircraft",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Maximum_Cargo_Capacity",
                table: "Aircraft",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Maximum_Seating_Capacity",
                table: "Aircraft",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Maximum_Cargo_Capacity",
                table: "Aircraft");

            migrationBuilder.DropColumn(
                name: "Maximum_Seating_Capacity",
                table: "Aircraft");

            migrationBuilder.AlterColumn<string>(
                name: "AircraftRegNo",
                table: "Aircraft",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 6);
        }
    }
}
