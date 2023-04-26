using System.Text.Json;
using CollectionsAndLinq.BLL.DTOs;
using CollectionsAndLinq.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService service;
    public ProjectsController(IProjectService projectService)
    {
        service = projectService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetAll() => Ok(await service.GetAll());

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDTO>> GetById(int id) => Ok(await service.GetValueById(id));
    [HttpPost]
    public async Task<ActionResult<ProjectDTO>> Create([FromBody] ProjectDTO project) => Created("Created entity", await service.Create(project));
    [HttpPut]
    public async Task<ActionResult<ProjectDTO>> Update([FromBody] ProjectDTO project) => Created("Updated entity", await service.Update(project));

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectDTO>> DeleteById(int id) => Ok(await service.Delete(id));
}
