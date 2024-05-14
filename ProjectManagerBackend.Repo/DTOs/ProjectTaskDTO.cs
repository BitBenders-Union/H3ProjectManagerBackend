using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.DTOs
{
    public class ProjectTaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public PriorityDTO Priority { get; set; }
        public ProjectTaskStatusDTO Status { get; set; }
        public ProjectTaskCategoryDTO ProjectTaskCategory { get; set; }
        public ProjectDTO Project { get; set; }
        public List<UserDetailDTOResponse> ProjectTaskUserDetail { get; set; }
        public List<CommentDTO>? Comments { get; set; }

    }
}
