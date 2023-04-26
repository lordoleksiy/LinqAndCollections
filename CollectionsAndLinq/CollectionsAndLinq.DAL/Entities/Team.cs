using CollectionsAndLinq.DAL.Interfaces;

namespace CollectionsAndLinq.DAL.Entities;

public class Team : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<Project> Projects { get; set; }
    public IEnumerable<User> Users { get; set; }
}
