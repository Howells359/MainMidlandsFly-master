using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MainMidlandsFly.Migrations.Crew
{
    public partial class crew2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //https://stackoverflow.com/questions/5974554/ef-code-first-how-to-set-identity-seed
            //Sql("DBCC CHECKIDENT ('CrewID', RESEED, 10000)");  - need verifiying if correct KM
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
