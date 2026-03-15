using LibraryApi.DTOs.Autores;
using LibraryApi.Exceptions;
using LibraryApi.Models;
using LibraryApi.Repositories;

namespace LibraryApi.Services;

public class AutorService : IAutorService
{
    private readonly IAutorRepository _repository;

    public AutorService(IAutorRepository repository)
    {
        _repository = repository;
    }

    public async Task<AutorResponse?> GetByIdAsync(Guid id)
    {
        var autor = await _repository.GetByIdAsync(id);
        if (autor is null) return null;

        return MapToResponse(autor);
    }

    public async Task<IList<AutorResponse>> SearchAsync(string? nome, string? nacionalidade)
    {
        var autores = await _repository.SearchAsync(nome, nacionalidade);
        return autores.Select(MapToResponse).ToList();
    }

    public async Task<Guid> AddAsync(CadastroAutorRequest request)
    {
        var duplicado = await _repository.ExistsAsync(request.Nome, request.DataNascimento, request.Nacionalidade);
        if (duplicado)
            throw new RegistroDuplicadoException();

        var autor = new Autor
        {
            Nome = request.Nome,
            DataNascimento = request.DataNascimento,
            Nacionalidade = request.Nacionalidade,
            DataCadastro = DateTime.UtcNow
        };

        await _repository.AddAsync(autor);

        return autor.Id;
    }

    public async Task UpdateAsync(Guid id, CadastroAutorRequest request)
    {
        var autor = await _repository.GetByIdAsync(id);
        if (autor is null)
            throw new KeyNotFoundException();

        var duplicado = await _repository.ExistsAsync(request.Nome, request.DataNascimento, request.Nacionalidade, ignoreId: id);
        if (duplicado)
            throw new RegistroDuplicadoException();

        autor.Nome = request.Nome;
        autor.DataNascimento = request.DataNascimento;
        autor.Nacionalidade = request.Nacionalidade;
        autor.DataUltimaAtualizacao = DateTime.UtcNow;

        await _repository.UpdateAsync(autor);
    }

    public async Task DeleteAsync(Guid id)
    {
        var autor = await _repository.GetByIdAsync(id);
        if (autor is null)
            throw new KeyNotFoundException();

        var temLivros = await _repository.HasLivrosAsync(id);
        if (temLivros)
            throw new OperacaoNaoPermitidaException("Erro na exclusão: registro está sendo utilizado.");

        await _repository.DeleteAsync(autor);
    }

    private static AutorResponse MapToResponse(Autor autor) => new()
    {
        Id = autor.Id,
        Nome = autor.Nome,
        DataNascimento = autor.DataNascimento,
        Nacionalidade = autor.Nacionalidade
    };
}
