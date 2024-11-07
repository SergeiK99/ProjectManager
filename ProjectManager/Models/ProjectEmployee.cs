using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Models
{
    public class ProjectEmployee
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Project")]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        [DisplayName("Employees")]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
