using System.ComponentModel.DataAnnotations;

namespace MarianaMVCApp.Areas.Manage.ViewModels
{
    public class EmployeeUpdateVm
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string FullName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Position { get; set; } = null!;
        public string Image { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
