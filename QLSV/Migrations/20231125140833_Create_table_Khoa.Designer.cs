﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QLSV.Data;

#nullable disable

namespace QLSV.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231125140833_Create_table_Khoa")]
    partial class Create_table_Khoa
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("QLSV.Models.Khoa", b =>
                {
                    b.Property<string>("Makhoa")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tenkhoa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Makhoa");

                    b.ToTable("Khoa");
                });
#pragma warning restore 612, 618
        }
    }
}
