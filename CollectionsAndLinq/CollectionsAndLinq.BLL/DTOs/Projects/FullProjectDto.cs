namespace CollectionsAndLinq.BLL.DTOs;

public record FullProjectDto(
    int Id,
    string Name,
    string Description,
    DateTime CreatedAt,
    DateTime Deadline,
    List<TaskWithPerfomerDto> Tasks,
    UserDTO Author,
    TeamDTO Team)
{

}
