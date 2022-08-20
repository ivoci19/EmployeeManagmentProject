using System.ComponentModel.DataAnnotations;

namespace SharedModels.ViewModels
{
    public class RoleBaseViewModel
    {
        [StringLength(50, ErrorMessage = "Role cannot be longer than 50 characters.")]
        [Required]
        public string RoleName { get; set; }
    }
    public class RoleViewModel : RoleBaseViewModel
    {
        [Required]
        public int Id { get; set; }
    }
    public class RoleEditViewModel : RoleBaseViewModel
    {

    }
}
