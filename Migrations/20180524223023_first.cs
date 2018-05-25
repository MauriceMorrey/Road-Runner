using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace road_runner.Migrations
{
    public partial class first : Migration
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
                name: "friends",
                columns: table => new
                {
                    friendId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    accepted = table.Column<bool>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    receiverId = table.Column<int>(nullable: false),
                    senderId = table.Column<int>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: true),
                    userId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friends", x => x.friendId);
                    table.ForeignKey(
                        name: "FK_friends_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_friends_users_userId1",
                        column: x => x.userId1,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "trips",
                columns: table => new
                {
                    tripId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    currentUser = table.Column<bool>(nullable: false),
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
                name: "posts",
                columns: table => new
                {
                    postId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    content = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    creatorId = table.Column<int>(nullable: false),
                    currentUser = table.Column<bool>(nullable: false),
                    tripId = table.Column<int>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.postId);
                    table.ForeignKey(
                        name: "FK_posts_users_creatorId",
                        column: x => x.creatorId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_posts_trips_tripId",
                        column: x => x.tripId,
                        principalTable: "trips",
                        principalColumn: "tripId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    likeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    postId = table.Column<int>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_likes", x => x.likeId);
                    table.ForeignKey(
                        name: "FK_likes_posts_postId",
                        column: x => x.postId,
                        principalTable: "posts",
                        principalColumn: "postId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_likes_users_userId",
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
                name: "IX_friends_userId",
                table: "friends",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_friends_userId1",
                table: "friends",
                column: "userId1");

            migrationBuilder.CreateIndex(
                name: "IX_likes_postId",
                table: "likes",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "IX_likes_userId",
                table: "likes",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_posts_creatorId",
                table: "posts",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_posts_tripId",
                table: "posts",
                column: "tripId");

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
                name: "likes");

            migrationBuilder.DropTable(
                name: "runners");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "trips");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
