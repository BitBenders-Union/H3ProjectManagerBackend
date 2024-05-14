using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagerBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {

        private readonly IMappingService _mappingService;
        private readonly IUserRepository _userRepository;
        private readonly IValidationService _validationService;

        public UserDetailsController(
            IMappingService mapping,
            IValidationService validationService,
            IUserRepository userRepository
            )
        {
            _mappingService = mapping;
            _userRepository = userRepository;
            _validationService = validationService;
        }

        // get controller for user details
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDepartmentResponseDTO>> GetUserDepartment(int id)
        {
            try
            {
                var user = await _userRepository.GetUserDetail(id);

                if (user == null)
                    return NotFound("No User Found");

                return new UserDepartmentResponseDTO
                {
                    Id = user.Id,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CreatedDate = user.CreatedDate,
                    Department = _mappingService.Map<Department, DepartmentDTO>(user.Department),
                    Role = _mappingService.Map<Role, RoleDTO>(user.Role)
                };

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // set an authguard so only the user can update it's own details
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUserDepartment(UserDepartmentResponseDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (dto == null)
                    return BadRequest("body cannot be null");

                if (!_validationService.WhiteSpaceValidation(dto))
                    return BadRequest("White Space Error");

                // Map the dto to the model

                var model = await _mappingService.UserMap(dto);

                if (!await _userRepository.UpdateUser(model))
                    return BadRequest("Could not update user");


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailDTOResponse>>> GetAll()
        {
            try
            {
                // get the userdetail
                // map to dto
                // return Ok

                List<UserDetail> result = await _userRepository.GetAll();

                List<UserDetailDTOResponse> resultList = new List<UserDetailDTOResponse>();

                foreach (var item in result)
                {
                    resultList.Add(_mappingService.UserMap(item));
                }

                return Ok(resultList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
