using System.ComponentModel.DataAnnotations;

namespace MarianaMVCApp.Areas.Manage.ViewModels
{
    public class RegisterVm
    {
        [Required]
        [MaxLength(27)]
        [MinLength(3)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(27)]
        [MinLength(3)]
        public string SurName { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]

        [MinLength(3)]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string UserName { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))] 
        public string ComfirmPassword { get; set; } = null!;
    }
}
