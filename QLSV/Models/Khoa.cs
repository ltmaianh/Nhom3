using System.ComponentModel.DataAnnotations;
namespace QLSV.Models
{
    public class Khoa
    {
    [Key]
    [Required(ErrorMessage = "Mã khoa không được để trống")]
    public string? Makhoa{get;set;}
    [Required(ErrorMessage = "Tên khoa không được để trống")]
    public string? Tenkhoa { get; set; }
}
}