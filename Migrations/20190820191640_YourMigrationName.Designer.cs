﻿// <auto-generated />
using System;
using LoginRegistration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoginRegistration.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20190820191640_YourMigrationName")]
    partial class YourMigrationName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LoginRegistration.Models.Login", b =>
                {
                    b.Property<int>("loginId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("createdAt");

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<string>("password")
                        .IsRequired();

                    b.Property<DateTime>("updatedAt");

                    b.HasKey("loginId");

                    b.ToTable("logins");
                });

            modelBuilder.Entity("LoginRegistration.Models.Register", b =>
                {
                    b.Property<int>("registerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("createdAt");

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<string>("firstName")
                        .IsRequired();

                    b.Property<string>("lastName")
                        .IsRequired();

                    b.Property<string>("password")
                        .IsRequired();

                    b.Property<DateTime>("updatedAt");

                    b.HasKey("registerId");

                    b.ToTable("registers");
                });
#pragma warning restore 612, 618
        }
    }
}
