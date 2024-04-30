using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.DTOs
{
    public class LoginDTO
    {
        required public string Username { get; set; }
        required public string Password { get; set; }
    }
}
