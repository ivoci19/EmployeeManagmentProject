using EmployeesData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
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

        public IHttpContextAccessor _httpAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option, IHttpContextAccessor httpAccessor) : base(option)
        {
            _httpAccessor = httpAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Adding unique keyword on models
            builder.Entity<User>()
           .HasIndex(b => b.Username)
           .IsUnique();
            builder.Entity<User>()
            .HasIndex(b => b.Email)
            .IsUnique();
            builder.Entity<Project>()
            .HasIndex(b => b.Code)
            .IsUnique();
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            UpdateAuditEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }


        private void UpdateAuditEntities()
        {
            string userName = _httpAccessor.HttpContext?.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value?.Trim();

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAudit && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entry in modifiedEntries)
            {
                var entity = (IAudit)entry.Entity;
                var now = DateTime.UtcNow;

                //if we are creating a new entity
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = userName;
                }
                //if it is in update state
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = now;
                entity.UpdatedBy = userName;
            }
        }


    }
}
