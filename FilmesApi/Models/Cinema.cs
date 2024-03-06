using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

/// <summary>
/// 
/// </summary>
public class Cinema
{
    /// <summary>
    /// 
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string? Nome { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int EnderecoId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual Endereco? Endereco { get; set; }

    public virtual ICollection<Sessao> Sessoes { get; set; }
}
