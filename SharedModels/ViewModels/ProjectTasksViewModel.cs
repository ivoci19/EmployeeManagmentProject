using SharedModels.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels
{
    public class ProjectTasksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public TaskStatusEnum TaskStatus { get; set; }
        public string? AssignedTo { get; set; }
        public int ProjectId { get; set; }
    }
}
