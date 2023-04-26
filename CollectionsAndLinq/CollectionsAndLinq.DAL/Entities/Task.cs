using CollectionsAndLinq.DAL.Interfaces;

namespace CollectionsAndLinq.DAL.Entities;

public class Task :IEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int PerformerId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public TaskState State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public Project Project { get; set; }
    public User? Performer { get; set; }
}
