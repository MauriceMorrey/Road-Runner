using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace road_runner.Migrations
{
    public partial class newrunner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "currentUser",
                table: "trips",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currentUser",
                table: "trips");
        }
    }
}
