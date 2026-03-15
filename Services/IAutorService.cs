using LibraryApi.DTOs.Autores;

namespace LibraryApi.Services;

public interface IAutorService
{
    Task<AutorResponse?> GetByIdAsync(Guid id);
    Task<IList<AutorResponse>> SearchAsync(string? nome, string? nacionalidade);
    Task<Guid> AddAsync(CadastroAutorRequest request);
    Task UpdateAsync(Guid id, CadastroAutorRequest request);
    Task DeleteAsync(Guid id);
}
