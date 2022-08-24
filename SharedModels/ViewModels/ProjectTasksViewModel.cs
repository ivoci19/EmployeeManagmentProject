using SharedModels.Enum;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.ViewModels
{
    public class ProjectTaskBaseViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Content cannot be longer than 1000 characters.")]
        public string Content { get; set; }
        [Required]
        public TaskStatusEnum TaskStatus { get; set; }
    }
    public class ProjectTaskViewModel : ProjectTaskBaseViewModel
    {
        [Required]
        public int Id { get; set; }
        public string Username { get; set; }
        public string ProjectName { get; set; }

    }
    public class ProjectTaskEditViewModel : ProjectTaskBaseViewModel
    {
        public int? AssignedTo { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}
