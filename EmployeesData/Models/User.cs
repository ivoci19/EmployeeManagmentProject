using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Models
{
    public class User : Audit
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }   
        public string FirstName { get; set; }
        public string LastName { get; set; }   
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string RoleName { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
