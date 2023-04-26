using Microsoft.AspNetCore.Mvc;
using CollectionsAndLinq.BLL.Interfaces;
using CollectionsAndLinq.BLL.DTOs;

namespace CollectionsAndLinq.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService service;
    public TasksController(ITaskService taskService)
    {
        service = taskService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDTO>>> GetAll() => Ok(await service.GetAll());
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDTO>> GetById(int id) => Ok(await service.GetValueById(id));
    [HttpPost]
    public async Task<ActionResult<TaskDTO>> Create([FromBody] TaskDTO task) => Created("Created entity", await service.Create(task));
    [HttpPut]
    public async Task<ActionResult<TaskDTO>> Update([FromBody] TaskDTO task) => Created("Updated entity", await service.Update(task));

    [HttpDelete("{id}")]
    public async Task<ActionResult<TaskDTO>> DeleteById(int id) => Ok(await service.Delete(id));
}
