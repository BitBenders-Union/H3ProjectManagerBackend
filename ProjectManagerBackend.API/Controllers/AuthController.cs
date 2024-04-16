using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;
using ProjectManagerBackend.Repo.Services;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : GenericController<UserLogin>

    {
        private readonly IMappingService mappingService;
        private readonly IUserRepository userRepository;
        private readonly IHashingService hashingService;



        public AuthController(

            IGenericRepository<UserLogin> genericRepo,

            IMappingService mappingService,
            IUserRepository userRepository,
            IHashingService hashingService) : base(genericRepo)
        {
            this.mappingService = mappingService;
            this.userRepository = userRepository;
            this.hashingService = hashingService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserLogin>> RegisterUser([FromBody] UserDetailDTO userDTO)
        {
            if (userDTO.UserName.IsNullOrEmpty() || userDTO.Password.IsNullOrEmpty())
            {
                return BadRequest();
            }

            var userExist = await userRepository.CheckUser(userDTO.UserName);

            if (userExist)
            {
                ModelState.AddModelError("", "User already exist");
                return StatusCode(442, ModelState);
            }

            byte[] salt = hashingService.GenerateSalt();
            byte[] hash = hashingService.PasswordHashing(userDTO.Password, salt);


            UserLogin newUser = new()

            {
                Username = userDTO.UserName,
                PasswordHash = hash,
                PasswordSalt = salt,
                IsActive = true,
                UserDetail = new()
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName
                }

            };

            return Ok(await _repository.CreateAsync(newUser));
        }
    }
}