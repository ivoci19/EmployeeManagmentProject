using SharedModels.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels
{
    public class ProjectTaskBaseViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public TaskStatusEnum TaskStatus { get; set; }
    }
    public class ProjectTaskViewModel : ProjectTaskBaseViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ProjectName { get; set; }
        
    }

    public class ProjectTaskEditViewModel : ProjectTaskBaseViewModel
    {
        public int? AssignedTo { get; set; }
        public int ProjectId { get; set; }
    }
}
