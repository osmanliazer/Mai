using MarianaMVCApp.Models;

namespace MarianaMVCApp.ViewModels
{
    public class HomeVm
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }
}
