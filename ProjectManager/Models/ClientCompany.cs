using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class ClientCompany
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
