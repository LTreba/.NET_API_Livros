using LibraryApi.DTOs.Autores;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("autores")]
public class AutoresController : ControllerBase
{
    private readonly IAutorService _service;

    public AutoresController(IAutorService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CadastroAutorRequest request)
    {
        var id = await _service.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var autor = await _service.GetByIdAsync(id);
        if (autor is null) return NotFound();

        return Ok(autor);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? nome, [FromQuery] string? nacionalidade)
    {
        var autores = await _service.SearchAsync(nome, nacionalidade);
        return Ok(autores);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] CadastroAutorRequest request)
    {
        await _service.UpdateAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
