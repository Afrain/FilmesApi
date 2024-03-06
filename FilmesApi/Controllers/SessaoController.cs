using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos.SessaoDto;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class SessaoController : Controller
{
    private FilmeContext _context;
    private IMapper _mapper;

    public SessaoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult SalvarSessao([FromBody] CreateSessaoDto createSessaoDto)
    {
        var sessao = _mapper.Map<Sessao>(createSessaoDto);
        _context.Sessoes.Add(sessao);
        _context.SaveChanges();
        var sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
        return CreatedAtAction(nameof(RecuperarSessaoId), new { filmeId = sessaoDto.FilmeId, cinemaId = sessaoDto.CinemaId }, sessaoDto);
    }

    [HttpGet]
    public IEnumerable<ReadSessaoDto> RecuperaSessao([FromQuery] int skip = 0, int take = 50) 
    {
        var listaSessoes = _context.Sessoes.Skip(skip).Take(take).ToList();
        var listaSessoesDto = _mapper.Map<List<ReadSessaoDto>>(listaSessoes);
        return listaSessoesDto;
    }

    [HttpGet("{filmeId}/{cinemaId}")]
    public IActionResult RecuperarSessaoId(int filmeId, int cinemaId) 
    {
        var sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);
        if (sessao == null) return NotFound();
        var sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
        return Ok(sessaoDto);
    }
}
