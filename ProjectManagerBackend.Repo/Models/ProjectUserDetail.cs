using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Models
{
    public class ProjectUserDetail
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int UserDetailId { get; set; }
        public UserDetail UserDetail { get; set; }
    }
}
