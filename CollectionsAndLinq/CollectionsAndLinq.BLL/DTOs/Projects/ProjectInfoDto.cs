namespace CollectionsAndLinq.BLL.DTOs;

public record ProjectInfoDto(
    ProjectDTO Project,
    TaskDTO LongestTaskByDescription,
    TaskDTO ShortestTaskByName,
    int? TeamMembersCount = null)
{

}
