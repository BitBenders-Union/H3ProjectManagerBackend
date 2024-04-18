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
        private readonly IHashingService _hashingService;
        private readonly IJwtService _jwtService;


        public AuthController(

            IGenericRepository<UserDetail> genericRepo,
            IHashingService hashingService,
            IMappingService mapping,
            IUserRepository userRepository,
            IJwtService jwtService
            ) : base(genericRepo, mapping)
        {
            _mappingService = mapping;
            _userRepository = userRepository;
            _hashingService = hashingService;
            _jwtService = jwtService;
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

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            if (request == null)
            {
                return NotFound();
            }

            if (!await _userRepository.CheckUser(request.Username))
            {
                return NotFound();
            }

            var getUserDetail =  _userRepository.GetUserDetail(request.Username);
            byte[] hashRequestPassword = _hashingService.PasswordHashing(request.Password, getUserDetail.PasswordSalt);

            if (!await _userRepository.AccountExist(request.Username, hashRequestPassword))
            {
                ModelState.AddModelError("", "Something went wrong with the server");
                return StatusCode(500, ModelState);
            }

            getUserDetail.Token = _jwtService.CreateToken(getUserDetail);
            var newRefreshToken = await _jwtService.CreateRefreshToken();
            var newToken = getUserDetail.Token;
            getUserDetail.RefreshToken = newRefreshToken;
            getUserDetail.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            await _userRepository.Save();
            
            return Ok(new TokenDTO()
            {
                AccessToken = newToken,
                RefreshToken = newRefreshToken
            });
        }

    }
}