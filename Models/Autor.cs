namespace LibraryApi.Models;

public class Autor
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public string Nacionalidade { get; set; } = string.Empty;

    // Auditoria
    public DateTime DataCadastro { get; set; }
    public DateTime? DataUltimaAtualizacao { get; set; }
    public string? UsuarioUltimaAtualizacao { get; set; }

    // Navegação
    public ICollection<Livro> Livros { get; set; } = [];
}
