namespace CollectionsAndLinq.BLL.DTOs;

public record TeamWithMembersDto(
    int Id,
    string Name,
    List<UserDTO> Members)
{

}
