using SharedModels.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; }
        public TaskStatusEnum TaskStatus { get; set; }
        [ForeignKey("User")]
        public int? AssignedTo { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }

    }
}
