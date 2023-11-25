using System.ComponentModel.DataAnnotations;
namespace QLSV.Models
{
    public class Lop
    {
    [Key]
    [Required(ErrorMessage = "Mã lớp không được để trống")]
    public string? Malop{get;set;}
    [Required(ErrorMessage = "Tên lớp không được để trống")]
    public string? Tenlop { get; set; }
}
}