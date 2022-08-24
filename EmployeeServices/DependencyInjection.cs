using EmployeesData.IRepositories;
using EmployeesData.Repositories;
using EmployeeServices.IServices;
using EmployeeServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeServices
{
    public static class DependencyInjection
    {
        //Extension method of IServiceCollection
        public static void AddEmployeeServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserServices, UserServices>();

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectServices, ProjectServices>();

            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddScoped<ITaskServices, TaskServices>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleServices, RoleServices>();
        }

    }
}
