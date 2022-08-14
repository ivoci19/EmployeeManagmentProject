using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels
{
    public class UserViewModel
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
    }
}
