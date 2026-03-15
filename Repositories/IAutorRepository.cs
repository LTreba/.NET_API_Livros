using LibraryApi.Models;

namespace LibraryApi.Repositories;

public interface IAutorRepository
{
    Task<Autor?> GetByIdAsync(Guid id);
    Task<IList<Autor>> SearchAsync(string? nome, string? nacionalidade);
    Task AddAsync(Autor autor);
    Task UpdateAsync(Autor autor);
    Task DeleteAsync(Autor autor);
    Task<bool> ExistsAsync(string nome, DateOnly dataNascimento, string nacionalidade, Guid? ignoreId = null);
    Task<bool> HasLivrosAsync(Guid id);
}
