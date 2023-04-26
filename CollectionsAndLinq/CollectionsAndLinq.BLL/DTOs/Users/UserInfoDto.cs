namespace CollectionsAndLinq.BLL.DTOs;

public record UserInfoDto(
    UserDTO User,
    ProjectDTO LastProject,
    int LastProjectTasksCount,
    int NotFinishedOrCanceledTasksCount,
    TaskDTO LongestTask)
{

}
