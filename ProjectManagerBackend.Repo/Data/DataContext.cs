using Microsoft.EntityFrameworkCore;
using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Data
{
    public class DataContext: DbContext
    {

        public DataContext(DbContextOptions<DataContext> options): base(options)
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
                .HasKey(pu => new {pu.ProjectTaskId, pu.UserDetailId });

            modelBuilder.Entity<ProjectTaskUserDetail>()
                .HasOne(pu => pu.ProjectTask)
                .WithMany(p => p.ProjectTaskUserDetail)
                .HasForeignKey(pu => pu.ProjectTaskId);

            modelBuilder.Entity<ProjectTaskUserDetail>()
                .HasOne(pu => pu.UserDetail)
                .WithMany(u => u.ProjectTaskUserDetail)
                .HasForeignKey(pu => pu.UserDetailId);


            modelBuilder.Entity<ProjectDepartment>()
                .HasKey(pd => new {pd.ProjectId, pd.DepartmentId});

            modelBuilder.Entity<ProjectDepartment>()
                .HasOne(pd => pd.Project)
                .WithMany(p => p.ProjectDepartment)
                .HasForeignKey(pd => pd.ProjectId);

            modelBuilder.Entity<ProjectDepartment>()
                .HasOne(pd => pd.Department)
                .WithMany(d => d.ProjectDepartment)
                .HasForeignKey(pd => pd.DepartmentId);


            modelBuilder.Entity<DepartmentLocation>()
                .HasKey(dl => new {dl.DepartmentId, dl.LocationId});

            modelBuilder.Entity<DepartmentLocation>()
                .HasOne(dl => dl.Department)
                .WithMany(d => d.DepartmentLocation)
                .HasForeignKey(dl =>  dl.DepartmentId);

            modelBuilder.Entity<DepartmentLocation>()
                .HasOne(dl => dl.Location)
                .WithMany(l => l.DepartmentLocation)
                .HasForeignKey(dl => dl.LocationId);


        }
    }
}
