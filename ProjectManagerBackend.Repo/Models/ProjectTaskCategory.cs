namespace ProjectManagerBackend.Repo.Models;

public class ProjectTaskCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ProjectTask> ProjectTask { get; set; }
}