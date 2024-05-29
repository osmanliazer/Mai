namespace MarianaMVCApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
