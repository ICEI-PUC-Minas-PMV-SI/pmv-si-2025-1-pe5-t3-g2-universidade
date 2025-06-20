using ApiNovaHorizonte.Interfaces;
using ApiNovaHorizonte.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Api/V1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("Get-All")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet]
    [Route("Get-By-Id/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromBody] Usuario user)
    {
        var id = await _userService.AddAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = id }, user);
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Usuario user)
    {
        if (id != user.Id)
            return BadRequest("ID da URL diferente do corpo da requisição.");

        var updated = await _userService.UpdateAsync(user);
        if (!updated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _userService.RemoveAsync(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }
}
