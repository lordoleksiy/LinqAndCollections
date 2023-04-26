using CollectionsAndLinq.BLL.DTOs;
using CollectionsAndLinq.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataProcessingController: ControllerBase
{
    private readonly IDataProcessingService service;
    public DataProcessingController(IDataProcessingService service)
    {
        this.service = service;
    }

    [HttpGet("TaskCountInProjectByUserID/{userId}")]
    public async Task<ActionResult<Dictionary<string, int>>> GetTasksCountInProjectsByUserIdAsync(int userId) => Ok(await service.GetTasksCountInProjectsByUserIdAsync(userId));
    [HttpGet("TasksByTeamSize/{teamSize}")]
    public async Task<ActionResult<IEnumerable<string>>> GetProjectsByTeamSizeAsync(int teamSize) => Ok((await service.GetProjectsByTeamSizeAsync(teamSize)).Select(a => $"{a.Id}: {a.Name}"));
    [HttpGet("GetCapitalTasks/{userId}")]
    public async Task<ActionResult<IEnumerable<TaskDTO>>> GetCapitalTasksByUserIdAsync(int userId) => Ok(await service.GetCapitalTasksByUserIdAsync(userId));
    [HttpGet("GetSortedTeamByMembersWithYear/{year}")]
    public async Task<ActionResult<IEnumerable<TeamWithMembersDto>>> GetSortedTeamByMembersWithYearAsync(int year) => Ok(await service.GetSortedTeamByMembersWithYearAsync(year));
    [HttpGet("GetSortedUsersWithSortedTasks")]
    public async Task<ActionResult<List<UserWithTasksDto>>> GetSortedUsersWithSortedTasks() => Ok(await service.GetSortedUsersWithSortedTasksAsync());
    [HttpGet("GetUserInfo/{id}")]
    public async Task<ActionResult<UserInfoDto>> GetUserInfo(int id) => Ok(await service.GetUserInfoAsync(id));
    [HttpGet("GetProjectsInfo")]
    public async Task<ActionResult<List<ProjectInfoDto>>> GetProjectsInfo() => Ok(await service.GetProjectsInfoAsync());
}
