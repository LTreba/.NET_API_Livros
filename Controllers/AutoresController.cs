using LibraryApi.DTOs.Autores;
using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("autores")]
public class AutoresController : ControllerBase
{
    private readonly IAutorRepository _repository;

    public AutoresController(IAutorRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CadastroAutorRequest request)
    {
        var duplicado = await _repository.ExistsAsync(request.Nome, request.DataNascimento, request.Nacionalidade);
        if (duplicado)
            return Conflict(new { status = 409, message = "Registro Duplicado", errors = Array.Empty<object>() });

        var autor = new Autor
        {
            Nome = request.Nome,
            DataNascimento = request.DataNascimento,
            Nacionalidade = request.Nacionalidade,
            DataCadastro = DateTime.UtcNow
        };

        await _repository.AddAsync(autor);

        return CreatedAtAction(nameof(GetById), new { id = autor.Id }, null);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var autor = await _repository.GetByIdAsync(id);
        if (autor is null)
            return NotFound();

        var response = new AutorResponse
        {
            Id = autor.Id,
            Nome = autor.Nome,
            DataNascimento = autor.DataNascimento,
            Nacionalidade = autor.Nacionalidade
        };

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? nome, [FromQuery] string? nacionalidade)
    {
        var autores = await _repository.SearchAsync(nome, nacionalidade);

        var response = autores.Select(a => new AutorResponse
        {
            Id = a.Id,
            Nome = a.Nome,
            DataNascimento = a.DataNascimento,
            Nacionalidade = a.Nacionalidade
        });

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] CadastroAutorRequest request)
    {
        var autor = await _repository.GetByIdAsync(id);
        if (autor is null)
            return NotFound();

        var duplicado = await _repository.ExistsAsync(request.Nome, request.DataNascimento, request.Nacionalidade, ignoreId: id);
        if (duplicado)
            return Conflict(new { status = 409, message = "Registro Duplicado", errors = Array.Empty<object>() });

        autor.Nome = request.Nome;
        autor.DataNascimento = request.DataNascimento;
        autor.Nacionalidade = request.Nacionalidade;
        autor.DataUltimaAtualizacao = DateTime.UtcNow;

        await _repository.UpdateAsync(autor);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var autor = await _repository.GetByIdAsync(id);
        if (autor is null)
            return NotFound();

        var temLivros = await _repository.HasLivrosAsync(id);
        if (temLivros)
            return BadRequest(new { status = 400, message = "Erro na exclusão: registro está sendo utilizado.", errors = Array.Empty<object>() });

        await _repository.DeleteAsync(autor);

        return NoContent();
    }
}
