﻿using Microsoft.EntityFrameworkCore;
using ProjectManagerBackend.Repo.Data;
using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;
using System.Collections;
using System.Xml;


namespace ProjectManagerBackend.Repo
{
    public class MappingService : IMappingService
    {
        private readonly IHashingService hashingService;
        private readonly DataContext _context;

        public MappingService(IHashingService hashingService, DataContext context)
        {
            this.hashingService = hashingService;
            _context = context;
        }
        public UserDetail AddUser(UserDetailDTO userDetailDTO)
        {
            byte[] salt = hashingService.GenerateSalt();
            byte[] hash = hashingService.PasswordHashing(userDetailDTO.Password, salt);

            UserDetail userDetail = new()
            {
                Username = userDetailDTO.Username,
                PasswordHash = hash,
                PasswordSalt = salt,
                IsActive = true,
                FirstName = userDetailDTO.FirstName,
                LastName = userDetailDTO.LastName,
                CreatedDate = DateTime.Now
            };

            return userDetail;
        }

        public UserDetail UserToken(TokenDTO tokenDTO)
        {
            UserDetail userToken = new()
            {
                Token = tokenDTO.AccessToken,
                RefreshToken = tokenDTO.RefreshToken
            };

            return userToken;
        }

        public TMapped? Map<T, TMapped>(T source)
        {
            if (source == null)
                return default;

            // since we don't know the type of the source, we can't use the new keyword.
            // therefore we are forced to use the Activator.CreateInstance<T>(); to create an object of our model
            // look at link, for an explanation.
            // https://stackoverflow.com/questions/1649066/activator-createinstancet-vs-new
            var destination = Activator.CreateInstance<TMapped>();

            // get the properties of the entities
            var sourceProperties = typeof(T).GetProperties();
            var destinationProperties = typeof(TMapped).GetProperties();


            // loop over the properties in the source
            foreach (var sourceProperty in sourceProperties)
            {
                // find matching property
                var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);

                // if matching property exist
                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    // map the property from source to destination
                    // source is input, destination = output
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }

            // return mapped object
            return destination;
        }

        //Generic list mapping
        public List<TMapped>? MapList<T, TMapped>(List<T> source)
        {
            if (source == null)
                return default;

            var destination = new List<TMapped>();

            var sourceType = typeof(T);
            var destinationType = typeof(TMapped);

            foreach (var item in source)
            {
                var mappedItem = Activator.CreateInstance<TMapped>();

                foreach (var sourceProperty in sourceType.GetProperties())
                {
                    var destinationProperty = destinationType.GetProperty(sourceProperty.Name, sourceProperty.PropertyType);

                    if (destinationProperty != null && destinationProperty.CanWrite)
                    {
                        destinationProperty.SetValue(mappedItem, sourceProperty.GetValue(item));
                    }
                }

                destination.Add(mappedItem);
            }

            return destination;
        }



        public UserDetail UserLogin(LoginDTO loginDTO)
        {
            UserDetail userDetail = new()
            {
                Username = loginDTO.Username,
            };
            return userDetail;
        }


        public async Task<Project> ProjectCreateMapping(ProjectDTO dto)
        {
            Project project = new()
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Owner = dto.Owner,
                ProjectStatus = await _context.ProjectStatus.FirstOrDefaultAsync(x => x.Name == dto.Status.Name),
                ProjectCategory = await _context.ProjectCategories.FirstOrDefaultAsync(x => x.Name == dto.Category.Name),
                Priority = await _context.Priorities.FirstOrDefaultAsync(x => x.Level == dto.Priority.Level),
            };

