using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatusDTO Status { get; set; }
        public ProjectCategoryDTO Category { get; set; }
        public PriorityDTO Priority { get; set; }
        public ClientDTO? Client { get; set; }
        public List<ProjectTaskDTO>? ProjectTasks { get; set; }
        public List<DepartmentDTO>? Departmenets { get; set; }
        public List<UserDetailDTO>? Users { get; set; }
        public string Owner { get; set; }

    }
}
