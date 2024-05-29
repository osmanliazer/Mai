namespace MarianaMVCApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Tittle { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
