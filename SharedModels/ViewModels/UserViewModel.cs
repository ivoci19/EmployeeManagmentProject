using System.ComponentModel.DataAnnotations;

namespace SharedModels.ViewModels
{
    public class UserBaseViewModel
    {

        [Required(ErrorMessage = "An Username is required")]
        [StringLength(160)]
        public string Username { get; set; }
        [Required(ErrorMessage = "A First Name is required")]
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
    }
    public class UserViewModel : UserBaseViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }
    }

    public class UserEditViewModel : UserBaseViewModel
    {
        [Required]
        public string Password { get; set; }
    }
    public class UserPhotoViewModel : UserViewModel
    {
        public string Photo { get; set; }
    }


}
