using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectManagerBackend.Repo.Data;
using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Services
{
    public class JwtService : IJwtService
    {
        private readonly DataContext dataContext;
        private readonly IConfiguration configuration;

        public JwtService(DataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
        }

        //Create token
        public string CreateToken(UserDetail userDetail)
        {
            //create list of info that's is included in the token
            List<Claim> claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.NameId, userDetail.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, userDetail.Username),
                new Claim("firstname", userDetail.FirstName),
                new Claim("lastname", userDetail.LastName),
            };

            //define a secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("JwtSettings:SecretKey").Value!));

            //create a encripted token with key and encryption method
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

           //include parameters in the token
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: cred
                );

            //Make the token
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<string> CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            if (await dataContext.UserDetails.AnyAsync(x => x.RefreshToken == refreshToken))
                return await CreateRefreshToken();
            return refreshToken;
        }

        public ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            var toeknHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:SecretKey").Value!);
            var validationParameter = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                var claimsPrincipal = toeknHandler.ValidateToken(token, validationParameter, out var validatedToken);
                return claimsPrincipal;
            }

            catch (SecurityTokenException)
            {
                return null;
            }
            
        }
    }
}
