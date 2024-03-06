using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos.FilmesDto;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;
/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[Controller]")]
public class FilmeController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para a criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarFilmeId), new { id = filme.Id }, filme);
    }

    /// <summary>
    /// Retorna uma lista de todos os filmes cadastrados no banco de dados
    /// </summary>
    /// <param name="skip">Número página</param>
    /// <param name="take">Quantidade de registro por página</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso busca seja feita com sucesso</response>    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadFilmeDto> RecuperarFilmes([FromQuery] int skip = 0, int take = 50, string? nomeFilme = null)
    {
        if (nomeFilme == null) 
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take).ToList());

        }

        return _mapper.Map<List<ReadFilmeDto>>(
            _context.Filmes.Skip(skip)
                           .Take(take)
                           .Where(filme => filme.Sessoes.Any(sessao => sessao.Cinema.Nome == nomeFilme))
                           .ToList());

    }

    /// <summary>
    /// Retorna um filme buscado pelo id
    /// </summary>
    /// <param name="id">Propriedade de identificação do filme a ser buscado</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a busca seja feita com sucesso</response>
    /// <response code="404">Caso não seja encontrado nenhum filme pelo id informardo</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecuperarFilmeId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }

    /// <summary>
    /// Atualiza um filme pelo id
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para atualização de um filme</param>
    /// <param name="id">Propriedade de identificação do filme a ser atualizado</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    /// <response code="404">Caso não seja encontrado nenhum filme pelo id informardo</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public IActionResult AtualizarFilme([FromBody] UpdateFilmeDto filmeDto, int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound(); 
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent(); 
    }

    /// <summary>
    /// Atualiza parcialmente um filme pelo id
    /// </summary>
    /// <param name="patch">Objeto com os campos necessários para atualização parcial de um filme</param>
    /// <param name="id">Propriedade de identificação do filme a ser atualizado parcialmente</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    /// <response code="404">Caso não seja encontrado nenhum filme pelo id informardo</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizarFilmeParcial([FromBody] JsonPatchDocument<UpdateFilmeDto> patch, int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        var filmeParaAtualizarDto = _mapper.Map<UpdateFilmeDto>(filme);
        
        patch.ApplyTo(filmeParaAtualizarDto, ModelState);

        if (!TryValidateModel(filmeParaAtualizarDto))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParaAtualizarDto, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deletar um filme pelo id
    /// </summary>
    /// <param name="id">Propriedade de identificação do filme a ser deletado</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    /// <response code="404">Caso não seja encontrado nenhum filme pelo id informardo</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletarFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        _context.Filmes.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
