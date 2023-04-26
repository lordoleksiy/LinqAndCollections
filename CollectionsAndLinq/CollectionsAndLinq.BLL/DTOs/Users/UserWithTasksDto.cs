namespace CollectionsAndLinq.BLL.DTOs;

public record UserWithTasksDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime RegisteredAt,
    DateTime BirthDay,
    List<TaskDTO> Tasks)
{

}
