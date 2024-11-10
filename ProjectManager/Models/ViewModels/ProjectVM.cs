namespace ProjectManager.Models.ViewModels
{
    public class ProjectVM
    {
        public Project Project { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
