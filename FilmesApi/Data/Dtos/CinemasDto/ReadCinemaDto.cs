using FilmesApi.Data.Dtos.EnderecosDto;
using FilmesApi.Data.Dtos.SessaoDto;

namespace FilmesApi.Data.Dtos.CinemasDto
{
    public class ReadCinemaDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public ReadEnderecoDto Endereco { get; set; }
        public ICollection<ReadSessaoDto> Sessoes { get; set; }
    }
}
