using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.DTOs
{
    public class UserDetailDTOResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
