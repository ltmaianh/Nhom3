
using Microsoft.EntityFrameworkCore;
using QLSV.Models;

namespace QLSV.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<Khoa> Khoa { get; set;}
        public DbSet<Lop> Lop {get; set;}
        public DbSet<SinhVien> SinhVien {get; set;}
        public DbSet<Diem> Diem { get; set;}
        public DbSet<Quanlydiem> Quanlydiem {get; set;}
        public DbSet<Quanlymonhoc> Quanlymonhoc {get; set;}
       
    }
}
