using SolutisHelpDesk.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SolutisHelpDesk.Data.DTOs;

public class CreateRespostaDto {
	[JsonIgnore]
	public DateTime Data { get; set; }
	[Required(ErrorMessage = "A mensagem é requerida")]
	public string Mensagem { get; set; }

	[JsonIgnore]
	public string? Autor { get; set; }

	[Required(ErrorMessage = "O Id do chamado é requerido")]
	public int ChamadoId { get; set; }

	public CreateRespostaDto() {
		Data = DateTime.Now;
	}
}
