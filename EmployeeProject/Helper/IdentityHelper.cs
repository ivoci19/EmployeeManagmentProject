using EmployeeServices.IServices;
using SharedModels.ViewModels;
using System.Linq;
using System.Security.Claims;

namespace EmployeeProject.Helpers
{
    public class IdentityHelper : IIdentityHelper
    {
        IUserServices _userServices;
        public IdentityHelper(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public UserViewModel GetCurrentUser(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                var userClaims = identity.Claims;

                var username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
                var user = _userServices.GetUserByUsername(username);
                return user;
            }
            return null;
        }
    }
}
