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

    public class AuthController : GenericController<UserDetail, UserDetailDTO>

    {
        private readonly IMappingService<UserDetail, UserDetailDTO> mappingService;
        private readonly IUserRepository userRepository;


        public AuthController(

            IGenericRepository<UserDetail> genericRepo,

            IMappingService<UserDetailDTO, UserDetail> mapping,
            IUserRepository userRepository
            ) : base(genericRepo, mapping)
        {
            this.mappingService = mappingService;
            this.userRepository = userRepository;
        }


        [HttpPost]
        public async override Task<ActionResult<UserDetail>> Create([FromBody] UserDetailDTO userDTO)
        {
            if (userDTO.UserName.IsNullOrEmpty() || userDTO.Password.IsNullOrEmpty())
            {
                return BadRequest();
            }

            if (await userRepository.CheckUser(userDTO.UserName))
            {
                ModelState.AddModelError("", "User already exist");
                return StatusCode(442, ModelState);
            }


            return Ok(
                await _repository.CreateAsync(
                    mappingService.AddUser(userDTO)
                    )
                );
        }
    }
}