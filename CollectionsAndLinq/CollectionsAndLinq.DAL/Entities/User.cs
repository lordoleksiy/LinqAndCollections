using CollectionsAndLinq.DAL.Interfaces;

namespace CollectionsAndLinq.DAL.Entities;

public class User: IEntity
{
    public int Id{ get; set; }
    public int? TeamId { get; set; }
    public string FirstName{ get; set; }
    public string LastName{ get; set; }
    public string Email{ get; set; }
    public DateTime RegisteredAt{ get; set; }
    public DateTime BirthDay{ get; set; }
    public Team? Team { get; set; }
    public IEnumerable<Project>? Projects { get; set; }
    public IEnumerable<Task>? Tasks { get; set; }
}
