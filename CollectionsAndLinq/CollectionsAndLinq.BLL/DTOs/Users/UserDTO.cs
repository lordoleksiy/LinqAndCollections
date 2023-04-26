namespace CollectionsAndLinq.BLL.DTOs;

public record UserDTO(
    int Id,
    int? TeamId,
    string FirstName,
    string LastName,
    string Email,
    DateTime RegisteredAt,
    DateTime BirthDay)
{

}
