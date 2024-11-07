using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<ProjectEmployee> Projects { get; set; }
    }
}
