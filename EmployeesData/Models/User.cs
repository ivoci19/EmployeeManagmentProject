using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeesData.Models
{
    public class User : Audit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "An Username is required")]
        [StringLength(160)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Job Title cannot be longer than 50 characters.")]
        public string JobTitle { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string Photo { get; set; }
        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
