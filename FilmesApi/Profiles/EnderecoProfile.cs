using AutoMapper;
using FilmesApi.Data.Dtos.EnderecosDto;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile()
        {
            CreateMap<CreateEnderecoDto, Endereco>();
            CreateMap<Endereco, ReadEnderecoDto>();
            CreateMap<UpdateEnderecoDto, Endereco>();
            CreateMap<Endereco, UpdateEnderecoDto>();
        }
    }
}
