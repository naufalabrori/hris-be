using HRIS.Infrastructure.Utils.Interfaces;
using HRIS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Data.Contexts
{
    public class HrisContext : DbContext
    {
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public HrisContext(DbContextOptions<HrisContext> options, ICurrentUserAccessor currentUserAccessor) : base(options) 
        {
            _currentUserAccessor = currentUserAccessor;
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<RolePermission> RolePermissions { get; set; } = null!;
        public DbSet<Applicant> Applicants { get; set; } = null!;
        public DbSet<Attendance> Attendances { get; set; } = null!;
        public DbSet<Benefits> Benefits { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<EmployeeTraining> EmployeeTrainings { get; set; } = null!;
        public DbSet<JobTitle> JobTitles { get; set; } = null!;
        public DbSet<Leave> Leaves { get; set; } = null!;
        public DbSet<Payroll> Payrolls { get; set; } = null!;
        public DbSet<PerformanceReview> PerformanceReviews { get; set; } = null!;
        public DbSet<Recruitment> Recruitments { get; set; } = null!;
        public DbSet<Training> Trainings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.EmployeeId);
                entity.HasIndex(x => x.Username).IsUnique();
                entity.HasIndex(x => x.Email).IsUnique();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.RoleName).IsUnique();
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.PermissionName).IsUnique();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.UserId);
                entity.HasIndex(x => x.RoleId);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.RoleId);
                entity.HasIndex(x => x.PermissionId);
            });

            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.RecruitmentId);
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.EmployeeId);
            });

            modelBuilder.Entity<Benefits>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.EmployeeId);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.ManagerId);
                entity.HasIndex(x => x.DepartmentName).IsUnique();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.JobTitleId);
                entity.HasIndex(x => x.DepartmentId);
                entity.HasIndex(x => x.ManagerId);
            });

            modelBuilder.Entity<Training>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<EmployeeTraining>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.EmployeeId);
                entity.HasIndex(x => x.TrainingId);
            });

            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Leave>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.EmployeeId);
            });

            modelBuilder.Entity<Payroll>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.EmployeeId);
            });

            modelBuilder.Entity<PerformanceReview>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.EmployeeId);
                entity.HasIndex(x => x.ReviewerId);
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var uid = _currentUserAccessor.GetCurrentUserId();
            var fullname = _currentUserAccessor.GetCurrentFullname();
            var entries = ChangeTracker
            .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedBy = !string.IsNullOrWhiteSpace(uid) ? Guid.Parse(uid) : null;
                    ((BaseEntity)entry.Entity).CreatedByName = !string.IsNullOrWhiteSpace(fullname) ? fullname : "SYSTEM";
                    ((BaseEntity)entry.Entity).CreatedDate = DateTime.UtcNow;

                    ((BaseEntity)entry.Entity).ModifiedBy = null;
                    ((BaseEntity)entry.Entity).ModifiedByName = null;
                    ((BaseEntity)entry.Entity).ModifiedDate = null;
                }
                else if (entry.State == EntityState.Modified)
                {
                    ((BaseEntity)entry.Entity).ModifiedBy = !string.IsNullOrWhiteSpace(uid) ? Guid.Parse(uid) : null;
                    ((BaseEntity)entry.Entity).ModifiedByName = !string.IsNullOrWhiteSpace(fullname) ? fullname : "SYSTEM";
                    ((BaseEntity)entry.Entity).ModifiedDate = DateTime.UtcNow;
                }

            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
