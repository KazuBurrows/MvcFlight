using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFlight.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightNo = table.Column<string>(type: "TEXT", nullable: false),
                    Airline = table.Column<string>(type: "TEXT", nullable: false),
                    DepartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArriveTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DepartLocation = table.Column<string>(type: "TEXT", nullable: false),
                    ArriveLocation = table.Column<string>(type: "TEXT", nullable: false),
                    Gate = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flight");
        }
    }
}
