using ApiNovaHorizonte.Interfaces;
using ApiNovaHorizonte.Models;
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
    public async Task<IActionResult> GetAll()
    {
        var matriculas = await _matriculaService.GetAllAsync();
        return Ok(matriculas);
    }

    [HttpGet("Get-By-Id/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var matricula = await _matriculaService.GetByIdAsync(id);
        if (matricula == null)
            return NotFound();

        return Ok(matricula);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] Matricula matricula)
    {
        var id = await _matriculaService.AddAsync(matricula);
        return CreatedAtAction(nameof(GetById), new { id = id }, matricula);
    }

    [HttpPut("Update/{id}")]
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
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _matriculaService.RemoveAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
