using CollectionsAndLinq.BLL.DTOs;
using CollectionsAndLinq.BLL.Interfaces;
using CollectionsAndLinq.DAL.Entities;
using CollectionsAndLinq.DAL.Interfaces;
using CollectionsAndLinq.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using static CollectionsAndLinq.WebApi.Infrastructure.MyAutoMapper;

namespace CollectionsAndLinq.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService service;
    public TeamsController(ITeamService taskService)
    {
        service = taskService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamDTO>>> GetAll() => Ok(await service.GetAll());
    [HttpGet("{id}")]
    public async Task<ActionResult<TeamDTO>> GetById(int id) => Ok(await service.GetValueById(id));
    [HttpPost]
    public async Task<ActionResult<TeamDTO>> Create([FromBody] TeamDTO task) => Created("Created entity", await service.Create(task));
    [HttpPut]
    public async Task<ActionResult<TeamDTO>> Update([FromBody] TeamDTO task) => Created("Updated entity", await service.Update(task));

    [HttpDelete("{id}")]
    public async Task<ActionResult<TeamDTO>> DeleteById(int id) => Ok(await service.Delete(id));
}
