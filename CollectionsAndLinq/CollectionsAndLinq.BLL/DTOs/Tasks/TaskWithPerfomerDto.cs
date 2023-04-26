namespace CollectionsAndLinq.BLL.DTOs;

public record TaskWithPerfomerDto(
    int Id,
    string Name,
    string Description,
    string State,
    DateTime CreatedAt,
    DateTime? FinishedAt,
    UserDTO Perfomer)
{

}
