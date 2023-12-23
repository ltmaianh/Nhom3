using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QLSV.Models
{
    public class Quanlydiem
    {
    [Key]
    public int Sothutu{get;set;}

    [Required(ErrorMessage = "Mã sinh viên không được để trống")]
    public string? MaSV { get; set; }
     [ForeignKey("MaSV")]
    public SinhVien? Masv{ get; set; }

    [Required(ErrorMessage = "Tên sinh viên không được để trống")]
    public string? TenSV { get; set; }
    public string? Mamonhoc { get; set;}
    [ForeignKey("Mamonhoc")]
    public Quanlymonhoc? Monhoc { get; set; }
    public string DiemMH { get; set; }
}
}