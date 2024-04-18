using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Interfaces
{
    public interface IJwtService
    {
        public string CreateToken(UserDetail userDetail);
        public Task<string> CreateRefreshToken();
        public ClaimsPrincipal GetClaimsPrincipal(string token);
    }
}
