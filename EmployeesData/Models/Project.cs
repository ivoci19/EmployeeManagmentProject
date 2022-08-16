using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Models
{
    public class Project : Audit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required] 
        public string Code { get; set; }
        public ICollection<ProjectTask> ProjectTasks { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
