﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebEmployee.web.Data;

#nullable disable

namespace WebEmployee.web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240921201402_AddEmployeeTypeAndBoss")]
    partial class AddEmployeeTypeAndBoss
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebEmployee.web.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BirthPlaceCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BirthPlaceCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BirthPlaceState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BossId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("DateofBirth")
                        .HasColumnType("date");

                    b.Property<string>("EmployeeBadge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Latitude")
                        .HasColumnType("real");

                    b.Property<float?>("Longitude")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BossId");

                    b.HasIndex("EmployeeTypeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("WebEmployee.web.Models.EmployeeImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeImagess");
                });

            modelBuilder.Entity("WebEmployee.web.Models.EmployeeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmployeeTypes");
                });

            modelBuilder.Entity("WebEmployee.web.Models.Employee", b =>
                {
                    b.HasOne("WebEmployee.web.Models.Employee", "Boss")
                        .WithMany()
                        .HasForeignKey("BossId");

                    b.HasOne("WebEmployee.web.Models.EmployeeType", "EmployeeType")
                        .WithMany()
                        .HasForeignKey("EmployeeTypeId");

                    b.Navigation("Boss");

                    b.Navigation("EmployeeType");
                });

            modelBuilder.Entity("WebEmployee.web.Models.EmployeeImage", b =>
                {
                    b.HasOne("WebEmployee.web.Models.Employee", "Employee")
                        .WithMany("EmployeeImages")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("WebEmployee.web.Models.Employee", b =>
                {
                    b.Navigation("EmployeeImages");
                });
#pragma warning restore 612, 618
        }
    }
}
