﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Scheduler.Infrastructure;

#nullable disable

namespace Scheduler.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241025175215_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Scheduler.Domain.Entities.ApiKey", b =>
                {
                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Uuid");

                    b.ToTable("api_key", (string)null);

                    b.HasData(
                        new
                        {
                            Uuid = new Guid("5b2a367c-55c1-4b65-b1c8-e9f54c3b57d1"),
                            CreatedAt = new DateTime(2024, 10, 25, 17, 52, 15, 358, DateTimeKind.Utc).AddTicks(1695),
                            Description = "first apikey",
                            Name = "master",
                            Token = "W8aKo6fhzZoWBECW86Gt7osp5i2UAp5Rs3JAbpFMRLSaQPgu9Hc4hHVpEepkm5MW",
                            UpdatedAt = new DateTime(2024, 10, 25, 17, 52, 15, 358, DateTimeKind.Utc).AddTicks(1695)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}