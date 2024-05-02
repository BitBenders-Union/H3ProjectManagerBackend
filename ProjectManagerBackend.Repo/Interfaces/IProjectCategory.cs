using ProjectManagerBackend.Repo.Models;

namespace ProjectManagerBackend.Repo.Interfaces; 

public interface IProjectCategory
{
    public Task<ProjectCategory> CreateCategory(ProjectCategory category);
    public Task<ICollection<ProjectCategory>> GetAllCategories();
    public Task<ProjectCategory> GetCategoryById(int id);
    public Task<bool> UpdateCategory(ProjectCategory category);
    public Task<bool> DeleteCategory(int id);
    public Task<bool> DoesExist(int id);
}
