
namespace ProjectManagerBackend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectCategoryController : ControllerBase
{
    private readonly IProjectCategory _projectCategory;
    private readonly IMappingService _mappingService;

    public ProjectCategoryController(IProjectCategory projectCategory, IMappingService mappingService)
    {
        _projectCategory = projectCategory;
        _mappingService = mappingService;

    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectCategoryDTO projectCategory)
    {
        try
        {
            if (projectCategory == null)
            {
                return BadRequest("Category cannot be null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }

            var createdCategory = await _projectCategory.CreateCategory(_mappingService.Map<ProjectCategoryDTO, ProjectCategory>(projectCategory));

            if (createdCategory == null)
            {
                return BadRequest("Category could not be created");
            }

            return Ok(projectCategory);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }


    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _projectCategory.GetAllCategories();

        if (categories == null)
        {
            return NotFound("No categories found");
        }

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model state");
        }

        if (!await _projectCategory.DoesExist(id))
        {
            return NotFound("Category not found");
        }

        return Ok(await _projectCategory.GetCategoryById(id));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectCategoryDTO projectCategory)
    {
        if (projectCategory == null)
        {
            return BadRequest("Category cannot be null");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model state");
        }

        if (!await _projectCategory.DoesExist(projectCategory.Id))
        {
            return NotFound("Category not found");
        }

        return Ok(await _projectCategory.UpdateCategory(
            _mappingService.Map<ProjectCategoryDTO, ProjectCategory>(projectCategory)));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            if (id == null)
            {
                return BadRequest("Category cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }
            if (!await _projectCategory.DoesExist(id))
            {
                return NotFound("Category not found");
            }

            return Ok(await _projectCategory.DeleteCategory(id));
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

}
