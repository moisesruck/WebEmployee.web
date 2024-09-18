using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebEmployee.web.Models
{
    public class EmployeeImage
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
