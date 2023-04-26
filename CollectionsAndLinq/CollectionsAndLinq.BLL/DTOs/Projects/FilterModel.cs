namespace CollectionsAndLinq.BLL.DTOs;

public record FilterModel(
    string Name = null,
    string Description = null,
    string AutorFirstName = null,
    string AutorLastName = null,
    string TeamName = null)
{

}
