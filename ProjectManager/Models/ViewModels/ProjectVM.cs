namespace ProjectManager.Models.ViewModels
{
    public class ProjectVM
    {
        public Project Project { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public List<int> SelectedEmployeeIds { get; set; } = new List<int>();
    }
}
