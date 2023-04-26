namespace CollectionsAndLinq.BLL.DTOs;

public record TaskDTO(
    int Id,
    int ProjectId,
    int PerformerId,
    string Name,
    string Description,
    string State,
    DateTime CreatedAt,
    DateTime? FinishedAt)
{

}
