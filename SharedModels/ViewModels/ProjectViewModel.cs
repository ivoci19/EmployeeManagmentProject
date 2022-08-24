using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.ViewModels
{
    public class ProjectBaseViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        public string Code { get; set; }
    }
    public class ProjectViewModel : ProjectBaseViewModel
    {
        [Required]
        public int Id { get; set; }
    }
    public class ProjectEditViewModel : ProjectBaseViewModel
    {
    }
    //View Model only for Get Project by Id, when you want all the project data
    public class AllDataProjectViewModel : ProjectViewModel
    {
        public ICollection<ProjectTaskViewModel> ProjectTasks { get; set; }
        public ICollection<EmployeeViewModel> Users { get; set; }
    }
}
