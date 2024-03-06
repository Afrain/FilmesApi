using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos.EnderecosDto;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class EnderecoController : ControllerBase
    {

        private FilmeContext _context;
        private IMapper _mapper;

        public EnderecoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult SalvarEndereco([FromBody] CreateEnderecoDto createEnderecoDto)
        {
            var endereco = _mapper.Map<Endereco>(createEnderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarEnderecoId), new { id = endereco.Id }, endereco);
        }

        [HttpGet]
        public IEnumerable<ReadEnderecoDto> RecuperarEndereco([FromQuery] int skip = 0, int take = 50)
        {
            var listaEnderecoDto = _mapper.Map<List<ReadEnderecoDto>> (_context.Enderecos.Skip(skip).Take(take).ToList());
            return listaEnderecoDto;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarEnderecoId(int id) 
        {
            var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null) return NotFound();
            var enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
            return Ok(enderecoDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarFilme([FromBody] UpdateEnderecoDto enderecoDto, int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if(endereco == null) return NotFound();
            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarEnderecoParcial([FromBody] JsonPatchDocument<UpdateEnderecoDto> patch, int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null) return NotFound();

            var enderecoParaAtualizarDto = _mapper.Map<UpdateEnderecoDto>(endereco);

            patch.ApplyTo(enderecoParaAtualizarDto, ModelState);

            if (!TryValidateModel(enderecoParaAtualizarDto))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(enderecoParaAtualizarDto, endereco);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarEndereco(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null) return NotFound();

            _context.Enderecos.Remove(endereco);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
