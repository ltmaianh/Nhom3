using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSV.Models
{
    public class Diem
    {
        [Key]
        public string? MaSV { get; set; }
        public string? TenSV { get; set; }
        public string? TenMH { get; set; }
         [ForeignKey("MaMH")]
        public string? DiemMH { get; set; }
    }
}