using LibraryApi.Models.Enums;

namespace LibraryApi.Models;

public class Livro
{
    public Guid Id { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public DateOnly DataPublicacao { get; set; }
    public GeneroLivro? Genero { get; set; }
    public decimal? Preco { get; set; }
    public Guid AutorId { get; set; }
    public Autor Autor { get; set; } = null!;
    public DateTime DataCadastro { get; set; }
    public DateTime? DataUltimaAtualizacao { get; set; }
    public string? UsuarioUltimaAtualizacao { get; set; }
}
