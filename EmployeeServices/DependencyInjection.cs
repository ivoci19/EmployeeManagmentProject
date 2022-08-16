using EmployeesData.IRepositories;
using EmployeesData.Repositories;
using EmployeeServices.IServices;
using EmployeeServices.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices
{
    public static class DependencyInjection
    {
        public static void AddEmployeeServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserServices, UserServices>();

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectServices, ProjectServices>();

            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddScoped<ITaskServices, TaskServices>();
        }
    }
}
