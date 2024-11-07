using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class ExecutorCompany
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
