using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MainMidlandsFly.Migrations.MainFlight
{
    public partial class newflightsch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flight_Aircraft_Crew_Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AircraftId = table.Column<int>(type: "int", nullable: false),
                    CabinCrewId = table.Column<int>(type: "int", nullable: false),
                    CabinCrewId2 = table.Column<int>(type: "int", nullable: false),
                    CabinCrewId3 = table.Column<int>(type: "int", nullable: false),
                    FlightCrewId1 = table.Column<int>(type: "int", nullable: false),
                    FlightCrewId2 = table.Column<int>(type: "int", nullable: false),
                    FlightCrewId3 = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    Flying_Hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight_Aircraft_Crew_Schedule", x => x.ScheduleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flight_Aircraft_Crew_Schedule");
        }
    }
}
