using AutoMapper;
using FilmesApi.Data.Dtos.FilmesDto;
using FilmesApi.Models;

namespace FilmesApi.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
        CreateMap<UpdateFilmeDto, Filme>();
        CreateMap<Filme, UpdateFilmeDto>();
        CreateMap<Filme, ReadFilmeDto>()
            .ForMember(readFilmeDto => readFilmeDto.Sessoes,
                opcao => opcao.MapFrom(cinema => cinema.Sessoes)); ;
    }
}
