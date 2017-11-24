using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MainMidlandsFly.Migrations.NewAircraft
{
    public partial class newnewair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCarry",
                table: "Aircraft");

            migrationBuilder.DropColumn(
                name: "MaxSeat",
                table: "Aircraft");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaxCarry",
                table: "Aircraft",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaxSeat",
                table: "Aircraft",
                nullable: false,
                defaultValue: "");
        }
    }
}
