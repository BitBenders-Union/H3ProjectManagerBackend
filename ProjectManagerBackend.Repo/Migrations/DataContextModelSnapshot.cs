﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManagerBackend.Repo.Data;

#nullable disable

namespace ProjectManagerBackend.Repo.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DepartmentLocation", b =>
                {
                    b.Property<int>("DepartmentsId")
                        .HasColumnType("int");

                    b.Property<int>("LocationsId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentsId", "LocationsId");

                    b.HasIndex("LocationsId");

                    b.ToTable("DepartmentLocation");
                });

            modelBuilder.Entity("DepartmentProject", b =>
                {
                    b.Property<int>("DepartmentsId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentsId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("DepartmentProject");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectTaskId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserDetailId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectTaskId");

                    b.HasIndex("UserDetailId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectCategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("ProjectCategoryId");

                    b.HasIndex("ProjectStatusId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.ProjectCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectCategories");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.ProjectStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectStatus");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.ProjectTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectTaskCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PriorityId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectTaskCategoryId");

                    b.HasIndex("StatusId");

                    b.ToTable("ProjectTasks");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.ProjectTaskCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectTaskCategories");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.ProjectTaskStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectTaskStatus");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.UserDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("ProjectTaskUserDetail", b =>
                {
                    b.Property<int>("ProjectTasksId")
                        .HasColumnType("int");

                    b.Property<int>("UserDetailsId")
                        .HasColumnType("int");

                    b.HasKey("ProjectTasksId", "UserDetailsId");

                    b.HasIndex("UserDetailsId");

                    b.ToTable("ProjectTaskUserDetail");
                });

            modelBuilder.Entity("ProjectUserDetail", b =>
                {
                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("ProjectsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ProjectUserDetail");
                });

            modelBuilder.Entity("DepartmentLocation", b =>
                {
                    b.HasOne("ProjectManagerBackend.Repo.Models.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagerBackend.Repo.Models.Location", null)
                        .WithMany()
                        .HasForeignKey("LocationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DepartmentProject", b =>
                {
                    b.HasOne("ProjectManagerBackend.Repo.Models.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagerBackend.Repo.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Comment", b =>
                {
                    b.HasOne("ProjectManagerBackend.Repo.Models.ProjectTask", "ProjectTask")
                        .WithMany("Comments")
                        .HasForeignKey("ProjectTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagerBackend.Repo.Models.UserDetail", "UserDetail")
                        .WithMany()
                        .HasForeignKey("UserDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectTask");

                    b.Navigation("UserDetail");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Project", b =>
                {
                    b.HasOne("ProjectManagerBackend.Repo.Models.Client", "Client")
                        .WithMany("Projects")
                        .HasForeignKey("ClientId");

                    b.HasOne("ProjectManagerBackend.Repo.Models.Priority", "Priority")
                        .WithMany("Projects")
                        .HasForeignKey("PriorityId");

                    b.HasOne("ProjectManagerBackend.Repo.Models.ProjectCategory", "ProjectCategory")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectCategoryId");

                    b.HasOne("ProjectManagerBackend.Repo.Models.ProjectStatus", "ProjectStatus")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectStatusId");

                    b.Navigation("Client");

                    b.Navigation("Priority");

                    b.Navigation("ProjectCategory");

                    b.Navigation("ProjectStatus");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.ProjectTask", b =>
                {
                    b.HasOne("ProjectManagerBackend.Repo.Models.Priority", "Priority")
                        .WithMany("ProjectTasks")
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagerBackend.Repo.Models.Project", "Project")
                        .WithMany("ProjectTasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagerBackend.Repo.Models.ProjectTaskCategory", "ProjectTaskCategory")
                        .WithMany("ProjectTask")
                        .HasForeignKey("ProjectTaskCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagerBackend.Repo.Models.ProjectTaskStatus", "Status")
                        .WithMany("ProjectTasks")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Priority");

                    b.Navigation("Project");

                    b.Navigation("ProjectTaskCategory");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.UserDetail", b =>
                {
                    b.HasOne("ProjectManagerBackend.Repo.Models.Department", "Department")
                        .WithMany("UserDetails")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("ProjectManagerBackend.Repo.Models.Role", "Role")
                        .WithMany("UserDetails")
                        .HasForeignKey("RoleId");

                    b.Navigation("Department");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ProjectTaskUserDetail", b =>
                {
                    b.HasOne("ProjectManagerBackend.Repo.Models.ProjectTask", null)
                        .WithMany()
                        .HasForeignKey("ProjectTasksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagerBackend.Repo.Models.UserDetail", null)
                        .WithMany()
                        .HasForeignKey("UserDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectUserDetail", b =>
                {
                    b.HasOne("ProjectManagerBackend.Repo.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagerBackend.Repo.Models.UserDetail", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Client", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Department", b =>
                {
                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Priority", b =>
                {
                    b.Navigation("ProjectTasks");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Project", b =>
                {
                    b.Navigation("ProjectTasks");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.ProjectCategory", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.ProjectStatus", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.ProjectTask", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.ProjectTaskCategory", b =>
                {
                    b.Navigation("ProjectTask");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.ProjectTaskStatus", b =>
                {
                    b.Navigation("ProjectTasks");
                });

            modelBuilder.Entity("ProjectManagerBackend.Repo.Models.Role", b =>
                {
                    b.Navigation("UserDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
