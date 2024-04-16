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

    public class AuthController : GenericController<UserDetail, UserDetailDTO, UserDetailDTOResponse>

    {
        private readonly IMappingService _mappingService;
        private readonly IUserRepository _userRepository;


        public AuthController(

            IGenericRepository<UserDetail> genericRepo,

            IMappingService mapping,
            IUserRepository userRepository
            ) : base(genericRepo, mapping)
        {
            _mappingService = mapping;
            _userRepository = userRepository;
        }


        [HttpPost]
        public async override Task<ActionResult<UserDetailDTOResponse>> Create([FromBody] UserDetailDTO userDTO)
        {
            if (userDTO.Username.IsNullOrEmpty() || userDTO.Password.IsNullOrEmpty())
            {
                return BadRequest();
            }

            if (await _userRepository.CheckUser(userDTO.Username))
            {
                ModelState.AddModelError("", "User already exist");
                return StatusCode(442, ModelState);
            }

            var mappingTing = _mappingService.AddUser(userDTO);

            var result = await _userRepository.CreateUserAsync(mappingTing);

            return Ok(
                _mappingService.Map<UserDetail, UserDetailDTOResponse>(result)
                );
        }
    }
}