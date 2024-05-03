using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Models
{
    public class ProjectTaskUserDetail
    {
        public int ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; }
        public int UserDetailId { get; set; }
        public UserDetail UserDetail { get; set; }

    }
}
