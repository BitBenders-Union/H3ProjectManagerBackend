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

        /// <summary>
        /// This method generates a new refresh token for a user. 
        /// It first generates a random byte array of length 64, converts it to a base64 string, 
        /// and then checks if that token already exists in the UserDetails table. If it does, it recursively calls itself to generate a new token.
        /// </summary>
        /// <returns>A unique refresh token.</returns>
        public async Task<string> CreateRefreshToken()
        {
            // Generate a random byte array of length 64
            var tokenBytes = RandomNumberGenerator.GetBytes(64);

            // Convert the byte array to a base64 string
            var refreshToken = Convert.ToBase64String(tokenBytes);

            // Check if the generated token already exists in the UserDetails table
            if (await dataContext.UserDetails.AnyAsync(x => x.RefreshToken == refreshToken))
            {
                // If the token already exists, recursively call the method to generate a new token
                return await CreateRefreshToken();
            }

            // If the token is unique, return it
            return refreshToken;
        }

        /// <summary>
        /// This method takes a JWT token and returns a ClaimsPrincipal object if the token is valid, otherwise it returns null.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>A ClaimsPrincipal object if the token is valid, otherwise null.</returns>
        public ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            // Create a new instance of JwtSecurityTokenHandler to handle JWT tokens
            var toeknHandler = new JwtSecurityTokenHandler();

            // Get the secret key from the configuration section "JwtSettings:SecretKey"
            var key = Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:SecretKey").Value!);

            // Define the validation parameters for the token
            var validationParameter = new TokenValidationParameters
            {
                // Set ValidateIssuerSigningKey to true to validate the token's signing key
                ValidateIssuerSigningKey = true,

                // Set the signing key to a SymmetricSecurityKey created from the secret key
                IssuerSigningKey = new SymmetricSecurityKey(key),

                // Set ValidateIssuer to false to disable token issuer validation
                ValidateIssuer = false,

                // Set ValidateAudience to false to disable token audience validation
                ValidateAudience = false
            };

            try
            {
                // Try to validate the token using the token handler and validation parameters
                var claimsPrincipal = toeknHandler.ValidateToken(token, validationParameter, out var validatedToken);

                // Return the validated claims principal
                return claimsPrincipal;
            }
            catch (SecurityTokenException)
            {
                // If the token validation fails, return null
                return null;
            }
        }
    }
}
