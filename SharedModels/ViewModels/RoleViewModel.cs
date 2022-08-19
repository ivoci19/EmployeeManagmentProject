using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels
{
    public class RoleBaseViewModel
    {
        public string RoleName { get; set; }
    }
    public class RoleViewModel : RoleBaseViewModel
    {
        public int Id { get; set; }
    }
    public class RoleEditViewModel : RoleBaseViewModel
    {

    }
}
