﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using road_runner.Models;
using System;

namespace road_runner.Migrations
{
    [DbContext(typeof(RoadRunnerContext))]
    [Migration("20180522165204_runnermigrations")]
    partial class runnermigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("road_runner.Models.Feature", b =>
                {
                    b.Property<int>("featureId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<string>("location");

                    b.Property<string>("name");

                    b.Property<int?>("tripId");

                    b.Property<DateTime>("updated_at");

                    b.HasKey("featureId");

                    b.HasIndex("tripId");

                    b.ToTable("features");
                });

            modelBuilder.Entity("road_runner.Models.Friend", b =>
                {
                    b.Property<int>("friendId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<int>("friendAId");

                    b.Property<int>("friendBId");

                    b.Property<DateTime>("updated_at");

                    b.Property<int?>("userId");

                    b.HasKey("friendId");

                    b.HasIndex("friendAId");

                    b.HasIndex("friendBId");

                    b.HasIndex("userId");

                    b.ToTable("friends");
                });

            modelBuilder.Entity("road_runner.Models.FriendA", b =>
                {
                    b.Property<int>("friendAId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<DateTime>("updated_at");

                    b.Property<int>("userId");

                    b.HasKey("friendAId");

                    b.HasIndex("userId");

                    b.ToTable("friend_a");
                });

            modelBuilder.Entity("road_runner.Models.FriendB", b =>
                {
                    b.Property<int>("friendBId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<DateTime>("updated_at");

                    b.Property<int>("userId");

                    b.HasKey("friendBId");

                    b.HasIndex("userId");

                    b.ToTable("friend_b");
                });

            modelBuilder.Entity("road_runner.Models.Runner", b =>
                {
                    b.Property<int>("runnerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<int>("tripId");

                    b.Property<DateTime>("updated_at");

                    b.Property<int>("userId");

                    b.HasKey("runnerId");

                    b.HasIndex("tripId");

                    b.HasIndex("userId");

                    b.ToTable("runners");
                });

            modelBuilder.Entity("road_runner.Models.Trip", b =>
                {
                    b.Property<int>("tripId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<string>("description");

                    b.Property<string>("destination");

                    b.Property<DateTime>("end_date");

                    b.Property<DateTime>("start_date");

                    b.Property<string>("start_point");

                    b.Property<DateTime>("updated_at");

                    b.Property<int>("userId");

                    b.HasKey("tripId");

                    b.HasIndex("userId");

                    b.ToTable("trips");
                });

            modelBuilder.Entity("road_runner.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<string>("email");

                    b.Property<string>("first_name");

                    b.Property<string>("last_name");

                    b.Property<string>("password");

                    b.Property<DateTime>("updated_at");

                    b.Property<string>("username");

                    b.HasKey("userId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("road_runner.Models.Feature", b =>
                {
                    b.HasOne("road_runner.Models.Trip")
                        .WithMany("features")
                        .HasForeignKey("tripId");
                });

            modelBuilder.Entity("road_runner.Models.Friend", b =>
                {
                    b.HasOne("road_runner.Models.FriendA", "friend_a")
                        .WithMany()
                        .HasForeignKey("friendAId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("road_runner.Models.FriendB", "friend_b")
                        .WithMany()
                        .HasForeignKey("friendBId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("road_runner.Models.User")
                        .WithMany("friends")
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("road_runner.Models.FriendA", b =>
                {
                    b.HasOne("road_runner.Models.User", "friend1")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("road_runner.Models.FriendB", b =>
                {
                    b.HasOne("road_runner.Models.User", "friend2")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("road_runner.Models.Runner", b =>
                {
                    b.HasOne("road_runner.Models.Trip", "trip")
                        .WithMany("runners")
                        .HasForeignKey("tripId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("road_runner.Models.User", "user")
                        .WithMany("attended")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("road_runner.Models.Trip", b =>
                {
                    b.HasOne("road_runner.Models.User", "planner")
                        .WithMany("planned")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
