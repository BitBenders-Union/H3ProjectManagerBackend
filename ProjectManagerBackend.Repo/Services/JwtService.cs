using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Services
{
    public class JwtService : IJwtService
    {
        public string CreateRefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public string CreateToken(UserDetail userDetail)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            throw new NotImplementedException();
        }
    }
}
