using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeesData.Models
{
    public class Role : Audit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Role cannot be longer than 50 characters.")]
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
