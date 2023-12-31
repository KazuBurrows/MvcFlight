﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcFlight.Data;

#nullable disable

namespace MvcFlight.Migrations
{
    [DbContext(typeof(MvcFlightContext))]
    [Migration("20230827074806_SecondInitialCreate")]
    partial class SecondInitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("MvcFlight.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Airline")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ArriveLocation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ArriveTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("DepartLocation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DepartTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("FlightNo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Gate")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeparture")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Flight");
                });
#pragma warning restore 612, 618
        }
    }
}
