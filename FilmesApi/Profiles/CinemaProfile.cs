using AutoMapper;
using FilmesApi.Data.Dtos.CinemasDto;
using FilmesApi.Models;

namespace FilmesApi.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CreateCinemaDto, Cinema>();
        CreateMap<Cinema, ReadCinemaDto>()
            .ForMember(readCinemaDto => readCinemaDto.Endereco, 
                opcao => opcao.MapFrom(cinema => cinema.Endereco))
            .ForMember(readCinemaDto => readCinemaDto.Sessoes,
                opcao => opcao.MapFrom(cinema => cinema.Sessoes));
        CreateMap<UpdateCinemaDto, Cinema>();
        CreateMap<Cinema, UpdateCinemaDto>();
    }
}
