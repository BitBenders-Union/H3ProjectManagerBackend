﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus? ProjectStatus { get; set; }
        public List<ProjectTask>? ProjectTasks { get; set; }
        public ProjectCategory? ProjectCategory { get; set; }
        public Priority? Priority { get; set; }
        public Client? Client { get; set; }
        public List<ProjectDepartment>? ProjectDepartment { get; set; }
        public List<ProjectUserDetail>? ProjectUserDetail { get; set; }
        public string Owner { get; set; }
    }
}
