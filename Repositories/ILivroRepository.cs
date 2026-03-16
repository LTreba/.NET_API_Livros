using LibraryApi.Models;
using LibraryApi.Models.Enums;

namespace LibraryApi.Repositories;

public interface ILivroRepository
{
    Task<Livro?> GetByIdAsync(Guid id);
    Task<IList<Livro>> SearchAsync(string? isbn, string? titulo, string? nomeAutor, GeneroLivro? genero, int? anoPublicacao);
    Task AddAsync(Livro livro);
    Task UpdateAsync(Livro livro);
    Task DeleteAsync(Livro livro);
    Task<bool> ExistsIsbnAsync(string isbn, Guid? ignoreId = null);
}
