using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Models
{
    internal class ProjectUser
    {
        public int ProjectsId { get; set; }
        public int UsersId { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }
    }
}
