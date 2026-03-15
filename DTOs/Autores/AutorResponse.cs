namespace LibraryApi.DTOs.Autores;

public class AutorResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public string Nacionalidade { get; set; } = string.Empty;
}
