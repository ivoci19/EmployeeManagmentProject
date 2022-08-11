

using EmployeesData.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace EmployeeProject.Authorization
{
	public class ProfileService : IProfileService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;

		public ProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)

		{
			_userManager = userManager;
			_claimsFactory = claimsFactory;
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var sub = context.Subject.GetSubjectId();
			var user = await _userManager.FindByIdAsync(sub);
			var principal = await _claimsFactory.CreateAsync(user);
			var claims = principal.Claims.ToList();
			claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
			context.IssuedClaims = claims;
		}


		public async Task IsActiveAsync(IsActiveContext context)
		{
			var sub = context.Subject.GetSubjectId();
			var user = await _userManager.FindByIdAsync(sub);
			context.IsActive = true;
		}
    }
}
