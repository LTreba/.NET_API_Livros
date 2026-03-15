using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs.Autores;

public class CadastroAutorRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data de nascimento é obrigatória")]
    public DateOnly DataNascimento { get; set; }

    [Required(ErrorMessage = "Nacionalidade é obrigatória")]
    [MaxLength(50, ErrorMessage = "Nacionalidade deve ter no máximo 50 caracteres")]
    public string Nacionalidade { get; set; } = string.Empty;
}
