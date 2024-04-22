using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public List<Project>? Projects { get; set; }

    }
}
