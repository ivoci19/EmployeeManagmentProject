using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels
{
    public class UserBaseViewModel {

        [Required(ErrorMessage = "An Username is required")]
        [StringLength(160)]
        public string Username { get; set; }
        [Required(ErrorMessage = "A Password is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "A Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "A Job Title is required")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "An Email is required")]
        [EmailAddress(ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "An Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "A Photo is required")]
        public string Photo { get; set; }
    }
    public class UserViewModel : UserBaseViewModel
    {
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }
    }

    public class UserEditViewModel : UserBaseViewModel { 
        public string Password { get; set; }
    }
}
