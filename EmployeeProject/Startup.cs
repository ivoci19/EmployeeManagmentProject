using EmployeeProject.Authorization;
using EmployeeProject.IdentityServer;
using EmployeesData;
using EmployeesData.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(Configuration["ConnectionStrings:MainConnection"],b => b.MigrationsAssembly("EmployeesData")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                     .AddEntityFrameworkStores<ApplicationDbContext>()
                      .AddDefaultTokenProviders();

            var identityServerBuilder = services.AddIdentityServer()
                                        .AddInMemoryPersistedGrants()
                                        .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                                        .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
                                        .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                                        .AddInMemoryClients(IdentityServerConfig.GetClients())
                                        .AddAspNetIdentity<ApplicationUser>()
                                        .AddProfileService<ProfileService>();

            identityServerBuilder.AddDeveloperSigningCredential();

            var applicationUrl = Configuration["ServerUrl"];

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = applicationUrl;
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.RequireHttpsMetadata = false;  
                    options.ApiName = IdentityServerConfig.ApiName;
                    options.SaveToken = true;
                });

            services.AddAuthorization();

            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeProject", Version = "v1" });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("/connect/token", UriKind.Relative),
                            Scopes = new Dictionary<string, string>
                            {
                                {IdentityServerConfig.ApiName, IdentityServerConfig.ApiFriendlyName}
                            }
                        }
                    }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeProject v1");
                    c.OAuthClientId(IdentityServerConfig.SwaggerClientID);
                    c.OAuthClientSecret("no_password"); 
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseIdentityServer();
            app.UseAuthentication();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
