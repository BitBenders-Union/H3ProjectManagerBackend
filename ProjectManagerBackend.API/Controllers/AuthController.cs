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

            var result = await _repository.CreateAsync(_mappingService.AddUser(userDTO));

            return Ok(_mappingService.Map<UserDetail, UserDetailDTOResponse>(result));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDetailDTO userDto)
        {
            try
            {
                if (userDto == null || userDto.Id == 0)
                    return BadRequest("Invalid User or Id");

                if (!ModelState.IsValid)
                    return StatusCode(500, ModelState);

                var userEntity = _mapping.AddUser(userDto);

                if (await _userRepository.CheckUser(userEntity.Username))
                    return NotFound("User not found");

                return await _userRepository.UpdateUser(userEntity) ? Ok() : BadRequest("Could not update");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}