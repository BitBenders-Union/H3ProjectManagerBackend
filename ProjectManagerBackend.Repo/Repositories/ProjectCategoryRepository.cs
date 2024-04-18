
using Microsoft.EntityFrameworkCore;
using ProjectManagerBackend.Repo.Data;
using ProjectManagerBackend.Repo.Interfaces;
using ProjectManagerBackend.Repo.Models;

namespace ProjectManagerBackend.Repo.Repositories;

public class ProjectCategoryRepository : IProjectCategory
{
    private readonly DataContext _context;

    public ProjectCategoryRepository(DataContext context)
    {
        _context = context;

    }
    public async Task<ProjectCategory> CreateCategory(ProjectCategory projectCategory)
    {
        await _context.AddAsync(projectCategory);
        await _context.SaveChangesAsync();

        return projectCategory;
    }

    public async Task<ICollection<ProjectCategory>> GetAllCategories()
    {
        return await _context.ProjectCategories.ToListAsync();
    }

    public async Task<ProjectCategory> GetCategoryById(int id)
    {
        return await _context.ProjectCategories.FirstOrDefaultAsync(x => x.Id == id);
    } 

    public async Task<ProjectCategory> UpdateCategory(ProjectCategory projectCategory)
    {
        _context.Update(projectCategory);
        await _context.SaveChangesAsync();
        return projectCategory;
    }
    public async Task<bool> DeleteCategory(ProjectCategory projectCategory)
    {
        _context.Remove(projectCategory);
        int saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

   
    public async Task<bool> DoesExist(int id)
    {
        return await _context.ProjectCategories.AnyAsync(x => x.Id == id);
    }

}
