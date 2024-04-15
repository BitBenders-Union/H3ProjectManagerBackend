using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.DTOs
{
    public class PriorityDTO
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public List<ProjectDTO> MyProperty { get; set; }
    }
}