            return project;
        }

        public ProjectDTO ProjectMapping(Project dto)
        {
            ProjectDTO project = new ProjectDTO
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Owner = dto.Owner,
                Status = Map<ProjectStatus, ProjectStatusDTO>(dto.ProjectStatus),
                Category = Map<ProjectCategory, ProjectCategoryDTO>(dto.ProjectCategory),
                Priority = Map<Priority, PriorityDTO>(dto.Priority),
                Client = Map<Client, ClientDTO>(dto.Client),
                Departments = dto.ProjectDepartment.Select(x => Map<Department, DepartmentDTO>(x.Department)).ToList(),
                Users = dto.ProjectUserDetail.Select(x => Map<UserDetail, UserDetailDTOResponse>(x.UserDetail)).ToList(),
            };

            return project;
        }

        public Project ProjectMapping(ProjectDTO dto)
        {
            Project project = new Project
            {
                Id = dto.Id,
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Owner = dto.Owner,
                ProjectStatus = Map<ProjectStatusDTO, ProjectStatus>(dto.Status),
                ProjectCategory = Map<ProjectCategoryDTO, ProjectCategory>(dto.Category),
                Priority = Map<PriorityDTO, Priority>(dto.Priority),
                Client = Map<ClientDTO, Client>(dto.Client),    
                ProjectDepartment = dto.Departments.Select(x => new ProjectDepartment { Department = Map<DepartmentDTO, Department>(x)}).ToList(),
                ProjectUserDetail = dto.Users.Select(x => new ProjectUserDetail { UserDetail = Map<UserDetailDTOResponse, UserDetail>(x) }).ToList(),

            };
            return project;
        }


        public async Task<Project> ProjectUpdateMap(ProjectDTO dto)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectDepartment)
                    .ThenInclude(pd => pd.Department)
                .Include(p => p.ProjectUserDetail)
                    .ThenInclude(pu => pu.UserDetail)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            project.Name = dto.Name;
            project.StartDate = dto.StartDate;
            project.EndDate = dto.EndDate;
            project.Owner = dto.Owner;
            project.Priority = await _context.Priorities.FirstOrDefaultAsync(x => x.Id == dto.Priority.Id);
            project.ProjectStatus = await _context.ProjectStatus.FirstOrDefaultAsync(x => x.Id == dto.Status.Id);
            project.ProjectCategory = await _context.ProjectCategories.FirstOrDefaultAsync(x => x.Id == dto.Category.Id);


            // no reason to remove the existing departments and users, since we are updating the project
            // if we do this we can't update the project, since the data we are updating on is removed
            // and if we are to do it later, it should not be inside the mapping service
            
            //_context.ProjectDepartments.RemoveRange(project.ProjectDepartment);
            foreach (var departmentDto in dto.Departments)
            {
                // finds first department that matches the id
                var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == departmentDto.Id);

                // used to see if the department already exist in the return object 'project'
                var existingProjectDepartment = project.ProjectDepartment.FirstOrDefault(pd => pd.DepartmentId == department.Id);

                if (existingProjectDepartment != null)
                {
                    existingProjectDepartment.Department = department;
                }
                else
                {
                    ProjectDepartment projectDepartment = new ProjectDepartment
                    {
                        DepartmentId = department.Id,
                        ProjectId = project.Id
                    };
                    project.ProjectDepartment.Add(projectDepartment);
                }
            }

            //_context.ProjectUserDetails.RemoveRange(project.ProjectUserDetail);
            foreach (var userDto in dto.Users)
            {
                var userDetail = await _context.UserDetails.FirstOrDefaultAsync(ud => ud.Id == userDto.Id);

                var existingProjectUserDetail = project.ProjectUserDetail.FirstOrDefault(pu => pu.UserDetailId == userDetail.Id);

                if (existingProjectUserDetail != null)
                {
                    // Update existing entry in the junction table
                    existingProjectUserDetail.UserDetail = userDetail;
                }
                else
                {
                    // Add new entry to the junction table
                    project.ProjectUserDetail.Add(new ProjectUserDetail {
                        UserDetailId = userDetail.Id,
                        ProjectId = project.Id
                    });
                }
            }

            return project;
        }


        public async Task<UserDetail> UserMap(UserDepartmentResponseDTO dto)
        {
            UserDetail user = await _context.UserDetails.FirstOrDefaultAsync(x => x.Id == dto.Id);

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Department = Map<DepartmentDTO, Department>(dto.Department);
            user.Role = Map<RoleDTO, Role>(dto.Role);

            return user;

        }

        public UserDetailDTOResponse UserMap(UserDetail user)
        {
            return new UserDetailDTOResponse
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedDate = user.CreatedDate
            };
        }

        public async Task<ProjectTask> ProjectTaskCreateMapping(ProjectTaskDTO dto)
        {
            ProjectTask projectTask = new()
            {
                Name = dto.Name,
                Description = dto.Description,
                Priority = await _context.Priorities.FirstOrDefaultAsync(x => x.Id == dto.Priority.Id),
                Status = await _context.ProjectTaskStatus.FirstOrDefaultAsync(x => x.Id == dto.Status.Id),
                ProjectTaskCategory = await _context.ProjectTaskCategories.FirstOrDefaultAsync(x => x.Id == dto.ProjectTaskCategory.Id),
                Project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == dto.ProjectId),
                Comments = new List<Comment>(),
                ProjectTaskUserDetail = new List<ProjectTaskUserDetail>()
            };



            return projectTask;
        }

        public async Task<ProjectTask> ProjectTaskUpdateMapping(ProjectTaskDTO dto)
        {
            // Fetch a single project task from the database that matches the specified ID.
            // This query includes related entities to avoid multiple database trips (eager loading).
            var projectTask = await _context.ProjectTasks.Include(pt => pt.ProjectTaskUserDetail)
                .ThenInclude(ptud => ptud.UserDetail)
                .Include(pt => pt.Priority)
                .Include(pt => pt.Status)
                .Include(pt => pt.ProjectTaskCategory)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            // Update the project task properties with the new values from the DTO
            projectTask.Name = dto.Name;
            projectTask.Description = dto.Description;
            projectTask.Priority.Name = dto.Priority.Name;
            projectTask.Priority.Level = dto.Priority.Level;
            projectTask.Status.Name = dto.Status.Name;
            projectTask.ProjectTaskCategory.Name = dto.ProjectTaskCategory.Name;


            _context.ProjectTaskUserDetails.RemoveRange(projectTask.ProjectTaskUserDetail); // Remove old user details and add new ones
            projectTask.ProjectTaskUserDetail.Clear(); // Clear the existing collection to avoid duplicate entries

            //For each user in the DTO, add a new ProjectTaskUserDetail to the project task, if the user is not null, and save changes to the database
            foreach (var userDto in dto.ProjectTaskUserDetail)
            {
                var userDetail = await _context.UserDetails.FirstOrDefaultAsync(ud => ud.Id == userDto.Id);
                if (userDetail != null)
                {
                    projectTask.ProjectTaskUserDetail.Add(new ProjectTaskUserDetail { UserDetail = userDetail });
                }
            }

            await _context.SaveChangesAsync(); // Save changes to the database

            return projectTask;
        }

        public async Task<Project> ProjectMappingFromDto(ProjectDTO dto)
        {
            var projectStatus = await _context.ProjectStatus.FirstOrDefaultAsync(x => x.Name == dto.Status.Name);
            var projectCategory = await _context.ProjectCategories.FirstOrDefaultAsync(x => x.Name == dto.Category.Name);
            var priority = await _context.Priorities.FirstOrDefaultAsync(x => x.Level == dto.Priority.Level);

            var projectDepartment = new List<ProjectDepartment>();

            foreach(var item in dto.Departments)
            {
                var department = await _context.Departments.FirstOrDefaultAsync(x => x.Name == item.Name);
                projectDepartment.Add(new ProjectDepartment {
                    DepartmentId = department.Id,
                    ProjectId = dto.Id
                });
            }

            var projectUserDetail = new List<ProjectUserDetail>();

            foreach (var item in dto.Users)
            {
                var userDetail = await _context.UserDetails.FirstOrDefaultAsync(x => x.Username == item.Username);
                projectUserDetail.Add(new ProjectUserDetail
                {
                    UserDetailId = userDetail.Id,
                    ProjectId = dto.Id
                });
            }

            var project = new Project
            {
                Id = dto.Id,
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Owner = dto.Owner,
                ProjectStatus = projectStatus,
                ProjectCategory = projectCategory,
                Priority = priority,
                Client = null,
                ProjectDepartment = projectDepartment,
                ProjectUserDetail = projectUserDetail

            };

            return project;
        }
    }
}
