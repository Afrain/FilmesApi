using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos.FilmesDto;
/// <summary>
/// CreateFilmeDto
/// </summary>
public class CreateFilmeDto
{
    /// <summary>
    /// Titulo
    /// </summary>
    [Required(ErrorMessage = "O título do filme é obrigatório")]
    public string? Titulo { get; set; }

    /// <summary>
    /// Genero
    /// </summary>
    [Required(ErrorMessage = "O gênero do filme é obrigatório")]
    [StringLength(50, ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres")]
    public string? Genero { get; set; }

    /// <summary>
    /// Duracao
    /// </summary>
    [Required]
    [Range(70, 600, ErrorMessage = "A duração deve ser entre 70 e 600")]
    public int Duracao { get; set; }
}
