using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QLSV.Models
{
    public class Lop
    {
    [Key]
    [Required(ErrorMessage = "Mã lớp không được để trống")]
    public string? Malop{get;set;}
    [Required(ErrorMessage = "Tên lớp không được để trống")]
    public string? Tenlop { get; set; }

    public string? Makhoa { get; set; }
    [ForeignKey("Makhoa")]
    public Khoa? Khoa { get; set; }
    
}
}