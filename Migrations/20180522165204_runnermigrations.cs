using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace road_runner.Migrations
{
    public partial class runnermigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false),
                    username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "friend_a",
                columns: table => new
                {
                    friendAId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friend_a", x => x.friendAId);
                    table.ForeignKey(
                        name: "FK_friend_a_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "friend_b",
                columns: table => new
                {
                    friendBId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friend_b", x => x.friendBId);
                    table.ForeignKey(
                        name: "FK_friend_b_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trips",
                columns: table => new
                {
                    tripId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    destination = table.Column<string>(nullable: true),
                    end_date = table.Column<DateTime>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: false),
                    start_point = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trips", x => x.tripId);
                    table.ForeignKey(
                        name: "FK_trips_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "friends",
                columns: table => new
                {
                    friendId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    friendAId = table.Column<int>(nullable: false),
                    friendBId = table.Column<int>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friends", x => x.friendId);
                    table.ForeignKey(
                        name: "FK_friends_friend_a_friendAId",
                        column: x => x.friendAId,
                        principalTable: "friend_a",
                        principalColumn: "friendAId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_friends_friend_b_friendBId",
                        column: x => x.friendBId,
                        principalTable: "friend_b",
                        principalColumn: "friendBId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_friends_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "features",
                columns: table => new
                {
                    featureId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    location = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    tripId = table.Column<int>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_features", x => x.featureId);
                    table.ForeignKey(
                        name: "FK_features_trips_tripId",
                        column: x => x.tripId,
                        principalTable: "trips",
                        principalColumn: "tripId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "runners",
                columns: table => new
                {
                    runnerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    tripId = table.Column<int>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_runners", x => x.runnerId);
                    table.ForeignKey(
                        name: "FK_runners_trips_tripId",
                        column: x => x.tripId,
                        principalTable: "trips",
                        principalColumn: "tripId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_runners_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_features_tripId",
                table: "features",
                column: "tripId");

            migrationBuilder.CreateIndex(
                name: "IX_friend_a_userId",
                table: "friend_a",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_friend_b_userId",
                table: "friend_b",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_friends_friendAId",
                table: "friends",
                column: "friendAId");

            migrationBuilder.CreateIndex(
                name: "IX_friends_friendBId",
                table: "friends",
                column: "friendBId");

            migrationBuilder.CreateIndex(
                name: "IX_friends_userId",
                table: "friends",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_runners_tripId",
                table: "runners",
                column: "tripId");

            migrationBuilder.CreateIndex(
                name: "IX_runners_userId",
                table: "runners",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_trips_userId",
                table: "trips",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "features");

            migrationBuilder.DropTable(
                name: "friends");

            migrationBuilder.DropTable(
                name: "runners");

            migrationBuilder.DropTable(
                name: "friend_a");

            migrationBuilder.DropTable(
                name: "friend_b");

            migrationBuilder.DropTable(
                name: "trips");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
