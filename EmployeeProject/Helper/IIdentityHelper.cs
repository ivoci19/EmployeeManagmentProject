using SharedModels.ViewModels;
using System.Security.Claims;

namespace EmployeeProject.Helpers
{
    public interface IIdentityHelper
    {
        public UserViewModel GetCurrentUser(ClaimsIdentity identity);
    }
}
