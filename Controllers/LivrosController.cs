using LibraryApi.DTOs.Livros;
using LibraryApi.Models.Enums;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("livros")]
public class LivrosController : ControllerBase
{
    private readonly ILivroService _service;

    public LivrosController(ILivroService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CadastroLivroRequest request)
    {
        var id = await _service.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var livro = await _service.GetByIdAsync(id);
        if (livro is null) return NotFound();

        return Ok(livro);
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] string? isbn,
        [FromQuery] string? titulo,
        [FromQuery] string? nomeAutor,
        [FromQuery] GeneroLivro? genero,
        [FromQuery] int? anoPublicacao)
    {
        var livros = await _service.SearchAsync(isbn, titulo, nomeAutor, genero, anoPublicacao);
        return Ok(livros);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] CadastroLivroRequest request)
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
