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
            _userRepository = userRepository;
        }


        [HttpPost]
        public async override Task<ActionResult<UserDetailDTOResponse>> Create([FromBody] UserDetailDTO userDTO)
        {
            if (userDTO.UserName.IsNullOrEmpty() || userDTO.Password.IsNullOrEmpty())
            {
                return BadRequest();
            }

            if (await _userRepository.CheckUser(userDTO.UserName))
            {
                ModelState.AddModelError("", "User already exist");
                return StatusCode(442, ModelState);
            }

            var result = await _repository.CreateAsync(
                    _mappingService.AddUser(userDTO)
                    );

            return Ok(
                _mappingService.Map<UserDetail, UserDetailDTOResponse>(result)
                );
        }
    }
}