using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [Required]
        [Range(0, 5)]
        public int Priority { get; set; }
        [DisplayName("Client Company")]
        public int ClientCompanyId { get; set; }
        [ForeignKey("ClientCompanyId")]
        public ClientCompany ClientCompany { get; set; }
        [DisplayName("Executor Company")]
        public int ExecutorCompanyId { get; set; }
        [ForeignKey("ExecutorCompanyId")]
        public ExecutorCompany ExecutorCompany { get; set; }
        [DisplayName("Project Manager")]
        public int? ProjectManagerId { get; set; }
        [ForeignKey("ProjectManagerId")]
        public Employee ProjectManager { get; set; }
        public ICollection<ProjectEmployee> Employees { get; set; }
    }
}
