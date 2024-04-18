
namespace ProjectManagerBackend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectCategoryController : ControllerBase
{
    private readonly IProjectCategory _projectCategory;

    public ProjectCategoryController(IProjectCategory projectCategory)
    {
        _projectCategory = projectCategory;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectCategory projectCategory)
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

            var createdCategory = await _projectCategory.CreateCategory(projectCategory);

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
    public async Task<IActionResult> Update(ProjectCategory projectCategory)
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

        return Ok(await _projectCategory.UpdateCategory(projectCategory));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(ProjectCategory projectCategory)
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
            if (!await _projectCategory.DoesExist(projectCategory.Id))
            {
                return NotFound("Category not found");
            }

            return Ok(await _projectCategory.DeleteCategory(projectCategory));
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

}
