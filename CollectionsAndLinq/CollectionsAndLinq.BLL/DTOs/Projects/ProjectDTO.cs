namespace CollectionsAndLinq.BLL.DTOs;

public record ProjectDTO(
    int Id,
    int AuthorId,
    int TeamId,
    string Name,
    string Description,
    DateTime CreatedAt,
    DateTime Deadline)
{

}
