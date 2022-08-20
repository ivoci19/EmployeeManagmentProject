using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeesData.Models
{
    public class Project : Audit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        public string Code { get; set; }
        public ICollection<ProjectTask> ProjectTasks { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
