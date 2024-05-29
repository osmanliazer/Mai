using System.ComponentModel.DataAnnotations;

namespace MarianaMVCApp.Areas.Manage.ViewModels
{
    public class LoginVm
    {
        [Required]
        [MaxLength(200)]
        [MinLength(3)]
        public string UserNameOrEail { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }=null!;
        public bool IsRemembered { get; set; }
    }
}
