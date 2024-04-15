using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UserDetail UserDetail { get; set; }
        public ProjectTask ProjectTask { get; set; }
    }
}
