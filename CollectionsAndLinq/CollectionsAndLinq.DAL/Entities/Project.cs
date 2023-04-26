using System.Net;
using CollectionsAndLinq.DAL.Interfaces;

namespace CollectionsAndLinq.DAL.Entities;

public class Project : IEntity
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int TeamId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime Deadline { get; set; }
    public User Author { get; set; }
    public Team Team { get; set; }
    public IEnumerable<Task> Tasks { get; set; }

}
