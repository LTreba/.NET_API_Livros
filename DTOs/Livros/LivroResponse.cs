using LibraryApi.DTOs.Autores;
using LibraryApi.Models.Enums;

namespace LibraryApi.DTOs.Livros;

public class LivroResponse
{
    public Guid Id { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public DateOnly DataPublicacao { get; set; }
    public GeneroLivro? Genero { get; set; }
    public decimal? Preco { get; set; }
    public AutorResponse Autor { get; set; } = null!;
}
