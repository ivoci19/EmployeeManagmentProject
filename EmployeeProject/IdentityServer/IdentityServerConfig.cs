using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace EmployeeProject.IdentityServer
{
	public class IdentityServerConfig
	{
		public const string ApiName = "EmployeeApi";
		public const string ApiFriendlyName = "EmployeeApi";
		public const string EmployeeClientID = "EmployeeApi";
		public const string SwaggerClientID = "swaggerui";
		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Phone(),
				new IdentityResources.Email(),
				new IdentityResource("roles", new List<string> { JwtClaimTypes.Role })
			};
		}

		public static IEnumerable<ApiScope> GetApiScopes()
		{
			return new List<ApiScope>
			{
				new ApiScope(ApiName)
				{
					UserClaims =
					{
						JwtClaimTypes.Name,
						JwtClaimTypes.Email,
						JwtClaimTypes.PhoneNumber,
						JwtClaimTypes.Role,
						"permission"
					}
				}
			};
		}

		// Api resources.
		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource(ApiName) {
					Scopes = {
						ApiName
					}
				}
			};
		}

		
		public static IEnumerable<Client> GetClients()
		{
			Secret secret = new Secret("employeeSecretApi".Sha256());
			
			return new List<Client>
			{
                // http://docs.identityserver.io/en/release/reference/client.html.
                new Client
				{
					ClientId = EmployeeClientID,
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, 
                    AllowAccessTokensViaBrowser = true,
					RequireClientSecret = false, 
                    
                    AllowedScopes = {
						IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Phone,
						IdentityServerConstants.StandardScopes.Email,
						"roles",
						ApiName
					},
					AllowOfflineAccess = true, // For refresh token.
                    RefreshTokenExpiration = TokenExpiration.Absolute,
					RefreshTokenUsage = TokenUsage.OneTimeOnly,
					AccessTokenLifetime = 28800, // Lifetime of access token in seconds.
                    AbsoluteRefreshTokenLifetime = 28800,
                   
                },

				new Client
				{
					ClientId = SwaggerClientID,
					ClientName = "Swagger UI",
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
					AllowAccessTokensViaBrowser = true,
					RequireClientSecret = false,

					AllowedScopes = {
						ApiName
					}
				}
			};
		}
	}
}
