using ApiNovaHorizonte.Interfaces;
using ApiNovaHorizonte.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Api/V1/[controller]")]
public class MatriculaController : ControllerBase
{
    private readonly IMatriculaService _matriculaService;

    public MatriculaController(IMatriculaService matriculaService)
    {
        _matriculaService = matriculaService;
    }

    [HttpGet("Get-All")]
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> GetAll()
    {
        var matriculas = await _matriculaService.GetAllAsync();
        return Ok(matriculas);
    }

    [HttpGet("Get-By-Id/{id}")]
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> GetById(int id)
    {
        var matricula = await _matriculaService.GetByIdAsync(id);
        if (matricula == null)
            return NotFound();

        return Ok(matricula);
    }

    [HttpPost("Create")]
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> Create([FromBody] Matricula matricula)
    {
        var id = await _matriculaService.AddAsync(matricula);
        return CreatedAtAction(nameof(GetById), new { id = id }, matricula);
    }

    [HttpPut("Update/{id}")]
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> Update(int id, [FromBody] Matricula matricula)
    {
        if (id != matricula.Id)
            return BadRequest("ID da URL diferente do corpo da requisição.");

        var updated = await _matriculaService.UpdateAsync(matricula);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("Delete/{id}")]
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _matriculaService.RemoveAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpGet("Get-Matriculas-Pendentes")]
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> GetPendentes()
    {
        var pendentes = await _matriculaService.GetPendentesAsync();
        return Ok(pendentes);
    }

    [HttpPut("Aprovar/{id}")]
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> Aprovar(int id)
    {
        var result = await _matriculaService.AprovarAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPut("Rejeitar/{id}")]
    [Authorize(Roles = "Funcionario")]
    public async Task<IActionResult> Rejeitar(int id)
    {
        var result = await _matriculaService.RejeitarAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
