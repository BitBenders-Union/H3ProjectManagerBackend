using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LocationDTO>? Locations { get; set; }
        public List<UserDetailDTO>? UserDetails { get; set; }
        public List<ProjectDTO>? Projects { get; set; }
    }
}
