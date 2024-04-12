
namespace ProjectManagerBackend.Repo.Models;

public class ProjectTaskStatus
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ProjectTask> ProjectTasks { get; set; }
}
