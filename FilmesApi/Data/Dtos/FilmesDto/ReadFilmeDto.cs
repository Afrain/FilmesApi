using FilmesApi.Data.Dtos.SessaoDto;
using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos.FilmesDto
{
    public class ReadFilmeDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Genero { get; set; }
        public int Duracao { get; set; }
        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
        public ICollection<ReadSessaoDto> Sessoes { get; set; }

    }
}
