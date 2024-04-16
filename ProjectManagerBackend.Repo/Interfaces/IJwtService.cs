using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Interfaces
{
    public interface IJwtService
    {
        public string CreateToken(UserLogin userLogin);
        public string CreateRefreshToken(string token);
    }
}
