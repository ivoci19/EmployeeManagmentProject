using EmployeeProject.IdentityServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeProject.Authorization
{
	internal class AuthorizeCheckOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var authorizeAttribute = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
				.Union(context.MethodInfo.GetCustomAttributes(true))
				.OfType<AuthorizeAttribute>();

			var hasAuthorize = authorizeAttribute.Any();

			var hasPolicy = authorizeAttribute.Where(p => p.Policy != null).Any();

			if (hasAuthorize)
			{
				operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });

				var oAuthScheme = new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
				};

				operation.Security = new List<OpenApiSecurityRequirement>
				{
					new OpenApiSecurityRequirement
					{
						[oAuthScheme] = new [] { IdentityServerConfig.ApiName}
					}
				};
			}
		}
	}
}
