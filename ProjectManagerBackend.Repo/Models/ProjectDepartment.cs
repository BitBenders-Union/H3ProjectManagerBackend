using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Models
{
    public class ProjectDepartment
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
