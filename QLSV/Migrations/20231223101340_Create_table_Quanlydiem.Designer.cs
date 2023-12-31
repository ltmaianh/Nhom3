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
    [Migration("20231223101340_Create_table_Quanlydiem")]
    partial class Create_table_Quanlydiem
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

            modelBuilder.Entity("QLSV.Models.Lop", b =>
                {
                    b.Property<string>("Malop")
                        .HasColumnType("TEXT");

                    b.Property<string>("Makhoa")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tenlop")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Malop");

                    b.HasIndex("Makhoa");

                    b.ToTable("Lop");
                });

            modelBuilder.Entity("QLSV.Models.Quanlydiem", b =>
                {
                    b.Property<int>("Sothutu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DiemMH")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MaSV")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Mamonhoc")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenSV")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Sothutu");

                    b.HasIndex("MaSV");

                    b.HasIndex("Mamonhoc");

                    b.ToTable("Quanlydiem");
                });

            modelBuilder.Entity("QLSV.Models.Quanlymonhoc", b =>
                {
                    b.Property<string>("Mamonhoc")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tenmonhoc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Mamonhoc");

                    b.ToTable("Quanlymonhoc");
                });

            modelBuilder.Entity("QLSV.Models.SinhVien", b =>
                {
                    b.Property<string>("MaSV")
                        .HasColumnType("TEXT");

                    b.Property<string>("DiaChi")
                        .HasColumnType("TEXT");

                    b.Property<string>("Hovaten")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Makhoa")
                        .HasColumnType("TEXT");

                    b.Property<string>("Malop")
                        .HasColumnType("TEXT");

                    b.HasKey("MaSV");

                    b.HasIndex("Makhoa");

                    b.HasIndex("Malop");

                    b.ToTable("SinhVien");
                });

            modelBuilder.Entity("QLSV.Models.Lop", b =>
                {
                    b.HasOne("QLSV.Models.Khoa", "Khoa")
                        .WithMany()
                        .HasForeignKey("Makhoa");

                    b.Navigation("Khoa");
                });

            modelBuilder.Entity("QLSV.Models.Quanlydiem", b =>
                {
                    b.HasOne("QLSV.Models.SinhVien", "Masv")
                        .WithMany()
                        .HasForeignKey("MaSV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QLSV.Models.Quanlymonhoc", "Monhoc")
                        .WithMany()
                        .HasForeignKey("Mamonhoc");

                    b.Navigation("Masv");

                    b.Navigation("Monhoc");
                });

            modelBuilder.Entity("QLSV.Models.SinhVien", b =>
                {
                    b.HasOne("QLSV.Models.Khoa", "Khoa")
                        .WithMany()
                        .HasForeignKey("Makhoa");

                    b.HasOne("QLSV.Models.Lop", "Lop")
                        .WithMany()
                        .HasForeignKey("Malop");

                    b.Navigation("Khoa");

                    b.Navigation("Lop");
                });
#pragma warning restore 612, 618
        }
    }
}
