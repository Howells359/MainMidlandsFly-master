using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MainMidlandsFly.Migrations
{
    public partial class aircraft_migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FlyingHoursCount",
                table: "Aircraft",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FlyingHoursCount",
                table: "Aircraft",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
