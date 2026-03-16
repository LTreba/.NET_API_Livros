using System.ComponentModel.DataAnnotations;
using LibraryApi.Models.Enums;

namespace LibraryApi.DTOs.Livros;

public class CadastroLivroRequest
{
    [Required(ErrorMessage = "ISBN é obrigatório")]
    [MaxLength(20, ErrorMessage = "ISBN deve ter no máximo 20 caracteres")]
    public string ISBN { get; set; } = string.Empty;

    [Required(ErrorMessage = "Título é obrigatório")]
    [MaxLength(150, ErrorMessage = "Título deve ter no máximo 150 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data de publicação é obrigatória")]
    public DateOnly DataPublicacao { get; set; }

    public GeneroLivro? Genero { get; set; }

    public decimal? Preco { get; set; }

    [Required(ErrorMessage = "Autor é obrigatório")]
    public Guid IdAutor { get; set; }
}
