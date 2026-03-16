using LibraryApi.DTOs.Autores;
using LibraryApi.DTOs.Livros;
using LibraryApi.Exceptions;
using LibraryApi.Models;
using LibraryApi.Models.Enums;
using LibraryApi.Repositories;

namespace LibraryApi.Services;

public class LivroService : ILivroService
{
    private readonly ILivroRepository _repository;
    private readonly IAutorRepository _autorRepository;

    public LivroService(ILivroRepository repository, IAutorRepository autorRepository)
    {
        _repository = repository;
        _autorRepository = autorRepository;
    }

    public async Task<LivroResponse?> GetByIdAsync(Guid id)
    {
        var livro = await _repository.GetByIdAsync(id);
        if (livro is null) return null;

        return MapToResponse(livro);
    }

    public async Task<IList<LivroResponse>> SearchAsync(string? isbn, string? titulo, string? nomeAutor, GeneroLivro? genero, int? anoPublicacao)
    {
        var livros = await _repository.SearchAsync(isbn, titulo, nomeAutor, genero, anoPublicacao);
        return livros.Select(MapToResponse).ToList();
    }

    public async Task<Guid> AddAsync(CadastroLivroRequest request)
    {
        var autor = await _autorRepository.GetByIdAsync(request.IdAutor);
        if (autor is null)
            throw new KeyNotFoundException("Autor não encontrado.");

        var isbnDuplicado = await _repository.ExistsIsbnAsync(request.ISBN);
        if (isbnDuplicado)
            throw new RegistroDuplicadoException();

        if (request.DataPublicacao > DateOnly.FromDateTime(DateTime.Today))
            throw new OperacaoNaoPermitidaException("Data de publicação não pode ser uma data futura.");

        if (request.DataPublicacao.Year >= 2020 && request.Preco is null)
            throw new OperacaoNaoPermitidaException("Preço é obrigatório para livros publicados a partir de 2020.");

        var livro = new Livro
        {
            ISBN = request.ISBN,
            Titulo = request.Titulo,
            DataPublicacao = request.DataPublicacao,
            Genero = request.Genero,
            Preco = request.Preco,
            AutorId = request.IdAutor,
            DataCadastro = DateTime.UtcNow
        };

        await _repository.AddAsync(livro);

        return livro.Id;
    }

    public async Task UpdateAsync(Guid id, CadastroLivroRequest request)
    {
        var livro = await _repository.GetByIdAsync(id);
        if (livro is null)
            throw new KeyNotFoundException();

        var autor = await _autorRepository.GetByIdAsync(request.IdAutor);
        if (autor is null)
            throw new KeyNotFoundException("Autor não encontrado.");

        var isbnDuplicado = await _repository.ExistsIsbnAsync(request.ISBN, ignoreId: id);
        if (isbnDuplicado)
            throw new RegistroDuplicadoException();

        if (request.DataPublicacao > DateOnly.FromDateTime(DateTime.Today))
            throw new OperacaoNaoPermitidaException("Data de publicação não pode ser uma data futura.");

        if (request.DataPublicacao.Year >= 2020 && request.Preco is null)
            throw new OperacaoNaoPermitidaException("Preço é obrigatório para livros publicados a partir de 2020.");

        livro.ISBN = request.ISBN;
        livro.Titulo = request.Titulo;
        livro.DataPublicacao = request.DataPublicacao;
        livro.Genero = request.Genero;
        livro.Preco = request.Preco;
        livro.AutorId = request.IdAutor;
        livro.DataUltimaAtualizacao = DateTime.UtcNow;

        await _repository.UpdateAsync(livro);
    }

    public async Task DeleteAsync(Guid id)
    {
        var livro = await _repository.GetByIdAsync(id);
        if (livro is null)
            throw new KeyNotFoundException();

        await _repository.DeleteAsync(livro);
    }

    private static LivroResponse MapToResponse(Livro livro) => new()
    {
        Id = livro.Id,
        ISBN = livro.ISBN,
        Titulo = livro.Titulo,
        DataPublicacao = livro.DataPublicacao,
        Genero = livro.Genero,
        Preco = livro.Preco,
        Autor = new AutorResponse
        {
            Id = livro.Autor.Id,
            Nome = livro.Autor.Nome,
            DataNascimento = livro.Autor.DataNascimento,
            Nacionalidade = livro.Autor.Nacionalidade
        }
    };
}
