
namespace ProjectManagerBackend.Repo.Models;

public class ProjectTask
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public ProjectTaskCategory ProjectTaskCategory { get; set; }
    public Project Project { get; set; }
    public List<ProjectTaskUserDetail> ProjectTaskUserDetail { get; set; }
    public List<Comment>? Comments { get; set; }
}
