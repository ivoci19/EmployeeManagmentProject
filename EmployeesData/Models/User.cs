using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Models
{
    public class User : Audit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "An Username is required")]
        [StringLength(160)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
