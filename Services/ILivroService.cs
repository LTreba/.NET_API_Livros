using LibraryApi.DTOs.Livros;
using LibraryApi.Models.Enums;

namespace LibraryApi.Services;

public interface ILivroService
{
    Task<LivroResponse?> GetByIdAsync(Guid id);
    Task<IList<LivroResponse>> SearchAsync(string? isbn, string? titulo, string? nomeAutor, GeneroLivro? genero, int? anoPublicacao);
    Task<Guid> AddAsync(CadastroLivroRequest request);
    Task UpdateAsync(Guid id, CadastroLivroRequest request);
    Task DeleteAsync(Guid id);
}
