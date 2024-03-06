using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos.CinemasDto
{
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage = "O nome do cinema é obrigatório")]
        public string? Nome { get; set; }
    }
}
