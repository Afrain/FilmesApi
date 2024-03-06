using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos.CinemasDto;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class CinemaController : ControllerBase
{
    private readonly FilmeContext _context;
    private readonly IMapper _mapper;

    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult SalvarCinema([FromBody] CreateCinemaDto cinemaDto)
    {
        var cinema = _mapper.Map<Cinema>(cinemaDto);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarCinemaId), new { id = cinema.Id }, cinemaDto);
    }

    [HttpGet]
    public IEnumerable<ReadCinemaDto> RecuperarCinemas([FromQuery] int skip = 0, int take = 50)
    {
        var listaCinemaDto = _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.Skip(skip).Take(take).ToList());
        return listaCinemaDto;
    }

    [HttpGet("{id}")]
    public IActionResult RecuperarCinemaId(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null) return NotFound();
        var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
        return Ok(cinemaDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarCinema([FromBody] UpdateCinemaDto cinemaDto, int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if(cinema == null) return NotFound();
        _mapper.Map(cinemaDto, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizarCinemaParcial([FromBody] JsonPatchDocument<UpdateCinemaDto> patch, int id) 
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null) return NotFound();

        var cinemaParaAtualizarDto = _mapper.Map<UpdateCinemaDto>(cinema);

        patch.ApplyTo(cinemaParaAtualizarDto, ModelState);

        if (!TryValidateModel(cinemaParaAtualizarDto))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(cinemaParaAtualizarDto, cinema);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarCinema(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if(cinema == null) return NotFound();

        _context.Cinemas.Remove(cinema);
        _context.SaveChanges();
        return NoContent();
    }
}
