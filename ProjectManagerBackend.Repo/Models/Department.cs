﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DepartmentLocation> DepartmentLocation { get; set; }
        public List<UserDetail> UserDetails { get; set; }
        public List<ProjectDepartment> ProjectDepartment { get; set; }

    }
}
