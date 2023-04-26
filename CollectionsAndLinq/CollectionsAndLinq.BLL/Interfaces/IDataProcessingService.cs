using CollectionsAndLinq.BLL.DTOs;

namespace CollectionsAndLinq.BLL.Interfaces;

public interface IDataProcessingService
{
    Task<Dictionary<string, int>> GetTasksCountInProjectsByUserIdAsync(int userId);
    Task<List<TaskDTO>> GetCapitalTasksByUserIdAsync(int userId);
    Task<List<(int Id, string Name)>> GetProjectsByTeamSizeAsync(int teamSize);
    Task<List<TeamWithMembersDto>> GetSortedTeamByMembersWithYearAsync(int year);
    Task<List<UserWithTasksDto>> GetSortedUsersWithSortedTasksAsync();
    Task<UserInfoDto> GetUserInfoAsync(int userId);
    Task<List<ProjectInfoDto>> GetProjectsInfoAsync();
    Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjectsAsync(PageModel pageModel, FilterModel filterModel, SortingModel sortingModel);
}

