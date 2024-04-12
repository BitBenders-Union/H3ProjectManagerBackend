namespace ProjectManagerBackend.Repo.Models;

public class Priority
{
    public int Id { get; set; }
    public int Level { get; set; }
    public string Name { get; set; }
    public List<Project> Projects { get; set; }
    public List<ProjectTask> ProjectTasks { get; set; }

}