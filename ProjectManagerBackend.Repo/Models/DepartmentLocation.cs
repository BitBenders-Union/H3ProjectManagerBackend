using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Models
{
    public class DepartmentLocation
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }

    }
}
