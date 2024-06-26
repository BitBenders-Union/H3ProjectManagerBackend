﻿using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Interfaces
{
    public interface IProjectRepository
    {
        public Task<List<Project>> GetAllProjectDashboards(int userId);
        public Task<string> GetOwnerName(int ownerId);
        //public Task<Project> GetProject(int id);
        public Task<Project> Get(int id);
        public Task CreateManyToMany(int projectId, int userId);
        public bool UpdateProject(Project updatedProject);
        public bool DeletePD(List<ProjectDepartment> pd);
        public bool UpdatePD(List<ProjectDepartment> pd);
        public bool DeletePUD(List<ProjectUserDetail> pud);
        public bool UpdatePUD(List<ProjectUserDetail> pud);
    }
}
