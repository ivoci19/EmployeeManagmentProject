using EmployeesData.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTask = EmployeesData.Models.ProjectTask;

namespace EmployeesData
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        
    }
}
