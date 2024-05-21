using Microsoft.EntityFrameworkCore;
using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Models;


namespace ProjectManagerBackend.Repo.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectTaskCategory> ProjectTaskCategories { get; set; }
        public DbSet<ProjectTaskStatus> ProjectTaskStatus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }

        // many to many
        /* 
        Project - UserDetail
        projectTask - Userdetail
        project - department
        department - location
         */

        public DbSet<ProjectUserDetail> ProjectUserDetails { get; set; }
        public DbSet<ProjectTaskUserDetail> ProjectTaskUserDetails { get; set; }
        public DbSet<ProjectDepartment> ProjectDepartments { get; set; }
        public DbSet<DepartmentLocation> DepartmentLocations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectUserDetail>()
                .HasKey(pd => new { pd.ProjectId, pd.UserDetailId });

            modelBuilder.Entity<ProjectUserDetail>()
                .HasOne(pd => pd.Project)
                .WithMany(p => p.ProjectUserDetail)
                .HasForeignKey(pd => pd.ProjectId);

            modelBuilder.Entity<ProjectUserDetail>()
                .HasOne(pd => pd.UserDetail)
                .WithMany(u => u.ProjectUserDetail)
                .HasForeignKey(pd => pd.UserDetailId);


            modelBuilder.Entity<ProjectTaskUserDetail>()
                .HasKey(pu => new { pu.ProjectTaskId, pu.UserDetailId });

            modelBuilder.Entity<ProjectTaskUserDetail>()
                .HasOne(pu => pu.ProjectTask)
                .WithMany(p => p.ProjectTaskUserDetail)
                .HasForeignKey(pu => pu.ProjectTaskId);

            modelBuilder.Entity<ProjectTaskUserDetail>()
                .HasOne(pu => pu.UserDetail)
                .WithMany(u => u.ProjectTaskUserDetail)
                .HasForeignKey(pu => pu.UserDetailId);


            modelBuilder.Entity<ProjectDepartment>()
                .HasKey(pd => new { pd.ProjectId, pd.DepartmentId });

            modelBuilder.Entity<ProjectDepartment>()
                .HasOne(pd => pd.Project)
                .WithMany(p => p.ProjectDepartment)
                .HasForeignKey(pd => pd.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProjectDepartment>()
                .HasOne(pd => pd.Department)
                .WithMany(d => d.ProjectDepartment)
                .HasForeignKey(pd => pd.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<DepartmentLocation>()
                .HasKey(dl => new { dl.DepartmentId, dl.LocationId });

            modelBuilder.Entity<DepartmentLocation>()
                .HasOne(dl => dl.Department)
                .WithMany(d => d.DepartmentLocation)
                .HasForeignKey(dl => dl.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<DepartmentLocation>()
                .HasOne(dl => dl.Location)
                .WithMany(l => l.DepartmentLocation)
                .HasForeignKey(dl => dl.LocationId)
                .OnDelete(DeleteBehavior.NoAction);


            // seed data

            // Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "IT"},
                new Department { Id = 2, Name = "HR" },
                new Department { Id = 3, Name = "Finance" },
                new Department { Id = 4, Name = "Admin" },
                new Department { Id = 5, Name = "Sales" });


            // Locations
            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "Cph", Address = "Copenhagen" },
                new Location { Id = 2, Name = "Aarhus", Address = "Aarhus" },
                new Location { Id = 3, Name = "Odense", Address = "Odense" },
                new Location { Id = 4, Name = "Aalborg", Address = "Aalborg" },
                new Location { Id = 5, Name = "Roskilde", Address = "Roskilde" });

            // Priorities

            modelBuilder.Entity<Priority>().HasData(
                new Priority { Id = 1, Name = "Low", Level = 0 },
                new Priority { Id = 2, Name = "Medium", Level = 1 },
                new Priority { Id = 3, Name = "High", Level = 2 },
                new Priority { Id = 4, Name = "Critical", Level = 3 },
                new Priority { Id = 5, Name = "Critical+", Level = 4 },
                new Priority { Id = 6, Name = "Critical++", Level = 5 });

            // Project Categories

            modelBuilder.Entity<ProjectCategory>().HasData(
                new ProjectCategory { Id = 1, Name = "Web Development"},
                new ProjectCategory { Id = 2, Name = "Mobile Development" },
                new ProjectCategory { Id = 3, Name = "Desktop Development" },
                new ProjectCategory { Id = 4, Name = "Game Development" },
                new ProjectCategory { Id = 5, Name = "AI Development" });

            // Project Status
            modelBuilder.Entity<ProjectStatus>().HasData(
                new ProjectStatus { Id = 1, Name = "New" },
                new ProjectStatus { Id = 2, Name = "In Progress" },
                new ProjectStatus { Id = 3, Name = "Completed" },
                new ProjectStatus { Id = 4, Name = "On Hold" },
                new ProjectStatus { Id = 5, Name = "Cancelled" });

            // Project Task Categories
            modelBuilder.Entity<ProjectTaskCategory>().HasData(
                new ProjectTaskCategory { Id = 1, Name = "Development" },
                new ProjectTaskCategory { Id = 2, Name = "Testing" },
                new ProjectTaskCategory { Id = 3, Name = "Deployment" },
                new ProjectTaskCategory { Id = 4, Name = "Documentation" },
                new ProjectTaskCategory { Id = 5, Name = "Meeting" });

            // Project Task Status
            modelBuilder.Entity<ProjectTaskStatus>().HasData(
                new ProjectTaskStatus { Id = 1, Name = "New" },
                new ProjectTaskStatus { Id = 2, Name = "In Progress" },
                new ProjectTaskStatus { Id = 3, Name = "Completed" },
                new ProjectTaskStatus { Id = 4, Name = "On Hold" },
                new ProjectTaskStatus { Id = 5, Name = "Cancelled" });
                        
            // Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Admin role", IsActive = true },
                new Role { Id = 2, Name = "User", Description = "User role", IsActive = false },
                new Role { Id = 3, Name = "Manager", Description = "Manager role", IsActive = true },
                new Role { Id = 4, Name = "Developer", Description = "Developer role", IsActive = true },
                new Role { Id = 5, Name = "Tester", Description = "Tester role", IsActive = true });
        }

    }
}
