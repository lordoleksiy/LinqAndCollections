namespace CollectionsAndLinq.WebApi.Controllers;

using CollectionsAndLinq.BLL.DTOs;
using CollectionsAndLinq.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService service;
    public UsersController(IUserService taskService)
    {
        service = taskService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll() => Ok(await service.GetAll());
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetById(int id) => Ok(await service.GetValueById(id));
    [HttpPost]
    public async Task<ActionResult<UserDTO>> Create([FromBody] UserDTO task) => Created("Created entity", await service.Create(task));
    [HttpPut]
    public async Task<ActionResult<UserDTO>> Update([FromBody] UserDTO task) => Created("Updated entity", await service.Update(task));

    [HttpDelete("{id}")]
    public async Task<ActionResult<UserDTO>> DeleteById(int id) => Ok(await service.Delete(id));
}
