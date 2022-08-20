using SharedModels.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesData.Models
{
    public class ProjectTask : Audit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Content cannot be longer than 1000 characters.")]
        public string Content { get; set; }
        [Required]
        public TaskStatusEnum TaskStatus { get; set; }
        [ForeignKey("User")]
        public int? AssignedTo { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }

    }
}
