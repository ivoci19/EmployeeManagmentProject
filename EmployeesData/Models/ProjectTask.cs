using SharedModels.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Models
{
    public class ProjectTask : Audit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public TaskStatusEnum TaskStatus { get; set; }
        [ForeignKey("User")]
        public int? AssignedTo { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }

    }
}
