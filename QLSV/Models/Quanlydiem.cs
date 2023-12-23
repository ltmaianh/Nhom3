using System.ComponentModel.DataAnnotations;
namespace QLSV.Models
{
    public class Quanlydiem
    {
    [Key]
    public int Sothutu {get;set;}
   
    [Required(ErrorMessage = "Mã sinh viên không được để trống")]
    public string? MaSV {get;set;}
    
    [Required(ErrorMessage = "Tên sinh viên không được để trống")]
    public string? TenSV { get; set; }
    public string? Tenmonhoc { get; set;}
    public int Diem { get; set; }
}
}
//Diem-QLDiem-QLMonhoc_CRUD