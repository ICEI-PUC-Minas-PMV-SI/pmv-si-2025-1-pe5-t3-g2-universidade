using ApiNovaHorizonte.DTOs;
using ApiNovaHorizonte.Interfaces;
using ApiNovaHorizonte.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet]
    [Route("Get-By-Id/{id}")]
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    [Route("Create")]
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        var id = await _userService.AddAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = id }, user);
    }

    [HttpPut]
    [Route("Update/{id}")]
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> Update(int id, [FromBody] User user)
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
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _userService.RemoveAsync(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }

    [HttpPost]
    [Route("Login")]

    public async Task<IActionResult> Login([FromBody] LoginDTO login)
    {
        var token = await _userService.LoginJwtAsync(login.Email, login.Senha);
        if (token == null)
            return Unauthorized("Credenciais inválidas");

        return Ok(new { Token = token });
    }
}
