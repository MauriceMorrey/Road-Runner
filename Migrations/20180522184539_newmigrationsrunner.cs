using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace road_runner.Migrations
{
    public partial class newmigrationsrunner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "accepted",
                table: "friends",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accepted",
                table: "friends");
        }
    }
}
