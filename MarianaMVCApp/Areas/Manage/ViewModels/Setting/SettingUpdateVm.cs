using System.ComponentModel.DataAnnotations;

namespace MarianaMVCApp.Areas.Manage.ViewModels
{
    public class SettingUpdateVm
    {
        [Required]
        public string Value { get; set; } = null!;
    }
}
